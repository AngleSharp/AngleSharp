namespace AngleSharp.Css.Parser
{
    using AngleSharp.Common;
    using AngleSharp.Text;
    using System;
    using System.Text;

    /// <summary>
    /// The CSS tokenizer.
    /// See http://dev.w3.org/csswg/css-syntax/#tokenization for more details.
    /// </summary>
    sealed class CssTokenizer
	{
        #region Fields

        private readonly StringSource _source;

        #endregion

        #region ctor

        public CssTokenizer(StringSource source)
        {
            _source = source;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the next available token.
        /// </summary>
        public CssSelectorToken Get()
        {
            return Data(_source.Current);
        }

        #endregion

        #region States

        /// <summary>
        /// 4.4.1. Data state
        /// </summary>
        private CssSelectorToken Data(Char current)
        {
            switch (current)
            {
                case Symbols.FormFeed:
                case Symbols.LineFeed:
                case Symbols.CarriageReturn:
                case Symbols.Tab:
                case Symbols.Space:
                    _source.SkipSpaces();
                    return new CssSelectorToken(CssTokenType.Whitespace, " ");

                case Symbols.Num:
                    return HashStart();

                case Symbols.Dot:
                    current = _source.Next();

                    if (current.IsDigit())
                    {
                        return NumberStart(_source.Back());
                    }
                    else
                    {
                        var token = Data(current);

                        if (token.Type == CssTokenType.Ident)
                        {
                            return new CssSelectorToken(CssTokenType.Class, token.Data);
                        }
                    }

                    return NewInvalid();

                case Symbols.Asterisk:
                    current = _source.Next();

                    if (current == Symbols.Equality)
                    {
                        _source.Next();
                        return NewMatch(CombinatorSymbols.InText);
                    }

                    return NewDelimiter(Symbols.Asterisk);

                case Symbols.Comma:
                    _source.Next();
                    return new CssSelectorToken(CssTokenType.Comma, ",");

                case Symbols.GreaterThan:
                    current = _source.Next();

                    if (current == Symbols.GreaterThan)
                    {
                        current = _source.Next();

                        if (current == Symbols.GreaterThan)
                        {
                            _source.Next();
                            return new CssSelectorToken(CssTokenType.Deep, ">>>");
                        }
                        
                        return new CssSelectorToken(CssTokenType.Descendent, ">>");
                    }

                    return NewDelimiter(Symbols.GreaterThan);

                case Symbols.Minus:
                {
                    var c1 = _source.Next();

                    if (c1 != Symbols.EndOfFile)
                    {
                        var c2 = _source.Next();
                        _source.Back(2);

                        if (c1.IsDigit() || (c1 == Symbols.Dot && c2.IsDigit()))
                        {
                            return NumberStart(current);
                        }
                        else if (c1.IsNameStart())
                        {
                            return IdentStart(current);
                        }
                        else if (c1 == Symbols.ReverseSolidus && !c2.IsLineBreak() && c2 != Symbols.EndOfFile)
                        {
                            return IdentStart(current);
                        }
                        else if (c1 == Symbols.Minus && c2 == Symbols.GreaterThan)
                        {
                            _source.Next(2);
                            return NewInvalid();
                        }

                        _source.Next();
                    }

                    return NewDelimiter(Symbols.Minus);
                }

                case Symbols.Plus:
                {
                    var c1 = _source.Next();

                    if (c1 != Symbols.EndOfFile)
                    {
                        var c2 = _source.Next();
                        _source.Back();

                        if (c1.IsDigit() || (c1 == Symbols.Dot && c2.IsDigit()))
                        {
                            _source.Back();
                            return NumberStart(current);
                        }
                    }

                    return NewDelimiter(Symbols.Plus);
                }

                case Symbols.Colon:
                    _source.Next();
                    return new CssSelectorToken(CssTokenType.Colon, ":");

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

                case Symbols.DoubleQuote:
                    return StringDQ();

                case Symbols.SingleQuote:
                    return StringSQ();

                case 'U':
                case 'u':
                    current = _source.Next();

                    if (current == Symbols.Plus)
                    {
                        current = _source.Next();

                        if (current.IsHex() || current == Symbols.QuestionMark)
                        {
                            return UnicodeRange(current);
                        }

                        current = _source.Back();
                    }

                    return IdentStart(_source.Back());

                case Symbols.SquareBracketOpen:
                    _source.Next();
                    return new CssSelectorToken(CssTokenType.SquareBracketOpen, "[");

                case Symbols.SquareBracketClose:
                    _source.Next();
                    return new CssSelectorToken(CssTokenType.SquareBracketClose, "]");

                case Symbols.RoundBracketClose:
                    _source.Next();
                    return new CssSelectorToken(CssTokenType.RoundBracketClose, ")");

                case Symbols.Solidus:
                    current = _source.Next();

                    if (current == Symbols.Asterisk)
                    {
                        return Data(_source.SkipCssComment());
                    }

                    return NewDelimiter(Symbols.Solidus);

                case Symbols.ReverseSolidus:
                    current = _source.Next();

                    if (current.IsLineBreak() || current == Symbols.EndOfFile)
                    {
                        return NewDelimiter(Symbols.ReverseSolidus);
                    }

                    return IdentStart(_source.Back());

                case Symbols.LessThan:
                    current = _source.Next();

                    if (current == Symbols.ExclamationMark)
                    {
                        current = _source.Next();

                        if (current == Symbols.Minus)
                        {
                            current = _source.Next();

                            if (current == Symbols.Minus)
                            {
                                _source.Next();
                                return NewInvalid();
                            }

                            current = _source.Back();
                        }

                        current = _source.Back();
                    }

                    return NewDelimiter(Symbols.LessThan);

                case Symbols.Accent:
                    current = _source.Next();

                    if (current == Symbols.Equality)
                    {
                        _source.Next();
                        return NewMatch(CombinatorSymbols.Begins);
                    }

                    return NewDelimiter(Symbols.Accent);

                case Symbols.EndOfFile:
                    return new CssSelectorToken(CssTokenType.EndOfFile, String.Empty);

                case Symbols.Pipe:
                    current = _source.Next();

                    if (current == Symbols.Equality)
                    {
                        _source.Next();
                        return NewMatch(CombinatorSymbols.InToken);
                    }
                    else if (current == Symbols.Pipe)
                    {
                        _source.Next();
                        return new CssSelectorToken(CssTokenType.Column, CombinatorSymbols.Column);
                    }

                    return NewDelimiter(Symbols.Pipe);

                case Symbols.Dollar:
                    current = _source.Next();

                    if (current == Symbols.Equality)
                    {
                        _source.Next();
                        return NewMatch(CombinatorSymbols.Ends);
                    }

                    return NewDelimiter(Symbols.Dollar);

                case Symbols.Tilde:
                    current = _source.Next();

                    if (current == Symbols.Equality)
                    {
                        _source.Next();
                        return NewMatch(CombinatorSymbols.InList);
                    }

                    return NewDelimiter(Symbols.Tilde);

                case Symbols.ExclamationMark:
                    current = _source.Next();

                    if (current == Symbols.Equality)
                    {
                        _source.Next();
                        return NewMatch(CombinatorSymbols.Unlike);
                    }

                    return NewDelimiter(Symbols.ExclamationMark);

                case Symbols.At:
                    return AtKeywordStart();

                case Symbols.RoundBracketOpen:
                case Symbols.CurlyBracketOpen:
                case Symbols.CurlyBracketClose:
                case Symbols.Semicolon:
                    _source.Next();
                    return NewInvalid();

                default:
                    if (current.IsNameStart())
                    {
                        return IdentStart(current);
                    }

                    _source.Next();
                    return NewDelimiter(current);
            }
        }

        /// <summary>
        /// 4.4.2. Double quoted string state
        /// </summary>
        private CssSelectorToken StringDQ()
        {
            using var buffer = new ValueStringBuilder(100);

            while (true)
            {
                var current = _source.Next();

                switch (current)
                {
                    case Symbols.DoubleQuote:
                    case Symbols.EndOfFile:
                        _source.Next();
                        return NewString(buffer.ToString());

                    case Symbols.FormFeed:
                    case Symbols.LineFeed:
                        return NewString(buffer.ToString());

                    case Symbols.ReverseSolidus:
                        current = _source.Next();

                        if (current.IsLineBreak())
                        {
                            buffer.Append(Environment.NewLine);
                        }
                        else if (current != Symbols.EndOfFile)
                        {
                            _source.Back();
                            buffer.Append(_source.ConsumeEscape());
                        }
                        else
                        {
                            return NewString(buffer.ToString());
                        }

                        break;

                    default:
                        buffer.Append(current);
                        break;
                }
            }
        }

        /// <summary>
        /// 4.4.3. Single quoted string state
        /// </summary>
        private CssSelectorToken StringSQ()
        {
            using var buffer = new ValueStringBuilder(100);

            while (true)
            {
                var current = _source.Next();

                switch (current)
                {
                    case Symbols.SingleQuote:
                    case Symbols.EndOfFile:
                        _source.Next();
                        return NewString(buffer.ToString());

                    case Symbols.FormFeed:
                    case Symbols.LineFeed:
                        return NewString(buffer.ToString());

                    case Symbols.ReverseSolidus:
                        current = _source.Next();

                        if (current.IsLineBreak())
                        {
                            buffer.Append(Environment.NewLine);
                        }
                        else if (current != Symbols.EndOfFile)
                        {
                            _source.Back();
                            buffer.Append(_source.ConsumeEscape());
                        }
                        else
                        {
                            return NewString(buffer.ToString());
                        }

                        break;

                    default:
                        buffer.Append(current);
                        break;
                }
            }
        }

        /// <summary>
        /// 4.4.4. Hash state
        /// </summary>
        private CssSelectorToken HashStart()
        {
            var current = _source.Next();

            if (current.IsNameStart() || current == Symbols.Minus)
            {
                var sb = new ValueStringBuilder(64);
                sb.Append(current);
                return HashRest(ref sb);
            }
            else if (_source.IsValidEscape())
            {
                var sb = new ValueStringBuilder(64);
                sb.Append(_source.ConsumeEscape());
                return HashRest(ref sb);
            }
            else
            {
                return NewDelimiter(Symbols.Num);
            }
        }

        /// <summary>
        /// 4.4.5. Hash-rest state
        /// </summary>
        private CssSelectorToken HashRest(ref ValueStringBuilder sb)
        {
            while (true)
            {
                var current = _source.Next();

                if (current.IsName())
                {
                    sb.Append(current);
                }
                else if (_source.IsValidEscape())
                {
                    sb.Append(_source.ConsumeEscape());
                }
                else
                {
                    return new CssSelectorToken(CssTokenType.Hash, sb.ToString());
                }
            }
        }

        /// <summary>
        /// 4.4.7. At-keyword state
        /// </summary>
        private CssSelectorToken AtKeywordStart()
        {
            var current = _source.Next();

            if (current == Symbols.Minus)
            {
                current = _source.Next();

                if (current.IsNameStart() || _source.IsValidEscape())
                {
                    return AtKeywordRest(current);
                }

                _source.Back();
                return NewDelimiter(Symbols.At);
            }
            else if (current.IsNameStart())
            {
                return AtKeywordRest(_source.Next());
            }
            else if (_source.IsValidEscape())
            {
                _source.ConsumeEscape();
                return AtKeywordRest(_source.Next());
            }
            else
            {
                return NewDelimiter(Symbols.At);
            }
        }

        /// <summary>
        /// 4.4.8. At-keyword-rest state
        /// </summary>
        private CssSelectorToken AtKeywordRest(Char current)
        {
            while (true)
            {
                if (current.IsName())
                {
                }
                else if (_source.IsValidEscape())
                {
                    _source.ConsumeEscape();
                }
                else
                {
                    return NewInvalid();
                }

                current = _source.Next();
            }
        }

        /// <summary>
        /// 4.4.9. Ident state
        /// </summary>
        private CssSelectorToken IdentStart(Char current)
        {
            const int estimatedIdentLength = 20;

            if (current == Symbols.Minus)
            {
                current = _source.Next();

                if (current.IsNameStart() || _source.IsValidEscape())
                {
                    var sb = new ValueStringBuilder(stackalloc char[estimatedIdentLength]);

                    sb.Append(Symbols.Minus);
                    return IdentRest(current, ref sb);
                }
                
                return NewDelimiter(Symbols.Minus);
            }
            else if (current.IsNameStart())
            {
                var sb = new ValueStringBuilder(stackalloc char[estimatedIdentLength]);
                sb.Append(current);
                return IdentRest(_source.Next(), ref sb);
            }
            else if (current == Symbols.ReverseSolidus && _source.IsValidEscape())
            {
                var sb = new ValueStringBuilder(stackalloc char[estimatedIdentLength]);
                sb.Append(_source.ConsumeEscape());
                return IdentRest(_source.Next(), ref sb);
            }

            return Data(current);
        }

        /// <summary>
        /// 4.4.10. Ident-rest state
        /// </summary>
        private CssSelectorToken IdentRest(Char current, ref ValueStringBuilder sb)
        {
            while (true)
            {
                if (current.IsName())
                {
                    sb.Append(current);
                }
                else if (_source.IsValidEscape())
                {
                    sb.Append(_source.ConsumeEscape());
                }
                else if (current == Symbols.RoundBracketOpen)
                {
                    var name = sb.ToString();

                    if (name.Isi(Keywords.Url))
                    {
                        return UrlStart();
                    }

                    _source.Next();
                    return new CssSelectorToken(CssTokenType.Function, name);
                }
                else
                {
                    return new CssSelectorToken(CssTokenType.Ident, sb.ToString());
                }

                current = _source.Next();
            }
        }

        /// <summary>
        /// 4.4.12. Number state
        /// </summary>
        private CssSelectorToken NumberStart(Char current)
        {
            const int estimatedNumberLength = 16;

            while (true)
            {
                if (current is Symbols.Plus or Symbols.Minus)
                {
                    var sb = new ValueStringBuilder(stackalloc char[estimatedNumberLength]);

                    sb.Append(current);
                    current = _source.Next();

                    if (current == Symbols.Dot)
                    {
                        sb.Append(current);
                        sb.Append(_source.Next());
                        return NumberFraction(ref sb);
                    }

                    sb.Append(current);
                    return NumberRest(ref sb);
                }
                else if (current == Symbols.Dot)
                {
                    var sb = new ValueStringBuilder(stackalloc char[estimatedNumberLength]);
                    sb.Append(current);
                    sb.Append(_source.Next());
                    return NumberFraction(ref sb);
                }
                else if (current.IsDigit())
                {
                    var sb = new ValueStringBuilder(stackalloc char[estimatedNumberLength]);
                    sb.Append(current);
                    return NumberRest(ref sb);
                }

                current = _source.Next();
            }
        }

        /// <summary>
        /// 4.4.13. Number-rest state
        /// </summary>
        private CssSelectorToken NumberRest(ref ValueStringBuilder sb)
        {
            var current = _source.Next();

            while (true)
            {
                if (current.IsDigit())
                {
                    sb.Append(current);
                }
                else if (current.IsNameStart())
                {
                    sb.Append(current);
                    return Dimension(ref sb);
                }
                else if (_source.IsValidEscape())
                {
                    sb.Append(_source.ConsumeEscape());
                    return Dimension(ref sb);
                }
                else
                {
                    break;
                }

                current = _source.Next();
            }

            switch (current)
            {
                case Symbols.Dot:
                    current = _source.Next();

                    if (current.IsDigit())
                    {
                        sb.Append(Symbols.Dot);
                        sb.Append(current);
                        return NumberFraction(ref sb);
                    }

                    return NewNumber(sb.ToString());

                case '%':
                    _source.Next();
                    sb.Append('%');
                    return NewDimension(sb.ToString());

                case 'e':
                case 'E':
                    return NumberExponential(current, ref sb);

                case Symbols.Minus:
                    return NumberDash(ref sb);

                default:
                    return NewNumber(sb.ToString());
            }
        }

        /// <summary>
        /// 4.4.14. Number-fraction state
        /// </summary>
        private CssSelectorToken NumberFraction(ref ValueStringBuilder sb)
        {
            var current = _source.Next();

            while (true)
            {
                if (current.IsDigit())
                {
                    sb.Append(current);
                }
                else if (current.IsNameStart())
                {
                    sb.Append(current);
                    return Dimension(ref sb);
                }
                else if (_source.IsValidEscape())
                {
                    sb.Append(_source.ConsumeEscape());
                    return Dimension(ref sb);
                }
                else
                {
                    break;
                }

                current = _source.Next();
            }

            switch (current)
            {
                case 'e':
                case 'E':
                    return NumberExponential(current, ref sb);

                case '%':
                    _source.Next();
                    sb.Append('%');
                    return NewDimension(sb.ToString());

                case Symbols.Minus:
                    return NumberDash(ref sb);

                default:
                    return NewNumber(sb.ToString());
            }
        }

        /// <summary>
        /// 4.4.15. Dimension state
        /// </summary>
        private CssSelectorToken Dimension(ref ValueStringBuilder sb)
        {
            while (true)
            {
                var current = _source.Next();

                if (current.IsLetter())
                {
                    sb.Append(current);
                }
                else if (_source.IsValidEscape())
                {
                    sb.Append(_source.ConsumeEscape());
                }
                else
                {
                    return NewDimension(sb.ToString());
                }
            }
        }

        /// <summary>
        /// 4.4.16. SciNotation state
        /// </summary>
        private CssSelectorToken SciNotation(ref ValueStringBuilder sb)
        {
            while (true)
            {
                var current = _source.Next();

                if (current.IsDigit())
                {
                    sb.Append(current);
                }
                else
                {
                    return NewNumber(sb.ToString());
                }
            }
        }

        /// <summary>
        /// 4.4.17. URL state
        /// </summary>
        private CssSelectorToken UrlStart()
        {
            var current = _source.SkipSpaces();

            switch (current)
            {
                case Symbols.EndOfFile:
                    return NewInvalid();

                case Symbols.DoubleQuote:
                    return UrlDQ();

                case Symbols.SingleQuote:
                    return UrlSQ();

                case Symbols.RoundBracketClose:
                    _source.Next();
                    return NewInvalid();

                default:
                    return UrlUQ(current);
            }
        }

        /// <summary>
        /// 4.4.18. URL-double-quoted state
        /// </summary>
        private CssSelectorToken UrlDQ()
        {
            while (true)
            {
                var current = _source.Next();

                if (current.IsLineBreak())
                {
                    return UrlBad();
                }
                else if (Symbols.EndOfFile == current)
                {
                    return NewInvalid();
                }
                else if (current == Symbols.DoubleQuote)
                {
                    return UrlEnd();
                }
                else if (current == Symbols.ReverseSolidus)
                {
                    current = _source.Next();

                    if (current == Symbols.EndOfFile)
                    {
                        return NewInvalid();
                    }
                    else if (!current.IsLineBreak())
                    {
                        _source.Back();
                        _source.ConsumeEscape();
                    }
                }
            }
        }

        /// <summary>
        /// 4.4.19. URL-single-quoted state
        /// </summary>
        private CssSelectorToken UrlSQ()
        {
            while (true)
            {
                var current = _source.Next();

                if (current.IsLineBreak())
                {
                    return UrlBad();
                }
                else if (current == Symbols.EndOfFile)
                {
                    return NewInvalid();
                }
                else if (current == Symbols.SingleQuote)
                {
                    return UrlEnd();
                }
                else if (current == Symbols.ReverseSolidus)
                {
                    current = _source.Next();

                    if (current == Symbols.EndOfFile)
                    {
                        return NewInvalid();
                    }
                    else if (!current.IsLineBreak())
                    {
                        _source.Back();
                        _source.ConsumeEscape();
                    }
                }
            }
        }

        /// <summary>
        /// 4.4.21. URL-unquoted state
        /// </summary>
        private CssSelectorToken UrlUQ(Char current)
        {
            while (true)
            {
                if (current.IsSpaceCharacter())
                {
                    return UrlEnd();
                }
                else if (current is Symbols.RoundBracketClose or Symbols.EndOfFile)
                {
                    _source.Next();
                    return NewInvalid();
                }
                else if (current is Symbols.DoubleQuote or Symbols.SingleQuote or Symbols.RoundBracketOpen || current.IsNonPrintable())
                {
                    return UrlBad();
                }
                else if (current == Symbols.ReverseSolidus && _source.IsValidEscape())
                {
                    _source.ConsumeEscape();
                }
                else
                {
                    return UrlBad();
                }

                current = _source.Next();
            }
        }

        /// <summary>
        /// 4.4.20. URL-end state
        /// </summary>
        private CssSelectorToken UrlEnd()
        {
            while (true)
            {
                var current = _source.Next();

                if (current == Symbols.RoundBracketClose)
                {
                    _source.Next();
                    return NewInvalid();
                }
                else if (!current.IsSpaceCharacter())
                {
                    _source.Back();
                    return UrlBad();
                }
            }
        }

        /// <summary>
        /// 4.4.22. Bad URL state
        /// </summary>
        private CssSelectorToken UrlBad()
        {
            var current = _source.Current;
            var curly = 0;
            var round = 1;

            while (current != Symbols.EndOfFile)
            {
                if (current == Symbols.Semicolon)
                {
                    return NewInvalid();
                }
                else if (current == Symbols.CurlyBracketClose && --curly == -1)
                {
                    return NewInvalid();
                }
                else if (current == Symbols.RoundBracketClose && --round == 0)
                {
                    return NewInvalid();
                }
                else if (_source.IsValidEscape())
                {
                    _source.ConsumeEscape();
                }
                else
                {
                    if (current == Symbols.RoundBracketOpen)
                    {
                        ++round;
                    }
                    else if (curly == Symbols.CurlyBracketOpen)
                    {
                        ++curly;
                    }
                }

                current = _source.Next();
            }

            return NewInvalid();
        }

        /// <summary>
        /// 4.4.23. Unicode-range State
        /// </summary>
        private CssSelectorToken UnicodeRange(Char current)
        {
            var count = 0;

            for (var i = 0; i < 6 && current.IsHex(); i++)
            {
                ++count;
                current = _source.Next();
            }

            if (count != 6)
            {
                for (var i = 0; i < 6 - count; i++)
                {
                    if (current != Symbols.QuestionMark)
                    {
                        current = _source.Back();
                        break;
                    }
                    
                    current = _source.Next();
                }

                return NewInvalid();
            }
            else if (current == Symbols.Minus)
            {
                current = _source.Next();

                if (current.IsHex())
                {
                    for (var i = 0; i < 6; i++)
                    {
                        if (!current.IsHex())
                        {
                            current = _source.Back();
                            break;
                        }
                        
                        current = _source.Next();
                    }
                    
                    return new CssSelectorToken(CssTokenType.Invalid, String.Empty);
                }
                else
                {
                    _source.Back();
                    return NewInvalid();
                }
            }

            return NewInvalid();
        }

        #endregion

        #region Tokens

        private CssSelectorToken NewMatch(String match)
        {
            return new CssSelectorToken(CssTokenType.Match, match);
        }

        private CssSelectorToken NewInvalid()
        {
            return new CssSelectorToken(CssTokenType.Invalid, String.Empty);
        }

        private CssSelectorToken NewString(String value)
        {
            return new CssSelectorToken(CssTokenType.String, value);
        }

        private CssSelectorToken NewDimension(String value)
        {
            return new CssSelectorToken(CssTokenType.Dimension, value);
        }

        private CssSelectorToken NewNumber(String number)
        {
            return new CssSelectorToken(CssTokenType.Number, number);
        }

        private CssSelectorToken NewDelimiter(Char c)
        {
            return new CssSelectorToken(CssTokenType.Delim, c.ToString());
        }

        #endregion

        #region Helpers

        private CssSelectorToken NumberExponential(Char letter, ref ValueStringBuilder sb)
        {
            var current = _source.Next();

            if (current.IsDigit())
            {
                sb.Append(letter);
                sb.Append(current);
                return SciNotation(ref sb);
            }
            else if (current == Symbols.Plus || current == Symbols.Minus)
            {
                var op = current;
                current = _source.Next();

                if (current.IsDigit())
                {
                    sb.Append(letter);
                    sb.Append(op);
                    sb.Append(current);
                    return SciNotation(ref sb);
                }

                _source.Back();
            }

            sb.Append(letter);
            _source.Back();
            return Dimension(ref sb);
        }

        private CssSelectorToken NumberDash(ref ValueStringBuilder sb)
        {
            var current = _source.Next();

            if (current.IsNameStart())
            {
                sb.Append(Symbols.Minus);
                sb.Append(current);
                return Dimension(ref sb);
            }
            else if (_source.IsValidEscape())
            {
                sb.Append(Symbols.Minus);
                sb.Append(_source.ConsumeEscape());
                return Dimension(ref sb);
            }
            else
            {
                _source.Back();
                return NewNumber(sb.ToString());
            }
        }

        #endregion
    }
}
