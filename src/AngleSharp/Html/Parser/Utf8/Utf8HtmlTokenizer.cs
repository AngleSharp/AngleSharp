#pragma warning disable CS1591 // Experimental API surface; shape is intentionally unsettled.

using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Common;

namespace AngleSharp.Html.Parser.Utf8;

/// <summary>
/// Experimental monotonic UTF-8 tokenizer kernel for read-only construction. HTML syntax is scanned as ASCII bytes;
/// source text is passed directly to the sink and only token parts crossing callbacks use reusable buffers.
/// </summary>
/// <remarks>
/// Token shapes are covered by the pinned html5lib tokenizer corpus across contiguous and segmented UTF-8 input.
/// Parse-error reporting and source positions remain separate conformance work.
/// </remarks>
public sealed class Utf8HtmlTokenizer
{
    private enum State : byte
    {
        Data,
        Plaintext,
        TagOpen,
        EndTagOpen,
        TagName,
        BeforeAttributeName,
        AttributeName,
        AfterAttributeName,
        BeforeAttributeValue,
        AttributeValueDoubleQuoted,
        AttributeValueSingleQuoted,
        AttributeValueUnquoted,
        AfterAttributeValueQuoted,
        SelfClosingStartTag,
        MarkupDeclaration,
        CommentStart,
        CommentStartDash,
        Comment,
        CommentLessThan,
        CommentLessThanBang,
        CommentLessThanBangDash,
        CommentLessThanBangDashDash,
        CommentEndDash,
        CommentEnd,
        CommentEndBang,
        BogusComment,
        Doctype,
        CharacterReference,
        CDataSection,
        CDataSectionBracket,
        CDataSectionEnd,
        RawText,
        RawLessThan,
        RawEndTagOpen,
        RawEndTagName,
        ScriptData,
        ScriptLessThan,
        ScriptEndTagName,
        ScriptEscapeStart,
        ScriptEscapeStartDash,
        ScriptEscaped,
        ScriptEscapedDash,
        ScriptEscapedDashDash,
        ScriptEscapedLessThan,
        ScriptEscapedEndTagName,
        ScriptDoubleEscapeStart,
        ScriptDoubleEscaped,
        ScriptDoubleEscapedDash,
        ScriptDoubleEscapedDashDash,
        ScriptDoubleEscapedLessThan,
        ScriptDoubleEscapeEnd,
    }

    private enum DoctypeState : byte
    {
        BeforeName,
        Name,
        AfterName,
        AfterPublicKeyword,
        BeforePublicIdentifier,
        PublicIdentifierDoubleQuoted,
        PublicIdentifierSingleQuoted,
        AfterPublicIdentifier,
        BetweenPublicAndSystemIdentifiers,
        AfterSystemKeyword,
        BeforeSystemIdentifier,
        SystemIdentifierDoubleQuoted,
        SystemIdentifierSingleQuoted,
        AfterSystemIdentifier,
        Bogus,
    }

    private static readonly SearchValues<Byte> DataTextTerminators = SearchValues.Create(
        "<&\0\r"u8
    );
    private static readonly SearchValues<Byte> RawTextTerminators = SearchValues.Create("<\0\r"u8);

    private readonly Utf8HtmlTokenizerStateMetrics? _stateMetrics;
    private readonly ArrayBufferWriter<Byte> _name = new(32);
    private readonly ArrayBufferWriter<Byte> _attributeName = new(32);
    private readonly ArrayBufferWriter<Byte> _attributeValue = new(128);
    private readonly ArrayBufferWriter<Byte> _seenAttributeNames = new(128);
    private readonly ArrayBufferWriter<Byte> _candidate = new(64);
    private readonly ArrayBufferWriter<Byte> _doctypePublic = new(64);
    private readonly ArrayBufferWriter<Byte> _doctypeSystem = new(64);
    private readonly Byte[] _utf8Carry = new Byte[4];
    private State _state;
    private State _returnState;
    private Boolean _isEndTag;
    private Boolean _startTagEmitted;
    private Boolean _pendingCarriageReturn;
    private String? _rawEndTag;
    private Int64 _bytesConsumed;
    private Int64 _segments;
    private Int64 _reconsumes;
    private Int64 _bufferedTokenBytes;
    private Int32 _maximumBufferedTokenBytes;
    private Int32 _validatedAsciiPrefixLength;
    private Int32 _utf8CarryLength;
    private Int32 _textUtf8CarryLength;
    private Int32 _textUtf8ExpectedLength;
    private UInt32 _textUtf8Carry;
    private Boolean _numericReferenceOverflow;
    private Boolean _numericReferenceHasDigits;
    private UInt32 _numericReferenceValue;
    private Boolean _yieldRequested;
    private Boolean _completed;
    private UInt64 _tagHash;
    private Boolean _attributeCaptureDecided;
    private Boolean _captureAttributeValue = true;
    private readonly Int32 _maximumBufferedTokenBytesAllowed;
    private readonly Int64 _maximumInputBytesAllowed;
    private readonly IUtf8HtmlTokenSink _sink;

    public Utf8HtmlTokenizer(IUtf8HtmlTokenSink sink)
        : this(sink, null, HtmlStreamingLimits.Default, countInputBytes: true) { }

    public Utf8HtmlTokenizer(IUtf8HtmlTokenSink sink, HtmlStreamingLimits limits)
        : this(sink, null, limits, countInputBytes: true) { }

    public Utf8HtmlTokenizer(IUtf8HtmlTokenSink sink, Utf8HtmlTokenizerStateMetrics? stateMetrics)
        : this(sink, stateMetrics, HtmlStreamingLimits.Default, countInputBytes: true) { }

    public Utf8HtmlTokenizer(
        IUtf8HtmlTokenSink sink,
        Utf8HtmlTokenizerStateMetrics? stateMetrics,
        HtmlStreamingLimits limits,
        Boolean countInputBytes
    )
    {
        ArgumentNullException.ThrowIfNull(limits);

        _sink = Adapt(sink);
        _stateMetrics = stateMetrics;
        _maximumBufferedTokenBytesAllowed = limits.MaximumBufferedTokenBytes;
        _maximumInputBytesAllowed = countInputBytes ? limits.MaximumInputBytes : Int64.MaxValue;
    }

    public static Int32 StateCount => Enum.GetValues<State>().Length;

    public IReadOnlyList<Utf8HtmlTokenizerStateMetric> GetStateMetrics() =>
        _stateMetrics?.Snapshot(Enum.GetNames<State>()) ?? [];

    public Utf8HtmlTokenizerCounters Counters =>
        new(_bytesConsumed, _segments, _reconsumes, 0, _maximumBufferedTokenBytes);

    /// <summary>
    /// Applies the tokenizer state selected by an external tree constructor.
    /// </summary>
    public void SetMode(HtmlParseMode mode, String? contextTagName)
    {
        _rawEndTag = mode switch
        {
            HtmlParseMode.RCData => "rcdata:" + (contextTagName ?? "\0"),
            HtmlParseMode.Rawtext => contextTagName ?? "\0",
            HtmlParseMode.Script => contextTagName ?? "script",
            HtmlParseMode.Plaintext => "\0",
            _ => null,
        };
        _state = mode switch
        {
            HtmlParseMode.RCData or HtmlParseMode.Rawtext => State.RawText,
            HtmlParseMode.Script => State.ScriptData,
            HtmlParseMode.Plaintext => State.Plaintext,
            _ => State.Data,
        };
    }

    public Boolean IsAcceptingCharacterData { get; set; }

    public Boolean IsModeControlledExternally { get; set; }

    /// <summary>
    /// Enters the CDATA section state after the tree constructor accepts a CDATA declaration in foreign content.
    /// </summary>
    public void EnterCDataSection() => _state = State.CDataSection;

    public void Write(ReadOnlyMemory<Byte> utf8)
    {
        ThrowIfCompleted();
        _segments++;
        Write(utf8.Span);
    }

    public void Write(ReadOnlySpan<Byte> utf8) => WriteCore(utf8, yieldOnRequest: false);

    /// <summary>
    /// Consumes input until the sink requests a yield. The caller must resubmit the unconsumed suffix before offering
    /// unrelated input.
    /// </summary>
    /// <returns>The number of bytes consumed from <paramref name="utf8"/>.</returns>
    internal Int32 WriteUntilYield(ReadOnlySpan<Byte> utf8)
    {
        _yieldRequested = false;
        return WriteCore(utf8, yieldOnRequest: true);
    }

    internal void RequestYield() => _yieldRequested = true;

