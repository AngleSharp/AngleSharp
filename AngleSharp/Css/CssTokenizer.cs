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
    [DebuggerStepThrough]
    sealed class CssTokenizer : BaseTokenizer
    {
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
        /// Gets the underlying stream.
        /// </summary>
        public SourceManager Stream
        {
            get { return _src; }
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
                case Specification.LF:
                case Specification.CR:
                case Specification.TAB:
                case Specification.SPACE:
                    do { current = _src.Next; }
                    while (current.IsSpaceCharacter());
                    _src.Back();
                    return CssSpecialCharacter.Whitespace;

                case Specification.DQ:
                    return StringDQ(_src.Next);

                case Specification.NUM:
                    return HashStart(_src.Next);

                case Specification.DOLLAR:
                    current = _src.Next;

                    if (current == Specification.EQ)
                        return CssMatchToken.Suffix;

                    return CssToken.Delim(_src.Previous);

                case Specification.SQ:
                    return StringSQ(_src.Next);

                case Specification.RBO:
                    return CssBracketToken.OpenRound;

                case Specification.RBC:
                    return CssBracketToken.CloseRound;

                case Specification.ASTERISK:
                    current = _src.Next;

                    if (current == Specification.EQ)
                        return CssMatchToken.Substring;

                    return CssToken.Delim(_src.Previous);

                case Specification.PLUS:
                    {
                        var c1 = _src.Next;

                        if (c1 == Specification.EOF)
                        {
                            _src.Back();
                        }
                        else
                        {
                            var c2 = _src.Next;
                            _src.Back(2);

                            if (c1.IsDigit() || (c1 == Specification.DOT && c2.IsDigit()))
                                return NumberStart(current);
                        }
                        
                        return CssToken.Delim(current);
                    }

                case Specification.COMMA:
                    return CssSpecialCharacter.Comma;

                case Specification.DOT:
                    {
                        var c = _src.Next;

                        if (c.IsDigit())
                            return NumberStart(_src.Previous);
                        
                        return CssToken.Delim(_src.Previous);
                    }

                case Specification.MINUS:
                    {
                        var c1 = _src.Next;

                        if (c1 == Specification.EOF)
                        {
                            _src.Back();
                        }
                        else
                        {
                            var c2 = _src.Next;
                            _src.Back(2);

                            if (c1.IsDigit() || (c1 == Specification.DOT && c2.IsDigit()))
                                return NumberStart(current);
                            else if (c1.IsNameStart())
                                return IdentStart(current);
                            else if (c1 == Specification.RSOLIDUS && !c2.IsLineBreak() && c2 != Specification.EOF)
                                return IdentStart(current);
                            else if (c1 == Specification.MINUS && c2 == Specification.GT)
                            {
                                _src.Advance(2);
                                return CssCommentToken.Close;
                            }
                        }
                        
                        return CssToken.Delim(current);
                    }

                case Specification.SOLIDUS:
                    current = _src.Next;

                    if (current == Specification.ASTERISK)
                        return Comment(_src.Next);
                        
                    return CssToken.Delim(_src.Previous);

                case Specification.RSOLIDUS:
                    current = _src.Next;

                    if (current.IsLineBreak() || current == Specification.EOF)
                    {
                        RaiseErrorOccurred(current == Specification.EOF ? ErrorCode.EOF : ErrorCode.LineBreakUnexpected);
                        return CssToken.Delim(_src.Previous);
                    }

                    return IdentStart(_src.Previous);

                case Specification.COLON:
                    return CssSpecialCharacter.Colon;

                case Specification.SC:
                    return CssSpecialCharacter.Semicolon;

                case Specification.LT:
                    current = _src.Next;

                    if (current == Specification.EM)
                    {
                        current = _src.Next;

                        if (current == Specification.MINUS)
                        {
                            current = _src.Next;

                            if (current == Specification.MINUS)
                                return CssCommentToken.Open;

                            current = _src.Previous;
                        }

                        current = _src.Previous;
                    }

                    return CssToken.Delim(_src.Previous);

                case Specification.AT:
                    return AtKeywordStart(_src.Next);

                case Specification.SBO:
                    return CssBracketToken.OpenSquare;

                case Specification.SBC:
                    return CssBracketToken.CloseSquare;

                case Specification.ACCENT:
                    current = _src.Next;

                    if (current == Specification.EQ)
                        return CssMatchToken.Prefix;

                    return CssToken.Delim(_src.Previous);

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
                    current = _src.Next;

                    if (current == Specification.PLUS)
                    {
                        current = _src.Next;

                        if (current.IsHex() || current == Specification.QM)
                            return UnicodeRange(current);

                        current = _src.Previous;
                    }

                    return IdentStart(_src.Previous);

                case Specification.PIPE:
                    current = _src.Next;

                    if (current == Specification.EQ)
                        return CssMatchToken.Dash;
                    else if (current == Specification.PIPE)
                        return CssToken.Column;

                    return CssToken.Delim(_src.Previous);

                case Specification.TILDE:
                    current = _src.Next;

                    if (current == Specification.EQ)
                        return CssMatchToken.Include;

                    return CssToken.Delim(_src.Previous);

                case Specification.EOF:
                    return null;

                case Specification.EM:
                    current = _src.Next;

                    if (current == Specification.EQ)
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
                    case Specification.DQ:
                    case Specification.EOF:
                        return CssStringToken.Plain(FlushBuffer());

                    case Specification.FF:
                    case Specification.LF:
                        RaiseErrorOccurred(ErrorCode.LineBreakUnexpected);
                        _src.Back();
                        return CssStringToken.Plain(FlushBuffer(), true);

                    case Specification.RSOLIDUS:
                        current = _src.Next;

                        if (current.IsLineBreak())
                            _stringBuffer.AppendLine();
                        else if (current != Specification.EOF)
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
                    case Specification.SQ:
                    case Specification.EOF:
                        return CssStringToken.Plain(FlushBuffer());

                    case Specification.FF:
                    case Specification.LF:
                        RaiseErrorOccurred(ErrorCode.LineBreakUnexpected);
                        _src.Back();
                        return (CssStringToken.Plain(FlushBuffer(), true));

                    case Specification.RSOLIDUS:
                        current = _src.Next;

                        if (current.IsLineBreak())
                            _stringBuffer.AppendLine();
                        else if (current != Specification.EOF)
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
            else if (current == Specification.RSOLIDUS)
            {
                RaiseErrorOccurred(ErrorCode.InvalidCharacter);
                _src.Back();
                return CssToken.Delim(Specification.NUM);
            }
            else
            {
                _src.Back();
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
                if (current.IsName())
                    _stringBuffer.Append(current);
                else if (IsValidEscape(current))
                {
                    current = _src.Next;
                    _stringBuffer.Append(ConsumeEscape(current));
                }
                else if (current == Specification.RSOLIDUS)
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
                if (current == Specification.ASTERISK)
                {
                    current = _src.Next;

                    if (current == Specification.SOLIDUS)
                        return Data(_src.Next);
                }
                else if (current == Specification.EOF)
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
            if (current == Specification.MINUS)
            {
                current = _src.Next;

                if (current.IsNameStart() || IsValidEscape(current))
                {
                    _stringBuffer.Append(Specification.MINUS);
                    return AtKeywordRest(current);
                }

                _src.Back(2);
                return CssToken.Delim(Specification.AT);
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
            if (current == Specification.MINUS)
            {
                current = _src.Next;

                if (current.IsNameStart() || IsValidEscape(current))
                {
                    _stringBuffer.Append(Specification.MINUS);
                    return IdentRest(current);
                }

                _src.Back();
                return CssToken.Delim(Specification.MINUS);
            }
            else if (current.IsNameStart())
            {
                _stringBuffer.Append(current);
                return IdentRest(_src.Next);
            }
            else if (current == Specification.RSOLIDUS)
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
                else if (current == Specification.RBO)
                {
                    switch (_stringBuffer.ToString().ToLower())
                    {
                        case FunctionNames.URL:
                            _stringBuffer.Clear();
                            return UrlStart(_src.Next, CssTokenType.Url);

                        case FunctionNames.DOMAIN:
                            _stringBuffer.Clear();
                            return UrlStart(_src.Next, CssTokenType.Domain);

                        case FunctionNames.URL_PREFIX:
                            _stringBuffer.Clear();
                            return UrlStart(_src.Next, CssTokenType.UrlPrefix);

                        default:
                            return CssKeywordToken.Function(FlushBuffer());
                    }

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

                if (current == Specification.RBO)
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
                if (current == Specification.PLUS || current == Specification.MINUS)
                {
                    _stringBuffer.Append(current);
                    current = _src.Next;

                    if (current == Specification.DOT)
                    {
                        _stringBuffer.Append(current);
                        _stringBuffer.Append(_src.Next);
                        return NumberFraction(_src.Next);
                    }

                    _stringBuffer.Append(current);
                    return NumberRest(_src.Next);
                }
                else if (current == Specification.DOT)
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
                case Specification.DOT:
                    current = _src.Next;

                    if (current.IsDigit())
                    {
                        _stringBuffer.Append(Specification.DOT).Append(current);
                        return NumberFraction(_src.Next);
                    }

                    _src.Back();
                    return CssToken.Number(FlushBuffer());

                case '%':
                    return CssUnitToken.Percentage(FlushBuffer());

                case 'e':
                case 'E':
                    return NumberExponential(current);

                case Specification.MINUS:
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

                case Specification.MINUS:
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
                case Specification.EOF:
                    RaiseErrorOccurred(ErrorCode.EOF);
                    return CssStringToken.Url(type, String.Empty, true);

                case Specification.DQ:
                    return UrlDQ(_src.Next, type);

                case Specification.SQ:
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
                else if (Specification.EOF == current)
                {
                    return CssStringToken.Url(type, FlushBuffer());
                }
                else if (current == Specification.DQ)
                {
                    return UrlEnd(_src.Next, type);
                }
                else if (current == Specification.RSOLIDUS)
                {
                    current = _src.Next;

                    if (current == Specification.EOF)
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
                else if (Specification.EOF == current)
                {
                    return CssStringToken.Url(type, FlushBuffer());
                }
                else if (current == Specification.SQ)
                {
                    return UrlEnd(_src.Next, type);
                }
                else if (current == Specification.RSOLIDUS)
                {
                    current = _src.Next;

                    if (current == Specification.EOF)
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
                else if (current == Specification.RBC || current == Specification.EOF)
                {
                    return CssStringToken.Url(type, FlushBuffer());
                }
                else if (current == Specification.DQ || current == Specification.SQ || current == Specification.RBO || current.IsNonPrintable())
                {
                    RaiseErrorOccurred(ErrorCode.InvalidCharacter);
                    return UrlBad(_src.Next, type);
                }
                else if (current == Specification.RSOLIDUS)
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
                if (current == Specification.RBC)
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
                if (current == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    return CssStringToken.Url(type, FlushBuffer(), true);
                }
                else if (current == Specification.RBC)
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
                    if (current != Specification.QM)
                    {
                        current = _src.Previous;
                        break;
                    }

                    _stringBuffer.Append(current);
                    current = _src.Next;
                }

                var range = FlushBuffer();
                var start = range.Replace(Specification.QM, '0');
                var end = range.Replace(Specification.QM, 'F');
                return CssToken.Range(start, end);
            }
            else if (current == Specification.MINUS)
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
            else if (current == Specification.PLUS || current == Specification.MINUS)
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
                _stringBuffer.Append(Specification.MINUS).Append(current);
                return Dimension(_src.Next, number);
            }
            else if (IsValidEscape(current))
            {
                current = _src.Next;
                var number = FlushBuffer();
                _stringBuffer.Append(Specification.MINUS).Append(ConsumeEscape(current));
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
            if (current != Specification.RSOLIDUS)
                return false;

            current = _src.Next;
            _src.Back();

            if (current == Specification.EOF)
                return false;
            else if (current.IsLineBreak())
                return false;

            return true;
        }

        #endregion
    }
}
