#pragma warning disable CS1591 // Experimental API surface; shape is intentionally unsettled.

using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Threading;
using System.Threading.Tasks;

namespace AngleSharp.Html.Parser.Utf8;

public sealed class Utf8HtmlTokenizer
{
    private const Int32 AttributeIndexPromotionThreshold = 16;

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

    private enum AttributeCapture : byte
    {
        Undecided,
        Capture,
        Discard,
        Duplicate,
    }

    private static readonly SearchValues<Byte> DataTextTerminators = SearchValues.Create("<&\0\r"u8);
    private static readonly SearchValues<Byte> RawTextTerminators = SearchValues.Create("<\0\r"u8);
    private static readonly SearchValues<Byte> PlaintextTerminators = SearchValues.Create("\0\r"u8);
    private static readonly SearchValues<Byte> TagNameTerminators =
        SearchValues.Create("\0\t\n\f\r />"u8);
    private static readonly SearchValues<Byte> AttributeNameTerminators =
        SearchValues.Create("\0\t\n\f\r /=>"u8);
    private static readonly SearchValues<Byte> DiscardedAttributeNameTerminators =
        SearchValues.Create("\t\n\f\r /=>"u8);
    private static readonly SearchValues<Byte> DoubleQuotedAttributeValueTerminators =
        SearchValues.Create("\"&\0\r"u8);
    private static readonly SearchValues<Byte> SingleQuotedAttributeValueTerminators =
        SearchValues.Create("'&\0\r"u8);
    private static readonly SearchValues<Byte> UnquotedAttributeValueTerminators =
        SearchValues.Create("\0&>\t\n\f\r "u8);
    private static readonly SearchValues<Byte> DiscardedUnquotedAttributeValueTerminators =
        SearchValues.Create(">\t\n\f\r "u8);
    private static readonly SearchValues<Byte> CommentTerminators = SearchValues.Create("<-\0\r"u8);
    private static readonly String[] StateNames = Enum.GetNames<State>();

    private readonly Utf8HtmlTokenizerStateMetrics? _stateMetrics;
    private readonly ArrayBufferWriter<Byte> _name = new(32);
    private readonly ArrayBufferWriter<Byte> _attributeName = new(32);
    private ArrayBufferWriter<Byte>? _attributeValue;
    private readonly ArrayBufferWriter<Byte> _seenAttributeNames = new(128);
    private readonly ArrayBufferWriter<Byte> _candidate = new(64);
    private ArrayBufferWriter<Byte>? _doctypePublic;
    private ArrayBufferWriter<Byte>? _doctypeSystem;
    private Utf8RuneValidator _validator;
    private State _state;
    private State _returnState;
    private Boolean _isEndTag;
    private Boolean _startTagEmitted;
    private Boolean _captureStartTagAttributes;
    private Boolean _captureText;
    private Boolean _pendingCarriageReturn;
    private String? _rawEndTag;
    private Int64 _segments;
    private Int64 _reconsumes;
    private Int64 _bufferedTokenBytes;
    private Int32 _maximumBufferedTokenBytes;
    private Int32 _textUtf8CarryLength;
    private Int32 _textUtf8ExpectedLength;
    private UInt32 _textUtf8Carry;
    private Boolean _numericReferenceOverflow;
    private Boolean _numericReferenceHasDigits;
    private UInt32 _numericReferenceValue;
    private Boolean _yieldRequested;
    private Boolean _completed;
    private Utf8HtmlNameHashCache _tagNameHashCache;
    private Utf8HtmlNameHashCache _attributeNameHashCache;
    private Utf8AttributeNameIndex.Entry[]? _seenAttributeIndex;
    private Int32 _seenAttributeCount;
    private AttributeCapture _attributeCapture;
    private readonly Int32 _maximumBufferedTokenBytesAllowed;
    private readonly IUtf8HtmlTokenSink _sink;
    private readonly IUtf8HtmlStreamingCommentSink? _streamingCommentSink;
    private readonly IUtf8HtmlStartTagSourceRangeSink? _startTagSourceRangeSink;
    private Boolean _streamingCommentStarted;
    private Boolean _captureStreamingComment;
    private Int64 _normalizedBytesConsumed;
    private Int64 _currentSourceOffset;
    private Int64 _lastLessThanSourceOffset;
    private Int64 _currentTagSourceOffset;

    private ArrayBufferWriter<Byte> AttributeValue => _attributeValue ??= new(128);

    private ArrayBufferWriter<Byte> DoctypePublic => _doctypePublic ??= new(64);

    private ArrayBufferWriter<Byte> DoctypeSystem => _doctypeSystem ??= new(64);

    public Utf8HtmlTokenizer(IUtf8HtmlTokenSink sink)
        : this(sink, null, HtmlStreamingLimits.Default, countInputBytes: true) { }

    public Utf8HtmlTokenizer(IUtf8HtmlTokenSink sink, HtmlStreamingLimits limits)
        : this(sink, null, limits, countInputBytes: true) { }