    private Int32 WriteCore(ReadOnlySpan<Byte> utf8, Boolean yieldOnRequest)
    {
        ThrowIfCompleted();
        var previousBytesConsumed = _bytesConsumed;
        var observedInputBytes = SaturatingAdd(_bytesConsumed, utf8.Length);
        if (observedInputBytes > _maximumInputBytesAllowed)
        {
            ThrowLimitExceeded(
                HtmlStreamingLimit.InputBytes,
                _maximumInputBytesAllowed,
                observedInputBytes
            );
        }

        _bytesConsumed = observedInputBytes;
        var index = 0;
        while (_utf8CarryLength != 0)
        {
            var status = Rune.DecodeFromUtf8(
                _utf8Carry.AsSpan(0, _utf8CarryLength),
                out _,
                out var consumed
            );
            if (status == OperationStatus.Done)
            {
                WriteValidUtf8(_utf8Carry.AsSpan(0, consumed), yieldOnRequest);
                _utf8CarryLength = 0;
                break;
            }
            if (status == OperationStatus.InvalidData)
            {
                WriteValidUtf8("\uFFFD"u8, yieldOnRequest);
                ShiftUtf8Carry(Math.Max(consumed, 1));
                continue;
            }
            if (index == utf8.Length)
            {
                return index;
            }

            _utf8Carry[_utf8CarryLength++] = utf8[index++];
        }

        while (index < utf8.Length)
        {
            var asciiStart = index;
            var usedValidatedPrefix = _validatedAsciiPrefixLength != 0;
            if (usedValidatedPrefix)
            {
                index += Math.Min(_validatedAsciiPrefixLength, utf8.Length - index);
            }
            else
            {
                var remaining = utf8[index..];
                var nonAscii = remaining.IndexOfAnyExceptInRange((Byte)0x00, (Byte)0x7F);
                if (nonAscii < 0)
                {
                    nonAscii = remaining.Length;
                }

                index += nonAscii;
            }

            if (index != asciiStart)
            {
                var asciiLength = index - asciiStart;
                var asciiConsumed = WriteValidUtf8(utf8[asciiStart..index], yieldOnRequest);
                if (usedValidatedPrefix)
                {
                    _validatedAsciiPrefixLength -= asciiConsumed;
                }
                else if (asciiConsumed != asciiLength)
                {
                    _validatedAsciiPrefixLength = asciiLength - asciiConsumed;
                }

                if (asciiConsumed != asciiLength)
                {
                    index = asciiStart + asciiConsumed;
                    _bytesConsumed = SaturatingAdd(previousBytesConsumed, index);
                    return index;
                }
            }

            if (index == utf8.Length)
            {
                break;
            }

            var status = Rune.DecodeFromUtf8(utf8[index..], out _, out var consumed);
            if (status == OperationStatus.Done)
            {
                WriteValidUtf8(utf8.Slice(index, consumed), yieldOnRequest);
                index += consumed;
            }
            else if (status == OperationStatus.InvalidData)
            {
                WriteValidUtf8("\uFFFD"u8, yieldOnRequest);
                index += Math.Max(consumed, 1);
            }
            else
            {
                utf8[index..].CopyTo(_utf8Carry);
                _utf8CarryLength = utf8.Length - index;
                break;
            }
        }

        return index;
    }

    private Int32 WriteValidUtf8(ReadOnlySpan<Byte> utf8, Boolean yieldOnRequest)
    {
        var index = 0;
        while (index < utf8.Length)
        {
            if (_state == State.TagName)
            {
                var remaining = utf8[index..];
                var run = remaining.IndexOfAnyExceptInRange((Byte)'a', (Byte)'z');
                if (run < 0)
                {
                    run = remaining.Length;
                }

                if (run > 0)
                {
                    _stateMetrics?.Record((Int32)_state, run);
                    AppendTagName(remaining[..run]);
                    index += run;
                    continue;
                }
            }
            else if (
                _state is State.Data or State.RawText or State.ScriptData or State.Plaintext
                && !_pendingCarriageReturn
                && _textUtf8CarryLength == 0
            )
            {
                var run =
                    _state == State.Plaintext
                        ? FindPlaintextTerminator(utf8[index..])
                        : FindTextTerminator(utf8[index..], _state == State.Data || IsRcData());
                if (run > 0)
                {
                    _stateMetrics?.Record((Int32)_state, run);
                    var text = utf8.Slice(index, run);
                    var safeLength =
                        run == utf8.Length - index ? CompleteUtf8PrefixLength(text) : run;
                    if (safeLength > 0)
                    {
                        _sink.Text(text[..safeLength]);
                    }

                    if (safeLength != run)
                    {
                        text[safeLength..].CopyTo(_utf8Carry);
                        _utf8CarryLength = run - safeLength;
                    }
                    index += run;
                    continue;
                }
            }
            else if (
                _state is State.AttributeValueDoubleQuoted or State.AttributeValueSingleQuoted
                && !_pendingCarriageReturn
            )
            {
                var remaining = utf8[index..];
                var quote = _state == State.AttributeValueDoubleQuoted ? (Byte)'"' : (Byte)'\'';
                var run = FindQuotedAttributeValueTerminator(remaining, quote);
                if (run > 0)
                {
                    _stateMetrics?.Record((Int32)_state, run);
                    if (_captureAttributeValue)
                    {
                        Append(_attributeValue, remaining[..run]);
                    }

                    index += run;
                    continue;
                }
            }
            else if (_state == State.AttributeValueUnquoted && !_pendingCarriageReturn)
            {
                var remaining = utf8[index..];
                var run = FindUnquotedAttributeValueTerminator(remaining);
                if (run > 0)
                {
                    _stateMetrics?.Record((Int32)_state, run);
                    if (_captureAttributeValue)
                    {
                        Append(_attributeValue, remaining[..run]);
                    }

                    index += run;
                    continue;
                }
            }
            else if (_state == State.Comment && !_pendingCarriageReturn)
            {
                var remaining = utf8[index..];
                var run = FindCommentTerminator(remaining);
                if (run > 0)
                {
                    _stateMetrics?.Record((Int32)_state, run);
                    Append(_candidate, remaining[..run]);
                    index += run;
                    continue;
                }
            }
            var value = utf8[index++];
            if (_pendingCarriageReturn)
            {
                _pendingCarriageReturn = false;
                if (value == (Byte)'\n')
                {
                    continue;
                }
            }
            if (value == (Byte)'\r')
            {
                _pendingCarriageReturn = true;
                value = (Byte)'\n';
            }
            Process(value);
            if (yieldOnRequest && _yieldRequested)
            {
                return index;
            }
        }

        return index;
    }

    public void Complete()
    {
        if (_completed)
        {
            return;
        }

        if (_utf8CarryLength != 0)
        {
            EmitReplacementCharacter();
            _utf8CarryLength = 0;
        }
        switch (_state)
        {
            case State.TagOpen:
                _sink.Text("<"u8);
                break;
            case State.EndTagOpen:
                _sink.Text("</"u8);
                break;
            case State.CharacterReference:
                ResolveCharacterReference();
                break;
            case State.CDataSectionBracket:
                _sink.Text("]"u8);
                break;
            case State.CDataSectionEnd:
                _sink.Text("]]"u8);
                break;
            case State.RawLessThan:
            case State.RawEndTagOpen:
            case State.RawEndTagName:
                _sink.Text(_candidate.WrittenSpan);
                break;
            case State.CommentStart:
            case State.CommentStartDash:
            case State.Comment:
            case State.CommentLessThan:
            case State.CommentLessThanBang:
            case State.CommentLessThanBangDash:
            case State.CommentLessThanBangDashDash:
            case State.CommentEndDash:
            case State.CommentEnd:
            case State.CommentEndBang:
            case State.BogusComment:
            case State.MarkupDeclaration:
                _sink.Comment(_candidate.WrittenSpan);
                break;
            case State.Doctype:
                EmitDoctype(forceEofQuirks: true);
                break;
            // EOF in a tag discards the incomplete token.
            case State.TagName:
            case State.BeforeAttributeName:
            case State.AttributeName:
            case State.AfterAttributeName:
            case State.BeforeAttributeValue:
            case State.AttributeValueDoubleQuoted:
            case State.AttributeValueSingleQuoted:
            case State.AttributeValueUnquoted:
            case State.AfterAttributeValueQuoted:
            case State.SelfClosingStartTag:
                break;
        }

        _sink.EndOfFile();
        _completed = true;
    }

