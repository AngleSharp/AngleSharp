namespace AngleSharp.Parser.Html
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;

    /// <summary>
    /// Performs the tokenization of the source code. Follows the tokenization algorithm at:
    /// http://www.w3.org/html/wg/drafts/html/master/syntax.html
    /// </summary>
    [DebuggerStepThrough]
    sealed class HtmlTokenizer : BaseTokenizer
    {
        #region Fields

        readonly StringBuilder _buffer;

        Boolean _acceptsCharacterData;
        String _lastStartTag;
        HtmlParseMode _state;
        HtmlToken _buffered;

        #endregion

        #region ctor

        /// <summary>
        /// See 8.2.4 Tokenization
        /// </summary>
        /// <param name="source">The source code manager.</param>
        public HtmlTokenizer(ITextSource source)
            : base(source)
        {
            _state = HtmlParseMode.PCData;
            _acceptsCharacterData = false;
            _buffer = new StringBuilder();
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
        /// Gets the next available token.
        /// </summary>
        /// <returns>The next available token.</returns>
        public HtmlToken Get()
        {
            var token = _buffered;

            if (token != null)
            {
                _buffered = null;
                return token;
            }

            var current = Next;

            if (IsEnded) 
                return HtmlToken.EOF;

            switch (_state)
            {
                case HtmlParseMode.PCData:
                    token = Data(current);
                    break;

                case HtmlParseMode.RCData:
                    token = RCData(current);
                    break;

                case HtmlParseMode.Plaintext:
                    token = Plaintext(current);
                    break;

                case HtmlParseMode.Rawtext:
                    token = Rawtext(current);
                    break;

                case HtmlParseMode.Script:
                    token = ScriptData(current);
                    break;
            }

            if (_buffer.Length > 0)
            {
                _buffered = token;
                token = HtmlToken.Character(_buffer.ToString());
                _buffer.Clear();
            }

            return token;
        }

        #endregion

        #region General

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
                    case Specification.Null:
                        RaiseErrorOccurred(ErrorCode.Null);
                        _buffer.Append(Specification.Replacement);
                        break;

                    case Specification.EndOfFile:
                        return HtmlToken.EOF;

                    default:
                        _buffer.Append(c);
                        break;
                }

                c = Next;
            }
        }

        /// <summary>
        /// See 8.2.4.1 Data state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken Data(Char c)
        {
            while (true)
            {
                switch (c)
                {
                    case Specification.Ampersand:
                        var value = CharacterReference(Next);

                        if (value == null)
                            _buffer.Append(Specification.Ampersand);

                        _buffer.Append(value);
                        break;

                    case Specification.LessThan:
                        return TagOpen(Next);

                    case Specification.Null:
                        RaiseErrorOccurred(ErrorCode.Null);
                        return Data(Next);

                    case Specification.EndOfFile:
                        return HtmlToken.EOF;

                    default:
                        _buffer.Append(c);
                        break;
                }

                c = Next;
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
            while (true)
            {
                switch (c)
                {
                    case Specification.Ampersand:
                        var value = CharacterReference(Next);

                        if (value == null)
                            _buffer.Append(Specification.Ampersand);

                        _buffer.Append(value);
                        break;

                    case Specification.LessThan:
                        return RCDataLT(Next);

                    case Specification.Null:
                        RaiseErrorOccurred(ErrorCode.Null);
                        _buffer.Append(Specification.Replacement);
                        break;

                    case Specification.EndOfFile:
                        return HtmlToken.EOF;

                    default:
                        _buffer.Append(c);
                        break;
                }

                c = Next;
            }
        }

        /// <summary>
        /// See 8.2.4.11 RCDATA less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken RCDataLT(Char c)
        {
            if (c == Specification.Solidus)
            {
                _stringBuffer.Clear();
                return RCDataEndTag(Next);
            }

            _buffer.Append(Specification.LessThan);
            return RCData(c);
        }

        /// <summary>
        /// See 8.2.4.12 RCDATA end tag open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken RCDataEndTag(Char c)
        {
            if (c.IsUppercaseAscii())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(Char.ToLower(c));
                return RCDataNameEndTag(Next, HtmlToken.CloseTag());
            }
            else if (c.IsLowercaseAscii())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return RCDataNameEndTag(Next, HtmlToken.CloseTag());
            }

            _buffer.Append(Specification.LessThan).Append(Specification.Solidus);
            return RCData(c);
        }

        /// <summary>
        /// See 8.2.4.13 RCDATA end tag name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken RCDataNameEndTag(Char c, HtmlTagToken tag)
        {
            var name = _stringBuffer.ToString();
            var appropriateTag = name == _lastStartTag;

            if (appropriateTag && c.IsSpaceCharacter())
            {
                tag.Name = name;
                return AttributeBeforeName(Next, tag);
            }
            else if (appropriateTag && c == Specification.Solidus)
            {
                tag.Name = name;
                return TagSelfClosing(Next, tag);
            }
            else if (appropriateTag && c == Specification.GreaterThan)
            {
                tag.Name = name;
                return EmitTag(tag);
            }
            else if (c.IsUppercaseAscii())
            {
                _stringBuffer.Append(Char.ToLower(c));
                return RCDataNameEndTag(Next, tag);
            }
            else if (c.IsLowercaseAscii())
            {
                _stringBuffer.Append(c);
                return RCDataNameEndTag(Next, tag);
            }

            _buffer.Append(Specification.LessThan).Append(Specification.Solidus);
            _buffer.Append(_stringBuffer.ToString());
            return RCData(c);
        }

        #endregion

        #region Rawtext

        /// <summary>
        /// See 8.2.4.5 RAWTEXT state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken Rawtext(Char c)
        {
            while (true)
            {
                switch (c)
                {
                    case Specification.LessThan:
                        return RawtextLT(Next);

                    case Specification.Null:
                        RaiseErrorOccurred(ErrorCode.Null);
                        _buffer.Append(Specification.Replacement);
                        break;

                    case Specification.EndOfFile:
                        return HtmlToken.EOF;

                    default:
                        _buffer.Append(c);
                        break;
                }

                c = Next;
            }
        }

        /// <summary>
        /// See 8.2.4.14 RAWTEXT less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken RawtextLT(Char c)
        {
            if (c == Specification.Solidus)
            {
                _stringBuffer.Clear();
                return RawtextEndTag(Next);
            }

            _buffer.Append(Specification.LessThan);
            return Rawtext(c);
        }

        /// <summary>
        /// See 8.2.4.15 RAWTEXT end tag open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken RawtextEndTag(Char c)
        {
            if (c.IsUppercaseAscii())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(Char.ToLower(c));
                return RawtextNameEndTag(Next, HtmlToken.CloseTag());
            }
            else if (c.IsLowercaseAscii())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return RawtextNameEndTag(Next, HtmlToken.CloseTag());
            }

            _buffer.Append(Specification.LessThan).Append(Specification.Solidus);
            return Rawtext(c);
        }

        /// <summary>
        /// See 8.2.4.16 RAWTEXT end tag name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken RawtextNameEndTag(Char c, HtmlTagToken tag)
        {
            var name = _stringBuffer.ToString();
            var appropriateTag = name == _lastStartTag;

            if (appropriateTag && c.IsSpaceCharacter())
            {
                tag.Name = name;
                return AttributeBeforeName(Next, tag);
            }
            else if (appropriateTag && c == Specification.Solidus)
            {
                tag.Name = name;
                return TagSelfClosing(Next, tag);
            }
            else if (appropriateTag && c == Specification.GreaterThan)
            {
                tag.Name = name;
                return EmitTag(tag);
            }
            else if (c.IsUppercaseAscii())
            {
                _stringBuffer.Append(Char.ToLower(c));
                return RawtextNameEndTag(Next, tag);
            }
            else if (c.IsLowercaseAscii())
            {
                _stringBuffer.Append(c);
                return RawtextNameEndTag(Next, tag);
            }

            _buffer.Append(Specification.LessThan).Append(Specification.Solidus);
            _buffer.Append(_stringBuffer.ToString());
            return Rawtext(c);
        }

        #endregion

        #region CDATA

        /// <summary>
        /// See 8.2.4.68 CDATA section state
        /// </summary>
        HtmlToken CData(Char c)
        {
            _stringBuffer.Clear();

            while (true)
            {
                if (c == Specification.EndOfFile)
                {
                    Back();
                    break;
                }
                else if (c == Specification.SquareBracketClose && ContinuesWith("]]>"))
                {
                    Advance(2);
                    break;
                }

                _stringBuffer.Append(c);
                c = Next;
            }

            return HtmlToken.Character(_stringBuffer.ToString());
        }

        /// <summary>
        /// See 8.2.4.69 Tokenizing character references
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="allowedCharacter">The additionally allowed character if there is one.</param>
        String CharacterReference(Char c, Char allowedCharacter = Specification.Null)
        {
            if (c.IsSpaceCharacter() || c == Specification.LessThan || c == Specification.EndOfFile || c == Specification.Ampersand || c == allowedCharacter)
            {
                Back();
                return null;
            }

            if (c == Specification.Num)
            {
                var exp = 10;
                var basis = 1;
                var num = 0;
                var nums = new List<Int32>();
                c = Next;
                var isHex = c == 'x' || c == 'X';

                if (isHex)
                {
                    exp = 16;

                    while ((c = Next).IsHex())
                        nums.Add(c.FromHex());
                }
                else
                {
                    while (c.IsDigit())
                    {
                        nums.Add(c.FromHex());
                        c = Next;
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

                    RaiseErrorOccurred(ErrorCode.CharacterReferenceWrongNumber);
                    return null;
                }

                if (c != Specification.Semicolon)
                {
                    RaiseErrorOccurred(ErrorCode.CharacterReferenceSemicolonMissing);
                    Back();
                }

                if (Entities.IsInCharacterTable(num))
                {
                    RaiseErrorOccurred(ErrorCode.CharacterReferenceInvalidCode);
                    return Entities.GetSymbolFromTable(num);
                }

                if (Entities.IsInvalidNumber(num))
                {
                    RaiseErrorOccurred(ErrorCode.CharacterReferenceInvalidNumber);
                    return Specification.Replacement.ToString();
                }

                if (Entities.IsInInvalidRange(num))
                    RaiseErrorOccurred(ErrorCode.CharacterReferenceInvalidRange);

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
                    if (chr == Specification.Semicolon || !chr.IsName())
                        break;

                    reference[index++] = chr;
                    var value = new String(reference, 0, index);
                    chr = Next;
                    consumed++;
                    value = chr == Specification.Semicolon ? Entities.GetSymbol(value) : Entities.GetSymbolWithoutSemicolon(value);

                    if (value != null)
                    {
                        consumed = 0;
                        last = value;
                    }
                }
                while (!IsEnded && index < 31);

                Back(consumed);
                chr = Current;

                if (chr != Specification.Semicolon)
                {
                    if (allowedCharacter != Specification.Null && (chr == Specification.Equality || chr.IsAlphanumericAscii()))
                    {
                        if (chr == Specification.Equality)
                            RaiseErrorOccurred(ErrorCode.CharacterReferenceAttributeEqualsFound);

                        InsertionPoint = start;
                        return null;
                    }

                    Back();
                    RaiseErrorOccurred(ErrorCode.CharacterReferenceNotTerminated);
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
            if (c == Specification.ExclamationMark)
            {
                return MarkupDeclaration(Next);
            }
            else if (c == Specification.Solidus)
            {
                return TagEnd(Next);
            }
            else if (c.IsUppercaseAscii())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(Char.ToLower(c));
                return TagName(Next, HtmlToken.OpenTag());
            }
            else if (c.IsLowercaseAscii())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return TagName(Next, HtmlToken.OpenTag());
            }
            else if (c == Specification.QuestionMark)
            {
                RaiseErrorOccurred(ErrorCode.BogusComment);
                return BogusComment(c);
            }

            _state = HtmlParseMode.PCData;
            RaiseErrorOccurred(ErrorCode.AmbiguousOpenTag);
            _buffer.Append(Specification.LessThan);
            return Data(c);
        }

        /// <summary>
        /// See 8.2.4.9 End tag open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken TagEnd(Char c)
        {
            if (c.IsUppercaseAscii())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(Char.ToLower(c));
                return TagName(Next, HtmlToken.CloseTag());
            }
            else if (c.IsLowercaseAscii())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return TagName(Next, HtmlToken.CloseTag());
            }
            else if (c == Specification.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return Data(Next);
            }
            else if (c == Specification.EndOfFile)
            {
                Back();
                RaiseErrorOccurred(ErrorCode.EOF);
                _buffer.Append(Specification.LessThan).Append(Specification.Solidus);
                return HtmlToken.EOF;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.BogusComment);
                return BogusComment(c);
            }
        }

        /// <summary>
        /// See 8.2.4.10 Tag name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken TagName(Char c, HtmlTagToken tag)
        {
            while (true)
            {
                if (c.IsSpaceCharacter())
                {
                    tag.Name = _stringBuffer.ToString();
                    return AttributeBeforeName(Next, tag);
                }
                else if (c == Specification.Solidus)
                {
                    tag.Name = _stringBuffer.ToString();
                    return TagSelfClosing(Next, tag);
                }
                else if (c == Specification.GreaterThan)
                {
                    tag.Name = _stringBuffer.ToString();
                    return EmitTag(tag);
                }
                else if (c == Specification.Null)
                {
                    RaiseErrorOccurred(ErrorCode.Null);
                    _stringBuffer.Append(Specification.Replacement);
                }
                else if (c == Specification.EndOfFile)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    return HtmlToken.EOF;
                }
                else if (c.IsUppercaseAscii())
                    _stringBuffer.Append(Char.ToLower(c));
                else
                    _stringBuffer.Append(c);

                c = Next;
            }
        }

        /// <summary>
        /// See 8.2.4.43 Self-closing start tag state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken TagSelfClosing(Char c, HtmlTagToken tag)
        {
            if (c == Specification.GreaterThan)
            {
                tag.IsSelfClosing = true;
                return EmitTag(tag);
            }
            else if (c == Specification.EndOfFile)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return HtmlToken.EOF;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.ClosingSlashMisplaced);
                return AttributeBeforeName(c, tag);
            }
        }

        /// <summary>
        /// See 8.2.4.45 Markup declaration open state
        /// </summary>
        HtmlToken MarkupDeclaration(Char c)
        {
            if (ContinuesWith("--"))
            {
                Advance();
                return CommentStart(Next);
            }
            else if (ContinuesWith(Tags.Doctype))
            {
                Advance(6);
                return Doctype(Next);
            }
            else if (_acceptsCharacterData && ContinuesWith("[CDATA[", ignoreCase: false))
            {
                Advance(6);
                return CData(Next);
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.UndefinedMarkupDeclaration);
                return BogusComment(c);
            }
        }

        #endregion

        #region Comments

        /// <summary>
        /// See 8.2.4.44 Bogus comment state
        /// </summary>
        HtmlToken BogusComment(Char c)
        {
            _stringBuffer.Clear();

            while(true)
            {

                if (c == Specification.GreaterThan)
                    break;
                else if (c == Specification.EndOfFile)
                {
                    Back();
                    break;
                }
                else if (c == Specification.Null)
                    _stringBuffer.Append(Specification.Replacement);
                else
                    _stringBuffer.Append(c);

                c = Next;
            }

            _state = HtmlParseMode.PCData;
            return HtmlToken.Comment(_stringBuffer.ToString());
        }

        /// <summary>
        /// See 8.2.4.46 Comment start state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlCommentToken CommentStart(Char c)
        {
            _stringBuffer.Clear();

            if (c == Specification.Minus)
                return CommentDashStart(Next);
            else if (c == Specification.Null)
            {
                RaiseErrorOccurred(ErrorCode.Null);
                _stringBuffer.Append(Specification.Replacement);
                return Comment(Next);
            }
            else if (c == Specification.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return HtmlToken.Comment(_stringBuffer.ToString());
            }
            else if (c == Specification.EndOfFile)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                Back();
                return HtmlToken.Comment(_stringBuffer.ToString());
            }
            else
            {
                _stringBuffer.Append(c);
                return Comment(Next);
            }
        }

        /// <summary>
        /// See 8.2.4.47 Comment start dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlCommentToken CommentDashStart(Char c)
        {
            if (c == Specification.Minus)
                return CommentEnd(Next);
            else if (c == Specification.Null)
            {
                RaiseErrorOccurred(ErrorCode.Null);
                _stringBuffer.Append(Specification.Minus);
                _stringBuffer.Append(Specification.Replacement);
                return Comment(Next);
            }
            else if (c == Specification.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return HtmlToken.Comment(_stringBuffer.ToString());
            }
            else if (c == Specification.EndOfFile)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                Back();
                return HtmlToken.Comment(_stringBuffer.ToString());
            }

            _stringBuffer.Append(Specification.Minus);
            _stringBuffer.Append(c);
            return Comment(Next);
        }

        /// <summary>
        /// See 8.2.4.48 Comment state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlCommentToken Comment(Char c)
        {
            while (true)
            {
                if (c == Specification.Minus)
                {
                    var result = CommentDashEnd(Next);

                    if (result != null)
                        return result;
                }
                else if (c == Specification.EndOfFile)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    Back();
                    return HtmlToken.Comment(_stringBuffer.ToString());
                }
                else if (c == Specification.Null)
                {
                    RaiseErrorOccurred(ErrorCode.Null);
                    c = Specification.Replacement;
                    _stringBuffer.Append(c);
                }
                else
                    _stringBuffer.Append(c);

                c = Next;
            }
        }

        /// <summary>
        /// See 8.2.4.49 Comment end dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlCommentToken CommentDashEnd(Char c)
        {
            if (c == Specification.Minus)
                return CommentEnd(Next);
            else if (c == Specification.EndOfFile)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                Back();
                return HtmlToken.Comment(_stringBuffer.ToString());
            }
            else if (c == Specification.Null)
            {
                RaiseErrorOccurred(ErrorCode.Null);
                c = Specification.Replacement;
            }

            _stringBuffer.Append(Specification.Minus);
            _stringBuffer.Append(c);
            return null;
        }

        /// <summary>
        /// See 8.2.4.50 Comment end state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlCommentToken CommentEnd(Char c)
        {
            while (true)
            {
                if (c == Specification.GreaterThan)
                {
                    _state = HtmlParseMode.PCData;
                    return HtmlToken.Comment(_stringBuffer.ToString());
                }
                else if (c == Specification.Null)
                {
                    RaiseErrorOccurred(ErrorCode.Null);
                    _stringBuffer.Append(Specification.Minus);
                    _stringBuffer.Append(Specification.Replacement);
                    return null;
                }
                else if (c == Specification.ExclamationMark)
                {
                    RaiseErrorOccurred(ErrorCode.CommentEndedWithEM);
                    return CommentBangEnd(Next);
                }
                else if (c == Specification.Minus)
                {
                    RaiseErrorOccurred(ErrorCode.CommentEndedWithDash);
                    _stringBuffer.Append(Specification.Minus);
                    c = Next;
                    continue;
                }
                else if (c == Specification.EndOfFile)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    Back();
                    return HtmlToken.Comment(_stringBuffer.ToString());
                }

                RaiseErrorOccurred(ErrorCode.CommentEndedUnexpected);
                _stringBuffer.Append(Specification.Minus);
                _stringBuffer.Append(Specification.Minus);
                _stringBuffer.Append(c);
                return null;
            }
        }

        /// <summary>
        /// See 8.2.4.51 Comment end bang state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlCommentToken CommentBangEnd(Char c)
        {
            if (c == Specification.Minus)
            {
                _stringBuffer.Append(Specification.Minus);
                _stringBuffer.Append(Specification.Minus);
                _stringBuffer.Append(Specification.ExclamationMark);
                return CommentDashEnd(Next);
            }
            else if (c == Specification.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
                return HtmlToken.Comment(_stringBuffer.ToString());
            }
            else if (c == Specification.Null)
            {
                RaiseErrorOccurred(ErrorCode.Null);
                _stringBuffer.Append(Specification.Minus);
                _stringBuffer.Append(Specification.Minus);
                _stringBuffer.Append(Specification.ExclamationMark);
                _stringBuffer.Append(Specification.Replacement);
                return null;
            }
            else if (c == Specification.EndOfFile)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                Back();
                return HtmlToken.Comment(_stringBuffer.ToString());
            }

            _stringBuffer.Append(Specification.Minus);
            _stringBuffer.Append(Specification.Minus);
            _stringBuffer.Append(Specification.ExclamationMark);
            _stringBuffer.Append(c);
            return null;
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
                return DoctypeNameBefore(Next);
            else if (c == Specification.EndOfFile)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                Back();
                return HtmlToken.Doctype(true);
            }

            RaiseErrorOccurred(ErrorCode.DoctypeUnexpected);
            return DoctypeNameBefore(c);
        }

        /// <summary>
        /// See 8.2.4.53 Before DOCTYPE name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken DoctypeNameBefore(Char c)
        {
            while (c.IsSpaceCharacter())
                c = Next;

            if (c.IsUppercaseAscii())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(Char.ToLower(c));
                return DoctypeName(Next, HtmlToken.Doctype(false));
            }
            else if (c == Specification.Null)
            {
                RaiseErrorOccurred(ErrorCode.Null);
                _stringBuffer.Clear();
                _stringBuffer.Append(Specification.Replacement);
                return DoctypeName(Next, HtmlToken.Doctype(false));
            }
            else if (c == Specification.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return HtmlToken.Doctype(true);
            }
            else if (c == Specification.EndOfFile)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                Back();
                return HtmlToken.Doctype(true);
            }

            _stringBuffer.Clear();
            _stringBuffer.Append(c);
            return DoctypeName(Next, HtmlToken.Doctype(false));
        }

        /// <summary>
        /// See 8.2.4.54 DOCTYPE name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypeName(Char c, HtmlDoctypeToken doctype)
        {
            while (true)
            {
                if (c.IsSpaceCharacter())
                {
                    doctype.Name = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return DoctypeNameAfter(Next, doctype);
                }
                else if (c == Specification.GreaterThan)
                {
                    _state = HtmlParseMode.PCData;
                    doctype.Name = _stringBuffer.ToString();
                    return doctype;
                }
                else if (c.IsUppercaseAscii())
                    _stringBuffer.Append(Char.ToLower(c));
                else if (c == Specification.Null)
                {
                    RaiseErrorOccurred(ErrorCode.Null);
                    _stringBuffer.Append(Specification.Replacement);
                }
                else if (c == Specification.EndOfFile)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    Back();
                    doctype.IsQuirksForced = true;
                    doctype.Name = _stringBuffer.ToString();
                    return doctype;
                }
                else
                    _stringBuffer.Append(c);

                c = Next;
            }
        }

        /// <summary>
        /// See 8.2.4.55 After DOCTYPE name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypeNameAfter(Char c, HtmlDoctypeToken doctype)
        {
            while (c.IsSpaceCharacter())
                c = Next;

            if (c == Specification.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
                return doctype;
            }
            else if (c == Specification.EndOfFile)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                Back();
                doctype.IsQuirksForced = true;
                return doctype;
            }
            else if (ContinuesWith("public"))
            {
                Advance(5);
                return DoctypePublic(Next, doctype);
            }
            else if (ContinuesWith("system"))
            {
                Advance(5);
                return DoctypeSystem(Next, doctype);
            }

            RaiseErrorOccurred(ErrorCode.DoctypeUnexpectedAfterName);
            doctype.IsQuirksForced = true;
            return BogusDoctype(Next, doctype);
        }

        /// <summary>
        /// See 8.2.4.56 After DOCTYPE public keyword state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypePublic(Char c, HtmlDoctypeToken doctype)
        {
            if (c.IsSpaceCharacter())
            {
                return DoctypePublicIdentifierBefore(Next, doctype);
            }
            else if (c == Specification.DoubleQuote)
            {
                RaiseErrorOccurred(ErrorCode.DoubleQuotationMarkUnexpected);
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierDoubleQuoted(Next, doctype);
            }
            else if (c == Specification.SingleQuote)
            {
                RaiseErrorOccurred(ErrorCode.SingleQuotationMarkUnexpected);
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierSingleQuoted(Next, doctype);
            }
            else if (c == Specification.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.IsQuirksForced = true;
                return doctype;
            }
            else if (c == Specification.EndOfFile)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypePublicInvalid);
            doctype.IsQuirksForced = true;
            return BogusDoctype(Next, doctype);
        }

        /// <summary>
        /// See 8.2.4.57 Before DOCTYPE public identifier state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypePublicIdentifierBefore(Char c, HtmlDoctypeToken doctype)
        {
            while (c.IsSpaceCharacter())
                c = Next;

            if (c == Specification.DoubleQuote)
            {
                _stringBuffer.Clear();
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierDoubleQuoted(Next, doctype);
            }
            else if (c == Specification.SingleQuote)
            {
                _stringBuffer.Clear();
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierSingleQuoted(Next, doctype);
            }
            else if (c == Specification.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.IsQuirksForced = true;
                return doctype;
            }
            else if (c == Specification.EndOfFile)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypePublicInvalid);
            doctype.IsQuirksForced = true;
            return BogusDoctype(Next, doctype);
        }

        /// <summary>
        /// See 8.2.4.58 DOCTYPE public identifier (double-quoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypePublicIdentifierDoubleQuoted(Char c, HtmlDoctypeToken doctype)
        {
            while (true)
            {
                if (c == Specification.DoubleQuote)
                {
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return DoctypePublicIdentifierAfter(Next, doctype); ;
                }
                else if (c == Specification.Null)
                {
                    RaiseErrorOccurred(ErrorCode.Null);
                    _stringBuffer.Append(Specification.Replacement);
                }
                else if (c == Specification.GreaterThan)
                {
                    _state = HtmlParseMode.PCData;
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    return doctype;
                }
                else if (c == Specification.EndOfFile)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    Back();
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    return doctype;
                }
                else
                    _stringBuffer.Append(c);

                c = Next;
            }
        }

        /// <summary>
        /// See 8.2.4.59 DOCTYPE public identifier (single-quoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypePublicIdentifierSingleQuoted(Char c, HtmlDoctypeToken doctype)
        {
            while (true)
            {
                if (c == Specification.SingleQuote)
                {
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return DoctypePublicIdentifierAfter(Next, doctype);
                }
                else if (c == Specification.Null)
                {
                    RaiseErrorOccurred(ErrorCode.Null);
                    _stringBuffer.Append(Specification.Replacement);
                }
                else if (c == Specification.GreaterThan)
                {
                    _state = HtmlParseMode.PCData;
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    return doctype;
                }
                else if (c == Specification.EndOfFile)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    Back();
                    return doctype;
                }
                else
                    _stringBuffer.Append(c);

                c = Next;
            }
        }

        /// <summary>
        /// See 8.2.4.60 After DOCTYPE public identifier state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypePublicIdentifierAfter(Char c, HtmlDoctypeToken doctype)
        {
            if (c.IsSpaceCharacter())
            {
                _stringBuffer.Clear();
                return DoctypeBetween(Next, doctype);
            }
            else if (c == Specification.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
                return doctype;
            }
            else if (c == Specification.DoubleQuote)
            {
                RaiseErrorOccurred(ErrorCode.DoubleQuotationMarkUnexpected);
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(Next, doctype);
            }
            else if (c == Specification.SingleQuote)
            {
                RaiseErrorOccurred(ErrorCode.SingleQuotationMarkUnexpected);
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierSingleQuoted(Next, doctype);
            }
            else if (c == Specification.EndOfFile)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
            doctype.IsQuirksForced = true;
            return BogusDoctype(Next, doctype);
        }

        /// <summary>
        /// See 8.2.4.61 Between DOCTYPE public and system identifiers state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypeBetween(Char c, HtmlDoctypeToken doctype)
        {
            while (c.IsSpaceCharacter())
                c = Next;

            if (c == Specification.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
                return doctype;
            }
            else if (c == Specification.DoubleQuote)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(Next, doctype);
            }
            else if (c == Specification.SingleQuote)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierSingleQuoted(Next, doctype);
            }
            else if (c == Specification.EndOfFile)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
            doctype.IsQuirksForced = true;
            return BogusDoctype(Next, doctype);
        }

        /// <summary>
        /// See 8.2.4.62 After DOCTYPE system keyword state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypeSystem(Char c, HtmlDoctypeToken doctype)
        {
            if (c.IsSpaceCharacter())
            {
                _state = HtmlParseMode.PCData;
                return DoctypeSystemIdentifierBefore(Next, doctype);
            }
            else if (c == Specification.DoubleQuote)
            {
                RaiseErrorOccurred(ErrorCode.DoubleQuotationMarkUnexpected);
                doctype.SystemIdentifier = string.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(Next, doctype);
            }
            else if (c == Specification.SingleQuote)
            {
                RaiseErrorOccurred(ErrorCode.SingleQuotationMarkUnexpected);
                doctype.SystemIdentifier = string.Empty;
                return DoctypeSystemIdentifierSingleQuoted(Next, doctype);
            }
            else if (c == Specification.GreaterThan)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.SystemIdentifier = _stringBuffer.ToString();
                doctype.IsQuirksForced = true;
                return doctype;
            }
            else if (c == Specification.EndOfFile)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypeSystemInvalid);
            doctype.IsQuirksForced = true;
            return BogusDoctype(Next, doctype);
        }

        /// <summary>
        /// See 8.2.4.63 Before DOCTYPE system identifier state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypeSystemIdentifierBefore(Char c, HtmlDoctypeToken doctype)
        {
            while (c.IsSpaceCharacter())
                c = Next;

            if (c == Specification.DoubleQuote)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(Next, doctype);
            }
            else if (c == Specification.SingleQuote)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierSingleQuoted(Next, doctype);
            }
            else if (c == Specification.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = _stringBuffer.ToString();
                return doctype;
            }
            else if (c == Specification.EndOfFile)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = _stringBuffer.ToString();
                Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
            doctype.IsQuirksForced = true;
            return BogusDoctype(Next, doctype);
        }

        /// <summary>
        /// See 8.2.4.64 DOCTYPE system identifier (double-quoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypeSystemIdentifierDoubleQuoted(Char c, HtmlDoctypeToken doctype)
        {
            while (true)
            {
                if (c == Specification.DoubleQuote)
                {
                    doctype.SystemIdentifier = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return DoctypeSystemIdentifierAfter(Next, doctype);
                }
                else if (c == Specification.Null)
                {
                    RaiseErrorOccurred(ErrorCode.Null);
                    _stringBuffer.Append(Specification.Replacement);
                }
                else if (c == Specification.GreaterThan)
                {
                    _state = HtmlParseMode.PCData;
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    doctype.IsQuirksForced = true;
                    doctype.SystemIdentifier = _stringBuffer.ToString();
                    return doctype;
                }
                else if (c == Specification.EndOfFile)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    doctype.IsQuirksForced = true;
                    doctype.SystemIdentifier = _stringBuffer.ToString();
                    Back();
                    return doctype;
                }
                else
                    _stringBuffer.Append(c);

                c = Next;
            }
        }

        /// <summary>
        /// See 8.2.4.65 DOCTYPE system identifier (single-quoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypeSystemIdentifierSingleQuoted(Char c, HtmlDoctypeToken doctype)
        {
            while (true)
            {
                if (c == Specification.SingleQuote)
                {
                    doctype.SystemIdentifier = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return DoctypeSystemIdentifierAfter(Next, doctype);
                }
                else if (c == Specification.Null)
                {
                    RaiseErrorOccurred(ErrorCode.Null);
                    _stringBuffer.Append(Specification.Replacement);
                }
                else if (c == Specification.GreaterThan)
                {
                    _state = HtmlParseMode.PCData;
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    doctype.IsQuirksForced = true;
                    doctype.SystemIdentifier = _stringBuffer.ToString();
                    return doctype;
                }
                else if (c == Specification.EndOfFile)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    doctype.IsQuirksForced = true;
                    doctype.SystemIdentifier = _stringBuffer.ToString();
                    Back();
                    return doctype;
                }
                else
                    _stringBuffer.Append(c);

                c = Next;
            }
        }

        /// <summary>
        /// See 8.2.4.66 After DOCTYPE system identifier state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypeSystemIdentifierAfter(Char c, HtmlDoctypeToken doctype)
        {
            while (c.IsSpaceCharacter())
                c = Next;

            if (c == Specification.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
                return doctype;
            }
            else if (c == Specification.EndOfFile)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
            return BogusDoctype(Next, doctype);
        }

        /// <summary>
        /// See 8.2.4.67 Bogus DOCTYPE state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken BogusDoctype(Char c, HtmlDoctypeToken doctype)
        {
            while (true)
            {
                if (c == Specification.EndOfFile)
                {
                    Back();
                    return doctype;
                }
                else if (c == Specification.GreaterThan)
                {
                    _state = HtmlParseMode.PCData;
                    return doctype;
                }

                c = Next;
            }
        }

        #endregion

        #region Attributes

        /// <summary>
        /// See 8.2.4.34 Before attribute name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken AttributeBeforeName(Char c, HtmlTagToken tag)
        {
            while (c.IsSpaceCharacter())
                c = Next;

            if (c == Specification.Solidus)
            {
                return TagSelfClosing(Next, tag);
            }
            else if (c == Specification.GreaterThan)
            {
                return EmitTag(tag);
            }
            else if (c.IsUppercaseAscii())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(Char.ToLower(c));
                return AttributeName(Next, tag);
            }
            else if (c == Specification.Null)
            {
                RaiseErrorOccurred(ErrorCode.Null);
                _stringBuffer.Clear();
                _stringBuffer.Append(Specification.Replacement);
                return AttributeName(Next, tag);
            }
            else if (c == Specification.SingleQuote || c == Specification.DoubleQuote || c == Specification.Equality || c == Specification.LessThan)
            {
                RaiseErrorOccurred(ErrorCode.AttributeNameInvalid);
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return AttributeName(Next, tag);
            }
            else if (c == Specification.EndOfFile)
            {
                return HtmlToken.EOF;
            }
            else
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return AttributeName(Next, tag);
            }
        }

        /// <summary>
        /// See 8.2.4.35 Attribute name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken AttributeName(Char c, HtmlTagToken tag)
        {
            while (true)
            {
                if (c.IsSpaceCharacter())
                {
                    tag.AddAttribute(_stringBuffer.ToString());
                    return AttributeAfterName(Next, tag);
                }
                else if (c == Specification.Solidus)
                {
                    tag.AddAttribute(_stringBuffer.ToString());
                    return TagSelfClosing(Next, tag);
                }
                else if (c == Specification.Equality)
                {
                    tag.AddAttribute(_stringBuffer.ToString());
                    return AttributeBeforeValue(Next, tag);
                }
                else if (c == Specification.GreaterThan)
                {
                    tag.AddAttribute(_stringBuffer.ToString());
                    return EmitTag(tag);
                }
                else if (c == Specification.EndOfFile)
                    return HtmlToken.EOF;
                else if (c == Specification.Null)
                {
                    RaiseErrorOccurred(ErrorCode.Null);
                    _stringBuffer.Append(Specification.Replacement);
                }
                else if (c.IsUppercaseAscii())
                    _stringBuffer.Append(Char.ToLower(c));
                else if (c == Specification.DoubleQuote || c == Specification.SingleQuote || c == Specification.LessThan)
                {
                    RaiseErrorOccurred(ErrorCode.AttributeNameInvalid);
                    _stringBuffer.Append(c);
                }
                else
                    _stringBuffer.Append(c);

                c = Next;
            }
        }

        /// <summary>
        /// See 8.2.4.36 After attribute name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken AttributeAfterName(Char c, HtmlTagToken tag)
        {
            while (c.IsSpaceCharacter())
                c = Next;

            if (c == Specification.Solidus)
            {
                return TagSelfClosing(Next, tag);
            }
            else if (c == Specification.Equality)
            {
                return AttributeBeforeValue(Next, tag);
            }
            else if (c == Specification.GreaterThan)
            {
                return EmitTag(tag);
            }
            else if (c.IsUppercaseAscii())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(Char.ToLower(c));
                return AttributeName(Next, tag);
            }
            else if (c == Specification.Null)
            {
                RaiseErrorOccurred(ErrorCode.Null);
                _stringBuffer.Clear();
                _stringBuffer.Append(Specification.Replacement);
                return AttributeName(Next, tag);
            }
            else if (c == Specification.DoubleQuote || c == Specification.SingleQuote || c == Specification.LessThan)
            {
                RaiseErrorOccurred(ErrorCode.AttributeNameInvalid);
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return AttributeName(Next, tag);
            }
            else if (c == Specification.EndOfFile)
            {
                return HtmlToken.EOF;
            }
            else
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return AttributeName(Next, tag);
            }
        }

        /// <summary>
        /// See 8.2.4.37 Before attribute value state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken AttributeBeforeValue(Char c, HtmlTagToken tag)
        {
            while (c.IsSpaceCharacter())
                c = Next;

            if (c == Specification.DoubleQuote)
            {
                _stringBuffer.Clear();
                return AttributeDoubleQuotedValue(Next, tag);
            }
            else if (c == Specification.Ampersand)
            {
                _stringBuffer.Clear();
                return AttributeUnquotedValue(c, tag);
            }
            else if (c == Specification.SingleQuote)
            {
                _stringBuffer.Clear();
                return AttributeSingleQuotedValue(Next, tag);
            }
            else if (c == Specification.Null)
            {
                RaiseErrorOccurred(ErrorCode.Null);
                _stringBuffer.Append(Specification.Replacement);
                return AttributeUnquotedValue(Next, tag);
            }
            else if (c == Specification.GreaterThan)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return EmitTag(tag);
            }
            else if (c == Specification.LessThan || c == Specification.Equality || c == Specification.CurvedQuote)
            {
                RaiseErrorOccurred(ErrorCode.AttributeValueInvalid);
                _stringBuffer.Clear().Append(c);
                return AttributeUnquotedValue(Next, tag);
            }
            else if (c == Specification.EndOfFile)
            {
                return HtmlToken.EOF;
            }
            else
            {
                _stringBuffer.Clear().Append(c);
                return AttributeUnquotedValue(Next, tag);
            }
        }

        /// <summary>
        /// See 8.2.4.38 Attribute value (double-quoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken AttributeDoubleQuotedValue(Char c, HtmlTagToken tag)
        {
            while (true)
            {
                if (c == Specification.DoubleQuote)
                {
                    tag.SetAttributeValue(_stringBuffer.ToString());
                    return AttributeAfterValue(Next, tag);
                }
                else if (c == Specification.Ampersand)
                {
                    var value = CharacterReference(Next, Specification.DoubleQuote);

                    if (value == null)
                        _stringBuffer.Append(Specification.Ampersand);
                    else
                        _stringBuffer.Append(value);
                }
                else if (c == Specification.Null)
                {
                    RaiseErrorOccurred(ErrorCode.Null);
                    _stringBuffer.Append(Specification.Replacement);
                }
                else if (c == Specification.EndOfFile)
                    return HtmlToken.EOF;
                else
                    _stringBuffer.Append(c);

                c = Next;
            }
        }

        /// <summary>
        /// See 8.2.4.39 Attribute value (single-quoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken AttributeSingleQuotedValue(Char c, HtmlTagToken tag)
        {
            while (true)
            {
                if (c == Specification.SingleQuote)
                {
                    tag.SetAttributeValue(_stringBuffer.ToString());
                    return AttributeAfterValue(Next, tag);
                }
                else if (c == Specification.Ampersand)
                {
                    var value = CharacterReference(Next, Specification.SingleQuote);

                    if (value == null)
                        _stringBuffer.Append(Specification.Ampersand);
                    else
                        _stringBuffer.Append(value);
                }
                else if (c == Specification.Null)
                {
                    RaiseErrorOccurred(ErrorCode.Null);
                    _stringBuffer.Append(Specification.Replacement);
                }
                else if (c == Specification.EndOfFile)
                    return HtmlToken.EOF;
                else
                    _stringBuffer.Append(c);

                c = Next;
            }
        }

        /// <summary>
        /// See 8.2.4.40 Attribute value (unquoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken AttributeUnquotedValue(Char c, HtmlTagToken tag)
        {
            while (true)
            {
                if (c.IsSpaceCharacter())
                {
                    tag.SetAttributeValue(_stringBuffer.ToString());
                    return AttributeBeforeName(Next, tag);
                }
                else if (c == Specification.Ampersand)
                {
                    var value = CharacterReference(Next, Specification.GreaterThan);

                    if (value == null)
                        _stringBuffer.Append(Specification.Ampersand);
                    else
                        _stringBuffer.Append(value);
                }
                else if (c == Specification.GreaterThan)
                {
                    tag.SetAttributeValue(_stringBuffer.ToString());
                    return EmitTag(tag);
                }
                else if (c == Specification.Null)
                {
                    RaiseErrorOccurred(ErrorCode.Null);
                    _stringBuffer.Append(Specification.Replacement);
                }
                else if (c == Specification.DoubleQuote || c == Specification.SingleQuote || c == Specification.LessThan || c == Specification.Equality || c == Specification.CurvedQuote)
                {
                    RaiseErrorOccurred(ErrorCode.AttributeValueInvalid);
                    _stringBuffer.Append(c);
                }
                else if (c == Specification.EndOfFile)
                    return HtmlToken.EOF;
                else
                    _stringBuffer.Append(c);

                c = Next;
            }
        }

        /// <summary>
        /// See 8.2.4.42 After attribute value (quoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken AttributeAfterValue(Char c, HtmlTagToken tag)
        {
            if (c.IsSpaceCharacter())
                return AttributeBeforeName(Next, tag);
            else if (c == Specification.Solidus)
                return TagSelfClosing(Next, tag);
            else if (c == Specification.GreaterThan)
                return EmitTag(tag);
            else if (c == Specification.EndOfFile)
                return HtmlTagToken.EOF;

            RaiseErrorOccurred(ErrorCode.AttributeNameExpected);
            return AttributeBeforeName(c, tag);
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
                    case Specification.LessThan:
                        return ScriptDataLT(Next);

                    case Specification.Null:
                        RaiseErrorOccurred(ErrorCode.Null);
                        _buffer.Append(Specification.Replacement);
                        break;

                    case Specification.EndOfFile:
                        return HtmlToken.EOF;

                    default:
                        _buffer.Append(c);
                        break;
                }

                c = Next;
            }
        }
        
        /// <summary>
        /// See 8.2.4.17 Script data less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataLT(Char c)
        {
            if (c == Specification.Solidus)
            {
                return ScriptDataEndTag(Next);
            }
            else if (c == Specification.ExclamationMark)
            {
                _buffer.Append(Specification.LessThan).Append(Specification.ExclamationMark);
                return ScriptDataStartEscape(Next);
            }

            _buffer.Append(Specification.LessThan);
            return ScriptData(c);
        }

        /// <summary>
        /// See 8.2.4.18 Script data end tag open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEndTag(Char c)
        {
            if (c.IsLetter())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return ScriptDataNameEndTag(Next, HtmlToken.CloseTag());
            }

            _buffer.Append(Specification.LessThan).Append(Specification.Solidus);
            return ScriptData(c);
        }

        /// <summary>
        /// See 8.2.4.19 Script data end tag name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken ScriptDataNameEndTag(Char c, HtmlTagToken tag)
        {
            var name = _stringBuffer.ToString().ToLowerInvariant();
            var appropriateEndTag = name == _lastStartTag;

            if (appropriateEndTag && c.IsSpaceCharacter())
            {
                tag.Name = name;
                return AttributeBeforeName(Next, tag);
            }
            else if (appropriateEndTag && c == Specification.Solidus)
            {
                tag.Name = name;
                return TagSelfClosing(Next, tag);
            }
            else if (appropriateEndTag && c == Specification.GreaterThan)
            {
                tag.Name = name;
                return EmitTag(tag);
            }
            else if (c.IsLetter())
            {
                _stringBuffer.Append(c);
                return ScriptDataNameEndTag(Next, tag);
            }

            _buffer.Append(Specification.LessThan).Append(Specification.Solidus);
            _buffer.Append(_stringBuffer.ToString());
            return ScriptData(c);
        }

        /// <summary>
        /// See 8.2.4.20 Script data escape start state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataStartEscape(Char c)
        {
            if (c == Specification.Minus)
            {
                _buffer.Append(Specification.Minus);
                return ScriptDataStartEscapeDash(Next);
            }

            return ScriptData(c);
        }

        /// <summary>
        /// See 8.2.4.22 Script data escaped state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscaped(Char c)
        {
            if (c == Specification.Minus)
            {
                _buffer.Append(Specification.Minus);
                return ScriptDataEscapedDash(Next);
            }
            else if (c == Specification.LessThan)
            {
                return ScriptDataEscapedLT(Next);
            }
            else if (c == Specification.Null)
            {
                RaiseErrorOccurred(ErrorCode.Null);
                _buffer.Append(Specification.Replacement);
                return ScriptDataEscaped(Next);
            }
            else if (c == Specification.EndOfFile)
            {
                return HtmlToken.EOF;
            }

            return ScriptData(c);
        }

        /// <summary>
        /// See 8.2.4.21 Script data escape start dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataStartEscapeDash(Char c)
        {
            if (c == Specification.Minus)
            {
                _buffer.Append(Specification.Minus);
                return ScriptDataEscapedDashDash(Next);
            }

            return ScriptData(c);
        }

        /// <summary>
        /// See 8.2.4.23 Script data escaped dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedDash(Char c)
        {
            if (c == Specification.Minus)
            {
                _buffer.Append(Specification.Minus);
                return ScriptDataEscapedDashDash(Next);
            }
            else if (c == Specification.LessThan)
            {
                return ScriptDataEscapedLT(Next);
            }
            else if (c == Specification.Null)
            {
                RaiseErrorOccurred(ErrorCode.Null);
                _buffer.Append(Specification.Replacement);
                return ScriptDataEscaped(Next);
            }
            else if (c == Specification.EndOfFile)
            {
                return HtmlToken.EOF;
            }

            _buffer.Append(c);
            return ScriptDataEscaped(Next);
        }

        /// <summary>
        /// See 8.2.4.24 Script data escaped dash dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedDashDash(Char c)
        {
            if (c == Specification.Minus)
            {
                _buffer.Append(Specification.Minus);
                return ScriptDataEscapedDashDash(Next);
            }
            else if (c == Specification.LessThan)
            {
                return ScriptDataEscapedLT(Next);
            }
            else if (c == Specification.GreaterThan)
            {
                _buffer.Append(Specification.GreaterThan);
                return ScriptData(Next);
            }
            else if (c == Specification.Null)
            {
                RaiseErrorOccurred(ErrorCode.Null);
                _buffer.Append(Specification.Replacement);
                return ScriptDataEscaped(Next);
            }
            else if (c == Specification.EndOfFile)
            {
                return HtmlToken.EOF;
            }

            _buffer.Append(c);
            return ScriptDataEscaped(Next);
        }

        /// <summary>
        /// See 8.2.4.25 Script data escaped less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedLT(Char c)
        {
            if (c == Specification.Solidus)
            {
                return ScriptDataEndTag(Next);
            }
            else if (c.IsLetter())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                _buffer.Append(Specification.LessThan);
                _buffer.Append(c);
                return ScriptDataStartDoubleEscape(Next);
            }

            _buffer.Append(Specification.LessThan);
            return ScriptDataEscaped(c);
        }

        /// <summary>
        /// See 8.2.4.26 Script data escaped end tag open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken ScriptDataEscapedEndTag(Char c, HtmlTagToken tag)
        {
            if (c.IsLetter())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return ScriptDataEscapedEndTag(Next, tag);
            }

            _buffer.Append(Specification.LessThan).Append(Specification.Solidus);
            return ScriptDataEscaped(c);
        }

        /// <summary>
        /// See 8.2.4.27 Script data escaped end tag name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken ScriptDataEscapedNameTag(Char c, HtmlTagToken tag)
        {
            var name = _stringBuffer.ToString().ToLowerInvariant();
            var appropriateEndTag = name == _lastStartTag;

            if (appropriateEndTag && c.IsSpaceCharacter())
            {
                tag.Name = name;
                return AttributeBeforeName(Next, tag);
            }
            else if (appropriateEndTag && c == Specification.Solidus)
            {
                tag.Name = name;
                return TagSelfClosing(Next, tag);
            }
            else if (appropriateEndTag && c == Specification.GreaterThan)
            {
                tag.Name = name;
                return EmitTag(tag);
            }
            else if (c.IsLetter())
            {
                _stringBuffer.Append(c);
                return ScriptDataEscapedNameTag(Next, tag);
            }

            _buffer.Append(Specification.LessThan).Append(Specification.Solidus);
            _buffer.Append(_stringBuffer.ToString());
            return ScriptDataEscaped(c);
        }

        /// <summary>
        /// See 8.2.4.28 Script data double escape start state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataStartDoubleEscape(Char c)
        {
            if (c.IsSpaceCharacter() || c == Specification.Solidus || c == Specification.GreaterThan)
            {
                _buffer.Append(c);

                if (String.Compare(_stringBuffer.ToString(), Tags.Script, StringComparison.OrdinalIgnoreCase) == 0)
                    return ScriptDataEscapedDouble(Next);

                return ScriptDataEscaped(Next);
            }
            else if (c.IsLetter())
            {
                _stringBuffer.Append(c);
                _buffer.Append(c);
                return ScriptDataStartDoubleEscape(Next);
            }

            return ScriptDataEscaped(c);
        }

        /// <summary>
        /// See 8.2.4.29 Script data double escaped state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedDouble(Char c)
        {
            if (c == Specification.Minus)
            {
                _buffer.Append(Specification.Minus);
                return ScriptDataEscapedDoubleDash(Next);
            }
            else if (c == Specification.LessThan)
            {
                _buffer.Append(Specification.LessThan);
                return ScriptDataEscapedDoubleLT(Next);
            }
            else if (c == Specification.Null)
            {
                RaiseErrorOccurred(ErrorCode.Null);
                _buffer.Append(Specification.Replacement);
            }
            else if (c == Specification.EndOfFile)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return HtmlToken.EOF;
            }

            _buffer.Append(c);
            return ScriptDataEscapedDouble(Next);
        }

        /// <summary>
        /// See 8.2.4.30 Script data double escaped dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedDoubleDash(Char c)
        {
            if (c == Specification.Minus)
            {
                _buffer.Append(Specification.Minus);
                return ScriptDataEscapedDoubleDashDash(Next);
            }
            else if (c == Specification.LessThan)
            {
                _buffer.Append(Specification.LessThan);
                return ScriptDataEscapedDoubleLT(Next);
            }
            else if (c == Specification.Null)
            {
                RaiseErrorOccurred(ErrorCode.Null);
                _buffer.Append(Specification.Replacement);
                return ScriptDataEscapedDouble(Next);
            }
            else if (c == Specification.EndOfFile)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return HtmlToken.EOF;
            }

            _buffer.Append(c);
            return ScriptDataEscapedDouble(Next);
        }

        /// <summary>
        /// See 8.2.4.31 Script data double escaped dash dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedDoubleDashDash(Char c)
        {
            if (c == Specification.Minus)
            {
                _buffer.Append(Specification.Minus);
                return ScriptDataEscapedDoubleDashDash(Next);
            }
            else if (c == Specification.LessThan)
            {
                _buffer.Append(Specification.LessThan);
                return ScriptDataEscapedDoubleLT(Next);
            }
            else if (c == Specification.GreaterThan)
            {
                _buffer.Append(Specification.GreaterThan);
                return ScriptData(Next);
            }
            else if (c == Specification.Null)
            {
                RaiseErrorOccurred(ErrorCode.Null);
                _buffer.Append(Specification.Replacement);
                return ScriptDataEscapedDouble(Next);
            }
            else if (c == Specification.EndOfFile)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return HtmlToken.EOF;
            }

            _buffer.Append(c);
            return ScriptDataEscapedDouble(Next);
        }

        /// <summary>
        /// See 8.2.4.32 Script data double escaped less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedDoubleLT(Char c)
        {
            if (c == Specification.Solidus)
            {
                _stringBuffer.Clear();
                _buffer.Append(Specification.Solidus);
                return ScriptDataEndDoubleEscape(Next);
            }

            return ScriptDataEscapedDouble(c);
        }

        /// <summary>
        /// See 8.2.4.33 Script data double escape end state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEndDoubleEscape(Char c)
        {
            if (c.IsSpaceCharacter() || c == Specification.Solidus || c == Specification.GreaterThan)
            {
                _buffer.Append(c);

                if (String.Compare(_stringBuffer.ToString(), Tags.Script, StringComparison.OrdinalIgnoreCase) == 0)
                    return ScriptDataEscaped(Next);

                return ScriptDataEscapedDouble(Next);
            }
            else if (c.IsLetter())
            {
                _stringBuffer.Append(c);
                _buffer.Append(c);
                return ScriptDataEndDoubleEscape(Next);
            }

            return ScriptDataEscapedDouble(c);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Emits the current token as a tag token.
        /// </summary>
        HtmlTagToken EmitTag(HtmlTagToken tag)
        {
            _state = HtmlParseMode.PCData;

            if (tag.Type == HtmlTokenType.StartTag)
            {
                for (var i = tag.Attributes.Count - 1; i > 0; i--)
                {
                    for (var j = i - 1; j >= 0; j--)
                    {
                        if (tag.Attributes[j].Key == tag.Attributes[i].Key)
                        {
                            tag.Attributes.RemoveAt(i);
                            RaiseErrorOccurred(ErrorCode.AttributeDuplicateOmitted);
                            break;
                        }
                    }
                }

                _lastStartTag = tag.Name;
            }
            else
            {
                if (tag.IsSelfClosing)
                    RaiseErrorOccurred(ErrorCode.EndTagCannotBeSelfClosed);

                if (tag.Attributes.Count != 0)
                    RaiseErrorOccurred(ErrorCode.EndTagCannotHaveAttributes);
            }

            return tag;
        }

        #endregion
    }
}