    public Utf8HtmlTokenizer(IUtf8HtmlTokenSink sink, Utf8InputContract inputContract)
        : this(sink, null, HtmlStreamingLimits.Default, countInputBytes: true, inputContract) { }

    public Utf8HtmlTokenizer(
        IUtf8HtmlTokenSink sink,
        HtmlStreamingLimits limits,
        Utf8InputContract inputContract
    )
        : this(sink, null, limits, countInputBytes: true, inputContract) { }

    public Utf8HtmlTokenizer(IUtf8HtmlTokenSink sink, Utf8HtmlTokenizerStateMetrics? stateMetrics)
        : this(sink, stateMetrics, HtmlStreamingLimits.Default, countInputBytes: true) { }

    public Utf8HtmlTokenizer(
        IUtf8HtmlTokenSink sink,
        Utf8HtmlTokenizerStateMetrics? stateMetrics,
        HtmlStreamingLimits limits,
        Boolean countInputBytes
    )
        : this(
            sink,
            stateMetrics,
            limits,
            countInputBytes,
            Utf8InputContract.ArbitraryBytes
        ) { }

    public Utf8HtmlTokenizer(
        IUtf8HtmlTokenSink sink,
        Utf8HtmlTokenizerStateMetrics? stateMetrics,
        HtmlStreamingLimits limits,
        Boolean countInputBytes,
        Utf8InputContract inputContract
    )
    {
        ArgumentNullException.ThrowIfNull(limits);
        ArgumentNullException.ThrowIfNull(sink);

        _sink = sink;
        RefreshCapture();
        _streamingCommentSink = sink as IUtf8HtmlStreamingCommentSink;
        _startTagSourceRangeSink =
            sink is IUtf8HtmlStartTagSourceRangeSink { WantsStartTagSourceRanges: true } sourceRangeSink
                ? sourceRangeSink
                : null;
        _stateMetrics = stateMetrics;
        _maximumBufferedTokenBytesAllowed = limits.MaximumBufferedTokenBytes;
        _validator = new Utf8RuneValidator(
            countInputBytes ? limits.MaximumInputBytes : Int64.MaxValue,
            inputContract
        );
    }

    public static Int32 StateCount => StateNames.Length;

    public IReadOnlyList<Utf8HtmlTokenizerStateMetric> GetStateMetrics() =>
        _stateMetrics?.Snapshot(StateNames) ?? [];