    public static async ValueTask<Utf8HtmlTokenizerCounters> TokenizeAsync(
        PipeReader reader,
        IUtf8HtmlTokenSink sink,
        CancellationToken cancellationToken = default,
        HtmlStreamingLimits? limits = null
    )
    {
        ArgumentNullException.ThrowIfNull(reader);
        var tokenizer = new Utf8HtmlTokenizer(sink, limits ?? HtmlStreamingLimits.Default);
        while (true)
        {
            var result = await reader.ReadAsync(cancellationToken).ConfigureAwait(false);
            var buffer = result.Buffer;
            if (result.IsCanceled)
            {
                reader.AdvanceTo(buffer.Start, buffer.End);
                throw new OperationCanceledException(cancellationToken);
            }
            try
            {
                foreach (var segment in buffer)
                {
                    tokenizer.Write(segment);
                }
            }
            finally
            {
                reader.AdvanceTo(buffer.End);
            }
            if (result.IsCompleted)
            {
                break;
            }
        }
        tokenizer.Complete();
        return tokenizer.Counters;
    }

    private static IUtf8HtmlTokenSink Adapt(IUtf8HtmlTokenSink sink)
    {
        ArgumentNullException.ThrowIfNull(sink);
        return sink as IUtf8HtmlTokenSink ?? new BasicSinkAdapter(sink);
    }

    private sealed class BasicSinkAdapter(IUtf8HtmlTokenSink sink) : IUtf8HtmlTokenSink
    {
        public void Text(ReadOnlySpan<Byte> utf8) => sink.Text(utf8);

        public void StartTag(ReadOnlySpan<Byte> name, UInt64 hash) => sink.StartTag(name, hash);

        public void Attribute(ReadOnlySpan<Byte> name, ReadOnlySpan<Byte> value) =>
            sink.Attribute(name, value);

        public void StartTagEnd(Boolean selfClosing) => sink.StartTagEnd(selfClosing);

        public void EndTag(ReadOnlySpan<Byte> name, UInt64 hash) => sink.EndTag(name, hash);

        public Boolean WantsAttribute(ReadOnlySpan<Byte> name) => true;

        public void Comment(ReadOnlySpan<Byte> utf8) => sink.Comment(utf8);

        public void Doctype(ReadOnlySpan<Byte> utf8) => sink.Doctype(utf8);

        public void Doctype(in Utf8DoctypeToken token) => sink.Doctype(token);

        public void EndOfFile() => sink.EndOfFile();
    }

