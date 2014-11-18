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
                        return TagOpen();

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
                        // See 8.2.4.11 RCDATA less-than sign state
                        var position = GetCurrentPosition();
                        c = Next;

                        if (c == Specification.Solidus)
                        {
                            _stringBuffer.Clear();
                            return RCDataEndTag(position);
                        }

                        _buffer.Append(Specification.LessThan);
                        continue;

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
        /// See 8.2.4.12 RCDATA end tag open state
        /// </summary>
        /// <param name="position">The start position.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken RCDataEndTag(TextPosition position)
        {
            var c = Next;

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
                _buffer.Append(Specification.LessThan)
                    .Append(Specification.Solidus);
                return RCData(c);
            }

            var tag = HtmlToken.CloseTag();
            tag.Start = position;
            return RCDataNameEndTag(tag);
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
                var c = Next;
                var name = _stringBuffer.ToString();
                var appropriateTag = name == _lastStartTag;

                if (appropriateTag && c.IsSpaceCharacter())
                {
                    tag.Name = name;
                    return AttributeBeforeName(tag);
                }
                else if (appropriateTag && c == Specification.Solidus)
                {
                    tag.Name = name;
                    return TagSelfClosing(tag);
                }
                else if (appropriateTag && c == Specification.GreaterThan)
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
                    _buffer.Append(Specification.LessThan)
                        .Append(Specification.Solidus)
                        .Append(_stringBuffer.ToString());
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
                    case Specification.LessThan:
                        return RawtextLT();

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
        HtmlToken RawtextLT()
        {
            var position = GetCurrentPosition();
            var c = Next;

            if (c == Specification.Solidus)
            {
                _stringBuffer.Clear();
                return RawtextEndTag(position);
            }

            _buffer.Append(Specification.LessThan);
            return Rawtext(c);
        }

        /// <summary>
        /// See 8.2.4.15 RAWTEXT end tag open state
        /// </summary>
        /// <param name="position">The start position.</param>
        HtmlToken RawtextEndTag(TextPosition position)
        {
            var c = Next;

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
                _buffer.Append(Specification.LessThan)
                    .Append(Specification.Solidus);
                return Rawtext(c);
            }

            var tag = HtmlToken.CloseTag();
            tag.Start = position;
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
                var c = Next;
                var name = _stringBuffer.ToString();
                var appropriateTag = name == _lastStartTag;

                if (appropriateTag && c.IsSpaceCharacter())
                {
                    tag.Name = name;
                    return AttributeBeforeName(tag);
                }
                else if (appropriateTag && c == Specification.Solidus)
                {
                    tag.Name = name;
                    return TagSelfClosing(tag);
                }
                else if (appropriateTag && c == Specification.GreaterThan)
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
                    _buffer.Append(Specification.LessThan)
                        .Append(Specification.Solidus)
                        .Append(_stringBuffer.ToString());
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
                var c = Next;

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
        HtmlToken TagOpen()
        {
            var position = GetCurrentPosition();
            var c = Next;

            if (c == Specification.Solidus)
            {
                return TagEnd(Next, position);
            }
            else if (c.IsLowercaseAscii())
            {
                var tag = HtmlToken.OpenTag();
                tag.Start = position;
                _stringBuffer.Clear().Append(c);
                return TagName(tag);
            }
            else if (c.IsUppercaseAscii())
            {
                var tag = HtmlToken.OpenTag();
                tag.Start = position;
                _stringBuffer.Clear().Append(Char.ToLower(c));
                return TagName(tag);
            }
            else if (c == Specification.ExclamationMark)
            {
                return MarkupDeclaration(position);
            }
            else if (c == Specification.QuestionMark)
            {
                RaiseErrorOccurred(ErrorCode.BogusComment);
                return BogusComment(c, position);
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
        /// <param name="position">The start position.</param>
        HtmlToken TagEnd(Char c, TextPosition position)
        {
            if (c.IsLowercaseAscii())
            {
                var tag = HtmlToken.CloseTag();
                tag.Start = position;
                _stringBuffer.Clear().Append(c);
                return TagName(tag);
            }
            else if (c.IsUppercaseAscii())
            {
                var tag = HtmlToken.CloseTag();
                tag.Start = position;
                _stringBuffer.Clear().Append(Char.ToLower(c));
                return TagName(tag);
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
                _buffer.Append(Specification.LessThan)
                    .Append(Specification.Solidus);
                return HtmlToken.EOF;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.BogusComment);
                return BogusComment(c, position);
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
                var c = Next;

                if (c.IsSpaceCharacter())
                {
                    tag.Name = _stringBuffer.ToString();
                    return AttributeBeforeName(tag);
                }
                else if (c == Specification.Solidus)
                {
                    tag.Name = _stringBuffer.ToString();
                    return TagSelfClosing(tag);
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
            }
        }

        /// <summary>
        /// See 8.2.4.43 Self-closing start tag state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken TagSelfClosing(HtmlTagToken tag)
        {
            switch (Next)
            {
                case Specification.GreaterThan:
                    tag.IsSelfClosing = true;
                    return EmitTag(tag);
                case Specification.EndOfFile:
                    RaiseErrorOccurred(ErrorCode.EOF);
                    return HtmlToken.EOF;
                default:
                    RaiseErrorOccurred(ErrorCode.ClosingSlashMisplaced);
                    Back();
                    return AttributeBeforeName(tag);
            }
        }

        /// <summary>
        /// See 8.2.4.45 Markup declaration open state
        /// </summary>
        /// <param name="position">The start position.</param>
        HtmlToken MarkupDeclaration(TextPosition position)
        {
            var c = Next;

            if (ContinuesWith("--"))
            {
                Advance();
                return CommentStart(position);
            }
            else if (ContinuesWith(Tags.Doctype))
            {
                Advance(6);
                return Doctype(position);
            }
            else if (_acceptsCharacterData && ContinuesWith("[CDATA[", ignoreCase: false))
            {
                Advance(6);
                return CData();
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.UndefinedMarkupDeclaration);
                return BogusComment(c, position);
            }
        }

        #endregion

        #region Comments

        /// <summary>
        /// See 8.2.4.44 Bogus comment state
        /// </summary>
        /// <param name="c">The current character.</param>
        /// <param name="position">The start position.</param>
        HtmlToken BogusComment(Char c, TextPosition position)
        {
            _stringBuffer.Clear();

            while(true)
            {
                switch (c)
                {
                    case Specification.GreaterThan:
                        break;
                    case Specification.EndOfFile:
                        Back();
                        break;
                    case Specification.Null:
                        _stringBuffer.Append(Specification.Replacement);
                        c = Next;
                        continue;
                    default:
                        _stringBuffer.Append(c);
                        c = Next;
                        continue;
                }

                _state = HtmlParseMode.PCData;
                return EmitComment(position);
            }
        }

        /// <summary>
        /// See 8.2.4.46 Comment start state
        /// </summary>
        /// <param name="position">The start position.</param>
        HtmlCommentToken CommentStart(TextPosition position)
        {
            var c = Next;
            _stringBuffer.Clear();

            switch (c)
            {
                case Specification.Minus:
                    return CommentDashStart(position);
                case Specification.Null:
                    RaiseErrorOccurred(ErrorCode.Null);
                    _stringBuffer.Append(Specification.Replacement);
                    return Comment(position);
                case Specification.GreaterThan:
                    _state = HtmlParseMode.PCData;
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    break;
                case Specification.EndOfFile:
                    RaiseErrorOccurred(ErrorCode.EOF);
                    Back();
                    break;
                default:
                    _stringBuffer.Append(c);
                    return Comment(position);
            }

            return EmitComment(position);
        }

        /// <summary>
        /// See 8.2.4.47 Comment start dash state
        /// </summary>
        /// <param name="position">The start position.</param>
        HtmlCommentToken CommentDashStart(TextPosition position)
        {
            var c = Next;

            switch (c)
            {
                case Specification.Minus:
                    return CommentEnd(position);
                case Specification.Null:
                    RaiseErrorOccurred(ErrorCode.Null);
                    _stringBuffer.Append(Specification.Minus)
                        .Append(Specification.Replacement);
                    return Comment(position);
                case Specification.GreaterThan:
                    _state = HtmlParseMode.PCData;
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    break;
                case Specification.EndOfFile:
                    RaiseErrorOccurred(ErrorCode.EOF);
                    Back();
                    break;
                default:
                    _stringBuffer.Append(Specification.Minus)
                        .Append(c);
                    return Comment(position);
            }

            return EmitComment(position);
        }

        /// <summary>
        /// See 8.2.4.48 Comment state
        /// </summary>
        /// <param name="position">The start position.</param>
        HtmlCommentToken Comment(TextPosition position)
        {
            while (true)
            {
                var c = Next;

                switch (c)
                {
                    case Specification.Minus:
                        var result = CommentDashEnd(position);

                        if (result != null)
                            return result;

                        continue;
                    case Specification.EndOfFile:
                        RaiseErrorOccurred(ErrorCode.EOF);
                        Back();
                        break;
                    case Specification.Null:
                        RaiseErrorOccurred(ErrorCode.Null);
                        c = Specification.Replacement;
                        _stringBuffer.Append(c);
                        continue;
                    default:
                        _stringBuffer.Append(c);
                        continue;
                }

                return EmitComment(position);
            }
        }

        /// <summary>
        /// See 8.2.4.49 Comment end dash state
        /// </summary>
        /// <param name="position">The start position.</param>
        HtmlCommentToken CommentDashEnd(TextPosition position)
        {
            var c = Next;

            switch (c)
            {
                case Specification.Minus:
                    return CommentEnd(position);
                case Specification.EndOfFile:
                    RaiseErrorOccurred(ErrorCode.EOF);
                    Back();
                    return EmitComment(position);
                case Specification.Null:
                    RaiseErrorOccurred(ErrorCode.Null);
                    c = Specification.Replacement;
                    break;
            }

            _stringBuffer.Append(Specification.Minus)
                .Append(c);
            return null;
        }

        /// <summary>
        /// See 8.2.4.50 Comment end state
        /// </summary>
        /// <param name="position">The start position.</param>
        HtmlCommentToken CommentEnd(TextPosition position)
        {
            while (true)
            {
                var c = Next;

                switch (c)
                {
                    case Specification.GreaterThan:
                        _state = HtmlParseMode.PCData;
                        break;
                    case Specification.Null:
                        RaiseErrorOccurred(ErrorCode.Null);
                        _stringBuffer.Append(Specification.Minus)
                            .Append(Specification.Replacement);
                        return null;
                    case Specification.ExclamationMark:
                        RaiseErrorOccurred(ErrorCode.CommentEndedWithEM);
                        return CommentBangEnd(position);
                    case Specification.Minus:
                        RaiseErrorOccurred(ErrorCode.CommentEndedWithDash);
                        _stringBuffer.Append(Specification.Minus);
                        continue;
                    case Specification.EndOfFile:
                        RaiseErrorOccurred(ErrorCode.EOF);
                        Back();
                        break;
                    default:
                        RaiseErrorOccurred(ErrorCode.CommentEndedUnexpected);
                        _stringBuffer.Append(Specification.Minus)
                            .Append(Specification.Minus)
                            .Append(c);
                        return null;
                }

                return EmitComment(position);
            }
        }

        /// <summary>
        /// See 8.2.4.51 Comment end bang state
        /// </summary>
        /// <param name="position">The start position.</param>
        HtmlCommentToken CommentBangEnd(TextPosition position)
        {
            var c = Next;

            switch (c)
            {
                case Specification.Minus:
                    _stringBuffer.Append(Specification.Minus)
                        .Append(Specification.Minus)
                        .Append(Specification.ExclamationMark);
                    return CommentDashEnd(position);
                case Specification.GreaterThan:
                    _state = HtmlParseMode.PCData;
                    break;
                case Specification.Null:
                    RaiseErrorOccurred(ErrorCode.Null);
                    _stringBuffer.Append(Specification.Minus)
                        .Append(Specification.Minus)
                        .Append(Specification.ExclamationMark)
                        .Append(Specification.Replacement);
                    return null;
                case Specification.EndOfFile:
                    RaiseErrorOccurred(ErrorCode.EOF);
                    Back();
                    break;
                default:
                    _stringBuffer.Append(Specification.Minus)
                        .Append(Specification.Minus)
                        .Append(Specification.ExclamationMark)
                        .Append(c);
                    return null;
            }

            return EmitComment(position);
        }

        #endregion

        #region Doctype

        /// <summary>
        /// See 8.2.4.52 DOCTYPE state
        /// </summary>
        /// <param name="position">The start position.</param>
        HtmlToken Doctype(TextPosition position)
        {
            var c = Next;

            if (c.IsSpaceCharacter())
                return DoctypeNameBefore(Next, position);
            else if (c == Specification.EndOfFile)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                Back();
                var doctype = HtmlToken.Doctype(true);
                doctype.Start = position;
                doctype.End = GetCurrentPosition();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypeUnexpected);
            return DoctypeNameBefore(c, position);
        }

        /// <summary>
        /// See 8.2.4.53 Before DOCTYPE name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="position">The start position.</param>
        HtmlToken DoctypeNameBefore(Char c, TextPosition position)
        {
            while (c.IsSpaceCharacter())
                c = Next;

            if (c.IsUppercaseAscii())
            {
                var doctype = HtmlToken.Doctype(false);
                doctype.Start = position;
                _stringBuffer.Clear().Append(Char.ToLower(c));
                return DoctypeName(doctype);
            }
            else if (c == Specification.Null)
            {
                var doctype = HtmlToken.Doctype(false);
                doctype.Start = position;
                RaiseErrorOccurred(ErrorCode.Null);
                _stringBuffer.Clear().Append(Specification.Replacement);
                return DoctypeName(doctype);
            }
            else if (c == Specification.GreaterThan)
            {
                var doctype = HtmlToken.Doctype(true);
                doctype.Start = position;
                _state = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.End = GetCurrentPosition();
                return doctype;
            }
            else if (c == Specification.EndOfFile)
            {
                var doctype = HtmlToken.Doctype(true);
                doctype.Start = position;
                RaiseErrorOccurred(ErrorCode.EOF);
                Back();
                doctype.End = GetCurrentPosition();
                return doctype;
            }
            else
            {
                var doctype = HtmlToken.Doctype(false);
                doctype.Start = position;
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
                var c = Next;

                if (c.IsSpaceCharacter())
                {
                    doctype.Name = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return DoctypeNameAfter(doctype);
                }
                else if (c == Specification.GreaterThan)
                {
                    _state = HtmlParseMode.PCData;
                    doctype.Name = _stringBuffer.ToString();
                    break;
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
                    break;
                }
                else
                    _stringBuffer.Append(c);
            }

            doctype.End = GetCurrentPosition();
            return doctype;
        }

        /// <summary>
        /// See 8.2.4.55 After DOCTYPE name state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypeNameAfter(HtmlDoctypeToken doctype)
        {
            var c = SkipSpaces();

            if (c == Specification.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
            }
            else if (c == Specification.EndOfFile)
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

            doctype.End = GetCurrentPosition();
            return doctype;
        }

        /// <summary>
        /// See 8.2.4.56 After DOCTYPE public keyword state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypePublic(HtmlDoctypeToken doctype)
        {
            var c = Next;

            if (c.IsSpaceCharacter())
            {
                return DoctypePublicIdentifierBefore(doctype);
            }
            else if (c == Specification.DoubleQuote)
            {
                RaiseErrorOccurred(ErrorCode.DoubleQuotationMarkUnexpected);
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierDoubleQuoted(doctype);
            }
            else if (c == Specification.SingleQuote)
            {
                RaiseErrorOccurred(ErrorCode.SingleQuotationMarkUnexpected);
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierSingleQuoted(doctype);
            }
            else if (c == Specification.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.IsQuirksForced = true;
            }
            else if (c == Specification.EndOfFile)
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

            doctype.End = GetCurrentPosition();
            return doctype;
        }

        /// <summary>
        /// See 8.2.4.57 Before DOCTYPE public identifier state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypePublicIdentifierBefore(HtmlDoctypeToken doctype)
        {
            var c = SkipSpaces();

            if (c == Specification.DoubleQuote)
            {
                _stringBuffer.Clear();
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierDoubleQuoted(doctype);
            }
            else if (c == Specification.SingleQuote)
            {
                _stringBuffer.Clear();
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierSingleQuoted(doctype);
            }
            else if (c == Specification.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.IsQuirksForced = true;
            }
            else if (c == Specification.EndOfFile)
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

            doctype.End = GetCurrentPosition();
            return doctype;
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
                var c = Next;

                if (c == Specification.DoubleQuote)
                {
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return DoctypePublicIdentifierAfter(doctype);
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
                    break;
                }
                else if (c == Specification.EndOfFile)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    Back();
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    break;
                }
                else
                    _stringBuffer.Append(c);
            }

            doctype.End = GetCurrentPosition();
            return doctype;
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
                var c = Next;

                if (c == Specification.SingleQuote)
                {
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return DoctypePublicIdentifierAfter(doctype);
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
                    break;
                }
                else if (c == Specification.EndOfFile)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    Back();
                    break;
                }
                else
                    _stringBuffer.Append(c);
            }

            doctype.End = GetCurrentPosition();
            return doctype;
        }

        /// <summary>
        /// See 8.2.4.60 After DOCTYPE public identifier state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypePublicIdentifierAfter(HtmlDoctypeToken doctype)
        {
            var c = Next;

            if (c.IsSpaceCharacter())
            {
                _stringBuffer.Clear();
                return DoctypeBetween(doctype);
            }
            else if (c == Specification.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
            }
            else if (c == Specification.DoubleQuote)
            {
                RaiseErrorOccurred(ErrorCode.DoubleQuotationMarkUnexpected);
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(doctype);
            }
            else if (c == Specification.SingleQuote)
            {
                RaiseErrorOccurred(ErrorCode.SingleQuotationMarkUnexpected);
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierSingleQuoted(doctype);
            }
            else if (c == Specification.EndOfFile)
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

            doctype.End = GetCurrentPosition();
            return doctype;
        }

        /// <summary>
        /// See 8.2.4.61 Between DOCTYPE public and system identifiers state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypeBetween(HtmlDoctypeToken doctype)
        {
            var c = SkipSpaces();

            if (c == Specification.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
            }
            else if (c == Specification.DoubleQuote)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(doctype);
            }
            else if (c == Specification.SingleQuote)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierSingleQuoted(doctype);
            }
            else if (c == Specification.EndOfFile)
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

            doctype.End = GetCurrentPosition();
            return doctype;
        }

        /// <summary>
        /// See 8.2.4.62 After DOCTYPE system keyword state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypeSystem(HtmlDoctypeToken doctype)
        {
            var c = Next;

            if (c.IsSpaceCharacter())
            {
                _state = HtmlParseMode.PCData;
                return DoctypeSystemIdentifierBefore(doctype);
            }
            else if (c == Specification.DoubleQuote)
            {
                RaiseErrorOccurred(ErrorCode.DoubleQuotationMarkUnexpected);
                doctype.SystemIdentifier = string.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(doctype);
            }
            else if (c == Specification.SingleQuote)
            {
                RaiseErrorOccurred(ErrorCode.SingleQuotationMarkUnexpected);
                doctype.SystemIdentifier = string.Empty;
                return DoctypeSystemIdentifierSingleQuoted(doctype);
            }
            else if (c == Specification.GreaterThan)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.SystemIdentifier = _stringBuffer.ToString();
                doctype.IsQuirksForced = true;
            }
            else if (c == Specification.EndOfFile)
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

            doctype.End = GetCurrentPosition();
            return doctype;
        }

        /// <summary>
        /// See 8.2.4.63 Before DOCTYPE system identifier state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypeSystemIdentifierBefore(HtmlDoctypeToken doctype)
        {
            var c = SkipSpaces();

            if (c == Specification.DoubleQuote)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(doctype);
            }
            else if (c == Specification.SingleQuote)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierSingleQuoted(doctype);
            }
            else if (c == Specification.GreaterThan)
            {
                _state = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = _stringBuffer.ToString();
            }
            else if (c == Specification.EndOfFile)
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

            doctype.End = GetCurrentPosition();
            return doctype;
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
                var c = Next;

                if (c == Specification.DoubleQuote)
                {
                    doctype.SystemIdentifier = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return DoctypeSystemIdentifierAfter(doctype);
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
                    break;
                }
                else if (c == Specification.EndOfFile)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    doctype.IsQuirksForced = true;
                    doctype.SystemIdentifier = _stringBuffer.ToString();
                    Back();
                    break;
                }
                else
                    _stringBuffer.Append(c);
            }

            doctype.End = GetCurrentPosition();
            return doctype;
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
                var c = Next;

                switch (c)
                {
                    case Specification.SingleQuote:
                        doctype.SystemIdentifier = _stringBuffer.ToString();
                        _stringBuffer.Clear();
                        return DoctypeSystemIdentifierAfter(doctype);
                    case Specification.Null:
                        RaiseErrorOccurred(ErrorCode.Null);
                        _stringBuffer.Append(Specification.Replacement);
                        continue;
                    case Specification.GreaterThan:
                        _state = HtmlParseMode.PCData;
                        RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                        doctype.IsQuirksForced = true;
                        doctype.SystemIdentifier = _stringBuffer.ToString();
                        break;
                    case Specification.EndOfFile:
                        RaiseErrorOccurred(ErrorCode.EOF);
                        doctype.IsQuirksForced = true;
                        doctype.SystemIdentifier = _stringBuffer.ToString();
                        Back();
                        break;
                    default:
                        _stringBuffer.Append(c);
                        continue;
                }

                doctype.End = GetCurrentPosition();
                return doctype;
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
                case Specification.GreaterThan:
                    _state = HtmlParseMode.PCData;
                    break;
                case Specification.EndOfFile:
                    RaiseErrorOccurred(ErrorCode.EOF);
                    doctype.IsQuirksForced = true;
                    Back();
                    break;
                default:
                    RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
                    return BogusDoctype(doctype);
            }

            doctype.End = GetCurrentPosition();
            return doctype;
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
                switch (Next)
                {
                    case Specification.GreaterThan:
                        _state = HtmlParseMode.PCData;
                        break;
                    case Specification.EndOfFile:
                        Back();
                        break;
                    default:
                        continue;
                }

                doctype.End = GetCurrentPosition();
                return doctype;
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

            if (c == Specification.Solidus)
            {
                return TagSelfClosing(tag);
            }
            else if (c == Specification.GreaterThan)
            {
                return EmitTag(tag);
            }
            else if (c.IsUppercaseAscii())
            {
                _stringBuffer.Clear().Append(Char.ToLower(c));
                return AttributeName(tag);
            }
            else if (c == Specification.Null)
            {
                RaiseErrorOccurred(ErrorCode.Null);
                _stringBuffer.Clear().Append(Specification.Replacement);
                return AttributeName(tag);
            }
            else if (c == Specification.SingleQuote || c == Specification.DoubleQuote || c == Specification.Equality || c == Specification.LessThan)
            {
                RaiseErrorOccurred(ErrorCode.AttributeNameInvalid);
                _stringBuffer.Clear().Append(c);
                return AttributeName(tag);
            }
            else if (c == Specification.EndOfFile)
            {
                return HtmlToken.EOF;
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
                var c = Next;

                if (c.IsSpaceCharacter())
                {
                    tag.AddAttribute(_stringBuffer.ToString());
                    return AttributeAfterName(tag);
                }
                else if (c == Specification.Solidus)
                {
                    tag.AddAttribute(_stringBuffer.ToString());
                    return TagSelfClosing(tag);
                }
                else if (c == Specification.Equality)
                {
                    tag.AddAttribute(_stringBuffer.ToString());
                    return AttributeBeforeValue(tag);
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

            if (c == Specification.Solidus)
            {
                return TagSelfClosing(tag);
            }
            else if (c == Specification.Equality)
            {
                return AttributeBeforeValue(tag);
            }
            else if (c == Specification.GreaterThan)
            {
                return EmitTag(tag);
            }
            else if (c.IsUppercaseAscii())
            {
                _stringBuffer.Clear().Append(Char.ToLower(c));
                return AttributeName(tag);
            }
            else if (c == Specification.Null)
            {
                RaiseErrorOccurred(ErrorCode.Null);
                _stringBuffer.Clear().Append(Specification.Replacement);
                return AttributeName(tag);
            }
            else if (c == Specification.DoubleQuote || c == Specification.SingleQuote || c == Specification.LessThan)
            {
                RaiseErrorOccurred(ErrorCode.AttributeNameInvalid);
                _stringBuffer.Clear().Append(c);
                return AttributeName(tag);
            }
            else if (c == Specification.EndOfFile)
            {
                return HtmlToken.EOF;
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

            if (c == Specification.DoubleQuote)
            {
                _stringBuffer.Clear();
                return AttributeDoubleQuotedValue(tag);
            }
            else if (c == Specification.Ampersand)
            {
                _stringBuffer.Clear();
                return AttributeUnquotedValue(c, tag);
            }
            else if (c == Specification.SingleQuote)
            {
                _stringBuffer.Clear();
                return AttributeSingleQuotedValue(tag);
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
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken AttributeDoubleQuotedValue(HtmlTagToken tag)
        {
            while (true)
            {
                var c = Next;

                if (c == Specification.DoubleQuote)
                {
                    tag.SetAttributeValue(_stringBuffer.ToString());
                    return AttributeAfterValue(tag);
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
                var c = Next;

                if (c == Specification.SingleQuote)
                {
                    tag.SetAttributeValue(_stringBuffer.ToString());
                    return AttributeAfterValue(tag);
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
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken AttributeAfterValue(HtmlTagToken tag)
        {
            var c = Next;

            if (c.IsSpaceCharacter())
                return AttributeBeforeName(tag);
            else if (c == Specification.Solidus)
                return TagSelfClosing(tag);
            else if (c == Specification.GreaterThan)
                return EmitTag(tag);
            else if (c == Specification.EndOfFile)
                return HtmlTagToken.EOF;

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
                    case Specification.LessThan:
                        //See 8.2.4.17 Script data less-than sign state
                        var position = GetCurrentPosition();
                        c = Next;

                        if (c == Specification.Solidus)
                        {
                            //See 8.2.4.18 Script data end tag open state
                            c = Next;

                            if (c.IsLetter())
                            {
                                var tag = HtmlToken.CloseTag();
                                tag.Start = position;
                                _stringBuffer.Clear().Append(c);
                                return ScriptDataNameEndTag(tag);
                            }

                            _buffer.Append(Specification.LessThan)
                                .Append(Specification.Solidus);
                            continue;
                        }

                        _buffer.Append(Specification.LessThan);

                        if (c == Specification.ExclamationMark)
                        {
                            //See 8.2.4.20 Script data escape start state
                            c = Next;
                            _buffer.Append(Specification.ExclamationMark);

                            if (c == Specification.Minus)
                            {
                                //See 8.2.4.21 Script data escape start dash state
                                c = Next;
                                _buffer.Append(Specification.Minus);

                                if (c == Specification.Minus)
                                {
                                    _buffer.Append(Specification.Minus);
                                    return ScriptDataEscapedDashDash();
                                }
                            }
                        }

                        continue;

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
        /// See 8.2.4.19 Script data end tag name state
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken ScriptDataNameEndTag(HtmlTagToken tag)
        {
            while (true)
            {
                var c = Next;
                var name = _stringBuffer.ToString().ToLowerInvariant();
                var appropriateEndTag = name == _lastStartTag;

                if (appropriateEndTag)
                {
                    if (c.IsSpaceCharacter())
                    {
                        tag.Name = name;
                        return AttributeBeforeName(tag);
                    }
                    else if (c == Specification.Solidus)
                    {
                        tag.Name = name;
                        return TagSelfClosing(tag);
                    }
                    else if (c == Specification.GreaterThan)
                    {
                        tag.Name = name;
                        return EmitTag(tag);
                    }
                }
                
                if (!c.IsLetter())
                {
                    _buffer.Append(Specification.LessThan)
                        .Append(Specification.Solidus)
                        .Append(_stringBuffer.ToString());
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
                    case Specification.Minus:
                        _buffer.Append(Specification.Minus);
                        c = Next;

                        //See 8.2.4.23 Script data escaped dash state
                        switch (c)
                        {
                            case Specification.Minus:
                                _buffer.Append(Specification.Minus);
                                return ScriptDataEscapedDashDash();
                            case Specification.LessThan:
                                return ScriptDataEscapedLT();
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

                        break;
                    case Specification.LessThan:
                        return ScriptDataEscapedLT();
                    case Specification.Null:
                        RaiseErrorOccurred(ErrorCode.Null);
                        _buffer.Append(Specification.Replacement);
                        break;
                    case Specification.EndOfFile:
                        return HtmlToken.EOF;
                    default:
                        return ScriptData(c);
                }

                c = Next;
            }
        }

        /// <summary>
        /// See 8.2.4.24 Script data escaped dash dash state
        /// </summary>
        HtmlToken ScriptDataEscapedDashDash()
        {
            while (true)
            {
                var c = Next;

                switch (c)
                {
                    case Specification.Minus:
                        _buffer.Append(Specification.Minus);
                        break;
                    case Specification.LessThan:
                        return ScriptDataEscapedLT();
                    case Specification.GreaterThan:
                        _buffer.Append(Specification.GreaterThan);
                        return ScriptData(Next);
                    case Specification.Null:
                        RaiseErrorOccurred(ErrorCode.Null);
                        _buffer.Append(Specification.Replacement);
                        return ScriptDataEscaped(Next);
                    case Specification.EndOfFile:
                        return HtmlToken.EOF;
                    default:
                        _buffer.Append(c);
                        return ScriptDataEscaped(Next);
                }
            }
        }

        /// <summary>
        /// See 8.2.4.25 Script data escaped less-than sign state
        /// </summary>
        HtmlToken ScriptDataEscapedLT()
        {
            var position = GetCurrentPosition();
            var c = Next;

            if (c == Specification.Solidus)
                return ScriptDataEscapedEndTag(position);

            if (c.IsLetter())
            {
                _stringBuffer.Clear().Append(c);
                _buffer.Append(Specification.LessThan)
                    .Append(c);
                return ScriptDataStartDoubleEscape();
            }

            _buffer.Append(Specification.LessThan);
            return ScriptDataEscaped(c);
        }

        /// <summary>
        /// See 8.2.4.26 Script data escaped end tag open state
        /// </summary>
        /// <param name="position">The start position.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken ScriptDataEscapedEndTag(TextPosition position)
        {
            var c = Next;

            if (c.IsLetter())
            {
                var tag = HtmlToken.CloseTag();
                tag.Start = position;
                _stringBuffer.Clear().Append(c);
                return ScriptDataEscapedNameTag(tag);
            }

            _buffer.Append(Specification.LessThan)
                .Append(Specification.Solidus);
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
                var c = Next;
                var name = _stringBuffer.ToString().ToLowerInvariant();
                var appropriateEndTag = name == _lastStartTag;

                if (appropriateEndTag)
                {
                    if (c.IsSpaceCharacter())
                    {
                        tag.Name = name;
                        return AttributeBeforeName(tag);
                    }
                    else if (c == Specification.Solidus)
                    {
                        tag.Name = name;
                        return TagSelfClosing(tag);
                    }
                    else if (c == Specification.GreaterThan)
                    {
                        tag.Name = name;
                        return EmitTag(tag);
                    }
                }

                if (!c.IsLetter())
                {
                    _buffer.Append(Specification.LessThan)
                        .Append(Specification.Solidus)
                        .Append(_stringBuffer.ToString());
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
                var c = Next;

                if (c == Specification.Solidus || c == Specification.GreaterThan || c.IsSpaceCharacter())
                {
                    _buffer.Append(c);

                    if (_stringBuffer.ToString().Equals(Tags.Script, StringComparison.OrdinalIgnoreCase))
                        return ScriptDataEscapedDouble(Next);

                    return ScriptDataEscaped(Next);
                }
                else if (c.IsLetter())
                {
                    _stringBuffer.Append(c);
                    _buffer.Append(c);
                }
                else
                    return ScriptDataEscaped(c);
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
                    case Specification.Minus:
                        _buffer.Append(Specification.Minus);
                        //See 8.2.4.30 Script data double escaped dash state
                        c = Next;

                        switch (c)
                        {
                            case Specification.Minus:
                                _buffer.Append(Specification.Minus);
                                return ScriptDataEscapedDoubleDashDash();
                            case Specification.LessThan:
                                _buffer.Append(Specification.LessThan);
                                return ScriptDataEscapedDoubleLT();
                            case Specification.Null:
                                RaiseErrorOccurred(ErrorCode.Null);
                                c = Specification.Replacement;
                                break;
                            case Specification.EndOfFile:
                                RaiseErrorOccurred(ErrorCode.EOF);
                                return HtmlToken.EOF;
                        }
                        break;
                    case Specification.LessThan:
                        _buffer.Append(Specification.LessThan);
                        return ScriptDataEscapedDoubleLT();
                    case Specification.Null:
                        RaiseErrorOccurred(ErrorCode.Null);
                        _buffer.Append(Specification.Replacement);
                        break;
                    case Specification.EndOfFile:
                        RaiseErrorOccurred(ErrorCode.EOF);
                        return HtmlToken.EOF;
                }

                _buffer.Append(c);
                c = Next;
            }
        }

        /// <summary>
        /// See 8.2.4.31 Script data double escaped dash dash state
        /// </summary>
        HtmlToken ScriptDataEscapedDoubleDashDash()
        {
            while (true)
            {
                var c = Next;

                switch (c)
                {
                    case Specification.Minus:
                        _buffer.Append(Specification.Minus);
                        break;
                    case Specification.LessThan:
                        _buffer.Append(Specification.LessThan);
                        return ScriptDataEscapedDoubleLT();
                    case Specification.GreaterThan:
                        _buffer.Append(Specification.GreaterThan);
                        return ScriptData(Next);
                    case Specification.Null:
                        RaiseErrorOccurred(ErrorCode.Null);
                        _buffer.Append(Specification.Replacement);
                        return ScriptDataEscapedDouble(Next);
                    case Specification.EndOfFile:
                        RaiseErrorOccurred(ErrorCode.EOF);
                        return HtmlToken.EOF;
                    default:
                        _buffer.Append(c);
                        return ScriptDataEscapedDouble(Next);
                }
            }
        }

        /// <summary>
        /// See 8.2.4.32 Script data double escaped less-than sign state
        /// </summary>
        HtmlToken ScriptDataEscapedDoubleLT()
        {
            var c = Next;

            if (c == Specification.Solidus)
            {
                _stringBuffer.Clear();
                _buffer.Append(Specification.Solidus);
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
                var c = Next;

                if (c.IsSpaceCharacter() || c == Specification.Solidus || c == Specification.GreaterThan)
                {
                    _buffer.Append(c);

                    if (_stringBuffer.ToString().Equals(Tags.Script, StringComparison.OrdinalIgnoreCase))
                        return ScriptDataEscaped(Next);

                    return ScriptDataEscapedDouble(Next);
                }
                else if (c.IsLetter())
                {
                    _stringBuffer.Append(c);
                    _buffer.Append(c);
                }
                else
                    return ScriptDataEscapedDouble(c);
            }
        }

        #endregion

        #region Helpers

        HtmlCommentToken EmitComment(TextPosition position)
        {
            var comment = HtmlToken.Comment(_stringBuffer.ToString());
            comment.Start = position;
            comment.End = GetCurrentPosition();
            return comment;
        }

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

            tag.End = GetCurrentPosition();
            return tag;
        }

        #endregion
    }
}
