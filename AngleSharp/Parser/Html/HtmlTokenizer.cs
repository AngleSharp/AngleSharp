namespace AngleSharp.Parser.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
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
        public HtmlTokenizer(TextSource source)
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

            var current = GetNext();

            if (IsEnded) 
                return HtmlToken.EndOfFile;

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

        public override void Dispose()
        {
            base.Dispose();
            _buffer.ToPool();
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
                    case Symbols.Null:
                        RaiseErrorOccurred(ErrorCode.Null);
                        _buffer.Append(Symbols.Replacement);
                        break;

                    case Symbols.EndOfFile:
                        return HtmlToken.EndOfFile;

                    default:
                        _buffer.Append(c);
                        break;
                }

                c = GetNext();
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
                    case Symbols.Ampersand:
                        var value = CharacterReference(GetNext());

                        if (value == null)
                            _buffer.Append(Symbols.Ampersand);

                        _buffer.Append(value);
                        break;

                    case Symbols.LessThan:
                        return TagOpen();

                    case Symbols.Null:
                        RaiseErrorOccurred(ErrorCode.Null);
                        return Data(GetNext());

                    case Symbols.EndOfFile:
                        return HtmlToken.EndOfFile;

                    default:
                        _buffer.Append(c);
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
            while (true)
            {
                switch (c)
                {
                    case Symbols.Ampersand:
                        var value = CharacterReference(GetNext());

                        if (value == null)
                            _buffer.Append(Symbols.Ampersand);

                        _buffer.Append(value);
                        break;

                    case Symbols.LessThan:
                        // See 8.2.4.11 RCDATA less-than sign state
                        c = GetNext();

                        if (c == Symbols.Solidus)
                        {
                            _stringBuffer.Clear();
                            return RCDataEndTag();
                        }

                        _buffer.Append(Symbols.LessThan);
                        continue;

                    case Symbols.Null:
                        RaiseErrorOccurred(ErrorCode.Null);
                        _buffer.Append(Symbols.Replacement);
                        break;

                    case Symbols.EndOfFile:
                        return HtmlToken.EndOfFile;

                    default:
                        _buffer.Append(c);
                        break;
                }

                c = GetNext();
            }
        }

        /// <summary>
        /// See 8.2.4.12 RCDATA end tag open state
        /// </summary>
        /// <returns>The emitted token.</returns>
        HtmlToken RCDataEndTag()
        {
            var c = GetNext();

            if (c.IsUppercaseAscii())
            {
                _stringBuffer.Clear().Append(Char.ToLower(c));
            }
            else if (c.IsLowercaseAscii())
            {
                _stringBuffer.Clear().Append(c);
            }
            else
            {
                _buffer.Append(Symbols.LessThan).Append(Symbols.Solidus);
                return RCData(c);
            }

            return RCDataNameEndTag(HtmlTagToken.Close());
        }

        /// <summary>
        /// See 8.2.4.13 RCDATA end tag name state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken RCDataNameEndTag(HtmlTagToken tag)
        {
            while (true)
            {
                var c = GetNext();
                var name = _stringBuffer.ToString();
                var appropriateTag = name == _lastStartTag;

                if (appropriateTag && c.IsSpaceCharacter())
                {
                    tag.Name = name;
                    return AttributeBeforeName(tag);
                }
                else if (appropriateTag && c == Symbols.Solidus)
                {
                    tag.Name = name;
                    return TagSelfClosing(tag);
                }
                else if (appropriateTag && c == Symbols.GreaterThan)
                {
                    tag.Name = name;
                    return EmitTag(tag);
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
                    _buffer.Append(Symbols.LessThan).Append(Symbols.Solidus).Append(_stringBuffer.ToString());
                    return RCData(c);
                }
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
            while (true)
            {
                switch (c)
                {
                    case Symbols.LessThan:
                        return RawtextLT();

                    case Symbols.Null:
                        RaiseErrorOccurred(ErrorCode.Null);
                        _buffer.Append(Symbols.Replacement);
                        break;

                    case Symbols.EndOfFile:
                        return HtmlToken.EndOfFile;

                    default:
                        _buffer.Append(c);
                        break;
                }

                c = GetNext();
            }
        }

        /// <summary>
        /// See 8.2.4.14 RAWTEXT less-than sign state
        /// </summary>
        HtmlToken RawtextLT()
        {
            var position = GetCurrentPosition();
            var c = GetNext();

            if (c == Symbols.Solidus)
            {
                _stringBuffer.Clear();
                return RawtextEndTag(position);
            }

            _buffer.Append(Symbols.LessThan);
            return Rawtext(c);
        }

        /// <summary>
        /// See 8.2.4.15 RAWTEXT end tag open state
        /// </summary>
        /// <param name="position">The start position.</param>
        HtmlToken RawtextEndTag(TextPosition position)
        {
            var c = GetNext();

            if (c.IsUppercaseAscii())
            {
                _stringBuffer.Clear().Append(Char.ToLower(c));
            }
            else if (c.IsLowercaseAscii())
            {
                _stringBuffer.Clear().Append(c);
            }
            else
            {
                _buffer.Append(Symbols.LessThan).Append(Symbols.Solidus);
                return Rawtext(c);
            }

            var tag = HtmlTagToken.Close();
            //tag.Start = position;
            return RawtextNameEndTag(tag);
        }

        /// <summary>
        /// See 8.2.4.16 RAWTEXT end tag name state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken RawtextNameEndTag(HtmlTagToken tag)
        {
            while (true)
            {
                var c = GetNext();
                var name = _stringBuffer.ToString();
                var appropriateTag = name == _lastStartTag;

                if (appropriateTag && c.IsSpaceCharacter())
                {
                    tag.Name = name;
                    return AttributeBeforeName(tag);
                }
                else if (appropriateTag && c == Symbols.Solidus)
                {
                    tag.Name = name;
                    return TagSelfClosing(tag);
                }
                else if (appropriateTag && c == Symbols.GreaterThan)
                {
                    tag.Name = name;
                    return EmitTag(tag);
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
                    _buffer.Append(Symbols.LessThan).Append(Symbols.Solidus).Append(_stringBuffer.ToString());
                    return Rawtext(c);
                }
            }
        }

        #endregion

        #region CDATA

        /// <summary>
        /// See 8.2.4.68 CDATA section state
        /// </summary>
        HtmlToken CData()
        {
            _stringBuffer.Clear();

            while (true)
            {
                var c = GetNext();

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

                _stringBuffer.Append(c);
            }

            return HtmlToken.Character(_stringBuffer.ToString());
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

                    RaiseErrorOccurred(ErrorCode.CharacterReferenceWrongNumber);
                    return null;
                }

                if (c != Symbols.Semicolon)
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
                    return Symbols.Replacement.ToString();
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
                while (!IsEnded && index < 31);

                Back(consumed);
                chr = Current;

                if (chr != Symbols.Semicolon)
                {
                    if (allowedCharacter != Symbols.Null && (chr == Symbols.Equality || chr.IsAlphanumericAscii()))
                    {
                        if (chr == Symbols.Equality)
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
        HtmlToken TagOpen()
        {
            var c = GetNext();

            if (c == Symbols.Solidus)
            {
                return TagEnd(GetNext());
            }
            else if (c.IsLowercaseAscii())
            {
                var tag = HtmlTagToken.Open();
                _stringBuffer.Clear().Append(c);
                return TagName(tag);
            }
            else if (c.IsUppercaseAscii())
            {
                var tag = HtmlTagToken.Open();
                _stringBuffer.Clear().Append(Char.ToLower(c));
                return TagName(tag);
            }
            else if (c == Symbols.ExclamationMark)
            {
                return MarkupDeclaration();
            }
            else if (c == Symbols.QuestionMark)
            {
                RaiseErrorOccurred(ErrorCode.BogusComment);
                return BogusComment(c);
            }

            _state = HtmlParseMode.PCData;
            RaiseErrorOccurred(ErrorCode.AmbiguousOpenTag);
            _buffer.Append(Symbols.LessThan);
            return Data(c);
        }

        /// <summary>
        /// See 8.2.4.9 End tag open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken TagEnd(Char c)
        {
            if (c.IsLowercaseAscii())
            {
                var tag = HtmlTagToken.Close();
                _stringBuffer.Clear().Append(c);
                return TagName(tag);
            }
            else if (c.IsUppercaseAscii())
            {
                var tag = HtmlTagToken.Close();
                _stringBuffer.Clear().Append(Char.ToLower(c));
                return TagName(tag);
            }
            else if (c == Symbols.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return Data(GetNext());
            }
            else if (c == Symbols.EndOfFile)
            {
                Back();
                RaiseErrorOccurred(ErrorCode.EOF);
                _buffer.Append(Symbols.LessThan).Append(Symbols.Solidus);
                return HtmlToken.EndOfFile;
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
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken TagName(HtmlTagToken tag)
        {
            while (true)
            {
                var c = GetNext();

                if (c.IsSpaceCharacter())
                {
                    tag.Name = _stringBuffer.ToString();
                    return AttributeBeforeName(tag);
                }
                else if (c == Symbols.Solidus)
                {
                    tag.Name = _stringBuffer.ToString();
                    return TagSelfClosing(tag);
                }
                else if (c == Symbols.GreaterThan)
                {
                    tag.Name = _stringBuffer.ToString();
                    return EmitTag(tag);
                }
                else if (c == Symbols.Null)
                {
                    RaiseErrorOccurred(ErrorCode.Null);
                    _stringBuffer.Append(Symbols.Replacement);
                }
                else if (c == Symbols.EndOfFile)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    return HtmlToken.EndOfFile;
                }
                else if (c.IsUppercaseAscii())
                {
                    _stringBuffer.Append(Char.ToLower(c));
                }
                else
                {
                    _stringBuffer.Append(c);
                }
            }
        }

        /// <summary>
        /// See 8.2.4.43 Self-closing start tag state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken TagSelfClosing(HtmlTagToken tag)
        {
            switch (GetNext())
            {
                case Symbols.GreaterThan:
                    tag.IsSelfClosing = true;
                    return EmitTag(tag);
                case Symbols.EndOfFile:
                    RaiseErrorOccurred(ErrorCode.EOF);
                    return HtmlToken.EndOfFile;
                default:
                    RaiseErrorOccurred(ErrorCode.ClosingSlashMisplaced);
                    Back();
                    return AttributeBeforeName(tag);
            }
        }

        /// <summary>
        /// See 8.2.4.45 Markup declaration open state
        /// </summary>
        HtmlToken MarkupDeclaration()
        {
            var c = GetNext();

            if (ContinuesWith("--"))
            {
                Advance();
                return CommentStart();
            }
            else if (ContinuesWith(Tags.Doctype))
            {
                Advance(6);
                return Doctype();
            }
            else if (_acceptsCharacterData && ContinuesWith("[CDATA[", ignoreCase: false))
            {
                Advance(6);
                return CData();
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
                return EmitComment();
            }
        }

        /// <summary>
        /// See 8.2.4.46 Comment start state
        /// </summary>
        HtmlToken CommentStart()
        {
            var c = GetNext();
            _stringBuffer.Clear();

            switch (c)
            {
                case Symbols.Minus:
                    return CommentDashStart();
                case Symbols.Null:
                    RaiseErrorOccurred(ErrorCode.Null);
                    _stringBuffer.Append(Symbols.Replacement);
                    return Comment();
                case Symbols.GreaterThan:
                    _state = HtmlParseMode.PCData;
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    break;
                case Symbols.EndOfFile:
                    RaiseErrorOccurred(ErrorCode.EOF);
                    Back();
                    break;
                default:
                    _stringBuffer.Append(c);
                    return Comment();
            }

            return EmitComment();
        }

        /// <summary>
        /// See 8.2.4.47 Comment start dash state
        /// </summary>
        HtmlToken CommentDashStart()
        {
            var c = GetNext();

            switch (c)
            {
                case Symbols.Minus:
                    return CommentEnd();
                case Symbols.Null:
                    RaiseErrorOccurred(ErrorCode.Null);
                    _stringBuffer.Append(Symbols.Minus).Append(Symbols.Replacement);
                    return Comment();
                case Symbols.GreaterThan:
                    _state = HtmlParseMode.PCData;
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    break;
                case Symbols.EndOfFile:
                    RaiseErrorOccurred(ErrorCode.EOF);
                    Back();
                    break;
                default:
                    _stringBuffer.Append(Symbols.Minus).Append(c);
                    return Comment();
            }

            return EmitComment();
        }

        /// <summary>
        /// See 8.2.4.48 Comment state
        /// </summary>
        HtmlToken Comment()
        {
            while (true)
            {
                var c = GetNext();

                switch (c)
                {
                    case Symbols.Minus:
                        var result = CommentDashEnd();

                        if (result != null)
                            return result;

                        continue;
                    case Symbols.EndOfFile:
                        RaiseErrorOccurred(ErrorCode.EOF);
                        Back();
                        break;
                    case Symbols.Null:
                        RaiseErrorOccurred(ErrorCode.Null);
                        c = Symbols.Replacement;
                        _stringBuffer.Append(c);
                        continue;
                    default:
                        _stringBuffer.Append(c);
                        continue;
                }

                return EmitComment();
            }
        }

        /// <summary>
        /// See 8.2.4.49 Comment end dash state
        /// </summary>
        HtmlToken CommentDashEnd()
        {
            var c = GetNext();

            switch (c)
            {
                case Symbols.Minus:
                    return CommentEnd();
                case Symbols.EndOfFile:
                    RaiseErrorOccurred(ErrorCode.EOF);
                    Back();
                    return EmitComment();
                case Symbols.Null:
                    RaiseErrorOccurred(ErrorCode.Null);
                    c = Symbols.Replacement;
                    break;
            }

            _stringBuffer.Append(Symbols.Minus).Append(c);
            return null;
        }

        /// <summary>
        /// See 8.2.4.50 Comment end state
        /// </summary>
        HtmlToken CommentEnd()
        {
            while (true)
            {
                var c = GetNext();

                switch (c)
                {
                    case Symbols.GreaterThan:
                        _state = HtmlParseMode.PCData;
                        break;
                    case Symbols.Null:
                        RaiseErrorOccurred(ErrorCode.Null);
                        _stringBuffer.Append(Symbols.Minus).Append(Symbols.Replacement);
                        return null;
                    case Symbols.ExclamationMark:
                        RaiseErrorOccurred(ErrorCode.CommentEndedWithEM);
                        return CommentBangEnd();
                    case Symbols.Minus:
                        RaiseErrorOccurred(ErrorCode.CommentEndedWithDash);
                        _stringBuffer.Append(Symbols.Minus);
                        continue;
                    case Symbols.EndOfFile:
                        RaiseErrorOccurred(ErrorCode.EOF);
                        Back();
                        break;
                    default:
                        RaiseErrorOccurred(ErrorCode.CommentEndedUnexpected);
                        _stringBuffer.Append(Symbols.Minus).Append(Symbols.Minus).Append(c);
                        return null;
                }

                return EmitComment();
            }
        }

        /// <summary>
        /// See 8.2.4.51 Comment end bang state
        /// </summary>
        HtmlToken CommentBangEnd()
        {
            var c = GetNext();

            switch (c)
            {
                case Symbols.Minus:
                    _stringBuffer.Append(Symbols.Minus).Append(Symbols.Minus).Append(Symbols.ExclamationMark);
                    return CommentDashEnd();
                case Symbols.GreaterThan:
                    _state = HtmlParseMode.PCData;
                    break;
                case Symbols.Null:
                    RaiseErrorOccurred(ErrorCode.Null);
                    _stringBuffer.Append(Symbols.Minus).Append(Symbols.Minus).Append(Symbols.ExclamationMark).Append(Symbols.Replacement);
                    return null;
                case Symbols.EndOfFile:
                    RaiseErrorOccurred(ErrorCode.EOF);
                    Back();
                    break;
                default:
                    _stringBuffer.Append(Symbols.Minus).Append(Symbols.Minus).Append(Symbols.ExclamationMark).Append(c);
                    return null;
            }

            return EmitComment();
        }

        #endregion

        #region Doctype

        /// <summary>
        /// See 8.2.4.52 DOCTYPE state
        /// </summary>
        HtmlToken Doctype()
        {
            var c = GetNext();

            if (c.IsSpaceCharacter())
            {
                return DoctypeNameBefore(GetNext());
            }
            else if (c == Symbols.EndOfFile)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                Back();
                var doctype = HtmlToken.Doctype(true);
                return Emit(doctype);
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
                c = GetNext();

            if (c.IsUppercaseAscii())
            {
                var doctype = HtmlToken.Doctype(false);
                _stringBuffer.Clear().Append(Char.ToLower(c));
                return DoctypeName(doctype);
            }
            else if (c == Symbols.Null)
            {
                var doctype = HtmlToken.Doctype(false);
                RaiseErrorOccurred(ErrorCode.Null);
                _stringBuffer.Clear().Append(Symbols.Replacement);
                return DoctypeName(doctype);
            }
            else if (c == Symbols.GreaterThan)
            {
                var doctype = HtmlToken.Doctype(true);
                _state = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return Emit(doctype);
            }
            else if (c == Symbols.EndOfFile)
            {
                var doctype = HtmlToken.Doctype(true);
                RaiseErrorOccurred(ErrorCode.EOF);
                Back();
                return Emit(doctype);
            }
            else
            {
                var doctype = HtmlToken.Doctype(false);
                _stringBuffer.Clear().Append(c);
                return DoctypeName(doctype);
            }
        }

        /// <summary>
        /// See 8.2.4.54 DOCTYPE name state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
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
                    break;
                }
                else if (c.IsUppercaseAscii())
                {
                    _stringBuffer.Append(Char.ToLower(c));
                }
                else if (c == Symbols.Null)
                {
                    RaiseErrorOccurred(ErrorCode.Null);
                    _stringBuffer.Append(Symbols.Replacement);
                }
                else if (c == Symbols.EndOfFile)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    Back();
                    doctype.IsQuirksForced = true;
                    doctype.Name = _stringBuffer.ToString();
                    break;
                }
                else
                {
                    _stringBuffer.Append(c);
                }
            }

            return Emit(doctype);
        }

        /// <summary>
        /// See 8.2.4.55 After DOCTYPE name state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypeNameAfter(HtmlDoctypeToken doctype)
        {
            var c = SkipSpaces();

            if (c == Symbols.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
            }
            else if (c == Symbols.EndOfFile)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
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
                RaiseErrorOccurred(ErrorCode.DoctypeUnexpectedAfterName);
                doctype.IsQuirksForced = true;
                return BogusDoctype(doctype);
            }

            return Emit(doctype);
        }

        /// <summary>
        /// See 8.2.4.56 After DOCTYPE public keyword state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypePublic(HtmlDoctypeToken doctype)
        {
            var c = GetNext();

            if (c.IsSpaceCharacter())
            {
                return DoctypePublicIdentifierBefore(doctype);
            }
            else if (c == Symbols.DoubleQuote)
            {
                RaiseErrorOccurred(ErrorCode.DoubleQuotationMarkUnexpected);
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierDoubleQuoted(doctype);
            }
            else if (c == Symbols.SingleQuote)
            {
                RaiseErrorOccurred(ErrorCode.SingleQuotationMarkUnexpected);
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierSingleQuoted(doctype);
            }
            else if (c == Symbols.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.IsQuirksForced = true;
            }
            else if (c == Symbols.EndOfFile)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                Back();
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.DoctypePublicInvalid);
                doctype.IsQuirksForced = true;
                return BogusDoctype(doctype);
            }

            return Emit(doctype);
        }

        /// <summary>
        /// See 8.2.4.57 Before DOCTYPE public identifier state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypePublicIdentifierBefore(HtmlDoctypeToken doctype)
        {
            var c = SkipSpaces();

            if (c == Symbols.DoubleQuote)
            {
                _stringBuffer.Clear();
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierDoubleQuoted(doctype);
            }
            else if (c == Symbols.SingleQuote)
            {
                _stringBuffer.Clear();
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierSingleQuoted(doctype);
            }
            else if (c == Symbols.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.IsQuirksForced = true;
            }
            else if (c == Symbols.EndOfFile)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                Back();
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.DoctypePublicInvalid);
                doctype.IsQuirksForced = true;
                return BogusDoctype(doctype);
            }

            return Emit(doctype);
        }

        /// <summary>
        /// See 8.2.4.58 DOCTYPE public identifier (double-quoted) state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
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
                    RaiseErrorOccurred(ErrorCode.Null);
                    _stringBuffer.Append(Symbols.Replacement);
                }
                else if (c == Symbols.GreaterThan)
                {
                    _state = HtmlParseMode.PCData;
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    break;
                }
                else if (c == Symbols.EndOfFile)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    Back();
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    break;
                }
                else
                {
                    _stringBuffer.Append(c);
                }
            }

            return Emit(doctype);
        }

        /// <summary>
        /// See 8.2.4.59 DOCTYPE public identifier (single-quoted) state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
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
                    RaiseErrorOccurred(ErrorCode.Null);
                    _stringBuffer.Append(Symbols.Replacement);
                }
                else if (c == Symbols.GreaterThan)
                {
                    _state = HtmlParseMode.PCData;
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    break;
                }
                else if (c == Symbols.EndOfFile)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    Back();
                    break;
                }
                else
                {
                    _stringBuffer.Append(c);
                }
            }

            return Emit(doctype);
        }

        /// <summary>
        /// See 8.2.4.60 After DOCTYPE public identifier state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypePublicIdentifierAfter(HtmlDoctypeToken doctype)
        {
            var c = GetNext();

            if (c.IsSpaceCharacter())
            {
                _stringBuffer.Clear();
                return DoctypeBetween(doctype);
            }
            else if (c == Symbols.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
            }
            else if (c == Symbols.DoubleQuote)
            {
                RaiseErrorOccurred(ErrorCode.DoubleQuotationMarkUnexpected);
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(doctype);
            }
            else if (c == Symbols.SingleQuote)
            {
                RaiseErrorOccurred(ErrorCode.SingleQuotationMarkUnexpected);
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierSingleQuoted(doctype);
            }
            else if (c == Symbols.EndOfFile)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                Back();
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
                doctype.IsQuirksForced = true;
                return BogusDoctype(doctype);
            }

            return Emit(doctype);
        }

        /// <summary>
        /// See 8.2.4.61 Between DOCTYPE public and system identifiers state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
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
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                Back();
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
                doctype.IsQuirksForced = true;
                return BogusDoctype(doctype);
            }

            return Emit(doctype);
        }

        /// <summary>
        /// See 8.2.4.62 After DOCTYPE system keyword state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
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
                RaiseErrorOccurred(ErrorCode.DoubleQuotationMarkUnexpected);
                doctype.SystemIdentifier = string.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(doctype);
            }
            else if (c == Symbols.SingleQuote)
            {
                RaiseErrorOccurred(ErrorCode.SingleQuotationMarkUnexpected);
                doctype.SystemIdentifier = string.Empty;
                return DoctypeSystemIdentifierSingleQuoted(doctype);
            }
            else if (c == Symbols.GreaterThan)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.SystemIdentifier = _stringBuffer.ToString();
                doctype.IsQuirksForced = true;
            }
            else if (c == Symbols.EndOfFile)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                Back();
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.DoctypeSystemInvalid);
                doctype.IsQuirksForced = true;
                return BogusDoctype(doctype);
            }

            return Emit(doctype);
        }

        /// <summary>
        /// See 8.2.4.63 Before DOCTYPE system identifier state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
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
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = _stringBuffer.ToString();
            }
            else if (c == Symbols.EndOfFile)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = _stringBuffer.ToString();
                Back();
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
                doctype.IsQuirksForced = true;
                return BogusDoctype(doctype);
            }

            return Emit(doctype);
        }

        /// <summary>
        /// See 8.2.4.64 DOCTYPE system identifier (double-quoted) state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
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
                    RaiseErrorOccurred(ErrorCode.Null);
                    _stringBuffer.Append(Symbols.Replacement);
                }
                else if (c == Symbols.GreaterThan)
                {
                    _state = HtmlParseMode.PCData;
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    doctype.IsQuirksForced = true;
                    doctype.SystemIdentifier = _stringBuffer.ToString();
                    break;
                }
                else if (c == Symbols.EndOfFile)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    doctype.IsQuirksForced = true;
                    doctype.SystemIdentifier = _stringBuffer.ToString();
                    Back();
                    break;
                }
                else
                {
                    _stringBuffer.Append(c);
                }
            }

            return Emit(doctype);
        }

        /// <summary>
        /// See 8.2.4.65 DOCTYPE system identifier (single-quoted) state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
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
                        RaiseErrorOccurred(ErrorCode.Null);
                        _stringBuffer.Append(Symbols.Replacement);
                        continue;
                    case Symbols.GreaterThan:
                        _state = HtmlParseMode.PCData;
                        RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                        doctype.IsQuirksForced = true;
                        doctype.SystemIdentifier = _stringBuffer.ToString();
                        break;
                    case Symbols.EndOfFile:
                        RaiseErrorOccurred(ErrorCode.EOF);
                        doctype.IsQuirksForced = true;
                        doctype.SystemIdentifier = _stringBuffer.ToString();
                        Back();
                        break;
                    default:
                        _stringBuffer.Append(c);
                        continue;
                }

                return Emit(doctype);
            }
        }

        /// <summary>
        /// See 8.2.4.66 After DOCTYPE system identifier state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypeSystemIdentifierAfter(HtmlDoctypeToken doctype)
        {
            var c = SkipSpaces();

            switch (c)
            {
                case Symbols.GreaterThan:
                    _state = HtmlParseMode.PCData;
                    break;
                case Symbols.EndOfFile:
                    RaiseErrorOccurred(ErrorCode.EOF);
                    doctype.IsQuirksForced = true;
                    Back();
                    break;
                default:
                    RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
                    return BogusDoctype(doctype);
            }

            return Emit(doctype);
        }

        /// <summary>
        /// See 8.2.4.67 Bogus DOCTYPE state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
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

                return Emit(doctype);
            }
        }

        #endregion

        #region Attributes

        /// <summary>
        /// See 8.2.4.34 Before attribute name state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
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
                _stringBuffer.Clear().Append(Char.ToLower(c));
                return AttributeName(tag);
            }
            else if (c == Symbols.Null)
            {
                RaiseErrorOccurred(ErrorCode.Null);
                _stringBuffer.Clear().Append(Symbols.Replacement);
                return AttributeName(tag);
            }
            else if (c == Symbols.SingleQuote || c == Symbols.DoubleQuote || c == Symbols.Equality || c == Symbols.LessThan)
            {
                RaiseErrorOccurred(ErrorCode.AttributeNameInvalid);
                _stringBuffer.Clear().Append(c);
                return AttributeName(tag);
            }
            else if (c == Symbols.EndOfFile)
            {
                return HtmlToken.EndOfFile;
            }
            else
            {
                _stringBuffer.Clear().Append(c);
                return AttributeName(tag);
            }
        }

        /// <summary>
        /// See 8.2.4.35 Attribute name state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken AttributeName(HtmlTagToken tag)
        {
            while (true)
            {
                var c = GetNext();

                if (c.IsSpaceCharacter())
                {
                    tag.AddAttribute(_stringBuffer.ToString());
                    return AttributeAfterName(tag);
                }
                else if (c == Symbols.Solidus)
                {
                    tag.AddAttribute(_stringBuffer.ToString());
                    return TagSelfClosing(tag);
                }
                else if (c == Symbols.Equality)
                {
                    tag.AddAttribute(_stringBuffer.ToString());
                    return AttributeBeforeValue(tag);
                }
                else if (c == Symbols.GreaterThan)
                {
                    tag.AddAttribute(_stringBuffer.ToString());
                    return EmitTag(tag);
                }
                else if (c == Symbols.EndOfFile)
                {
                    return HtmlToken.EndOfFile;
                }
                else if (c == Symbols.Null)
                {
                    RaiseErrorOccurred(ErrorCode.Null);
                    _stringBuffer.Append(Symbols.Replacement);
                }
                else if (c.IsUppercaseAscii())
                {
                    _stringBuffer.Append(Char.ToLower(c));
                }
                else if (c == Symbols.DoubleQuote || c == Symbols.SingleQuote || c == Symbols.LessThan)
                {
                    RaiseErrorOccurred(ErrorCode.AttributeNameInvalid);
                    _stringBuffer.Append(c);
                }
                else
                {
                    _stringBuffer.Append(c);
                }
            }
        }

        /// <summary>
        /// See 8.2.4.36 After attribute name state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken AttributeAfterName(HtmlTagToken tag)
        {
            var c = SkipSpaces();

            if (c == Symbols.Solidus)
            {
                return TagSelfClosing(tag);
            }
            else if (c == Symbols.Equality)
            {
                return AttributeBeforeValue(tag);
            }
            else if (c == Symbols.GreaterThan)
            {
                return EmitTag(tag);
            }
            else if (c.IsUppercaseAscii())
            {
                _stringBuffer.Clear().Append(Char.ToLower(c));
                return AttributeName(tag);
            }
            else if (c == Symbols.Null)
            {
                RaiseErrorOccurred(ErrorCode.Null);
                _stringBuffer.Clear().Append(Symbols.Replacement);
                return AttributeName(tag);
            }
            else if (c == Symbols.DoubleQuote || c == Symbols.SingleQuote || c == Symbols.LessThan)
            {
                RaiseErrorOccurred(ErrorCode.AttributeNameInvalid);
                _stringBuffer.Clear().Append(c);
                return AttributeName(tag);
            }
            else if (c == Symbols.EndOfFile)
            {
                return HtmlToken.EndOfFile;
            }
            else
            {
                _stringBuffer.Clear().Append(c);
                return AttributeName(tag);
            }
        }

        /// <summary>
        /// See 8.2.4.37 Before attribute value state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken AttributeBeforeValue(HtmlTagToken tag)
        {
            var c = SkipSpaces();

            if (c == Symbols.DoubleQuote)
            {
                _stringBuffer.Clear();
                return AttributeDoubleQuotedValue(tag);
            }
            else if (c == Symbols.Ampersand)
            {
                _stringBuffer.Clear();
                return AttributeUnquotedValue(c, tag);
            }
            else if (c == Symbols.SingleQuote)
            {
                _stringBuffer.Clear();
                return AttributeSingleQuotedValue(tag);
            }
            else if (c == Symbols.Null)
            {
                RaiseErrorOccurred(ErrorCode.Null);
                _stringBuffer.Append(Symbols.Replacement);
                return AttributeUnquotedValue(GetNext(), tag);
            }
            else if (c == Symbols.GreaterThan)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return EmitTag(tag);
            }
            else if (c == Symbols.LessThan || c == Symbols.Equality || c == Symbols.CurvedQuote)
            {
                RaiseErrorOccurred(ErrorCode.AttributeValueInvalid);
                _stringBuffer.Clear().Append(c);
                return AttributeUnquotedValue(GetNext(), tag);
            }
            else if (c == Symbols.EndOfFile)
            {
                return HtmlToken.EndOfFile;
            }
            else
            {
                _stringBuffer.Clear().Append(c);
                return AttributeUnquotedValue(GetNext(), tag);
            }
        }

        /// <summary>
        /// See 8.2.4.38 Attribute value (double-quoted) state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken AttributeDoubleQuotedValue(HtmlTagToken tag)
        {
            while (true)
            {
                var c = GetNext();

                if (c == Symbols.DoubleQuote)
                {
                    tag.SetAttributeValue(_stringBuffer.ToString());
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
                    RaiseErrorOccurred(ErrorCode.Null);
                    _stringBuffer.Append(Symbols.Replacement);
                }
                else if (c == Symbols.EndOfFile)
                {
                    return HtmlToken.EndOfFile;
                }
                else
                {
                    _stringBuffer.Append(c);
                }
            }
        }

        /// <summary>
        /// See 8.2.4.39 Attribute value (single-quoted) state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken AttributeSingleQuotedValue(HtmlTagToken tag)
        {
            while (true)
            {
                var c = GetNext();

                if (c == Symbols.SingleQuote)
                {
                    tag.SetAttributeValue(_stringBuffer.ToString());
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
                    RaiseErrorOccurred(ErrorCode.Null);
                    _stringBuffer.Append(Symbols.Replacement);
                }
                else if (c == Symbols.EndOfFile)
                {
                    return HtmlToken.EndOfFile;
                }
                else
                {
                    _stringBuffer.Append(c);
                }
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
                else if (c == Symbols.GreaterThan)
                {
                    tag.SetAttributeValue(_stringBuffer.ToString());
                    return EmitTag(tag);
                }
                else if (c == Symbols.Null)
                {
                    RaiseErrorOccurred(ErrorCode.Null);
                    _stringBuffer.Append(Symbols.Replacement);
                }
                else if (c == Symbols.DoubleQuote || c == Symbols.SingleQuote || c == Symbols.LessThan || c == Symbols.Equality || c == Symbols.CurvedQuote)
                {
                    RaiseErrorOccurred(ErrorCode.AttributeValueInvalid);
                    _stringBuffer.Append(c);
                }
                else if (c == Symbols.EndOfFile)
                {
                    return HtmlToken.EndOfFile;
                }
                else
                {
                    _stringBuffer.Append(c);
                }

                c = GetNext();
            }
        }

        /// <summary>
        /// See 8.2.4.42 After attribute value (quoted) state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken AttributeAfterValue(HtmlTagToken tag)
        {
            var c = GetNext();

            if (c.IsSpaceCharacter())
                return AttributeBeforeName(tag);
            else if (c == Symbols.Solidus)
                return TagSelfClosing(tag);
            else if (c == Symbols.GreaterThan)
                return EmitTag(tag);
            else if (c == Symbols.EndOfFile)
                return HtmlToken.EndOfFile;

            RaiseErrorOccurred(ErrorCode.AttributeNameExpected);
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
                    case Symbols.LessThan:
                        //See 8.2.4.17 Script data less-than sign state
                        c = GetNext();

                        if (c == Symbols.Solidus)
                        {
                            //See 8.2.4.18 Script data end tag open state
                            c = GetNext();

                            if (c.IsLetter())
                            {
                                var tag = HtmlTagToken.Close();
                                _stringBuffer.Clear().Append(c);
                                return ScriptDataNameEndTag(tag);
                            }

                            _buffer.Append(Symbols.LessThan).Append(Symbols.Solidus);
                            continue;
                        }

                        _buffer.Append(Symbols.LessThan);

                        if (c == Symbols.ExclamationMark)
                        {
                            //See 8.2.4.20 Script data escape start state
                            c = GetNext();
                            _buffer.Append(Symbols.ExclamationMark);

                            if (c == Symbols.Minus)
                            {
                                //See 8.2.4.21 Script data escape start dash state
                                c = GetNext();
                                _buffer.Append(Symbols.Minus);

                                if (c == Symbols.Minus)
                                {
                                    _buffer.Append(Symbols.Minus);
                                    return ScriptDataEscapedDashDash();
                                }
                            }
                        }

                        continue;

                    case Symbols.Null:
                        RaiseErrorOccurred(ErrorCode.Null);
                        _buffer.Append(Symbols.Replacement);
                        break;

                    case Symbols.EndOfFile:
                        return HtmlToken.EndOfFile;

                    default:
                        _buffer.Append(c);
                        break;
                }

                c = GetNext();
            }
        }

        /// <summary>
        /// See 8.2.4.19 Script data end tag name state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken ScriptDataNameEndTag(HtmlTagToken tag)
        {
            while (true)
            {
                var c = GetNext();
                var name = _stringBuffer.ToString().ToLowerInvariant();
                var appropriateEndTag = name == _lastStartTag;

                if (appropriateEndTag)
                {
                    if (c.IsSpaceCharacter())
                    {
                        tag.Name = name;
                        return AttributeBeforeName(tag);
                    }
                    else if (c == Symbols.Solidus)
                    {
                        tag.Name = name;
                        return TagSelfClosing(tag);
                    }
                    else if (c == Symbols.GreaterThan)
                    {
                        tag.Name = name;
                        return EmitTag(tag);
                    }
                }
                
                if (!c.IsLetter())
                {
                    _buffer.Append(Symbols.LessThan).Append(Symbols.Solidus).Append(_stringBuffer.ToString());
                    return ScriptData(c);
                }

                _stringBuffer.Append(c);
            }
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
                        _buffer.Append(Symbols.Minus);
                        c = GetNext();

                        //See 8.2.4.23 Script data escaped dash state
                        switch (c)
                        {
                            case Symbols.Minus:
                                _buffer.Append(Symbols.Minus);
                                return ScriptDataEscapedDashDash();
                            case Symbols.LessThan:
                                return ScriptDataEscapedLT();
                            case Symbols.Null:
                                RaiseErrorOccurred(ErrorCode.Null);
                                _buffer.Append(Symbols.Replacement);
                                break;
                            case Symbols.EndOfFile:
                                return HtmlToken.EndOfFile;
                            default:
                                _buffer.Append(c);
                                break;
                        }

                        break;
                    case Symbols.LessThan:
                        return ScriptDataEscapedLT();
                    case Symbols.Null:
                        RaiseErrorOccurred(ErrorCode.Null);
                        _buffer.Append(Symbols.Replacement);
                        break;
                    case Symbols.EndOfFile:
                        return HtmlToken.EndOfFile;
                    default:
                        return ScriptData(c);
                }

                c = GetNext();
            }
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
                        _buffer.Append(Symbols.Minus);
                        break;
                    case Symbols.LessThan:
                        return ScriptDataEscapedLT();
                    case Symbols.GreaterThan:
                        _buffer.Append(Symbols.GreaterThan);
                        return ScriptData(GetNext());
                    case Symbols.Null:
                        RaiseErrorOccurred(ErrorCode.Null);
                        _buffer.Append(Symbols.Replacement);
                        return ScriptDataEscaped(GetNext());
                    case Symbols.EndOfFile:
                        return HtmlToken.EndOfFile;
                    default:
                        _buffer.Append(c);
                        return ScriptDataEscaped(GetNext());
                }
            }
        }

        /// <summary>
        /// See 8.2.4.25 Script data escaped less-than sign state
        /// </summary>
        HtmlToken ScriptDataEscapedLT()
        {
            var c = GetNext();

            if (c == Symbols.Solidus)
                return ScriptDataEscapedEndTag();

            if (c.IsLetter())
            {
                _stringBuffer.Clear().Append(c);
                _buffer.Append(Symbols.LessThan).Append(c);
                return ScriptDataStartDoubleEscape();
            }

            _buffer.Append(Symbols.LessThan);
            return ScriptDataEscaped(c);
        }

        /// <summary>
        /// See 8.2.4.26 Script data escaped end tag open state
        /// </summary>
        /// <returns>The emitted token.</returns>
        HtmlToken ScriptDataEscapedEndTag()
        {
            var c = GetNext();

            if (c.IsLetter())
            {
                var tag = HtmlTagToken.Close();
                _stringBuffer.Clear().Append(c);
                return ScriptDataEscapedNameTag(tag);
            }

            _buffer.Append(Symbols.LessThan).Append(Symbols.Solidus);
            return ScriptDataEscaped(c);
        }

        /// <summary>
        /// See 8.2.4.27 Script data escaped end tag name state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken ScriptDataEscapedNameTag(HtmlTagToken tag)
        {
            while (true)
            {
                var c = GetNext();
                var name = _stringBuffer.ToString().ToLowerInvariant();
                var appropriateEndTag = name == _lastStartTag;

                if (appropriateEndTag)
                {
                    if (c.IsSpaceCharacter())
                    {
                        tag.Name = name;
                        return AttributeBeforeName(tag);
                    }
                    else if (c == Symbols.Solidus)
                    {
                        tag.Name = name;
                        return TagSelfClosing(tag);
                    }
                    else if (c == Symbols.GreaterThan)
                    {
                        tag.Name = name;
                        return EmitTag(tag);
                    }
                }

                if (!c.IsLetter())
                {
                    _buffer.Append(Symbols.LessThan).Append(Symbols.Solidus).Append(_stringBuffer.ToString());
                    return ScriptDataEscaped(c);
                }

                _stringBuffer.Append(c);
            }
        }

        /// <summary>
        /// See 8.2.4.28 Script data double escape start state
        /// </summary>
        HtmlToken ScriptDataStartDoubleEscape()
        {
            while (true)
            {
                var c = GetNext();

                if (c == Symbols.Solidus || c == Symbols.GreaterThan || c.IsSpaceCharacter())
                {
                    _buffer.Append(c);

                    if (_stringBuffer.ToString().Equals(Tags.Script, StringComparison.OrdinalIgnoreCase))
                        return ScriptDataEscapedDouble(GetNext());

                    return ScriptDataEscaped(GetNext());
                }
                else if (c.IsLetter())
                {
                    _stringBuffer.Append(c);
                    _buffer.Append(c);
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
                        _buffer.Append(Symbols.Minus);
                        //See 8.2.4.30 Script data double escaped dash state
                        c = GetNext();

                        switch (c)
                        {
                            case Symbols.Minus:
                                _buffer.Append(Symbols.Minus);
                                return ScriptDataEscapedDoubleDashDash();
                            case Symbols.LessThan:
                                _buffer.Append(Symbols.LessThan);
                                return ScriptDataEscapedDoubleLT();
                            case Symbols.Null:
                                RaiseErrorOccurred(ErrorCode.Null);
                                c = Symbols.Replacement;
                                break;
                            case Symbols.EndOfFile:
                                RaiseErrorOccurred(ErrorCode.EOF);
                                return HtmlToken.EndOfFile;
                        }
                        break;
                    case Symbols.LessThan:
                        _buffer.Append(Symbols.LessThan);
                        return ScriptDataEscapedDoubleLT();
                    case Symbols.Null:
                        RaiseErrorOccurred(ErrorCode.Null);
                        _buffer.Append(Symbols.Replacement);
                        break;
                    case Symbols.EndOfFile:
                        RaiseErrorOccurred(ErrorCode.EOF);
                        return HtmlToken.EndOfFile;
                }

                _buffer.Append(c);
                c = GetNext();
            }
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
                        _buffer.Append(Symbols.Minus);
                        break;
                    case Symbols.LessThan:
                        _buffer.Append(Symbols.LessThan);
                        return ScriptDataEscapedDoubleLT();
                    case Symbols.GreaterThan:
                        _buffer.Append(Symbols.GreaterThan);
                        return ScriptData(GetNext());
                    case Symbols.Null:
                        RaiseErrorOccurred(ErrorCode.Null);
                        _buffer.Append(Symbols.Replacement);
                        return ScriptDataEscapedDouble(GetNext());
                    case Symbols.EndOfFile:
                        RaiseErrorOccurred(ErrorCode.EOF);
                        return HtmlToken.EndOfFile;
                    default:
                        _buffer.Append(c);
                        return ScriptDataEscapedDouble(GetNext());
                }
            }
        }

        /// <summary>
        /// See 8.2.4.32 Script data double escaped less-than sign state
        /// </summary>
        HtmlToken ScriptDataEscapedDoubleLT()
        {
            var c = GetNext();

            if (c == Symbols.Solidus)
            {
                _stringBuffer.Clear();
                _buffer.Append(Symbols.Solidus);
                return ScriptDataEndDoubleEscape();
            }

            return ScriptDataEscapedDouble(c);
        }

        /// <summary>
        /// See 8.2.4.33 Script data double escape end state
        /// </summary>
        HtmlToken ScriptDataEndDoubleEscape()
        {
            while (true)
            {
                var c = GetNext();

                if (c.IsSpaceCharacter() || c == Symbols.Solidus || c == Symbols.GreaterThan)
                {
                    _buffer.Append(c);

                    if (_stringBuffer.ToString().Equals(Tags.Script, StringComparison.OrdinalIgnoreCase))
                        return ScriptDataEscaped(GetNext());

                    return ScriptDataEscapedDouble(GetNext());
                }
                else if (c.IsLetter())
                {
                    _stringBuffer.Append(c);
                    _buffer.Append(c);
                }
                else
                {
                    return ScriptDataEscapedDouble(c);
                }
            }
        }

        #endregion

        #region Helpers

        HtmlToken Emit(HtmlToken token)
        {
            return token;
        }

        HtmlToken EmitComment()
        {
            var comment = HtmlToken.Comment(_stringBuffer.ToString());
            return Emit(comment);
        }

        HtmlToken EmitTag(HtmlTagToken tag)
        {
            _state = HtmlParseMode.PCData;
            var attributes = tag.Attributes;

            if (tag.Type == HtmlTokenType.StartTag)
            {
                for (var i = attributes.Count - 1; i > 0; i--)
                {
                    for (var j = i - 1; j >= 0; j--)
                    {
                        if (attributes[j].Key == attributes[i].Key)
                        {
                            attributes.RemoveAt(i);
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

                if (attributes.Count != 0)
                    RaiseErrorOccurred(ErrorCode.EndTagCannotHaveAttributes);
            }

            return Emit(tag);
        }

        #endregion
    }
}
