using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AngleSharp.Css
{
    /// <summary>
    /// The CSS tokenizer.
    /// See http://dev.w3.org/csswg/css-syntax/#tokenization for more details.
    /// </summary>
    class CssTokenizer
    {
        #region Members

        Action state;
        Single number;
        StringBuilder stringBuffer;
        Queue<CssToken> tokenBuffer;
        Boolean buffered;
        SourceManager src;
        Char current;

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
            tokenBuffer = new Queue<CssToken>();
            stringBuffer = new StringBuilder();
            src = source;
            state = Data;
            current = src.Current;
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
                while (state != null)
                {
                    state();
                    ReadNext();

                    while (buffered)
                        yield return DequeueToken();
                }
            }
        }

        #endregion

        #region States

        /// <summary>
        /// 4.4.1. Data state
        /// </summary>
        void Data()
        {
            switch (current)
            {
                case Specification.LF:
                case Specification.CR:
                case Specification.TAB:
                case Specification.SPACE:
                    do { ReadNext(); }
                    while (Specification.IsSpaceCharacter(current));
                    ReadPrevious();
                    EnqueueToken(CssSpecialCharacter.Whitespace);
                    break;

                case Specification.DQ:
                    state = StringDQ;
                    break;

                case Specification.NUM:
                    state = HashStart;
                    break;

                case Specification.DOLLAR:
                    ReadNext();

                    if (current == Specification.EQ)
                        EnqueueToken(CssMatchToken.Suffix);
                    else
                    {
                        ReadPrevious();
                        EnqueueToken(CssToken.Delim(current));
                    }

                    break;

                case Specification.SQ:
                    state = StringSQ;
                    break;

                case '(':
                    EnqueueToken(CssBracketToken.OpenRound);
                    break;

                case ')':
                    EnqueueToken(CssBracketToken.CloseRound);
                    break;

                case Specification.ASTERISK:
                    ReadNext();

                    if (current == Specification.EQ)
                        EnqueueToken(CssMatchToken.Substring);
                    else
                    {
                        ReadPrevious();
                        EnqueueToken(CssToken.Delim(current));
                    }

                    break;

                case Specification.PLUS:
                    {
                        ReadNext();
                        var c1 = current;
                        ReadNext();
                        var c2 = current;
                        ReadPrevious();
                        ReadPrevious();

                        if (Specification.IsDigit(c1) || (c1 == Specification.FS && Specification.IsDigit(c2)))
                            InstantSwitch(NumberStart);
                        else
                            EnqueueToken(CssToken.Delim(current));
                    }

                    break;

                case Specification.COMMA:
                    EnqueueToken(CssSpecialCharacter.Comma);
                    break;

                case Specification.FS:
                    {
                        ReadNext();
                        var c = current;
                        ReadPrevious();

                        if (Specification.IsDigit(c))
                            InstantSwitch(NumberStart);
                        else
                            EnqueueToken(CssToken.Delim(current));
                    }

                    break;

                case Specification.DASH:
                    {
                        ReadNext();
                        var c1 = current;
                        ReadNext();
                        var c2 = current;
                        ReadPrevious();
                        ReadPrevious();

                        if (Specification.IsDigit(c1) || (c1 == Specification.FS && Specification.IsDigit(c2)))
                            InstantSwitch(NumberStart);
                        else if (Specification.IsNameStart(c1))
                            InstantSwitch(IdentStart);
                        else if (c1 == Specification.RSOLIDUS && !Specification.IsLineBreak(c2) && c2 != Specification.EOF)
                            InstantSwitch(IdentStart);
                        else if (c1 == Specification.DASH && c2 == Specification.GT)
                        {
                            ReadNext();
                            ReadNext();
                            EnqueueToken(CssCommentToken.Close);
                        }
                        else
                            EnqueueToken(CssToken.Delim(current));
                    }

                    break;

                case Specification.SOLIDUS:
                    ReadNext();

                    if (current == Specification.ASTERISK)
                        state = Comment;
                    else
                    {
                        ReadPrevious();
                        EnqueueToken(CssToken.Delim(current));
                    }

                    break;

                case Specification.RSOLIDUS:
                    ReadNext();

                    if (Specification.IsLineBreak(current) || current == Specification.EOF)
                    {
                        RaiseErrorOccurred(current == Specification.EOF ? ErrorCode.EOF : ErrorCode.LineBreakUnexpected);
                        ReadPrevious();
                        EnqueueToken(CssToken.Delim(current));
                    }
                    else
                    {
                        ReadPrevious();
                        InstantSwitch(IdentStart);
                    }

                    break;

                case Specification.COL:
                    EnqueueToken(CssSpecialCharacter.Colon);
                    break;

                case Specification.SC:
                    EnqueueToken(CssSpecialCharacter.Semicolon);
                    break;

                case Specification.LT:
                    ReadNext();

                    if (current == Specification.EM)
                    {
                        ReadNext();

                        if (current == Specification.DASH)
                        {
                            ReadNext();

                            if (current == Specification.DASH)
                            {
                                EnqueueToken(CssCommentToken.Open);
                                break;
                            }

                            ReadPrevious();
                        }

                        ReadPrevious();
                    }

                    ReadPrevious();
                    EnqueueToken(CssToken.Delim(current));
                    break;

                case Specification.AT:
                    state = AtKeywordStart;
                    break;

                case '[':
                    EnqueueToken(CssBracketToken.OpenSquare);
                    break;

                case ']':
                    EnqueueToken(CssBracketToken.CloseSquare);
                    break;

                case Specification.CA:
                    ReadNext();

                    if (current == Specification.EQ)
                    {
                        EnqueueToken(CssMatchToken.Prefix);
                    }
                    else
                    {
                        ReadPrevious();
                        EnqueueToken(CssToken.Delim(current));
                    }

                    break;

                case '{':
                    EnqueueToken(CssBracketToken.OpenCurly);
                    break;

                case '}':
                    EnqueueToken(CssBracketToken.CloseCurly);
                    break;

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
                    InstantSwitch(NumberStart);
                    break;

                case 'U':
                case 'u':
                    ReadNext();

                    if (current == Specification.PLUS)
                    {
                        ReadNext();
                        var c = current;
                        ReadPrevious();

                        if (Specification.IsHex(c) || c == Specification.QM)
                        {
                            state = UnicodeRange;
                            break;
                        }
                    }

                    ReadPrevious();
                    InstantSwitch(IdentStart);

                    break;

                case Specification.PIPE:
                    ReadNext();

                    if (current == Specification.EQ)
                        EnqueueToken(CssMatchToken.Dash);
                    else if (current == Specification.PIPE)
                        EnqueueToken(CssToken.Column);
                    else
                    {
                        ReadPrevious();
                        EnqueueToken(CssToken.Delim(current));
                    }

                    break;

                case Specification.TILDE:
                    ReadNext();

                    if (current == Specification.EQ)
                        EnqueueToken(CssMatchToken.Include);
                    else
                    {
                        ReadPrevious();
                        EnqueueToken(CssToken.Delim(current));
                    }

                    break;

                case Specification.EOF:
                    state = null;
                    break;

                case Specification.EM:
                    ReadNext();

                    if (current == Specification.EQ)
                        EnqueueToken(CssMatchToken.Not);
                    else
                    {
                        ReadPrevious();
                        EnqueueToken(CssToken.Delim(current));
                    }

                    break;

                default:
                    if (Specification.IsNameStart(current))
                        InstantSwitch(IdentStart);
                    else
                        EnqueueToken(CssToken.Delim(current));

                    break;
            }
        }

        /// <summary>
        /// 4.4.2. Double quoted string state
        /// </summary>
        void StringDQ()
        {
            switch (current)
            {
                case Specification.DQ:
                case Specification.EOF:
                    EnqueueToken(CssStringToken.Plain(stringBuffer.ToString()));
                    stringBuffer.Clear();
                    state = Data;
                    break;

                case Specification.FF:
                case Specification.LF:
                    RaiseErrorOccurred(ErrorCode.LineBreakUnexpected);
                    EnqueueToken(CssStringToken.Plain(stringBuffer.ToString(), true));
                    stringBuffer.Clear();
                    InstantSwitch(Data);
                    break;

                case Specification.RSOLIDUS:
                    ReadNext();

                    if (Specification.IsLineBreak(current))
                        stringBuffer.AppendLine();
                    else if (current != Specification.EOF)
                        stringBuffer.Append(ConsumeEscape());
                    else
                    {
                        RaiseErrorOccurred(ErrorCode.EOF);
                        EnqueueToken(CssStringToken.Plain(stringBuffer.ToString(), true));
                        stringBuffer.Clear();
                        InstantSwitch(Data);
                    }

                    break;

                default:
                    stringBuffer.Append(current);
                    break;
            }
        }

        /// <summary>
        /// 4.4.3. Single quoted string state
        /// </summary>
        void StringSQ()
        {
            switch (current)
            {
                case Specification.SQ:
                case Specification.EOF:
                    EnqueueToken(CssStringToken.Plain(stringBuffer.ToString()));
                    stringBuffer.Clear();
                    state = Data;
                    break;

                case Specification.FF:
                case Specification.LF:
                    RaiseErrorOccurred(ErrorCode.LineBreakUnexpected);
                    EnqueueToken(CssStringToken.Plain(stringBuffer.ToString(), true));
                    stringBuffer.Clear();
                    InstantSwitch(Data);
                    break;

                case Specification.RSOLIDUS:
                    ReadNext();

                    if (Specification.IsLineBreak(current))
                        stringBuffer.AppendLine();
                    else if (current != Specification.EOF)
                        stringBuffer.Append(ConsumeEscape());
                    else
                    {
                        RaiseErrorOccurred(ErrorCode.EOF);
                        EnqueueToken(CssStringToken.Plain(stringBuffer.ToString(), true));
                        stringBuffer.Clear();
                        InstantSwitch(Data);
                    }

                    break;

                default:
                    stringBuffer.Append(current);
                    break;
            }
        }

        /// <summary>
        /// 4.4.4. Hash state
        /// </summary>
        void HashStart()
        {
            if (Specification.IsNameStart(current))
            {
                stringBuffer.Append(current);
                state = HashRest;
            }
            else if (IsValidEscape())
            {
                ReadNext();
                stringBuffer.Append(ConsumeEscape());
                state = HashRest;
            }
            else if (current == Specification.RSOLIDUS)
            {
                RaiseErrorOccurred(ErrorCode.InvalidCharacter);
                EnqueueToken(CssToken.Delim(Specification.NUM));
                InstantSwitch(Data);
            }
            else
            {
                EnqueueToken(CssToken.Delim(Specification.NUM));
                InstantSwitch(Data);
            }
        }

        /// <summary>
        /// 4.4.5. Hash-rest state
        /// </summary>
        void HashRest()
        {
            if (Specification.IsName(current))
                stringBuffer.Append(current);
            else if (IsValidEscape())
            {
                ReadNext();
                stringBuffer.Append(ConsumeEscape());
            }
            else if (current == Specification.RSOLIDUS)
            {
                RaiseErrorOccurred(ErrorCode.InvalidCharacter);
                EnqueueToken(CssKeywordToken.Hash(stringBuffer.ToString()));
                stringBuffer.Clear();
                InstantSwitch(Data);
            }
            else
            {
                EnqueueToken(CssKeywordToken.Hash(stringBuffer.ToString()));
                stringBuffer.Clear();
                InstantSwitch(Data);
            }
        }

        /// <summary>
        /// 4.4.6. Comment state
        /// </summary>
        void Comment()
        {
            if (current == Specification.ASTERISK)
            {
                ReadNext();

                if (current == Specification.SOLIDUS)
                    state = Data;
                else
                    Comment();
            }
            else if (current == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                InstantSwitch(Data);
            }
        }

        /// <summary>
        /// 4.4.7. At-keyword state
        /// </summary>
        void AtKeywordStart()
        {
            if (current == Specification.DASH)
            {
                ReadNext();

                if (Specification.IsNameStart(current) || IsValidEscape())
                {
                    stringBuffer.Append(Specification.DASH);
                    InstantSwitch(AtKeywordRest);
                }
                else
                {
                    ReadPrevious();
                    EnqueueToken(CssToken.Delim(Specification.AT));
                    InstantSwitch(Data);
                }
            }
            else if (Specification.IsNameStart(current))
            {
                stringBuffer.Append(current);
                state = AtKeywordRest;
            }
            else if (IsValidEscape())
            {
                ReadNext();
                stringBuffer.Append(ConsumeEscape());
                state = AtKeywordRest;
            }
            else
            {
                EnqueueToken(CssToken.Delim(Specification.AT));
                InstantSwitch(Data);
            }
        }

        /// <summary>
        /// 4.4.8. At-keyword-rest state
        /// </summary>
        void AtKeywordRest()
        {
            if (Specification.IsName(current))
                stringBuffer.Append(current);
            else if (IsValidEscape())
            {
                ReadNext();
                stringBuffer.Append(ConsumeEscape());
            }
            else
            {
                EnqueueToken(CssKeywordToken.At(stringBuffer.ToString()));
                stringBuffer.Clear();
                InstantSwitch(Data);
            }
        }

        /// <summary>
        /// 4.4.9. Ident state
        /// </summary>
        void IdentStart()
        {
            if (current == Specification.DASH)
            {
                ReadNext();

                if (Specification.IsNameStart(current) || IsValidEscape())
                {
                    stringBuffer.Append(Specification.DASH);
                    InstantSwitch(IdentRest);
                }
                else
                {
                    EnqueueToken(CssToken.Delim(Specification.DASH));
                    InstantSwitch(Data);
                }
            }
            else if (Specification.IsNameStart(current))
            {
                stringBuffer.Append(current);
                state = IdentRest;
            }
            else if (current == Specification.RSOLIDUS)
            {
                if (IsValidEscape())
                {
                    ReadNext();
                    stringBuffer.Append(ConsumeEscape());
                    state = IdentRest;
                }
                else
                    InstantSwitch(Data);
            }
        }

        /// <summary>
        /// 4.4.10. Ident-rest state
        /// </summary>
        void IdentRest()
        {
            if (Specification.IsName(current))
                stringBuffer.Append(current);
            else if (IsValidEscape())
            {
                ReadNext();
                stringBuffer.Append(ConsumeEscape());
            }
            else if (current == '(')
            {
                if (stringBuffer.ToString().Equals("url", StringComparison.OrdinalIgnoreCase))
                {
                    stringBuffer.Clear();
                    state = UrlStart;
                }
                else
                {
                    EnqueueToken(CssKeywordToken.Function(stringBuffer.ToString()));
                    stringBuffer.Clear();
                    state = Data;
                }
            }
            //false could be replaced with a transform whitespace flag, which is set to true if in SVG transform mode.
            //else if (false && Specification.IsSpaceCharacter(current))
            //    InstantSwitch(TransformFunctionWhitespace);
            else
            {
                EnqueueToken(CssKeywordToken.Ident(stringBuffer.ToString()));
                stringBuffer.Clear();
                InstantSwitch(Data);
            }
        }

        /// <summary>
        /// 4.4.11. Transform-function-whitespace state
        /// </summary>
        void TransformFunctionWhitespace()
        {
            ReadNext();

            if (current == '(')
            {
                EnqueueToken(CssKeywordToken.Function(stringBuffer.ToString()));
                stringBuffer.Clear();
                InstantSwitch(Data);
            }
            else if (!Specification.IsSpaceCharacter(current))
            {
                ReadPrevious();
                EnqueueToken(CssKeywordToken.Ident(stringBuffer.ToString()));
                stringBuffer.Clear();
                InstantSwitch(Data);
            }
            else
                ReadPrevious();
        }

        /// <summary>
        /// 4.4.12. Number state
        /// </summary>
        void NumberStart()
        {
            if (current == Specification.PLUS || current == Specification.DASH)
            {
                stringBuffer.Append(current);

                ReadNext();

                if (current == Specification.FS)
                {
                    stringBuffer.Append(current);
                    ReadNext();
                    stringBuffer.Append(current);
                    state = NumberFraction;
                }
                else
                {
                    stringBuffer.Append(current);
                    state = NumberRest;
                }
            }
            else if (current == Specification.FS)
            {
                stringBuffer.Append(current);
                ReadNext();
                stringBuffer.Append(current);
                state = NumberFraction;
            }
            else if (Specification.IsDigit(current))
            {
                stringBuffer.Append(current);
                state = NumberRest;
            }
        }

        /// <summary>
        /// 4.4.13. Number-rest state
        /// </summary>
        void NumberRest()
        {
            if (NumberCheck())
                return;

            switch (current)
            {
                case Specification.FS:
                    ReadNext();

                    if (Specification.IsDigit(current))
                    {
                        stringBuffer.Append(Specification.FS).Append(current);
                        state = NumberFraction;
                    }
                    else
                    {
                        EnqueueToken(CssToken.Number(Interpret(stringBuffer.ToString())));
                        stringBuffer.Clear();
                        InstantSwitch(Data);
                    }

                    break;

                case '%':
                    EnqueueToken(CssUnitToken.Percentage(Interpret(stringBuffer.ToString())));
                    stringBuffer.Clear();
                    state = Data;
                    break;

                case 'e':
                case 'E':
                    NumberExponential();
                    break;

                case Specification.DASH:
                    NumberDash();
                    break;

                default:
                    EnqueueToken(CssToken.Number(Interpret(stringBuffer.ToString())));
                    stringBuffer.Clear();
                    InstantSwitch(Data);
                    break;
            }
        }

        /// <summary>
        /// 4.4.14. Number-fraction state
        /// </summary>
        void NumberFraction()
        {
            if (NumberCheck())
                return;

            switch (current)
            {
                case 'e':
                case 'E':
                    NumberExponential();
                    break;

                case '%':
                    EnqueueToken(CssUnitToken.Percentage(Interpret(stringBuffer.ToString())));
                    stringBuffer.Clear();
                    state = Data;
                    break;

                case Specification.DASH:
                    NumberDash();
                    break;

                default:
                    EnqueueToken(CssToken.Number(Interpret(stringBuffer.ToString())));
                    stringBuffer.Clear();
                    InstantSwitch(Data);
                    break;
            }
        }

        /// <summary>
        /// 4.4.15. Dimension state
        /// </summary>
        void Dimension()
        {
            if (Specification.IsName(current))
                stringBuffer.Append(current);
            else if (IsValidEscape())
            {
                ReadNext();
                stringBuffer.Append(ConsumeEscape());
            }
            else
            {
                EnqueueToken(CssUnitToken.Dimension(number, stringBuffer.ToString()));
                stringBuffer.Clear();
                InstantSwitch(Data);
            }
        }

        /// <summary>
        /// 4.4.16. SciNotation state
        /// </summary>
        void SciNotation()
        {
            if (Specification.IsDigit(current))
                stringBuffer.Append(current);
            else
            {
                EnqueueToken(CssToken.Number(Interpret(stringBuffer.ToString())));
                stringBuffer.Clear();
                InstantSwitch(Data);
            }
        }

        /// <summary>
        /// 4.4.17. URL state
        /// </summary>
        void UrlStart()
        {
            if (Specification.IsSpaceCharacter(current))
                return;

            switch (current)
            {
                case Specification.EOF:
                    RaiseErrorOccurred(ErrorCode.EOF);
                    EnqueueToken(CssStringToken.Url(String.Empty, true));
                    state = Data;
                    break;

                case Specification.DQ:
                    state = UrlDQ;
                    break;

                case Specification.SQ:
                    state = UrlSQ;
                    break;

                case ')':
                    EnqueueToken(CssStringToken.Url(String.Empty, false));
                    state = Data;
                    break;

                default:
                    InstantSwitch(UrlUQ);
                    break;
            }
        }

        /// <summary>
        /// 4.4.18. URL-double-quoted state
        /// </summary>
        void UrlDQ()
        {
            if (Specification.IsLineBreak(current))
            {
                RaiseErrorOccurred(ErrorCode.LineBreakUnexpected);
                state = UrlBad;
            }
            else if (Specification.EOF == current)
            {
                EnqueueToken(CssStringToken.Url(stringBuffer.ToString()));
                stringBuffer.Clear();
                state = Data;
            }
            else if (current == Specification.DQ)
            {
                state = UrlEnd;
            }
            else if (current == Specification.RSOLIDUS)
            {
                ReadNext();

                if (current == Specification.EOF)
                {
                    ReadPrevious();
                    RaiseErrorOccurred(ErrorCode.EOF);
                    EnqueueToken(CssStringToken.Url(stringBuffer.ToString(), true));
                    stringBuffer.Clear();
                    InstantSwitch(Data);
                }
                else if (Specification.IsLineBreak(current))
                    stringBuffer.AppendLine();
                else
                    stringBuffer.Append(ConsumeEscape());
            }
            else
                stringBuffer.Append(current);
        }

        /// <summary>
        /// 4.4.19. URL-single-quoted state
        /// </summary>
        void UrlSQ()
        {
            if (Specification.IsLineBreak(current))
            {
                RaiseErrorOccurred(ErrorCode.LineBreakUnexpected);
                state = UrlBad;
            }
            else if (Specification.EOF == current)
            {
                EnqueueToken(CssStringToken.Url(stringBuffer.ToString()));
                stringBuffer.Clear();
                state = Data;
            }
            else if (current == Specification.SQ)
            {
                state = UrlEnd;
            }
            else if (current == Specification.RSOLIDUS)
            {
                ReadNext();

                if (current == Specification.EOF)
                {
                    ReadPrevious();
                    RaiseErrorOccurred(ErrorCode.EOF);
                    EnqueueToken(CssStringToken.Url(stringBuffer.ToString(), true));
                    stringBuffer.Clear();
                    InstantSwitch(Data);
                }
                else if (Specification.IsLineBreak(current))
                    stringBuffer.AppendLine();
                else
                    stringBuffer.Append(ConsumeEscape());
            }
            else
                stringBuffer.Append(current);
        }

        /// <summary>
        /// 4.4.21. URL-unquoted state
        /// </summary>
        void UrlUQ()
        {
            if (Specification.IsSpaceCharacter(current))
            {
                state = UrlEnd;
            }
            else if (current == ')' || current == Specification.EOF)
            {
                EnqueueToken(CssStringToken.Url(stringBuffer.ToString()));
                stringBuffer.Clear();
                state = Data;
            }
            else if (current == Specification.DQ || current == Specification.SQ || current == '(' || Specification.IsNonPrintable(current))
            {
                RaiseErrorOccurred(ErrorCode.InvalidCharacter);
                state = UrlBad;
            }
            else if (current == Specification.RSOLIDUS)
            {
                if (IsValidEscape())
                {
                    ReadNext();
                    stringBuffer.Append(ConsumeEscape());
                }
                else
                {
                    RaiseErrorOccurred(ErrorCode.InvalidCharacter);
                    state = UrlBad;
                }
            }
            else
                stringBuffer.Append(current);
        }

        /// <summary>
        /// 4.4.20. URL-end state
        /// </summary>
        void UrlEnd()
        {
            if (current == ')')
            {
                EnqueueToken(CssStringToken.Url(stringBuffer.ToString()));
                stringBuffer.Clear();
                state = Data;
            }
            else if (!Specification.IsSpaceCharacter(current))
            {
                RaiseErrorOccurred(ErrorCode.InvalidCharacter);
                InstantSwitch(UrlBad);
            }
        }

        /// <summary>
        /// 4.4.22. Bad URL state
        /// </summary>
        void UrlBad()
        {
            if (current == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                EnqueueToken(CssStringToken.Url(stringBuffer.ToString(), true));
                stringBuffer.ToString();
                state = Data;
            }
            else if (current == ')')
            {
                EnqueueToken(CssStringToken.Url(stringBuffer.ToString(), true));
                stringBuffer.ToString();
                state = Data;
            }
            else if (IsValidEscape())
            {
                ReadNext();
                stringBuffer.Append(ConsumeEscape());
            }
        }

        /// <summary>
        /// 4.4.23. Unicode-range State
        /// </summary>
        void UnicodeRange()
        {
            for (int i = 0; i < 6; i++)
            {
                if (!Specification.IsHex(current))
                    break;

                stringBuffer.Append(current);
                ReadNext();
            }

            if (stringBuffer.Length != 6)
            {
                for (int i = 0; i < 6 - stringBuffer.Length; i++)
                {
                    if (current != Specification.QM)
                    {
                        ReadPrevious();
                        break;
                    }

                    stringBuffer.Append(current);
                    ReadNext();
                }

                var range = stringBuffer.ToString();
                var start = range.Replace(Specification.QM, '0');
                var end = range.Replace(Specification.QM, 'F');
                EnqueueToken(CssToken.Range(start, end));
                stringBuffer.Clear();
                state = Data;
            }
            else if (current == Specification.DASH)
            {
                ReadNext();

                if (Specification.IsHex(current))
                {
                    var start = stringBuffer.ToString();
                    stringBuffer.Clear();

                    for (int i = 0; i < 6; i++)
                    {
                        if (!Specification.IsHex(current))
                        {
                            ReadPrevious();
                            break;
                        }

                        stringBuffer.Append(current);
                        ReadNext();
                    }

                    var end = stringBuffer.ToString();
                    stringBuffer.Clear();
                    EnqueueToken(CssToken.Range(start, end));
                    state = Data;
                }
                else
                {
                    ReadPrevious();
                    EnqueueToken(CssToken.Range(stringBuffer.ToString(), null));
                    stringBuffer.Clear();
                    InstantSwitch(Data);
                }
            }
            else
            {
                EnqueueToken(CssToken.Range(stringBuffer.ToString(), null));
                stringBuffer.Clear();
                InstantSwitch(Data);
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Checks if the current position is the start of an identifier.
        /// </summary>
        /// <returns>The result of the check.</returns>
        bool StartsWithAnIdentifier()
        {
            if (current == Specification.DASH)
            {
                ReadNext();
                var check = Specification.IsNameStart(current) || IsValidEscape();
                ReadPrevious();
                return check;
            }

            return Specification.IsNameStart(current) || IsValidEscape();
        }

        /// <summary>
        /// Substate of several Number states.
        /// </summary>
        void NumberExponential()
        {
            ReadNext();

            if (Specification.IsDigit(current))
            {
                stringBuffer.Append('e').Append(current);
                state = SciNotation;
                return;
            }
            else if (current == Specification.PLUS || current == Specification.DASH)
            {
                var op = current;
                ReadNext();

                if (Specification.IsDigit(current))
                {
                    stringBuffer.Append('e').Append(op).Append(current);
                    state = SciNotation;
                    return;
                }

                ReadPrevious();
            }

            ReadPrevious();
            number = Interpret(stringBuffer.ToString());
            stringBuffer.Clear();
            stringBuffer.Append(current);
            state = Dimension;
        }

        /// <summary>
        /// Substate of several Number states.
        /// </summary>
        void NumberDash()
        {
            ReadNext();

            if (Specification.IsNameStart(current))
            {
                number = Interpret(stringBuffer.ToString());
                stringBuffer.Clear();
                stringBuffer.Append(Specification.DASH).Append(current);
                state = Dimension;
            }
            else if (IsValidEscape())
            {
                ReadNext();
                number = Interpret(stringBuffer.ToString());
                stringBuffer.Clear();
                stringBuffer.Append(Specification.DASH).Append(ConsumeEscape());
                state = Dimension;
            }
            else
            {
                ReadPrevious();
                EnqueueToken(CssToken.Number(Interpret(stringBuffer.ToString())));
                stringBuffer.Clear();
                InstantSwitch(Data);
            }
        }

        /// <summary>
        /// Substate of several Number states.
        /// </summary>
        /// <returns>True if the current character has been used, otherwise false.</returns>
        bool NumberCheck()
        {
            if (Specification.IsDigit(current))
                stringBuffer.Append(current);
            else if (Specification.IsNameStart(current))
            {
                number = Interpret(stringBuffer.ToString());
                stringBuffer.Clear();
                stringBuffer.Append(current);
                state = Dimension;
            }
            else if (IsValidEscape())
            {
                ReadNext();
                number = Interpret(stringBuffer.ToString());
                stringBuffer.Clear();
                stringBuffer.Append(ConsumeEscape());
                state = Dimension;
            }
            else
                return false;

            return true;
        }

        /// <summary>
        /// Interprets the given string as a number.
        /// </summary>
        /// <param name="number">The string to interpret.</param>
        /// <returns>The floating point precision number.</returns>
        float Interpret(string number)
        {
            return float.Parse(number, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
        }

        /// <summary>
        /// Consumes an escaped character AFTER the solidus has already been
        /// consumed.
        /// </summary>
        /// <returns>The escaped character.</returns>
        string ConsumeEscape()
        {
            if (Specification.IsHex(current))
            {
                var escape = new List<char>();

                for (int i = 0; i < 6; i++)
                {
                    escape.Add(current);
                    ReadNext();

                    if (!Specification.IsHex(current))
                        break;
                }

                ReadPrevious();
                int code = int.Parse(new string(escape.ToArray()), System.Globalization.NumberStyles.HexNumber);
                return char.ConvertFromUtf32(code);
            }

            return current.ToString();
        }

        /// <summary>
        /// Checks if the current position is the beginning of a valid escape sequence.
        /// </summary>
        /// <returns>The result of the check.</returns>
        bool IsValidEscape()
        {
            if (current != Specification.RSOLIDUS)
                return false;

            ReadNext();
            var c = current;
            ReadPrevious();

            if (c == Specification.EOF)
                return false;
            else if (Specification.IsLineBreak(c))
                return false;

            return true;
        }

        /// <summary>
        /// Switches to the specified state and reconsumes the current character.
        /// </summary>
        /// <param name="newState">The state to switch to.</param>
        [DebuggerStepThrough]
        void InstantSwitch(Action newState)
        {
            (state = newState)();
        }

        /// <summary>
        /// Reads the previous character.
        /// </summary>
        [DebuggerStepThrough]
        void ReadPrevious()
        {
            current = src.Previous;
        }

        /// <summary>
        /// Reads the next character.
        /// </summary>
        [DebuggerStepThrough]
        void ReadNext()
        {
            current = src.Next;
        }

        /// <summary>
        /// Enqueues a token to be emitted as soon as possible.
        /// </summary>
        /// <param name="token">The token to queue.</param>
        [DebuggerStepThrough]
        void EnqueueToken(CssToken token)
        {
            buffered = true;
            tokenBuffer.Enqueue(token);
        }

        /// <summary>
        /// Dequeues a token which has been saved in the buffer.
        /// </summary>
        /// <returns>The dequeued token.</returns>
        [DebuggerStepThrough]
        CssToken DequeueToken()
        {
            buffered = tokenBuffer.Count > 1;
            return tokenBuffer.Dequeue();
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
