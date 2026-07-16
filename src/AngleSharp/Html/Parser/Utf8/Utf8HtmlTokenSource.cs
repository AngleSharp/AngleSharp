using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Common;
using AngleSharp.Html.Parser.Tokens;
using AngleSharp.Html.Parser.Tokens.Struct;
using AngleSharp.Text;

namespace AngleSharp.Html.Parser.Utf8;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Adapts the borrowed UTF-8 tokenizer to the existing token-at-a-time tree constructor.
/// Payloads are decoded directly to the strings ultimately retained by the mutable DOM.
/// </summary>
internal sealed class Utf8HtmlTokenSource :
    IHtmlTokenSource,
    IOptimizedUtf8HtmlTokenSink,
    IAsyncDisposable
{
    private const Int32 TokenCapacity = 3;

    private readonly IAsyncEnumerator<ReadOnlyMemory<Byte>> _input;
    private readonly Utf8HtmlTokenizer _tokenizer;
    private readonly PooledByteBuffer _text = new();
    private TokenBuffer _tokens;
    private ReadyBuffer _ready;
    private ReadOnlyMemory<Byte> _segment;
    private ReadOnlyMemory<Char> _lastStartTagName;
    private ReadOnlyMemory<Char> _pendingAttributeName;
    private HtmlTokenizerOptions _options;
    private HtmlParseMode _state;
    private Int32 _segmentOffset;
    private Int32 _startTagSlot = -1;
    private Int32 _readyRead;
    private Int32 _readyCount;
    private Byte _usedSlots;
    private Boolean _inputCompleted;
    private Boolean _disposed;
    private Boolean _hasCurrent;
    private Boolean _pendingAttributeNameIsDecoded;

    public Utf8HtmlTokenSource(
        IAsyncEnumerable<ReadOnlyMemory<Byte>> input,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(input);
        _input = input.GetAsyncEnumerator(cancellationToken);
        _tokenizer = new Utf8HtmlTokenizer(this) { IsModeControlledExternally = true };
    }

    public void Configure(
        HtmlTokenizerOptions options,
        Action<HtmlToken, TextRange>? onToken,
        Action<HtmlParseError, TextPosition> reportError)
    {
        _options = options;
    }

    public void SetState(HtmlParseMode state)
    {
        _state = state;
        String? contextTagName;
        if (state is HtmlParseMode.PCData or HtmlParseMode.Plaintext || _lastStartTagName.IsEmpty)
        {
            contextTagName = null;
        }
        else
        {
            contextTagName = _lastStartTagName.ToString();
        }
        _tokenizer.SetMode(state, contextTagName);
    }

    public void SetAcceptingCharacterData(Boolean value) => _tokenizer.IsAcceptingCharacterData = value;

    public ref StructHtmlToken Current
    {
        get
        {
            if (!_hasCurrent)
            {
                throw new InvalidOperationException("The token source has no current token.");
            }

            return ref _tokens[_ready[_readyRead]];
        }
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowNoProgress() => throw new InvalidOperationException("The UTF-8 tokenizer did not consume input or produce a token.");

    public Boolean TryMoveNext()
    {
        ReleaseCurrent();

        if (_readyCount != 0)
        {
            _hasCurrent = true;
            return true;
        }

        while (_segmentOffset < _segment.Length)
        {
            var remaining = _segment.Span[_segmentOffset..];
            var consumed = _tokenizer.WriteUntilYield(remaining);

            if (consumed <= 0)
            {
                ThrowNoProgress();
            }

            _segmentOffset += consumed;

            if (_readyCount != 0)
            {
                _hasCurrent = true;
                return true;
            }
        }

        return false;
    }

    public Task WaitForInputAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        while (!_inputCompleted && _segmentOffset >= _segment.Length)
        {
            var moveNext = _input.MoveNextAsync();
            if (!moveNext.IsCompletedSuccessfully)
            {
                return WaitForInputSlowAsync(moveNext);
            }

            if (AcceptInput(moveNext.Result))
            {
                break;
            }
        }

        return Task.CompletedTask;
    }

    private async Task WaitForInputSlowAsync(ValueTask<Boolean> moveNext)
    {
        while (!_inputCompleted && _segmentOffset >= _segment.Length)
        {
            if (AcceptInput(await moveNext.ConfigureAwait(false)))
            {
                return;
            }

            moveNext = _input.MoveNextAsync();
        }
    }

    private Boolean AcceptInput(Boolean hasInput)
    {
        if (!hasInput)
        {
            _inputCompleted = true;
            _tokenizer.Complete();
            return true;
        }

        _segment = _input.Current;
        _segmentOffset = 0;
        return !_segment.IsEmpty;
    }

    public void Text(ReadOnlySpan<Byte> utf8)
    {
        if (!ShouldSkipText())
        {
            _text.Append(utf8);
        }
    }

    public void StartTag(ReadOnlySpan<Byte> name) => StartTag(name, Utf8NameHash.Compute(name));

    public void StartTag(ReadOnlySpan<Byte> name, UInt64 hash)
    {
        FlushText();
        var decoded = DecodeTagName(name, hash);
        _lastStartTagName = decoded.Memory;
        _startTagSlot = ReserveSlot();
        _tokens[_startTagSlot] = StructHtmlToken.Open(decoded);
        _pendingAttributeName = default;
        _pendingAttributeNameIsDecoded = false;
    }

    public Boolean WantsAttribute(ReadOnlySpan<Byte> name)
    {
        var decoded = DecodeAttributeName(name);
        _pendingAttributeName = decoded.Memory;
        _pendingAttributeNameIsDecoded = true;
        return _options.ShouldEmitAttribute(ref GetStartTag(), _pendingAttributeName);
    }

    public void Attribute(ReadOnlySpan<Byte> name, ReadOnlySpan<Byte> value)
    {
        var decodedName = _pendingAttributeNameIsDecoded
            ? new StringOrMemory(_pendingAttributeName)
            : DecodeAttributeName(name);
        GetStartTag().AddAttribute(decodedName, Decode(value));
        _pendingAttributeName = default;
        _pendingAttributeNameIsDecoded = false;
    }

    public void StartTagEnd(Boolean selfClosing)
    {
        GetStartTag().IsSelfClosing = selfClosing;
        Enqueue(_startTagSlot);
        _startTagSlot = -1;
        _tokenizer.RequestYield();
    }

    public void EndTag(ReadOnlySpan<Byte> name) => EndTag(name, Utf8NameHash.Compute(name));

    public void EndTag(ReadOnlySpan<Byte> name, UInt64 hash) =>
        EnqueueEndTag(name, hash);

    public void Comment(ReadOnlySpan<Byte> utf8)
    {
        FlushText();
        if (!_options.SkipComments)
        {
            var slot = ReserveSlot();
            _tokens[slot] = StructHtmlToken.Comment(Decode(utf8), default);
            Enqueue(slot);
            _tokenizer.RequestYield();
        }
    }

    public void Doctype(ReadOnlySpan<Byte> utf8)
    {
        FlushText();
        var token = StructHtmlToken.Doctype(quirksForced: false, default);
        token.Name = Decode(utf8);
        var slot = ReserveSlot();
        _tokens[slot] = token;
        Enqueue(slot);
        _tokenizer.RequestYield();
    }

    public void Doctype(in Utf8DoctypeToken doctype)
    {
        FlushText();
        var token = StructHtmlToken.Doctype(doctype.IsQuirksForced, default);
        token.Name = Decode(doctype.Name);
        if (!doctype.IsPublicIdentifierMissing)
        {
            token.PublicIdentifier = Decode(doctype.PublicIdentifier);
        }
        if (!doctype.IsSystemIdentifierMissing)
        {
            token.SystemIdentifier = Decode(doctype.SystemIdentifier);
        }
        var slot = ReserveSlot();
        _tokens[slot] = token;
        Enqueue(slot);
        _tokenizer.RequestYield();
    }

    public void EndOfFile()
    {
        FlushText();
        var slot = ReserveSlot();
        _tokens[slot] = StructHtmlToken.EndOfFile(default);
        Enqueue(slot);
        _tokenizer.RequestYield();
    }

    private void EnqueueEndTag(ReadOnlySpan<Byte> name, UInt64 hash)
    {
        FlushText();
        var slot = ReserveSlot();
        _tokens[slot] = StructHtmlToken.Close(DecodeTagName(name, hash));
        Enqueue(slot);
        _tokenizer.RequestYield();
    }

    private StringOrMemory DecodeTagName(ReadOnlySpan<Byte> name, UInt64 hash) =>
        Utf8CanonicalNameProvider.TryGetTag(name, hash, out var canonical) ? canonical : Decode(name);

    private StringOrMemory DecodeAttributeName(ReadOnlySpan<Byte> name) =>
        Utf8CanonicalNameProvider.TryGetAttribute(name, out var canonical) ? canonical : Decode(name);

    private static StringOrMemory Decode(ReadOnlySpan<Byte> utf8) =>
        utf8.IsEmpty ? StringOrMemory.Empty : Encoding.UTF8.GetString(utf8);

    private void FlushText()
    {
        if (_text.IsEmpty)
        {
            return;
        }

        var slot = ReserveSlot();
        _tokens[slot] = StructHtmlToken.Character(Decode(_text.WrittenSpan), default);
        Enqueue(slot);
        _text.Clear();
    }

    private ref StructHtmlToken GetStartTag()
    {
        if (_startTagSlot < 0)
        {
            throw new InvalidOperationException("The tokenizer has no pending start tag.");
        }

        return ref _tokens[_startTagSlot];
    }

    private Int32 ReserveSlot()
    {
        for (var slot = 0; slot < TokenCapacity; slot++)
        {
            var mask = (Byte)(1 << slot);
            if ((_usedSlots & mask) == 0)
            {
                _usedSlots |= mask;
                return slot;
            }
        }

        throw new InvalidOperationException("The UTF-8 tokenizer emitted more than three outstanding tokens.");
    }

    private void Enqueue(Int32 slot)
    {
        if (_readyCount == TokenCapacity)
        {
            throw new InvalidOperationException("The UTF-8 tokenizer emitted more than three ready tokens.");
        }

        _ready[(_readyRead + _readyCount) % TokenCapacity] = (Byte)slot;
        _readyCount++;
    }

    private void ReleaseCurrent()
    {
        if (!_hasCurrent)
        {
            return;
        }

        var slot = _ready[_readyRead];
        _readyRead = (_readyRead + 1) % TokenCapacity;
        _readyCount--;
        _usedSlots &= (Byte)~(1 << slot);
        _hasCurrent = false;
    }

    private Boolean ShouldSkipText() => _state switch
    {
        HtmlParseMode.Rawtext => _options.SkipRawText,
        HtmlParseMode.Script => _options.SkipScriptText,
        HtmlParseMode.Plaintext => _options.SkipPlaintext,
        HtmlParseMode.RCData => _options.SkipRCDataText,
        _ => _options.SkipDataText,
    };

    public async ValueTask DisposeAsync()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _text.Dispose();
        _tokens = default;
        _ready = default;
        _lastStartTagName = default;
        _pendingAttributeName = default;
        await _input.DisposeAsync().ConfigureAwait(false);
    }

    private sealed class PooledByteBuffer : IDisposable
    {
        private const Int32 MinimumBufferSize = 4096;

        private Byte[]? _buffer;
        private Int32 _written;

        public Boolean IsEmpty => _written == 0;

        public ReadOnlySpan<Byte> WrittenSpan => _buffer.AsSpan(0, _written);

        public void Append(ReadOnlySpan<Byte> value)
        {
            EnsureCapacity(value.Length);
            value.CopyTo(_buffer.AsSpan(_written));
            _written += value.Length;
        }

        public void Clear() => _written = 0;

        private void EnsureCapacity(Int32 additional)
        {
            if (_buffer is not null && additional <= _buffer.Length - _written)
            {
                return;
            }

            var next = ArrayPool<Byte>.Shared.Rent(Math.Max(MinimumBufferSize, checked(_written + additional)));
            if (_written != 0)
            {
                _buffer.AsSpan(0, _written).CopyTo(next);
            }
            if (_buffer is not null)
            {
                ArrayPool<Byte>.Shared.Return(_buffer);
            }
            _buffer = next;
        }

        public void Dispose()
        {
            if (_buffer is not null)
            {
                ArrayPool<Byte>.Shared.Return(_buffer);
                _buffer = null;
            }
            _written = 0;
        }
    }

    [InlineArray(TokenCapacity)]
    private struct TokenBuffer
    {
        private StructHtmlToken _element0;
    }

    [InlineArray(TokenCapacity)]
    private struct ReadyBuffer
    {
        private Byte _element0;
    }
}
