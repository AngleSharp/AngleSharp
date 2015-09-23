namespace AngleSharp.Parser.Css
{
    using AngleSharp.Events;
    using AngleSharp.Extensions;
    using System;
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

        Boolean _valueMode;
        TextPosition _position;

        #endregion

        #region ctor

        /// <summary>
        /// CSS Tokenization
        /// </summary>
        /// <param name="source">The source code manager.</param>
        /// <param name="events">The event aggregator to use.</param>
        public CssTokenizer(TextSource source, IEventAggregator events)
            : base(source, events)
        {
            _valueMode = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if we are currently in value mode.
        /// </summary>
        public Boolean IsInValue
        {
            get { return _valueMode; }
            set { _valueMode = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the next available token.
        /// </summary>
        /// <returns>The next available token.</returns>
        public CssToken Get()
        {
            var current = GetNext();
            _position = GetCurrentPosition();
            return Data(current);
        }

        /// <summary>
        /// Fires an error occurred event.
        /// </summary>
        /// <param name="error">The associated error code.</param>
        /// <param name="position">Position of the error.</param>
        public void RaiseErrorOccurred(CssParseError error, TextPosition position)
        {
            if (_events != null)
            {
                var errorEvent = new CssParseErrorEvent(error.GetCode(), error.GetMessage(), position);
                _events.Publish(errorEvent);
            }
        }

        /// <summary>
        /// Fires an error occurred event at the current position.
        /// </summary>
        /// <param name="code">The associated error code.</param>
        public void RaiseErrorOccurred(CssParseError code)
        {
            RaiseErrorOccurred(code, GetCurrentPosition());
        }

        #endregion

        #region States

        /// <summary>
        /// 4.4.1. Data state
        /// </summary>
        CssToken Data(Char current)
        {
            _position = GetCurrentPosition();

            switch (current)
            {
                case Symbols.FormFeed:
                case Symbols.LineFeed:
                case Symbols.CarriageReturn:
                case Symbols.Tab:
                case Symbols.Space:
                    return NewWhitespace(current);

                case Symbols.DoubleQuote:
                    return StringDQ();

                case Symbols.Num:
                    return _valueMode ? ColorLiteral() : HashStart();

                case Symbols.Dollar:
                    current = GetNext();

                    if (current == Symbols.Equality)
                        return NewSuffix();

                    return NewDelimiter(GetPrevious());

                case Symbols.SingleQuote:
                    return StringSQ();

                case Symbols.RoundBracketOpen:
                    return NewOpenRound();

                case Symbols.RoundBracketClose:
                    return NewCloseRound();

                case Symbols.Asterisk:
                    current = GetNext();

                    if (current == Symbols.Equality)
                        return NewSubstring();

                    return NewDelimiter(GetPrevious());

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

                    return NewDelimiter(current);
                }

                case Symbols.Comma:
                    return NewComma();

                case Symbols.Dot:
                {
                    var c = GetNext();

                    if (c.IsDigit())
                        return NumberStart(GetPrevious());

                    return NewDelimiter(GetPrevious());
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
                            return NewCloseComment();
                        }
                    }
                    else
                        Back();

                    return NewDelimiter(current);
                }

                case Symbols.Solidus:
                    current = GetNext();

                    if (current == Symbols.Asterisk)
                        return Comment();

                    return NewDelimiter(GetPrevious());

                case Symbols.ReverseSolidus:
                    current = GetNext();

                    if (current.IsLineBreak())
                    {
                        RaiseErrorOccurred(CssParseError.LineBreakUnexpected);
                        return NewDelimiter(GetPrevious());
                    }
                    else if (current == Symbols.EndOfFile)
                    {
                        RaiseErrorOccurred(CssParseError.EOF);
                        return NewDelimiter(GetPrevious());
                    }

                    return IdentStart(GetPrevious());

                case Symbols.Colon:
                    return NewColon();

                case Symbols.Semicolon:
                    return NewSemicolon();

                case Symbols.LessThan:
                    current = GetNext();

                    if (current == Symbols.ExclamationMark)
                    {
                        current = GetNext();

                        if (current == Symbols.Minus)
                        {
                            current = GetNext();

                            if (current == Symbols.Minus)
                                return NewOpenComment();

                            current = GetPrevious();
                        }

                        current = GetPrevious();
                    }

                    return NewDelimiter(GetPrevious());

                case Symbols.At:
                    return AtKeywordStart();

                case Symbols.SquareBracketOpen:
                    return NewOpenSquare();

                case Symbols.SquareBracketClose:
                    return NewCloseSquare();

                case Symbols.Accent:
                    current = GetNext();

                    if (current == Symbols.Equality)
                        return NewPrefix();

                    return NewDelimiter(GetPrevious());

                case Symbols.CurlyBracketOpen:
                    return NewOpenCurly();

                case Symbols.CurlyBracketClose:
                    return NewCloseCurly();

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
                        return NewDash();
                    else if (current == Symbols.Pipe)
                        return NewColumn();

                    return NewDelimiter(GetPrevious());

                case Symbols.Tilde:
                    current = GetNext();

                    if (current == Symbols.Equality)
                        return NewInclude();

                    return NewDelimiter(GetPrevious());

                case Symbols.EndOfFile:
                    return NewEof();

                case Symbols.ExclamationMark:

                    current = GetNext();

                    if (current == Symbols.Equality)
                        return NewNot();

                    return NewDelimiter(GetPrevious());

                default:
                    if (current.IsNameStart())
                        return IdentStart(current);

                    return NewDelimiter(current);
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
                        return NewString(FlushBuffer());

                    case Symbols.FormFeed:
                    case Symbols.LineFeed:
                        RaiseErrorOccurred(CssParseError.LineBreakUnexpected);
                        Back();
                        return NewString(FlushBuffer(), bad: true);

                    case Symbols.ReverseSolidus:
                        current = GetNext();

                        if (current.IsLineBreak())
                        {
                            _stringBuffer.AppendLine();
                        }
                        else if (current != Symbols.EndOfFile)
                        {
                            _stringBuffer.Append(ConsumeEscape(current));
                        }
                        else
                        {
                            RaiseErrorOccurred(CssParseError.EOF);
                            Back();
                            return NewString(FlushBuffer(), bad: true);
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
                        return NewString(FlushBuffer());

                    case Symbols.FormFeed:
                    case Symbols.LineFeed:
                        RaiseErrorOccurred(CssParseError.LineBreakUnexpected);
                        Back();
                        return NewString(FlushBuffer(), bad: true);

                    case Symbols.ReverseSolidus:
                        current = GetNext();

                        if (current.IsLineBreak())
                        {
                            _stringBuffer.AppendLine();
                        }
                        else if (current != Symbols.EndOfFile)
                        {
                            _stringBuffer.Append(ConsumeEscape(current));
                        }
                        else
                        {
                            RaiseErrorOccurred(CssParseError.EOF);
                            Back();
                            return NewString(FlushBuffer(), bad: true);
                        }

                        break;

                    default:
                        _stringBuffer.Append(current);
                        break;
                }
            }
        }

        /// <summary>
        /// Color literal state.
        /// </summary>
        CssToken ColorLiteral()
        {
            var current = GetNext();

            while (current.IsHex())
            {
                _stringBuffer.Append(current);
                current = GetNext();
            }

            Back();
            return NewColor(FlushBuffer());
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
                return NewDelimiter(Symbols.Num);
            }
            else
            {
                Back();
                return NewDelimiter(Symbols.Num);
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
                    return NewHash(FlushBuffer());
                }
                else
                {
                    Back();
                    return NewHash(FlushBuffer());
                }
            }
        }

        /// <summary>
        /// 4.4.6. Comment state
        /// </summary>
        CssToken Comment()
        {
            var current = GetNext();

            while (current != Symbols.EndOfFile)
            {
                if (current == Symbols.Asterisk)
                {
                    current = GetNext();

                    if (current == Symbols.Solidus)
                        return NewComment(FlushBuffer());

                    _stringBuffer.Append(Symbols.Asterisk);
                }
                else
                {
                    _stringBuffer.Append(current);
                    current = GetNext();
                }
            }

            RaiseErrorOccurred(CssParseError.EOF);
            return NewComment(FlushBuffer(), bad: true);
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
                return NewDelimiter(Symbols.At);
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
                return NewDelimiter(Symbols.At);
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
                    return NewAtKeyword(FlushBuffer());
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
                return NewDelimiter(Symbols.Minus);
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
                    var fn = FlushBuffer();
                    var type = fn.GetTypeFromName();

                    switch (type)
                    {
                        case CssTokenType.Function:
                            return NewFunction(fn);
                        default:
                            return UrlStart(type);
                    }
                }
                else
                {
                    Back();
                    return NewIdent(FlushBuffer());
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
                    return NewFunction(FlushBuffer());
                }
                else if (!current.IsSpaceCharacter())
                {
                    Back(2);
                    return NewIdent(FlushBuffer());
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
                    return NewNumber(FlushBuffer());

                case '%':
                    return NewPercentage(FlushBuffer());

                case 'e':
                case 'E':
                    return NumberExponential(current);

                case Symbols.Minus:
                    return NumberDash();

                default:
                    Back();
                    return NewNumber(FlushBuffer());
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
                    return NumberExponential(current);

                case '%':
                    return NewPercentage(FlushBuffer());

                case Symbols.Minus:
                    return NumberDash();

                default:
                    Back();
                    return NewNumber(FlushBuffer());
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
                    return NewDimension(number, FlushBuffer());
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
                    return NewNumber(FlushBuffer());
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
                    return NewUrl(type, String.Empty, bad: true);

                case Symbols.DoubleQuote:
                    return UrlDQ(type);

                case Symbols.SingleQuote:
                    return UrlSQ(type);

                case Symbols.RoundBracketClose:
                    return NewUrl(type, String.Empty, bad: false);

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
                    return NewUrl(type, FlushBuffer());
                }
                else if (current == Symbols.DoubleQuote)
                {
                    return UrlEnd(type);
                }
                else if (current != Symbols.ReverseSolidus)
                {
                    _stringBuffer.Append(current);
                }
                else
                {
                    current = GetNext();

                    if (current == Symbols.EndOfFile)
                    {
                        Back(2);
                        RaiseErrorOccurred(CssParseError.EOF);
                        return NewUrl(type, FlushBuffer(), bad: true);
                    }
                    else if (current.IsLineBreak())
                        _stringBuffer.AppendLine();
                    else
                        _stringBuffer.Append(ConsumeEscape(current));
                }
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
                    return NewUrl(type, FlushBuffer());
                }
                else if (current == Symbols.SingleQuote)
                {
                    return UrlEnd(type);
                }
                else if (current != Symbols.ReverseSolidus)
                {
                    _stringBuffer.Append(current);
                }
                else
                {
                    current = GetNext();

                    if (current == Symbols.EndOfFile)
                    {
                        Back(2);
                        RaiseErrorOccurred(CssParseError.EOF);
                        return NewUrl(type, FlushBuffer(), bad: true);
                    }
                    else if (current.IsLineBreak())
                        _stringBuffer.AppendLine();
                    else
                        _stringBuffer.Append(ConsumeEscape(current));
                }
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
                    return NewUrl(type, FlushBuffer());
                }
                else if (current == Symbols.DoubleQuote || current == Symbols.SingleQuote || current == Symbols.RoundBracketOpen || current.IsNonPrintable())
                {
                    RaiseErrorOccurred(CssParseError.InvalidCharacter);
                    return UrlBad(type);
                }
                else if (current != Symbols.ReverseSolidus)
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
                    RaiseErrorOccurred(CssParseError.InvalidCharacter);
                    return UrlBad(type);
                }

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
                    return NewUrl(type, FlushBuffer());
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
            var current = Current;
            var curly = 0;
            var round = 1;

            while (current != Symbols.EndOfFile)
            {
                if (current == Symbols.Semicolon)
                {
                    Back();
                    return NewUrl(type, FlushBuffer(), true);
                }
                else if (current == Symbols.CurlyBracketClose && --curly == -1)
                {
                    Back();
                    return NewUrl(type, FlushBuffer(), true);
                }
                else if (current == Symbols.RoundBracketClose && --round == 0)
                {
                    return NewUrl(type, FlushBuffer(), true);
                }
                else if (IsValidEscape(current))
                {
                    current = GetNext();
                    _stringBuffer.Append(ConsumeEscape(current));
                }
                else
                {
                    if (current == Symbols.RoundBracketOpen)
                        ++round;
                    else if (curly == Symbols.CurlyBracketOpen)
                        ++curly;

                    _stringBuffer.Append(current);
                }

                current = GetNext();
            }

            RaiseErrorOccurred(CssParseError.EOF);
            return NewUrl(type, FlushBuffer(), bad: true);
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

                return NewRange(FlushBuffer());
            }
            else if (current == Symbols.Minus)
            {
                current = GetNext();

                if (current.IsHex())
                {
                    var start = FlushBuffer();

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
                    return NewRange(start, end);
                }
                else
                {
                    Back(2);
                    return NewRange(FlushBuffer());
                }
            }
            else
            {
                Back();
                return NewRange(FlushBuffer());
            }
        }

        #endregion

        #region Tokens

        CssToken NewNot()
        {
            return new CssToken(CssTokenType.NotMatch, "!=", _position);
        }

        CssToken NewInclude()
        {
            return new CssToken(CssTokenType.IncludeMatch, "~=", _position);
        }

        CssToken NewColumn()
        {
            return new CssToken(CssTokenType.Column, "||", _position);
        }

        CssToken NewDash()
        {
            return new CssToken(CssTokenType.DashMatch, "|=", _position);
        }

        CssToken NewCloseCurly()
        {
            return new CssToken(CssTokenType.CurlyBracketClose, "}", _position);
        }

        CssToken NewOpenCurly()
        {
            return new CssToken(CssTokenType.CurlyBracketOpen, "{", _position);
        }

        CssToken NewPrefix()
        {
            return new CssToken(CssTokenType.PrefixMatch, "^=", _position);
        }

        CssToken NewCloseSquare()
        {
            return new CssToken(CssTokenType.SquareBracketClose, "]", _position);
        }

        CssToken NewOpenSquare()
        {
            return new CssToken(CssTokenType.SquareBracketOpen, "[", _position);
        }

        CssToken NewOpenComment()
        {
            return new CssToken(CssTokenType.Cdo, "<!--", _position);
        }

        CssToken NewSemicolon()
        {
            return new CssToken(CssTokenType.Semicolon, ";", _position);
        }

        CssToken NewColon()
        {
            return new CssToken(CssTokenType.Colon, ":", _position);
        }

        CssToken NewCloseComment()
        {
            return new CssToken(CssTokenType.Cdc, "-->", _position);
        }

        CssToken NewComma()
        {
            return new CssToken(CssTokenType.Comma, ",", _position);
        }

        CssToken NewSubstring()
        {
            return new CssToken(CssTokenType.SubstringMatch, "*=", _position);
        }

        CssToken NewCloseRound()
        {
            return new CssToken(CssTokenType.RoundBracketClose, ")", _position);
        }

        CssToken NewOpenRound()
        {
            return new CssToken(CssTokenType.RoundBracketOpen, "(", _position);
        }

        CssToken NewSuffix()
        {
            return new CssToken(CssTokenType.SuffixMatch, "$=", _position);
        }

        CssToken NewString(String value, Boolean bad = false)
        {
            return new CssStringToken(CssTokenType.String, value, bad, _position);
        }

        CssToken NewHash(String value)
        {
            return new CssKeywordToken(CssTokenType.Hash, value, _position);
        }

        CssToken NewComment(String value, Boolean bad = false)
        {
            return new CssStringToken(CssTokenType.Comment, value, bad, _position);
        }

        CssToken NewAtKeyword(String value)
        {
            return new CssKeywordToken(CssTokenType.AtKeyword, value, _position);
        }

        CssToken NewIdent(String value)
        {
            return new CssKeywordToken(CssTokenType.Ident, value, _position);
        }

        CssToken NewFunction(String value)
        {
            var function = new CssFunctionToken(value, _position);
            var token = Get();

            while (token.Type != CssTokenType.Eof)
            {
                if (token.Type == CssTokenType.RoundBracketClose)
                {
                    function.Close(token);
                    break;
                }

                function.With(token);
                token = Get();
            }

            return function;
        }

        CssToken NewPercentage(String value)
        {
            return new CssUnitToken(CssTokenType.Percentage, value, "%", _position);
        }

        CssToken NewDimension(String value, String unit)
        {
            return new CssUnitToken(CssTokenType.Dimension, value, unit, _position);
        }

        CssToken NewUrl(CssTokenType type, String data, Boolean bad = false)
        {
            return new CssStringToken(type, data, bad, _position);
        }

        CssToken NewRange(String range)
        {
            return new CssRangeToken(range, _position);
        }

        CssToken NewRange(String start, String end)
        {
            return new CssRangeToken(start, end, _position);
        }

        CssToken NewWhitespace(Char c)
        {
            return new CssToken(CssTokenType.Whitespace, c.ToString(), _position);
        }

        CssToken NewNumber(String number)
        {
            return new CssNumberToken(number, _position);
        }

        CssToken NewDelimiter(Char c)
        {
            return new CssToken(CssTokenType.Delim, c, _position);
        }

        CssToken NewColor(String text)
        {
            var bad = text.Length != 3 && text.Length != 6;
            return new CssStringToken(CssTokenType.Color, text, bad, _position);
        }

        CssToken NewEof()
        {
            return new CssToken(CssTokenType.Eof, String.Empty, _position);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Substate of several Number states.
        /// </summary>
        CssToken NumberExponential(Char letter)
        {
            var current = GetNext();

            if (current.IsDigit())
            {
                _stringBuffer.Append(letter).Append(current);
                return SciNotation();
            }
            else if (current == Symbols.Plus || current == Symbols.Minus)
            {
                var op = current;
                current = GetNext();

                if (current.IsDigit())
                {
                    _stringBuffer.Append(letter).Append(op).Append(current);
                    return SciNotation();
                }

                Back();
            }

            var number = FlushBuffer();
            _stringBuffer.Append(letter);
            Back();
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
                return NewNumber(FlushBuffer());
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
                var escape = new Char[6];
                var length = 0;

                while (length < escape.Length)
                {
                    escape[length++] = current;
                    current = GetNext();

                    if (!current.IsHex())
                        break;
                }

                if (!current.IsSpaceCharacter())
                    Back();

                var code = Int32.Parse(new String(escape, 0, length), NumberStyles.HexNumber);

                if (!code.IsInvalid())
                    return code.ConvertFromUtf32();

                current = Symbols.Replacement;
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
