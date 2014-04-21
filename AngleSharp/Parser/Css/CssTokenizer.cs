namespace AngleSharp.Parser.Css
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// The CSS tokenizer.
    /// See http://dev.w3.org/csswg/css-syntax/#tokenization for more details.
    /// </summary>
    [DebuggerStepThrough]
    sealed class CssTokenizer : BaseTokenizer
	{
		#region Fields

		Boolean _ignoreWs;
		Boolean _ignoreCs;

		#endregion

		#region ctor

		public CssTokenizer(SourceManager source)
            : base(source)
        {
            _stringBuffer = new StringBuilder();
            _src = source;
        }

        #endregion

        #region Properties

		/// <summary>
		/// Gets or sets if whitespace tokens should be ignored.
		/// </summary>
		public Boolean IgnoreWhitespace
		{
			get { return _ignoreWs; }
			set { _ignoreWs = value; }
		}

		/// <summary>
		/// Gets or sets if HTML comment tokens should be ignored.
		/// </summary>
		public Boolean IgnoreComments
		{
			get { return _ignoreCs; }
			set { _ignoreCs = value; }
		}

        /// <summary>
        /// Gets the underlying stream.
        /// </summary>
        public SourceManager Stream
        {
            get { return _src; }
        }

        /// <summary>
        /// Gets the token enumerable.
        /// </summary>
        public IEnumerable<CssToken> Tokens
        {
            get
            {
                CssToken token;

                while(true)
                {
                    token = Data(_src.Current);

                    if (token == null)
                        yield break;

                    _src.Advance();
                    yield return token;
                }
            }
        }

        #endregion

        #region States

        /// <summary>
        /// 4.4.1. Data state
        /// </summary>
        CssToken Data(Char current)
        {
            switch (current)
            {
                case Specification.LineFeed:
                case Specification.CarriageReturn:
                case Specification.Tab:
                case Specification.Space:
                    do { current = _src.Next; }
                    while (current.IsSpaceCharacter());

					if (_ignoreWs)
						return Data(current);

                    _src.Back();
                    return CssSpecialCharacter.Whitespace;

                case Specification.DoubleQuote:
                    return StringDQ(_src.Next);

                case Specification.Num:
                    return HashStart(_src.Next);

                case Specification.Dollar:
                    current = _src.Next;

                    if (current == Specification.Equality)
                        return CssMatchToken.Suffix;

                    return CssToken.Delim(_src.Previous);

                case Specification.SingleQuote:
                    return StringSQ(_src.Next);

                case Specification.RoundBracketOpen:
                    return CssBracketToken.OpenRound;

                case Specification.RoundBracketClose:
                    return CssBracketToken.CloseRound;

                case Specification.Asterisk:
                    current = _src.Next;

                    if (current == Specification.Equality)
                        return CssMatchToken.Substring;

                    return CssToken.Delim(_src.Previous);

                case Specification.Plus:
                    {
                        var c1 = _src.Next;

                        if (c1 == Specification.EndOfFile)
                        {
                            _src.Back();
                        }
                        else
                        {
                            var c2 = _src.Next;
                            _src.Back(2);

                            if (c1.IsDigit() || (c1 == Specification.Dot && c2.IsDigit()))
                                return NumberStart(current);
                        }
                        
                        return CssToken.Delim(current);
                    }

                case Specification.Comma:
                    return CssSpecialCharacter.Comma;

                case Specification.Dot:
                    {
                        var c = _src.Next;

                        if (c.IsDigit())
                            return NumberStart(_src.Previous);
                        
                        return CssToken.Delim(_src.Previous);
                    }

                case Specification.Minus:
                    {
                        var c1 = _src.Next;

                        if (c1 == Specification.EndOfFile)
                        {
                            _src.Back();
                        }
                        else
                        {
                            var c2 = _src.Next;
                            _src.Back(2);

                            if (c1.IsDigit() || (c1 == Specification.Dot && c2.IsDigit()))
                                return NumberStart(current);
                            else if (c1.IsNameStart())
                                return IdentStart(current);
                            else if (c1 == Specification.ReverseSolidus && !c2.IsLineBreak() && c2 != Specification.EndOfFile)
                                return IdentStart(current);
                            else if (c1 == Specification.Minus && c2 == Specification.GreaterThan)
                            {
                                _src.Advance(2);

								if (_ignoreCs)
									return Data(_src.Next);

                                return CssCommentToken.Close;
                            }
                        }
                        
                        return CssToken.Delim(current);
                    }

                case Specification.Solidus:
                    current = _src.Next;

                    if (current == Specification.Asterisk)
                        return Comment(_src.Next);
                        
                    return CssToken.Delim(_src.Previous);

                case Specification.ReverseSolidus:
                    current = _src.Next;

                    if (current.IsLineBreak() || current == Specification.EndOfFile)
                    {
                        RaiseErrorOccurred(current == Specification.EndOfFile ? ErrorCode.EOF : ErrorCode.LineBreakUnexpected);
                        return CssToken.Delim(_src.Previous);
                    }

                    return IdentStart(_src.Previous);

                case Specification.Colon:
                    return CssSpecialCharacter.Colon;

                case Specification.Semicolon:
                    return CssSpecialCharacter.Semicolon;

                case Specification.LessThan:
                    current = _src.Next;

                    if (current == Specification.ExclamationMark)
                    {
                        current = _src.Next;

                        if (current == Specification.Minus)
                        {
                            current = _src.Next;

							if (current == Specification.Minus)
							{
								if (_ignoreCs)
									return Data(_src.Next);

								return CssCommentToken.Open;
							}

                            current = _src.Previous;
                        }

                        current = _src.Previous;
                    }

                    return CssToken.Delim(_src.Previous);

                case Specification.At:
                    return AtKeywordStart(_src.Next);

                case Specification.SquareBracketOpen:
                    return CssBracketToken.OpenSquare;

                case Specification.SquareBracketClose:
                    return CssBracketToken.CloseSquare;

                case Specification.Accent:
                    current = _src.Next;

                    if (current == Specification.Equality)
                        return CssMatchToken.Prefix;

                    return CssToken.Delim(_src.Previous);

                case Specification.CurlyBracketOpen:
                    return CssBracketToken.OpenCurly;

                case Specification.CurlyBracketClose:
                    return CssBracketToken.CloseCurly;

                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return NumberStart(current);

                case 'U':
                case 'u':
                    current = _src.Next;

                    if (current == Specification.Plus)
                    {
                        current = _src.Next;

                        if (current.IsHex() || current == Specification.QuestionMark)
                            return UnicodeRange(current);

                        current = _src.Previous;
                    }

                    return IdentStart(_src.Previous);

                case Specification.Pipe:
                    current = _src.Next;

                    if (current == Specification.Equality)
                        return CssMatchToken.Dash;
                    else if (current == Specification.Pipe)
                        return CssToken.Column;

                    return CssToken.Delim(_src.Previous);

                case Specification.Tilde:
                    current = _src.Next;

                    if (current == Specification.Equality)
                        return CssMatchToken.Include;

                    return CssToken.Delim(_src.Previous);

                case Specification.EndOfFile:
                    return null;

                case Specification.ExclamationMark:
                    current = _src.Next;

                    if (current == Specification.Equality)
                        return CssMatchToken.Not;

                    return CssToken.Delim(_src.Previous);

                default:
                    if (current.IsNameStart())
                        return IdentStart(current);

                    return CssToken.Delim(current);
            }
        }

        /// <summary>
        /// 4.4.2. Double quoted string state
        /// </summary>
        CssToken StringDQ(Char current)
        {
            while (true)
            {
                switch (current)
                {
                    case Specification.DoubleQuote:
                    case Specification.EndOfFile:
                        return CssStringToken.Plain(FlushBuffer());

                    case Specification.FormFeed:
                    case Specification.LineFeed:
                        RaiseErrorOccurred(ErrorCode.LineBreakUnexpected);
                        _src.Back();
                        return CssStringToken.Plain(FlushBuffer(), true);

                    case Specification.ReverseSolidus:
                        current = _src.Next;

                        if (current.IsLineBreak())
                            _stringBuffer.AppendLine();
                        else if (current != Specification.EndOfFile)
                            _stringBuffer.Append(ConsumeEscape(current));
                        else
                        {
                            RaiseErrorOccurred(ErrorCode.EOF);
                            _src.Back();
                            return CssStringToken.Plain(FlushBuffer(), true);
                        }

                        break;

                    default:
                        _stringBuffer.Append(current);
                        break;
                }

                current = _src.Next;
            }
        }

        /// <summary>
        /// 4.4.3. Single quoted string state
        /// </summary>
        CssToken StringSQ(Char current)
        {
            while (true)
            {
                switch (current)
                {
                    case Specification.SingleQuote:
                    case Specification.EndOfFile:
                        return CssStringToken.Plain(FlushBuffer());

                    case Specification.FormFeed:
                    case Specification.LineFeed:
                        RaiseErrorOccurred(ErrorCode.LineBreakUnexpected);
                        _src.Back();
                        return (CssStringToken.Plain(FlushBuffer(), true));

                    case Specification.ReverseSolidus:
                        current = _src.Next;

                        if (current.IsLineBreak())
                            _stringBuffer.AppendLine();
                        else if (current != Specification.EndOfFile)
                            _stringBuffer.Append(ConsumeEscape(current));
                        else
                        {
                            RaiseErrorOccurred(ErrorCode.EOF);
                            _src.Back();
                            return(CssStringToken.Plain(FlushBuffer(), true));
                        }

                        break;

                    default:
                        _stringBuffer.Append(current);
                        break;
                }

                current = _src.Next;
            }
        }

        /// <summary>
        /// 4.4.4. Hash state
        /// </summary>
        CssToken HashStart(Char current)
        {
            if (current.IsNameStart())
            {
                _stringBuffer.Append(current);
                return HashRest(_src.Next);
            }
            else if (IsValidEscape(current))
            {
                current = _src.Next;
                _stringBuffer.Append(ConsumeEscape(current));
                return HashRest(_src.Next);
            }
            else if (current == Specification.ReverseSolidus)
            {
                RaiseErrorOccurred(ErrorCode.InvalidCharacter);
                _src.Back();
                return CssToken.Delim(Specification.Num);
            }
            else
            {
                _src.Back();
                return CssToken.Delim(Specification.Num);
            }
        }

        /// <summary>
        /// 4.4.5. Hash-rest state
        /// </summary>
        CssToken HashRest(Char current)
        {
            while (true)
            {
                if (current.IsName())
                    _stringBuffer.Append(current);
                else if (IsValidEscape(current))
                {
                    current = _src.Next;
                    _stringBuffer.Append(ConsumeEscape(current));
                }
                else if (current == Specification.ReverseSolidus)
                {
                    RaiseErrorOccurred(ErrorCode.InvalidCharacter);
                    _src.Back();
                    return CssKeywordToken.Hash(FlushBuffer());
                }
                else
                {
                    _src.Back();
                    return CssKeywordToken.Hash(FlushBuffer());
                }

                current = _src.Next;
            }
        }

        /// <summary>
        /// 4.4.6. Comment state
        /// </summary>
        CssToken Comment(Char current)
        {
            while (true)
            {
                if (current == Specification.Asterisk)
                {
                    current = _src.Next;

                    if (current == Specification.Solidus)
                        return Data(_src.Next);
                }
                else if (current == Specification.EndOfFile)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    return Data(current);
                }

                current = _src.Next;
            }
        }

        /// <summary>
        /// 4.4.7. At-keyword state
        /// </summary>
        CssToken AtKeywordStart(Char current)
        {
            if (current == Specification.Minus)
            {
                current = _src.Next;

                if (current.IsNameStart() || IsValidEscape(current))
                {
                    _stringBuffer.Append(Specification.Minus);
                    return AtKeywordRest(current);
                }

                _src.Back(2);
                return CssToken.Delim(Specification.At);
            }
            else if (current.IsNameStart())
            {
                _stringBuffer.Append(current);
                return AtKeywordRest(_src.Next);
            }
            else if (IsValidEscape(current))
            {
                current = _src.Next;
                _stringBuffer.Append(ConsumeEscape(current));
                return AtKeywordRest(_src.Next);
            }
            else
            {
                _src.Back();
                return CssToken.Delim(Specification.At);
            }
        }

        /// <summary>
        /// 4.4.8. At-keyword-rest state
        /// </summary>
        CssToken AtKeywordRest(Char current)
        {
            while (true)
            {
                if (current.IsName())
                    _stringBuffer.Append(current);
                else if (IsValidEscape(current))
                {
                    current = _src.Next;
                    _stringBuffer.Append(ConsumeEscape(current));
                }
                else
                {
                    _src.Back();
                    return CssKeywordToken.At(FlushBuffer());
                }

                current = _src.Next;
            }
        }

        /// <summary>
        /// 4.4.9. Ident state
        /// </summary>
        CssToken IdentStart(Char current)
        {
            if (current == Specification.Minus)
            {
                current = _src.Next;

                if (current.IsNameStart() || IsValidEscape(current))
                {
                    _stringBuffer.Append(Specification.Minus);
                    return IdentRest(current);
                }

                _src.Back();
                return CssToken.Delim(Specification.Minus);
            }
            else if (current.IsNameStart())
            {
                _stringBuffer.Append(current);
                return IdentRest(_src.Next);
            }
            else if (current == Specification.ReverseSolidus)
            {
                if (IsValidEscape(current))
                {
                    current = _src.Next;
                    _stringBuffer.Append(ConsumeEscape(current));
                    return IdentRest(_src.Next);
                }
            }

            return Data(current);
        }

        /// <summary>
        /// 4.4.10. Ident-rest state
        /// </summary>
        CssToken IdentRest(Char current)
        {
            while (true)
            {
                if (current.IsName())
                    _stringBuffer.Append(current);
                else if (IsValidEscape(current))
                {
                    current = _src.Next;
                    _stringBuffer.Append(ConsumeEscape(current));
                }
                else if (current == Specification.RoundBracketOpen)
                {
                    var fn = _stringBuffer.ToString().ToLower();

                    if (fn == FunctionNames.Url)
                    {
                        _stringBuffer.Clear();
                        return UrlStart(_src.Next, CssTokenType.Url);
                    }
                    else if (fn == FunctionNames.Domain)
                    {
                        _stringBuffer.Clear();
                        return UrlStart(_src.Next, CssTokenType.Domain);
                    }
                    else if (fn == FunctionNames.Url_Prefix)
                    {
                        _stringBuffer.Clear();
                        return UrlStart(_src.Next, CssTokenType.UrlPrefix);
                    }

                    return CssKeywordToken.Function(FlushBuffer());

                }
                //false could be replaced with a transform whitespace flag, which is set to true if in SVG transform mode.
                //else if (false && Specification.IsSpaceCharacter(current))
                //    InstantSwitch(TransformFunctionWhitespace);
                else
                {
                    _src.Back();
                    return CssKeywordToken.Ident(FlushBuffer());
                }

                current = _src.Next;
            }
        }

        /// <summary>
        /// 4.4.11. Transform-function-whitespace state
        /// </summary>
        CssToken TransformFunctionWhitespace(Char current)
        {
            while (true)
            {
                current = _src.Next;

                if (current == Specification.RoundBracketOpen)
                {
                    _src.Back();
                    return CssKeywordToken.Function(FlushBuffer());
                }
                else if (!current.IsSpaceCharacter())
                {
                    _src.Back(2);
                    return CssKeywordToken.Ident(FlushBuffer());
                }
            }
        }

        /// <summary>
        /// 4.4.12. Number state
        /// </summary>
        CssToken NumberStart(Char current)
        {
            while (true)
            {
                if (current == Specification.Plus || current == Specification.Minus)
                {
                    _stringBuffer.Append(current);
                    current = _src.Next;

                    if (current == Specification.Dot)
                    {
                        _stringBuffer.Append(current);
                        _stringBuffer.Append(_src.Next);
                        return NumberFraction(_src.Next);
                    }

                    _stringBuffer.Append(current);
                    return NumberRest(_src.Next);
                }
                else if (current == Specification.Dot)
                {
                    _stringBuffer.Append(current);
                    _stringBuffer.Append(_src.Next);
                    return NumberFraction(_src.Next);
                }
                else if (current.IsDigit())
                {
                    _stringBuffer.Append(current);
                    return NumberRest(_src.Next);
                }

                current = _src.Next;
            }
        }

        /// <summary>
        /// 4.4.13. Number-rest state
        /// </summary>
        CssToken NumberRest(Char current)
        {
            while (true)
            {
                if (current.IsDigit())
                    _stringBuffer.Append(current);
                else if (current.IsNameStart())
                {
                    var number = FlushBuffer();
                    _stringBuffer.Append(current);
                    return Dimension(_src.Next, number);
                }
                else if (IsValidEscape(current))
                {
                    current = _src.Next;
                    var number = FlushBuffer();
                    _stringBuffer.Append(ConsumeEscape(current));
                    return Dimension(_src.Next, number);
                }
                else
                    break;

                current = _src.Next;
            }

            switch (current)
            {
                case Specification.Dot:
                    current = _src.Next;

                    if (current.IsDigit())
                    {
                        _stringBuffer.Append(Specification.Dot).Append(current);
                        return NumberFraction(_src.Next);
                    }

                    _src.Back();
                    return CssToken.Number(FlushBuffer());

                case '%':
                    return CssUnitToken.Percentage(FlushBuffer());

                case 'e':
                case 'E':
                    return NumberExponential(current);

                case Specification.Minus:
                    return NumberDash(current);

                default:
                    _src.Back();
                    return CssToken.Number(FlushBuffer());
            }
        }

        /// <summary>
        /// 4.4.14. Number-fraction state
        /// </summary>
        CssToken NumberFraction(Char current)
        {
            while (true)
            {
                if (current.IsDigit())
                    _stringBuffer.Append(current);
                else if (current.IsNameStart())
                {
                    var number = FlushBuffer();
                    _stringBuffer.Append(current);
                    return Dimension(_src.Next, number);
                }
                else if (IsValidEscape(current))
                {
                    current = _src.Next;
                    var number = FlushBuffer();
                    _stringBuffer.Append(ConsumeEscape(current));
                    return Dimension(_src.Next, number);
                }
                else
                    break;

                current = _src.Next;
            }

            switch (current)
            {
                case 'e':
                case 'E':
                    return NumberExponential(current);

                case '%':
                    return CssUnitToken.Percentage(FlushBuffer());

                case Specification.Minus:
                    return NumberDash(current);

                default:
                    _src.Back();
                    return CssToken.Number(FlushBuffer());
            }
        }

        /// <summary>
        /// 4.4.15. Dimension state
        /// </summary>
        CssToken Dimension(Char current, String number)
        {
            while (true)
            {
                if (current.IsName())
                    _stringBuffer.Append(current);
                else if (IsValidEscape(current))
                {
                    current = _src.Next;
                    _stringBuffer.Append(ConsumeEscape(current));
                }
                else
                {
                    _src.Back();
                    return CssUnitToken.Dimension(number, FlushBuffer());
                }

                current = _src.Next;
            }
        }

        /// <summary>
        /// 4.4.16. SciNotation state
        /// </summary>
        CssToken SciNotation(Char current)
        {
            while (true)
            {
                if (current.IsDigit())
                    _stringBuffer.Append(current);
                else
                {
                    _src.Back();
                    return CssToken.Number(FlushBuffer());
                }

                current = _src.Next;
            }
        }

        /// <summary>
        /// 4.4.17. URL state
        /// </summary>
        CssToken UrlStart(Char current, CssTokenType type)
        {
            while (current.IsSpaceCharacter())
                current = _src.Next;

            switch (current)
            {
                case Specification.EndOfFile:
                    RaiseErrorOccurred(ErrorCode.EOF);
                    return CssStringToken.Url(type, String.Empty, true);

                case Specification.DoubleQuote:
                    return UrlDQ(_src.Next, type);

                case Specification.SingleQuote:
                    return UrlSQ(_src.Next, type);

                case ')':
                    return CssStringToken.Url(type, String.Empty, false);

                default:
                    return UrlUQ(current, type);
            }
        }

        /// <summary>
        /// 4.4.18. URL-double-quoted state
        /// </summary>
        CssToken UrlDQ(Char current, CssTokenType type)
        {
            while (true)
            {
                if (current.IsLineBreak())
                {
                    RaiseErrorOccurred(ErrorCode.LineBreakUnexpected);
                    return UrlBad(_src.Next, type);
                }
                else if (Specification.EndOfFile == current)
                {
                    return CssStringToken.Url(type, FlushBuffer());
                }
                else if (current == Specification.DoubleQuote)
                {
                    return UrlEnd(_src.Next, type);
                }
                else if (current == Specification.ReverseSolidus)
                {
                    current = _src.Next;

                    if (current == Specification.EndOfFile)
                    {
                        _src.Back(2);
                        RaiseErrorOccurred(ErrorCode.EOF);
                        return CssStringToken.Url(type, FlushBuffer(), true);
                    }
                    else if (current.IsLineBreak())
                        _stringBuffer.AppendLine();
                    else
                        _stringBuffer.Append(ConsumeEscape(current));
                }
                else
                    _stringBuffer.Append(current);

                current = _src.Next;
            }
        }

        /// <summary>
        /// 4.4.19. URL-single-quoted state
        /// </summary>
        CssToken UrlSQ(Char current, CssTokenType type)
        {
            while (true)
            {
                if (current.IsLineBreak())
                {
                    RaiseErrorOccurred(ErrorCode.LineBreakUnexpected);
                    return UrlBad(_src.Next, type);
                }
                else if (Specification.EndOfFile == current)
                {
                    return CssStringToken.Url(type, FlushBuffer());
                }
                else if (current == Specification.SingleQuote)
                {
                    return UrlEnd(_src.Next, type);
                }
                else if (current == Specification.ReverseSolidus)
                {
                    current = _src.Next;

                    if (current == Specification.EndOfFile)
                    {
                        _src.Back(2);
                        RaiseErrorOccurred(ErrorCode.EOF);
                        return CssStringToken.Url(type, FlushBuffer(), true);
                    }
                    else if (current.IsLineBreak())
                        _stringBuffer.AppendLine();
                    else
                        _stringBuffer.Append(ConsumeEscape(current));
                }
                else
                    _stringBuffer.Append(current);

                current = _src.Next;
            }
        }

        /// <summary>
        /// 4.4.21. URL-unquoted state
        /// </summary>
        CssToken UrlUQ(Char current, CssTokenType type)
        {
            while (true)
            {
                if (current.IsSpaceCharacter())
                {
                    return UrlEnd(_src.Next, type);
                }
                else if (current == Specification.RoundBracketClose || current == Specification.EndOfFile)
                {
                    return CssStringToken.Url(type, FlushBuffer());
                }
                else if (current == Specification.DoubleQuote || current == Specification.SingleQuote || current == Specification.RoundBracketOpen || current.IsNonPrintable())
                {
                    RaiseErrorOccurred(ErrorCode.InvalidCharacter);
                    return UrlBad(_src.Next, type);
                }
                else if (current == Specification.ReverseSolidus)
                {
                    if (IsValidEscape(current))
                    {
                        current = _src.Next;
                        _stringBuffer.Append(ConsumeEscape(current));
                    }
                    else
                    {
                        RaiseErrorOccurred(ErrorCode.InvalidCharacter);
                        return UrlBad(_src.Next, type);
                    }
                }
                else
                    _stringBuffer.Append(current);

                current = _src.Next;
            }
        }

        /// <summary>
        /// 4.4.20. URL-end state
        /// </summary>
        CssToken UrlEnd(Char current, CssTokenType type)
        {
            while (true)
            {
                if (current == Specification.RoundBracketClose)
                    return CssStringToken.Url(type, FlushBuffer());
                else if (!current.IsSpaceCharacter())
                {
                    RaiseErrorOccurred(ErrorCode.InvalidCharacter);
                    return UrlBad(current, type);
                }

                current = _src.Next;
            }
        }

        /// <summary>
        /// 4.4.22. Bad URL state
        /// </summary>
        CssToken UrlBad(Char current, CssTokenType type)
        {
            while (true)
            {
                if (current == Specification.EndOfFile)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    return CssStringToken.Url(type, FlushBuffer(), true);
                }
                else if (current == Specification.RoundBracketClose)
                {
                    return CssStringToken.Url(type, FlushBuffer(), true);
                }
                else if (IsValidEscape(current))
                {
                    current = _src.Next;
                    _stringBuffer.Append(ConsumeEscape(current));
                }

                current = _src.Next;
            }
        }

        /// <summary>
        /// 4.4.23. Unicode-range State
        /// </summary>
        CssToken UnicodeRange(Char current)
        {
            for (int i = 0; i < 6; i++)
            {
                if (!current.IsHex())
                    break;

                _stringBuffer.Append(current);
                current = _src.Next;
            }

            if (_stringBuffer.Length != 6)
            {
                for (int i = 0; i < 6 - _stringBuffer.Length; i++)
                {
                    if (current != Specification.QuestionMark)
                    {
                        current = _src.Previous;
                        break;
                    }

                    _stringBuffer.Append(current);
                    current = _src.Next;
                }

                var range = FlushBuffer();
                var start = range.Replace(Specification.QuestionMark, '0');
                var end = range.Replace(Specification.QuestionMark, 'F');
                return CssToken.Range(start, end);
            }
            else if (current == Specification.Minus)
            {
                current = _src.Next;

                if (current.IsHex())
                {
                    var start = _stringBuffer.ToString();
                    _stringBuffer.Clear();

                    for (int i = 0; i < 6; i++)
                    {
                        if (!current.IsHex())
                        {
                            current = _src.Previous;
                            break;
                        }

                        _stringBuffer.Append(current);
                        current = _src.Next;
                    }

                    var end = FlushBuffer();
                    return CssToken.Range(start, end);
                }
                else
                {
                    _src.Back(2);
                    return CssToken.Range(FlushBuffer(), null);
                }
            }
            else
            {
                _src.Back();
                return CssToken.Range(FlushBuffer(), null);
            }
        }

        #endregion

        #region Helpers

        String FlushBuffer()
        {
            var tmp = _stringBuffer.ToString();
            _stringBuffer.Clear();
            return tmp;
        }

        /// <summary>
        /// Substate of several Number states.
        /// </summary>
        CssToken NumberExponential(Char current)
        {
            current = _src.Next;

            if (current.IsDigit())
            {
                _stringBuffer.Append('e').Append(current);
                return SciNotation(_src.Next);
            }
            else if (current == Specification.Plus || current == Specification.Minus)
            {
                var op = current;
                current = _src.Next;

                if (current.IsDigit())
                {
                    _stringBuffer.Append('e').Append(op).Append(current);
                    return SciNotation(_src.Next);
                }

                _src.Back();
            }

            current = _src.Previous;
            var number = FlushBuffer();
            _stringBuffer.Append(current);
            return Dimension(_src.Next, number);
        }

        /// <summary>
        /// Substate of several Number states.
        /// </summary>
        CssToken NumberDash(Char current)
        {
            current = _src.Next;

            if (current.IsNameStart())
            {
                var number = FlushBuffer();
                _stringBuffer.Append(Specification.Minus).Append(current);
                return Dimension(_src.Next, number);
            }
            else if (IsValidEscape(current))
            {
                current = _src.Next;
                var number = FlushBuffer();
                _stringBuffer.Append(Specification.Minus).Append(ConsumeEscape(current));
                return Dimension(_src.Next, number);
            }
            else
            {
                _src.Back(2);
                return CssToken.Number(FlushBuffer());
            }
        }

        /// <summary>
        /// Consumes an escaped character AFTER the solidus has already been
        /// consumed.
        /// </summary>
        /// <returns>The escaped character.</returns>
        String ConsumeEscape(Char current)
        {
            if (current.IsHex())
            {
                var escape = new List<Char>();

                for (int i = 0; i < 6; i++)
                {
                    escape.Add(current);
                    current = _src.Next;

                    if (!current.IsHex())
                        break;
                }

                current = _src.Previous;
                var code = Int32.Parse(new String(escape.ToArray()), NumberStyles.HexNumber);
                return Char.ConvertFromUtf32(code);
            }

            return current.ToString();
        }

        /// <summary>
        /// Checks if the current position is the beginning of a valid escape sequence.
        /// </summary>
        /// <returns>The result of the check.</returns>
        Boolean IsValidEscape(Char current)
        {
            if (current != Specification.ReverseSolidus)
                return false;

            current = _src.Next;
            _src.Back();

            if (current == Specification.EndOfFile)
                return false;
            else if (current.IsLineBreak())
                return false;

            return true;
        }

        #endregion
    }
}
