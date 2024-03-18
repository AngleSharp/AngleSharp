namespace AngleSharp.Html.Parser
{
    using Common;
    using AngleSharp.Dom;
    using Html;
    using Dom.Events;
    using Text;
    using System;
    using System.Buffers;
    using System.Collections.Generic;
    using Tokens;
    using Tokens.Struct;
    using AttributeName = System.ReadOnlyMemory<System.Char>;

    /// <summary>
    /// Determines if the attribute should be emitted
    /// </summary>
    public delegate Boolean ShouldEmitAttribute(ref StructHtmlToken token, AttributeName attributeName);

    /// <summary>
    /// Token consumption result
    /// </summary>
    public enum TokenConsumptionResult
    {
        /// <summary>
        /// Tokenization should continue.
        /// </summary>
        Continue,

        /// <summary>
        /// Tokenization should stop.
        /// </summary>
        Stop
    }

    /// <summary>
    /// Represent token consumer delegate.
    /// </summary>
    public delegate void TokenConsumer(ref StructHtmlToken token);

    /// <summary>
    /// Represents the callback that is invoked when a token is consumed.
    /// </summary>
    public delegate TokenConsumptionResult TokenizerMiddleware(ref StructHtmlToken token, TokenConsumer next);

    /// <summary>
    /// Performs the tokenization of the source code. Follows the tokenization algorithm at:
    /// http://www.w3.org/html/wg/drafts/html/master/syntax.html
    /// </summary>
    public sealed class HtmlTokenizer : BaseTokenizer
    {
        #region Fields

        private readonly EntityProvider _resolver;
        private StringOrMemory _lastStartTag;
        private TextPosition _position;
        private StructHtmlToken _token;
        private ShouldEmitAttribute _shouldEmitAttribute = static Boolean (ref StructHtmlToken _, AttributeName _) => true;
        private Char[]? _characterReferenceBuffer;

        #endregion

        #region Events

        /// <summary>
        /// Fired in case of a parse error.
        /// </summary>
        public event EventHandler<HtmlErrorEvent>? Error;

        #endregion

        #region ctor

        /// <summary>
        /// See 8.2.4 Tokenization
        /// </summary>
        /// <param name="source">The source code manager.</param>
        /// <param name="resolver">The entity resolver to use.</param>
        public HtmlTokenizer(TextSource source, IEntityProvider resolver)
            : base(source)
        {
            State = HtmlParseMode.PCData;
            _lastStartTag = StringOrMemory.Empty;
            _resolver = new EntityProvider(resolver);
        }

        /// <summary>
        /// See 8.2.4 Tokenization
        /// </summary>
        /// <param name="source">The source code manager.</param>
        /// <param name="resolver">The entity resolver to use.</param>
        public HtmlTokenizer(TextSource source, IEntityProviderExtended resolver)
            : base(source)
        {
            State = HtmlParseMode.PCData;
            _lastStartTag = StringOrMemory.Empty;
            _resolver = new EntityProvider(resolver);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if the tokenizer should skip data text.
        /// </summary>
        public Boolean SkipDataText { get; set; }

        /// <summary>
        /// Gets or sets if the tokenizer should skip script text.
        /// </summary>
        public Boolean SkipScriptText { get; set; }

        /// <summary>
        /// Gets or sets if the tokenizer should skip raw text.
        /// </summary>
        public Boolean SkipRawText { get; set; }

        /// <summary>
        /// Gets or sets if the tokenizer should skip comments.
        /// </summary>
        public Boolean SkipComments { get; set; }

        /// <summary>
        /// Gets or sets if the tokenizer should skip plaintext.
        /// </summary>
        public Boolean SkipPlaintext { get; set; }

        /// <summary>
        /// Gets or sets if the tokenizer should skip RCDATA text.
        /// </summary>
        public Boolean SkipRCDataText { get; set; }

        /// <summary>
        /// Gets or sets if the tokenizer should skip CDATA text.
        /// </summary>
        public Boolean SkipCDATA { get; set; }

        /// <summary>
        /// Gets or sets if the tokenizer should skip processing instructions.
        /// </summary>
        public Boolean SkipProcessingInstructions { get; set; }

        /// <summary>
        /// Gets or sets delegate to determine if the attribute should be emitted.
        /// </summary>
        public ShouldEmitAttribute ShouldEmitAttribute
        {
            get => _shouldEmitAttribute;
            set
            {
                if (value != null)
                {
                    _shouldEmitAttribute = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets if CDATA sections are accepted.
        /// </summary>
        public Boolean IsAcceptingCharacterData
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if attribute names should be taken as is.
        /// </summary>
        public Boolean IsPreservingAttributeNames
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if character references should be avoided.
        /// </summary>
        public Boolean IsNotConsumingCharacterReferences
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the current parse mode.
        /// </summary>
        public HtmlParseMode State
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if strict mode is used.
        /// </summary>
        public Boolean IsStrictMode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if XML processing instructions should
        /// be parsed into DOM nodes.
        /// </summary>
        public Boolean IsSupportingProcessingInstructions
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the callback once a new token is read.
        /// </summary>
        public Action<HtmlToken, TextRange>? OnToken
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the next available token.
        /// </summary>
        /// <returns>The next available token.</returns>
        public HtmlToken Get() => GetStructToken().ToHtmlToken();

        /// <summary>
        /// Gets the next available token as a struct
        /// </summary>
        /// <returns>The next available token.</returns>
        public ref StructHtmlToken GetStructToken()
        {
            ref var token = ref GetNextStructToken();
            OnToken?.Invoke(token.ToHtmlToken(), new TextRange(_position, GetCurrentPosition().After(Current)));
            return ref token;
        }

        internal void RaiseErrorOccurred(HtmlParseError code, TextPosition position)
        {
            var handler = Error;

            if (IsStrictMode)
            {
                var message = "Error while parsing the provided HTML document.";
                throw new HtmlParseException(code.GetCode(), message, position);
            }
            else if (handler != null)
            {
                var errorEvent = new HtmlErrorEvent(code, position);
                handler.Invoke(this, errorEvent);
            }
        }

        #endregion

        #region Data

        /// <summary>
        /// See 8.2.4.1 Data state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private ref StructHtmlToken Data(Char c)
        {
            return ref c == Symbols.LessThan ? ref TagOpen(GetNext()) : ref DataText(c);
        }

        private ref StructHtmlToken DataText(Char c)
        {
            while (true)
            {
                switch (c)
                {
                    case Symbols.LessThan:
                    case Symbols.EndOfFile:
                        Back();
                        if (SkipDataText)
                        {
                            return ref NewSkippedContent();
                        }
                        return ref NewCharacter();

                    case Symbols.Ampersand:
                        AppendCharacterReference(GetNext());
                        break;

                    case Symbols.Null:
                        RaiseErrorOccurred(HtmlParseError.Null);
                        break;

                    default:
                        Append(c);
                        break;
                }

                c = GetNext();
            }
        }

        #endregion

        #region Plaintext

        /// <summary>
        /// See 8.2.4.7 PLAINTEXT state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private ref StructHtmlToken Plaintext(Char c)
        {
            while (true)
            {
                switch (c)
                {
                    case Symbols.Null:
                        AppendReplacement();
                        break;

                    case Symbols.EndOfFile:
                        Back();

                        if (SkipPlaintext)
                        {
                            return ref NewSkippedContent();
                        }

                        return ref NewCharacter();

                    default:
                        Append(c);
                        break;
                }

                c = GetNext();
            }
        }

        #endregion

        #region RCData

        /// <summary>
        /// See 8.2.4.3 RCDATA state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private ref StructHtmlToken RCData(Char c)
        {
            return ref c == Symbols.LessThan ? ref RCDataLt(GetNext()) : ref RCDataText(c);
        }

        private ref StructHtmlToken RCDataText(Char c)
        {
            while (true)
            {
                switch (c)
                {
                    case Symbols.Ampersand:
                        AppendCharacterReference(GetNext());
                        break;

                    case Symbols.LessThan:
                    case Symbols.EndOfFile:
                        Back();
                        if (SkipRCDataText)
                        {
                            return ref NewSkippedContent();
                        }

                        return ref NewCharacter();

                    case Symbols.Null:
                        AppendReplacement();
                        break;

                    default:
                        Append(c);
                        break;
                }

                c = GetNext();
            }
        }

        /// <summary>
        /// See 8.2.4.11 RCDATA less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private ref StructHtmlToken RCDataLt(Char c)
        {
            if (c == Symbols.Solidus)
            {
                // See 8.2.4.12 RCDATA end tag open state
                c = GetNext();

                if (c.IsUppercaseAscii())
                {
                    Append(Char.ToLowerInvariant(c));
                    return ref RCDataNameEndTag(GetNext());
                }
                else if (c.IsLowercaseAscii())
                {
                    Append(c);
                    return ref RCDataNameEndTag(GetNext());
                }
                else
                {
                    Append(Symbols.LessThan, Symbols.Solidus);
                    return ref RCDataText(c);
                }
            }
            else
            {
                Append(Symbols.LessThan);
                return ref RCDataText(c);
            }
        }

        /// <summary>
        /// See 8.2.4.13 RCDATA end tag name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private ref StructHtmlToken RCDataNameEndTag(Char c)
        {
            while (true)
            {
                if (CreateIfAppropriate(c, ref _token))
                {
                    return ref _token;
                }
                else if (c.IsUppercaseAscii())
                {
                    Append(Char.ToLowerInvariant(c));
                }
                else if (c.IsLowercaseAscii())
                {
                    Append(c);
                }
                else
                {
                    CharBuffer.Insert(0, Symbols.LessThan).Insert(1, Symbols.Solidus);
                    return ref RCDataText(c);
                }

                c = GetNext();
            }
        }

        #endregion

        #region Rawtext

        /// <summary>
        /// See 8.2.4.5 RAWTEXT state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private ref StructHtmlToken Rawtext(Char c)
        {
            return ref c == Symbols.LessThan ? ref RawtextLT(GetNext()) : ref RawtextText(c);
        }

        private ref StructHtmlToken RawtextText(Char c)
        {
            while (true)
            {
                switch (c)
                {
                    case Symbols.LessThan:
                    case Symbols.EndOfFile:
                        Back();
                        if (SkipRawText)
                        {
                            return ref NewSkippedContent();
                        }

                        return ref NewCharacter();

                    case Symbols.Null:
                        AppendReplacement();
                        break;

                    default:
                        Append(c);
                        break;
                }

                c = GetNext();
            }
        }

        /// <summary>
        /// See 8.2.4.14 RAWTEXT less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private ref StructHtmlToken RawtextLT(Char c)
        {
            if (c == Symbols.Solidus)
            {
                // See 8.2.4.15 RAWTEXT end tag open state
                c = GetNext();

                if (c.IsUppercaseAscii())
                {
                    Append(Char.ToLowerInvariant(c));
                    return ref RawtextNameEndTag(GetNext());
                }
                else if (c.IsLowercaseAscii())
                {
                    Append(c);
                    return ref RawtextNameEndTag(GetNext());
                }
                else
                {
                    Append(Symbols.LessThan, Symbols.Solidus);
                    return ref RawtextText(c);
                }
            }
            else
            {
                Append(Symbols.LessThan);
                return ref RawtextText(c);
            }
        }

        /// <summary>
        /// See 8.2.4.16 RAWTEXT end tag name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private ref StructHtmlToken RawtextNameEndTag(Char c)
        {
            while (true)
            {
                if (CreateIfAppropriate(c, ref _token))
                {
                    return ref _token;
                }
                else if (c.IsUppercaseAscii())
                {
                    Append(Char.ToLowerInvariant(c));
                }
                else if (c.IsLowercaseAscii())
                {
                    Append(c);
                }
                else
                {
                    CharBuffer.Insert(0, Symbols.LessThan).Insert(1, Symbols.Solidus);
                    return ref RawtextText(c);
                }

                c = GetNext();
            }
        }

        #endregion

        #region CDATA

        /// <summary>
        /// See 8.2.4.68 CDATA section state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private ref StructHtmlToken CharacterData(Char c)
        {
            while (true)
            {
                if (c == Symbols.EndOfFile)
                {
                    Back();
                    break;
                }
                else if (c == Symbols.SquareBracketClose && ContinuesWithSensitive("]]>"))
                {
                    Advance(2);
                    break;
                }
                else
                {
                    Append(c);
                    c = GetNext();
                }
            }

            if (SkipCDATA)
            {
                return ref NewSkippedContent();
            }

            return ref NewCharacter();
        }

        /// <summary>
        /// See 8.2.4.69 Tokenizing character references
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="allowedCharacter">The additionally allowed character if there is one.</param>
        /// <param name="isAttribute">Determines if we are in attribute parsing. Here, non-terminated refs are allowed.</param>
        private void AppendCharacterReference(Char c, Char allowedCharacter = Symbols.Null, Boolean isAttribute = false)
        {
            if (IsNotConsumingCharacterReferences || c.IsSpaceCharacter() || c == Symbols.LessThan || c == Symbols.EndOfFile || c == Symbols.Ampersand || c == allowedCharacter)
            {
                Back();
                Append(Symbols.Ampersand);
            }
            else
            {
                var entity = default(String);

                if (c == Symbols.Num)
                {
                    entity = GetNumericCharacterReference(GetNext(), isAttribute);
                }
                else
                {
                    entity = GetLookupCharacterReference(allowedCharacter, isAttribute);
                }

                if (entity is null)
                {
                    Append(Symbols.Ampersand);
                }
                else
                {
                    CharBuffer.Append(entity.AsSpan());
                }
            }
        }

        private String? GetNumericCharacterReference(Char c, Boolean isAttribute)
        {
            var exp = 10;
            var basis = 1;
            var num = 0;
            var nums = new List<Int32>();
            var isHex = c == 'x' || c == 'X';

            if (isHex)
            {
                exp = 16;

                while ((c = GetNext()).IsHex())
                {
                    nums.Add(c.FromHex());
                }
            }
            else
            {
                while (c.IsDigit())
                {
                    nums.Add(c.FromHex());
                    c = GetNext();
                }
            }

            for (var i = nums.Count - 1; i >= 0; i--)
            {
                num += nums[i] * basis;
                basis *= exp;
            }

            if (nums.Count == 0)
            {
                Back(2);

                if (isHex)
                {
                    Back();
                }

                if (!isAttribute)
                {
                    RaiseErrorOccurred(HtmlParseError.CharacterReferenceWrongNumber);
                }

                return null;
            }

            if (c != Symbols.Semicolon)
            {
                RaiseErrorOccurred(HtmlParseError.CharacterReferenceSemicolonMissing);
                Back();
            }

            if (HtmlEntityProvider.IsInCharacterTable(num))
            {
                RaiseErrorOccurred(HtmlParseError.CharacterReferenceInvalidCode);
                return HtmlEntityProvider.GetSymbolFromTable(num);
            }
            else if (HtmlEntityProvider.IsInvalidNumber(num))
            {
                RaiseErrorOccurred(HtmlParseError.CharacterReferenceInvalidNumber);
                return Symbols.Replacement.ToString();
            }
            else if (HtmlEntityProvider.IsInInvalidRange(num))
            {
                RaiseErrorOccurred(HtmlParseError.CharacterReferenceInvalidRange);
            }

            return Char.ConvertFromUtf32(num);
        }

        private String? GetLookupCharacterReference(Char allowedCharacter, Boolean isAttribute)
        {
            var entity = default(String);
            var start = InsertionPoint - 1;
            _characterReferenceBuffer ??= new Char[32];

            var index = 0;
            var chr = Current;

            do
            {
                if (chr == Symbols.Semicolon || !chr.IsName())
                {
                    break;
                }

                _characterReferenceBuffer[index++] = chr;
                chr = GetNext();
            }
            while (chr != Symbols.EndOfFile && index < 31);

            if (chr == Symbols.Semicolon)
            {
                _characterReferenceBuffer[index] = Symbols.Semicolon;
                entity = _resolver.GetSymbol(new StringOrMemory(_characterReferenceBuffer.AsMemory(0, index + 1)));
            }

            while (entity is null && index > 0)
            {
                entity = _resolver.GetSymbol(new StringOrMemory(_characterReferenceBuffer.AsMemory(0, index--)));
                if (entity is null)
                {
                    Back();
                }
            }

            chr = Current;

            if (chr != Symbols.Semicolon)
            {
                if (allowedCharacter != Symbols.Null && (chr == Symbols.Equality || chr.IsAlphanumericAscii()))
                {
                    if (chr == Symbols.Equality)
                    {
                        RaiseErrorOccurred(HtmlParseError.CharacterReferenceAttributeEqualsFound);
                    }

                    InsertionPoint = start;
                    return null;
                }

                Back();

                if (!isAttribute)
                {
                    RaiseErrorOccurred(HtmlParseError.CharacterReferenceNotTerminated);
                }
            }

            return entity;
        }

        #endregion

        #region Tags

        /// <summary>
        /// See 8.2.4.8 Tag open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private ref StructHtmlToken TagOpen(Char c)
        {
            if (c == Symbols.Solidus)
            {
                return ref TagEnd(GetNext());
            }
            else if (c.IsLowercaseAscii())
            {
                Append(c);
                return ref TagName(ref NewTagOpen());
            }
            else if (c.IsUppercaseAscii())
            {
                Append(Char.ToLowerInvariant(c));
                return ref TagName(ref NewTagOpen());
            }
            else if (c == Symbols.ExclamationMark)
            {
                return ref MarkupDeclaration(GetNext());
            }
            else if (c == Symbols.QuestionMark && IsSupportingProcessingInstructions)
            {
                return ref ProcessingInstruction(c);
            }
            else if (c != Symbols.QuestionMark)
            {
                State = HtmlParseMode.PCData;
                RaiseErrorOccurred(HtmlParseError.AmbiguousOpenTag);
                Append(Symbols.LessThan);
                return ref DataText(c);
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.BogusComment);
                return ref BogusComment(c);
            }
        }

        /// <summary>
        /// See 8.2.4.9 End tag open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private ref StructHtmlToken TagEnd(Char c)
        {
            if (c.IsLowercaseAscii())
            {
                Append(c);
                return ref TagName(ref NewTagClose());
            }
            else if (c.IsUppercaseAscii())
            {
                Append(Char.ToLowerInvariant(c));
                return ref TagName(ref NewTagClose());
            }
            else if (c == Symbols.GreaterThan)
            {
                State = HtmlParseMode.PCData;
                RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                return ref Data(GetNext());
            }
            else if (c == Symbols.EndOfFile)
            {
                Back();
                RaiseErrorOccurred(HtmlParseError.EOF);
                Append(Symbols.LessThan, Symbols.Solidus);
                return ref NewCharacter();
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.BogusComment);
                return ref BogusComment(c);
            }
        }

        /// <summary>
        /// See 8.2.4.10 Tag name state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        private ref StructHtmlToken TagName(ref StructHtmlToken tag)
        {
            while (true)
            {
                var c = GetNext();

                if (c == Symbols.GreaterThan)
                {
                    tag.Name = FlushBufferFast(stringResolver: HtmlTagNameLookup.TryGetWellKnownTagName);
                    return ref EmitTag(ref tag);
                }
                else if (c.IsSpaceCharacter())
                {
                    tag.Name = FlushBufferFast(stringResolver: HtmlTagNameLookup.TryGetWellKnownTagName);
                    return ref ParseAttributes(ref tag);
                }
                else if (c == Symbols.Solidus)
                {
                    tag.Name = FlushBufferFast(stringResolver: HtmlTagNameLookup.TryGetWellKnownTagName);
                    return ref TagSelfClosing(ref tag);
                }
                else if (c.IsUppercaseAscii())
                {
                    Append(Char.ToLowerInvariant(c));
                }
                else if (c == Symbols.Null)
                {
                    AppendReplacement();
                }
                else if (c != Symbols.EndOfFile)
                {
                    Append(c);
                }
                else
                {
                    return ref NewEof();
                }
            }
        }

        /// <summary>
        /// See 8.2.4.43 Self-closing start tag state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        private ref StructHtmlToken TagSelfClosing(ref StructHtmlToken tag)
        {
            if (TagSelfClosingInner(ref tag))
            {
                return ref tag;
            }
            else
            {
                return ref ParseAttributes(ref tag);
            }
        }

        private Boolean TagSelfClosingInner(ref StructHtmlToken tag)
        {
            while (true)
            {
                switch (GetNext())
                {
                    case Symbols.GreaterThan:
                        tag.IsSelfClosing = true;
                        tag = EmitTag(ref tag);
                        return true;
                    case Symbols.EndOfFile:
                        tag = NewEof();
                        return true;
                    case Symbols.Solidus:
                        RaiseErrorOccurred(HtmlParseError.ClosingSlashMisplaced);
                        break;
                    default:
                        RaiseErrorOccurred(HtmlParseError.ClosingSlashMisplaced);
                        Back();
                        return false;
                }
            }
        }

        /// <summary>
        /// See 8.2.4.45 Markup declaration open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private ref StructHtmlToken MarkupDeclaration(Char c)
        {
            if (ContinuesWithSensitive("--"))
            {
                Advance();
                return ref CommentStart(GetNext());
            }
            else if (ContinuesWithInsensitive(TagNames.Doctype))
            {
                Advance(6);
                return ref Doctype(GetNext());
            }
            else if (IsAcceptingCharacterData && ContinuesWithSensitive(Keywords.CData))
            {
                Advance(6);
                return ref CharacterData(GetNext());
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.UndefinedMarkupDeclaration);
                return ref BogusComment(c);
            }
        }

        #endregion

        #region ProcessingInstructions

        private ref StructHtmlToken ProcessingInstruction(Char c)
        {
            CharBuffer.Discard();

            while (true)
            {
                switch (c)
                {
                    case Symbols.GreaterThan:
                        break;
                    case Symbols.EndOfFile:
                        Back();
                        break;
                    case Symbols.Null:
                        c = Symbols.Replacement;
                        goto default;
                    default:
                        Append(c);
                        c = GetNext();
                        continue;
                }

                State = HtmlParseMode.PCData;

                return ref NewProcessingInstruction();
            }
        }

        #endregion

        #region Comments

        /// <summary>
        /// See 8.2.4.44 Bogus comment state
        /// </summary>
        /// <param name="c">The current character.</param>
        private ref StructHtmlToken BogusComment(Char c)
        {
            CharBuffer.Discard();

            while (true)
            {
                switch (c)
                {
                    case Symbols.GreaterThan:
                        break;
                    case Symbols.EndOfFile:
                        Back();
                        break;
                    case Symbols.Null:
                        c = Symbols.Replacement;
                        goto default;
                    default:
                        Append(c);
                        c = GetNext();
                        continue;
                }

                State = HtmlParseMode.PCData;
                return ref NewComment();
            }
        }

        /// <summary>
        /// See 8.2.4.46 Comment start state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private ref StructHtmlToken CommentStart(Char c)
        {
            CharBuffer.Discard();

            switch (c)
            {
                case Symbols.Minus:
                    if (CommentDashStart(GetNext(), ref _token))
                    {
                        return ref _token;
                    }
                    return  ref Comment(GetNext());
                case Symbols.Null:
                    AppendReplacement();
                    return ref Comment(GetNext());
                case Symbols.GreaterThan:
                    State = HtmlParseMode.PCData;
                    RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                    break;
                case Symbols.EndOfFile:
                    RaiseErrorOccurred(HtmlParseError.EOF);
                    Back();
                    break;
                default:
                    Append(c);
                    return ref Comment(GetNext());
            }

            return ref NewComment();
        }

        /// <summary>
        /// See 8.2.4.47 Comment start dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="token"></param>
        private Boolean CommentDashStart(Char c, ref StructHtmlToken token)
        {
            switch (c)
            {
                case Symbols.Minus:
                    return CommentEnd(GetNext(), ref token);
                case Symbols.Null:
                    RaiseErrorOccurred(HtmlParseError.Null);
                    Append(Symbols.Minus, Symbols.Replacement);
                    token = ref Comment(GetNext());
                    return true;
                case Symbols.GreaterThan:
                    State = HtmlParseMode.PCData;
                    RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                    break;
                case Symbols.EndOfFile:
                    RaiseErrorOccurred(HtmlParseError.EOF);
                    Back();
                    break;
                default:
                    Append(Symbols.Minus, c);
                    token = ref Comment(GetNext());
                    return true;
            }

            token = NewComment();
            return true;
        }

        /// <summary>
        /// See 8.2.4.48 Comment state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private ref StructHtmlToken Comment(Char c)
        {
            while (true)
            {
                switch (c)
                {
                    case Symbols.Minus:
                        if (CommentDashEnd(GetNext(), ref _token))
                        {
                            return ref _token;
                        }
                        break;
                    case Symbols.EndOfFile:
                        RaiseErrorOccurred(HtmlParseError.EOF);
                        Back();
                        return ref NewComment();
                    case Symbols.Null:
                        AppendReplacement();
                        break;
                    default:
                        Append(c);
                        break;
                }

                c = GetNext();
            }
        }

        /// <summary>
        /// See 8.2.4.49 Comment end dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="token"></param>
        private Boolean CommentDashEnd(Char c, ref StructHtmlToken token)
        {
            switch (c)
            {
                case Symbols.Minus:
                    return CommentEnd(GetNext(), ref token);
                case Symbols.EndOfFile:
                    RaiseErrorOccurred(HtmlParseError.EOF);
                    Back();
                    token = NewComment();
                    return true;
                case Symbols.Null:
                    RaiseErrorOccurred(HtmlParseError.Null);
                    c = Symbols.Replacement;
                    break;
            }

            Append(Symbols.Minus, c);
            return false;
        }

        /// <summary>
        /// See 8.2.4.50 Comment end state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="token"></param>
        private Boolean CommentEnd(Char c, ref StructHtmlToken token)
        {
            while (true)
            {
                switch (c)
                {
                    case Symbols.GreaterThan:
                        State = HtmlParseMode.PCData;
                        token = NewComment();
                        return true;
                    case Symbols.Null:
                        RaiseErrorOccurred(HtmlParseError.Null);
                        Append(Symbols.Minus, Symbols.Replacement);
                        return false;
                    case Symbols.ExclamationMark:
                        RaiseErrorOccurred(HtmlParseError.CommentEndedWithEM);
                        return CommentBangEnd(GetNext(), ref token);
                    case Symbols.Minus:
                        RaiseErrorOccurred(HtmlParseError.CommentEndedWithDash);
                        Append(Symbols.Minus);
                        break;
                    case Symbols.EndOfFile:
                        RaiseErrorOccurred(HtmlParseError.EOF);
                        Back();
                        token = NewComment();
                        return true;
                    default:
                        RaiseErrorOccurred(HtmlParseError.CommentEndedUnexpected);
                        Append(Symbols.Minus, Symbols.Minus, c);
                        return false;
                }

                c = GetNext();
            }
        }

        /// <summary>
        /// See 8.2.4.51 Comment end bang state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="token"></param>
        private Boolean CommentBangEnd(Char c, ref StructHtmlToken token)
        {
            switch (c)
            {
                case Symbols.Minus:
                    Append(Symbols.Minus, Symbols.Minus, Symbols.ExclamationMark);
                    return CommentDashEnd(GetNext(), ref token);
                case Symbols.GreaterThan:
                    State = HtmlParseMode.PCData;
                    break;
                case Symbols.Null:
                    RaiseErrorOccurred(HtmlParseError.Null);
                    Append(Symbols.Minus, Symbols.Minus, Symbols.ExclamationMark, Symbols.Replacement);
                    return false;
                case Symbols.EndOfFile:
                    RaiseErrorOccurred(HtmlParseError.EOF);
                    Back();
                    break;
                default:
                    Append(Symbols.Minus, Symbols.Minus, Symbols.ExclamationMark, c);
                    return false;
            }

            token = NewComment();
            return true;
        }

        #endregion

        #region Doctype

        /// <summary>
        /// See 8.2.4.52 DOCTYPE state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private ref StructHtmlToken Doctype(Char c)
        {
            if (c.IsSpaceCharacter())
            {
                return ref DoctypeNameBefore(GetNext());
            }
            else if (c == Symbols.EndOfFile)
            {
                RaiseErrorOccurred(HtmlParseError.EOF);
                Back();
                return ref NewDoctype(true);
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.DoctypeUnexpected);
                return ref DoctypeNameBefore(c);
            }
        }

        /// <summary>
        /// See 8.2.4.53 Before DOCTYPE name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private ref StructHtmlToken DoctypeNameBefore(Char c)
        {
            while (c.IsSpaceCharacter())
            {
                c = GetNext();
            }

            if (c.IsUppercaseAscii())
            {
                ref var doctype = ref NewDoctype(false);
                Append(Char.ToLowerInvariant(c));
                return ref DoctypeName(ref doctype);
            }
            else if (c == Symbols.Null)
            {
                ref var doctype = ref NewDoctype(false);
                AppendReplacement();
                return ref DoctypeName(ref doctype);
            }
            else if (c == Symbols.GreaterThan)
            {
                ref var doctype = ref NewDoctype(true);
                State = HtmlParseMode.PCData;
                RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                return ref doctype;
            }
            else if (c == Symbols.EndOfFile)
            {
                ref var doctype = ref NewDoctype(true);
                RaiseErrorOccurred(HtmlParseError.EOF);
                Back();
                return ref doctype;
            }
            else
            {
                ref var doctype = ref NewDoctype(false);
                Append(c);
                return ref DoctypeName(ref doctype);
            }
        }

        /// <summary>
        /// See 8.2.4.54 DOCTYPE name state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private ref StructHtmlToken DoctypeName(ref StructHtmlToken doctype)
        {
            while (true)
            {
                var c = GetNext();

                if (c.IsSpaceCharacter())
                {
                    doctype.Name = FlushBufferFast();
                    return ref DoctypeNameAfter(ref doctype);
                }
                else if (c == Symbols.GreaterThan)
                {
                    State = HtmlParseMode.PCData;
                    doctype.Name = FlushBufferFast();
                    break;
                }
                else if (c.IsUppercaseAscii())
                {
                    Append(Char.ToLowerInvariant(c));
                }
                else if (c == Symbols.Null)
                {
                    AppendReplacement();
                }
                else if (c == Symbols.EndOfFile)
                {
                    RaiseErrorOccurred(HtmlParseError.EOF);
                    Back();
                    doctype.IsQuirksForced = true;
                    doctype.Name = FlushBufferFast();
                    break;
                }
                else
                {
                    Append(c);
                }
            }

            return ref doctype;
        }

        /// <summary>
        /// See 8.2.4.55 After DOCTYPE name state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private ref StructHtmlToken DoctypeNameAfter(ref StructHtmlToken doctype)
        {
            var c = SkipSpaces();

            if (c == Symbols.GreaterThan)
            {
                State = HtmlParseMode.PCData;
            }
            else if (c == Symbols.EndOfFile)
            {
                RaiseErrorOccurred(HtmlParseError.EOF);
                Back();
                doctype.IsQuirksForced = true;
            }
            else if (ContinuesWithInsensitive(Keywords.Public))
            {
                Advance(5);
                return ref DoctypePublic(ref doctype);
            }
            else if (ContinuesWithInsensitive(Keywords.System))
            {
                Advance(5);
                return ref DoctypeSystem(ref doctype);
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.DoctypeUnexpectedAfterName);
                doctype.IsQuirksForced = true;
                return ref BogusDoctype(ref doctype);
            }

            return ref doctype;
        }

        /// <summary>
        /// See 8.2.4.56 After DOCTYPE public keyword state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private ref StructHtmlToken DoctypePublic(ref StructHtmlToken doctype)
        {
            var c = GetNext();

            if (c.IsSpaceCharacter())
            {
                return ref DoctypePublicIdentifierBefore(ref doctype);
            }
            else if (c == Symbols.DoubleQuote)
            {
                RaiseErrorOccurred(HtmlParseError.DoubleQuotationMarkUnexpected);
                doctype.PublicIdentifier = StringOrMemory.Empty;
                return ref DoctypePublicIdentifierDoubleQuoted(ref doctype);
            }
            else if (c == Symbols.SingleQuote)
            {
                RaiseErrorOccurred(HtmlParseError.SingleQuotationMarkUnexpected);
                doctype.PublicIdentifier = StringOrMemory.Empty;
                return ref DoctypePublicIdentifierSingleQuoted(ref doctype);
            }
            else if (c == Symbols.GreaterThan)
            {
                State = HtmlParseMode.PCData;
                RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                doctype.IsQuirksForced = true;
            }
            else if (c == Symbols.EndOfFile)
            {
                RaiseErrorOccurred(HtmlParseError.EOF);
                doctype.IsQuirksForced = true;
                Back();
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.DoctypePublicInvalid);
                doctype.IsQuirksForced = true;
                return ref BogusDoctype(ref doctype);
            }

            return ref doctype;
        }

        /// <summary>
        /// See 8.2.4.57 Before DOCTYPE public identifier state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private ref StructHtmlToken DoctypePublicIdentifierBefore(ref StructHtmlToken doctype)
        {
            var c = SkipSpaces();

            if (c == Symbols.DoubleQuote)
            {
                doctype.PublicIdentifier = StringOrMemory.Empty;
                return ref DoctypePublicIdentifierDoubleQuoted(ref doctype);
            }
            else if (c == Symbols.SingleQuote)
            {
                doctype.PublicIdentifier = StringOrMemory.Empty;
                return ref DoctypePublicIdentifierSingleQuoted(ref doctype);
            }
            else if (c == Symbols.GreaterThan)
            {
                State = HtmlParseMode.PCData;
                RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                doctype.IsQuirksForced = true;
            }
            else if (c == Symbols.EndOfFile)
            {
                RaiseErrorOccurred(HtmlParseError.EOF);
                doctype.IsQuirksForced = true;
                Back();
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.DoctypePublicInvalid);
                doctype.IsQuirksForced = true;
                return ref BogusDoctype(ref doctype);
            }

            return ref doctype;
        }

        /// <summary>
        /// See 8.2.4.58 DOCTYPE public identifier (double-quoted) state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private ref StructHtmlToken DoctypePublicIdentifierDoubleQuoted(ref StructHtmlToken doctype)
        {
            while (true)
            {
                var c = GetNext();

                if (c == Symbols.DoubleQuote)
                {
                    doctype.PublicIdentifier = FlushBufferFast();
                    return ref DoctypePublicIdentifierAfter(ref doctype);
                }
                else if (c == Symbols.Null)
                {
                    AppendReplacement();
                }
                else if (c == Symbols.GreaterThan)
                {
                    State = HtmlParseMode.PCData;
                    RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = FlushBufferFast();
                    break;
                }
                else if (c == Symbols.EndOfFile)
                {
                    RaiseErrorOccurred(HtmlParseError.EOF);
                    Back();
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = FlushBufferFast();
                    break;
                }
                else
                {
                    Append(c);
                }
            }

            return ref doctype;
        }

        /// <summary>
        /// See 8.2.4.59 DOCTYPE public identifier (single-quoted) state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private ref StructHtmlToken DoctypePublicIdentifierSingleQuoted(ref StructHtmlToken doctype)
        {
            while (true)
            {
                var c = GetNext();

                if (c == Symbols.SingleQuote)
                {
                    doctype.PublicIdentifier = FlushBufferFast();
                    return ref DoctypePublicIdentifierAfter(ref doctype);
                }
                else if (c == Symbols.Null)
                {
                    AppendReplacement();
                }
                else if (c == Symbols.GreaterThan)
                {
                    State = HtmlParseMode.PCData;
                    RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = FlushBufferFast();
                    break;
                }
                else if (c == Symbols.EndOfFile)
                {
                    RaiseErrorOccurred(HtmlParseError.EOF);
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = FlushBufferFast();
                    Back();
                    break;
                }
                else
                {
                    Append(c);
                }
            }

            return ref doctype;
        }

        /// <summary>
        /// See 8.2.4.60 After DOCTYPE public identifier state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private ref StructHtmlToken DoctypePublicIdentifierAfter(ref StructHtmlToken doctype)
        {
            var c = GetNext();

            if (c.IsSpaceCharacter())
            {
                return ref DoctypeBetween(ref doctype);
            }
            else if (c == Symbols.GreaterThan)
            {
                State = HtmlParseMode.PCData;
            }
            else if (c == Symbols.DoubleQuote)
            {
                RaiseErrorOccurred(HtmlParseError.DoubleQuotationMarkUnexpected);
                doctype.SystemIdentifier = StringOrMemory.Empty;
                return ref DoctypeSystemIdentifierDoubleQuoted(ref doctype);
            }
            else if (c == Symbols.SingleQuote)
            {
                RaiseErrorOccurred(HtmlParseError.SingleQuotationMarkUnexpected);
                doctype.SystemIdentifier = StringOrMemory.Empty;
                return ref DoctypeSystemIdentifierSingleQuoted(ref doctype);
            }
            else if (c == Symbols.EndOfFile)
            {
                RaiseErrorOccurred(HtmlParseError.EOF);
                doctype.IsQuirksForced = true;
                Back();
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.DoctypeInvalidCharacter);
                doctype.IsQuirksForced = true;
                return ref BogusDoctype(ref doctype);
            }

            return ref doctype;
        }

        /// <summary>
        /// See 8.2.4.61 Between DOCTYPE public and system identifiers state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private ref StructHtmlToken DoctypeBetween(ref StructHtmlToken doctype)
        {
            var c = SkipSpaces();

            if (c == Symbols.GreaterThan)
            {
                State = HtmlParseMode.PCData;
            }
            else if (c == Symbols.DoubleQuote)
            {
                doctype.SystemIdentifier = StringOrMemory.Empty;
                return ref DoctypeSystemIdentifierDoubleQuoted(ref doctype);
            }
            else if (c == Symbols.SingleQuote)
            {
                doctype.SystemIdentifier = StringOrMemory.Empty;
                return ref DoctypeSystemIdentifierSingleQuoted(ref doctype);
            }
            else if (c == Symbols.EndOfFile)
            {
                RaiseErrorOccurred(HtmlParseError.EOF);
                doctype.IsQuirksForced = true;
                Back();
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.DoctypeInvalidCharacter);
                doctype.IsQuirksForced = true;
                return ref BogusDoctype(ref doctype);
            }

            return ref doctype;
        }

        /// <summary>
        /// See 8.2.4.62 After DOCTYPE system keyword state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private ref StructHtmlToken DoctypeSystem(ref StructHtmlToken doctype)
        {
            var c = GetNext();

            if (c.IsSpaceCharacter())
            {
                State = HtmlParseMode.PCData;
                return ref DoctypeSystemIdentifierBefore(ref doctype);
            }
            else if (c == Symbols.DoubleQuote)
            {
                RaiseErrorOccurred(HtmlParseError.DoubleQuotationMarkUnexpected);
                doctype.SystemIdentifier = StringOrMemory.Empty;
                return ref DoctypeSystemIdentifierDoubleQuoted(ref doctype);
            }
            else if (c == Symbols.SingleQuote)
            {
                RaiseErrorOccurred(HtmlParseError.SingleQuotationMarkUnexpected);
                doctype.SystemIdentifier = StringOrMemory.Empty;
                return ref DoctypeSystemIdentifierSingleQuoted(ref doctype);
            }
            else if (c == Symbols.GreaterThan)
            {
                RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                doctype.SystemIdentifier = FlushBufferFast();
                doctype.IsQuirksForced = true;
            }
            else if (c == Symbols.EndOfFile)
            {
                RaiseErrorOccurred(HtmlParseError.EOF);
                doctype.IsQuirksForced = true;
                Back();
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.DoctypeSystemInvalid);
                doctype.IsQuirksForced = true;
                return ref BogusDoctype(ref doctype);
            }

            return ref doctype;
        }

        /// <summary>
        /// See 8.2.4.63 Before DOCTYPE system identifier state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private ref StructHtmlToken DoctypeSystemIdentifierBefore(ref StructHtmlToken doctype)
        {
            var c = SkipSpaces();

            if (c == Symbols.DoubleQuote)
            {
                doctype.SystemIdentifier = StringOrMemory.Empty;
                return ref DoctypeSystemIdentifierDoubleQuoted(ref doctype);
            }
            else if (c == Symbols.SingleQuote)
            {
                doctype.SystemIdentifier = StringOrMemory.Empty;
                return ref DoctypeSystemIdentifierSingleQuoted(ref doctype);
            }
            else if (c == Symbols.GreaterThan)
            {
                State = HtmlParseMode.PCData;
                RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = FlushBufferFast();
            }
            else if (c == Symbols.EndOfFile)
            {
                RaiseErrorOccurred(HtmlParseError.EOF);
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = FlushBufferFast();
                Back();
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.DoctypeInvalidCharacter);
                doctype.IsQuirksForced = true;
                return ref BogusDoctype(ref doctype);
            }

            return ref doctype;
        }

        /// <summary>
        /// See 8.2.4.64 DOCTYPE system identifier (double-quoted) state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private ref StructHtmlToken DoctypeSystemIdentifierDoubleQuoted(ref StructHtmlToken doctype)
        {
            while (true)
            {
                var c = GetNext();

                if (c == Symbols.DoubleQuote)
                {
                    doctype.SystemIdentifier = FlushBufferFast();
                    return ref DoctypeSystemIdentifierAfter(ref doctype);
                }
                else if (c == Symbols.Null)
                {
                    AppendReplacement();
                }
                else if (c == Symbols.GreaterThan)
                {
                    State = HtmlParseMode.PCData;
                    RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                    doctype.IsQuirksForced = true;
                    doctype.SystemIdentifier = FlushBufferFast();
                    break;
                }
                else if (c == Symbols.EndOfFile)
                {
                    RaiseErrorOccurred(HtmlParseError.EOF);
                    doctype.IsQuirksForced = true;
                    doctype.SystemIdentifier = FlushBufferFast();
                    Back();
                    break;
                }
                else
                {
                    Append(c);
                }
            }

            return ref doctype;
        }

        /// <summary>
        /// See 8.2.4.65 DOCTYPE system identifier (single-quoted) state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private ref StructHtmlToken DoctypeSystemIdentifierSingleQuoted(ref StructHtmlToken doctype)
        {
            while (true)
            {
                var c = GetNext();

                switch (c)
                {
                    case Symbols.SingleQuote:
                        doctype.SystemIdentifier = FlushBufferFast();
                        return ref DoctypeSystemIdentifierAfter(ref doctype);
                    case Symbols.Null:
                        AppendReplacement();
                        continue;
                    case Symbols.GreaterThan:
                        State = HtmlParseMode.PCData;
                        RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                        doctype.IsQuirksForced = true;
                        doctype.SystemIdentifier = FlushBufferFast();
                        break;
                    case Symbols.EndOfFile:
                        RaiseErrorOccurred(HtmlParseError.EOF);
                        doctype.IsQuirksForced = true;
                        doctype.SystemIdentifier = FlushBufferFast();
                        Back();
                        break;
                    default:
                        Append(c);
                        continue;
                }

                return ref doctype;
            }
        }

        /// <summary>
        /// See 8.2.4.66 After DOCTYPE system identifier state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private ref StructHtmlToken DoctypeSystemIdentifierAfter(ref StructHtmlToken doctype)
        {
            var c = SkipSpaces();

            switch (c)
            {
                case Symbols.GreaterThan:
                    State = HtmlParseMode.PCData;
                    break;
                case Symbols.EndOfFile:
                    RaiseErrorOccurred(HtmlParseError.EOF);
                    doctype.IsQuirksForced = true;
                    Back();
                    break;
                default:
                    RaiseErrorOccurred(HtmlParseError.DoctypeInvalidCharacter);
                    return ref BogusDoctype(ref doctype);
            }

            return ref doctype;
        }

        /// <summary>
        /// See 8.2.4.67 Bogus DOCTYPE state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private ref StructHtmlToken BogusDoctype(ref StructHtmlToken doctype)
        {
            while (true)
            {
                switch (GetNext())
                {
                    case Symbols.GreaterThan:
                        State = HtmlParseMode.PCData;
                        break;
                    case Symbols.EndOfFile:
                        Back();
                        break;
                    default:
                        continue;
                }

                return ref doctype;
            }
        }

        #endregion

        #region Attributes

        private enum AttributeState : Byte
        {
            BeforeName,
            Name,
            AfterName,
            BeforeValue,
            QuotedValue,
            AfterValue,
            UnquotedValue,
            SelfClose
        }

        private ref StructHtmlToken ParseAttributes(ref StructHtmlToken tag)
        {
            var state = AttributeState.BeforeName;
            var quote = Symbols.DoubleQuote;
            var c = Symbols.Null;
            var pos = GetCurrentPosition();
            var attributeAllowed = false;

            while (true)
            {
                switch (state)
                {
                    // See 8.2.4.34 Before attribute name state
                    case AttributeState.BeforeName:
                    {
                        c = SkipSpaces();

                        if (c == Symbols.Solidus)
                        {
                            state = AttributeState.SelfClose;
                        }
                        else if (c == Symbols.GreaterThan)
                        {
                            return ref EmitTag(ref tag);
                        }
                        else if (c.IsUppercaseAscii() && !IsPreservingAttributeNames)
                        {
                            Append(Char.ToLowerInvariant(c));
                            pos = GetCurrentPosition();
                            state = AttributeState.Name;
                        }
                        else if (c == Symbols.Null)
                        {
                            AppendReplacement();
                            pos = GetCurrentPosition();
                            state = AttributeState.Name;
                        }
                        else if (c == Symbols.SingleQuote || c == Symbols.DoubleQuote || c == Symbols.Equality || c == Symbols.LessThan)
                        {
                            RaiseErrorOccurred(HtmlParseError.AttributeNameInvalid);
                            Append(c);
                            pos = GetCurrentPosition();
                            state = AttributeState.Name;
                        }
                        else if (c != Symbols.EndOfFile)
                        {
                            Append(c);
                            pos = GetCurrentPosition();
                            state = AttributeState.Name;
                        }
                        else
                        {
                            return ref NewEof();
                        }

                        break;
                    }

                    // See 8.2.4.35 Attribute name state
                    case AttributeState.Name:
                    {
                        c = GetNext();

                        if (c == Symbols.Equality)
                        {
                            var attributeName = FlushBufferFast(HtmlAttributesLookup.TryGetWellKnownAttributeName);
                            attributeAllowed = _shouldEmitAttribute(ref tag, attributeName.Memory);
                            if (attributeAllowed)
                            {
                                tag.AddAttribute(attributeName, pos);
                            }
                            state = AttributeState.BeforeValue;
                        }
                        else if (c == Symbols.GreaterThan)
                        {
                            var attributeName = FlushBufferFast(HtmlAttributesLookup.TryGetWellKnownAttributeName);
                            attributeAllowed = _shouldEmitAttribute(ref tag, attributeName.Memory);
                            if (attributeAllowed)
                            {
                                tag.AddAttribute(attributeName, pos);
                            }

                            return ref EmitTag(ref tag);
                        }
                        else if (c.IsSpaceCharacter())
                        {
                            var attributeName = FlushBufferFast(HtmlAttributesLookup.TryGetWellKnownAttributeName);
                            attributeAllowed = _shouldEmitAttribute(ref tag, attributeName.Memory);
                            if (attributeAllowed)
                            {
                                tag.AddAttribute(attributeName, pos);
                            }
                            state = AttributeState.AfterName;
                        }
                        else if (c == Symbols.Solidus)
                        {
                            var attributeName = FlushBufferFast(HtmlAttributesLookup.TryGetWellKnownAttributeName);
                            attributeAllowed = _shouldEmitAttribute(ref tag, attributeName.Memory);
                            if (attributeAllowed)
                            {
                                tag.AddAttribute(attributeName, pos);
                            }
                            state = AttributeState.SelfClose;
                        }
                        else if (c.IsUppercaseAscii() && !IsPreservingAttributeNames)
                        {
                            Append(Char.ToLowerInvariant(c));
                        }
                        else if (c == Symbols.DoubleQuote || c == Symbols.SingleQuote || c == Symbols.LessThan)
                        {
                            RaiseErrorOccurred(HtmlParseError.AttributeNameInvalid);
                            Append(c);
                        }
                        else if (c == Symbols.Null)
                        {
                            AppendReplacement();
                        }
                        else if (c != Symbols.EndOfFile)
                        {
                            Append(c);
                        }
                        else
                        {
                            return ref NewEof();
                        }

                        break;
                    }

                    // See 8.2.4.36 After attribute name state
                    case AttributeState.AfterName:
                    {
                        c = SkipSpaces();

                        if (c == Symbols.GreaterThan)
                        {
                            return ref EmitTag(ref tag);
                        }
                        else if (c == Symbols.Equality)
                        {
                            state = AttributeState.BeforeValue;
                        }
                        else if (c == Symbols.Solidus)
                        {
                            state = AttributeState.SelfClose;
                        }
                        else if (c.IsUppercaseAscii() && !IsPreservingAttributeNames)
                        {
                            Append(Char.ToLowerInvariant(c));
                            pos = GetCurrentPosition();
                            state = AttributeState.Name;
                        }
                        else if (c == Symbols.DoubleQuote || c == Symbols.SingleQuote || c == Symbols.LessThan)
                        {
                            RaiseErrorOccurred(HtmlParseError.AttributeNameInvalid);
                            Append(c);
                            pos = GetCurrentPosition();
                            state = AttributeState.Name;
                        }
                        else if (c == Symbols.Null)
                        {
                            AppendReplacement();
                            pos = GetCurrentPosition();
                            state = AttributeState.Name;
                        }
                        else if (c != Symbols.EndOfFile)
                        {
                            Append(c);
                            pos = GetCurrentPosition();
                            state = AttributeState.Name;
                        }
                        else
                        {
                            return ref NewEof();
                        }

                        break;
                    }

                    // See 8.2.4.37 Before attribute value state
                    case AttributeState.BeforeValue:
                    {
                        c = SkipSpaces();

                        if (c == Symbols.DoubleQuote || c == Symbols.SingleQuote)
                        {
                            state = AttributeState.QuotedValue;
                            quote = c;
                        }
                        else if (c == Symbols.Ampersand)
                        {
                            state = AttributeState.UnquotedValue;
                        }
                        else if (c == Symbols.GreaterThan)
                        {
                            RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                            return ref EmitTag(ref tag);
                        }
                        else if (c == Symbols.LessThan || c == Symbols.Equality || c == Symbols.CurvedQuote)
                        {
                            RaiseErrorOccurred(HtmlParseError.AttributeValueInvalid);
                            Append(c);
                            state = AttributeState.UnquotedValue;
                            c = GetNext();
                        }
                        else if (c == Symbols.Null)
                        {
                            AppendReplacement();
                            state = AttributeState.UnquotedValue;
                            c = GetNext();
                        }
                        else if (c != Symbols.EndOfFile)
                        {
                            Append(c);
                            state = AttributeState.UnquotedValue;
                            c = GetNext();
                        }
                        else
                        {
                            return ref NewEof();
                        }

                        break;
                    }

                    // See 8.2.4.38 Attribute value (double-quoted) state
                    // and 8.2.4.39 Attribute value (single-quoted) state
                    case AttributeState.QuotedValue:
                    {
                        c = GetNext();

                        if (c == quote)
                        {
                            if (attributeAllowed)
                            {
                                var value = FlushBufferFast();
                                tag.SetAttributeValue(value);
                            }
                            else
                            {
                                CharBuffer.Discard();
                            }
                            state = AttributeState.AfterValue;
                        }
                        else if (c == Symbols.Ampersand)
                        {
                            AppendCharacterReference(GetNext(), quote, true);
                        }
                        else if (c == Symbols.Null)
                        {
                            AppendReplacement();
                        }
                        else if (c != Symbols.EndOfFile)
                        {
                            Append(c);
                        }
                        else
                        {
                            return ref NewEof();
                        }

                        break;
                    }

                    // See 8.2.4.40 Attribute value (unquoted) state
                    case AttributeState.UnquotedValue:
                    {
                        if (c == Symbols.GreaterThan)
                        {
                            if (attributeAllowed)
                            {
                                var value = FlushBufferFast();
                                tag.SetAttributeValue(value);
                            }
                            else
                            {
                                CharBuffer.Discard();
                            }
                            return ref EmitTag(ref tag);
                        }
                        else if (c.IsSpaceCharacter())
                        {
                            if (attributeAllowed)
                            {
                                var value = FlushBufferFast();
                                tag.SetAttributeValue(value);
                            }
                            else
                            {
                                CharBuffer.Discard();
                            }
                            state = AttributeState.BeforeName;
                        }
                        else if (c == Symbols.Ampersand)
                        {
                            AppendCharacterReference(GetNext(), Symbols.GreaterThan, true);
                            c = GetNext();
                        }
                        else if (c == Symbols.Null)
                        {
                            AppendReplacement();
                            c = GetNext();
                        }
                        else if (c == Symbols.DoubleQuote || c == Symbols.SingleQuote || c == Symbols.LessThan || c == Symbols.Equality || c == Symbols.CurvedQuote)
                        {
                            RaiseErrorOccurred(HtmlParseError.AttributeValueInvalid);
                            Append(c);
                            c = GetNext();
                        }
                        else if (c != Symbols.EndOfFile)
                        {
                            Append(c);
                            c = GetNext();
                        }
                        else
                        {
                            return ref NewEof();
                        }

                        break;
                    }

                    // See 8.2.4.42 After attribute value (quoted) state
                    case AttributeState.AfterValue:
                    {
                        c = GetNext();

                        if (c == Symbols.GreaterThan)
                        {
                            return ref EmitTag(ref tag);
                        }
                        else if (c.IsSpaceCharacter())
                        {
                            state = AttributeState.BeforeName;
                        }
                        else if (c == Symbols.Solidus)
                        {
                            state = AttributeState.SelfClose;
                        }
                        else if (c == Symbols.EndOfFile)
                        {
                            return ref NewEof();
                        }
                        else
                        {
                            RaiseErrorOccurred(HtmlParseError.AttributeNameExpected);
                            Back();
                            state = AttributeState.BeforeName;
                        }

                        break;
                    }

                    case AttributeState.SelfClose:
                    {
                        if (!TagSelfClosingInner(ref tag))
                        {
                            state = AttributeState.BeforeName;
                            break;
                        }

                        return ref tag;
                    }
                }
            }
        }

        #endregion

        #region Script

        private enum ScriptState : Byte
        {
            Normal,
            OpenTag,
            EndTag,
            StartEscape,
            Escaped,
            StartEscapeDash,
            EscapedDash,
            EscapedDashDash,
            EscapedOpenTag,
            EscapedEndTag,
            EscapedNameEndTag,
            StartDoubleEscape,
            EscapedDouble,
            EscapedDoubleDash,
            EscapedDoubleDashDash,
            EscapedDoubleOpenTag,
            EndDoubleEscape
        }

        private ref StructHtmlToken ScriptData(Char c)
        {
            var length = _lastStartTag.Length;
            var scriptLength = TagNames.Script.Length;
            var state = ScriptState.Normal;
            var offset = 0;

            while (true)
            {
                switch (state)
                {
                    // See 8.2.4.6 Script data state
                    case ScriptState.Normal:
                    {
                        switch (c)
                        {
                            case Symbols.Null:
                                AppendReplacement();
                                break;

                            case Symbols.LessThan:
                                Append(Symbols.LessThan);
                                state = ScriptState.OpenTag;
                                continue;

                            case Symbols.EndOfFile:
                                Back();
                                if (SkipScriptText)
                                {
                                    return ref NewSkippedContent();
                                }

                                return ref NewCharacter();

                            default:
                                Append(c);
                                break;
                        }

                        c = GetNext();
                        break;
                    }

                    // See 8.2.4.17 Script data less-than sign state
                    case ScriptState.OpenTag:
                    {
                        c = GetNext();

                        if (c == Symbols.Solidus)
                        {
                            state = ScriptState.EndTag;
                        }
                        else if (c == Symbols.ExclamationMark)
                        {
                            state = ScriptState.StartEscape;
                        }
                        else
                        {
                            state = ScriptState.Normal;
                        }

                        break;
                    }

                    // See 8.2.4.20 Script data escape start state
                    case ScriptState.StartEscape:
                    {
                        Append(Symbols.ExclamationMark);
                        c = GetNext();

                        if (c == Symbols.Minus)
                        {
                            state = ScriptState.StartEscapeDash;
                        }
                        else
                        {
                            state = ScriptState.Normal;
                        }

                        break;
                    }

                    // See 8.2.4.21 Script data escape start dash state
                    case ScriptState.StartEscapeDash:
                    {
                        c = GetNext();
                        Append(Symbols.Minus);

                        if (c == Symbols.Minus)
                        {
                            Append(Symbols.Minus);
                            state = ScriptState.EscapedDashDash;
                        }
                        else
                        {
                            state = ScriptState.Normal;
                        }

                        break;
                    }

                    // See 8.2.4.18 Script data end tag open state
                    case ScriptState.EndTag:
                    {
                        c = GetNext();
                        Append(Symbols.Solidus);
                        offset = CharBuffer.Length;
                        ref var tag = ref NewTagClose();

                        while (c.IsLetter())
                        {
                            // See 8.2.4.19 Script data end tag name state
                            Append(c);
                            c = GetNext();
                            var isspace = c.IsSpaceCharacter();
                            var isclosed = c == Symbols.GreaterThan;
                            var isslash = c == Symbols.Solidus;
                            var hasLength = CharBuffer.Length - offset == length;

                            if (hasLength && (isspace || isclosed || isslash))
                            {
                                if (CharBuffer.HasTextAt(_lastStartTag.Memory.Span, offset, length, StringComparison.OrdinalIgnoreCase))
                                {
                                    if (offset > 2)
                                    {
                                        Back(3 + length);
                                        CharBuffer.Remove(offset - 2, length + 2);
                                        if (SkipScriptText)
                                        {
                                            return ref NewSkippedContent();
                                        }

                                        return ref NewCharacter();
                                    }

                                    CharBuffer.Discard();

                                    if (isspace)
                                    {
                                        tag.Name = _lastStartTag;
                                        return ref ParseAttributes(ref tag);
                                    }
                                    else if (isslash)
                                    {
                                        tag.Name = _lastStartTag;
                                        return ref TagSelfClosing(ref tag);
                                    }
                                    else if (isclosed)
                                    {
                                        tag.Name = _lastStartTag;
                                        return ref EmitTag(ref tag);
                                    }
                                }
                            }
                        }

                        state = ScriptState.Normal;
                        break;
                    }

                    // See 8.2.4.22 Script data escaped state
                    case ScriptState.Escaped:
                    {
                        switch (c)
                        {
                            case Symbols.Minus:
                                Append(Symbols.Minus);
                                c = GetNext();
                                state = ScriptState.EscapedDash;
                                continue;
                            case Symbols.LessThan:
                                c = GetNext();
                                state = ScriptState.EscapedOpenTag;
                                continue;
                            case Symbols.Null:
                                AppendReplacement();
                                break;
                            case Symbols.EndOfFile:
                                Back();
                                if (SkipScriptText)
                                {
                                    return ref NewSkippedContent();
                                }
                                return ref NewCharacter();
                            default:
                                state = ScriptState.Normal;
                                continue;
                        }

                        c = GetNext();
                        break;
                    }

                    // See 8.2.4.23 Script data escaped dash state
                    case ScriptState.EscapedDash:
                    {
                        switch (c)
                        {
                            case Symbols.Minus:
                                Append(Symbols.Minus);
                                state = ScriptState.EscapedDashDash;
                                continue;
                            case Symbols.LessThan:
                                c = GetNext();
                                state = ScriptState.EscapedOpenTag;
                                continue;
                            case Symbols.Null:
                                AppendReplacement();
                                break;
                            case Symbols.EndOfFile:
                                Back();
                                if (SkipScriptText)
                                {
                                    return ref NewSkippedContent();
                                }

                                return ref NewCharacter();
                            default:
                                Append(c);
                                break;
                        }

                        c = GetNext();
                        state = ScriptState.Escaped;
                        break;
                    }

                    // See 8.2.4.24 Script data escaped dash dash state
                    case ScriptState.EscapedDashDash:
                    {
                        c = GetNext();

                        switch (c)
                        {
                            case Symbols.Minus:
                                Append(Symbols.Minus);
                                break;
                            case Symbols.LessThan:
                                c = GetNext();
                                state = ScriptState.EscapedOpenTag;
                                continue;
                            case Symbols.GreaterThan:
                                Append(Symbols.GreaterThan);
                                c = GetNext();
                                state = ScriptState.Normal;
                                continue;
                            case Symbols.Null:
                                AppendReplacement();
                                c = GetNext();
                                state = ScriptState.Escaped;
                                continue;
                            case Symbols.EndOfFile:
                                if (SkipScriptText)
                                {
                                    return ref NewSkippedContent();
                                }

                                return ref NewCharacter();
                            default:
                                Append(c);
                                c = GetNext();
                                state = ScriptState.Escaped;
                                continue;
                        }

                        break;
                    }

                    // See 8.2.4.25 Script data escaped less-than sign state
                    case ScriptState.EscapedOpenTag:
                    {
                        if (c == Symbols.Solidus)
                        {
                            c = GetNext();
                            state = ScriptState.EscapedEndTag;
                        }
                        else if (c.IsLetter())
                        {
                            Append(Symbols.LessThan);
                            offset = CharBuffer.Length;
                            Append(c);
                            state = ScriptState.StartDoubleEscape;
                        }
                        else
                        {
                            Append(Symbols.LessThan);
                            state = ScriptState.Escaped;
                        }

                        break;
                    }

                    // See 8.2.4.26 Script data escaped end tag open state
                    case ScriptState.EscapedEndTag:
                    {
                        Append(Symbols.LessThan, Symbols.Solidus);
                        offset = CharBuffer.Length;

                        if (c.IsLetter())
                        {
                            Append(c);
                            state = ScriptState.EscapedNameEndTag;
                        }
                        else
                        {
                            state = ScriptState.Escaped;
                        }

                        break;
                    }

                    // See 8.2.4.27 Script data escaped end tag name state
                    case ScriptState.EscapedNameEndTag:
                    {
                        c = GetNext();
                        var hasLength = CharBuffer.Length - offset == scriptLength;
                        if (hasLength && (c == Symbols.Solidus || c == Symbols.GreaterThan || c.IsSpaceCharacter()))
                        {
                            if (CharBuffer.Isi(offset, scriptLength, TagNames.Script.AsSpan()))
                            {
                                Back(scriptLength + 3);
                                CharBuffer.Remove(offset - 2, scriptLength + 2);
                                if (SkipScriptText)
                                {
                                    return ref NewSkippedContent();
                                }

                                return ref NewCharacter();
                            }
                        }
                        else if (!c.IsLetter())
                        {
                            state = ScriptState.Escaped;
                        }
                        else
                        {
                            Append(c);
                        }

                        break;
                    }

                    // See 8.2.4.28 Script data double escape start state
                    case ScriptState.StartDoubleEscape:
                    {
                        c = GetNext();
                        var hasLength = CharBuffer.Length - offset == scriptLength;

                        if (hasLength && (c == Symbols.Solidus || c == Symbols.GreaterThan || c.IsSpaceCharacter()))
                        {
                            var isscript = CharBuffer.Isi(offset, scriptLength, TagNames.Script.AsSpan());
                            Append(c);
                            c = GetNext();
                            state = isscript ? ScriptState.EscapedDouble : ScriptState.Escaped;
                        }
                        else if (c.IsLetter())
                        {
                            Append(c);
                        }
                        else
                        {
                            state = ScriptState.Escaped;
                        }

                        break;
                    }

                    // See 8.2.4.29 Script data double escaped state
                    case ScriptState.EscapedDouble:
                    {
                        switch (c)
                        {
                            case Symbols.Minus:
                                Append(Symbols.Minus);
                                c = GetNext();
                                state = ScriptState.EscapedDoubleDash;
                                continue;

                            case Symbols.LessThan:
                                Append(Symbols.LessThan);
                                c = GetNext();
                                state = ScriptState.EscapedDoubleOpenTag;
                                continue;

                            case Symbols.Null:
                                AppendReplacement();
                                c = GetNext();
                                continue;

                            case Symbols.EndOfFile:
                                RaiseErrorOccurred(HtmlParseError.EOF);
                                Back();

                                if (SkipScriptText)
                                {
                                    return ref NewSkippedContent();
                                }

                                return ref NewCharacter();
                        }

                        Append(c);
                        c = GetNext();
                        break;
                    }

                    // See 8.2.4.30 Script data double escaped dash state
                    case ScriptState.EscapedDoubleDash:
                    {
                        switch (c)
                        {
                            case Symbols.Minus:
                                Append(Symbols.Minus);
                                state = ScriptState.EscapedDoubleDashDash;
                                continue;

                            case Symbols.LessThan:
                                Append(Symbols.LessThan);
                                c = GetNext();
                                state = ScriptState.EscapedDoubleOpenTag;
                                continue;

                            case Symbols.Null:
                                RaiseErrorOccurred(HtmlParseError.Null);
                                c = Symbols.Replacement;
                                break;

                            case Symbols.EndOfFile:
                                RaiseErrorOccurred(HtmlParseError.EOF);
                                Back();
                                if (SkipScriptText)
                                {
                                    return ref NewSkippedContent();
                                }

                                return ref NewCharacter();
                        }

                        state = ScriptState.EscapedDouble;
                        break;
                    }

                    // See 8.2.4.31 Script data double escaped dash dash state
                    case ScriptState.EscapedDoubleDashDash:
                    {
                        c = GetNext();

                        switch (c)
                        {
                            case Symbols.Minus:
                                Append(Symbols.Minus);
                                break;

                            case Symbols.LessThan:
                                Append(Symbols.LessThan);
                                c = GetNext();
                                state = ScriptState.EscapedDoubleOpenTag;
                                continue;

                            case Symbols.GreaterThan:
                                Append(Symbols.GreaterThan);
                                c = GetNext();
                                state = ScriptState.Normal;
                                continue;

                            case Symbols.Null:
                                AppendReplacement();
                                c = GetNext();
                                state = ScriptState.EscapedDouble;
                                continue;

                            case Symbols.EndOfFile:
                                RaiseErrorOccurred(HtmlParseError.EOF);
                                Back();
                                if (SkipScriptText)
                                {
                                    return ref NewSkippedContent();
                                }

                                return ref NewCharacter();

                            default:
                                Append(c);
                                c = GetNext();
                                state = ScriptState.EscapedDouble;
                                continue;
                        }

                        break;
                    }

                    // See 8.2.4.32 Script data double escaped less-than sign state
                    case ScriptState.EscapedDoubleOpenTag:
                    {
                        if (c == Symbols.Solidus)
                        {
                            Append(Symbols.Solidus);
                            offset = CharBuffer.Length;
                            state = ScriptState.EndDoubleEscape;
                        }
                        else
                        {
                            state = ScriptState.EscapedDouble;
                        }

                        break;
                    }

                    // See 8.2.4.33 Script data double escape end state
                    case ScriptState.EndDoubleEscape:
                    {
                        c = GetNext();
                        var hasLength = CharBuffer.Length - offset == scriptLength;

                        if (hasLength && (c.IsSpaceCharacter() || c == Symbols.Solidus || c == Symbols.GreaterThan))
                        {
                            var isscript = CharBuffer.Isi(offset, scriptLength, TagNames.Script.AsSpan());
                            Append(c);
                            c = GetNext();
                            state = isscript ? ScriptState.Escaped : ScriptState.EscapedDouble;
                        }
                        else if (c.IsLetter())
                        {
                            Append(c);
                        }
                        else
                        {
                            state = ScriptState.EscapedDouble;
                        }

                        break;
                    }
                }
            }
        }

        #endregion

        #region Tokens
		
		private ref StructHtmlToken GetNextStructToken()
        {
            var current = GetNext();
            _position = GetCurrentPosition();

            if (current != Symbols.EndOfFile)
            {
                switch (State)
                {
                    case HtmlParseMode.PCData:
                        return ref Data(current);
                    case HtmlParseMode.RCData:
                        return ref RCData(current);
                    case HtmlParseMode.Plaintext:
                        return ref Plaintext(current);
                    case HtmlParseMode.Rawtext:
                        return ref Rawtext(current);
                    case HtmlParseMode.Script:
                        return ref ScriptData(current);
                }
            }
            return ref NewEof(acceptable: true);
        }

        private ref StructHtmlToken NewSkippedContent(HtmlTokenType htmlTokenType = HtmlTokenType.Character)
        {
            CharBuffer.Discard();
            _token = StructHtmlToken.Skipped(htmlTokenType, _position);
            return ref _token;
        }

        private ref StructHtmlToken NewCharacter()
        {
            var content = FlushBufferFast();
            _token = StructHtmlToken.Character(content, _position);
            return ref _token;
        }

        private ref StructHtmlToken NewProcessingInstruction()
        {
            if (SkipProcessingInstructions)
            {
                return ref NewSkippedContent(HtmlTokenType.Comment);
            }

            var content = FlushBufferFast();
            _token = StructHtmlToken.ProcessingInstruction(content, _position);
            return ref _token;
        }

        private ref StructHtmlToken NewComment()
        {
            if (SkipComments)
            {
                return ref NewSkippedContent(HtmlTokenType.Comment);
            }

            var content = FlushBufferFast();
            _token = StructHtmlToken.Comment(content, _position);
            return ref _token;
        }

        private ref StructHtmlToken NewEof(Boolean acceptable = false)
        {
            if (!acceptable)
            {
                RaiseErrorOccurred(HtmlParseError.EOF);
            }

            _token = StructHtmlToken.EndOfFile(_position);
            return ref _token;
        }

        private ref StructHtmlToken NewDoctype(Boolean quirksForced)
        {
            _token = StructHtmlToken.Doctype(quirksForced, _position);
            return ref _token;
        }

        private ref StructHtmlToken NewTagOpen()
        {
            _token = StructHtmlToken.TagOpen(_position);
            return ref _token;
        }

        private ref StructHtmlToken NewTagClose()
        {
            _token = StructHtmlToken.TagClose(_position);
            return ref _token;
        }

        #endregion

        #region Helpers

        private void RaiseErrorOccurred(HtmlParseError code)
        {
            RaiseErrorOccurred(code, GetCurrentPosition());
        }

        private void AppendReplacement()
        {
            RaiseErrorOccurred(HtmlParseError.Null);
            Append(Symbols.Replacement);
        }

        private Boolean CreateIfAppropriate(Char c, ref StructHtmlToken token)
        {
            var isspace = c.IsSpaceCharacter();
            var isclosed = c == Symbols.GreaterThan;
            var isslash = c == Symbols.Solidus;
            var hasLength = CharBuffer.Length == _lastStartTag.Length;

            if (hasLength && (isspace || isclosed || isslash) && CharBuffer.Is(_lastStartTag))
            {
                ref var tag = ref NewTagClose();
                CharBuffer.Discard();

                if (isspace)
                {
                    tag.Name = _lastStartTag;
                    token = ParseAttributes(ref tag);
                    return true;
                }
                else if (isslash)
                {
                    tag.Name = _lastStartTag;
                    token = TagSelfClosing(ref tag);
                    return true;
                }
                else if (isclosed)
                {
                    tag.Name = _lastStartTag;
                    token = EmitTag(ref tag);
                    return true;
                }
            }

            return false;
        }

        private ref StructHtmlToken EmitTag(ref StructHtmlToken tag)
        {
            var attributes = tag.Attributes;
            State = HtmlParseMode.PCData;

            switch (tag.Type)
            {
                case HtmlTokenType.StartTag:
                    for (var i = attributes.Count - 1; i > 0; i--)
                    {
                        for (var j = i - 1; j >= 0; j--)
                        {
                            if (attributes[j].Name.Is(attributes[i].Name))
                            {
                                tag.RemoveAttributeAt(i);
                                RaiseErrorOccurred(HtmlParseError.AttributeDuplicateOmitted, tag.Position);
                                break;
                            }
                        }
                    }

                    _lastStartTag = tag.Name;
                    break;
                case HtmlTokenType.EndTag:
                    if (tag.IsSelfClosing)
                    {
                        RaiseErrorOccurred(HtmlParseError.EndTagCannotBeSelfClosed, tag.Position);
                    }

                    if (attributes.Count != 0)
                    {
                        RaiseErrorOccurred(HtmlParseError.EndTagCannotHaveAttributes, tag.Position);
                    }

                    break;
            }

            return ref tag;
        }

        #endregion

        #region Helpers

        private readonly struct EntityProvider
        {
            private readonly IEntityProviderExtended? _fast;
            private readonly IEntityProvider? _slow;

            public EntityProvider(IEntityProvider slow)
            {
                _slow = slow;
            }

            public EntityProvider(IEntityProviderExtended fast)
            {
                _fast = fast;
            }

            public String? GetSymbol(StringOrMemory name)
            {
                if (_fast != null)
                {
                    return _fast.GetSymbol(name);
                }

                if (_slow != null)
                {
                    return _slow.GetSymbol(name.ToString());
                }

                throw new InvalidOperationException("Should not get there, please file a bug report.");
            }
        }

        #endregion
    }
}