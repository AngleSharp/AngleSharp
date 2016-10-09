namespace AngleSharp.Parser.Html
{
    using AngleSharp.Dom.Events;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Services;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Performs the tokenization of the source code. Follows the tokenization algorithm at:
    /// http://www.w3.org/html/wg/drafts/html/master/syntax.html
    /// </summary>
    sealed class HtmlTokenizer : BaseTokenizer
    {
        #region Fields

        private readonly IEntityProvider _resolver;
        private String _lastStartTag;
        private TextPosition _position;

        #endregion

        #region Events

        /// <summary>
        /// Fired in case of a parse error.
        /// </summary>
        public event EventHandler<HtmlErrorEvent> Error;

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
            IsAcceptingCharacterData = false;
            IsStrictMode = false;
            _lastStartTag = String.Empty;
            _resolver = resolver;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if CDATA sections are accepted.
        /// </summary>
        public Boolean IsAcceptingCharacterData
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

        #endregion

        #region Methods

        /// <summary>
        /// Gets the next available token.
        /// </summary>
        /// <returns>The next available token.</returns>
        public HtmlToken Get()
        {
            var current = GetNext();
            _position = GetCurrentPosition();

            if (current != Symbols.EndOfFile)
            {
                switch (State)
                {
                    case HtmlParseMode.PCData:
                        return Data(current);
                    case HtmlParseMode.RCData:
                        return RCData(current);
                    case HtmlParseMode.Plaintext:
                        return Plaintext(current);
                    case HtmlParseMode.Rawtext:
                        return Rawtext(current);
                    case HtmlParseMode.Script:
                        return ScriptData(current);
                }
            }

            return NewEof(acceptable: true);
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
        private HtmlToken Data(Char c)
        {
            return c == Symbols.LessThan ? TagOpen(GetNext()) : DataText(c);
        }

        private HtmlToken DataText(Char c)
        {
            while (true)
            {
                switch (c)
                {
                    case Symbols.LessThan:
                    case Symbols.EndOfFile:
                        Back();
                        return NewCharacter();

                    case Symbols.Ampersand:
                        AppendCharacterReference(GetNext());
                        break;

                    case Symbols.Null:
                        RaiseErrorOccurred(HtmlParseError.Null);
                        break;

                    default:
                        StringBuffer.Append(c);
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
        private HtmlToken Plaintext(Char c)
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
                        return NewCharacter();

                    default:
                        StringBuffer.Append(c);
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
        private HtmlToken RCData(Char c)
        {
            return c == Symbols.LessThan ? RCDataLt(GetNext()) : RCDataText(c);
        }

        private HtmlToken RCDataText(Char c)
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
                        return NewCharacter();

                    case Symbols.Null:
                        AppendReplacement();
                        break;

                    default:
                        StringBuffer.Append(c);
                        break;
                }

                c = GetNext();
            }
        }

        /// <summary>
        /// See 8.2.4.11 RCDATA less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private HtmlToken RCDataLt(Char c)
        {
            if (c == Symbols.Solidus)
            {
                // See 8.2.4.12 RCDATA end tag open state
                c = GetNext();

                if (c.IsUppercaseAscii())
                {
                    StringBuffer.Append(Char.ToLowerInvariant(c));
                    return RCDataNameEndTag(GetNext());
                }
                else if (c.IsLowercaseAscii())
                {
                    StringBuffer.Append(c);
                    return RCDataNameEndTag(GetNext());
                }
                else
                {
                    StringBuffer.Append(Symbols.LessThan).Append(Symbols.Solidus);
                    return RCDataText(c);
                }
            }
            else
            {
                StringBuffer.Append(Symbols.LessThan);
                return RCDataText(c);
            }
        }

        /// <summary>
        /// See 8.2.4.13 RCDATA end tag name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private HtmlToken RCDataNameEndTag(Char c)
        {
            while (true)
            {
                var token = CreateIfAppropriate(c);

                if (token != null)
                {
                    return token;
                }
                else if (c.IsUppercaseAscii())
                {
                    StringBuffer.Append(Char.ToLowerInvariant(c));
                }
                else if (c.IsLowercaseAscii())
                {
                    StringBuffer.Append(c);
                }
                else
                {
                    StringBuffer.Insert(0, Symbols.LessThan).Insert(1, Symbols.Solidus);
                    return RCDataText(c);
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
        private HtmlToken Rawtext(Char c)
        {
            return c == Symbols.LessThan ? RawtextLT(GetNext()) : RawtextText(c);
        }

        private HtmlToken RawtextText(Char c)
        {
            while (true)
            {
                switch (c)
                {
                    case Symbols.LessThan:
                    case Symbols.EndOfFile:
                        Back();
                        return NewCharacter();

                    case Symbols.Null:
                        AppendReplacement();
                        break;

                    default:
                        StringBuffer.Append(c);
                        break;
                }

                c = GetNext();
            }
        }

        /// <summary>
        /// See 8.2.4.14 RAWTEXT less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private HtmlToken RawtextLT(Char c)
        {
            if (c == Symbols.Solidus)
            {
                // See 8.2.4.15 RAWTEXT end tag open state
                c = GetNext();

                if (c.IsUppercaseAscii())
                {
                    StringBuffer.Append(Char.ToLowerInvariant(c));
                    return RawtextNameEndTag(GetNext());
                }
                else if (c.IsLowercaseAscii())
                {
                    StringBuffer.Append(c);
                    return RawtextNameEndTag(GetNext());
                }
                else
                {
                    StringBuffer.Append(Symbols.LessThan).Append(Symbols.Solidus);
                    return RawtextText(c);
                }
            }
            else
            {
                StringBuffer.Append(Symbols.LessThan);
                return RawtextText(c);
            }
        }

        /// <summary>
        /// See 8.2.4.16 RAWTEXT end tag name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private HtmlToken RawtextNameEndTag(Char c)
        {
            while (true)
            {
                var token = CreateIfAppropriate(c);

                if (token != null)
                {
                    return token;
                }
                else if (c.IsUppercaseAscii())
                {
                    StringBuffer.Append(Char.ToLowerInvariant(c));
                }
                else if (c.IsLowercaseAscii())
                {
                    StringBuffer.Append(c);
                }
                else
                {
                    StringBuffer.Insert(0, Symbols.LessThan).Insert(1, Symbols.Solidus);
                    return RawtextText(c);
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
        private HtmlToken CharacterData(Char c)
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
                    StringBuffer.Append(c);
                    c = GetNext();
                }
            }

            return NewCharacter();
        }

        /// <summary>
        /// See 8.2.4.69 Tokenizing character references
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="allowedCharacter">The additionally allowed character if there is one.</param>
        private void AppendCharacterReference(Char c, Char allowedCharacter = Symbols.Null)
        {
            if (c.IsSpaceCharacter() || c == Symbols.LessThan || c == Symbols.EndOfFile || c == Symbols.Ampersand || c == allowedCharacter)
            {
                Back();
                StringBuffer.Append(Symbols.Ampersand);
            }
            else
            {
                var entity = default(String);

                if (c == Symbols.Num)
                {
                    entity = GetNumericCharacterReference(GetNext());
                }
                else
                {
                    entity = GetLookupCharacterReference(c, allowedCharacter);
                }

                if (entity == null)
                {
                    StringBuffer.Append(Symbols.Ampersand);
                }
                else
                {
                    StringBuffer.Append(entity);
                }
            }
        }

        private String GetNumericCharacterReference(Char c)
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

                RaiseErrorOccurred(HtmlParseError.CharacterReferenceWrongNumber);
                return null;
            }

            if (c != Symbols.Semicolon)
            {
                RaiseErrorOccurred(HtmlParseError.CharacterReferenceSemicolonMissing);
                Back();
            }

            if (HtmlEntityService.IsInCharacterTable(num))
            {
                RaiseErrorOccurred(HtmlParseError.CharacterReferenceInvalidCode);
                return HtmlEntityService.GetSymbolFromTable(num);
            }
            else if (HtmlEntityService.IsInvalidNumber(num))
            {
                RaiseErrorOccurred(HtmlParseError.CharacterReferenceInvalidNumber);
                return Symbols.Replacement.ToString();
            }
            else if (HtmlEntityService.IsInInvalidRange(num))
            {
                RaiseErrorOccurred(HtmlParseError.CharacterReferenceInvalidRange);
            }

            return num.ConvertFromUtf32();
        }

        private String GetLookupCharacterReference(Char c, Char allowedCharacter)
        {
            var entity = default(String);
            var start = InsertionPoint - 1;
            var reference = new Char[32];
            var index = 0;
            var chr = Current;

            do
            {
                if (chr == Symbols.Semicolon || !chr.IsName())
                {
                    break;
                }

                reference[index++] = chr;
                chr = GetNext();
            }
            while (chr != Symbols.EndOfFile && index < 31);

            if (chr == Symbols.Semicolon)
            {
                reference[index] = Symbols.Semicolon;
                var value = new String(reference, 0, index + 1);
                entity = _resolver.GetSymbol(value);
            }

            while (entity == null && index > 0)
            {
                var value = new String(reference, 0, index--);
                entity = _resolver.GetSymbol(value);

                if (entity == null)
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
                RaiseErrorOccurred(HtmlParseError.CharacterReferenceNotTerminated);
            }

            return entity;
        }

        #endregion

        #region Tags

        /// <summary>
        /// See 8.2.4.8 Tag open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private HtmlToken TagOpen(Char c)
        {
            if (c == Symbols.Solidus)
            {
                return TagEnd(GetNext());
            }
            else if (c.IsLowercaseAscii())
            {
                StringBuffer.Append(c);
                return TagName(NewTagOpen());
            }
            else if (c.IsUppercaseAscii())
            {
                StringBuffer.Append(Char.ToLowerInvariant(c));
                return TagName(NewTagOpen());
            }
            else if (c == Symbols.ExclamationMark)
            {
                return MarkupDeclaration(GetNext());
            }
            else if (c != Symbols.QuestionMark)
            {
                State = HtmlParseMode.PCData;
                RaiseErrorOccurred(HtmlParseError.AmbiguousOpenTag);
                StringBuffer.Append(Symbols.LessThan);
                return DataText(c);
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.BogusComment);
                return BogusComment(c);
            }
        }

        /// <summary>
        /// See 8.2.4.9 End tag open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private HtmlToken TagEnd(Char c)
        {
            if (c.IsLowercaseAscii())
            {
                StringBuffer.Append(c);
                return TagName(NewTagClose());
            }
            else if (c.IsUppercaseAscii())
            {
                StringBuffer.Append(Char.ToLowerInvariant(c));
                return TagName(NewTagClose());
            }
            else if (c == Symbols.GreaterThan)
            {
                State = HtmlParseMode.PCData;
                RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                return Data(GetNext());
            }
            else if (c == Symbols.EndOfFile)
            {
                Back();
                RaiseErrorOccurred(HtmlParseError.EOF);
                StringBuffer.Append(Symbols.LessThan).Append(Symbols.Solidus);
                return NewCharacter();
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.BogusComment);
                return BogusComment(c);
            }
        }

        /// <summary>
        /// See 8.2.4.10 Tag name state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        private HtmlToken TagName(HtmlTagToken tag)
        {
            while (true)
            {
                var c = GetNext();

                if (c == Symbols.GreaterThan)
                {
                    tag.Name = FlushBuffer();
                    return EmitTag(tag);
                }
                else if (c.IsSpaceCharacter())
                {
                    tag.Name = FlushBuffer();
                    return ParseAttributes(tag);
                }
                else if (c == Symbols.Solidus)
                {
                    tag.Name = FlushBuffer();
                    return TagSelfClosing(tag);
                }
                else if (c.IsUppercaseAscii())
                {
                    StringBuffer.Append(Char.ToLowerInvariant(c));
                }
                else if (c == Symbols.Null)
                {
                    AppendReplacement();
                }
                else if (c != Symbols.EndOfFile)
                {
                    StringBuffer.Append(c);
                }
                else
                {
                    return NewEof();
                }
            }
        }

        /// <summary>
        /// See 8.2.4.43 Self-closing start tag state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        private HtmlToken TagSelfClosing(HtmlTagToken tag)
        {
            switch (GetNext())
            {
                case Symbols.GreaterThan:
                    tag.IsSelfClosing = true;
                    return EmitTag(tag);
                case Symbols.EndOfFile:
                    return NewEof();
                default:
                    RaiseErrorOccurred(HtmlParseError.ClosingSlashMisplaced);
                    Back();
                    return ParseAttributes(tag);
            }
        }

        /// <summary>
        /// See 8.2.4.45 Markup declaration open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private HtmlToken MarkupDeclaration(Char c)
        {
            if (ContinuesWithSensitive("--"))
            {
                Advance();
                return CommentStart(GetNext());
            }
            else if (ContinuesWithInsensitive(TagNames.Doctype))
            {
                Advance(6);
                return Doctype(GetNext());
            }
            else if (IsAcceptingCharacterData && ContinuesWithSensitive(Keywords.CData))
            {
                Advance(6);
                return CharacterData(GetNext());
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.UndefinedMarkupDeclaration);
                return BogusComment(c);
            }
        }

        #endregion

        #region Comments

        /// <summary>
        /// See 8.2.4.44 Bogus comment state
        /// </summary>
        /// <param name="c">The current character.</param>
        private HtmlToken BogusComment(Char c)
        {
            StringBuffer.Clear();

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
                        StringBuffer.Append(c);
                        c = GetNext();
                        continue;
                }

                State = HtmlParseMode.PCData;
                return NewComment();
            }
        }

        /// <summary>
        /// See 8.2.4.46 Comment start state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private HtmlToken CommentStart(Char c)
        {
            StringBuffer.Clear();

            switch (c)
            {
                case Symbols.Minus:
                    return CommentDashStart(GetNext()) ?? Comment(GetNext());
                case Symbols.Null:
                    AppendReplacement();
                    return Comment(GetNext());
                case Symbols.GreaterThan:
                    State = HtmlParseMode.PCData;
                    RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                    break;
                case Symbols.EndOfFile:
                    RaiseErrorOccurred(HtmlParseError.EOF);
                    Back();
                    break;
                default:
                    StringBuffer.Append(c);
                    return Comment(GetNext());
            }

            return NewComment();
        }

        /// <summary>
        /// See 8.2.4.47 Comment start dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private HtmlToken CommentDashStart(Char c)
        {
            switch (c)
            {
                case Symbols.Minus:
                    return CommentEnd(GetNext());
                case Symbols.Null:
                    RaiseErrorOccurred(HtmlParseError.Null);
                    StringBuffer.Append(Symbols.Minus).Append(Symbols.Replacement);
                    return Comment(GetNext());
                case Symbols.GreaterThan:
                    State = HtmlParseMode.PCData;
                    RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                    break;
                case Symbols.EndOfFile:
                    RaiseErrorOccurred(HtmlParseError.EOF);
                    Back();
                    break;
                default:
                    StringBuffer.Append(Symbols.Minus).Append(c);
                    return Comment(GetNext());
            }

            return NewComment();
        }

        /// <summary>
        /// See 8.2.4.48 Comment state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private HtmlToken Comment(Char c)
        {
            while (true)
            {
                switch (c)
                {
                    case Symbols.Minus:
                        var result = CommentDashEnd(GetNext());

                        if (result != null)
                        {
                            return result;
                        }

                        break;
                    case Symbols.EndOfFile:
                        RaiseErrorOccurred(HtmlParseError.EOF);
                        Back();
                        return NewComment();
                    case Symbols.Null:
                        AppendReplacement();
                        break;
                    default:
                        StringBuffer.Append(c);
                        break;
                }

                c = GetNext();
            }
        }

        /// <summary>
        /// See 8.2.4.49 Comment end dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private HtmlToken CommentDashEnd(Char c)
        {
            switch (c)
            {
                case Symbols.Minus:
                    return CommentEnd(GetNext());
                case Symbols.EndOfFile:
                    RaiseErrorOccurred(HtmlParseError.EOF);
                    Back();
                    return NewComment();
                case Symbols.Null:
                    RaiseErrorOccurred(HtmlParseError.Null);
                    c = Symbols.Replacement;
                    break;
            }

            StringBuffer.Append(Symbols.Minus).Append(c);
            return null;
        }

        /// <summary>
        /// See 8.2.4.50 Comment end state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private HtmlToken CommentEnd(Char c)
        {
            while (true)
            {
                switch (c)
                {
                    case Symbols.GreaterThan:
                        State = HtmlParseMode.PCData;
                        return NewComment();
                    case Symbols.Null:
                        RaiseErrorOccurred(HtmlParseError.Null);
                        StringBuffer.Append(Symbols.Minus).Append(Symbols.Replacement);
                        return null;
                    case Symbols.ExclamationMark:
                        RaiseErrorOccurred(HtmlParseError.CommentEndedWithEM);
                        return CommentBangEnd(GetNext());
                    case Symbols.Minus:
                        RaiseErrorOccurred(HtmlParseError.CommentEndedWithDash);
                        StringBuffer.Append(Symbols.Minus);
                        break;
                    case Symbols.EndOfFile:
                        RaiseErrorOccurred(HtmlParseError.EOF);
                        Back();
                        return NewComment();
                    default:
                        RaiseErrorOccurred(HtmlParseError.CommentEndedUnexpected);
                        StringBuffer.Append(Symbols.Minus).Append(Symbols.Minus).Append(c);
                        return null;
                }

                c = GetNext();
            }
        }

        /// <summary>
        /// See 8.2.4.51 Comment end bang state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private HtmlToken CommentBangEnd(Char c)
        {
            switch (c)
            {
                case Symbols.Minus:
                    StringBuffer.Append(Symbols.Minus).Append(Symbols.Minus).Append(Symbols.ExclamationMark);
                    return CommentDashEnd(GetNext());
                case Symbols.GreaterThan:
                    State = HtmlParseMode.PCData;
                    break;
                case Symbols.Null:
                    RaiseErrorOccurred(HtmlParseError.Null);
                    StringBuffer.Append(Symbols.Minus).Append(Symbols.Minus).Append(Symbols.ExclamationMark).Append(Symbols.Replacement);
                    return null;
                case Symbols.EndOfFile:
                    RaiseErrorOccurred(HtmlParseError.EOF);
                    Back();
                    break;
                default:
                    StringBuffer.Append(Symbols.Minus).Append(Symbols.Minus).Append(Symbols.ExclamationMark).Append(c);
                    return null;
            }

            return NewComment();
        }

        #endregion

        #region Doctype

        /// <summary>
        /// See 8.2.4.52 DOCTYPE state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private HtmlToken Doctype(Char c)
        {
            if (c.IsSpaceCharacter())
            {
                return DoctypeNameBefore(GetNext());
            }
            else if (c == Symbols.EndOfFile)
            {
                RaiseErrorOccurred(HtmlParseError.EOF);
                Back();
                return NewDoctype(true);
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.DoctypeUnexpected);
                return DoctypeNameBefore(c);
            }
        }

        /// <summary>
        /// See 8.2.4.53 Before DOCTYPE name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        private HtmlToken DoctypeNameBefore(Char c)
        {
            while (c.IsSpaceCharacter())
                c = GetNext();

            if (c.IsUppercaseAscii())
            {
                var doctype = NewDoctype(false);
                StringBuffer.Append(Char.ToLowerInvariant(c));
                return DoctypeName(doctype);
            }
            else if (c == Symbols.Null)
            {
                var doctype = NewDoctype(false);
                AppendReplacement();
                return DoctypeName(doctype);
            }
            else if (c == Symbols.GreaterThan)
            {
                var doctype = NewDoctype(true);
                State = HtmlParseMode.PCData;
                RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                return doctype;
            }
            else if (c == Symbols.EndOfFile)
            {
                var doctype = NewDoctype(true);
                RaiseErrorOccurred(HtmlParseError.EOF);
                Back();
                return doctype;
            }
            else
            {
                var doctype = NewDoctype(false);
                StringBuffer.Append(c);
                return DoctypeName(doctype);
            }
        }

        /// <summary>
        /// See 8.2.4.54 DOCTYPE name state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private HtmlToken DoctypeName(HtmlDoctypeToken doctype)
        {
            while (true)
            {
                var c = GetNext();

                if (c.IsSpaceCharacter())
                {
                    doctype.Name = FlushBuffer();
                    return DoctypeNameAfter(doctype);
                }
                else if (c == Symbols.GreaterThan)
                {
                    State = HtmlParseMode.PCData;
                    doctype.Name = FlushBuffer();
                    break;
                }
                else if (c.IsUppercaseAscii())
                {
                    StringBuffer.Append(Char.ToLowerInvariant(c));
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
                    doctype.Name = FlushBuffer();
                    break;
                }
                else
                {
                    StringBuffer.Append(c);
                }
            }

            return doctype;
        }

        /// <summary>
        /// See 8.2.4.55 After DOCTYPE name state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private HtmlToken DoctypeNameAfter(HtmlDoctypeToken doctype)
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
                return DoctypePublic(doctype);
            }
            else if (ContinuesWithInsensitive(Keywords.System))
            {
                Advance(5);
                return DoctypeSystem(doctype);
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.DoctypeUnexpectedAfterName);
                doctype.IsQuirksForced = true;
                return BogusDoctype(doctype);
            }

            return doctype;
        }

        /// <summary>
        /// See 8.2.4.56 After DOCTYPE public keyword state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private HtmlToken DoctypePublic(HtmlDoctypeToken doctype)
        {
            var c = GetNext();

            if (c.IsSpaceCharacter())
            {
                return DoctypePublicIdentifierBefore(doctype);
            }
            else if (c == Symbols.DoubleQuote)
            {
                RaiseErrorOccurred(HtmlParseError.DoubleQuotationMarkUnexpected);
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierDoubleQuoted(doctype);
            }
            else if (c == Symbols.SingleQuote)
            {
                RaiseErrorOccurred(HtmlParseError.SingleQuotationMarkUnexpected);
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierSingleQuoted(doctype);
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
                return BogusDoctype(doctype);
            }

            return doctype;
        }

        /// <summary>
        /// See 8.2.4.57 Before DOCTYPE public identifier state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private HtmlToken DoctypePublicIdentifierBefore(HtmlDoctypeToken doctype)
        {
            var c = SkipSpaces();

            if (c == Symbols.DoubleQuote)
            {
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierDoubleQuoted(doctype);
            }
            else if (c == Symbols.SingleQuote)
            {
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierSingleQuoted(doctype);
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
                return BogusDoctype(doctype);
            }

            return doctype;
        }

        /// <summary>
        /// See 8.2.4.58 DOCTYPE public identifier (double-quoted) state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private HtmlToken DoctypePublicIdentifierDoubleQuoted(HtmlDoctypeToken doctype)
        {
            while (true)
            {
                var c = GetNext();

                if (c == Symbols.DoubleQuote)
                {
                    doctype.PublicIdentifier = FlushBuffer();
                    return DoctypePublicIdentifierAfter(doctype);
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
                    doctype.PublicIdentifier = FlushBuffer();
                    break;
                }
                else if (c == Symbols.EndOfFile)
                {
                    RaiseErrorOccurred(HtmlParseError.EOF);
                    Back();
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = FlushBuffer();
                    break;
                }
                else
                {
                    StringBuffer.Append(c);
                }
            }

            return doctype;
        }

        /// <summary>
        /// See 8.2.4.59 DOCTYPE public identifier (single-quoted) state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private HtmlToken DoctypePublicIdentifierSingleQuoted(HtmlDoctypeToken doctype)
        {
            while (true)
            {
                var c = GetNext();

                if (c == Symbols.SingleQuote)
                {
                    doctype.PublicIdentifier = FlushBuffer();
                    return DoctypePublicIdentifierAfter(doctype);
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
                    doctype.PublicIdentifier = FlushBuffer();
                    break;
                }
                else if (c == Symbols.EndOfFile)
                {
                    RaiseErrorOccurred(HtmlParseError.EOF);
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = FlushBuffer();
                    Back();
                    break;
                }
                else
                {
                    StringBuffer.Append(c);
                }
            }

            return doctype;
        }

        /// <summary>
        /// See 8.2.4.60 After DOCTYPE public identifier state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private HtmlToken DoctypePublicIdentifierAfter(HtmlDoctypeToken doctype)
        {
            var c = GetNext();

            if (c.IsSpaceCharacter())
            {
                return DoctypeBetween(doctype);
            }
            else if (c == Symbols.GreaterThan)
            {
                State = HtmlParseMode.PCData;
            }
            else if (c == Symbols.DoubleQuote)
            {
                RaiseErrorOccurred(HtmlParseError.DoubleQuotationMarkUnexpected);
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(doctype);
            }
            else if (c == Symbols.SingleQuote)
            {
                RaiseErrorOccurred(HtmlParseError.SingleQuotationMarkUnexpected);
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierSingleQuoted(doctype);
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
                return BogusDoctype(doctype);
            }

            return doctype;
        }

        /// <summary>
        /// See 8.2.4.61 Between DOCTYPE public and system identifiers state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private HtmlToken DoctypeBetween(HtmlDoctypeToken doctype)
        {
            var c = SkipSpaces();

            if (c == Symbols.GreaterThan)
            {
                State = HtmlParseMode.PCData;
            }
            else if (c == Symbols.DoubleQuote)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(doctype);
            }
            else if (c == Symbols.SingleQuote)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierSingleQuoted(doctype);
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
                return BogusDoctype(doctype);
            }

            return doctype;
        }

        /// <summary>
        /// See 8.2.4.62 After DOCTYPE system keyword state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private HtmlToken DoctypeSystem(HtmlDoctypeToken doctype)
        {
            var c = GetNext();

            if (c.IsSpaceCharacter())
            {
                State = HtmlParseMode.PCData;
                return DoctypeSystemIdentifierBefore(doctype);
            }
            else if (c == Symbols.DoubleQuote)
            {
                RaiseErrorOccurred(HtmlParseError.DoubleQuotationMarkUnexpected);
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(doctype);
            }
            else if (c == Symbols.SingleQuote)
            {
                RaiseErrorOccurred(HtmlParseError.SingleQuotationMarkUnexpected);
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierSingleQuoted(doctype);
            }
            else if (c == Symbols.GreaterThan)
            {
                RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                doctype.SystemIdentifier = FlushBuffer();
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
                return BogusDoctype(doctype);
            }

            return doctype;
        }

        /// <summary>
        /// See 8.2.4.63 Before DOCTYPE system identifier state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private HtmlToken DoctypeSystemIdentifierBefore(HtmlDoctypeToken doctype)
        {
            var c = SkipSpaces();

            if (c == Symbols.DoubleQuote)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(doctype);
            }
            else if (c == Symbols.SingleQuote)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierSingleQuoted(doctype);
            }
            else if (c == Symbols.GreaterThan)
            {
                State = HtmlParseMode.PCData;
                RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = FlushBuffer();
            }
            else if (c == Symbols.EndOfFile)
            {
                RaiseErrorOccurred(HtmlParseError.EOF);
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = FlushBuffer();
                Back();
            }
            else
            {
                RaiseErrorOccurred(HtmlParseError.DoctypeInvalidCharacter);
                doctype.IsQuirksForced = true;
                return BogusDoctype(doctype);
            }

            return doctype;
        }

        /// <summary>
        /// See 8.2.4.64 DOCTYPE system identifier (double-quoted) state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private HtmlToken DoctypeSystemIdentifierDoubleQuoted(HtmlDoctypeToken doctype)
        {
            while (true)
            {
                var c = GetNext();

                if (c == Symbols.DoubleQuote)
                {
                    doctype.SystemIdentifier = FlushBuffer();
                    return DoctypeSystemIdentifierAfter(doctype);
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
                    doctype.SystemIdentifier = FlushBuffer();
                    break;
                }
                else if (c == Symbols.EndOfFile)
                {
                    RaiseErrorOccurred(HtmlParseError.EOF);
                    doctype.IsQuirksForced = true;
                    doctype.SystemIdentifier = FlushBuffer();
                    Back();
                    break;
                }
                else
                {
                    StringBuffer.Append(c);
                }
            }

            return doctype;
        }

        /// <summary>
        /// See 8.2.4.65 DOCTYPE system identifier (single-quoted) state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private HtmlToken DoctypeSystemIdentifierSingleQuoted(HtmlDoctypeToken doctype)
        {
            while (true)
            {
                var c = GetNext();

                switch (c)
                {
                    case Symbols.SingleQuote:
                        doctype.SystemIdentifier = FlushBuffer();
                        return DoctypeSystemIdentifierAfter(doctype);
                    case Symbols.Null:
                        AppendReplacement();
                        continue;
                    case Symbols.GreaterThan:
                        State = HtmlParseMode.PCData;
                        RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                        doctype.IsQuirksForced = true;
                        doctype.SystemIdentifier = FlushBuffer();
                        break;
                    case Symbols.EndOfFile:
                        RaiseErrorOccurred(HtmlParseError.EOF);
                        doctype.IsQuirksForced = true;
                        doctype.SystemIdentifier = FlushBuffer();
                        Back();
                        break;
                    default:
                        StringBuffer.Append(c);
                        continue;
                }

                return doctype;
            }
        }

        /// <summary>
        /// See 8.2.4.66 After DOCTYPE system identifier state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private HtmlToken DoctypeSystemIdentifierAfter(HtmlDoctypeToken doctype)
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
                    return BogusDoctype(doctype);
            }

            return doctype;
        }

        /// <summary>
        /// See 8.2.4.67 Bogus DOCTYPE state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        private HtmlToken BogusDoctype(HtmlDoctypeToken doctype)
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

                return doctype;
            }
        }

        #endregion

        #region Attributes

        private enum AttributeState : byte
        {
            BeforeName,
            Name,
            AfterName,
            BeforeValue,
            QuotedValue,
            AfterValue,
            UnquotedValue
        }

        private HtmlToken ParseAttributes(HtmlTagToken tag)
        {
            var state = AttributeState.BeforeName;
            var quote = Symbols.DoubleQuote;
            var c = Symbols.Null;

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
                            return TagSelfClosing(tag);
                        }
                        else if (c == Symbols.GreaterThan)
                        {
                            return EmitTag(tag);
                        }
                        else if (c.IsUppercaseAscii())
                        {
                            StringBuffer.Append(Char.ToLowerInvariant(c));
                            state = AttributeState.Name;
                        }
                        else if (c == Symbols.Null)
                        {
                            AppendReplacement();
                            state = AttributeState.Name;
                        }
                        else if (c == Symbols.SingleQuote || c == Symbols.DoubleQuote || c == Symbols.Equality || c == Symbols.LessThan)
                        {
                            RaiseErrorOccurred(HtmlParseError.AttributeNameInvalid);
                            StringBuffer.Append(c);
                            state = AttributeState.Name;
                        }
                        else if (c != Symbols.EndOfFile)
                        {
                            StringBuffer.Append(c);
                            state = AttributeState.Name;
                        }
                        else
                        {
                            return NewEof();
                        }

                        break;
                    }

                    // See 8.2.4.35 Attribute name state
                    case AttributeState.Name:
                    {
                        c = GetNext();

                        if (c == Symbols.Equality)
                        {
                            tag.AddAttribute(FlushBuffer());
                            state = AttributeState.BeforeValue;
                        }
                        else if (c == Symbols.GreaterThan)
                        {
                            tag.AddAttribute(FlushBuffer());
                            return EmitTag(tag);
                        }
                        else if (c.IsSpaceCharacter())
                        {
                            tag.AddAttribute(FlushBuffer());
                            state = AttributeState.AfterName;
                        }
                        else if (c == Symbols.Solidus)
                        {
                            tag.AddAttribute(FlushBuffer());
                            return TagSelfClosing(tag);
                        }
                        else if (c.IsUppercaseAscii())
                        {
                            StringBuffer.Append(Char.ToLowerInvariant(c));
                        }
                        else if (c == Symbols.DoubleQuote || c == Symbols.SingleQuote || c == Symbols.LessThan)
                        {
                            RaiseErrorOccurred(HtmlParseError.AttributeNameInvalid);
                            StringBuffer.Append(c);
                        }
                        else if (c == Symbols.Null)
                        {
                            AppendReplacement();
                        }
                        else if (c != Symbols.EndOfFile)
                        {
                            StringBuffer.Append(c);
                        }
                        else
                        {
                            return NewEof();
                        }

                        break;
                    }

                    // See 8.2.4.36 After attribute name state
                    case AttributeState.AfterName:
                    {
                        c = SkipSpaces();

                        if (c == Symbols.GreaterThan)
                        {
                            return EmitTag(tag);
                        }
                        else if (c == Symbols.Equality)
                        {
                            state = AttributeState.BeforeValue;
                        }
                        else if (c == Symbols.Solidus)
                        {
                            return TagSelfClosing(tag);
                        }
                        else if (c.IsUppercaseAscii())
                        {
                            StringBuffer.Append(Char.ToLowerInvariant(c));
                            state = AttributeState.Name;
                        }
                        else if (c == Symbols.DoubleQuote || c == Symbols.SingleQuote || c == Symbols.LessThan)
                        {
                            RaiseErrorOccurred(HtmlParseError.AttributeNameInvalid);
                            StringBuffer.Append(c);
                            state = AttributeState.Name;
                        }
                        else if (c == Symbols.Null)
                        {
                            AppendReplacement();
                            state = AttributeState.Name;
                        }
                        else if (c != Symbols.EndOfFile)
                        {
                            StringBuffer.Append(c);
                            state = AttributeState.Name;
                        }
                        else
                        {
                            return NewEof();
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
                            return EmitTag(tag);
                        }
                        else if (c == Symbols.LessThan || c == Symbols.Equality || c == Symbols.CurvedQuote)
                        {
                            RaiseErrorOccurred(HtmlParseError.AttributeValueInvalid);
                            StringBuffer.Append(c);
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
                            StringBuffer.Append(c);
                            state = AttributeState.UnquotedValue;
                            c = GetNext();
                        }
                        else
                        {
                            return NewEof();
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
                            tag.SetAttributeValue(FlushBuffer());
                            state = AttributeState.AfterValue;
                        }
                        else if (c == Symbols.Ampersand)
                        {
                            AppendCharacterReference(GetNext(), quote);
                        }
                        else if (c == Symbols.Null)
                        {
                            AppendReplacement();
                        }
                        else if (c != Symbols.EndOfFile)
                        {
                            StringBuffer.Append(c);
                        }
                        else
                        {
                            return NewEof();
                        }

                        break;
                    }

                    // See 8.2.4.40 Attribute value (unquoted) state
                    case AttributeState.UnquotedValue:
                    {
                        if (c == Symbols.GreaterThan)
                        {
                            tag.SetAttributeValue(FlushBuffer());
                            return EmitTag(tag);
                        }
                        else if (c.IsSpaceCharacter())
                        {
                            tag.SetAttributeValue(FlushBuffer());
                            state = AttributeState.BeforeName;
                        }
                        else if (c == Symbols.Ampersand)
                        {
                            AppendCharacterReference(GetNext(), Symbols.GreaterThan);
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
                            StringBuffer.Append(c);
                            c = GetNext();
                        }
                        else if (c != Symbols.EndOfFile)
                        {
                            StringBuffer.Append(c);
                            c = GetNext();
                        }
                        else
                        {
                            return NewEof();
                        }

                        break;
                    }

                    // See 8.2.4.42 After attribute value (quoted) state
                    case AttributeState.AfterValue:
                    {
                        c = GetNext();

                        if (c == Symbols.GreaterThan)
                        {
                            return EmitTag(tag);
                        }
                        else if (c.IsSpaceCharacter())
                        {
                            state = AttributeState.BeforeName;
                        }
                        else if (c == Symbols.Solidus)
                        {
                            return TagSelfClosing(tag);
                        }
                        else if (c == Symbols.EndOfFile)
                        {
                            return NewEof();
                        }
                        else
                        {
                            RaiseErrorOccurred(HtmlParseError.AttributeNameExpected);
                            Back();
                            state = AttributeState.BeforeName;
                        }

                        break;
                    }
                }
            }
        }

        #endregion

        #region Script

        private enum ScriptState : byte
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

        private HtmlToken ScriptData(Char c)
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
                                StringBuffer.Append(Symbols.LessThan);
                                state = ScriptState.OpenTag;
                                continue;

                            case Symbols.EndOfFile:
                                Back();
                                return NewCharacter();

                            default:
                                StringBuffer.Append(c);
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
                        StringBuffer.Append(Symbols.ExclamationMark);
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
                        StringBuffer.Append(Symbols.Minus);

                        if (c == Symbols.Minus)
                        {
                            StringBuffer.Append(Symbols.Minus);
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
                        offset = StringBuffer.Append(Symbols.Solidus).Length;
                        var tag = NewTagClose();

                        while (c.IsLetter())
                        {
                            // See 8.2.4.19 Script data end tag name state
                            StringBuffer.Append(c);
                            c = GetNext();
                            var isspace = c.IsSpaceCharacter();
                            var isclosed = c == Symbols.GreaterThan;
                            var isslash = c == Symbols.Solidus;
                            var hasLength = StringBuffer.Length - offset == length;

                            if (hasLength && (isspace || isclosed || isslash))
                            {
                                var name = StringBuffer.ToString(offset, length);

                                if (name.Isi(_lastStartTag))
                                {
                                    if (offset > 2)
                                    {
                                        Back(3 + length);
                                        StringBuffer.Remove(offset - 2, length + 2);
                                        return NewCharacter();
                                    }

                                    StringBuffer.Clear();

                                    if (isspace)
                                    {
                                        tag.Name = _lastStartTag;
                                        return ParseAttributes(tag);
                                    }
                                    else if (isslash)
                                    {
                                        tag.Name = _lastStartTag;
                                        return TagSelfClosing(tag);
                                    }
                                    else if (isclosed)
                                    {
                                        tag.Name = _lastStartTag;
                                        return EmitTag(tag);
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
                                StringBuffer.Append(Symbols.Minus);
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
                                return NewCharacter();
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
                                StringBuffer.Append(Symbols.Minus);
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
                                return NewCharacter();
                            default:
                                StringBuffer.Append(c);
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
                                StringBuffer.Append(Symbols.Minus);
                                break;
                            case Symbols.LessThan:
                                c = GetNext();
                                state = ScriptState.EscapedOpenTag;
                                continue;
                            case Symbols.GreaterThan:
                                StringBuffer.Append(Symbols.GreaterThan);
                                c = GetNext();
                                state = ScriptState.Normal;
                                continue;
                            case Symbols.Null:
                                AppendReplacement();
                                c = GetNext();
                                state = ScriptState.Escaped;
                                continue;
                            case Symbols.EndOfFile:
                                return NewCharacter();
                            default:
                                StringBuffer.Append(c);
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
                            offset = StringBuffer.Append(Symbols.LessThan).Length;
                            StringBuffer.Append(c);
                            state = ScriptState.StartDoubleEscape;
                        }
                        else
                        {
                            StringBuffer.Append(Symbols.LessThan);
                            state = ScriptState.Escaped;
                        }

                        break;
                    }

                    // See 8.2.4.26 Script data escaped end tag open state
                    case ScriptState.EscapedEndTag:
                    {
                        offset = StringBuffer.Append(Symbols.LessThan).Append(Symbols.Solidus).Length;

                        if (c.IsLetter())
                        {
                            StringBuffer.Append(c);
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
                        var hasLength = StringBuffer.Length - offset == scriptLength;

                        if (hasLength && (c == Symbols.Solidus || c == Symbols.GreaterThan || c.IsSpaceCharacter()) &&
                            StringBuffer.ToString(offset, scriptLength).Isi(TagNames.Script))
                        {
                            Back(scriptLength + 3);
                            StringBuffer.Remove(offset - 2, scriptLength + 2);
                            return NewCharacter();
                        }
                        else if (!c.IsLetter())
                        {
                            state = ScriptState.Escaped;
                        }
                        else
                        {
                            StringBuffer.Append(c);
                        }

                        break;
                    }

                    // See 8.2.4.28 Script data double escape start state
                    case ScriptState.StartDoubleEscape:
                    {
                        c = GetNext();
                        var hasLength = StringBuffer.Length - offset == scriptLength;

                        if (hasLength && (c == Symbols.Solidus || c == Symbols.GreaterThan || c.IsSpaceCharacter()))
                        {
                            var isscript = StringBuffer.ToString(offset, scriptLength).Isi(TagNames.Script);
                            StringBuffer.Append(c);
                            c = GetNext();
                            state = isscript ? ScriptState.EscapedDouble : ScriptState.Escaped;
                        }
                        else if (c.IsLetter())
                        {
                            StringBuffer.Append(c);
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
                                StringBuffer.Append(Symbols.Minus);
                                c = GetNext();
                                state = ScriptState.EscapedDoubleDash;
                                continue;

                            case Symbols.LessThan:
                                StringBuffer.Append(Symbols.LessThan);
                                c = GetNext();
                                state = ScriptState.EscapedDoubleOpenTag;
                                continue;

                            case Symbols.Null:
                                AppendReplacement();
                                break;

                            case Symbols.EndOfFile:
                                RaiseErrorOccurred(HtmlParseError.EOF);
                                Back();
                                return NewCharacter();
                        }

                        StringBuffer.Append(c);
                        c = GetNext();
                        break;
                    }

                    // See 8.2.4.30 Script data double escaped dash state
                    case ScriptState.EscapedDoubleDash:
                    {
                        switch (c)
                        {
                            case Symbols.Minus:
                                StringBuffer.Append(Symbols.Minus);
                                state = ScriptState.EscapedDoubleDashDash;
                                continue;

                            case Symbols.LessThan:
                                StringBuffer.Append(Symbols.LessThan);
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
                                return NewCharacter();
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
                                StringBuffer.Append(Symbols.Minus);
                                break;

                            case Symbols.LessThan:
                                StringBuffer.Append(Symbols.LessThan);
                                c = GetNext();
                                state = ScriptState.EscapedDoubleOpenTag;
                                continue;

                            case Symbols.GreaterThan:
                                StringBuffer.Append(Symbols.GreaterThan);
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
                                return NewCharacter();

                            default:
                                StringBuffer.Append(c);
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
                            offset = StringBuffer.Append(Symbols.Solidus).Length;
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
                        var hasLength = StringBuffer.Length - offset == scriptLength;

                        if (hasLength && (c.IsSpaceCharacter() || c == Symbols.Solidus || c == Symbols.GreaterThan))
                        {
                            var isscript = StringBuffer.ToString(offset, scriptLength).Isi(TagNames.Script);
                            StringBuffer.Append(c);
                            c = GetNext();
                            state = isscript ? ScriptState.Escaped : ScriptState.EscapedDouble;
                        }
                        else if (c.IsLetter())
                        {
                            StringBuffer.Append(c);
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

        private HtmlToken NewCharacter()
        {
            var content = FlushBuffer();
            return new HtmlToken(HtmlTokenType.Character, _position, content);
        }

        private HtmlToken NewComment()
        {
            var content = FlushBuffer();
            return new HtmlToken(HtmlTokenType.Comment, _position, content);
        }

        private HtmlToken NewEof(Boolean acceptable = false)
        {
            if (!acceptable)
            {
                RaiseErrorOccurred(HtmlParseError.EOF);
            }

            return new HtmlToken(HtmlTokenType.EndOfFile, _position);
        }

        private HtmlDoctypeToken NewDoctype(Boolean quirksForced)
        {
            return new HtmlDoctypeToken(quirksForced, _position);
        }

        private HtmlTagToken NewTagOpen()
        {
            return new HtmlTagToken(HtmlTokenType.StartTag, _position);
        }

        private HtmlTagToken NewTagClose()
        {
            return new HtmlTagToken(HtmlTokenType.EndTag, _position);
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
            StringBuffer.Append(Symbols.Replacement);
        }

        private HtmlToken CreateIfAppropriate(Char c)
        {
            var isspace = c.IsSpaceCharacter();
            var isclosed = c == Symbols.GreaterThan;
            var isslash = c == Symbols.Solidus;
            var hasLength = StringBuffer.Length == _lastStartTag.Length;

            if (hasLength && (isspace || isclosed || isslash) && StringBuffer.ToString().Is(_lastStartTag))
            {
                var tag = NewTagClose();
                StringBuffer.Clear();

                if (isspace)
                {
                    tag.Name = _lastStartTag;
                    return ParseAttributes(tag);
                }
                else if (isslash)
                {
                    tag.Name = _lastStartTag;
                    return TagSelfClosing(tag);
                }
                else if (isclosed)
                {
                    tag.Name = _lastStartTag;
                    return EmitTag(tag);
                }
            }

            return null;
        }

        private HtmlToken EmitTag(HtmlTagToken tag)
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
                            if (attributes[j].Key == attributes[i].Key)
                            {
                                attributes.RemoveAt(i);
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

            return tag;
        }

        #endregion
    }
}
