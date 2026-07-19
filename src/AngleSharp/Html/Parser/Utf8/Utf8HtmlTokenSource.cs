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
    IAsyncHtmlTokenSource,
    IUtf8HtmlTokenSink,
    IUtf8HtmlStreamingCommentSink,
    IAsyncDisposable
{
    private const Int32 TokenCapacity = 3;
    private const Int32 StackDecodeThreshold = 512;

    private readonly IAsyncEnumerator<ReadOnlyMemory<Byte>> _input;
    private readonly Utf8HtmlTokenizer _tokenizer;
    private readonly Utf8TextAccumulator _text = new();
    private Utf8TextAccumulator? _comment;
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
    private Boolean _hasSkippedText;
    private Boolean _pendingAttributeNameIsDecoded;

    private Utf8TextAccumulator CommentAccumulator => _comment ??= new();

    public Utf8HtmlTokenSource(
        IAsyncEnumerable<ReadOnlyMemory<Byte>> input,
        CancellationToken cancellationToken = default
    ) : this(input, Utf8InputContract.ArbitraryBytes, cancellationToken) { }

    public Utf8HtmlTokenSource(
        IAsyncEnumerable<ReadOnlyMemory<Byte>> input,
        Utf8InputContract inputContract,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(input);
        _input = input.GetAsyncEnumerator(cancellationToken);
        _tokenizer = new Utf8HtmlTokenizer(this, inputContract)
        {
            IsModeControlledExternally = true,
        };
    }

    internal Utf8HtmlTokenizerCounters TokenizerCounters => _tokenizer.Counters;

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
            var remaining = _segment.Span.Slice(_segmentOffset);
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
        if (ShouldSkipText())
        {
            _hasSkippedText |= !utf8.IsEmpty;
        }
        else
        {
            _text.Append(utf8);
        }
    }

    public Utf8HtmlStartTagCapture StartTag(Utf8HtmlName name)
    {
        FlushText();
        var decoded = DecodeTagName(name);
        _lastStartTagName = decoded.Memory;
        _startTagSlot = ReserveSlot();
        _tokens[_startTagSlot].InitializeStartTag(decoded);
        _pendingAttributeName = default;
        _pendingAttributeNameIsDecoded = false;
        return Utf8HtmlStartTagCapture.Attributes;
    }

    public Boolean WantsAttribute(Utf8HtmlName name)
    {
        if (_options.EmitsAllAttributes)
        {
            return true;
        }

        var decoded = DecodeAttributeName(name);
        _pendingAttributeName = decoded.Memory;
        _pendingAttributeNameIsDecoded = true;
        return _options.ShouldEmitAttribute(ref GetStartTag(), _pendingAttributeName);
    }

    public void Attribute(Utf8HtmlName name, ReadOnlySpan<Byte> value)
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

    public void EndTag(Utf8HtmlName name) => EnqueueEndTag(name);

    public void Comment(ReadOnlySpan<Byte> utf8)
    {
        FlushText();
        var slot = ReserveSlot();
        _tokens[slot].InitializeComment(
            _options.SkipComments ? StringOrMemory.Empty : Decode(utf8),
            default
        );
        Enqueue(slot);
        _tokenizer.RequestYield();
    }

    Boolean IUtf8HtmlStreamingCommentSink.BeginComment() => !_options.SkipComments;

    void IUtf8HtmlStreamingCommentSink.CommentChunk(ReadOnlySpan<Byte> utf8) =>
        CommentAccumulator.Append(utf8);

    void IUtf8HtmlStreamingCommentSink.EndComment()
    {
        FlushText();
        var slot = ReserveSlot();
        _tokens[slot].InitializeComment(
            _options.SkipComments
                ? StringOrMemory.Empty
                : _comment?.Materialize() ?? StringOrMemory.Empty,
            default
        );
        Enqueue(slot);
        _tokenizer.RequestYield();
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
        _tokens[slot].InitializeEndOfFile(default);
        Enqueue(slot);
        _tokenizer.RequestYield();
    }

    private void EnqueueEndTag(Utf8HtmlName name)
    {
        FlushText();
        if (_state is HtmlParseMode.RCData or HtmlParseMode.Rawtext or HtmlParseMode.Script)
        {
            _state = HtmlParseMode.PCData;
        }
        var slot = ReserveSlot();
        _tokens[slot].InitializeEndTag(DecodeTagName(name));
        Enqueue(slot);
        _tokenizer.RequestYield();
    }

    private StringOrMemory DecodeTagName(Utf8HtmlName name) =>
        Utf8CanonicalNameProvider.TryGetTag(name, out var canonical)
            ? canonical
            : DecodeSemantic(name.Verbatim);

    private StringOrMemory DecodeAttributeName(Utf8HtmlName name)
    {
        if (_options.IsPreservingAttributeNames)
        {
            return Decode(name.Verbatim);
        }

        return Utf8CanonicalNameProvider.TryGetAttribute(name, out var canonical)
            ? canonical
            : DecodeSemantic(name.Verbatim);
    }

    private static StringOrMemory Decode(ReadOnlySpan<Byte> utf8)
    {
        if (utf8.IsEmpty)
        {
            return StringOrMemory.Empty;
        }

#if NET8_0_OR_GREATER
        if (utf8.Length <= StackDecodeThreshold)
        {
            Span<Char> characters = stackalloc Char[utf8.Length];
            var written = Encoding.UTF8.GetChars(utf8, characters);
            return new String(characters[..written]);
        }
#endif

        return Encoding.UTF8.GetString(utf8);
    }

    private static StringOrMemory DecodeSemantic(ReadOnlySpan<Byte> utf8)
    {
        var containsUppercaseAscii = false;
        foreach (var value in utf8)
        {
            if ((UInt32)(value - 'A') <= 'Z' - 'A')
            {
                containsUppercaseAscii = true;
                break;
            }
        }

        if (!containsUppercaseAscii)
        {
            return Decode(utf8);
        }

        if (utf8.Length <= StackDecodeThreshold)
        {
            Span<Byte> normalized = stackalloc Byte[utf8.Length];
            utf8.CopyTo(normalized);
            LowerAsciiInPlace(normalized);
            return Decode(normalized);
        }

        var bytes = ArrayPool<Byte>.Shared.Rent(utf8.Length);
        try
        {
            var normalized = bytes.AsSpan(0, utf8.Length);
            utf8.CopyTo(normalized);
            LowerAsciiInPlace(normalized);
            return Decode(normalized);
        }
        finally
        {
            ArrayPool<Byte>.Shared.Return(bytes);
        }
    }

    private static void LowerAsciiInPlace(Span<Byte> utf8)
    {
        for (var index = 0; index < utf8.Length; index++)
        {
            if ((UInt32)(utf8[index] - 'A') <= 'Z' - 'A')
            {
                utf8[index] += (Byte)('a' - 'A');
            }
        }
    }

    private void FlushText()
    {
        if (_hasSkippedText)
        {
            var skippedSlot = ReserveSlot();
            _tokens[skippedSlot].InitializeCharacter(StringOrMemory.Empty, default);
            Enqueue(skippedSlot);
            _hasSkippedText = false;
            return;
        }

        if (_text.IsEmpty)
        {
            return;
        }

        var slot = ReserveSlot();
        _tokens[slot].InitializeCharacter(_text.Materialize(), default);
        Enqueue(slot);
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
        _comment?.Dispose();
        _tokens = default;
        _ready = default;
        _lastStartTagName = default;
        _pendingAttributeName = default;
        await _input.DisposeAsync().ConfigureAwait(false);
    }

    private struct PooledByteBuffer : IDisposable
    {
        private const Int32 MinimumBufferSize = 4096;

        private Byte[]? _buffer;
        private Int32 _written;

        public Boolean IsEmpty => _written == 0;

        public Int32 WrittenCount => _written;

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

    private sealed class Utf8TextAccumulator : IDisposable
    {
        // Stay below the LOH threshold on the ordinary byte-buffer path.
        private const Int32 StreamingThreshold = 64 * 1024;

        private PooledByteBuffer _utf8;
        private PooledByteSequence? _sequence;

        public Boolean IsEmpty => (_sequence?.IsEmpty ?? true) && _utf8.IsEmpty;

        public void Append(ReadOnlySpan<Byte> utf8)
        {
            if (utf8.IsEmpty)
            {
                return;
            }

            if (
                (_sequence?.IsEmpty ?? true)
                && utf8.Length <= StreamingThreshold - _utf8.WrittenCount
            )
            {
                _utf8.Append(utf8);
                return;
            }

            EnsureSegmented();
            _sequence!.Append(utf8);
        }

        public StringOrMemory Materialize()
        {
            if (_sequence?.IsEmpty ?? true)
            {
                var result = DecodeUtf8(_utf8.WrittenSpan);
                _utf8.Clear();
                return result;
            }

            var sequenceBuffer = _sequence!;
            var sequence = sequenceBuffer.WrittenSequence;
            var text = Encoding.UTF8.GetString(in sequence);
            sequenceBuffer.Clear();
            return text;
        }

        private void EnsureSegmented()
        {
            if (!(_sequence?.IsEmpty ?? true))
            {
                return;
            }

            _sequence ??= new PooledByteSequence();
            _sequence.Append(_utf8.WrittenSpan);
            _utf8.Dispose();
        }

        private static StringOrMemory DecodeUtf8(ReadOnlySpan<Byte> utf8) =>
            Utf8HtmlTokenSource.Decode(utf8);

        public void Dispose()
        {
            _utf8.Dispose();
            _sequence?.Dispose();
        }
    }

    private sealed class PooledByteSequence : IDisposable
    {
        private const Int32 SegmentSize = 64 * 1024;

        private Segment? _first;
        private Segment? _last;

        public Boolean IsEmpty => _first is null;

        public ReadOnlySequence<Byte> WrittenSequence =>
            _first is null
                ? ReadOnlySequence<Byte>.Empty
                : new ReadOnlySequence<Byte>(_first, 0, _last!, _last!.WrittenCount);

        public void Append(ReadOnlySpan<Byte> value)
        {
            while (!value.IsEmpty)
            {
                if (_last is null || _last.Available == 0)
                {
                    AddSegment();
                }

                var written = _last!.Append(value);
                value = value.Slice(written);
            }
        }

        private void AddSegment()
        {
            var segment = new Segment(ArrayPool<Byte>.Shared.Rent(SegmentSize));
            if (_last is null)
            {
                _first = segment;
            }
            else
            {
                _last.SetNext(segment);
            }
            _last = segment;
        }

        public void Clear()
        {
            var segment = _first;
            while (segment is not null)
            {
                var next = segment.NextSegment;
                ArrayPool<Byte>.Shared.Return(segment.Buffer);
                segment = next;
            }
            _first = null;
            _last = null;
        }

        public void Dispose() => Clear();

        private sealed class Segment(Byte[] buffer) : ReadOnlySequenceSegment<Byte>
        {
            public Byte[] Buffer { get; } = buffer;

            public Int32 WrittenCount { get; private set; }

            public Int32 Available => Buffer.Length - WrittenCount;

            public Segment? NextSegment => (Segment?)Next;

            public Int32 Append(ReadOnlySpan<Byte> value)
            {
                var length = Math.Min(Available, value.Length);
                value.Slice(0, length).CopyTo(Buffer.AsSpan(WrittenCount));
                WrittenCount += length;
                Memory = Buffer.AsMemory(0, WrittenCount);
                return length;
            }

            public void SetNext(Segment next)
            {
                next.RunningIndex = RunningIndex + WrittenCount;
                Next = next;
            }
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