    private void Process(Byte value)
    {
        var reconsume = true;
        while (reconsume)
        {
            reconsume = false;
            _stateMetrics?.Record((Int32)_state, 1);
            if (IsScriptState(_state))
            {
                ProcessScript(value, ref reconsume);
                continue;
            }
            switch (_state)
            {
                case State.Data:
                    if (value == (Byte)'<')
                    {
                        _state = State.TagOpen;
                    }
                    else if (value == (Byte)'&')
                    {
                        BeginCharacterReference(State.Data);
                    }
                    else if (value == (Byte)'\r')
                    {
                        BeginCarriageReturn();
                    }
                    else if (value == 0)
                    {
                        EmitByte(value);
                    }
                    else
                    {
                        EmitByte(value);
                    }

                    break;
                case State.Plaintext:
                    if (value == (Byte)'\r')
                    {
                        BeginCarriageReturn();
                    }
                    else if (value == 0)
                    {
                        EmitReplacementCharacter();
                    }
                    else
                    {
                        EmitByte(value);
                    }

                    break;
                case State.TagOpen:
                    if (value == (Byte)'/')
                    {
                        _state = State.EndTagOpen;
                    }
                    else if (value == (Byte)'!')
                    {
                        Clear(_candidate);
                        _state = State.MarkupDeclaration;
                    }
                    else if (value == (Byte)'?')
                    {
                        Clear(_candidate);
                        Append(_candidate, value);
                        _state = State.BogusComment;
                    }
                    else if (IsAsciiLetter(value))
                    {
                        BeginTag(isEndTag: false, value);
                    }
                    else
                    {
                        _sink.Text("<"u8);
                        Reconsume(ref reconsume, State.Data);
                    }
                    break;
                case State.EndTagOpen:
                    if (IsAsciiLetter(value))
                    {
                        BeginTag(isEndTag: true, value);
                    }
                    else if (value == (Byte)'>')
                    {
                        _state = State.Data;
                    }
                    else
                    {
                        Clear(_candidate);
                        Reconsume(ref reconsume, State.BogusComment);
                    }
                    break;
                case State.TagName:
                    if (IsSpace(value))
                    {
                        EmitTagStart();
                        _state = State.BeforeAttributeName;
                    }
                    else if (value == (Byte)'/')
                    {
                        EmitTagStart();
                        _state = State.SelfClosingStartTag;
                    }
                    else if (value == (Byte)'>')
                    {
                        FinishTag(selfClosing: false);
                    }
                    else
                    {
                        AppendTagNameReplacedNull(value);
                    }

                    break;
                case State.BeforeAttributeName:
                    if (IsSpace(value))
                    {
                        break;
                    }

                    if (value == (Byte)'/')
                    {
                        _state = State.SelfClosingStartTag;
                        break;
                    }
                    if (value == (Byte)'>')
                    {
                        FinishTag(selfClosing: false);
                        break;
                    }
                    Clear(_attributeName);
                    Clear(_attributeValue);
                    AppendReplacedNull(_attributeName, value, lowerAscii: true);
                    _state = State.AttributeName;
                    break;
                case State.AttributeName:
                    if (IsSpace(value))
                    {
                        _state = State.AfterAttributeName;
                    }
                    else if (value == (Byte)'=')
                    {
                        DecideAttributeCapture();
                        _state = State.BeforeAttributeValue;
                    }
                    else if (value is (Byte)'/' or (Byte)'>')
                    {
                        CommitAttribute();
                        Reconsume(ref reconsume, State.BeforeAttributeName);
                    }
                    else
                    {
                        AppendReplacedNull(_attributeName, value, lowerAscii: true);
                    }

                    break;
                case State.AfterAttributeName:
                    if (IsSpace(value))
                    {
                        break;
                    }

                    if (value == (Byte)'=')
                    {
                        DecideAttributeCapture();
                        _state = State.BeforeAttributeValue;
                        break;
                    }
                    CommitAttribute();
                    Reconsume(ref reconsume, State.BeforeAttributeName);
                    break;
                case State.BeforeAttributeValue:
                    if (IsSpace(value))
                    {
                        break;
                    }

                    if (value == (Byte)'"')
                    {
                        _state = State.AttributeValueDoubleQuoted;
                    }
                    else if (value == (Byte)'\'')
                    {
                        _state = State.AttributeValueSingleQuoted;
                    }
                    else if (value == (Byte)'>')
                    {
                        CommitAttribute();
                        FinishTag(selfClosing: false);
                    }
                    else
                    {
                        _state = State.AttributeValueUnquoted;
                        Reconsume(ref reconsume, _state);
                    }
                    break;
                case State.AttributeValueDoubleQuoted:
                case State.AttributeValueSingleQuoted:
                    var quote = _state == State.AttributeValueDoubleQuoted ? (Byte)'"' : (Byte)'\'';
                    if (value == quote)
                    {
                        _state = State.AfterAttributeValueQuoted;
                    }
                    else if (value == (Byte)'&')
                    {
                        BeginCharacterReference(_state);
                    }
                    else
                    {
                        if (_captureAttributeValue)
                        {
                            AppendReplacedNull(_attributeValue, value, lowerAscii: false);
                        }
                    }
                    break;
                case State.AttributeValueUnquoted:
                    if (IsSpace(value))
                    {
                        CommitAttribute();
                        _state = State.BeforeAttributeName;
                    }
                    else if (value == (Byte)'&')
                    {
                        BeginCharacterReference(_state);
                    }
                    else if (value == (Byte)'>')
                    {
                        CommitAttribute();
                        FinishTag(selfClosing: false);
                    }
                    else
                    {
                        if (_captureAttributeValue)
                        {
                            AppendReplacedNull(_attributeValue, value, lowerAscii: false);
                        }
                    }
                    break;
                case State.AfterAttributeValueQuoted:
                    CommitAttribute();
                    if (IsSpace(value))
                    {
                        _state = State.BeforeAttributeName;
                    }
                    else if (value == (Byte)'/')
                    {
                        _state = State.SelfClosingStartTag;
                    }
                    else if (value == (Byte)'>')
                    {
                        FinishTag(selfClosing: false);
                    }
                    else
                    {
                        Reconsume(ref reconsume, State.BeforeAttributeName);
                    }

                    break;
                case State.SelfClosingStartTag:
                    if (value == (Byte)'>')
                    {
                        FinishTag(selfClosing: true);
                    }
                    else
                    {
                        Reconsume(ref reconsume, State.BeforeAttributeName);
                    }

                    break;
                case State.MarkupDeclaration:
                    ProcessMarkupDeclaration(value);
                    break;
                case State.CommentStart:
                    if (value == (Byte)'-')
                    {
                        _state = State.CommentStartDash;
                    }
                    else if (value == (Byte)'>')
                    {
                        EmitComment();
                    }
                    else
                    {
                        Reconsume(ref reconsume, State.Comment);
                    }

                    break;
                case State.CommentStartDash:
                    if (value == (Byte)'-')
                    {
                        _state = State.CommentEnd;
                    }
                    else if (value == (Byte)'>')
                    {
                        EmitComment();
                    }
                    else
                    {
                        Append(_candidate, (Byte)'-');
                        Reconsume(ref reconsume, State.Comment);
                    }
                    break;
                case State.Comment:
                    if (value == (Byte)'<')
                    {
                        Append(_candidate, value);
                        _state = State.CommentLessThan;
                    }
                    else if (value == (Byte)'-')
                    {
                        _state = State.CommentEndDash;
                    }
                    else if (value == 0)
                    {
                        AppendReplacement(_candidate);
                    }
                    else
                    {
                        Append(_candidate, value);
                    }

                    break;
                case State.CommentLessThan:
                    if (value == (Byte)'!')
                    {
                        Append(_candidate, value);
                        _state = State.CommentLessThanBang;
                    }
                    else if (value == (Byte)'<')
                    {
                        Append(_candidate, value);
                    }
                    else
                    {
                        Reconsume(ref reconsume, State.Comment);
                    }

                    break;
                case State.CommentLessThanBang:
                    if (value == (Byte)'-')
                    {
                        _state = State.CommentLessThanBangDash;
                    }
                    else
                    {
                        Reconsume(ref reconsume, State.Comment);
                    }

                    break;
                case State.CommentLessThanBangDash:
                    if (value == (Byte)'-')
                    {
                        _state = State.CommentLessThanBangDashDash;
                    }
                    else
                    {
                        Reconsume(ref reconsume, State.CommentEndDash);
                    }

                    break;
                case State.CommentLessThanBangDashDash:
                    Reconsume(ref reconsume, State.CommentEnd);
                    break;
                case State.CommentEndDash:
                    if (value == (Byte)'-')
                    {
                        _state = State.CommentEnd;
                    }
                    else
                    {
                        Append(_candidate, (Byte)'-');
                        Reconsume(ref reconsume, State.Comment);
                    }
                    break;
                case State.CommentEnd:
                    if (value == (Byte)'>')
                    {
                        EmitComment();
                    }
                    else if (value == (Byte)'!')
                    {
                        _state = State.CommentEndBang;
                    }
                    else if (value == (Byte)'-')
                    {
                        Append(_candidate, value);
                    }
                    else
                    {
                        Append(_candidate, "--"u8);
                        Reconsume(ref reconsume, State.Comment);
                    }
                    break;
                case State.CommentEndBang:
                    if (value == (Byte)'>')
                    {
                        EmitComment();
                    }
                    else
                    {
                        Append(_candidate, "--!"u8);
                        if (value == (Byte)'-')
                        {
                            _state = State.CommentEndDash;
                        }
                        else
                        {
                            Reconsume(ref reconsume, State.Comment);
                        }
                    }
                    break;
                case State.BogusComment:
                    if (value == (Byte)'>')
                    {
                        _sink.Comment(_candidate.WrittenSpan);
                        Clear(_candidate);
                        _state = State.Data;
                    }
                    else
                    {
                        AppendReplacedNull(_candidate, value, lowerAscii: false);
                    }

                    break;
                case State.Doctype:
                    if (value == (Byte)'>')
                    {
                        EmitDoctype(forceEofQuirks: false);
                        _state = State.Data;
                    }
                    else
                    {
                        Append(_candidate, value);
                    }

                    break;
                case State.CharacterReference:
                    ProcessCharacterReference(value, ref reconsume);
                    break;
                case State.CDataSection:
                    if (value == (Byte)']')
                    {
                        _state = State.CDataSectionBracket;
                    }
                    else
                    {
                        EmitByte(value);
                    }

                    break;
                case State.CDataSectionBracket:
                    if (value == (Byte)']')
                    {
                        _state = State.CDataSectionEnd;
                    }
                    else
                    {
                        _sink.Text("]"u8);
                        Reconsume(ref reconsume, State.CDataSection);
                    }
                    break;
                case State.CDataSectionEnd:
                    if (value == (Byte)']')
                    {
                        _sink.Text("]"u8);
                    }
                    else if (value == (Byte)'>')
                    {
                        _state = State.Data;
                    }
                    else
                    {
                        _sink.Text("]]"u8);
                        Reconsume(ref reconsume, State.CDataSection);
                    }
                    break;
                case State.RawText:
                    if (value == (Byte)'<')
                    {
                        Clear(_candidate);
                        Append(_candidate, value);
                        _state = State.RawLessThan;
                    }
                    else if (value == (Byte)'&' && IsRcData())
                    {
                        BeginCharacterReference(State.RawText);
                    }
                    else if (value == (Byte)'\r')
                    {
                        BeginCarriageReturn();
                    }
                    else if (value == 0)
                    {
                        EmitReplacementCharacter();
                    }
                    else
                    {
                        EmitByte(value);
                    }

                    break;
                case State.RawLessThan:
                    if (value == (Byte)'/')
                    {
                        Append(_candidate, value);
                        _state = State.RawEndTagOpen;
                    }
                    else
                    {
                        _sink.Text(_candidate.WrittenSpan);
                        Clear(_candidate);
                        Reconsume(ref reconsume, State.RawText);
                    }
                    break;
                case State.RawEndTagOpen:
                case State.RawEndTagName:
                    if (IsAsciiLetter(value))
                    {
                        Append(_candidate, AsciiLower(value));
                        _state = State.RawEndTagName;
                    }
                    else if (
                        _state == State.RawEndTagName
                        && IsTagDelimiter(value)
                        && RawCandidateMatches()
                    )
                    {
                        Clear(_name);
                        ResetTagHash();
                        AppendTagName(_candidate.WrittenSpan[2..]);
                        Clear(_candidate);
                        _isEndTag = true;
                        _rawEndTag = null;
                        if (value == (Byte)'>')
                        {
                            FinishTag(selfClosing: false);
                        }
                        else if (value == (Byte)'/')
                        {
                            _state = State.SelfClosingStartTag;
                        }
                        else
                        {
                            _state = State.BeforeAttributeName;
                        }
                    }
                    else
                    {
                        _sink.Text(_candidate.WrittenSpan);
                        Clear(_candidate);
                        Reconsume(ref reconsume, State.RawText);
                    }
                    break;
            }
        }
    }

    private void ProcessMarkupDeclaration(Byte value)
    {
        if (value == (Byte)'>')
        {
            _sink.Comment(_candidate.WrittenSpan);
            Clear(_candidate);
            _state = State.Data;
            return;
        }

        AppendReplacedNull(_candidate, value, lowerAscii: false);
        var candidate = _candidate.WrittenSpan;
        if ("--"u8.StartsWith(candidate))
        {
            if (candidate.Length == 2)
            {
                Clear(_candidate);
                _state = State.CommentStart;
            }
            return;
        }
        if (StartsWithAsciiIgnoreCase("doctype"u8, candidate))
        {
            if (candidate.Length == 7)
            {
                Clear(_candidate);
                _state = State.Doctype;
            }
            return;
        }
        if (IsAcceptingCharacterData && "[CDATA["u8.StartsWith(candidate))
        {
            if (candidate.Length == 7)
            {
                Clear(_candidate);
                _state = State.CDataSection;
            }
            return;
        }
        _state = State.BogusComment;
    }

    private void EmitComment()
    {
        _sink.Comment(_candidate.WrittenSpan);
        Clear(_candidate);
        _state = State.Data;
    }

