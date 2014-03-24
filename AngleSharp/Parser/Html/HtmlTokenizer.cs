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
        #region Members

        Boolean _acceptsCharacterData;
        String _lastStartTag;
        HtmlParseMode _model;
        StringBuilder _buffer;
        HtmlToken _buffered;

        #endregion

        #region ctor

        /// <summary>
        /// See 8.2.4 Tokenization
        /// </summary>
        /// <param name="source">The source code manager.</param>
        public HtmlTokenizer(SourceManager source)
            : base(source)
        {
            _model = HtmlParseMode.PCData;
            _acceptsCharacterData = false;
            _buffer = new StringBuilder();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if CDATA sections are accepted.
        /// </summary>
        public Boolean AcceptsCharacterData
        {
            get { return _acceptsCharacterData; }
            set { _acceptsCharacterData = value; }
        }

        /// <summary>
        /// Gets the underlying stream.
        /// </summary>
        public SourceManager Stream
        {
            get { return _src; }
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

            if (_src.IsEnded) 
                return HtmlToken.EOF;

            switch (_model)
            {
                case HtmlParseMode.PCData:
                    token = Data(_src.Current);
                    break;

                case HtmlParseMode.RCData:
                    token = RCData(_src.Current);
                    break;

                case HtmlParseMode.Plaintext:
                    token = Plaintext(_src.Current);
                    break;

                case HtmlParseMode.Rawtext:
                    token = Rawtext(_src.Current);
                    break;

                case HtmlParseMode.Script:
                    token = ScriptData(_src.Current);
                    break;
            }

            if (_buffer.Length > 0)
            {
                _buffered = token;
                token = HtmlToken.Character(_buffer.ToString());
                _buffer.Clear();
            }

            _src.Advance();
            return token;
        }

        /// <summary>
        /// Switches the current tokenization state
        /// to the desired content model.
        /// </summary>
        /// <param name="state">The new state.</param>
        public void Switch(HtmlParseMode state)
        {
            _model = state;
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
                    case Specification.NULL:
                        RaiseErrorOccurred(ErrorCode.NULL);
                        _buffer.Append(Specification.REPLACEMENT);
                        break;

                    case Specification.EOF:
                        return HtmlToken.EOF;

                    default:
                        _buffer.Append(c);
                        break;
                }

                c = _src.Next;
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
                    case Specification.AMPERSAND:
                        var value = CharacterReference(_src.Next);

                        if (value == null)
                            _buffer.Append(Specification.AMPERSAND);

                        _buffer.Append(value);
                        break;

                    case Specification.LT:
                        return TagOpen(_src.Next);

                    case Specification.NULL:
                        RaiseErrorOccurred(ErrorCode.NULL);
                        return Data(_src.Next);

                    case Specification.EOF:
                        return HtmlToken.EOF;

                    default:
                        _buffer.Append(c);
                        break;
                }

                c = _src.Next;
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
                    case Specification.AMPERSAND:
                        var value = CharacterReference(_src.Next);

                        if (value == null)
                            _buffer.Append(Specification.AMPERSAND);

                        _buffer.Append(value);
                        break;

                    case Specification.LT:
                        return RCDataLT(_src.Next);

                    case Specification.NULL:
                        RaiseErrorOccurred(ErrorCode.NULL);
                        _buffer.Append(Specification.REPLACEMENT);
                        break;

                    case Specification.EOF:
                        return HtmlToken.EOF;

                    default:
                        _buffer.Append(c);
                        break;
                }

                c = _src.Next;
            }
        }

        /// <summary>
        /// See 8.2.4.11 RCDATA less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken RCDataLT(Char c)
        {
            if (c == Specification.SOLIDUS)
            {
                _stringBuffer.Clear();
                return RCDataEndTag(_src.Next);
            }

            _buffer.Append(Specification.LT);
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
                return RCDataNameEndTag(_src.Next, HtmlToken.CloseTag());
            }
            else if (c.IsLowercaseAscii())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return RCDataNameEndTag(_src.Next, HtmlToken.CloseTag());
            }

            _buffer.Append(Specification.LT).Append(Specification.SOLIDUS);
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
                return AttributeBeforeName(_src.Next, tag);
            }
            else if (appropriateTag && c == Specification.SOLIDUS)
            {
                tag.Name = name;
                return TagSelfClosing(_src.Next, tag);
            }
            else if (appropriateTag && c == Specification.GT)
            {
                tag.Name = name;
                return EmitTag(tag);
            }
            else if (c.IsUppercaseAscii())
            {
                _stringBuffer.Append(Char.ToLower(c));
                return RCDataNameEndTag(_src.Next, tag);
            }
            else if (c.IsLowercaseAscii())
            {
                _stringBuffer.Append(c);
                return RCDataNameEndTag(_src.Next, tag);
            }

            _buffer.Append(Specification.LT).Append(Specification.SOLIDUS);
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
                    case Specification.LT:
                        return RawtextLT(_src.Next);

                    case Specification.NULL:
                        RaiseErrorOccurred(ErrorCode.NULL);
                        _buffer.Append(Specification.REPLACEMENT);
                        break;

                    case Specification.EOF:
                        return HtmlToken.EOF;

                    default:
                        _buffer.Append(c);
                        break;
                }

                c = _src.Next;
            }
        }

        /// <summary>
        /// See 8.2.4.14 RAWTEXT less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken RawtextLT(Char c)
        {
            if (c == Specification.SOLIDUS)
            {
                _stringBuffer.Clear();
                return RawtextEndTag(_src.Next);
            }

            _buffer.Append(Specification.LT);
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
                return RawtextNameEndTag(_src.Next, HtmlToken.CloseTag());
            }
            else if (c.IsLowercaseAscii())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return RawtextNameEndTag(_src.Next, HtmlToken.CloseTag());
            }

            _buffer.Append(Specification.LT).Append(Specification.SOLIDUS);
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
                return AttributeBeforeName(_src.Next, tag);
            }
            else if (appropriateTag && c == Specification.SOLIDUS)
            {
                tag.Name = name;
                return TagSelfClosing(_src.Next, tag);
            }
            else if (appropriateTag && c == Specification.GT)
            {
                tag.Name = name;
                return EmitTag(tag);
            }
            else if (c.IsUppercaseAscii())
            {
                _stringBuffer.Append(Char.ToLower(c));
                return RawtextNameEndTag(_src.Next, tag);
            }
            else if (c.IsLowercaseAscii())
            {
                _stringBuffer.Append(c);
                return RawtextNameEndTag(_src.Next, tag);
            }

            _buffer.Append(Specification.LT).Append(Specification.SOLIDUS);
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
                if (c == Specification.EOF)
                {
                    _src.Back();
                    break;
                }
                else if (c == Specification.SBC && _src.ContinuesWith("]]>"))
                {
                    _src.Advance(2);
                    break;
                }

                _stringBuffer.Append(c);
                c = _src.Next;
            }

            return HtmlToken.Character(_stringBuffer.ToString());
        }

        /// <summary>
        /// See 8.2.4.69 Tokenizing character references
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="allowedCharacter">The additionally allowed character if there is one.</param>
        String CharacterReference(Char c, Char allowedCharacter = Specification.NULL)
        {
            if (c.IsSpaceCharacter() || c == Specification.LT || c == Specification.EOF || c == Specification.AMPERSAND || c == allowedCharacter)
            {
                _src.Back();
                return null;
            }

            if (c == Specification.NUM)
            {
                var exp = 10;
                var basis = 1;
                var num = 0;
                var nums = new List<Int32>();
                c = _src.Next;
                var isHex = c == 'x' || c == 'X';

                if (isHex)
                {
                    exp = 16;

                    while ((c = _src.Next).IsHex())
                        nums.Add(c.FromHex());
                }
                else
                {
                    while (c.IsDigit())
                    {
                        nums.Add(c.FromHex());
                        c = _src.Next;
                    }
                }

                for (var i = nums.Count - 1; i >= 0; i--)
                {
                    num += nums[i] * basis;
                    basis *= exp;
                }

                if (nums.Count == 0)
                {
                    _src.Back(2);

                    if (isHex)
                        _src.Back();

                    RaiseErrorOccurred(ErrorCode.CharacterReferenceWrongNumber);
                    return null;
                }

                if (c != Specification.SC)
                {
                    RaiseErrorOccurred(ErrorCode.CharacterReferenceSemicolonMissing);
                    _src.Back();
                }

                if (Entities.IsInCharacterTable(num))
                {
                    RaiseErrorOccurred(ErrorCode.CharacterReferenceInvalidCode);
                    return Entities.GetSymbolFromTable(num);
                }

                if (Entities.IsInvalidNumber(num))
                {
                    RaiseErrorOccurred(ErrorCode.CharacterReferenceInvalidNumber);
                    return Specification.REPLACEMENT.ToString();
                }

                if (Entities.IsInInvalidRange(num))
                    RaiseErrorOccurred(ErrorCode.CharacterReferenceInvalidRange);

                return Entities.Convert(num);
            }
            else
            {
                var last = String.Empty;
                var consumed = 0;
                var start = _src.InsertionPoint - 1;
                var reference = new Char[31];
                var index = 0;
                var chr = _src.Current;

                do
                {
                    if (chr == Specification.SC || !chr.IsName())
                        break;

                    reference[index++] = chr;
                    var value = new String(reference, 0, index);
                    chr = _src.Next;
                    consumed++;
                    value = chr == Specification.SC ? Entities.GetSymbol(value) : Entities.GetSymbolWithoutSemicolon(value);

                    if (value != null)
                    {
                        consumed = 0;
                        last = value;
                    }
                }
                while (!_src.IsEnded);

                _src.Back(consumed);
                chr = _src.Current;

                if (chr != Specification.SC)
                {
                    if (allowedCharacter != Specification.NULL && (chr == Specification.EQ || chr.IsAlphanumericAscii()))
                    {
                        if (chr == Specification.EQ)
                            RaiseErrorOccurred(ErrorCode.CharacterReferenceAttributeEqualsFound);

                        _src.InsertionPoint = start;
                        return null;
                    }

                    _src.Back();
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
            if (c == Specification.EM)
            {
                return MarkupDeclaration(_src.Next);
            }
            else if (c == Specification.SOLIDUS)
            {
                return TagEnd(_src.Next);
            }
            else if (c.IsUppercaseAscii())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(Char.ToLower(c));
                return TagName(_src.Next, HtmlToken.OpenTag());
            }
            else if (c.IsLowercaseAscii())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return TagName(_src.Next, HtmlToken.OpenTag());
            }
            else if (c == Specification.QM)
            {
                RaiseErrorOccurred(ErrorCode.BogusComment);
                return BogusComment(c);
            }

            _model = HtmlParseMode.PCData;
            RaiseErrorOccurred(ErrorCode.AmbiguousOpenTag);
            _buffer.Append(Specification.LT);
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
                return TagName(_src.Next, HtmlToken.CloseTag());
            }
            else if (c.IsLowercaseAscii())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return TagName(_src.Next, HtmlToken.CloseTag());
            }
            else if (c == Specification.GT)
            {
                _model = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return Data(_src.Next);
            }
            else if (c == Specification.EOF)
            {
                _src.Back();
                RaiseErrorOccurred(ErrorCode.EOF);
                _buffer.Append(Specification.LT).Append(Specification.SOLIDUS);
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
                    return AttributeBeforeName(_src.Next, tag);
                }
                else if (c == Specification.SOLIDUS)
                {
                    tag.Name = _stringBuffer.ToString();
                    return TagSelfClosing(_src.Next, tag);
                }
                else if (c == Specification.GT)
                {
                    tag.Name = _stringBuffer.ToString();
                    return EmitTag(tag);
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    _stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    return HtmlToken.EOF;
                }
                else if (c.IsUppercaseAscii())
                    _stringBuffer.Append(Char.ToLower(c));
                else
                    _stringBuffer.Append(c);

                c = _src.Next;
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
            if (c == Specification.GT)
            {
                tag.IsSelfClosing = true;
                return EmitTag(tag);
            }
            else if (c == Specification.EOF)
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
            if (_src.ContinuesWith("--"))
            {
                _src.Advance();
                return CommentStart(_src.Next);
            }
            else if (_src.ContinuesWith(Tags.DOCTYPE))
            {
                _src.Advance(6);
                return Doctype(_src.Next);
            }
            else if (_acceptsCharacterData && _src.ContinuesWith("[CDATA[", false))
            {
                _src.Advance(6);
                return CData(_src.Next);
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

                if (c == Specification.GT)
                    break;
                else if (c == Specification.EOF)
                {
                    _src.Back();
                    break;
                }
                else if (c == Specification.NULL)
                    _stringBuffer.Append(Specification.REPLACEMENT);
                else
                    _stringBuffer.Append(c);

                c = _src.Next;
            }

            _model = HtmlParseMode.PCData;
            return HtmlToken.Comment(_stringBuffer.ToString());
        }

        /// <summary>
        /// See 8.2.4.46 Comment start state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken CommentStart(Char c)
        {
            _stringBuffer.Clear();

            if (c == Specification.MINUS)
                return CommentDashStart(_src.Next);
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _stringBuffer.Append(Specification.REPLACEMENT);
                return Comment(_src.Next);
            }
            else if (c == Specification.GT)
            {
                _model = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return HtmlToken.Comment(_stringBuffer.ToString());
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return HtmlToken.Comment(_stringBuffer.ToString());
            }
            else
            {
                _stringBuffer.Append(c);
                return Comment(_src.Next);
            }
        }

        /// <summary>
        /// See 8.2.4.47 Comment start dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken CommentDashStart(Char c)
        {
            if (c == Specification.MINUS)
                return CommentEnd(_src.Next);
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _stringBuffer.Append(Specification.MINUS);
                _stringBuffer.Append(Specification.REPLACEMENT);
                return Comment(_src.Next);
            }
            else if (c == Specification.GT)
            {
                _model = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return HtmlToken.Comment(_stringBuffer.ToString());
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return HtmlToken.Comment(_stringBuffer.ToString());
            }

            _stringBuffer.Append(Specification.MINUS);
            _stringBuffer.Append(c);
            return Comment(_src.Next);
        }

        /// <summary>
        /// See 8.2.4.48 Comment state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken Comment(Char c)
        {
            while (true)
            {
                if (c == Specification.MINUS)
                    return CommentDashEnd(_src.Next);
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    _src.Back();
                    return HtmlToken.Comment(_stringBuffer.ToString());
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    c = Specification.REPLACEMENT;
                }

                _stringBuffer.Append(c);
                c = _src.Next;
            }
        }

        /// <summary>
        /// See 8.2.4.49 Comment end dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken CommentDashEnd(Char c)
        {
            if (c == Specification.MINUS)
                return CommentEnd(_src.Next);
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return HtmlToken.Comment(_stringBuffer.ToString());
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                c = Specification.REPLACEMENT;
            }

            _stringBuffer.Append(Specification.MINUS);
            _stringBuffer.Append(c);
            return Comment(_src.Next);
        }

        /// <summary>
        /// See 8.2.4.50 Comment end state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken CommentEnd(Char c)
        {
            if (c == Specification.GT)
            {
                _model = HtmlParseMode.PCData;
                return HtmlToken.Comment(_stringBuffer.ToString());
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _stringBuffer.Append(Specification.MINUS);
                _stringBuffer.Append(Specification.REPLACEMENT);
                return Comment(_src.Next);
            }
            else if (c == Specification.EM)
            {
                RaiseErrorOccurred(ErrorCode.CommentEndedWithEM);
                return CommentBangEnd(_src.Next);
            }
            else if (c == Specification.MINUS)
            {
                RaiseErrorOccurred(ErrorCode.CommentEndedWithDash);
                _stringBuffer.Append(Specification.MINUS);
                return CommentEnd(_src.Next);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return HtmlToken.Comment(_stringBuffer.ToString());
            }

            RaiseErrorOccurred(ErrorCode.CommentEndedUnexpected);
            _stringBuffer.Append(Specification.MINUS);
            _stringBuffer.Append(Specification.MINUS);
            _stringBuffer.Append(c);
            return Comment(_src.Next);
        }

        /// <summary>
        /// See 8.2.4.51 Comment end bang state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken CommentBangEnd(Char c)
        {
            if (c == Specification.MINUS)
            {
                _stringBuffer.Append(Specification.MINUS);
                _stringBuffer.Append(Specification.MINUS);
                _stringBuffer.Append(Specification.EM);
                return CommentDashEnd(_src.Next);
            }
            else if (c == Specification.GT)
            {
                _model = HtmlParseMode.PCData;
                return HtmlToken.Comment(_stringBuffer.ToString());
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _stringBuffer.Append(Specification.MINUS);
                _stringBuffer.Append(Specification.MINUS);
                _stringBuffer.Append(Specification.EM);
                _stringBuffer.Append(Specification.REPLACEMENT);
                return Comment(_src.Next);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return HtmlToken.Comment(_stringBuffer.ToString());
            }

            _stringBuffer.Append(Specification.MINUS);
            _stringBuffer.Append(Specification.MINUS);
            _stringBuffer.Append(Specification.EM);
            _stringBuffer.Append(c);
            return Comment(_src.Next);
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
                return DoctypeNameBefore(_src.Next);
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
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
                c = _src.Next;

            if (c.IsUppercaseAscii())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(Char.ToLower(c));
                return DoctypeName(_src.Next, HtmlToken.Doctype(false));
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _stringBuffer.Clear();
                _stringBuffer.Append(Specification.REPLACEMENT);
                return DoctypeName(_src.Next, HtmlToken.Doctype(false));
            }
            else if (c == Specification.GT)
            {
                _model = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return HtmlToken.Doctype(true);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return HtmlToken.Doctype(true);
            }

            _stringBuffer.Clear();
            _stringBuffer.Append(c);
            return DoctypeName(_src.Next, HtmlToken.Doctype(false));
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
                    return DoctypeNameAfter(_src.Next, doctype);
                }
                else if (c == Specification.GT)
                {
                    _model = HtmlParseMode.PCData;
                    doctype.Name = _stringBuffer.ToString();
                    return doctype;
                }
                else if (c.IsUppercaseAscii())
                    _stringBuffer.Append(Char.ToLower(c));
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    _stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    _src.Back();
                    doctype.IsQuirksForced = true;
                    doctype.Name = _stringBuffer.ToString();
                    return doctype;
                }
                else
                    _stringBuffer.Append(c);

                c = _src.Next;
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
                c = _src.Next;

            if (c == Specification.GT)
            {
                _model = HtmlParseMode.PCData;
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                doctype.IsQuirksForced = true;
                return doctype;
            }
            else if (_src.ContinuesWith("public"))
            {
                _src.Advance(5);
                return DoctypePublic(_src.Next, doctype);
            }
            else if (_src.ContinuesWith("system"))
            {
                _src.Advance(5);
                return DoctypeSystem(_src.Next, doctype);
            }

            RaiseErrorOccurred(ErrorCode.DoctypeUnexpectedAfterName);
            doctype.IsQuirksForced = true;
            return BogusDoctype(_src.Next, doctype);
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
                return DoctypePublicIdentifierBefore(_src.Next, doctype);
            }
            else if (c == Specification.DQ)
            {
                RaiseErrorOccurred(ErrorCode.DoubleQuotationMarkUnexpected);
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierDoubleQuoted(_src.Next, doctype);
            }
            else if (c == Specification.SQ)
            {
                RaiseErrorOccurred(ErrorCode.SingleQuotationMarkUnexpected);
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierSingleQuoted(_src.Next, doctype);
            }
            else if (c == Specification.GT)
            {
                _model = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.IsQuirksForced = true;
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                _src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypePublicInvalid);
            doctype.IsQuirksForced = true;
            return BogusDoctype(_src.Next, doctype);
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
                c = _src.Next;

            if (c == Specification.DQ)
            {
                _stringBuffer.Clear();
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierDoubleQuoted(_src.Next, doctype);
            }
            else if (c == Specification.SQ)
            {
                _stringBuffer.Clear();
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierSingleQuoted(_src.Next, doctype);
            }
            else if (c == Specification.GT)
            {
                _model = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.IsQuirksForced = true;
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                _src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypePublicInvalid);
            doctype.IsQuirksForced = true;
            return BogusDoctype(_src.Next, doctype);
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
                if (c == Specification.DQ)
                {
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return DoctypePublicIdentifierAfter(_src.Next, doctype); ;
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    _stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.GT)
                {
                    _model = HtmlParseMode.PCData;
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    return doctype;
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    _src.Back();
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    return doctype;
                }
                else
                    _stringBuffer.Append(c);

                c = _src.Next;
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
                if (c == Specification.SQ)
                {
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return DoctypePublicIdentifierAfter(_src.Next, doctype);
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    _stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.GT)
                {
                    _model = HtmlParseMode.PCData;
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    return doctype;
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    _src.Back();
                    return doctype;
                }
                else
                    _stringBuffer.Append(c);

                c = _src.Next;
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
                return DoctypeBetween(_src.Next, doctype);
            }
            else if (c == Specification.GT)
            {
                _model = HtmlParseMode.PCData;
                return doctype;
            }
            else if (c == Specification.DQ)
            {
                RaiseErrorOccurred(ErrorCode.DoubleQuotationMarkUnexpected);
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(_src.Next, doctype);
            }
            else if (c == Specification.SQ)
            {
                RaiseErrorOccurred(ErrorCode.SingleQuotationMarkUnexpected);
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierSingleQuoted(_src.Next, doctype);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                _src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
            doctype.IsQuirksForced = true;
            return BogusDoctype(_src.Next, doctype);
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
                c = _src.Next;

            if (c == Specification.GT)
            {
                _model = HtmlParseMode.PCData;
                return doctype;
            }
            else if (c == Specification.DQ)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(_src.Next, doctype);
            }
            else if (c == Specification.SQ)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierSingleQuoted(_src.Next, doctype);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                _src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
            doctype.IsQuirksForced = true;
            return BogusDoctype(_src.Next, doctype);
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
                _model = HtmlParseMode.PCData;
                return DoctypeSystemIdentifierBefore(_src.Next, doctype);
            }
            else if (c == Specification.DQ)
            {
                RaiseErrorOccurred(ErrorCode.DoubleQuotationMarkUnexpected);
                doctype.SystemIdentifier = string.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(_src.Next, doctype);
            }
            else if (c == Specification.SQ)
            {
                RaiseErrorOccurred(ErrorCode.SingleQuotationMarkUnexpected);
                doctype.SystemIdentifier = string.Empty;
                return DoctypeSystemIdentifierSingleQuoted(_src.Next, doctype);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.SystemIdentifier = _stringBuffer.ToString();
                doctype.IsQuirksForced = true;
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                _src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypeSystemInvalid);
            doctype.IsQuirksForced = true;
            return BogusDoctype(_src.Next, doctype);
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
                c = _src.Next;

            if (c == Specification.DQ)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(_src.Next, doctype);
            }
            else if (c == Specification.SQ)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierSingleQuoted(_src.Next, doctype);
            }
            else if (c == Specification.GT)
            {
                _model = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = _stringBuffer.ToString();
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = _stringBuffer.ToString();
                _src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
            doctype.IsQuirksForced = true;
            return BogusDoctype(_src.Next, doctype);
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
                if (c == Specification.DQ)
                {
                    doctype.SystemIdentifier = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return DoctypeSystemIdentifierAfter(_src.Next, doctype);
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    _stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.GT)
                {
                    _model = HtmlParseMode.PCData;
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    doctype.IsQuirksForced = true;
                    doctype.SystemIdentifier = _stringBuffer.ToString();
                    return doctype;
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    doctype.IsQuirksForced = true;
                    doctype.SystemIdentifier = _stringBuffer.ToString();
                    _src.Back();
                    return doctype;
                }
                else
                    _stringBuffer.Append(c);

                c = _src.Next;
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
                if (c == Specification.SQ)
                {
                    doctype.SystemIdentifier = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return DoctypeSystemIdentifierAfter(_src.Next, doctype);
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    _stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.GT)
                {
                    _model = HtmlParseMode.PCData;
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    doctype.IsQuirksForced = true;
                    doctype.SystemIdentifier = _stringBuffer.ToString();
                    return doctype;
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    doctype.IsQuirksForced = true;
                    doctype.SystemIdentifier = _stringBuffer.ToString();
                    _src.Back();
                    return doctype;
                }
                else
                    _stringBuffer.Append(c);

                c = _src.Next;
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
                c = _src.Next;

            if (c == Specification.GT)
            {
                _model = HtmlParseMode.PCData;
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                _src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
            return BogusDoctype(_src.Next, doctype);
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
                if (c == Specification.EOF)
                {
                    _src.Back();
                    return doctype;
                }
                else if (c == Specification.GT)
                {
                    _model = HtmlParseMode.PCData;
                    return doctype;
                }

                c = _src.Next;
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
                c = _src.Next;

            if (c == Specification.SOLIDUS)
            {
                return TagSelfClosing(_src.Next, tag);
            }
            else if (c == Specification.GT)
            {
                return EmitTag(tag);
            }
            else if (c.IsUppercaseAscii())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(Char.ToLower(c));
                return AttributeName(_src.Next, tag);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _stringBuffer.Clear();
                _stringBuffer.Append(Specification.REPLACEMENT);
                return AttributeName(_src.Next, tag);
            }
            else if (c == Specification.SQ || c == Specification.DQ || c == Specification.EQ || c == Specification.LT)
            {
                RaiseErrorOccurred(ErrorCode.AttributeNameInvalid);
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return AttributeName(_src.Next, tag);
            }
            else if (c == Specification.EOF)
            {
                return HtmlToken.EOF;
            }
            else
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return AttributeName(_src.Next, tag);
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
                    return AttributeAfterName(_src.Next, tag);
                }
                else if (c == Specification.SOLIDUS)
                {
                    tag.AddAttribute(_stringBuffer.ToString());
                    return TagSelfClosing(_src.Next, tag);
                }
                else if (c == Specification.EQ)
                {
                    tag.AddAttribute(_stringBuffer.ToString());
                    return AttributeBeforeValue(_src.Next, tag);
                }
                else if (c == Specification.GT)
                {
                    tag.AddAttribute(_stringBuffer.ToString());
                    return EmitTag(tag);
                }
                else if (c == Specification.EOF)
                    return HtmlToken.EOF;
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    _stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c.IsUppercaseAscii())
                    _stringBuffer.Append(Char.ToLower(c));
                else if (c == Specification.DQ || c == Specification.SQ || c == Specification.LT)
                {
                    RaiseErrorOccurred(ErrorCode.AttributeNameInvalid);
                    _stringBuffer.Append(c);
                }
                else
                    _stringBuffer.Append(c);

                c = _src.Next;
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
                c = _src.Next;

            if (c == Specification.SOLIDUS)
            {
                return TagSelfClosing(_src.Next, tag);
            }
            else if (c == Specification.EQ)
            {
                return AttributeBeforeValue(_src.Next, tag);
            }
            else if (c == Specification.GT)
            {
                return EmitTag(tag);
            }
            else if (c.IsUppercaseAscii())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(Char.ToLower(c));
                return AttributeName(_src.Next, tag);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _stringBuffer.Clear();
                _stringBuffer.Append(Specification.REPLACEMENT);
                return AttributeName(_src.Next, tag);
            }
            else if (c == Specification.DQ || c == Specification.SQ || c == Specification.LT)
            {
                RaiseErrorOccurred(ErrorCode.AttributeNameInvalid);
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return AttributeName(_src.Next, tag);
            }
            else if (c == Specification.EOF)
            {
                return HtmlToken.EOF;
            }
            else
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return AttributeName(_src.Next, tag);
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
                c = _src.Next;

            if (c == Specification.DQ)
            {
                _stringBuffer.Clear();
                return AttributeDoubleQuotedValue(_src.Next, tag);
            }
            else if (c == Specification.AMPERSAND)
            {
                _stringBuffer.Clear();
                return AttributeUnquotedValue(c, tag);
            }
            else if (c == Specification.SQ)
            {
                _stringBuffer.Clear();
                return AttributeSingleQuotedValue(_src.Next, tag);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _stringBuffer.Append(Specification.REPLACEMENT);
                return AttributeUnquotedValue(_src.Next, tag);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return EmitTag(tag);
            }
            else if (c == Specification.LT || c == Specification.EQ || c == Specification.CQ)
            {
                RaiseErrorOccurred(ErrorCode.AttributeValueInvalid);
                _stringBuffer.Clear().Append(c);
                return AttributeUnquotedValue(_src.Next, tag);
            }
            else if (c == Specification.EOF)
            {
                return HtmlToken.EOF;
            }
            else
            {
                _stringBuffer.Clear().Append(c);
                return AttributeUnquotedValue(_src.Next, tag);
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
                if (c == Specification.DQ)
                {
                    tag.SetAttributeValue(_stringBuffer.ToString());
                    return AttributeAfterValue(_src.Next, tag);
                }
                else if (c == Specification.AMPERSAND)
                {
                    var value = CharacterReference(_src.Next, Specification.DQ);

                    if (value == null)
                        _stringBuffer.Append(Specification.AMPERSAND);
                    else
                        _stringBuffer.Append(value);
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    _stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.EOF)
                    return HtmlToken.EOF;
                else
                    _stringBuffer.Append(c);

                c = _src.Next;
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
                if (c == Specification.SQ)
                {
                    tag.SetAttributeValue(_stringBuffer.ToString());
                    return AttributeAfterValue(_src.Next, tag);
                }
                else if (c == Specification.AMPERSAND)
                {
                    var value = CharacterReference(_src.Next, Specification.SQ);

                    if (value == null)
                        _stringBuffer.Append(Specification.AMPERSAND);
                    else
                        _stringBuffer.Append(value);
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    _stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.EOF)
                    return HtmlToken.EOF;
                else
                    _stringBuffer.Append(c);

                c = _src.Next;
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
                    return AttributeBeforeName(_src.Next, tag);
                }
                else if (c == Specification.AMPERSAND)
                {
                    var value = CharacterReference(_src.Next, Specification.GT);

                    if (value == null)
                        _stringBuffer.Append(Specification.AMPERSAND);
                    else
                        _stringBuffer.Append(value);
                }
                else if (c == Specification.GT)
                {
                    tag.SetAttributeValue(_stringBuffer.ToString());
                    return EmitTag(tag);
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    _stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.DQ || c == Specification.SQ || c == Specification.LT || c == Specification.EQ || c == Specification.CQ)
                {
                    RaiseErrorOccurred(ErrorCode.AttributeValueInvalid);
                    _stringBuffer.Append(c);
                }
                else if (c == Specification.EOF)
                    return HtmlToken.EOF;
                else
                    _stringBuffer.Append(c);

                c = _src.Next;
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
                return AttributeBeforeName(_src.Next, tag);
            else if (c == Specification.SOLIDUS)
                return TagSelfClosing(_src.Next, tag);
            else if (c == Specification.GT)
                return EmitTag(tag);
            else if (c == Specification.EOF)
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
                    case Specification.LT:
                        return ScriptDataLT(_src.Next);

                    case Specification.NULL:
                        RaiseErrorOccurred(ErrorCode.NULL);
                        _buffer.Append(Specification.REPLACEMENT);
                        break;

                    case Specification.EOF:
                        return HtmlToken.EOF;

                    default:
                        _buffer.Append(c);
                        break;
                }

                c = _src.Next;
            }
        }
        
        /// <summary>
        /// See 8.2.4.17 Script data less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataLT(Char c)
        {
            if (c == Specification.SOLIDUS)
            {
                return ScriptDataEndTag(_src.Next);
            }
            else if (c == Specification.EM)
            {
                _buffer.Append(Specification.LT).Append(Specification.EM);
                return ScriptDataStartEscape(_src.Next);
            }

            _buffer.Append(Specification.LT);
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
                return ScriptDataNameEndTag(_src.Next, HtmlToken.CloseTag());
            }

            _buffer.Append(Specification.LT).Append(Specification.SOLIDUS);
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
            var name = _stringBuffer.ToString().ToLower();
            var appropriateEndTag = name == _lastStartTag;

            if (appropriateEndTag && c.IsSpaceCharacter())
            {
                tag.Name = name;
                return AttributeBeforeName(_src.Next, tag);
            }
            else if (appropriateEndTag && c == Specification.SOLIDUS)
            {
                tag.Name = name;
                return TagSelfClosing(_src.Next, tag);
            }
            else if (appropriateEndTag && c == Specification.GT)
            {
                tag.Name = name;
                return EmitTag(tag);
            }
            else if (c.IsLetter())
            {
                _stringBuffer.Append(c);
                return ScriptDataNameEndTag(_src.Next, tag);
            }

            _buffer.Append(Specification.LT).Append(Specification.SOLIDUS);
            _buffer.Append(_stringBuffer.ToString());
            return ScriptData(c);
        }

        /// <summary>
        /// See 8.2.4.20 Script data escape start state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataStartEscape(Char c)
        {
            if (c == Specification.MINUS)
            {
                _buffer.Append(Specification.MINUS);
                return ScriptDataStartEscapeDash(_src.Next);
            }

            return ScriptData(c);
        }

        /// <summary>
        /// See 8.2.4.22 Script data escaped state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscaped(Char c)
        {
            if (c == Specification.MINUS)
            {
                _buffer.Append(Specification.MINUS);
                return ScriptDataEscapedDash(_src.Next);
            }
            else if (c == Specification.LT)
            {
                return ScriptDataEscapedLT(_src.Next);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _buffer.Append(Specification.REPLACEMENT);
                return ScriptDataEscaped(_src.Next);
            }
            else if (c == Specification.EOF)
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
            if (c == Specification.MINUS)
            {
                _buffer.Append(Specification.MINUS);
                return ScriptDataEscapedDashDash(_src.Next);
            }

            return ScriptData(c);
        }

        /// <summary>
        /// See 8.2.4.23 Script data escaped dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedDash(Char c)
        {
            if (c == Specification.MINUS)
            {
                _buffer.Append(Specification.MINUS);
                return ScriptDataEscapedDashDash(_src.Next);
            }
            else if (c == Specification.LT)
            {
                return ScriptDataEscapedLT(_src.Next);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _buffer.Append(Specification.REPLACEMENT);
                return ScriptDataEscaped(_src.Next);
            }
            else if (c == Specification.EOF)
            {
                return HtmlToken.EOF;
            }

            _buffer.Append(c);
            return ScriptDataEscaped(_src.Next);
        }

        /// <summary>
        /// See 8.2.4.24 Script data escaped dash dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedDashDash(Char c)
        {
            if (c == Specification.MINUS)
            {
                _buffer.Append(Specification.MINUS);
                return ScriptDataEscapedDashDash(_src.Next);
            }
            else if (c == Specification.LT)
            {
                return ScriptDataEscapedLT(_src.Next);
            }
            else if (c == Specification.GT)
            {
                _buffer.Append(Specification.GT);
                return ScriptData(_src.Next);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _buffer.Append(Specification.REPLACEMENT);
                return ScriptDataEscaped(_src.Next);
            }
            else if (c == Specification.EOF)
            {
                return HtmlToken.EOF;
            }

            _buffer.Append(c);
            return ScriptDataEscaped(_src.Next);
        }

        /// <summary>
        /// See 8.2.4.25 Script data escaped less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedLT(Char c)
        {
            if (c == Specification.SOLIDUS)
            {
                return ScriptDataEndTag(_src.Next);
            }
            else if (c.IsLetter())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                _buffer.Append(Specification.LT);
                _buffer.Append(c);
                return ScriptDataStartDoubleEscape(_src.Next);
            }

            _buffer.Append(Specification.LT);
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
                return ScriptDataEscapedEndTag(_src.Next, tag);
            }

            _buffer.Append(Specification.LT).Append(Specification.SOLIDUS);
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
            var name = _stringBuffer.ToString().ToLower();
            var appropriateEndTag = name == _lastStartTag;

            if (appropriateEndTag && c.IsSpaceCharacter())
            {
                tag.Name = name;
                return AttributeBeforeName(_src.Next, tag);
            }
            else if (appropriateEndTag && c == Specification.SOLIDUS)
            {
                tag.Name = name;
                return TagSelfClosing(_src.Next, tag);
            }
            else if (appropriateEndTag && c == Specification.GT)
            {
                tag.Name = name;
                return EmitTag(tag);
            }
            else if (c.IsLetter())
            {
                _stringBuffer.Append(c);
                return ScriptDataEscapedNameTag(_src.Next, tag);
            }

            _buffer.Append(Specification.LT).Append(Specification.SOLIDUS);
            _buffer.Append(_stringBuffer.ToString());
            return ScriptDataEscaped(c);
        }

        /// <summary>
        /// See 8.2.4.28 Script data double escape start state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataStartDoubleEscape(Char c)
        {
            if (c.IsSpaceCharacter() || c == Specification.SOLIDUS || c == Specification.GT)
            {
                _buffer.Append(c);

                if (String.Compare(_stringBuffer.ToString(), Tags.SCRIPT, StringComparison.OrdinalIgnoreCase) == 0)
                    return ScriptDataEscapedDouble(_src.Next);

                return ScriptDataEscaped(_src.Next);
            }
            else if (c.IsLetter())
            {
                _stringBuffer.Append(c);
                _buffer.Append(c);
                return ScriptDataStartDoubleEscape(_src.Next);
            }

            return ScriptDataEscaped(c);
        }

        /// <summary>
        /// See 8.2.4.29 Script data double escaped state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedDouble(Char c)
        {
            if (c == Specification.MINUS)
            {
                _buffer.Append(Specification.MINUS);
                return ScriptDataEscapedDoubleDash(_src.Next);
            }
            else if (c == Specification.LT)
            {
                _buffer.Append(Specification.LT);
                return ScriptDataEscapedDoubleLT(_src.Next);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _buffer.Append(Specification.REPLACEMENT);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return HtmlToken.EOF;
            }

            _buffer.Append(c);
            return ScriptDataEscapedDouble(_src.Next);
        }

        /// <summary>
        /// See 8.2.4.30 Script data double escaped dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedDoubleDash(Char c)
        {
            if (c == Specification.MINUS)
            {
                _buffer.Append(Specification.MINUS);
                return ScriptDataEscapedDoubleDashDash(_src.Next);
            }
            else if (c == Specification.LT)
            {
                _buffer.Append(Specification.LT);
                return ScriptDataEscapedDoubleLT(_src.Next);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _buffer.Append(Specification.REPLACEMENT);
                return ScriptDataEscapedDouble(_src.Next);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return HtmlToken.EOF;
            }

            _buffer.Append(c);
            return ScriptDataEscapedDouble(_src.Next);
        }

        /// <summary>
        /// See 8.2.4.31 Script data double escaped dash dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedDoubleDashDash(Char c)
        {
            if (c == Specification.MINUS)
            {
                _buffer.Append(Specification.MINUS);
                return ScriptDataEscapedDoubleDashDash(_src.Next);
            }
            else if (c == Specification.LT)
            {
                _buffer.Append(Specification.LT);
                return ScriptDataEscapedDoubleLT(_src.Next);
            }
            else if (c == Specification.GT)
            {
                _buffer.Append(Specification.GT);
                return ScriptData(_src.Next);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _buffer.Append(Specification.REPLACEMENT);
                return ScriptDataEscapedDouble(_src.Next);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return HtmlToken.EOF;
            }

            _buffer.Append(c);
            return ScriptDataEscapedDouble(_src.Next);
        }

        /// <summary>
        /// See 8.2.4.32 Script data double escaped less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedDoubleLT(Char c)
        {
            if (c == Specification.SOLIDUS)
            {
                _stringBuffer.Clear();
                _buffer.Append(Specification.SOLIDUS);
                return ScriptDataEndDoubleEscape(_src.Next);
            }

            return ScriptDataEscapedDouble(c);
        }

        /// <summary>
        /// See 8.2.4.33 Script data double escape end state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEndDoubleEscape(Char c)
        {
            if (c.IsSpaceCharacter() || c == Specification.SOLIDUS || c == Specification.GT)
            {
                _buffer.Append(c);

                if (String.Compare(_stringBuffer.ToString(), Tags.SCRIPT, StringComparison.OrdinalIgnoreCase) == 0)
                    return ScriptDataEscaped(_src.Next);

                return ScriptDataEscapedDouble(_src.Next);
            }
            else if (c.IsLetter())
            {
                _stringBuffer.Append(c);
                _buffer.Append(c);
                return ScriptDataEndDoubleEscape(_src.Next);
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
            _model = HtmlParseMode.PCData;

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
