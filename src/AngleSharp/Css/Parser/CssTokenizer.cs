namespace AngleSharp.Css.Parser
{
    using AngleSharp.Common;
    using AngleSharp.Css.Parser.Tokens;
    using AngleSharp.Text;
    using System;
    using System.Globalization;

    /// <summary>
    /// The CSS tokenizer.
    /// See http://dev.w3.org/csswg/css-syntax/#tokenization for more details.
    /// </summary>
    sealed class CssTokenizer : BaseTokenizer
	{
        #region ctor

        /// <summary>
        /// CSS Tokenization
        /// </summary>
        /// <param name="source">The source code manager.</param>
        public CssTokenizer(TextSource source)
            : base(source)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the next available token.
        /// </summary>
        /// <returns>The next available token.</returns>
        public CssToken Get()
        {
            return Data(GetNext());
        }

        #endregion

        #region States

        /// <summary>
        /// 4.4.1. Data state
        /// </summary>
        private CssToken Data(Char current)
        {
            switch (current)
            {
                case Symbols.FormFeed:
                case Symbols.LineFeed:
                case Symbols.CarriageReturn:
                case Symbols.Tab:
                case Symbols.Space:
                    SkipSpaces();
                    Back();
                    return new CssToken(CssTokenType.Whitespace, " ");

                case Symbols.Num:
                    return HashStart();

                case Symbols.Dot:
                    current = GetNext();

                    if (current.IsDigit())
                    {
                        return NumberStart(GetPrevious());
                    }

                    return NewDelimiter(GetPrevious());

                case Symbols.Asterisk:
                    current = GetNext();

                    if (current == Symbols.Equality)
                    {
                        return NewMatch(CombinatorSymbols.InText);
                    }

                    return NewDelimiter(GetPrevious());

                case Symbols.Comma:
                    return new CssToken(CssTokenType.Comma, ",");

                case Symbols.GreaterThan:
                    current = GetNext();

                    if (current == Symbols.GreaterThan)
                    {
                        current = GetNext();

                        if (current == Symbols.GreaterThan)
                        {
                            return new CssToken(CssTokenType.Deep, ">>>");
                        }

                        current = GetPrevious();
                        return new CssToken(CssTokenType.Descendent, ">>");
                    }

                    return NewDelimiter(GetPrevious());

                case Symbols.Minus:
                {
                    var c1 = GetNext();

                    if (c1 != Symbols.EndOfFile)
                    {
                        var c2 = GetNext();
                        Back(2);

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
                            Advance(2);
                            return NewInvalid();
                        }
                    }
                    else
                    {
                        Back();
                    }

                    return NewDelimiter(current);
                }

                case Symbols.Plus:
                {
                    var c1 = GetNext();

                    if (c1 != Symbols.EndOfFile)
                    {
                        var c2 = GetNext();
                        Back(2);

                        if (c1.IsDigit() || (c1 == Symbols.Dot && c2.IsDigit()))
                        {
                            return NumberStart(current);
                        }
                    }
                    else
                    {
                        Back();
                    }

                    return NewDelimiter(current);
                }

                case Symbols.Colon:
                    return new CssToken(CssTokenType.Colon, ":");

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
                    current = GetNext();

                    if (current == Symbols.Plus)
                    {
                        current = GetNext();

                        if (current.IsHex() || current == Symbols.QuestionMark)
                        {
                            return UnicodeRange(current);
                        }

                        current = GetPrevious();
                    }

                    return IdentStart(GetPrevious());

                case Symbols.SquareBracketOpen:
                    return new CssToken(CssTokenType.SquareBracketOpen, "[");

                case Symbols.SquareBracketClose:
                    return new CssToken(CssTokenType.SquareBracketClose, "]");

                case Symbols.RoundBracketClose:
                    return new CssToken(CssTokenType.RoundBracketClose, ")");

                case Symbols.Solidus:
                    current = GetNext();

                    if (current == Symbols.Asterisk)
                    {
                        SkipComment();
                        return Get();
                    }

                    return NewDelimiter(GetPrevious());

                case Symbols.ReverseSolidus:
                    current = GetNext();

                    if (current.IsLineBreak() || current == Symbols.EndOfFile)
                    {
                        return NewDelimiter(GetPrevious());
                    }

                    return IdentStart(GetPrevious());

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
                                return NewInvalid();
                            }

                            current = GetPrevious();
                        }

                        current = GetPrevious();
                    }

                    return NewDelimiter(GetPrevious());

                case Symbols.Accent:
                    current = GetNext();

                    if (current == Symbols.Equality)
                    {
                        return NewMatch(CombinatorSymbols.Begins);
                    }

                    return NewDelimiter(GetPrevious());

                case Symbols.EndOfFile:
                    return new CssToken(CssTokenType.EndOfFile, String.Empty);

                case Symbols.Pipe:
                    current = GetNext();

                    if (current == Symbols.Equality)
                    {
                        return NewMatch(CombinatorSymbols.InToken);
                    }
                    else if (current == Symbols.Pipe)
                    {
                        return new CssToken(CssTokenType.Column, CombinatorSymbols.Column);
                    }

                    return NewDelimiter(GetPrevious());

                case Symbols.Dollar:
                    current = GetNext();

                    if (current == Symbols.Equality)
                    {
                        return NewMatch(CombinatorSymbols.Ends);
                    }

                    return NewDelimiter(GetPrevious());

                case Symbols.Tilde:
                    current = GetNext();

                    if (current == Symbols.Equality)
                    {
                        return NewMatch(CombinatorSymbols.InList);
                    }

                    return NewDelimiter(GetPrevious());

                case Symbols.ExclamationMark:
                    current = GetNext();

                    if (current == Symbols.Equality)
                    {
                        return NewMatch(CombinatorSymbols.Unlike);
                    }

                    return NewDelimiter(GetPrevious());

                case Symbols.At:
                    return AtKeywordStart();

                case Symbols.RoundBracketOpen:
                case Symbols.CurlyBracketOpen:
                case Symbols.CurlyBracketClose:
                case Symbols.Semicolon:
                    return NewInvalid();

                default:
                    if (current.IsNameStart())
                    {
                        return IdentStart(current);
                    }

                    return NewDelimiter(current);
            }
        }

        /// <summary>
        /// 4.4.2. Double quoted string state
        /// </summary>
        private CssToken StringDQ()
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
                        Back();
                        return NewString(FlushBuffer());

                    case Symbols.ReverseSolidus:
                        current = GetNext();

                        if (current.IsLineBreak())
                        {
                            StringBuffer.AppendLine();
                        }
                        else if (current != Symbols.EndOfFile)
                        {
                            StringBuffer.Append(ConsumeEscape(current));
                        }
                        else
                        {
                            Back();
                            return NewString(FlushBuffer());
                        }

                        break;

                    default:
                        StringBuffer.Append(current);
                        break;
                }
            }
        }

        /// <summary>
        /// 4.4.3. Single quoted string state
        /// </summary>
        private CssToken StringSQ()
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
                        Back();
                        return NewString(FlushBuffer());

                    case Symbols.ReverseSolidus:
                        current = GetNext();

                        if (current.IsLineBreak())
                        {
                            StringBuffer.AppendLine();
                        }
                        else if (current != Symbols.EndOfFile)
                        {
                            StringBuffer.Append(ConsumeEscape(current));
                        }
                        else
                        {
                            Back();
                            return NewString(FlushBuffer());
                        }

                        break;

                    default:
                        StringBuffer.Append(current);
                        break;
                }
            }
        }

        /// <summary>
        /// 4.4.4. Hash state
        /// </summary>
        private CssToken HashStart()
        {
            var current = GetNext();

            if (current.IsNameStart())
            {
                StringBuffer.Append(current);
                return HashRest();
            }
            else if (IsValidEscape(current))
            {
                current = GetNext();
                StringBuffer.Append(ConsumeEscape(current));
                return HashRest();
            }
            else if (current == Symbols.ReverseSolidus)
            {
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
        private CssToken HashRest()
        {
            while (true)
            {
                var current = GetNext();

                if (current.IsName())
                {
                    StringBuffer.Append(current);
                }
                else if (IsValidEscape(current))
                {
                    current = GetNext();
                    StringBuffer.Append(ConsumeEscape(current));
                }
                else if (current == Symbols.ReverseSolidus)
                {
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
        private void SkipComment()
        {
            var current = GetNext();

            while (current != Symbols.EndOfFile)
            {
                if (current == Symbols.Asterisk)
                {
                    current = GetNext();

                    if (current == Symbols.Solidus)
                    {
                        return;
                    }
                }
                else
                {
                    current = GetNext();
                }
            }
        }

        /// <summary>
        /// 4.4.7. At-keyword state
        /// </summary>
        private CssToken AtKeywordStart()
        {
            var current = GetNext();

            if (current == Symbols.Minus)
            {
                current = GetNext();

                if (current.IsNameStart() || IsValidEscape(current))
                {
                    StringBuffer.Append(Symbols.Minus);
                    return AtKeywordRest(current);
                }

                Back(2);
                return NewDelimiter(Symbols.At);
            }
            else if (current.IsNameStart())
            {
                StringBuffer.Append(current);
                return AtKeywordRest(GetNext());
            }
            else if (IsValidEscape(current))
            {
                current = GetNext();
                StringBuffer.Append(ConsumeEscape(current));
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
        private CssToken AtKeywordRest(Char current)
        {
            while (true)
            {
                if (current.IsName())
                {
                }
                else if (IsValidEscape(current))
                {
                    current = GetNext();
                    ConsumeEscape(current);
                }
                else
                {
                    Back();
                    return NewInvalid();
                }

                current = GetNext();
            }
        }

        /// <summary>
        /// 4.4.9. Ident state
        /// </summary>
        private CssToken IdentStart(Char current)
        {
            if (current == Symbols.Minus)
            {
                current = GetNext();

                if (current.IsNameStart() || IsValidEscape(current))
                {
                    StringBuffer.Append(Symbols.Minus);
                    return IdentRest(current);
                }

                Back();
                return NewDelimiter(Symbols.Minus);
            }
            else if (current.IsNameStart())
            {
                StringBuffer.Append(current);
                return IdentRest(GetNext());
            }
            else if (current == Symbols.ReverseSolidus && IsValidEscape(current))
            {
                current = GetNext();
                StringBuffer.Append(ConsumeEscape(current));
                return IdentRest(GetNext());
            }

            return Data(current);
        }

        /// <summary>
        /// 4.4.10. Ident-rest state
        /// </summary>
        private CssToken IdentRest(Char current)
        {
            while (true)
            {
                if (current.IsName())
                {
                    StringBuffer.Append(current);
                }
                else if (IsValidEscape(current))
                {
                    current = GetNext();
                    StringBuffer.Append(ConsumeEscape(current));
                }
                else if (current == Symbols.RoundBracketOpen)
                {
                    var name = FlushBuffer();

                    if (name.Isi(Keywords.Url))
                    {
                        return UrlStart();
                    }
                    
                    return NewFunction(name);
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
        private CssToken TransformFunctionWhitespace(Char current)
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
        private CssToken NumberStart(Char current)
        {
            while (true)
            {
                if (current.IsOneOf(Symbols.Plus, Symbols.Minus))
                {
                    StringBuffer.Append(current);
                    current = GetNext();

                    if (current == Symbols.Dot)
                    {
                        StringBuffer.Append(current);
                        StringBuffer.Append(GetNext());
                        return NumberFraction();
                    }

                    StringBuffer.Append(current);
                    return NumberRest();
                }
                else if (current == Symbols.Dot)
                {
                    StringBuffer.Append(current);
                    StringBuffer.Append(GetNext());
                    return NumberFraction();
                }
                else if (current.IsDigit())
                {
                    StringBuffer.Append(current);
                    return NumberRest();
                }

                current = GetNext();
            }
        }

        /// <summary>
        /// 4.4.13. Number-rest state
        /// </summary>
        private CssToken NumberRest()
        {
            var current = GetNext();

            while (true)
            {

                if (current.IsDigit())
                {
                    StringBuffer.Append(current);
                }
                else if (current.IsNameStart())
                {
                    var number = FlushBuffer();
                    StringBuffer.Append(current);
                    return Dimension(number);
                }
                else if (IsValidEscape(current))
                {
                    current = GetNext();
                    var number = FlushBuffer();
                    StringBuffer.Append(ConsumeEscape(current));
                    return Dimension(number);
                }
                else
                {
                    break;
                }

                current = GetNext();
            }

            switch (current)
            {
                case Symbols.Dot:
                    current = GetNext();

                    if (current.IsDigit())
                    {
                        StringBuffer.Append(Symbols.Dot).Append(current);
                        return NumberFraction();
                    }

                    Back();
                    return NewNumber(FlushBuffer());

                case '%':
                    return NewDimension(FlushBuffer(), "%");

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
        private CssToken NumberFraction()
        {
            var current = GetNext();

            while (true)
            {
                if (current.IsDigit())
                {
                    StringBuffer.Append(current);
                }
                else if (current.IsNameStart())
                {
                    var number = FlushBuffer();
                    StringBuffer.Append(current);
                    return Dimension(number);
                }
                else if (IsValidEscape(current))
                {
                    current = GetNext();
                    var number = FlushBuffer();
                    StringBuffer.Append(ConsumeEscape(current));
                    return Dimension(number);
                }
                else
                {
                    break;
                }

                current = GetNext();
            }

            switch (current)
            {
                case 'e':
                case 'E':
                    return NumberExponential(current);

                case '%':
                    return NewDimension(FlushBuffer(), "%");

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
        private CssToken Dimension(String number)
        {
            while (true)
            {
                var current = GetNext();

                if (current.IsLetter())
                {
                    StringBuffer.Append(current);
                }
                else if (IsValidEscape(current))
                {
                    current = GetNext();
                    StringBuffer.Append(ConsumeEscape(current));
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
        private CssToken SciNotation()
        {
            while (true)
            {
                var current = GetNext();

                if (current.IsDigit())
                {
                    StringBuffer.Append(current);
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
        private CssToken UrlStart()
        {
            var current = SkipSpaces();

            switch (current)
            {
                case Symbols.EndOfFile:
                    return NewInvalid();

                case Symbols.DoubleQuote:
                    return UrlDQ();

                case Symbols.SingleQuote:
                    return UrlSQ();

                case Symbols.RoundBracketClose:
                    return NewInvalid();

                default:
                    return UrlUQ(current);
            }
        }

        /// <summary>
        /// 4.4.18. URL-double-quoted state
        /// </summary>
        private CssToken UrlDQ()
        {
            while (true)
            {
                var current = GetNext();

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
                    current = GetNext();

                    if (current == Symbols.EndOfFile)
                    {
                        Back(2);
                        return NewInvalid();
                    }
                    else if (!current.IsLineBreak())
                    {
                        ConsumeEscape(current);
                    }
                }
            }
        }

        /// <summary>
        /// 4.4.19. URL-single-quoted state
        /// </summary>
        private CssToken UrlSQ()
        {
            while (true)
            {
                var current = GetNext();

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
                    current = GetNext();

                    if (current == Symbols.EndOfFile)
                    {
                        Back(2);
                        return NewInvalid();
                    }
                    else if (!current.IsLineBreak())
                    {
                        ConsumeEscape(current);
                    }
                }
            }
        }

        /// <summary>
        /// 4.4.21. URL-unquoted state
        /// </summary>
        private CssToken UrlUQ(Char current)
        {
            while (true)
            {
                if (current.IsSpaceCharacter())
                {
                    return UrlEnd();
                }
                else if (current.IsOneOf(Symbols.RoundBracketClose, Symbols.EndOfFile))
                {
                    return NewInvalid();
                }
                else if (current.IsOneOf(Symbols.DoubleQuote, Symbols.SingleQuote, Symbols.RoundBracketOpen) || current.IsNonPrintable())
                {
                    return UrlBad();
                }
                else if (current == Symbols.ReverseSolidus && IsValidEscape(current))
                {
                    current = GetNext();
                    ConsumeEscape(current);
                }
                else
                {
                    return UrlBad();
                }

                current = GetNext();
            }
        }

        /// <summary>
        /// 4.4.20. URL-end state
        /// </summary>
        private CssToken UrlEnd()
        {
            while (true)
            {
                var current = GetNext();

                if (current == Symbols.RoundBracketClose)
                {
                    return NewInvalid();
                }
                else if (!current.IsSpaceCharacter())
                {
                    Back();
                    return UrlBad();
                }
            }
        }

        /// <summary>
        /// 4.4.22. Bad URL state
        /// </summary>
        private CssToken UrlBad()
        {
            var current = Current;
            var curly = 0;
            var round = 1;

            while (current != Symbols.EndOfFile)
            {
                if (current == Symbols.Semicolon)
                {
                    Back();
                    return NewInvalid();
                }
                else if (current == Symbols.CurlyBracketClose && --curly == -1)
                {
                    Back();
                    return NewInvalid();
                }
                else if (current == Symbols.RoundBracketClose && --round == 0)
                {
                    return NewInvalid();
                }
                else if (IsValidEscape(current))
                {
                    current = GetNext();
                    ConsumeEscape(current);
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

                current = GetNext();
            }

            return NewInvalid();
        }

        /// <summary>
        /// 4.4.23. Unicode-range State
        /// </summary>
        private CssToken UnicodeRange(Char current)
        {
            var count = 0;

            for (var i = 0; i < 6 && current.IsHex(); i++)
            {
                ++count;
                current = GetNext();
            }

            if (count != 6)
            {
                for (var i = 0; i < 6 - count; i++)
                {
                    if (current != Symbols.QuestionMark)
                    {
                        current = GetPrevious();
                        break;
                    }
                    
                    current = GetNext();
                }

                return NewInvalid();
            }
            else if (current == Symbols.Minus)
            {
                current = GetNext();

                if (current.IsHex())
                {
                    var start = FlushBuffer();

                    for (var i = 0; i < 6; i++)
                    {
                        if (!current.IsHex())
                        {
                            current = GetPrevious();
                            break;
                        }
                        
                        current = GetNext();
                    }

                    var end = FlushBuffer();
                    return new CssToken(CssTokenType.Invalid, start + end);
                }
                else
                {
                    Back(2);
                    return NewInvalid();
                }
            }
            else
            {
                Back();
                return NewInvalid();
            }
        }

        #endregion

        #region Tokens

        private CssToken NewMatch(String match)
        {
            return new CssToken(CssTokenType.Match, match);
        }

        private CssToken NewInvalid()
        {
            return new CssToken(CssTokenType.Invalid, String.Empty);
        }

        private CssToken NewString(String value)
        {
            return new CssToken(CssTokenType.String, value);
        }

        private CssToken NewHash(String value)
        {
            return new CssToken(CssTokenType.Hash, value);
        }

        private CssToken NewIdent(String value)
        {
            return new CssToken(CssTokenType.Ident, value);
        }

        private CssToken NewFunction(String value)
        {
            var function = new CssFunctionToken(value);
            var token = Get();

            while (token.Type != CssTokenType.EndOfFile)
            {
                function.AddArgumentToken(token);

                if (token.Type == CssTokenType.RoundBracketClose)
                    break;

                token = Get();
            }

            return function;
        }

        private CssToken NewDimension(String value, String unit)
        {
            return new CssUnitToken(CssTokenType.Dimension, value, unit);
        }

        private CssToken NewNumber(String number)
        {
            return new CssToken(CssTokenType.Number, number);
        }

        private CssToken NewDelimiter(Char c)
        {
            return new CssToken(CssTokenType.Delim, c.ToString());
        }

        #endregion

        #region Helpers

        private CssToken NumberExponential(Char letter)
        {
            var current = GetNext();

            if (current.IsDigit())
            {
                StringBuffer.Append(letter).Append(current);
                return SciNotation();
            }
            else if (current == Symbols.Plus || current == Symbols.Minus)
            {
                var op = current;
                current = GetNext();

                if (current.IsDigit())
                {
                    StringBuffer.Append(letter).Append(op).Append(current);
                    return SciNotation();
                }

                Back();
            }

            var number = FlushBuffer();
            StringBuffer.Append(letter);
            Back();
            return Dimension(number);
        }

        private CssToken NumberDash()
        {
            var current = GetNext();

            if (current.IsNameStart())
            {
                var number = FlushBuffer();
                StringBuffer.Append(Symbols.Minus).Append(current);
                return Dimension(number);
            }
            else if (IsValidEscape(current))
            {
                current = GetNext();
                var number = FlushBuffer();
                StringBuffer.Append(Symbols.Minus).Append(ConsumeEscape(current));
                return Dimension(number);
            }
            else
            {
                Back(2);
                return NewNumber(FlushBuffer());
            }
        }

        private String ConsumeEscape(Char current)
        {
            if (current.IsHex())
            {
                var isHex = true;
                var escape = new Char[6];
                var length = 0;

                while (isHex && length < escape.Length)
                {
                    escape[length++] = current;
                    current = GetNext();
                    isHex = current.IsHex();
                }

                if (!current.IsSpaceCharacter())
                {
                    Back();
                }

                var code = Int32.Parse(new String(escape, 0, length), NumberStyles.HexNumber);

                if (!code.IsInvalid())
                {
                    return code.ConvertFromUtf32();
                }

                current = Symbols.Replacement;
            }

            return current.ToString();
        }

        private Boolean IsValidEscape(Char current)
        {
            if (current == Symbols.ReverseSolidus)
            {
                current = GetNext();
                Back();

                return current != Symbols.EndOfFile && !current.IsLineBreak();
            }
                
            return false;
        }

        #endregion
    }
}