    private void ProcessCharacterReference(Byte value, ref Boolean reconsume)
    {
        var source = _candidate.WrittenSpan;
        if (!source.IsEmpty && source[0] == (Byte)'#')
        {
            if (source.Length == 1 && value is (Byte)'x' or (Byte)'X')
            {
                Append(_candidate, value);
                return;
            }

            if (value == (Byte)';')
            {
                if (!_numericReferenceOverflow)
                {
                    Append(_candidate, value);
                }

                ResolveCharacterReference();
                _state = _returnState;
                return;
            }

            var isHex = source.Length > 1 && source[1] is (Byte)'x' or (Byte)'X';
            var isDigit = isHex
                ? (UInt32)(value - '0') <= 9 || (UInt32)(AsciiLower(value) - 'a') <= 5
                : (UInt32)(value - '0') <= 9;
            if (isDigit)
            {
                var digit =
                    (UInt32)(value - '0') <= 9
                        ? (UInt32)(value - '0')
                        : (UInt32)(AsciiLower(value) - 'a' + 10);
                var radix = isHex ? 16u : 10u;
                _numericReferenceHasDigits = true;
                if (
                    !_numericReferenceOverflow
                    && _numericReferenceValue <= (0x10FFFFu - digit) / radix
                )
                {
                    _numericReferenceValue = _numericReferenceValue * radix + digit;
                }
                else
                {
                    _numericReferenceOverflow = true;
                }

                if (_candidate.WrittenCount < 32)
                {
                    Append(_candidate, value);
                }

                return;
            }

            ResolveCharacterReference(value);
            Reconsume(ref reconsume, _returnState);
            return;
        }

        if (value == (Byte)';')
        {
            Append(_candidate, value);
            ResolveCharacterReference();
            _state = _returnState;
            return;
        }
        var length = _candidate.WrittenCount;
        if (
            length < 32
            && (
                IsAsciiAlphaNumeric(value)
                || (length == 0 && value == (Byte)'#')
                || (
                    length == 1
                    && _candidate.WrittenSpan[0] == (Byte)'#'
                    && value is (Byte)'x' or (Byte)'X'
                )
            )
        )
        {
            Append(_candidate, value);
            return;
        }
        ResolveCharacterReference(value);
        Reconsume(ref reconsume, _returnState);
    }

    private void ResolveCharacterReference(Byte? nextInput = null)
    {
        var source = _candidate.WrittenSpan;
        Span<Byte> replacement = stackalloc Byte[8];
        var replacementLength = 0;
        if (!source.IsEmpty && source[0] == (Byte)'#' && _numericReferenceHasDigits)
        {
            var scalar = (Int32)_numericReferenceValue;
            if (_numericReferenceOverflow)
            {
                replacementLength = Encoding.UTF8.GetBytes("\uFFFD", replacement);
            }
            else if (HtmlEntityProvider.IsInCharacterTable(scalar))
            {
                replacementLength = Encoding.UTF8.GetBytes(
                    HtmlEntityProvider.GetSymbolFromTable(scalar)!,
                    replacement
                );
            }
            else if (
                HtmlEntityProvider.IsInvalidNumber(scalar) || !Rune.TryCreate(scalar, out var rune)
            )
            {
                replacementLength = Encoding.UTF8.GetBytes("\uFFFD", replacement);
            }
            else
            {
                replacementLength = rune.EncodeToUtf8(replacement);
            }
        }
        else if (!source.IsEmpty)
        {
            for (var length = source.Length; length > 0; length--)
            {
                var entity = HtmlEntityProvider.GetSymbol(source[..length]);
                var missingSemicolon = source[length - 1] != (Byte)';';
                if (entity is null)
                {
                    continue;
                }

                if (
                    missingSemicolon
                    && IsAttributeReturnState()
                    && (
                        (
                            length < source.Length
                            && (source[length] == '=' || IsAsciiAlphaNumeric(source[length]))
                        )
                        || (
                            length == source.Length
                            && nextInput is Byte next
                            && (next == '=' || IsAsciiAlphaNumeric(next))
                        )
                    )
                )
                {
                    break;
                }

                var byteCount = Encoding.UTF8.GetByteCount(entity);
                if (byteCount <= replacement.Length)
                {
                    replacementLength = Encoding.UTF8.GetBytes(entity, replacement);
                    AppendCharacterReferenceResult(replacement[..replacementLength]);
                }
                else
                {
                    AppendCharacterReferenceResult(Encoding.UTF8.GetBytes(entity));
                }

                AppendCharacterReferenceResult(source[length..]);
                Clear(_candidate);
                return;
            }
        }

        if (replacementLength != 0)
        {
            AppendCharacterReferenceResult(replacement[..replacementLength]);
        }
        else
        {
            AppendCharacterReferenceResult("&"u8);
            AppendCharacterReferenceResult(source);
        }
        Clear(_candidate);
    }

    private void EmitCharacterReferenceFallback()
    {
        AppendCharacterReferenceResult("&"u8);
        AppendCharacterReferenceResult(_candidate.WrittenSpan);
        Clear(_candidate);
    }

    private void AppendCharacterReferenceResult(ReadOnlySpan<Byte> utf8)
    {
        if (_returnState is State.Data or State.RawText)
        {
            _sink.Text(utf8);
        }
        else if (_captureAttributeValue)
        {
            Append(_attributeValue, utf8);
        }
    }

    private void BeginCharacterReference(State returnState)
    {
        Clear(_candidate);
        _numericReferenceOverflow = false;
        _numericReferenceHasDigits = false;
        _numericReferenceValue = 0;
        _returnState = returnState;
        _state = State.CharacterReference;
    }

    private void BeginTag(Boolean isEndTag, Byte firstByte)
    {
        _isEndTag = isEndTag;
        _startTagEmitted = false;
        Clear(_name);
        Clear(_attributeName);
        Clear(_attributeValue);
        Clear(_seenAttributeNames);
        _attributeCaptureDecided = false;
        _captureAttributeValue = true;
        ResetTagHash();
        AppendTagName(AsciiLower(firstByte));
        _state = State.TagName;
    }

    private void EmitTagStart()
    {
        if (_startTagEmitted || _isEndTag)
        {
            return;
        }

        _sink.StartTag(_name.WrittenSpan, _tagHash);

        _startTagEmitted = true;
    }

    private void DecideAttributeCapture()
    {
        if (_attributeCaptureDecided)
        {
            return;
        }

        if (_isEndTag)
        {
            _captureAttributeValue = false;
            _attributeCaptureDecided = true;
            return;
        }
        EmitTagStart();
        _captureAttributeValue = _sink.WantsAttribute(_attributeName.WrittenSpan);
        _attributeCaptureDecided = true;
    }

    private void CommitAttribute()
    {
        if (_attributeName.WrittenCount == 0)
        {
            return;
        }

        if (_isEndTag)
        {
            Clear(_attributeName);
            Clear(_attributeValue);
            _attributeCaptureDecided = false;
            _captureAttributeValue = true;
            return;
        }
        EmitTagStart();
        DecideAttributeCapture();
        if (!HasSeenAttribute(_attributeName.WrittenSpan))
        {
            if (_captureAttributeValue)
            {
                _sink.Attribute(_attributeName.WrittenSpan, _attributeValue.WrittenSpan);
            }

            Append(_seenAttributeNames, _attributeName.WrittenSpan);
            Append(_seenAttributeNames, (Byte)0);
        }
        Clear(_attributeName);
        Clear(_attributeValue);
        _attributeCaptureDecided = false;
        _captureAttributeValue = true;
    }

    private Boolean HasSeenAttribute(ReadOnlySpan<Byte> name)
    {
        var seen = _seenAttributeNames.WrittenSpan;
        while (!seen.IsEmpty)
        {
            var end = seen.IndexOf((Byte)0);
            if (end < 0)
            {
                return false;
            }

            if (seen[..end].SequenceEqual(name))
            {
                return true;
            }

            seen = seen[(end + 1)..];
        }
        return false;
    }

