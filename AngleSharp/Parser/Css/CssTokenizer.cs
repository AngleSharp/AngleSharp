namespace AngleSharp.Parser.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;

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

        #region Events

        /// <summary>
        /// The event will be fired once an error has been detected.
        /// </summary>
        public event EventHandler<ParseErrorEventArgs> ErrorOccurred;

        #endregion

        #region ctor

        public CssTokenizer(TextSource source)
            : base(source)
        {
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
        /// Gets the token enumerable.
        /// </summary>
        public IEnumerable<CssToken> Tokens
        {
            get
            {
                CssToken token;

                while (true)
                {
                    token = Data(GetNext());

                    if (token == null)
                        yield break;

                    yield return token;
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fires an error occurred event.
        /// </summary>
        /// <param name="code">The associated error code.</param>
        public void RaiseErrorOccurred(CssParseError code)
        {
            if (ErrorOccurred != null)
            {
                var position = GetCurrentPosition();
                var errorArguments = new ParseErrorEventArgs(code.GetCode(), code.GetMessage(), position);
                ErrorOccurred(this, errorArguments);
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
                case Symbols.LineFeed:
                case Symbols.CarriageReturn:
                case Symbols.Tab:
                case Symbols.Space:
                    do { current = GetNext(); }
                    while (current.IsSpaceCharacter());

					if (_ignoreWs)
						return Data(current);

                    Back();
                    return CssSpecialCharacter.Whitespace;

                case Symbols.DoubleQuote:
                    return StringDQ();

                case Symbols.Num:
                    return HashStart();

                case Symbols.Dollar:
                    current = GetNext();

                    if (current == Symbols.Equality)
                        return CssMatchToken.Suffix;

                    return CssToken.Delim(GetPrevious());

                case Symbols.SingleQuote:
                    return StringSQ();

                case Symbols.RoundBracketOpen:
                    return CssBracketToken.OpenRound;

                case Symbols.RoundBracketClose:
                    return CssBracketToken.CloseRound;

                case Symbols.Asterisk:
                    current = GetNext();

                    if (current == Symbols.Equality)
                        return CssMatchToken.Substring;

                    return CssToken.Delim(GetPrevious());

                case Symbols.Plus:
                {
                    var c1 = GetNext();

                    if (c1 != Symbols.EndOfFile)
                    {
                        var c2 = GetNext();
                        Back(2);

                        if (c1.IsDigit() || (c1 == Symbols.Dot && c2.IsDigit()))
                            return NumberStart(current);
                    }
                    else
                        Back();
                        
                    return CssToken.Delim(current);
                }

                case Symbols.Comma:
                    return CssSpecialCharacter.Comma;

                case Symbols.Dot:
                {
                    var c = GetNext();

                    if (c.IsDigit())
                        return NumberStart(GetPrevious());
                        
                    return CssToken.Delim(GetPrevious());
                }

                case Symbols.Minus:
                {
                    var c1 = GetNext();

                    if (c1 != Symbols.EndOfFile)
                    {
                        var c2 = GetNext();
                        Back(2);

                        if (c1.IsDigit() || (c1 == Symbols.Dot && c2.IsDigit()))
                            return NumberStart(current);
                        else if (c1.IsNameStart())
                            return IdentStart(current);
                        else if (c1 == Symbols.ReverseSolidus && !c2.IsLineBreak() && c2 != Symbols.EndOfFile)
                            return IdentStart(current);
                        else if (c1 == Symbols.Minus && c2 == Symbols.GreaterThan)
                        {
                            Advance(2);

                            if (_ignoreCs)
                                return Data(GetNext());

                            return CssCommentToken.Close;
                        }
                    }
                    else
                        Back();
                        
                    return CssToken.Delim(current);
                }

                case Symbols.Solidus:
                    current = GetNext();

                    if (current == Symbols.Asterisk)
                        return Comment();
                        
                    return CssToken.Delim(GetPrevious());

                case Symbols.ReverseSolidus:
                    current = GetNext();

                    if (current.IsLineBreak() || current == Symbols.EndOfFile)
                    {
                        RaiseErrorOccurred(current == Symbols.EndOfFile ? CssParseError.EOF : CssParseError.LineBreakUnexpected);
                        return CssToken.Delim(GetPrevious());
                    }

                    return IdentStart(GetPrevious());

                case Symbols.Colon:
                    return CssSpecialCharacter.Colon;

                case Symbols.Semicolon:
                    return CssSpecialCharacter.Semicolon;

                case Symbols.LessThan:
                    current = GetNext();

                    if (current == Symbols.ExclamationMark)
                    {
                        current = GetNext();

                        if (current == Symbols.Minus)
                        {
                            current = GetNext();

							if (current == Symbols.Minus)
							{
								if (_ignoreCs)
									return Data(GetNext());

								return CssCommentToken.Open;
							}

                            current = GetPrevious();
                        }

                        current = GetPrevious();
                    }

                    return CssToken.Delim(GetPrevious());

                case Symbols.At:
                    return AtKeywordStart();

                case Symbols.SquareBracketOpen:
                    return CssBracketToken.OpenSquare;

                case Symbols.SquareBracketClose:
                    return CssBracketToken.CloseSquare;

                case Symbols.Accent:
                    current = GetNext();

                    if (current == Symbols.Equality)
                        return CssMatchToken.Prefix;

                    return CssToken.Delim(GetPrevious());

                case Symbols.CurlyBracketOpen:
                    return CssBracketToken.OpenCurly;

                case Symbols.CurlyBracketClose:
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
                    current = GetNext();

                    if (current == Symbols.Plus)
                    {
                        current = GetNext();

                        if (current.IsHex() || current == Symbols.QuestionMark)
                            return UnicodeRange(current);

                        current = GetPrevious();
                    }

                    return IdentStart(GetPrevious());

                case Symbols.Pipe:
                    current = GetNext();

                    if (current == Symbols.Equality)
                        return CssMatchToken.Dash;
                    else if (current == Symbols.Pipe)
                        return CssColumnToken.Instance;

                    return CssToken.Delim(GetPrevious());

                case Symbols.Tilde:
                    current = GetNext();

                    if (current == Symbols.Equality)
                        return CssMatchToken.Include;

                    return CssToken.Delim(GetPrevious());

                case Symbols.EndOfFile:
                    return null;

                case Symbols.ExclamationMark:
                    current = GetNext();

                    if (current == Symbols.Equality)
                        return CssMatchToken.Not;

                    return CssToken.Delim(GetPrevious());

                default:
                    if (current.IsNameStart())
                        return IdentStart(current);

                    return CssToken.Delim(current);
            }
        }

        /// <summary>
        /// 4.4.2. Double quoted string state
        /// </summary>
        CssToken StringDQ()
        {
            while (true)
            {
                var current = GetNext();

                switch (current)
                {
                    case Symbols.DoubleQuote:
                    case Symbols.EndOfFile:
                        return CssStringToken.Plain(FlushBuffer());

                    case Symbols.FormFeed:
                    case Symbols.LineFeed:
                        RaiseErrorOccurred(CssParseError.LineBreakUnexpected);
                        Back();
                        return CssStringToken.Plain(FlushBuffer(), true);

                    case Symbols.ReverseSolidus:
                        current = GetNext();

                        if (current.IsLineBreak())
                            _stringBuffer.AppendLine();
                        else if (current != Symbols.EndOfFile)
                            _stringBuffer.Append(ConsumeEscape(current));
                        else
                        {
                            RaiseErrorOccurred(CssParseError.EOF);
                            Back();
                            return CssStringToken.Plain(FlushBuffer(), true);
                        }

                        break;

                    default:
                        _stringBuffer.Append(current);
                        break;
                }
            }
        }

        /// <summary>
        /// 4.4.3. Single quoted string state
        /// </summary>
        CssToken StringSQ()
        {
            while (true)
            {
                var current = GetNext();

                switch (current)
                {
                    case Symbols.SingleQuote:
                    case Symbols.EndOfFile:
                        return CssStringToken.Plain(FlushBuffer());

                    case Symbols.FormFeed:
                    case Symbols.LineFeed:
                        RaiseErrorOccurred(CssParseError.LineBreakUnexpected);
                        Back();
                        return (CssStringToken.Plain(FlushBuffer(), true));

                    case Symbols.ReverseSolidus:
                        current = GetNext();

                        if (current.IsLineBreak())
                            _stringBuffer.AppendLine();
                        else if (current != Symbols.EndOfFile)
                            _stringBuffer.Append(ConsumeEscape(current));
                        else
                        {
                            RaiseErrorOccurred(CssParseError.EOF);
                            Back();
                            return(CssStringToken.Plain(FlushBuffer(), true));
                        }

                        break;

                    default:
                        _stringBuffer.Append(current);
                        break;
                }
            }
        }

        /// <summary>
        /// 4.4.4. Hash state
        /// </summary>
        CssToken HashStart()
        {
            var current = GetNext();

            if (current.IsNameStart())
            {
                _stringBuffer.Append(current);
                return HashRest();
            }
            else if (IsValidEscape(current))
            {
                current = GetNext();
                _stringBuffer.Append(ConsumeEscape(current));
                return HashRest();
            }
            else if (current == Symbols.ReverseSolidus)
            {
                RaiseErrorOccurred(CssParseError.InvalidCharacter);
                Back();
                return CssToken.Delim(Symbols.Num);
            }
            else
            {
                Back();
                return CssToken.Delim(Symbols.Num);
            }
        }

        /// <summary>
        /// 4.4.5. Hash-rest state
        /// </summary>
        CssToken HashRest()
        {
            while (true)
            {
                var current = GetNext();

                if (current.IsName())
                {
                    _stringBuffer.Append(current);
                }
                else if (IsValidEscape(current))
                {
                    current = GetNext();
                    _stringBuffer.Append(ConsumeEscape(current));
                }
                else if (current == Symbols.ReverseSolidus)
                {
                    RaiseErrorOccurred(CssParseError.InvalidCharacter);
                    Back();
                    return CssKeywordToken.Hash(FlushBuffer());
                }
                else
                {
                    Back();
                    return CssKeywordToken.Hash(FlushBuffer());
                }
            }
        }

        /// <summary>
        /// 4.4.6. Comment state
        /// </summary>
        CssToken Comment()
        {
            while (true)
            {
                var current = GetNext();

                if (current == Symbols.Asterisk)
                {
                    current = GetNext();

                    if (current == Symbols.Solidus)
                        return Data(GetNext());
                }
                else if (current == Symbols.EndOfFile)
                {
                    RaiseErrorOccurred(CssParseError.EOF);
                    return Data(current);
                }
            }
        }

        /// <summary>
        /// 4.4.7. At-keyword state
        /// </summary>
        CssToken AtKeywordStart()
        {
            var current = GetNext();

            if (current == Symbols.Minus)
            {
                current = GetNext();

                if (current.IsNameStart() || IsValidEscape(current))
                {
                    _stringBuffer.Append(Symbols.Minus);
                    return AtKeywordRest(current);
                }

                Back(2);
                return CssToken.Delim(Symbols.At);
            }
            else if (current.IsNameStart())
            {
                _stringBuffer.Append(current);
                return AtKeywordRest(GetNext());
            }
            else if (IsValidEscape(current))
            {
                current = GetNext();
                _stringBuffer.Append(ConsumeEscape(current));
                return AtKeywordRest(GetNext());
            }
            else
            {
                Back();
                return CssToken.Delim(Symbols.At);
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
                {
                    _stringBuffer.Append(current);
                }
                else if (IsValidEscape(current))
                {
                    current = GetNext();
                    _stringBuffer.Append(ConsumeEscape(current));
                }
                else
                {
                    Back();
                    return CssKeywordToken.At(FlushBuffer());
                }

                current = GetNext();
            }
        }

        /// <summary>
        /// 4.4.9. Ident state
        /// </summary>
        CssToken IdentStart(Char current)
        {
            if (current == Symbols.Minus)
            {
                current = GetNext();

                if (current.IsNameStart() || IsValidEscape(current))
                {
                    _stringBuffer.Append(Symbols.Minus);
                    return IdentRest(current);
                }

                Back();
                return CssToken.Delim(Symbols.Minus);
            }
            else if (current.IsNameStart())
            {
                _stringBuffer.Append(current);
                return IdentRest(GetNext());
            }
            else if (current == Symbols.ReverseSolidus)
            {
                if (IsValidEscape(current))
                {
                    current = GetNext();
                    _stringBuffer.Append(ConsumeEscape(current));
                    return IdentRest(GetNext());
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
                {
                    _stringBuffer.Append(current);
                }
                else if (IsValidEscape(current))
                {
                    current = GetNext();
                    _stringBuffer.Append(ConsumeEscape(current));
                }
                else if (current == Symbols.RoundBracketOpen)
                {
                    var fn = _stringBuffer.ToString().ToLowerInvariant();

                    if (fn == FunctionNames.Url)
                    {
                        _stringBuffer.Clear();
                        return UrlStart(CssTokenType.Url);
                    }
                    else if (fn == FunctionNames.Domain)
                    {
                        _stringBuffer.Clear();
                        return UrlStart(CssTokenType.Domain);
                    }
                    else if (fn == FunctionNames.Url_Prefix)
                    {
                        _stringBuffer.Clear();
                        return UrlStart(CssTokenType.UrlPrefix);
                    }

                    return CssKeywordToken.Function(FlushBuffer());

                }
                //false could be replaced with a transform whitespace flag, which is set to true if in SVG transform mode.
                //else if (false && Specification.IsSpaceCharacter(current))
                //    InstantSwitch(TransformFunctionWhitespace);
                else
                {
                    Back();
                    return CssKeywordToken.Ident(FlushBuffer());
                }

                current = GetNext();
            }
        }

        /// <summary>
        /// 4.4.11. Transform-function-whitespace state
        /// </summary>
        CssToken TransformFunctionWhitespace(Char current)
        {
            while (true)
            {
                current = GetNext();

                if (current == Symbols.RoundBracketOpen)
                {
                    Back();
                    return CssKeywordToken.Function(FlushBuffer());
                }
                else if (!current.IsSpaceCharacter())
                {
                    Back(2);
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
                if (current == Symbols.Plus || current == Symbols.Minus)
                {
                    _stringBuffer.Append(current);
                    current = GetNext();

                    if (current == Symbols.Dot)
                    {
                        _stringBuffer.Append(current);
                        _stringBuffer.Append(GetNext());
                        return NumberFraction();
                    }

                    _stringBuffer.Append(current);
                    return NumberRest();
                }
                else if (current == Symbols.Dot)
                {
                    _stringBuffer.Append(current);
                    _stringBuffer.Append(GetNext());
                    return NumberFraction();
                }
                else if (current.IsDigit())
                {
                    _stringBuffer.Append(current);
                    return NumberRest();
                }

                current = GetNext();
            }
        }

        /// <summary>
        /// 4.4.13. Number-rest state
        /// </summary>
        CssToken NumberRest()
        {
            var current = GetNext();

            while (true)
            {

                if (current.IsDigit())
                {
                    _stringBuffer.Append(current);
                }
                else if (current.IsNameStart())
                {
                    var number = FlushBuffer();
                    _stringBuffer.Append(current);
                    return Dimension(number);
                }
                else if (IsValidEscape(current))
                {
                    current = GetNext();
                    var number = FlushBuffer();
                    _stringBuffer.Append(ConsumeEscape(current));
                    return Dimension(number);
                }
                else
                    break;

                current = GetNext();
            }

            switch (current)
            {
                case Symbols.Dot:
                    current = GetNext();

                    if (current.IsDigit())
                    {
                        _stringBuffer.Append(Symbols.Dot).Append(current);
                        return NumberFraction();
                    }

                    Back();
                    return new CssNumberToken(FlushBuffer());

                case '%':
                    return CssUnitToken.Percentage(FlushBuffer());

                case 'e':
                case 'E':
                    return NumberExponential();

                case Symbols.Minus:
                    return NumberDash();

                default:
                    Back();
                    return new CssNumberToken(FlushBuffer());
            }
        }

        /// <summary>
        /// 4.4.14. Number-fraction state
        /// </summary>
        CssToken NumberFraction()
        {
            var current = GetNext();

            while (true)
            {
                if (current.IsDigit())
                {
                    _stringBuffer.Append(current);
                }
                else if (current.IsNameStart())
                {
                    var number = FlushBuffer();
                    _stringBuffer.Append(current);
                    return Dimension(number);
                }
                else if (IsValidEscape(current))
                {
                    current = GetNext();
                    var number = FlushBuffer();
                    _stringBuffer.Append(ConsumeEscape(current));
                    return Dimension(number);
                }
                else
                    break;

                current = GetNext();
            }

            switch (current)
            {
                case 'e':
                case 'E':
                    return NumberExponential();

                case '%':
                    return CssUnitToken.Percentage(FlushBuffer());

                case Symbols.Minus:
                    return NumberDash();

                default:
                    Back();
                    return new CssNumberToken(FlushBuffer());
            }
        }

        /// <summary>
        /// 4.4.15. Dimension state
        /// </summary>
        CssToken Dimension(String number)
        {
            while (true)
            {
                var current = GetNext();

                if (current.IsLetter())
                {
                    _stringBuffer.Append(current);
                }
                else if (IsValidEscape(current))
                {
                    current = GetNext();
                    _stringBuffer.Append(ConsumeEscape(current));
                }
                else
                {
                    Back();
                    return CssUnitToken.Dimension(number, FlushBuffer());
                }
            }
        }

        /// <summary>
        /// 4.4.16. SciNotation state
        /// </summary>
        CssToken SciNotation()
        {
            while (true)
            {
                var current = GetNext();

                if (current.IsDigit())
                {
                    _stringBuffer.Append(current);
                }
                else
                {
                    Back();
                    return new CssNumberToken(FlushBuffer());
                }
            }
        }

        /// <summary>
        /// 4.4.17. URL state
        /// </summary>
        CssToken UrlStart(CssTokenType type)
        {
            var current = SkipSpaces();

            switch (current)
            {
                case Symbols.EndOfFile:
                    RaiseErrorOccurred(CssParseError.EOF);
                    return CssStringToken.Url(type, String.Empty, true);

                case Symbols.DoubleQuote:
                    return UrlDQ(type);

                case Symbols.SingleQuote:
                    return UrlSQ(type);

                case ')':
                    return CssStringToken.Url(type, String.Empty, false);

                default:
                    return UrlUQ(current, type);
            }
        }

        /// <summary>
        /// 4.4.18. URL-double-quoted state
        /// </summary>
        CssToken UrlDQ(CssTokenType type)
        {
            while (true)
            {
                var current = GetNext();

                if (current.IsLineBreak())
                {
                    RaiseErrorOccurred(CssParseError.LineBreakUnexpected);
                    return UrlBad(type);
                }
                else if (Symbols.EndOfFile == current)
                {
                    return CssStringToken.Url(type, FlushBuffer());
                }
                else if (current == Symbols.DoubleQuote)
                {
                    return UrlEnd(type);
                }
                else if (current == Symbols.ReverseSolidus)
                {
                    current = GetNext();

                    if (current == Symbols.EndOfFile)
                    {
                        Back(2);
                        RaiseErrorOccurred(CssParseError.EOF);
                        return CssStringToken.Url(type, FlushBuffer(), true);
                    }
                    else if (current.IsLineBreak())
                        _stringBuffer.AppendLine();
                    else
                        _stringBuffer.Append(ConsumeEscape(current));
                }
                else
                    _stringBuffer.Append(current);
            }
        }

        /// <summary>
        /// 4.4.19. URL-single-quoted state
        /// </summary>
        CssToken UrlSQ(CssTokenType type)
        {
            while (true)
            {
                var current = GetNext();

                if (current.IsLineBreak())
                {
                    RaiseErrorOccurred(CssParseError.LineBreakUnexpected);
                    return UrlBad(type);
                }
                else if (Symbols.EndOfFile == current)
                {
                    return CssStringToken.Url(type, FlushBuffer());
                }
                else if (current == Symbols.SingleQuote)
                {
                    return UrlEnd(type);
                }
                else if (current == Symbols.ReverseSolidus)
                {
                    current = GetNext();

                    if (current == Symbols.EndOfFile)
                    {
                        Back(2);
                        RaiseErrorOccurred(CssParseError.EOF);
                        return CssStringToken.Url(type, FlushBuffer(), true);
                    }
                    else if (current.IsLineBreak())
                        _stringBuffer.AppendLine();
                    else
                        _stringBuffer.Append(ConsumeEscape(current));
                }
                else
                    _stringBuffer.Append(current);
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
                    return UrlEnd(type);
                }
                else if (current == Symbols.RoundBracketClose || current == Symbols.EndOfFile)
                {
                    return CssStringToken.Url(type, FlushBuffer());
                }
                else if (current == Symbols.DoubleQuote || current == Symbols.SingleQuote || current == Symbols.RoundBracketOpen || current.IsNonPrintable())
                {
                    RaiseErrorOccurred(CssParseError.InvalidCharacter);
                    return UrlBad(type);
                }
                else if (current == Symbols.ReverseSolidus)
                {
                    if (IsValidEscape(current))
                    {
                        current = GetNext();
                        _stringBuffer.Append(ConsumeEscape(current));
                    }
                    else
                    {
                        RaiseErrorOccurred(CssParseError.InvalidCharacter);
                        return UrlBad(type);
                    }
                }
                else
                    _stringBuffer.Append(current);

                current = GetNext();
            }
        }

        /// <summary>
        /// 4.4.20. URL-end state
        /// </summary>
        CssToken UrlEnd(CssTokenType type)
        {
            while (true)
            {
                var current = GetNext();

                if (current == Symbols.RoundBracketClose)
                {
                    return CssStringToken.Url(type, FlushBuffer());
                }
                else if (!current.IsSpaceCharacter())
                {
                    RaiseErrorOccurred(CssParseError.InvalidCharacter);
                    Back();
                    return UrlBad(type);
                }
            }
        }

        /// <summary>
        /// 4.4.22. Bad URL state
        /// </summary>
        CssToken UrlBad(CssTokenType type)
        {
            while (true)
            {
                var current = GetNext();

                if (current == Symbols.EndOfFile)
                {
                    RaiseErrorOccurred(CssParseError.EOF);
                    return CssStringToken.Url(type, FlushBuffer(), true);
                }
                else if (current == Symbols.RoundBracketClose)
                {
                    return CssStringToken.Url(type, FlushBuffer(), true);
                }
                else if (IsValidEscape(current))
                {
                    current = GetNext();
                    _stringBuffer.Append(ConsumeEscape(current));
                }
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
                current = GetNext();
            }

            if (_stringBuffer.Length != 6)
            {
                for (int i = 0; i < 6 - _stringBuffer.Length; i++)
                {
                    if (current != Symbols.QuestionMark)
                    {
                        current = GetPrevious();
                        break;
                    }

                    _stringBuffer.Append(current);
                    current = GetNext();
                }

                var range = FlushBuffer();
                var start = range.Replace(Symbols.QuestionMark, '0');
                var end = range.Replace(Symbols.QuestionMark, 'F');
                return new CssRangeToken(start, end);
            }
            else if (current == Symbols.Minus)
            {
                current = GetNext();

                if (current.IsHex())
                {
                    var start = _stringBuffer.ToString();
                    _stringBuffer.Clear();

                    for (int i = 0; i < 6; i++)
                    {
                        if (!current.IsHex())
                        {
                            current = GetPrevious();
                            break;
                        }

                        _stringBuffer.Append(current);
                        current = GetNext();
                    }

                    var end = FlushBuffer();
                    return new CssRangeToken(start, end);
                }
                else
                {
                    Back(2);
                    return new CssRangeToken(FlushBuffer(), null);
                }
            }
            else
            {
                Back();
                return new CssRangeToken(FlushBuffer(), null);
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
        CssToken NumberExponential()
        {
            var current = GetNext();

            if (current.IsDigit())
            {
                _stringBuffer.Append('e').Append(current);
                return SciNotation();
            }
            else if (current == Symbols.Plus || current == Symbols.Minus)
            {
                var op = current;
                current = GetNext();

                if (current.IsDigit())
                {
                    _stringBuffer.Append('e').Append(op).Append(current);
                    return SciNotation();
                }

                Back();
            }

            current = GetPrevious();
            var number = FlushBuffer();
            _stringBuffer.Append(current);
            return Dimension(number);
        }

        /// <summary>
        /// Substate of several Number states.
        /// </summary>
        CssToken NumberDash()
        {
            var current = GetNext();

            if (current.IsNameStart())
            {
                var number = FlushBuffer();
                _stringBuffer.Append(Symbols.Minus).Append(current);
                return Dimension(number);
            }
            else if (IsValidEscape(current))
            {
                current = GetNext();
                var number = FlushBuffer();
                _stringBuffer.Append(Symbols.Minus).Append(ConsumeEscape(current));
                return Dimension(number);
            }
            else
            {
                Back(2);
                return new CssNumberToken(FlushBuffer());
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
                    current = GetNext();

                    if (!current.IsHex())
                        break;
                }

                if (current != Symbols.Space)
                    Back();

                var code = Int32.Parse(new String(escape.ToArray()), NumberStyles.HexNumber);
                return code.ConvertFromUtf32();
            }

            return current.ToString();
        }

        /// <summary>
        /// Checks if the current position is the beginning of a valid escape sequence.
        /// </summary>
        /// <returns>The result of the check.</returns>
        Boolean IsValidEscape(Char current)
        {
            if (current != Symbols.ReverseSolidus)
                return false;

            current = GetNext();
            Back();

            if (current == Symbols.EndOfFile)
                return false;
            else if (current.IsLineBreak())
                return false;

            return true;
        }

        #endregion
    }
}
