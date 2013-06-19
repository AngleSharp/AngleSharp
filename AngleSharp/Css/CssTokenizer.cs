using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace AngleSharp.Css
{
    /// <summary>
    /// The CSS tokenizer.
    /// See http://dev.w3.org/csswg/css-syntax/#tokenization for more details.
    /// </summary>
    //[DebuggerStepThrough]
    class CssTokenizer
    {
        #region Members

        StringBuilder stringBuffer;
        SourceManager src;

        #endregion

        #region Events

        /// <summary>
        /// The event will be fired once an error has been detected.
        /// </summary>
        public event EventHandler<ParseErrorEventArgs> ErrorOccurred;

        #endregion

        #region ctor

        public CssTokenizer(SourceManager source)
        {
            stringBuffer = new StringBuilder();
            src = source;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the underlying stream.
        /// </summary>
        public SourceManager Stream
        {
            get { return src; }
        }

        /// <summary>
        /// Gets the iterator for the tokens.
        /// </summary>
        public IEnumerator<CssToken> Iterator
        {
            get { return Tokens.GetEnumerator(); }
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
                    token = Data(src.Current);

                    if (token == null)
                        yield break;

                    src.Advance();
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
                case Specification.LF:
                case Specification.CR:
                case Specification.TAB:
                case Specification.SPACE:
                    do { current = src.Next; }
                    while (Specification.IsSpaceCharacter(current));
                    src.Back();
                    return CssSpecialCharacter.Whitespace;

                case Specification.DQ:
                    return StringDQ(src.Next);

                case Specification.NUM:
                    return HashStart(src.Next);

                case Specification.DOLLAR:
                    current = src.Next;

                    if (current == Specification.EQ)
                        return CssMatchToken.Suffix;

                    return CssToken.Delim(src.Previous);

                case Specification.SQ:
                    return StringSQ(src.Next);

                case '(':
                    return CssBracketToken.OpenRound;

                case ')':
                    return CssBracketToken.CloseRound;

                case Specification.ASTERISK:
                    current = src.Next;

                    if (current == Specification.EQ)
                        return CssMatchToken.Substring;

                    return CssToken.Delim(src.Previous);

                case Specification.PLUS:
                    {
                        var c1 = src.Next;

                        if (c1 == Specification.EOF)
                        {
                            src.Back();
                        }
                        else
                        {
                            var c2 = src.Next;
                            src.Back(2);

                            if (Specification.IsDigit(c1) || (c1 == Specification.FS && Specification.IsDigit(c2)))
                                return NumberStart(current);
                        }
                        
                        return CssToken.Delim(current);
                    }

                case Specification.COMMA:
                    return CssSpecialCharacter.Comma;

                case Specification.FS:
                    {
                        var c = src.Next;

                        if (Specification.IsDigit(c))
                            return NumberStart(src.Previous);
                        
                        return CssToken.Delim(src.Previous);
                    }

                case Specification.DASH:
                    {
                        var c1 = src.Next;

                        if (c1 == Specification.EOF)
                        {
                            src.Back();
                        }
                        else
                        {
                            var c2 = src.Next;
                            src.Back(2);

                            if (Specification.IsDigit(c1) || (c1 == Specification.FS && Specification.IsDigit(c2)))
                                return NumberStart(current);
                            else if (Specification.IsNameStart(c1))
                                return IdentStart(current);
                            else if (c1 == Specification.RSOLIDUS && !Specification.IsLineBreak(c2) && c2 != Specification.EOF)
                                return IdentStart(current);
                            else if (c1 == Specification.DASH && c2 == Specification.GT)
                            {
                                src.Advance(2);
                                return CssCommentToken.Close;
                            }
                        }
                        
                        return CssToken.Delim(current);
                    }

                case Specification.SOLIDUS:
                    current = src.Next;

                    if (current == Specification.ASTERISK)
                        return Comment(src.Next);
                        
                    return CssToken.Delim(src.Previous);

                case Specification.RSOLIDUS:
                    current = src.Next;

                    if (Specification.IsLineBreak(current) || current == Specification.EOF)
                    {
                        RaiseErrorOccurred(current == Specification.EOF ? ErrorCode.EOF : ErrorCode.LineBreakUnexpected);
                        return CssToken.Delim(src.Previous);
                    }

                    return IdentStart(src.Previous);

                case Specification.COL:
                    return CssSpecialCharacter.Colon;

                case Specification.SC:
                    return CssSpecialCharacter.Semicolon;

                case Specification.LT:
                    current = src.Next;

                    if (current == Specification.EM)
                    {
                        current = src.Next;

                        if (current == Specification.DASH)
                        {
                            current = src.Next;

                            if (current == Specification.DASH)
                                return CssCommentToken.Open;

                            current = src.Previous;
                        }

                        current = src.Previous;
                    }

                    return CssToken.Delim(src.Previous);

                case Specification.AT:
                    return AtKeywordStart(src.Next);

                case '[':
                    return CssBracketToken.OpenSquare;

                case ']':
                    return CssBracketToken.CloseSquare;

                case Specification.CA:
                    current = src.Next;

                    if (current == Specification.EQ)
                        return CssMatchToken.Prefix;

                    return CssToken.Delim(src.Previous);

                case '{':
                    return CssBracketToken.OpenCurly;

                case '}':
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
                    current = src.Next;

                    if (current == Specification.PLUS)
                    {
                        current = src.Next;

                        if (Specification.IsHex(current) || current == Specification.QM)
                            return UnicodeRange(current);

                        current = src.Previous;
                    }

                    return IdentStart(src.Previous);

                case Specification.PIPE:
                    current = src.Next;

                    if (current == Specification.EQ)
                        return CssMatchToken.Dash;
                    else if (current == Specification.PIPE)
                        return CssToken.Column;

                    return CssToken.Delim(src.Previous);

                case Specification.TILDE:
                    current = src.Next;

                    if (current == Specification.EQ)
                        return CssMatchToken.Include;

                    return CssToken.Delim(src.Previous);

                case Specification.EOF:
                    return null;

                case Specification.EM:
                    current = src.Next;

                    if (current == Specification.EQ)
                        return CssMatchToken.Not;

                    return CssToken.Delim(src.Previous);

                default:
                    if (Specification.IsNameStart(current))
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
                    case Specification.DQ:
                    case Specification.EOF:
                        return CssStringToken.Plain(FlushBuffer());

                    case Specification.FF:
                    case Specification.LF:
                        RaiseErrorOccurred(ErrorCode.LineBreakUnexpected);
                        src.Back();
                        return CssStringToken.Plain(FlushBuffer(), true);

                    case Specification.RSOLIDUS:
                        current = src.Next;

                        if (Specification.IsLineBreak(current))
                            stringBuffer.AppendLine();
                        else if (current != Specification.EOF)
                            stringBuffer.Append(ConsumeEscape(current));
                        else
                        {
                            RaiseErrorOccurred(ErrorCode.EOF);
                            src.Back();
                            return CssStringToken.Plain(FlushBuffer(), true);
                        }

                        break;

                    default:
                        stringBuffer.Append(current);
                        break;
                }

                current = src.Next;
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
                    case Specification.SQ:
                    case Specification.EOF:
                        return CssStringToken.Plain(FlushBuffer());

                    case Specification.FF:
                    case Specification.LF:
                        RaiseErrorOccurred(ErrorCode.LineBreakUnexpected);
                        src.Back();
                        return (CssStringToken.Plain(FlushBuffer(), true));

                    case Specification.RSOLIDUS:
                        current = src.Next;

                        if (Specification.IsLineBreak(current))
                            stringBuffer.AppendLine();
                        else if (current != Specification.EOF)
                            stringBuffer.Append(ConsumeEscape(current));
                        else
                        {
                            RaiseErrorOccurred(ErrorCode.EOF);
                            src.Back();
                            return(CssStringToken.Plain(FlushBuffer(), true));
                        }

                        break;

                    default:
                        stringBuffer.Append(current);
                        break;
                }

                current = src.Next;
            }
        }

        /// <summary>
        /// 4.4.4. Hash state
        /// </summary>
        CssToken HashStart(Char current)
        {
            if (Specification.IsNameStart(current))
            {
                stringBuffer.Append(current);
                return HashRest(src.Next);
            }
            else if (IsValidEscape(current))
            {
                current = src.Next;
                stringBuffer.Append(ConsumeEscape(current));
                return HashRest(src.Next);
            }
            else if (current == Specification.RSOLIDUS)
            {
                RaiseErrorOccurred(ErrorCode.InvalidCharacter);
                src.Back();
                return CssToken.Delim(Specification.NUM);
            }
            else
            {
                src.Back();
                return CssToken.Delim(Specification.NUM);
            }
        }

        /// <summary>
        /// 4.4.5. Hash-rest state
        /// </summary>
        CssToken HashRest(Char current)
        {
            while (true)
            {
                if (Specification.IsName(current))
                    stringBuffer.Append(current);
                else if (IsValidEscape(current))
                {
                    current = src.Next;
                    stringBuffer.Append(ConsumeEscape(current));
                }
                else if (current == Specification.RSOLIDUS)
                {
                    RaiseErrorOccurred(ErrorCode.InvalidCharacter);
                    src.Back();
                    return CssKeywordToken.Hash(FlushBuffer());
                }
                else
                {
                    src.Back();
                    return CssKeywordToken.Hash(FlushBuffer());
                }

                current = src.Next;
            }
        }

        /// <summary>
        /// 4.4.6. Comment state
        /// </summary>
        CssToken Comment(Char current)
        {
            while (true)
            {
                if (current == Specification.ASTERISK)
                {
                    current = src.Next;

                    if (current == Specification.SOLIDUS)
                        return Data(src.Next);
                }
                else if (current == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    return Data(current);
                }

                current = src.Next;
            }
        }

        /// <summary>
        /// 4.4.7. At-keyword state
        /// </summary>
        CssToken AtKeywordStart(Char current)
        {
            if (current == Specification.DASH)
            {
                current = src.Next;

                if (Specification.IsNameStart(current) || IsValidEscape(current))
                {
                    stringBuffer.Append(Specification.DASH);
                    return AtKeywordRest(current);
                }

                src.Back(2);
                return CssToken.Delim(Specification.AT);
            }
            else if (Specification.IsNameStart(current))
            {
                stringBuffer.Append(current);
                return AtKeywordRest(src.Next);
            }
            else if (IsValidEscape(current))
            {
                current = src.Next;
                stringBuffer.Append(ConsumeEscape(current));
                return AtKeywordRest(src.Next);
            }
            else
            {
                src.Back();
                return CssToken.Delim(Specification.AT);
            }
        }

        /// <summary>
        /// 4.4.8. At-keyword-rest state
        /// </summary>
        CssToken AtKeywordRest(Char current)
        {
            while (true)
            {
                if (Specification.IsName(current))
                    stringBuffer.Append(current);
                else if (IsValidEscape(current))
                {
                    current = src.Next;
                    stringBuffer.Append(ConsumeEscape(current));
                }
                else
                {
                    src.Back();
                    return CssKeywordToken.At(FlushBuffer());
                }

                current = src.Next;
            }
        }

        /// <summary>
        /// 4.4.9. Ident state
        /// </summary>
        CssToken IdentStart(Char current)
        {
            if (current == Specification.DASH)
            {
                current = src.Next;

                if (Specification.IsNameStart(current) || IsValidEscape(current))
                {
                    stringBuffer.Append(Specification.DASH);
                    return IdentRest(current);
                }

                src.Back();
                return CssToken.Delim(Specification.DASH);
            }
            else if (Specification.IsNameStart(current))
            {
                stringBuffer.Append(current);
                return IdentRest(src.Next);
            }
            else if (current == Specification.RSOLIDUS)
            {
                if (IsValidEscape(current))
                {
                    current = src.Next;
                    stringBuffer.Append(ConsumeEscape(current));
                    return IdentRest(src.Next);
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
                if (Specification.IsName(current))
                    stringBuffer.Append(current);
                else if (IsValidEscape(current))
                {
                    current = src.Next;
                    stringBuffer.Append(ConsumeEscape(current));
                }
                else if (current == '(')
                {
                    if (stringBuffer.ToString().Equals("url", StringComparison.OrdinalIgnoreCase))
                    {
                        stringBuffer.Clear();
                        return UrlStart(src.Next);
                    }

                    return CssKeywordToken.Function(FlushBuffer());
                }
                //false could be replaced with a transform whitespace flag, which is set to true if in SVG transform mode.
                //else if (false && Specification.IsSpaceCharacter(current))
                //    InstantSwitch(TransformFunctionWhitespace);
                else
                {
                    src.Back();
                    return CssKeywordToken.Ident(FlushBuffer());
                }

                current = src.Next;
            }
        }

        /// <summary>
        /// 4.4.11. Transform-function-whitespace state
        /// </summary>
        CssToken TransformFunctionWhitespace(Char current)
        {
            while (true)
            {
                current = src.Next;

                if (current == '(')
                {
                    src.Back();
                    return CssKeywordToken.Function(FlushBuffer());
                }
                else if (!Specification.IsSpaceCharacter(current))
                {
                    src.Back(2);
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
                if (current == Specification.PLUS || current == Specification.DASH)
                {
                    stringBuffer.Append(current);
                    current = src.Next;

                    if (current == Specification.FS)
                    {
                        stringBuffer.Append(current);
                        stringBuffer.Append(src.Next);
                        return NumberFraction(src.Next);
                    }

                    stringBuffer.Append(current);
                    return NumberRest(src.Next);
                }
                else if (current == Specification.FS)
                {
                    stringBuffer.Append(current);
                    stringBuffer.Append(src.Next);
                    return NumberFraction(src.Next);
                }
                else if (Specification.IsDigit(current))
                {
                    stringBuffer.Append(current);
                    return NumberRest(src.Next);
                }

                current = src.Next;
            }
        }

        /// <summary>
        /// 4.4.13. Number-rest state
        /// </summary>
        CssToken NumberRest(Char current)
        {
            while (true)
            {
                if (Specification.IsDigit(current))
                    stringBuffer.Append(current);
                else if (Specification.IsNameStart(current))
                {
                    var number = FlushBuffer();
                    stringBuffer.Append(current);
                    return Dimension(src.Next, number);
                }
                else if (IsValidEscape(current))
                {
                    current = src.Next;
                    var number = FlushBuffer();
                    stringBuffer.Append(ConsumeEscape(current));
                    return Dimension(src.Next, number);
                }
                else
                    break;

                current = src.Next;
            }

            switch (current)
            {
                case Specification.FS:
                    current = src.Next;

                    if (Specification.IsDigit(current))
                    {
                        stringBuffer.Append(Specification.FS).Append(current);
                        return NumberFraction(src.Next);
                    }

                    src.Back();
                    return CssToken.Number(FlushBuffer());

                case '%':
                    return CssUnitToken.Percentage(FlushBuffer());

                case 'e':
                case 'E':
                    return NumberExponential(current);

                case Specification.DASH:
                    return NumberDash(current);

                default:
                    src.Back();
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
                if (Specification.IsDigit(current))
                    stringBuffer.Append(current);
                else if (Specification.IsNameStart(current))
                {
                    var number = FlushBuffer();
                    stringBuffer.Append(current);
                    return Dimension(src.Next, number);
                }
                else if (IsValidEscape(current))
                {
                    current = src.Next;
                    var number = FlushBuffer();
                    stringBuffer.Append(ConsumeEscape(current));
                    return Dimension(src.Next, number);
                }
                else
                    break;

                current = src.Next;
            }

            switch (current)
            {
                case 'e':
                case 'E':
                    return NumberExponential(current);

                case '%':
                    return CssUnitToken.Percentage(FlushBuffer());

                case Specification.DASH:
                    return NumberDash(current);

                default:
                    src.Back();
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
                if (Specification.IsName(current))
                    stringBuffer.Append(current);
                else if (IsValidEscape(current))
                {
                    current = src.Next;
                    stringBuffer.Append(ConsumeEscape(current));
                }
                else
                {
                    src.Back();
                    return CssUnitToken.Dimension(number, FlushBuffer());
                }

                current = src.Next;
            }
        }

        /// <summary>
        /// 4.4.16. SciNotation state
        /// </summary>
        CssToken SciNotation(Char current)
        {
            while (true)
            {
                if (Specification.IsDigit(current))
                    stringBuffer.Append(current);
                else
                {
                    src.Back();
                    return CssToken.Number(FlushBuffer());
                }

                current = src.Next;
            }
        }

        /// <summary>
        /// 4.4.17. URL state
        /// </summary>
        CssToken UrlStart(Char current)
        {
            while (Specification.IsSpaceCharacter(current))
                current = src.Next;

            switch (current)
            {
                case Specification.EOF:
                    RaiseErrorOccurred(ErrorCode.EOF);
                    return CssStringToken.Url(String.Empty, true);

                case Specification.DQ:
                    return UrlDQ(src.Next);

                case Specification.SQ:
                    return UrlSQ(src.Next);

                case ')':
                    return CssStringToken.Url(String.Empty, false);

                default:
                    return UrlUQ(current);
            }
        }

        /// <summary>
        /// 4.4.18. URL-double-quoted state
        /// </summary>
        CssToken UrlDQ(Char current)
        {
            while (true)
            {
                if (Specification.IsLineBreak(current))
                {
                    RaiseErrorOccurred(ErrorCode.LineBreakUnexpected);
                    return UrlBad(src.Next);
                }
                else if (Specification.EOF == current)
                {
                    return CssStringToken.Url(FlushBuffer());
                }
                else if (current == Specification.DQ)
                {
                    return UrlEnd(src.Next);
                }
                else if (current == Specification.RSOLIDUS)
                {
                    current = src.Next;

                    if (current == Specification.EOF)
                    {
                        src.Back(2);
                        RaiseErrorOccurred(ErrorCode.EOF);
                        return CssStringToken.Url(FlushBuffer(), true);
                    }
                    else if (Specification.IsLineBreak(current))
                        stringBuffer.AppendLine();
                    else
                        stringBuffer.Append(ConsumeEscape(current));
                }
                else
                    stringBuffer.Append(current);

                current = src.Next;
            }
        }

        /// <summary>
        /// 4.4.19. URL-single-quoted state
        /// </summary>
        CssToken UrlSQ(Char current)
        {
            while (true)
            {
                if (Specification.IsLineBreak(current))
                {
                    RaiseErrorOccurred(ErrorCode.LineBreakUnexpected);
                    return UrlBad(src.Next);
                }
                else if (Specification.EOF == current)
                {
                    return CssStringToken.Url(FlushBuffer());
                }
                else if (current == Specification.SQ)
                {
                    return UrlEnd(src.Next);
                }
                else if (current == Specification.RSOLIDUS)
                {
                    current = src.Next;

                    if (current == Specification.EOF)
                    {
                        src.Back(2);
                        RaiseErrorOccurred(ErrorCode.EOF);
                        return CssStringToken.Url(FlushBuffer(), true);
                    }
                    else if (Specification.IsLineBreak(current))
                        stringBuffer.AppendLine();
                    else
                        stringBuffer.Append(ConsumeEscape(current));
                }
                else
                    stringBuffer.Append(current);

                current = src.Next;
            }
        }

        /// <summary>
        /// 4.4.21. URL-unquoted state
        /// </summary>
        CssToken UrlUQ(Char current)
        {
            while (true)
            {
                if (Specification.IsSpaceCharacter(current))
                {
                    return UrlEnd(src.Next);
                }
                else if (current == ')' || current == Specification.EOF)
                {
                    return CssStringToken.Url(FlushBuffer());
                }
                else if (current == Specification.DQ || current == Specification.SQ || current == '(' || Specification.IsNonPrintable(current))
                {
                    RaiseErrorOccurred(ErrorCode.InvalidCharacter);
                    return UrlBad(src.Next);
                }
                else if (current == Specification.RSOLIDUS)
                {
                    if (IsValidEscape(current))
                    {
                        current = src.Next;
                        stringBuffer.Append(ConsumeEscape(current));
                    }
                    else
                    {
                        RaiseErrorOccurred(ErrorCode.InvalidCharacter);
                        return UrlBad(src.Next);
                    }
                }
                else
                    stringBuffer.Append(current);

                current = src.Next;
            }
        }

        /// <summary>
        /// 4.4.20. URL-end state
        /// </summary>
        CssToken UrlEnd(Char current)
        {
            while (true)
            {
                if (current == ')')
                    return CssStringToken.Url(FlushBuffer());
                else if (!Specification.IsSpaceCharacter(current))
                {
                    RaiseErrorOccurred(ErrorCode.InvalidCharacter);
                    return UrlBad(current);
                }

                current = src.Next;
            }
        }

        /// <summary>
        /// 4.4.22. Bad URL state
        /// </summary>
        CssToken UrlBad(Char current)
        {
            while (true)
            {
                if (current == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    return CssStringToken.Url(FlushBuffer(), true);
                }
                else if (current == ')')
                {
                    return CssStringToken.Url(FlushBuffer(), true);
                }
                else if (IsValidEscape(current))
                {
                    current = src.Next;
                    stringBuffer.Append(ConsumeEscape(current));
                }

                current = src.Next;
            }
        }

        /// <summary>
        /// 4.4.23. Unicode-range State
        /// </summary>
        CssToken UnicodeRange(Char current)
        {
            for (int i = 0; i < 6; i++)
            {
                if (!Specification.IsHex(current))
                    break;

                stringBuffer.Append(current);
                current = src.Next;
            }

            if (stringBuffer.Length != 6)
            {
                for (int i = 0; i < 6 - stringBuffer.Length; i++)
                {
                    if (current != Specification.QM)
                    {
                        current = src.Previous;
                        break;
                    }

                    stringBuffer.Append(current);
                    current = src.Next;
                }

                var range = FlushBuffer();
                var start = range.Replace(Specification.QM, '0');
                var end = range.Replace(Specification.QM, 'F');
                return CssToken.Range(start, end);
            }
            else if (current == Specification.DASH)
            {
                current = src.Next;

                if (Specification.IsHex(current))
                {
                    var start = stringBuffer.ToString();
                    stringBuffer.Clear();

                    for (int i = 0; i < 6; i++)
                    {
                        if (!Specification.IsHex(current))
                        {
                            current = src.Previous;
                            break;
                        }

                        stringBuffer.Append(current);
                        current = src.Next;
                    }

                    var end = FlushBuffer();
                    return CssToken.Range(start, end);
                }
                else
                {
                    src.Back(2);
                    return CssToken.Range(FlushBuffer(), null);
                }
            }
            else
            {
                src.Back();
                return CssToken.Range(FlushBuffer(), null);
            }
        }

        #endregion

        #region Helpers

        String FlushBuffer()
        {
            var tmp = stringBuffer.ToString();
            stringBuffer.Clear();
            return tmp;
        }

        /// <summary>
        /// Substate of several Number states.
        /// </summary>
        CssToken NumberExponential(Char current)
        {
            current = src.Next;

            if (Specification.IsDigit(current))
            {
                stringBuffer.Append('e').Append(current);
                return SciNotation(src.Next);
            }
            else if (current == Specification.PLUS || current == Specification.DASH)
            {
                var op = current;
                current = src.Next;

                if (Specification.IsDigit(current))
                {
                    stringBuffer.Append('e').Append(op).Append(current);
                    return SciNotation(src.Next);
                }

                src.Back();
            }

            current = src.Previous;
            var number = FlushBuffer();
            stringBuffer.Append(current);
            return Dimension(src.Next, number);
        }

        /// <summary>
        /// Substate of several Number states.
        /// </summary>
        CssToken NumberDash(Char current)
        {
            current = src.Next;

            if (Specification.IsNameStart(current))
            {
                var number = FlushBuffer();
                stringBuffer.Append(Specification.DASH).Append(current);
                return Dimension(src.Next, number);
            }
            else if (IsValidEscape(current))
            {
                current = src.Next;
                var number = FlushBuffer();
                stringBuffer.Append(Specification.DASH).Append(ConsumeEscape(current));
                return Dimension(src.Next, number);
            }
            else
            {
                src.Back(2);
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
            if (Specification.IsHex(current))
            {
                var escape = new List<Char>();

                for (int i = 0; i < 6; i++)
                {
                    escape.Add(current);
                    current = src.Next;

                    if (!Specification.IsHex(current))
                        break;
                }

                current = src.Previous;
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
            if (current != Specification.RSOLIDUS)
                return false;

            current = src.Next;
            src.Back();

            if (current == Specification.EOF)
                return false;
            else if (Specification.IsLineBreak(current))
                return false;

            return true;
        }

        #endregion

        #region Event-Helpers

        /// <summary>
        /// Fires an error occurred event.
        /// </summary>
        /// <param name="code">The associated error code.</param>
        void RaiseErrorOccurred(ErrorCode code)
        {
            if (ErrorOccurred != null)
            {
                var pck = new ParseErrorEventArgs((int)code, Errors.GetError(code));
                pck.Line = src.Line;
                pck.Column = src.Column;
                ErrorOccurred(this, pck);
            }
        }

        #endregion
    }
}