    private void EmitDoctype(Boolean forceEofQuirks)
    {
        var source = _candidate.WrittenSpan;
        var index = 0;
        var quirks = false;
        var publicMissing = true;
        var systemMissing = true;
        var state = DoctypeState.BeforeName;
        Clear(_name);
        Clear(_doctypePublic);
        Clear(_doctypeSystem);

        while (index < source.Length)
        {
            var value = source[index++];
            switch (state)
            {
                case DoctypeState.BeforeName:
                    if (IsSpace(value))
                    {
                        break;
                    }

                    AppendReplacedNull(_name, value, lowerAscii: true);
                    state = DoctypeState.Name;
                    break;
                case DoctypeState.Name:
                    if (IsSpace(value))
                    {
                        state = DoctypeState.AfterName;
                    }
                    else
                    {
                        AppendReplacedNull(_name, value, lowerAscii: true);
                    }

                    break;
                case DoctypeState.AfterName:
                    if (IsSpace(value))
                    {
                        break;
                    }

                    index--;
                    if (ConsumeKeyword(source, ref index, "public"u8))
                    {
                        state = DoctypeState.AfterPublicKeyword;
                    }
                    else if (ConsumeKeyword(source, ref index, "system"u8))
                    {
                        state = DoctypeState.AfterSystemKeyword;
                    }
                    else
                    {
                        quirks = true;
                        state = DoctypeState.Bogus;
                    }
                    break;
                case DoctypeState.AfterPublicKeyword:
                    if (IsSpace(value))
                    {
                        state = DoctypeState.BeforePublicIdentifier;
                    }
                    else if (value is (Byte)'"' or (Byte)'\'')
                    {
                        publicMissing = false;
                        state =
                            value == (Byte)'"'
                                ? DoctypeState.PublicIdentifierDoubleQuoted
                                : DoctypeState.PublicIdentifierSingleQuoted;
                    }
                    else
                    {
                        quirks = true;
                        state = DoctypeState.Bogus;
                    }
                    break;
                case DoctypeState.BeforePublicIdentifier:
                    if (IsSpace(value))
                    {
                        break;
                    }

                    if (value is (Byte)'"' or (Byte)'\'')
                    {
                        publicMissing = false;
                        state =
                            value == (Byte)'"'
                                ? DoctypeState.PublicIdentifierDoubleQuoted
                                : DoctypeState.PublicIdentifierSingleQuoted;
                    }
                    else
                    {
                        quirks = true;
                        state = DoctypeState.Bogus;
                    }
                    break;
                case DoctypeState.PublicIdentifierDoubleQuoted:
                case DoctypeState.PublicIdentifierSingleQuoted:
                    var publicQuote =
                        state == DoctypeState.PublicIdentifierDoubleQuoted ? (Byte)'"' : (Byte)'\'';
                    if (value == publicQuote)
                    {
                        state = DoctypeState.AfterPublicIdentifier;
                    }
                    else
                    {
                        AppendReplacedNull(_doctypePublic, value, lowerAscii: false);
                    }

                    break;
                case DoctypeState.AfterPublicIdentifier:
                    if (IsSpace(value))
                    {
                        state = DoctypeState.BetweenPublicAndSystemIdentifiers;
                    }
                    else if (value is (Byte)'"' or (Byte)'\'')
                    {
                        systemMissing = false;
                        state =
                            value == (Byte)'"'
                                ? DoctypeState.SystemIdentifierDoubleQuoted
                                : DoctypeState.SystemIdentifierSingleQuoted;
                    }
                    else
                    {
                        quirks = true;
                        state = DoctypeState.Bogus;
                    }
                    break;
                case DoctypeState.BetweenPublicAndSystemIdentifiers:
                    if (IsSpace(value))
                    {
                        break;
                    }

                    if (value is (Byte)'"' or (Byte)'\'')
                    {
                        systemMissing = false;
                        state =
                            value == (Byte)'"'
                                ? DoctypeState.SystemIdentifierDoubleQuoted
                                : DoctypeState.SystemIdentifierSingleQuoted;
                    }
                    else
                    {
                        quirks = true;
                        state = DoctypeState.Bogus;
                    }
                    break;
                case DoctypeState.AfterSystemKeyword:
                    if (IsSpace(value))
                    {
                        state = DoctypeState.BeforeSystemIdentifier;
                    }
                    else if (value is (Byte)'"' or (Byte)'\'')
                    {
                        systemMissing = false;
                        state =
                            value == (Byte)'"'
                                ? DoctypeState.SystemIdentifierDoubleQuoted
                                : DoctypeState.SystemIdentifierSingleQuoted;
                    }
                    else
                    {
                        quirks = true;
                        state = DoctypeState.Bogus;
                    }
                    break;
                case DoctypeState.BeforeSystemIdentifier:
                    if (IsSpace(value))
                    {
                        break;
                    }

                    if (value is (Byte)'"' or (Byte)'\'')
                    {
                        systemMissing = false;
                        state =
                            value == (Byte)'"'
                                ? DoctypeState.SystemIdentifierDoubleQuoted
                                : DoctypeState.SystemIdentifierSingleQuoted;
                    }
                    else
                    {
                        quirks = true;
                        state = DoctypeState.Bogus;
                    }
                    break;
                case DoctypeState.SystemIdentifierDoubleQuoted:
                case DoctypeState.SystemIdentifierSingleQuoted:
                    var systemQuote =
                        state == DoctypeState.SystemIdentifierDoubleQuoted ? (Byte)'"' : (Byte)'\'';
                    if (value == systemQuote)
                    {
                        state = DoctypeState.AfterSystemIdentifier;
                    }
                    else
                    {
                        AppendReplacedNull(_doctypeSystem, value, lowerAscii: false);
                    }

                    break;
                case DoctypeState.AfterSystemIdentifier:
                    if (!IsSpace(value))
                    {
                        state = DoctypeState.Bogus;
                    }

                    break;
                case DoctypeState.Bogus:
                    break;
                default:
                    throw new InvalidOperationException($"Unknown DOCTYPE state: {state}");
            }
        }

        if (_name.WrittenCount == 0)
        {
            quirks = true;
        }

        if (
            state
            is DoctypeState.AfterPublicKeyword
                or DoctypeState.BeforePublicIdentifier
                or DoctypeState.PublicIdentifierDoubleQuoted
                or DoctypeState.PublicIdentifierSingleQuoted
                or DoctypeState.AfterSystemKeyword
                or DoctypeState.BeforeSystemIdentifier
                or DoctypeState.SystemIdentifierDoubleQuoted
                or DoctypeState.SystemIdentifierSingleQuoted
        )
        {
            quirks = true;
        }

        if (
            forceEofQuirks
            && state
                is DoctypeState.BeforeName
                    or DoctypeState.Name
                    or DoctypeState.AfterName
                    or DoctypeState.AfterPublicKeyword
                    or DoctypeState.BeforePublicIdentifier
                    or DoctypeState.PublicIdentifierDoubleQuoted
                    or DoctypeState.PublicIdentifierSingleQuoted
                    or DoctypeState.AfterPublicIdentifier
                    or DoctypeState.BetweenPublicAndSystemIdentifiers
                    or DoctypeState.AfterSystemKeyword
                    or DoctypeState.BeforeSystemIdentifier
                    or DoctypeState.SystemIdentifierDoubleQuoted
                    or DoctypeState.SystemIdentifierSingleQuoted
                    or DoctypeState.AfterSystemIdentifier
        )
        {
            quirks = true;
        }

        var token = new Utf8DoctypeToken(
            _name.WrittenSpan,
            _doctypePublic.WrittenSpan,
            publicMissing,
            _doctypeSystem.WrittenSpan,
            systemMissing,
            quirks
        );
        _sink.Doctype(in token);
        Clear(_candidate);
        Clear(_name);
        Clear(_doctypePublic);
        Clear(_doctypeSystem);
    }

    private void AppendReplacedNull(
        ArrayBufferWriter<Byte> destination,
        Byte value,
        Boolean lowerAscii
    )
    {
        if (value == 0)
        {
            AppendReplacement(destination);
        }
        else
        {
            Append(destination, lowerAscii ? AsciiLower(value) : value);
        }
    }

    private static Boolean ConsumeKeyword(
        ReadOnlySpan<Byte> source,
        ref Int32 index,
        ReadOnlySpan<Byte> keyword
    )
    {
        if (
            source.Length - index < keyword.Length
            || !StartsWithAsciiIgnoreCase(source.Slice(index, keyword.Length), keyword)
        )
        {
            return false;
        }

        index += keyword.Length;
        return true;
    }

    private void AppendReplacement(ArrayBufferWriter<Byte> destination) =>
        Append(destination, "\uFFFD"u8);