    public Utf8HtmlTokenizerCounters Counters =>
        new(_validator.BytesConsumed, _segments, _reconsumes, 0, _maximumBufferedTokenBytes);

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
        _validator.Write(this, utf8.Span, yieldOnRequest: false);
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
        return _validator.Write(this, utf8, yieldOnRequest);
    }

    internal Boolean IsYieldRequested => _yieldRequested;

    internal Int32 WriteNormalizedUtf8(ReadOnlySpan<Byte> utf8, Boolean yieldOnRequest)
    {
        var trackSourceRanges = _startTagSourceRangeSink is not null;
        var sourceBase = trackSourceRanges ? _normalizedBytesConsumed : 0;
        var index = 0;
        try
        {
            while (index < utf8.Length)
            {
            if (
                !_pendingCarriageReturn
                && (_isEndTag || (_startTagEmitted && !_captureStartTagAttributes))
                && IsTagTailState(_state)
            )
            {
                var consumed = ScanDiscardedTagTail(
                    utf8[index..],
                    trackSourceRanges ? sourceBase + index : 0,
                    trackSourceRanges
                );
                if (consumed > 0)
                {
                    index += consumed;
                    if (yieldOnRequest && _yieldRequested)
                    {
                        return index;
                    }
                    continue;
                }
            }
            else if (_state == State.TagName)
            {
                var remaining = utf8.Slice(index);
                var run = remaining.IndexOfAny(TagNameTerminators);
                run = run < 0 ? remaining.Length : run;

                if (run > 0)
                {
                    _stateMetrics?.Record((Int32)_state, run);
                    AppendTagName(remaining[..run]);
                    index += run;
                    continue;
                }
            }
            else if (_state == State.AttributeName)
            {
                var remaining = utf8.Slice(index);
                var run = remaining.IndexOfAny(AttributeNameTerminators);
                run = run < 0 ? remaining.Length : run;

                if (run > 0)
                {
                    _stateMetrics?.Record((Int32)_state, run);
                    if (_captureStartTagAttributes)
                    {
                        Append(_attributeName, remaining[..run]);
                    }
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
                var remaining = utf8.Slice(index);
                var run = !_captureText
                    ? _state == State.Plaintext
                        ? remaining.Length
                        : remaining.IndexOf((Byte)'<')
                    : _state == State.Plaintext
                        ? FindPlaintextTerminator(remaining)
                        : FindTextTerminator(remaining, _state == State.Data || IsRcData());
                if (run < 0)
                {
                    run = remaining.Length;
                }
                if (run > 0)
                {
                    _stateMetrics?.Record((Int32)_state, run);
                    if (_captureText)
                    {
                        EmitText(utf8.Slice(index, run));
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
                var remaining = utf8.Slice(index);
                var quote = _state == State.AttributeValueDoubleQuoted ? (Byte)'"' : (Byte)'\'';
                var run =
                    _attributeCapture == AttributeCapture.Capture
                        ? FindQuotedAttributeValueTerminator(remaining, quote)
                        : FindDiscardedQuotedAttributeValueTerminator(remaining, quote);
                if (run > 0)
                {
                    _stateMetrics?.Record((Int32)_state, run);
                    if (_attributeCapture == AttributeCapture.Capture)
                    {
                        Append(AttributeValue, remaining[..run]);
                    }

                    index += run;
                    continue;
                }
            }
            else if (_state == State.AttributeValueUnquoted && !_pendingCarriageReturn)
            {
                var remaining = utf8.Slice(index);
                var run =
                    _attributeCapture == AttributeCapture.Capture
                        ? FindUnquotedAttributeValueTerminator(remaining)
                        : FindDiscardedUnquotedAttributeValueTerminator(remaining);
                if (run > 0)
                {
                    _stateMetrics?.Record((Int32)_state, run);
                    if (_attributeCapture == AttributeCapture.Capture)
                    {
                        Append(AttributeValue, remaining.Slice(0, run));
                    }

                    index += run;
                    continue;
                }
            }
            else if (_state == State.Comment && !_pendingCarriageReturn)
            {
                var remaining = utf8.Slice(index);
                var run = FindCommentTerminator(remaining);
                if (run > 0)
                {
                    _stateMetrics?.Record((Int32)_state, run);
                    AppendComment(remaining.Slice(0, run));
                    index += run;
                    continue;
                }
            }
            var value = utf8[index++];
            if (trackSourceRanges)
            {
                _currentSourceOffset = sourceBase + index;
                if (value == (Byte)'<')
                {
                    _lastLessThanSourceOffset = _currentSourceOffset - 1;
                }
            }
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
        finally
        {
            if (trackSourceRanges)
            {
                _normalizedBytesConsumed = sourceBase + index;
            }
        }
    }

    public void Complete()
    {
        if (_completed)
        {
            return;
        }

        _validator.Complete(this);
        Utf8AttributeNameIndex.Reset(ref _seenAttributeIndex);
        switch (_state)
        {
            case State.TagOpen:
                EmitText("<"u8);
                break;
            case State.EndTagOpen:
                EmitText("</"u8);
                break;
            case State.CharacterReference:
                ResolveCharacterReference();
                break;
            case State.CDataSectionBracket:
                EmitText("]"u8);
                break;
            case State.CDataSectionEnd:
                EmitText("]]"u8);
                break;
            case State.RawLessThan:
            case State.RawEndTagOpen:
            case State.RawEndTagName:
                EmitText(_candidate.WrittenSpan);
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
                EmitComment();
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
                    else if (value == (Byte)'&' && _captureText)
                    {
                        BeginCharacterReference(State.Data);
                    }
                    else if (_captureText)
                    {
                        EmitByte(value);
                    }

                    break;
                case State.Plaintext:
                    if (value == 0)
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
                        EmitText("<"u8);
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
                    if (_captureStartTagAttributes)
                    {
                        Clear(_attributeName);
                        Clear(_attributeValue);
                        _attributeNameHashCache.Reset();
                        AppendReplacedNull(_attributeName, value, lowerAscii: false);
                    }
                    else
                    {
                        _attributeCapture = AttributeCapture.Discard;
                    }
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
                        if (_captureStartTagAttributes)
                        {
                            AppendReplacedNull(_attributeName, value, lowerAscii: false);
                        }
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
                    else if (
                        value == (Byte)'&'
                        && _attributeCapture == AttributeCapture.Capture
                    )
                    {
                        BeginCharacterReference(_state);
                    }
                    else
                    {
                        if (_attributeCapture == AttributeCapture.Capture)
                        {
                            AppendReplacedNull(AttributeValue, value, lowerAscii: false);
                        }
                    }
                    break;
                case State.AttributeValueUnquoted:
                    if (IsSpace(value))
                    {
                        CommitAttribute();
                        _state = State.BeforeAttributeName;
                    }
                    else if (
                        value == (Byte)'&'
                        && _attributeCapture == AttributeCapture.Capture
                    )
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
                        if (_attributeCapture == AttributeCapture.Capture)
                        {
                            AppendReplacedNull(AttributeValue, value, lowerAscii: false);
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
                        AppendComment((Byte)'-');
                        Reconsume(ref reconsume, State.Comment);
                    }
                    break;
                case State.Comment:
                    if (value == (Byte)'<')
                    {
                        AppendComment(value);
                        _state = State.CommentLessThan;
                    }
                    else if (value == (Byte)'-')
                    {
                        _state = State.CommentEndDash;
                    }
                    else if (value == 0)
                    {
                        AppendCommentReplacement();
                    }
                    else
                    {
                        AppendComment(value);
                    }

                    break;
                case State.CommentLessThan:
                    if (value == (Byte)'!')
                    {
                        AppendComment(value);
                        _state = State.CommentLessThanBang;
                    }
                    else if (value == (Byte)'<')
                    {
                        AppendComment(value);
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
                        AppendComment((Byte)'-');
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
                        AppendComment(value);
                    }
                    else
                    {
                        AppendComment("--"u8);
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
                        AppendComment("--!"u8);
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
                        EmitComment();
                    }
                    else
                    {
                        AppendCommentReplacedNull(value);
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
                        EmitText("]"u8);
                        Reconsume(ref reconsume, State.CDataSection);
                    }
                    break;
                case State.CDataSectionEnd:
                    if (value == (Byte)']')
                    {
                        EmitText("]"u8);
                    }
                    else if (value == (Byte)'>')
                    {
                        _state = State.Data;
                    }
                    else
                    {
                        EmitText("]]"u8);
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
                    else if (value == (Byte)'&' && _captureText && IsRcData())
                    {
                        BeginCharacterReference(State.RawText);
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
                        EmitText(_candidate.WrittenSpan);
                        Clear(_candidate);
                        Reconsume(ref reconsume, State.RawText);
                    }
                    break;
                case State.RawEndTagOpen:
                case State.RawEndTagName:
                    if (IsAsciiLetter(value))
                    {
                        Append(_candidate, value);
                        _state = State.RawEndTagName;
                    }
                    else if (
                        _state == State.RawEndTagName
                        && IsTagDelimiter(value)
                        && RawCandidateMatches()
                    )
                    {
                        Clear(_name);
                        _tagNameHashCache.Reset();
                        AppendTagName(_candidate.WrittenSpan.Slice(2));
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
                        EmitText(_candidate.WrittenSpan);
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
            EmitComment();
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
        if (_streamingCommentSink is null)
        {
            _sink.Comment(_candidate.WrittenSpan);
        }
        else
        {
            EnsureStreamingCommentStarted();
            _streamingCommentSink.EndComment();
            _streamingCommentStarted = false;
            _captureStreamingComment = false;
        }
        Clear(_candidate);
        _state = State.Data;
    }

    private void AppendComment(Byte value)
    {
        Span<Byte> bytes = stackalloc Byte[1];
        bytes[0] = value;
        AppendComment(bytes);
    }

    private void AppendComment(ReadOnlySpan<Byte> value)
    {
        if (_streamingCommentSink is null)
        {
            Append(_candidate, value);
            return;
        }

        EnsureStreamingCommentStarted();
        if (_captureStreamingComment)
        {
            _streamingCommentSink.CommentChunk(value);
        }
    }

    private void AppendCommentReplacement() => AppendComment("\uFFFD"u8);

    private void AppendCommentReplacedNull(Byte value) =>
        AppendComment(value == 0 ? "\uFFFD"u8 : new ReadOnlySpan<Byte>(in value));

    private void EnsureStreamingCommentStarted()
    {
        if (_streamingCommentStarted)
        {
            return;
        }

        _captureStreamingComment = _streamingCommentSink!.BeginComment();
        _streamingCommentStarted = true;
        if (_captureStreamingComment && _candidate.WrittenCount != 0)
        {
            _streamingCommentSink.CommentChunk(_candidate.WrittenSpan);
        }
        Clear(_candidate);
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
                replacementLength = WriteReplacementCharacter(replacement);
            }
            else
            {
                var mappedScalar = HtmlEntityProvider.GetSymbolCodeFromTable(scalar);
                scalar = mappedScalar < 0 ? scalar : mappedScalar;
                replacementLength = HtmlEntityProvider.IsInvalidNumber(scalar)
                    ? WriteReplacementCharacter(replacement)
                    : WriteScalarUtf8(scalar, replacement);
            }
        }
        else if (!source.IsEmpty)
        {
            var entityLength = HtmlEntityProvider.WriteLongestSymbolUtf8(
                source,
                replacement,
                out var matchedLength
            );
            if (entityLength != 0)
            {
                var missingSemicolon = source[matchedLength - 1] != (Byte)';';
                if (
                    missingSemicolon
                    && IsAttributeReturnState()
                    && (
                        (
                            matchedLength < source.Length
                            && (
                                source[matchedLength] == '='
                                || IsAsciiAlphaNumeric(source[matchedLength])
                            )
                        )
                        || (
                            matchedLength == source.Length
                            && nextInput is Byte next
                            && (next == '=' || IsAsciiAlphaNumeric(next))
                        )
                    )
                )
                {
                    entityLength = 0;
                }
            }
            if (entityLength != 0)
            {
                AppendCharacterReferenceResult(replacement[..entityLength]);
                AppendCharacterReferenceResult(source[matchedLength..]);
                Clear(_candidate);
                return;
            }
        }

        if (replacementLength != 0)
        {
            AppendCharacterReferenceResult(replacement.Slice(0, replacementLength));
        }
        else
        {
            AppendCharacterReferenceResult("&"u8);
            AppendCharacterReferenceResult(source);
        }
        Clear(_candidate);
    }

    private static Int32 WriteReplacementCharacter(Span<Byte> destination)
    {
        "\uFFFD"u8.CopyTo(destination);
        return 3;
    }

    private static Int32 WriteScalarUtf8(Int32 scalar, Span<Byte> destination)
    {
        if (scalar <= 0x7F)
        {
            destination[0] = (Byte)scalar;
            return 1;
        }
        if (scalar <= 0x7FF)
        {
            destination[0] = (Byte)(0xC0 | (scalar >> 6));
            destination[1] = (Byte)(0x80 | (scalar & 0x3F));
            return 2;
        }
        if (scalar <= 0xFFFF)
        {
            destination[0] = (Byte)(0xE0 | (scalar >> 12));
            destination[1] = (Byte)(0x80 | ((scalar >> 6) & 0x3F));
            destination[2] = (Byte)(0x80 | (scalar & 0x3F));
            return 3;
        }

        destination[0] = (Byte)(0xF0 | (scalar >> 18));
        destination[1] = (Byte)(0x80 | ((scalar >> 12) & 0x3F));
        destination[2] = (Byte)(0x80 | ((scalar >> 6) & 0x3F));
        destination[3] = (Byte)(0x80 | (scalar & 0x3F));
        return 4;
    }

    private void AppendCharacterReferenceResult(ReadOnlySpan<Byte> utf8)
    {
        if (_returnState is State.Data or State.RawText)
        {
            EmitText(utf8);
        }
        else if (_attributeCapture == AttributeCapture.Capture)
        {
            Append(AttributeValue, utf8);
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
        if (_startTagSourceRangeSink is not null)
        {
            _currentTagSourceOffset = _lastLessThanSourceOffset;
        }
        _startTagEmitted = false;
        _captureStartTagAttributes = false;
        Clear(_name);
        Clear(_attributeName);
        Clear(_attributeValue);
        Clear(_seenAttributeNames);
        Utf8AttributeNameIndex.Reset(ref _seenAttributeIndex);
        _seenAttributeCount = 0;
        _tagNameHashCache.Reset();
        _attributeNameHashCache.Reset();
        _attributeCapture = AttributeCapture.Undecided;
        AppendTagName(firstByte);
        _state = State.TagName;
    }

    private void EmitTagStart()
    {
        if (_startTagEmitted || _isEndTag)
        {
            return;
        }

        _captureStartTagAttributes = (
            _sink.StartTag(CurrentTagName()) & Utf8HtmlStartTagCapture.Attributes
        ) != 0;
        _startTagEmitted = true;
    }

    private void DecideAttributeCapture()
    {
        if (_attributeCapture != AttributeCapture.Undecided)
        {
            return;
        }

        if (!_captureStartTagAttributes || _isEndTag)
        {
            _attributeCapture = AttributeCapture.Discard;
            return;
        }
        EmitTagStart();
        var name = CurrentAttributeName();
        var capture = _sink.WantsAttribute(name);
        _attributeCapture = HasSeenAttribute(name)
            ? AttributeCapture.Duplicate
            : capture
                ? AttributeCapture.Capture
                : AttributeCapture.Discard;
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
            _attributeCapture = AttributeCapture.Undecided;
            return;
        }
        DecideAttributeCapture();
        var name = CurrentAttributeName();
        if (_attributeCapture != AttributeCapture.Duplicate)
        {
            if (_attributeCapture == AttributeCapture.Capture)
            {
                _sink.Attribute(name, WrittenSpan(_attributeValue));
            }

            var nameOffset = _seenAttributeNames.WrittenCount;
            Append(_seenAttributeNames, name.Verbatim);
            Append(_seenAttributeNames, (Byte)0);
            if (_seenAttributeIndex is not null)
            {
                Utf8AttributeNameIndex.Add(
                    ref _seenAttributeIndex,
                    name.SemanticHash,
                    nameOffset
                );
            }
            _seenAttributeCount++;
        }
        Clear(_attributeName);
        Clear(_attributeValue);
        _attributeNameHashCache.Reset();
        _attributeCapture = AttributeCapture.Undecided;
    }

    private Boolean HasSeenAttribute(Utf8HtmlName name)
    {
        var index = _seenAttributeIndex;
        if (index is not null)
        {
            return Utf8AttributeNameIndex.Contains(
                index,
                name,
                _seenAttributeNames.WrittenSpan
            );
        }

        var seen = _seenAttributeNames.WrittenSpan;
        while (!seen.IsEmpty)
        {
            var end = seen.IndexOf((Byte)0);
            if (end < 0)
            {
                return false;
            }

            if (name.SemanticEquals(seen[..end]))
            {
                return true;
            }

            seen = seen.Slice(end + 1);
        }

        if (_seenAttributeCount >= AttributeIndexPromotionThreshold)
        {
            Utf8AttributeNameIndex.Initialize(
                ref _seenAttributeIndex,
                _seenAttributeNames.WrittenSpan,
                _seenAttributeCount
            );
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
                        AppendReplacedNull(DoctypePublic, value, lowerAscii: false);
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
                        AppendReplacedNull(DoctypeSystem, value, lowerAscii: false);
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
            WrittenSpan(_doctypePublic),
            publicMissing,
            WrittenSpan(_doctypeSystem),
            systemMissing,
            quirks
        );
        _sink.Doctype(in token);
        Clear(_candidate);
        Clear(_name);
        Clear(_doctypePublic);
        Clear(_doctypeSystem);
    }

    private void AppendReplacedNull(ArrayBufferWriter<Byte> destination, Byte value, Boolean lowerAscii)
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

    private static Boolean ConsumeKeyword(ReadOnlySpan<Byte> source, ref Int32 index, ReadOnlySpan<Byte> keyword)
    {
        if (source.Length - index < keyword.Length
            || !StartsWithAsciiIgnoreCase(source.Slice(index, keyword.Length), keyword))
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
                    EmitText("<!"u8);
                    _state = State.ScriptEscapeStart;
                }
                else
                {
                    EmitText("<"u8);
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
                    EmitText("<"u8);
                    Clear(_candidate);
                    Append(_candidate, AsciiLower(value));
                    EmitByte(value);
                    _state = State.ScriptDoubleEscapeStart;
                }
                else
                {
                    EmitText("<"u8);
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
            Append(_candidate, value);
            return;
        }
        var candidate = _candidate.WrittenSpan;
        if (RawCandidateMatches() && IsTagDelimiter(value))
        {
            Clear(_name);
            _tagNameHashCache.Reset();
            AppendTagName(candidate.Slice(2));
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
        EmitText(_candidate.WrittenSpan);
        Clear(_candidate);
        Reconsume(ref reconsume, fallback);
    }

    private void EmitScriptByte(Byte value)
    {
        if (value == 0)
        {
            EmitReplacementCharacter();
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
        if (
            _seenAttributeIndex is null
            && _seenAttributeCount < AttributeIndexPromotionThreshold
        )
        {
            CommitAttribute();
            FinishTagCore(selfClosing);
            _seenAttributeCount = 0;
            return;
        }

        try
        {
            CommitAttribute();
            FinishTagCore(selfClosing);
        }
        finally
        {
            Utf8AttributeNameIndex.Reset(ref _seenAttributeIndex);
            _seenAttributeCount = 0;
        }
    }

    private void FinishTagCore(Boolean selfClosing)
    {
        if (_isEndTag)
        {
            _sink.EndTag(CurrentTagName());
            RefreshCapture();
            _rawEndTag = null;
        }
        else
        {
            EmitTagStart();
            _startTagSourceRangeSink?.StartTagSourceRange(_currentTagSourceOffset, _currentSourceOffset);
            _sink.StartTagEnd(selfClosing);
            RefreshCapture();
            // In HTML, the trailing solidus does not make a non-void element self-closing.
            // Tree construction controls the mode in the DOM path; the standalone path must
            // therefore still infer text modes for e.g. <textarea/> and <plaintext/>.
            if (!IsModeControlledExternally)
            {
                var name = CurrentTagName();
                if (name.SemanticEquals("title"u8))
                {
                    _rawEndTag = "rcdata:title";
                }
                else if (name.SemanticEquals("textarea"u8))
                {
                    _rawEndTag = "rcdata:textarea";
                }
                else if (name.SemanticEquals("style"u8))
                {
                    _rawEndTag = "style";
                }
                else if (name.SemanticEquals("xmp"u8))
                {
                    _rawEndTag = "xmp";
                }
                else if (name.SemanticEquals("iframe"u8))
                {
                    _rawEndTag = "iframe";
                }
                else if (name.SemanticEquals("noembed"u8))
                {
                    _rawEndTag = "noembed";
                }
                else if (name.SemanticEquals("noframes"u8))
                {
                    _rawEndTag = "noframes";
                }
                else if (name.SemanticEquals("script"u8))
                {
                    _rawEndTag = "script";
                    _state = State.ScriptData;
                }
                else if (name.SemanticEquals("plaintext"u8))
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
        if (_rawEndTag is null)
        {
            return false;
        }

        var expected = _rawEndTag.StartsWith("rcdata:", StringComparison.Ordinal)
            ? _rawEndTag.AsSpan(7)
            : _rawEndTag.AsSpan();

        if (_candidate.WrittenCount != expected.Length + 2)
        {
            return false;
        }

        var candidate = _candidate.WrittenSpan.Slice(2);
        for (var index = 0; index < candidate.Length; index++)
        {
            if (AsciiLower(candidate[index]) != AsciiLower((Byte)expected[index]))
            {
                return false;
            }
        }
        return true;
    }

    private Boolean IsRcData() =>
        _rawEndTag?.StartsWith("rcdata:", StringComparison.Ordinal) == true;

    private void RefreshCapture() =>
        _captureText = (_sink.Capture & Utf8HtmlTokenCapture.Text) != 0;

    private void EmitText(ReadOnlySpan<Byte> utf8)
    {
        if (_captureText)
        {
            _sink.Text(utf8);
        }
    }

    private void EmitReplacementCharacter() => EmitText("\uFFFD"u8);

    private void EmitByte(Byte value)
    {
        if (!_captureText)
        {
            return;
        }
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

                EmitText(scalar[.._textUtf8CarryLength]);
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
        EmitText(single);
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

    private static ReadOnlySpan<Byte> WrittenSpan(ArrayBufferWriter<Byte>? buffer) =>
        buffer is null ? ReadOnlySpan<Byte>.Empty : buffer.WrittenSpan;

    private void Clear(ArrayBufferWriter<Byte>? buffer)
    {
        if (buffer is null)
        {
            return;
        }

        _bufferedTokenBytes -= buffer.WrittenCount;
        buffer.ResetWrittenCount();
    }

    private static Int64 SaturatingAdd(Int64 value, Int32 additional) =>
        value > Int64.MaxValue - additional ? Int64.MaxValue : value + additional;

    private static void ThrowLimitExceeded(
        HtmlStreamingLimit limit,
        Int64 allowed,
        Int64 observed
    ) => throw new HtmlStreamingLimitExceededException(limit, allowed, observed);

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

    private void AppendTagName(Byte value) => Append(_name, value);

    private void AppendTagName(ReadOnlySpan<Byte> value) => Append(_name, value);

    private void AppendTagNameReplacedNull(Byte value)
    {
        if (value == 0)
        {
            AppendTagName("\uFFFD"u8);
        }
        else
        {
            AppendTagName(value);
        }
    }

    private Utf8HtmlName CurrentTagName() =>
        new(_name.WrittenSpan, ref _tagNameHashCache);

    private Utf8HtmlName CurrentAttributeName() =>
        new(_attributeName.WrittenSpan, ref _attributeNameHashCache);

    private static Int32 FindPlaintextTerminator(ReadOnlySpan<Byte> value)
    {
        var terminator = value.IndexOfAny(PlaintextTerminators);
        return terminator < 0 ? value.Length : terminator;
    }

    private static Int32 FindQuotedAttributeValueTerminator(ReadOnlySpan<Byte> value, Byte quote)
    {
        var terminator = value.IndexOfAny(
            quote == (Byte)'"'
                ? DoubleQuotedAttributeValueTerminators
                : SingleQuotedAttributeValueTerminators
        );
        return terminator < 0 ? value.Length : terminator;
    }

    private static Int32 FindDiscardedQuotedAttributeValueTerminator(
        ReadOnlySpan<Byte> value,
        Byte quote
    )
    {
        var terminator = value.IndexOf(quote);
        return terminator < 0 ? value.Length : terminator;
    }

    private static Int32 FindUnquotedAttributeValueTerminator(ReadOnlySpan<Byte> value)
    {
        var terminator = value.IndexOfAny(UnquotedAttributeValueTerminators);
        return terminator < 0 ? value.Length : terminator;
    }

    private static Int32 FindDiscardedUnquotedAttributeValueTerminator(ReadOnlySpan<Byte> value)
    {
        var terminator = value.IndexOfAny(DiscardedUnquotedAttributeValueTerminators);
        return terminator < 0 ? value.Length : terminator;
    }

    private static Boolean IsTagTailState(State state) =>
        state is State.BeforeAttributeName
            or State.AttributeName
            or State.AfterAttributeName
            or State.BeforeAttributeValue
            or State.AttributeValueDoubleQuoted
            or State.AttributeValueSingleQuoted
            or State.AttributeValueUnquoted
            or State.AfterAttributeValueQuoted
            or State.SelfClosingStartTag;

    private Int32 ScanDiscardedTagTail(
        ReadOnlySpan<Byte> utf8,
        Int64 sourceOffset,
        Boolean trackSourceRanges
    )
    {
        var index = 0;
        while (index < utf8.Length && IsTagTailState(_state))
        {
            var state = _state;
            var remaining = utf8[index..];
            Int32 run;
            switch (state)
            {
                case State.AttributeName:
                    run = remaining.IndexOfAny(DiscardedAttributeNameTerminators);
                    run = run < 0 ? remaining.Length : run;
                    if (run > 0)
                    {
                        _stateMetrics?.Record((Int32)state, run);
                        index += run;
                        continue;
                    }
                    break;
                case State.AttributeValueDoubleQuoted:
                case State.AttributeValueSingleQuoted:
                    var quote = state == State.AttributeValueDoubleQuoted ? (Byte)'"' : (Byte)'\'';
                    run = remaining.IndexOf(quote);
                    run = run < 0 ? remaining.Length : run;
                    if (run > 0)
                    {
                        _stateMetrics?.Record((Int32)state, run);
                        index += run;
                        continue;
                    }
                    break;
                case State.AttributeValueUnquoted:
                    run = FindDiscardedUnquotedAttributeValueTerminator(remaining);
                    if (run > 0)
                    {
                        _stateMetrics?.Record((Int32)state, run);
                        index += run;
                        continue;
                    }
                    break;
            }

            var value = utf8[index];
            _stateMetrics?.Record((Int32)state, 1);
            switch (state)
            {
                case State.BeforeAttributeName:
                    if (IsSpace(value))
                    {
                        index++;
                    }
                    else if (value == (Byte)'/')
                    {
                        index++;
                        _state = State.SelfClosingStartTag;
                    }
                    else if (value == (Byte)'>')
                    {
                        FinishScannedTag(ref index, selfClosing: false, sourceOffset, trackSourceRanges);
                    }
                    else
                    {
                        _state = State.AttributeName;
                    }
                    break;
                case State.AttributeName:
                    if (IsSpace(value))
                    {
                        index++;
                        _state = State.AfterAttributeName;
                    }
                    else if (value == (Byte)'=')
                    {
                        index++;
                        _state = State.BeforeAttributeValue;
                    }
                    else
                    {
                        _state = State.BeforeAttributeName;
                    }
                    break;
                case State.AfterAttributeName:
                    if (IsSpace(value))
                    {
                        index++;
                    }
                    else if (value == (Byte)'=')
                    {
                        index++;
                        _state = State.BeforeAttributeValue;
                    }
                    else
                    {
                        _state = State.BeforeAttributeName;
                    }
                    break;
                case State.BeforeAttributeValue:
                    if (IsSpace(value))
                    {
                        index++;
                    }
                    else if (value == (Byte)'"')
                    {
                        index++;
                        _state = State.AttributeValueDoubleQuoted;
                    }
                    else if (value == (Byte)'\'')
                    {
                        index++;
                        _state = State.AttributeValueSingleQuoted;
                    }
                    else if (value == (Byte)'>')
                    {
                        FinishScannedTag(ref index, selfClosing: false, sourceOffset, trackSourceRanges);
                    }
                    else
                    {
                        _state = State.AttributeValueUnquoted;
                    }
                    break;
                case State.AttributeValueDoubleQuoted:
                case State.AttributeValueSingleQuoted:
                    index++;
                    _state = State.AfterAttributeValueQuoted;
                    break;
                case State.AttributeValueUnquoted:
                    if (value == (Byte)'>')
                    {
                        FinishScannedTag(ref index, selfClosing: false, sourceOffset, trackSourceRanges);
                    }
                    else
                    {
                        index++;
                        _state = State.BeforeAttributeName;
                    }
                    break;
                case State.AfterAttributeValueQuoted:
                    if (IsSpace(value))
                    {
                        index++;
                        _state = State.BeforeAttributeName;
                    }
                    else if (value == (Byte)'/')
                    {
                        index++;
                        _state = State.SelfClosingStartTag;
                    }
                    else if (value == (Byte)'>')
                    {
                        FinishScannedTag(ref index, selfClosing: false, sourceOffset, trackSourceRanges);
                    }
                    else
                    {
                        _state = State.BeforeAttributeName;
                    }
                    break;
                case State.SelfClosingStartTag:
                    if (value == (Byte)'>')
                    {
                        FinishScannedTag(ref index, selfClosing: true, sourceOffset, trackSourceRanges);
                    }
                    else
                    {
                        _state = State.BeforeAttributeName;
                    }
                    break;
            }
        }
        return index;
    }

    private void FinishScannedTag(
        ref Int32 index,
        Boolean selfClosing,
        Int64 sourceOffset,
        Boolean trackSourceRanges
    )
    {
        index++;
        if (trackSourceRanges)
        {
            _currentSourceOffset = sourceOffset + index;
        }
        FinishTag(selfClosing);
    }

    private static Int32 FindCommentTerminator(ReadOnlySpan<Byte> value)
    {
        var terminator = value.IndexOfAny(CommentTerminators);
        return terminator < 0 ? value.Length : terminator;
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

    private static Boolean IsSpace(Byte value) => value is 0x09 or 0x0A or 0x0C or 0x0D or 0x20;

    private static Boolean IsAsciiLetter(Byte value) =>
        (UInt32)(value - 'A') <= 'Z' - 'A' || (UInt32)(value - 'a') <= 'z' - 'a';

    private static Boolean IsAsciiAlphaNumeric(Byte value) =>
        IsAsciiLetter(value) || (UInt32)(value - '0') <= 9;

    private static Boolean IsTagDelimiter(Byte value) =>
        value is (Byte)'>' or (Byte)'/' || IsSpace(value);

    private static Byte AsciiLower(Byte value) =>
        Utf8NameHash.ToLowerAscii(value);

    private void ThrowIfCompleted()
    {
        if (_completed)
        {
            throw new InvalidOperationException("The tokenizer is already complete.");
        }
    }
}
