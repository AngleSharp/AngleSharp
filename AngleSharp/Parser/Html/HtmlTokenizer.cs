namespace AngleSharp.Parser.Html
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using AngleSharp.Events;
    using AngleSharp.Extensions;
    using AngleSharp.Html;

    /// <summary>
    /// Performs the tokenization of the source code. Follows the tokenization algorithm at:
    /// http://www.w3.org/html/wg/drafts/html/master/syntax.html
    /// </summary>
    [DebuggerStepThrough]
    sealed class HtmlTokenizer : BaseTokenizer
    {
        #region Fields
        
        Boolean _acceptsCharacterData;
        String _lastStartTag;
        HtmlParseMode _state;
        TextPosition _position;

        #endregion

        #region ctor

        /// <summary>
        /// See 8.2.4 Tokenization
        /// </summary>
        /// <param name="source">The source code manager.</param>
        /// <param name="events">The event aggregator to use.</param>
        public HtmlTokenizer(TextSource source, IEventAggregator events)
            : base(source, events)
        {
            _state = HtmlParseMode.PCData;
            _acceptsCharacterData = false;
            _lastStartTag = String.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if CDATA sections are accepted.
        /// </summary>
        public Boolean IsAcceptingCharacterData
        {
            get { return _acceptsCharacterData; }
            set { _acceptsCharacterData = value; }
        }

        /// <summary>
        /// Gets or sets the current parse mode.
        /// </summary>
        public HtmlParseMode State
        {
            get { return _state; }
            set { _state = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fires an error occurred event.
        /// </summary>
        /// <param name="code">The associated error code.</param>
        /// <param name="position">The position of the error.</param>
        public void RaiseErrorOccurred(HtmlParseError code, TextPosition position)
        {
            if (_events != null)
            {
                var errorEvent = new HtmlParseErrorEvent(code, position);
                _events.Publish(errorEvent);
            }
        }

        /// <summary>
        /// Fires an error occurred event at the current position.
        /// </summary>
        /// <param name="code">The associated error code.</param>
        public void RaiseErrorOccurred(HtmlParseError code)
        {
            RaiseErrorOccurred(code, GetCurrentPosition());
        }

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
                switch (_state)
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

            return NewEof();
        }

        #endregion

        #region Data

        /// <summary>
        /// See 8.2.4.1 Data state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken Data(Char c)
        {
            return c == Symbols.LessThan ? TagOpen(GetNext()) : DataText(c);
        }

        HtmlToken DataText(Char c)
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
                        var value = CharacterReference(GetNext());

                        if (value == null)
                            _stringBuffer.Append(Symbols.Ampersand);
                        else
                            _stringBuffer.Append(value);

                        break;

                    case Symbols.Null:
                        RaiseErrorOccurred(HtmlParseError.Null);
                        break;

                    default:
                        _stringBuffer.Append(c);
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
        HtmlToken Plaintext(Char c)
        {
            while (true)
            {
                switch (c)
                {
                    case Symbols.Null:
                        RaiseErrorOccurred(HtmlParseError.Null);
                        _stringBuffer.Append(Symbols.Replacement);
                        break;

                    case Symbols.EndOfFile:
                        Back();
                        return NewCharacter();

                    default:
                        _stringBuffer.Append(c);
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
        HtmlToken RCData(Char c)
        {
            return c == Symbols.LessThan ? RCDataLt(GetNext()) : RCDataText(c);
        }
        
        HtmlToken RCDataText(Char c)
        {
            while (true)
            {
                switch (c)
                {
                    case Symbols.Ampersand:
                        var value = CharacterReference(GetNext());

                        if (value == null)
                            _stringBuffer.Append(Symbols.Ampersand);

                        _stringBuffer.Append(value);
                        break;

                    case Symbols.LessThan:
                    case Symbols.EndOfFile:
                        Back();
                        return NewCharacter();

                    case Symbols.Null:
                        RaiseErrorOccurred(HtmlParseError.Null);
                        _stringBuffer.Append(Symbols.Replacement);
                        break;

                    default:
                        _stringBuffer.Append(c);
                        break;
                }

                c = GetNext();
            }
        }

        /// <summary>
        /// See 8.2.4.11 RCDATA less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken RCDataLt(Char c)
        {
            if (c == Symbols.Solidus)
            {
                // See 8.2.4.12 RCDATA end tag open state
                c = GetNext();

                if (c.IsUppercaseAscii())
                {
                    _stringBuffer.Append(Char.ToLower(c));
                    return RCDataNameEndTag(GetNext());
                }
                else if (c.IsLowercaseAscii())
                {
                    _stringBuffer.Append(c);
                    return RCDataNameEndTag(GetNext());
                }
                else
                {
                    _stringBuffer.Append(Symbols.LessThan).Append(Symbols.Solidus);
                    return RCDataText(c);
                }
            }
            else
            {
                _stringBuffer.Append(Symbols.LessThan);
                return RCDataText(c);
            }
        }

        /// <summary>
        /// See 8.2.4.13 RCDATA end tag name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken RCDataNameEndTag(Char c)
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
                    _stringBuffer.Append(Char.ToLower(c));
                }
                else if (c.IsLowercaseAscii())
                {
                    _stringBuffer.Append(c);
                }
                else
                {
                    _stringBuffer.Insert(0, Symbols.LessThan).Insert(1, Symbols.Solidus);
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
        HtmlToken Rawtext(Char c)
        {
            return c == Symbols.LessThan ? RawtextLT(GetNext()) : RawtextText(c);
        }

        HtmlToken RawtextText(Char c)
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
                        RaiseErrorOccurred(HtmlParseError.Null);
                        _stringBuffer.Append(Symbols.Replacement);
                        break;

                    default:
                        _stringBuffer.Append(c);
                        break;
                }

                c = GetNext();
            }
        }

        /// <summary>
        /// See 8.2.4.14 RAWTEXT less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken RawtextLT(Char c)
        {
            if (c == Symbols.Solidus)
            {
                // See 8.2.4.15 RAWTEXT end tag open state
                c = GetNext();

                if (c.IsUppercaseAscii())
                {
                    _stringBuffer.Append(Char.ToLower(c));
                    return RawtextNameEndTag(GetNext());
                }
                else if (c.IsLowercaseAscii())
                {
                    _stringBuffer.Append(c);
                    return RawtextNameEndTag(GetNext());
                }
                else
                {
                    _stringBuffer.Append(Symbols.LessThan).Append(Symbols.Solidus);
                    return RawtextText(c);
                }
            }
            else
            {
                _stringBuffer.Append(Symbols.LessThan);
                return RawtextText(c);
            }
        }

        /// <summary>
        /// See 8.2.4.16 RAWTEXT end tag name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken RawtextNameEndTag(Char c)
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
                    _stringBuffer.Append(Char.ToLower(c));
                }
                else if (c.IsLowercaseAscii())
                {
                    _stringBuffer.Append(c);
                }
                else
                {
                    _stringBuffer.Insert(0, Symbols.LessThan).Insert(1, Symbols.Solidus);
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
        HtmlToken CharacterData(Char c)
        {
            while (true)
            {
                if (c == Symbols.EndOfFile)
                {
                    Back();
                    break;
                }
                else if (c == Symbols.SquareBracketClose && ContinuesWith("]]>"))
                {
                    Advance(2);
                    break;
                }
                else
                {
                    _stringBuffer.Append(c);
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
        String CharacterReference(Char c, Char allowedCharacter = Symbols.Null)
        {
            if (c.IsSpaceCharacter() || c == Symbols.LessThan || c == Symbols.EndOfFile || c == Symbols.Ampersand || c == allowedCharacter)
            {
                Back();
                return null;
            }

            if (c == Symbols.Num)
            {
                var exp = 10;
                var basis = 1;
                var num = 0;
                var nums = new List<Int32>();
                c = GetNext();
                var isHex = c == 'x' || c == 'X';

                if (isHex)
                {
                    exp = 16;

                    while ((c = GetNext()).IsHex())
                        nums.Add(c.FromHex());
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
                        Back();

                    RaiseErrorOccurred(HtmlParseError.CharacterReferenceWrongNumber);
                    return null;
                }

                if (c != Symbols.Semicolon)
                {
                    RaiseErrorOccurred(HtmlParseError.CharacterReferenceSemicolonMissing);
                    Back();
                }

                if (Entities.IsInCharacterTable(num))
                {
                    RaiseErrorOccurred(HtmlParseError.CharacterReferenceInvalidCode);
                    return Entities.GetSymbolFromTable(num);
                }

                if (Entities.IsInvalidNumber(num))
                {
                    RaiseErrorOccurred(HtmlParseError.CharacterReferenceInvalidNumber);
                    return Symbols.Replacement.ToString();
                }

                if (Entities.IsInInvalidRange(num))
                    RaiseErrorOccurred(HtmlParseError.CharacterReferenceInvalidRange);

                return Entities.Convert(num);
            }
            else
            {
                String last = null;
                var consumed = 0;
                var start = InsertionPoint - 1;
                var reference = new Char[31];
                var index = 0;
                var chr = Current;

                do
                {
                    if (chr == Symbols.Semicolon || !chr.IsName())
                        break;

                    reference[index++] = chr;
                    var value = new String(reference, 0, index);
                    chr = GetNext();
                    consumed++;
                    value = chr == Symbols.Semicolon ? Entities.GetSymbol(value) : Entities.GetSymbolWithoutSemicolon(value);

                    if (value != null)
                    {
                        consumed = 0;
                        last = value;
                    }
                }
                while (chr != Symbols.EndOfFile && index < 31);

                Back(consumed);
                chr = Current;

                if (chr != Symbols.Semicolon)
                {
                    if (allowedCharacter != Symbols.Null && (chr == Symbols.Equality || chr.IsAlphanumericAscii()))
                    {
                        if (chr == Symbols.Equality)
                            RaiseErrorOccurred(HtmlParseError.CharacterReferenceAttributeEqualsFound);

                        InsertionPoint = start;
                        return null;
                    }

                    Back();
                    RaiseErrorOccurred(HtmlParseError.CharacterReferenceNotTerminated);
                }

                return last;
            }
        }

        #endregion

        #region Tags

        /// <summary>
        /// See 8.2.4.8 Tag open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken TagOpen(Char c)
        {
            if (c == Symbols.Solidus)
            {
                return TagEnd(GetNext());
            }
            else if (c.IsLowercaseAscii())
            {
                _stringBuffer.Append(c);
                return TagName(NewTagOpen());
            }
            else if (c.IsUppercaseAscii())
            {
                _stringBuffer.Append(Char.ToLower(c));
                return TagName(NewTagOpen());
            }
            else if (c == Symbols.ExclamationMark)
            {
                return MarkupDeclaration(GetNext());
            }
            else if (c != Symbols.QuestionMark)
            {
                _state = HtmlParseMode.PCData;
                RaiseErrorOccurred(HtmlParseError.AmbiguousOpenTag);
                _stringBuffer.Append(Symbols.LessThan);
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
        HtmlToken TagEnd(Char c)
        {
            if (c.IsLowercaseAscii())
            {
                _stringBuffer.Append(c);
                return TagName(NewTagClose());
            }
            else if (c.IsUppercaseAscii())
            {
                _stringBuffer.Append(Char.ToLower(c));
                return TagName(NewTagClose());
            }
            else if (c == Symbols.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
                RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                return Data(GetNext());
            }
            else if (c == Symbols.EndOfFile)
            {
                Back();
                RaiseErrorOccurred(HtmlParseError.EOF);
                _stringBuffer.Append(Symbols.LessThan).Append(Symbols.Solidus);
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
        HtmlToken TagName(HtmlTagToken tag)
        {
            while (true)
            {
                var c = GetNext();

                if (c == Symbols.GreaterThan)
                {
                    tag.Name = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return EmitTag(tag);
                }
                else if (c.IsSpaceCharacter())
                {
                    tag.Name = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return AttributeBeforeName(tag);
                }
                else if (c == Symbols.Solidus)
                {
                    tag.Name = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return TagSelfClosing(tag);
                }
                else if (c.IsUppercaseAscii())
                {
                    _stringBuffer.Append(Char.ToLower(c));
                }
                else if (c == Symbols.Null)
                {
                    RaiseErrorOccurred(HtmlParseError.Null);
                    _stringBuffer.Append(Symbols.Replacement);
                }
                else if (c != Symbols.EndOfFile)
                {
                    _stringBuffer.Append(c);
                }
                else
                {
                    RaiseErrorOccurred(HtmlParseError.EOF);
                    return NewEof();
                }
            }
        }

        /// <summary>
        /// See 8.2.4.43 Self-closing start tag state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        HtmlToken TagSelfClosing(HtmlTagToken tag)
        {
            switch (GetNext())
            {
                case Symbols.GreaterThan:
                    tag.IsSelfClosing = true;
                    return EmitTag(tag);
                case Symbols.EndOfFile:
                    RaiseErrorOccurred(HtmlParseError.EOF);
                    return NewEof();
                default:
                    RaiseErrorOccurred(HtmlParseError.ClosingSlashMisplaced);
                    Back();
                    return AttributeBeforeName(tag);
            }
        }

        /// <summary>
        /// See 8.2.4.45 Markup declaration open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken MarkupDeclaration(Char c)
        {
            if (ContinuesWith("--"))
            {
                Advance();
                return CommentStart(GetNext());
            }
            else if (ContinuesWith(Tags.Doctype))
            {
                Advance(6);
                return Doctype(GetNext());
            }
            else if (_acceptsCharacterData && ContinuesWith("[CDATA[", ignoreCase: false))
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
        HtmlToken BogusComment(Char c)
        {
            _stringBuffer.Clear();

            while(true)
            {
                switch (c)
                {
                    case Symbols.GreaterThan:
                        break;
                    case Symbols.EndOfFile:
                        Back();
                        break;
                    case Symbols.Null:
                        _stringBuffer.Append(Symbols.Replacement);
                        c = GetNext();
                        continue;
                    default:
                        _stringBuffer.Append(c);
                        c = GetNext();
                        continue;
                }

                _state = HtmlParseMode.PCData;
                return NewComment();
            }
        }

        /// <summary>
        /// See 8.2.4.46 Comment start state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken CommentStart(Char c)
        {
            _stringBuffer.Clear();

            switch (c)
            {
                case Symbols.Minus:
                    return CommentDashStart(GetNext());
                case Symbols.Null:
                    RaiseErrorOccurred(HtmlParseError.Null);
                    _stringBuffer.Append(Symbols.Replacement);
                    return Comment(GetNext());
                case Symbols.GreaterThan:
                    _state = HtmlParseMode.PCData;
                    RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                    break;
                case Symbols.EndOfFile:
                    RaiseErrorOccurred(HtmlParseError.EOF);
                    Back();
                    break;
                default:
                    _stringBuffer.Append(c);
                    return Comment(GetNext());
            }

            return NewComment();
        }

        /// <summary>
        /// See 8.2.4.47 Comment start dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken CommentDashStart(Char c)
        {
            switch (c)
            {
                case Symbols.Minus:
                    return CommentEnd(GetNext());
                case Symbols.Null:
                    RaiseErrorOccurred(HtmlParseError.Null);
                    _stringBuffer.Append(Symbols.Minus).Append(Symbols.Replacement);
                    return Comment(GetNext());
                case Symbols.GreaterThan:
                    _state = HtmlParseMode.PCData;
                    RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                    break;
                case Symbols.EndOfFile:
                    RaiseErrorOccurred(HtmlParseError.EOF);
                    Back();
                    break;
                default:
                    _stringBuffer.Append(Symbols.Minus).Append(c);
                    return Comment(GetNext());
            }

            return NewComment();
        }

        /// <summary>
        /// See 8.2.4.48 Comment state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken Comment(Char c)
        {
            while (true)
            {
                switch (c)
                {
                    case Symbols.Minus:
                        var result = CommentDashEnd(GetNext());

                        if (result != null)
                            return result;

                        break;
                    case Symbols.EndOfFile:
                        RaiseErrorOccurred(HtmlParseError.EOF);
                        Back();
                        return NewComment();
                    case Symbols.Null:
                        RaiseErrorOccurred(HtmlParseError.Null);
                        _stringBuffer.Append(Symbols.Replacement);
                        break;
                    default:
                        _stringBuffer.Append(c);
                        break;
                }

                c = GetNext();
            }
        }

        /// <summary>
        /// See 8.2.4.49 Comment end dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken CommentDashEnd(Char c)
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

            _stringBuffer.Append(Symbols.Minus).Append(c);
            return null;
        }

        /// <summary>
        /// See 8.2.4.50 Comment end state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken CommentEnd(Char c)
        {
            while (true)
            {
                switch (c)
                {
                    case Symbols.GreaterThan:
                        _state = HtmlParseMode.PCData;
                        return NewComment();
                    case Symbols.Null:
                        RaiseErrorOccurred(HtmlParseError.Null);
                        _stringBuffer.Append(Symbols.Minus).Append(Symbols.Replacement);
                        return null;
                    case Symbols.ExclamationMark:
                        RaiseErrorOccurred(HtmlParseError.CommentEndedWithEM);
                        return CommentBangEnd(GetNext());
                    case Symbols.Minus:
                        RaiseErrorOccurred(HtmlParseError.CommentEndedWithDash);
                        _stringBuffer.Append(Symbols.Minus);
                        break;
                    case Symbols.EndOfFile:
                        RaiseErrorOccurred(HtmlParseError.EOF);
                        Back();
                        return NewComment();
                    default:
                        RaiseErrorOccurred(HtmlParseError.CommentEndedUnexpected);
                        _stringBuffer.Append(Symbols.Minus).Append(Symbols.Minus).Append(c);
                        return null;
                }

                c = GetNext();
            }
        }

        /// <summary>
        /// See 8.2.4.51 Comment end bang state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken CommentBangEnd(Char c)
        {
            switch (c)
            {
                case Symbols.Minus:
                    _stringBuffer.Append(Symbols.Minus).Append(Symbols.Minus).Append(Symbols.ExclamationMark);
                    return CommentDashEnd(GetNext());
                case Symbols.GreaterThan:
                    _state = HtmlParseMode.PCData;
                    break;
                case Symbols.Null:
                    RaiseErrorOccurred(HtmlParseError.Null);
                    _stringBuffer.Append(Symbols.Minus).Append(Symbols.Minus).Append(Symbols.ExclamationMark).Append(Symbols.Replacement);
                    return null;
                case Symbols.EndOfFile:
                    RaiseErrorOccurred(HtmlParseError.EOF);
                    Back();
                    break;
                default:
                    _stringBuffer.Append(Symbols.Minus).Append(Symbols.Minus).Append(Symbols.ExclamationMark).Append(c);
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
        HtmlToken Doctype(Char c)
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
        HtmlToken DoctypeNameBefore(Char c)
        {
            while (c.IsSpaceCharacter())
                c = GetNext();

            if (c.IsUppercaseAscii())
            {
                var doctype = NewDoctype(false);
                _stringBuffer.Append(Char.ToLower(c));
                return DoctypeName(doctype);
            }
            else if (c == Symbols.Null)
            {
                var doctype = NewDoctype(false);
                RaiseErrorOccurred(HtmlParseError.Null);
                _stringBuffer.Append(Symbols.Replacement);
                return DoctypeName(doctype);
            }
            else if (c == Symbols.GreaterThan)
            {
                var doctype = NewDoctype(true);
                _state = HtmlParseMode.PCData;
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
                _stringBuffer.Append(c);
                return DoctypeName(doctype);
            }
        }

        /// <summary>
        /// See 8.2.4.54 DOCTYPE name state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        HtmlToken DoctypeName(HtmlDoctypeToken doctype)
        {
            while (true)
            {
                var c = GetNext();

                if (c.IsSpaceCharacter())
                {
                    doctype.Name = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return DoctypeNameAfter(doctype);
                }
                else if (c == Symbols.GreaterThan)
                {
                    _state = HtmlParseMode.PCData;
                    doctype.Name = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    break;
                }
                else if (c.IsUppercaseAscii())
                {
                    _stringBuffer.Append(Char.ToLower(c));
                }
                else if (c == Symbols.Null)
                {
                    RaiseErrorOccurred(HtmlParseError.Null);
                    _stringBuffer.Append(Symbols.Replacement);
                }
                else if (c == Symbols.EndOfFile)
                {
                    RaiseErrorOccurred(HtmlParseError.EOF);
                    Back();
                    doctype.IsQuirksForced = true;
                    doctype.Name = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    break;
                }
                else
                {
                    _stringBuffer.Append(c);
                }
            }

            return doctype;
        }

        /// <summary>
        /// See 8.2.4.55 After DOCTYPE name state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        HtmlToken DoctypeNameAfter(HtmlDoctypeToken doctype)
        {
            var c = SkipSpaces();

            if (c == Symbols.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
            }
            else if (c == Symbols.EndOfFile)
            {
                RaiseErrorOccurred(HtmlParseError.EOF);
                Back();
                doctype.IsQuirksForced = true;
            }
            else if (ContinuesWith("public"))
            {
                Advance(5);
                return DoctypePublic(doctype);
            }
            else if (ContinuesWith("system"))
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
        HtmlToken DoctypePublic(HtmlDoctypeToken doctype)
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
                _state = HtmlParseMode.PCData;
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
        HtmlToken DoctypePublicIdentifierBefore(HtmlDoctypeToken doctype)
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
                _state = HtmlParseMode.PCData;
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
        HtmlToken DoctypePublicIdentifierDoubleQuoted(HtmlDoctypeToken doctype)
        {
            while (true)
            {
                var c = GetNext();

                if (c == Symbols.DoubleQuote)
                {
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return DoctypePublicIdentifierAfter(doctype);
                }
                else if (c == Symbols.Null)
                {
                    RaiseErrorOccurred(HtmlParseError.Null);
                    _stringBuffer.Append(Symbols.Replacement);
                }
                else if (c == Symbols.GreaterThan)
                {
                    _state = HtmlParseMode.PCData;
                    RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    break;
                }
                else if (c == Symbols.EndOfFile)
                {
                    RaiseErrorOccurred(HtmlParseError.EOF);
                    Back();
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    break;
                }
                else
                {
                    _stringBuffer.Append(c);
                }
            }

            return doctype;
        }

        /// <summary>
        /// See 8.2.4.59 DOCTYPE public identifier (single-quoted) state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        HtmlToken DoctypePublicIdentifierSingleQuoted(HtmlDoctypeToken doctype)
        {
            while (true)
            {
                var c = GetNext();

                if (c == Symbols.SingleQuote)
                {
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return DoctypePublicIdentifierAfter(doctype);
                }
                else if (c == Symbols.Null)
                {
                    RaiseErrorOccurred(HtmlParseError.Null);
                    _stringBuffer.Append(Symbols.Replacement);
                }
                else if (c == Symbols.GreaterThan)
                {
                    _state = HtmlParseMode.PCData;
                    RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    break;
                }
                else if (c == Symbols.EndOfFile)
                {
                    RaiseErrorOccurred(HtmlParseError.EOF);
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    Back();
                    break;
                }
                else
                {
                    _stringBuffer.Append(c);
                }
            }

            return doctype;
        }

        /// <summary>
        /// See 8.2.4.60 After DOCTYPE public identifier state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        HtmlToken DoctypePublicIdentifierAfter(HtmlDoctypeToken doctype)
        {
            var c = GetNext();

            if (c.IsSpaceCharacter())
            {
                return DoctypeBetween(doctype);
            }
            else if (c == Symbols.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
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
        HtmlToken DoctypeBetween(HtmlDoctypeToken doctype)
        {
            var c = SkipSpaces();

            if (c == Symbols.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
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
        HtmlToken DoctypeSystem(HtmlDoctypeToken doctype)
        {
            var c = GetNext();

            if (c.IsSpaceCharacter())
            {
                _state = HtmlParseMode.PCData;
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
                doctype.SystemIdentifier = _stringBuffer.ToString();
                _stringBuffer.Clear();
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
        HtmlToken DoctypeSystemIdentifierBefore(HtmlDoctypeToken doctype)
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
                _state = HtmlParseMode.PCData;
                RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = _stringBuffer.ToString();
                _stringBuffer.Clear();
            }
            else if (c == Symbols.EndOfFile)
            {
                RaiseErrorOccurred(HtmlParseError.EOF);
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = _stringBuffer.ToString();
                _stringBuffer.Clear();
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
        HtmlToken DoctypeSystemIdentifierDoubleQuoted(HtmlDoctypeToken doctype)
        {
            while (true)
            {
                var c = GetNext();

                if (c == Symbols.DoubleQuote)
                {
                    doctype.SystemIdentifier = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return DoctypeSystemIdentifierAfter(doctype);
                }
                else if (c == Symbols.Null)
                {
                    RaiseErrorOccurred(HtmlParseError.Null);
                    _stringBuffer.Append(Symbols.Replacement);
                }
                else if (c == Symbols.GreaterThan)
                {
                    _state = HtmlParseMode.PCData;
                    RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                    doctype.IsQuirksForced = true;
                    doctype.SystemIdentifier = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    break;
                }
                else if (c == Symbols.EndOfFile)
                {
                    RaiseErrorOccurred(HtmlParseError.EOF);
                    doctype.IsQuirksForced = true;
                    doctype.SystemIdentifier = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    Back();
                    break;
                }
                else
                {
                    _stringBuffer.Append(c);
                }
            }

            return doctype;
        }

        /// <summary>
        /// See 8.2.4.65 DOCTYPE system identifier (single-quoted) state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        HtmlToken DoctypeSystemIdentifierSingleQuoted(HtmlDoctypeToken doctype)
        {
            while (true)
            {
                var c = GetNext();

                switch (c)
                {
                    case Symbols.SingleQuote:
                        doctype.SystemIdentifier = _stringBuffer.ToString();
                        _stringBuffer.Clear();
                        return DoctypeSystemIdentifierAfter(doctype);
                    case Symbols.Null:
                        RaiseErrorOccurred(HtmlParseError.Null);
                        _stringBuffer.Append(Symbols.Replacement);
                        continue;
                    case Symbols.GreaterThan:
                        _state = HtmlParseMode.PCData;
                        RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                        doctype.IsQuirksForced = true;
                        doctype.SystemIdentifier = _stringBuffer.ToString();
                        _stringBuffer.Clear();
                        break;
                    case Symbols.EndOfFile:
                        RaiseErrorOccurred(HtmlParseError.EOF);
                        doctype.IsQuirksForced = true;
                        doctype.SystemIdentifier = _stringBuffer.ToString();
                        _stringBuffer.Clear();
                        Back();
                        break;
                    default:
                        _stringBuffer.Append(c);
                        continue;
                }

                return doctype;
            }
        }

        /// <summary>
        /// See 8.2.4.66 After DOCTYPE system identifier state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        HtmlToken DoctypeSystemIdentifierAfter(HtmlDoctypeToken doctype)
        {
            var c = SkipSpaces();

            switch (c)
            {
                case Symbols.GreaterThan:
                    _state = HtmlParseMode.PCData;
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
        HtmlToken BogusDoctype(HtmlDoctypeToken doctype)
        {
            while (true)
            {
                switch (GetNext())
                {
                    case Symbols.GreaterThan:
                        _state = HtmlParseMode.PCData;
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

        /// <summary>
        /// See 8.2.4.34 Before attribute name state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        HtmlToken AttributeBeforeName(HtmlTagToken tag)
        {
            var c = SkipSpaces();

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
                _stringBuffer.Append(Char.ToLower(c));
                return AttributeName(tag);
            }
            else if (c == Symbols.Null)
            {
                RaiseErrorOccurred(HtmlParseError.Null);
                _stringBuffer.Append(Symbols.Replacement);
                return AttributeName(tag);
            }
            else if (c == Symbols.SingleQuote || c == Symbols.DoubleQuote || c == Symbols.Equality || c == Symbols.LessThan)
            {
                RaiseErrorOccurred(HtmlParseError.AttributeNameInvalid);
                _stringBuffer.Append(c);
                return AttributeName(tag);
            }
            else if (c != Symbols.EndOfFile)
            {
                _stringBuffer.Append(c);
                return AttributeName(tag);
            }
            else
            {
                return NewEof();
            }
        }

        /// <summary>
        /// See 8.2.4.35 Attribute name state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        HtmlToken AttributeName(HtmlTagToken tag)
        {
            while (true)
            {
                var c = GetNext();

                if (c == Symbols.Equality)
                {
                    tag.AddAttribute(_stringBuffer.ToString());
                    _stringBuffer.Clear();
                    return AttributeBeforeValue(tag);
                }
                else if (c == Symbols.GreaterThan)
                {
                    tag.AddAttribute(_stringBuffer.ToString());
                    _stringBuffer.Clear();
                    return EmitTag(tag);
                }
                else if (c.IsSpaceCharacter())
                {
                    tag.AddAttribute(_stringBuffer.ToString());
                    _stringBuffer.Clear();
                    return AttributeAfterName(tag);
                }
                else if (c == Symbols.Solidus)
                {
                    tag.AddAttribute(_stringBuffer.ToString());
                    _stringBuffer.Clear();
                    return TagSelfClosing(tag);
                }
                else if (c.IsUppercaseAscii())
                {
                    _stringBuffer.Append(Char.ToLower(c));
                }
                else if (c == Symbols.DoubleQuote || c == Symbols.SingleQuote || c == Symbols.LessThan)
                {
                    RaiseErrorOccurred(HtmlParseError.AttributeNameInvalid);
                    _stringBuffer.Append(c);
                }
                else if(c == Symbols.Null)
                {
                    RaiseErrorOccurred(HtmlParseError.Null);
                    _stringBuffer.Append(Symbols.Replacement);
                }
                else if (c != Symbols.EndOfFile)
                {
                    _stringBuffer.Append(c);
                }
                else
                {
                    return NewEof();
                }
            }
        }

        /// <summary>
        /// See 8.2.4.36 After attribute name state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        HtmlToken AttributeAfterName(HtmlTagToken tag)
        {
            var c = SkipSpaces();

            if (c == Symbols.GreaterThan)
            {
                return EmitTag(tag);
            }
            else if (c == Symbols.Equality)
            {
                return AttributeBeforeValue(tag);
            }
            else if (c == Symbols.Solidus)
            {
                return TagSelfClosing(tag);
            }
            else if (c.IsUppercaseAscii())
            {
                _stringBuffer.Append(Char.ToLower(c));
                return AttributeName(tag);
            }
            else if (c == Symbols.DoubleQuote || c == Symbols.SingleQuote || c == Symbols.LessThan)
            {
                RaiseErrorOccurred(HtmlParseError.AttributeNameInvalid);
                _stringBuffer.Append(c);
                return AttributeName(tag);
            }
            else if (c == Symbols.Null)
            {
                RaiseErrorOccurred(HtmlParseError.Null);
                _stringBuffer.Append(Symbols.Replacement);
                return AttributeName(tag);
            }
            else if (c != Symbols.EndOfFile)
            {
                _stringBuffer.Append(c);
                return AttributeName(tag);
            }
            else
            {
                return NewEof();
            }
        }

        /// <summary>
        /// See 8.2.4.37 Before attribute value state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        HtmlToken AttributeBeforeValue(HtmlTagToken tag)
        {
            var c = SkipSpaces();

            if (c == Symbols.DoubleQuote)
            {
                return AttributeDoubleQuotedValue(tag);
            }
            else if (c == Symbols.SingleQuote)
            {
                return AttributeSingleQuotedValue(tag);
            }
            else if (c == Symbols.Ampersand)
            {
                return AttributeUnquotedValue(c, tag);
            }
            else if (c == Symbols.GreaterThan)
            {
                RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
                return EmitTag(tag);
            }
            else if (c == Symbols.LessThan || c == Symbols.Equality || c == Symbols.CurvedQuote)
            {
                RaiseErrorOccurred(HtmlParseError.AttributeValueInvalid);
                _stringBuffer.Append(c);
                return AttributeUnquotedValue(GetNext(), tag);
            }
            else if (c == Symbols.Null)
            {
                RaiseErrorOccurred(HtmlParseError.Null);
                _stringBuffer.Append(Symbols.Replacement);
                return AttributeUnquotedValue(GetNext(), tag);
            }
            else if (c != Symbols.EndOfFile)
            {
                _stringBuffer.Append(c);
                return AttributeUnquotedValue(GetNext(), tag);
            }
            else
            {
                return NewEof();
            }
        }

        /// <summary>
        /// See 8.2.4.38 Attribute value (double-quoted) state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        HtmlToken AttributeDoubleQuotedValue(HtmlTagToken tag)
        {
            while (true)
            {
                var c = GetNext();

                if (c == Symbols.DoubleQuote)
                {
                    tag.SetAttributeValue(_stringBuffer.ToString());
                    _stringBuffer.Clear();
                    return AttributeAfterValue(tag);
                }
                else if (c == Symbols.Ampersand)
                {
                    var value = CharacterReference(GetNext(), Symbols.DoubleQuote);

                    if (value == null)
                        _stringBuffer.Append(Symbols.Ampersand);
                    else
                        _stringBuffer.Append(value);
                }
                else if (c == Symbols.Null)
                {
                    RaiseErrorOccurred(HtmlParseError.Null);
                    _stringBuffer.Append(Symbols.Replacement);
                }
                else if (c != Symbols.EndOfFile)
                {
                    _stringBuffer.Append(c);
                }
                else
                {
                    return NewEof();
                }
            }
        }

        /// <summary>
        /// See 8.2.4.39 Attribute value (single-quoted) state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        HtmlToken AttributeSingleQuotedValue(HtmlTagToken tag)
        {
            while (true)
            {
                var c = GetNext();

                if (c == Symbols.SingleQuote)
                {
                    tag.SetAttributeValue(_stringBuffer.ToString());
                    _stringBuffer.Clear();
                    return AttributeAfterValue(tag);
                }
                else if (c == Symbols.Ampersand)
                {
                    var value = CharacterReference(GetNext(), Symbols.SingleQuote);

                    if (value == null)
                        _stringBuffer.Append(Symbols.Ampersand);
                    else
                        _stringBuffer.Append(value);
                }
                else if (c == Symbols.Null)
                {
                    RaiseErrorOccurred(HtmlParseError.Null);
                    _stringBuffer.Append(Symbols.Replacement);
                }
                else if (c != Symbols.EndOfFile)
                {
                    _stringBuffer.Append(c);
                }
                else
                {
                    return NewEof();
                }
            }
        }

        /// <summary>
        /// See 8.2.4.40 Attribute value (unquoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        HtmlToken AttributeUnquotedValue(Char c, HtmlTagToken tag)
        {
            while (true)
            {
                if (c == Symbols.GreaterThan)
                {
                    tag.SetAttributeValue(_stringBuffer.ToString());
                    _stringBuffer.Clear();
                    return EmitTag(tag);
                }
                else if (c.IsSpaceCharacter())
                {
                    tag.SetAttributeValue(_stringBuffer.ToString());
                    _stringBuffer.Clear();
                    return AttributeBeforeName(tag);
                }
                else if (c == Symbols.Ampersand)
                {
                    var value = CharacterReference(GetNext(), Symbols.GreaterThan);

                    if (value == null)
                        _stringBuffer.Append(Symbols.Ampersand);
                    else
                        _stringBuffer.Append(value);
                }
                else if (c == Symbols.Null)
                {
                    RaiseErrorOccurred(HtmlParseError.Null);
                    _stringBuffer.Append(Symbols.Replacement);
                }
                else if (c == Symbols.DoubleQuote || c == Symbols.SingleQuote || c == Symbols.LessThan || c == Symbols.Equality || c == Symbols.CurvedQuote)
                {
                    RaiseErrorOccurred(HtmlParseError.AttributeValueInvalid);
                    _stringBuffer.Append(c);
                }
                else if (c != Symbols.EndOfFile)
                {
                    _stringBuffer.Append(c);
                }
                else
                {
                    return NewEof();
                }

                c = GetNext();
            }
        }

        /// <summary>
        /// See 8.2.4.42 After attribute value (quoted) state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        HtmlToken AttributeAfterValue(HtmlTagToken tag)
        {
            var c = GetNext();

            if (c == Symbols.GreaterThan)
                return EmitTag(tag);
            else if (c.IsSpaceCharacter())
                return AttributeBeforeName(tag);
            else if (c == Symbols.Solidus)
                return TagSelfClosing(tag);
            else if (c == Symbols.EndOfFile)
                return NewEof();

            RaiseErrorOccurred(HtmlParseError.AttributeNameExpected);
            Back();
            return AttributeBeforeName(tag);
        }

        #endregion

        #region Script

        /// <summary>
        /// See 8.2.4.6 Script data state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptData(Char c)
        {
            while (true)
            {
                switch (c)
                {
                    case Symbols.Null:
                        RaiseErrorOccurred(HtmlParseError.Null);
                        _stringBuffer.Append(Symbols.Replacement);
                        break;

                    case Symbols.LessThan:
                        return ScriptDataLt(GetNext());

                    case Symbols.EndOfFile:
                        Back();
                        return NewCharacter();

                    default:
                        _stringBuffer.Append(c);
                        break;
                }

                c = GetNext();
            }
        }

        /// <summary>
        /// See 8.2.4.17 Script data less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataLt(Char c)
        {
            _stringBuffer.Append(Symbols.LessThan);

            if (c == Symbols.Solidus)
            {
                // See 8.2.4.18 Script data end tag open state
                c = GetNext();
                var offset = _stringBuffer.Append(Symbols.Solidus).Length;

                if (c.IsLetter())
                {
                    _stringBuffer.Append(c);
                    return ScriptDataNameEndTag(NewTagClose(), offset);
                }
            }
            else if (c == Symbols.ExclamationMark)
            {
                // See 8.2.4.20 Script data escape start state
                _stringBuffer.Append(Symbols.ExclamationMark);
                c = GetNext();

                if (c == Symbols.Minus)
                    return ScriptDataEscapeDashLt(GetNext());
            }

            return ScriptData(c);
        }

        /// <summary>
        /// See 8.2.4.19 Script data end tag name state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        /// <param name="offset">The tag name's offset.</param>
        HtmlToken ScriptDataNameEndTag(HtmlTagToken tag, Int32 offset)
        {
            var length = _lastStartTag.Length;

            while (true)
            {
                var c = GetNext();
                var isspace = c.IsSpaceCharacter();
                var isclosed = c == Symbols.GreaterThan;
                var isslash = c == Symbols.Solidus;
                var hasLength = _stringBuffer.Length - offset == length;

                if (hasLength && (isspace || isclosed || isslash))
                {
                    var name = _stringBuffer.ToString(offset, length);

                    if (name.Equals(_lastStartTag, StringComparison.OrdinalIgnoreCase))
                    {
                        if (offset > 2)
                        {
                            Back(3 + length);
                            _stringBuffer.Remove(offset - 2, length + 2);
                            return NewCharacter();
                        }

                        _stringBuffer.Clear();

                        if (isspace)
                        {
                            tag.Name = _lastStartTag;
                            return AttributeBeforeName(tag);
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
                
                if (!c.IsLetter())
                {
                    return ScriptData(c);
                }

                _stringBuffer.Append(c);
            }
        }

        /// <summary>
        /// See 8.2.4.21 Script data escape start dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapeDashLt(Char c)
        {
            _stringBuffer.Append(Symbols.Minus);

            if (c == Symbols.Minus)
            {
                _stringBuffer.Append(Symbols.Minus);
                return ScriptDataEscapedDashDash();
            }

            return ScriptData(c);
        }

        /// <summary>
        /// See 8.2.4.22 Script data escaped state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscaped(Char c)
        {
            while (true)
            {
                switch (c)
                {
                    case Symbols.Minus:
                        _stringBuffer.Append(Symbols.Minus);
                        return ScriptDataEscapedDash(GetNext());
                    case Symbols.LessThan:
                        return ScriptDataEscapedLT(GetNext());
                    case Symbols.Null:
                        RaiseErrorOccurred(HtmlParseError.Null);
                        _stringBuffer.Append(Symbols.Replacement);
                        break;
                    case Symbols.EndOfFile:
                        Back();
                        return NewCharacter();
                    default:
                        return ScriptData(c);
                }

                c = GetNext();
            }
        }

        /// <summary>
        /// See 8.2.4.23 Script data escaped dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedDash(Char c)
        {
            switch (c)
            {
                case Symbols.Minus:
                    _stringBuffer.Append(Symbols.Minus);
                    return ScriptDataEscapedDashDash();
                case Symbols.LessThan:
                    return ScriptDataEscapedLT(GetNext());
                case Symbols.Null:
                    RaiseErrorOccurred(HtmlParseError.Null);
                    _stringBuffer.Append(Symbols.Replacement);
                    break;
                case Symbols.EndOfFile:
                    Back();
                    return NewCharacter();
                default:
                    _stringBuffer.Append(c);
                    break;
            }

            return ScriptDataEscaped(GetNext());
        }

        /// <summary>
        /// See 8.2.4.24 Script data escaped dash dash state
        /// </summary>
        HtmlToken ScriptDataEscapedDashDash()
        {
            while (true)
            {
                var c = GetNext();

                switch (c)
                {
                    case Symbols.Minus:
                        _stringBuffer.Append(Symbols.Minus);
                        break;
                    case Symbols.LessThan:
                        return ScriptDataEscapedLT(GetNext());
                    case Symbols.GreaterThan:
                        _stringBuffer.Append(Symbols.GreaterThan);
                        return ScriptData(GetNext());
                    case Symbols.Null:
                        RaiseErrorOccurred(HtmlParseError.Null);
                        _stringBuffer.Append(Symbols.Replacement);
                        return ScriptDataEscaped(GetNext());
                    case Symbols.EndOfFile:
                        return NewCharacter();
                    default:
                        _stringBuffer.Append(c);
                        return ScriptDataEscaped(GetNext());
                }
            }
        }

        /// <summary>
        /// See 8.2.4.25 Script data escaped less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedLT(Char c)
        {
            if (c == Symbols.Solidus)
                return ScriptDataEscapedEndTag(GetNext());

            if (c.IsLetter())
            {
                var offset = _stringBuffer.Append(Symbols.LessThan).Length;
                _stringBuffer.Append(c);
                return ScriptDataStartDoubleEscape(offset);
            }
            else
            {
                _stringBuffer.Append(Symbols.LessThan);
                return ScriptDataEscaped(c);
            }
        }

        /// <summary>
        /// See 8.2.4.26 Script data escaped end tag open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedEndTag(Char c)
        {
            var offset = _stringBuffer.Append(Symbols.LessThan).Append(Symbols.Solidus).Length;

            if (c.IsLetter())
            {
                _stringBuffer.Append(c);
                return ScriptDataEscapedNameEndTag(NewTagClose(), offset);
            }
            else
            {
                return ScriptDataEscaped(c);
            }
        }

        /// <summary>
        /// See 8.2.4.27 Script data escaped end tag name state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        /// <param name="offset">The tag name's offset.</param>
        HtmlToken ScriptDataEscapedNameEndTag(HtmlTagToken tag, Int32 offset)
        {
            var length = Tags.Script.Length;

            while (true)
            {
                var c = GetNext();
                var hasLength = _stringBuffer.Length - offset == length;

                if (hasLength && (c == Symbols.Solidus || c == Symbols.GreaterThan || c.IsSpaceCharacter()) && 
                    _stringBuffer.ToString(offset, length).Equals(Tags.Script, StringComparison.OrdinalIgnoreCase))
                {
                    Back(length + 3);
                    _stringBuffer.Remove(offset - 2, length + 2);
                    return NewCharacter();
                }
                else if (!c.IsLetter())
                {
                    return ScriptDataEscaped(c);
                }

                _stringBuffer.Append(c);
            }
        }

        /// <summary>
        /// See 8.2.4.28 Script data double escape start state
        /// </summary>
        /// <param name="offset">The tag name's offset.</param>
        HtmlToken ScriptDataStartDoubleEscape(Int32 offset)
        {
            var length = Tags.Script.Length;

            while (true)
            {
                var c = GetNext();
                var hasLength = _stringBuffer.Length - offset == length;

                if (hasLength && (c == Symbols.Solidus || c == Symbols.GreaterThan || c.IsSpaceCharacter()))
                {
                    var isscript = _stringBuffer.ToString(offset, length).Equals(Tags.Script, StringComparison.OrdinalIgnoreCase);
                    _stringBuffer.Append(c);
                    return isscript ? ScriptDataEscapedDouble(GetNext()) : ScriptDataEscaped(GetNext());
                }
                else if (c.IsLetter())
                {
                    _stringBuffer.Append(c);
                }
                else
                {
                    return ScriptDataEscaped(c);
                }
            }
        }

        /// <summary>
        /// See 8.2.4.29 Script data double escaped state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedDouble(Char c)
        {
            while (true)
            {
                switch (c)
                {
                    case Symbols.Minus:
                        _stringBuffer.Append(Symbols.Minus);
                        return ScriptDataEscapedDoubleDash(GetNext());
                    case Symbols.LessThan:
                        _stringBuffer.Append(Symbols.LessThan);
                        return ScriptDataEscapedDoubleLT(GetNext());
                    case Symbols.Null:
                        RaiseErrorOccurred(HtmlParseError.Null);
                        _stringBuffer.Append(Symbols.Replacement);
                        break;
                    case Symbols.EndOfFile:
                        RaiseErrorOccurred(HtmlParseError.EOF);
                        Back();
                        return NewCharacter();
                }

                _stringBuffer.Append(c);
                c = GetNext();
            }
        }

        /// <summary>
        /// See 8.2.4.30 Script data double escaped dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedDoubleDash(Char c)
        {
            switch (c)
            {
                case Symbols.Minus:
                    _stringBuffer.Append(Symbols.Minus);
                    return ScriptDataEscapedDoubleDashDash();
                case Symbols.LessThan:
                    _stringBuffer.Append(Symbols.LessThan);
                    return ScriptDataEscapedDoubleLT(GetNext());
                case Symbols.Null:
                    RaiseErrorOccurred(HtmlParseError.Null);
                    c = Symbols.Replacement;
                    break;
                case Symbols.EndOfFile:
                    RaiseErrorOccurred(HtmlParseError.EOF);
                    Back();
                    return NewCharacter();
            }

            return ScriptDataEscapedDouble(c);
        }

        /// <summary>
        /// See 8.2.4.31 Script data double escaped dash dash state
        /// </summary>
        HtmlToken ScriptDataEscapedDoubleDashDash()
        {
            while (true)
            {
                var c = GetNext();

                switch (c)
                {
                    case Symbols.Minus:
                        _stringBuffer.Append(Symbols.Minus);
                        break;
                    case Symbols.LessThan:
                        _stringBuffer.Append(Symbols.LessThan);
                        return ScriptDataEscapedDoubleLT(GetNext());
                    case Symbols.GreaterThan:
                        _stringBuffer.Append(Symbols.GreaterThan);
                        return ScriptData(GetNext());
                    case Symbols.Null:
                        RaiseErrorOccurred(HtmlParseError.Null);
                        _stringBuffer.Append(Symbols.Replacement);
                        return ScriptDataEscapedDouble(GetNext());
                    case Symbols.EndOfFile:
                        RaiseErrorOccurred(HtmlParseError.EOF);
                        Back();
                        return NewCharacter();
                    default:
                        _stringBuffer.Append(c);
                        return ScriptDataEscapedDouble(GetNext());
                }
            }
        }

        /// <summary>
        /// See 8.2.4.32 Script data double escaped less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedDoubleLT(Char c)
        {
            if (c == Symbols.Solidus)
            {
                var offset = _stringBuffer.Append(Symbols.Solidus).Length;
                return ScriptDataEndDoubleEscape(offset);
            }

            return ScriptDataEscapedDouble(c);
        }

        /// <summary>
        /// See 8.2.4.33 Script data double escape end state
        /// </summary>
        /// <param name="offset">The tag name's offset.</param>
        HtmlToken ScriptDataEndDoubleEscape(Int32 offset)
        {
            var length = Tags.Script.Length;

            while (true)
            {
                var c = GetNext();
                var hasLength = _stringBuffer.Length - offset == length;

                if (hasLength && (c.IsSpaceCharacter() || c == Symbols.Solidus || c == Symbols.GreaterThan))
                {
                    var isscript = _stringBuffer.ToString(offset, length).Equals(Tags.Script, StringComparison.OrdinalIgnoreCase);
                    _stringBuffer.Append(c);
                    return isscript ? ScriptDataEscaped(GetNext()) : ScriptDataEscapedDouble(GetNext());
                }
                else if (c.IsLetter())
                {
                    _stringBuffer.Append(c);
                }
                else
                {
                    return ScriptDataEscapedDouble(c);
                }
            }
        }

        #endregion

        #region Tokens

        HtmlToken NewCharacter()
        {
            var content = _stringBuffer.ToString();
            _stringBuffer.Clear();
            return new HtmlToken(HtmlTokenType.Character, _position, content);
        }

        HtmlToken NewComment()
        {
            var content = _stringBuffer.ToString();
            _stringBuffer.Clear();
            return new HtmlToken(HtmlTokenType.Comment, _position, content);
        }

        HtmlToken NewEof()
        {
            return new HtmlToken(HtmlTokenType.EndOfFile, _position);
        }

        HtmlDoctypeToken NewDoctype(Boolean quirksForced)
        {
            return new HtmlDoctypeToken(quirksForced, _position);
        }

        HtmlTagToken NewTagOpen()
        {
            return new HtmlTagToken(HtmlTokenType.StartTag, _position);
        }

        HtmlTagToken NewTagClose()
        {
            return new HtmlTagToken(HtmlTokenType.EndTag, _position);
        }

        #endregion

        #region Helpers

        HtmlToken CreateIfAppropriate(Char c)
        {
            var isspace = c.IsSpaceCharacter();
            var isclosed = c == Symbols.GreaterThan;
            var isslash = c == Symbols.Solidus;
            var hasLength = _stringBuffer.Length == _lastStartTag.Length;

            if (hasLength && (isspace || isclosed || isslash) && 
                _stringBuffer.ToString().Equals(_lastStartTag, StringComparison.Ordinal))
            {
                var tag = NewTagClose();
                _stringBuffer.Clear();

                if (isspace)
                {
                    tag.Name = _lastStartTag;
                    return AttributeBeforeName(tag);
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

        HtmlToken EmitTag(HtmlTagToken tag)
        {
            var attributes = tag.Attributes;
            _state = HtmlParseMode.PCData;

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
                        RaiseErrorOccurred(HtmlParseError.EndTagCannotBeSelfClosed, tag.Position);

                    if (attributes.Count != 0)
                        RaiseErrorOccurred(HtmlParseError.EndTagCannotHaveAttributes, tag.Position);

                    break;
            }

            return tag;
        }

        #endregion
    }
}