    private void ProcessScript(Byte value, ref Boolean reconsume)
    {
        switch (_state)
        {
            case State.ScriptData:
                if (value == '<')
                {
                    _state = State.ScriptLessThan;
                }
                else
                {
                    EmitScriptByte(value);
                }

                break;
            case State.ScriptLessThan:
                if (value == '/')
                {
                    BeginScriptEndTag(State.ScriptEndTagName);
                }
                else if (value == '!')
                {
                    _sink.Text("<!"u8);
                    _state = State.ScriptEscapeStart;
                }
                else
                {
                    _sink.Text("<"u8);
                    Reconsume(ref reconsume, State.ScriptData);
                }
                break;
            case State.ScriptEscapeStart:
                if (value == '-')
                {
                    EmitByte(value);
                    _state = State.ScriptEscapeStartDash;
                }
                else
                {
                    Reconsume(ref reconsume, State.ScriptData);
                }

                break;
            case State.ScriptEscapeStartDash:
                if (value == '-')
                {
                    EmitByte(value);
                    _state = State.ScriptEscapedDashDash;
                }
                else
                {
                    Reconsume(ref reconsume, State.ScriptData);
                }

                break;
            case State.ScriptEscaped:
                if (value == '-')
                {
                    EmitByte(value);
                    _state = State.ScriptEscapedDash;
                }
                else if (value == '<')
                {
                    _state = State.ScriptEscapedLessThan;
                }
                else
                {
                    EmitScriptByte(value);
                }

                break;
            case State.ScriptEscapedDash:
                if (value == '-')
                {
                    EmitByte(value);
                    _state = State.ScriptEscapedDashDash;
                }
                else if (value == '<')
                {
                    _state = State.ScriptEscapedLessThan;
                }
                else
                {
                    EmitScriptByte(value);
                    _state = State.ScriptEscaped;
                }
                break;
            case State.ScriptEscapedDashDash:
                if (value == '-')
                {
                    EmitByte(value);
                }
                else if (value == '<')
                {
                    _state = State.ScriptEscapedLessThan;
                }
                else if (value == '>')
                {
                    EmitByte(value);
                    _state = State.ScriptData;
                }
                else
                {
                    EmitScriptByte(value);
                    _state = State.ScriptEscaped;
                }
                break;
            case State.ScriptEscapedLessThan:
                if (value == '/')
                {
                    BeginScriptEndTag(State.ScriptEscapedEndTagName);
                }
                else if (IsAsciiLetter(value))
                {
                    _sink.Text("<"u8);
                    Clear(_candidate);
                    Append(_candidate, AsciiLower(value));
                    EmitByte(value);
                    _state = State.ScriptDoubleEscapeStart;
                }
                else
                {
                    _sink.Text("<"u8);
                    Reconsume(ref reconsume, State.ScriptEscaped);
                }
                break;
            case State.ScriptEndTagName:
                ProcessScriptEndTag(value, State.ScriptData, ref reconsume);
                break;
            case State.ScriptEscapedEndTagName:
                ProcessScriptEndTag(value, State.ScriptEscaped, ref reconsume);
                break;
            case State.ScriptDoubleEscapeStart:
                if (IsAsciiLetter(value))
                {
                    Append(_candidate, AsciiLower(value));
                    EmitByte(value);
                }
                else if (IsTagDelimiter(value))
                {
                    var script = _candidate.WrittenSpan.SequenceEqual("script"u8);
                    Clear(_candidate);
                    EmitByte(value);
                    _state = script ? State.ScriptDoubleEscaped : State.ScriptEscaped;
                }
                else
                {
                    Clear(_candidate);
                    Reconsume(ref reconsume, State.ScriptEscaped);
                }
                break;
            case State.ScriptDoubleEscaped:
                if (value == '-')
                {
                    EmitByte(value);
                    _state = State.ScriptDoubleEscapedDash;
                }
                else if (value == '<')
                {
                    EmitByte(value);
                    _state = State.ScriptDoubleEscapedLessThan;
                }
                else
                {
                    EmitScriptByte(value);
                }

                break;
            case State.ScriptDoubleEscapedDash:
                if (value == '-')
                {
                    EmitByte(value);
                    _state = State.ScriptDoubleEscapedDashDash;
                }
                else if (value == '<')
                {
                    EmitByte(value);
                    _state = State.ScriptDoubleEscapedLessThan;
                }
                else
                {
                    EmitScriptByte(value);
                    _state = State.ScriptDoubleEscaped;
                }
                break;
            case State.ScriptDoubleEscapedDashDash:
                if (value == '-')
                {
                    EmitByte(value);
                }
                else if (value == '<')
                {
                    EmitByte(value);
                    _state = State.ScriptDoubleEscapedLessThan;
                }
                else if (value == '>')
                {
                    EmitByte(value);
                    _state = State.ScriptData;
                }
                else
                {
                    EmitScriptByte(value);
                    _state = State.ScriptDoubleEscaped;
                }
                break;
            case State.ScriptDoubleEscapedLessThan:
                if (value == '/')
                {
                    EmitByte(value);
                    Clear(_candidate);
                    _state = State.ScriptDoubleEscapeEnd;
                }
                else
                {
                    Reconsume(ref reconsume, State.ScriptDoubleEscaped);
                }

                break;
            case State.ScriptDoubleEscapeEnd:
                if (IsAsciiLetter(value))
                {
                    Append(_candidate, AsciiLower(value));
                    EmitByte(value);
                }
                else if (IsTagDelimiter(value))
                {
                    var script = _candidate.WrittenSpan.SequenceEqual("script"u8);
                    Clear(_candidate);
                    EmitByte(value);
                    _state = script ? State.ScriptEscaped : State.ScriptDoubleEscaped;
                }
                else
                {
                    Clear(_candidate);
                    Reconsume(ref reconsume, State.ScriptDoubleEscaped);
                }
                break;
        }
    }

    private void BeginScriptEndTag(State state)
    {
        Clear(_candidate);
        Append(_candidate, "</"u8);
        _state = state;
    }

    private void ProcessScriptEndTag(Byte value, State fallback, ref Boolean reconsume)
    {
        if (IsAsciiLetter(value))
        {
            Append(_candidate, AsciiLower(value));
            return;
        }
        var candidate = _candidate.WrittenSpan;
        if (RawCandidateMatches() && IsTagDelimiter(value))
        {
            Clear(_name);
            ResetTagHash();
            AppendTagName(candidate[2..]);
            Clear(_candidate);
            _isEndTag = true;
            _rawEndTag = null;
            if (value == '>')
            {
                FinishTag(false);
            }
            else if (value == '/')
            {
                _state = State.SelfClosingStartTag;
            }
            else
            {
                _state = State.BeforeAttributeName;
            }

            return;
        }
        _sink.Text(_candidate.WrittenSpan);
        Clear(_candidate);
        Reconsume(ref reconsume, fallback);
    }

    private void EmitScriptByte(Byte value)
    {
        if (value == 0)
        {
            EmitReplacementCharacter();
        }
        else if (value == '\r')
        {
            BeginCarriageReturn();
        }
        else
        {
            EmitByte(value);
        }
    }

    private static Boolean IsScriptState(State state) =>
        state is >= State.ScriptData and <= State.ScriptDoubleEscapeEnd;

    private void FinishTag(Boolean selfClosing)
    {
        CommitAttribute();
        if (_isEndTag)
        {
            _sink.EndTag(_name.WrittenSpan, _tagHash);
            _rawEndTag = null;
        }
        else
        {
            EmitTagStart();
            _sink.StartTagEnd(selfClosing);
            if (!selfClosing && !IsModeControlledExternally)
            {
                var name = _name.WrittenSpan;
                if (name.SequenceEqual("title"u8) || name.SequenceEqual("textarea"u8))
                {
                    _rawEndTag = "rcdata:" + Encoding.ASCII.GetString(name);
                }
                else if (
                    name.SequenceEqual("style"u8)
                    || name.SequenceEqual("xmp"u8)
                    || name.SequenceEqual("iframe"u8)
                    || name.SequenceEqual("noembed"u8)
                    || name.SequenceEqual("noframes"u8)
                )
                {
                    _rawEndTag = Encoding.ASCII.GetString(name);
                }
                else if (name.SequenceEqual("script"u8))
                {
                    _rawEndTag = "script";
                    _state = State.ScriptData;
                }
                else if (name.SequenceEqual("plaintext"u8))
                {
                    _state = State.Plaintext;
                }
            }
        }
        Clear(_name);
        _isEndTag = false;
        _startTagEmitted = false;
        if (_state is not State.Plaintext and not State.ScriptData)
        {
            _state = _rawEndTag is null ? State.Data : State.RawText;
        }
    }

    private Boolean RawCandidateMatches()
    {
        var expected = RawName();
        if (expected is null || _candidate.WrittenCount != expected.Length + 2)
        {
            return false;
        }

        var candidate = _candidate.WrittenSpan[2..];
        for (var index = 0; index < candidate.Length; index++)
        {
            if (candidate[index] != (Byte)expected[index])
            {
                return false;
            }
        }
        return true;
    }

    private String? RawName() =>
        _rawEndTag?.StartsWith("rcdata:", StringComparison.Ordinal) == true
            ? _rawEndTag[7..]
            : _rawEndTag;

    private Boolean IsRcData() =>
        _rawEndTag?.StartsWith("rcdata:", StringComparison.Ordinal) == true;

    private void BeginCarriageReturn()
    {
        EmitNormalizedLineFeed();
        _pendingCarriageReturn = true;
    }

    private void EmitNormalizedLineFeed() => _sink.Text("\n"u8);

    private void EmitReplacementCharacter() => _sink.Text("\uFFFD"u8);

    private void EmitByte(Byte value)
    {
        if (_textUtf8CarryLength != 0)
        {
            _textUtf8Carry |= (UInt32)value << (_textUtf8CarryLength++ * 8);
            if (_textUtf8CarryLength == _textUtf8ExpectedLength)
            {
                Span<Byte> scalar = stackalloc Byte[4];
                for (var index = 0; index < _textUtf8CarryLength; index++)
                {
                    scalar[index] = (Byte)(_textUtf8Carry >> (index * 8));
                }

                _sink.Text(scalar[.._textUtf8CarryLength]);
                _textUtf8Carry = 0;
                _textUtf8CarryLength = 0;
                _textUtf8ExpectedLength = 0;
            }
            return;
        }
        if (value >= 0x80)
        {
            _textUtf8Carry = value;
            _textUtf8CarryLength = 1;
            _textUtf8ExpectedLength = Utf8SequenceLength(value);
            return;
        }
        Span<Byte> single = stackalloc Byte[1];
        single[0] = value;
        _sink.Text(single);
    }

    private void Reconsume(ref Boolean reconsume, State state)
    {
        _state = state;
        reconsume = true;
        _reconsumes++;
    }

    private void Append(ArrayBufferWriter<Byte> buffer, Byte value)
    {
        EnsureBufferedTokenCapacity(1);
        buffer.GetSpan(1)[0] = value;
        buffer.Advance(1);
        ObserveBufferAppend(1);
    }

    private void Append(ArrayBufferWriter<Byte> buffer, ReadOnlySpan<Byte> value)
    {
        EnsureBufferedTokenCapacity(value.Length);
        buffer.Write(value);
        ObserveBufferAppend(value.Length);
    }

    private void ObserveBufferAppend(Int32 count)
    {
        _bufferedTokenBytes += count;
        if (_bufferedTokenBytes > _maximumBufferedTokenBytes)
        {
            _maximumBufferedTokenBytes = (Int32)Math.Min(_bufferedTokenBytes, Int32.MaxValue);
        }
    }

    private void EnsureBufferedTokenCapacity(Int32 additional)
    {
        var observed = SaturatingAdd(_bufferedTokenBytes, additional);
        if (observed > _maximumBufferedTokenBytesAllowed)
        {
            ThrowLimitExceeded(
                HtmlStreamingLimit.BufferedTokenBytes,
                _maximumBufferedTokenBytesAllowed,
                observed
            );
        }
    }

    private void Clear(ArrayBufferWriter<Byte> buffer)
    {
        _bufferedTokenBytes -= buffer.WrittenCount;
        buffer.Clear();
    }

    private static Int64 SaturatingAdd(Int64 value, Int32 additional) =>
        value > Int64.MaxValue - additional ? Int64.MaxValue : value + additional;

    private static void ThrowLimitExceeded(
        HtmlStreamingLimit limit,
        Int64 allowed,
        Int64 observed
    ) => throw new HtmlStreamingLimitExceededException(limit, allowed, observed);

    private void ShiftUtf8Carry(Int32 consumed)
    {
        _utf8Carry.AsSpan(consumed, _utf8CarryLength - consumed).CopyTo(_utf8Carry);
        _utf8CarryLength -= consumed;
    }

    private static Int32 CompleteUtf8PrefixLength(ReadOnlySpan<Byte> value)
    {
        if (value.IsEmpty)
        {
            return 0;
        }

        var lead = value.Length - 1;
        while (lead > 0 && value[lead] is >= 0x80 and <= 0xBF && value.Length - lead < 4)
        {
            lead--;
        }

        var expected = Utf8SequenceLength(value[lead]);
        return expected > 1 && value.Length - lead < expected ? lead : value.Length;
    }

    private static Int32 Utf8SequenceLength(Byte lead) =>
        lead switch
        {
            < 0x80 => 1,
            < 0xE0 => 2,
            < 0xF0 => 3,
            < 0xF8 => 4,
            _ => 1,
        };

    private static Int32 FindTextTerminator(ReadOnlySpan<Byte> value, Boolean includeAmpersand)
    {
        var index = value.IndexOfAny(includeAmpersand ? DataTextTerminators : RawTextTerminators);
        return index < 0 ? value.Length : index;
    }

    private void ResetTagHash() => _tagHash = Utf8NameHash.Offset;

    private void AppendTagName(Byte value)
    {
        Append(_name, value);
        _tagHash = Utf8NameHash.Append(_tagHash, value);
    }

    private void AppendTagName(ReadOnlySpan<Byte> value)
    {
        Append(_name, value);
        _tagHash = Utf8NameHash.Append(_tagHash, value);
    }

    private void AppendTagNameReplacedNull(Byte value)
    {
        if (value == 0)
        {
            AppendTagName("\uFFFD"u8);
        }
        else
        {
            AppendTagName(AsciiLower(value));
        }
    }

    private static Int32 FindPlaintextTerminator(ReadOnlySpan<Byte> value)
    {
        for (var i = 0; i < value.Length; i++)
        {
            if (value[i] is 0 or (Byte)'\r')
            {
                return i;
            }
        }
        return value.Length;
    }

    private static Int32 FindQuotedAttributeValueTerminator(ReadOnlySpan<Byte> value, Byte quote)
    {
        for (var i = 0; i < value.Length; i++)
        {
            if (value[i] == quote || value[i] is (Byte)'&' or 0 or (Byte)'\r')
            {
                return i;
            }
        }
        return value.Length;
    }

    private static Int32 FindUnquotedAttributeValueTerminator(ReadOnlySpan<Byte> value)
    {
        for (var i = 0; i < value.Length; i++)
        {
            if (value[i] is 0 or (Byte)'&' or (Byte)'>' || IsSpace(value[i]))
            {
                return i;
            }
        }
        return value.Length;
    }

    private static Int32 FindCommentTerminator(ReadOnlySpan<Byte> value)
    {
        for (var i = 0; i < value.Length; i++)
        {
            if (value[i] is (Byte)'<' or (Byte)'-' or 0 or (Byte)'\r')
            {
                return i;
            }
        }
        return value.Length;
    }

    private Boolean IsAttributeReturnState() =>
        _returnState is not State.Data and not State.RawText;

    private static Boolean StartsWithAsciiIgnoreCase(
        ReadOnlySpan<Byte> expected,
        ReadOnlySpan<Byte> candidate
    )
    {
        if (candidate.Length > expected.Length)
        {
            return false;
        }

        for (var i = 0; i < candidate.Length; i++)
        {
            if (AsciiLower(expected[i]) != AsciiLower(candidate[i]))
            {
                return false;
            }
        }
        return true;
    }

    private static ReadOnlySpan<Byte> TrimAsciiWhitespace(ReadOnlySpan<Byte> value)
    {
        var start = 0;
        var end = value.Length;
        while (start < end && IsSpace(value[start]))
        {
            start++;
        }

        while (end > start && IsSpace(value[end - 1]))
        {
            end--;
        }

        return value[start..end];
    }

    private static Boolean IsSpace(Byte value) => value is 0x09 or 0x0A or 0x0C or 0x0D or 0x20;

    private static Boolean IsAsciiLetter(Byte value) =>
        (UInt32)(value - 'A') <= 'Z' - 'A' || (UInt32)(value - 'a') <= 'z' - 'a';

    private static Boolean IsAsciiAlphaNumeric(Byte value) =>
        IsAsciiLetter(value) || (UInt32)(value - '0') <= 9;

    private static Boolean IsTagDelimiter(Byte value) =>
        value is (Byte)'>' or (Byte)'/' || IsSpace(value);

    private static Byte AsciiLower(Byte value) =>
        (UInt32)(value - 'A') <= 'Z' - 'A' ? (Byte)(value + 0x20) : value;

    private void ThrowIfCompleted()
    {
        if (_completed)
        {
            throw new InvalidOperationException("The tokenizer is already complete.");
        }
    }
}
