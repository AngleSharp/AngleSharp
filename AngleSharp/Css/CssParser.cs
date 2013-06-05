using System;
using AngleSharp.DOM;
using AngleSharp.DOM.Css;
using AngleSharp.DOM.Collections;
using System.Collections.Generic;
using System.Text;

namespace AngleSharp.Css
{
    /// <summary>
    /// The CSS parser.
    /// See http://dev.w3.org/csswg/css-syntax/#parsing for more details.
    /// </summary>
    public class CssParser
    {
        #region Members

        Action _state;
        StringBuilder buffer;
        bool _quirksFlag;
        float temp;
        Queue<CssToken> tokens;
        CssSource _source;
        char current;

        #endregion

        #region Events

        /// <summary>
        /// The event will be fired once an error has been detected.
        /// </summary>
        public event EventHandler<ParseErrorEventArgs> ErrorOccurred;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS parser instance.
        /// </summary>
        /// <param name="source">The source where the parser is operating on.</param>
        public CssParser(string source)
        {
            tokens = new Queue<CssToken>();
            buffer = new StringBuilder();
            _source = new CssSource(source);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the iterator for the tokens.
        /// </summary>
        internal IEnumerator<CssToken> Iterator
        {
            get { return GetTokens().GetEnumerator(); }
        }

        /// <summary>
        /// Gets the list of tokens.
        /// </summary>
        internal List<CssToken> TokenList
        {
            get
            {
                var list = new List<CssToken>();
                var it = Iterator;

                while (it.MoveNext())
                    list.Add(it.Current);

                return list;
            }
        }

        /// <summary>
        /// Gets or sets the quirks mode flag (true = active).
        /// </summary>
        public bool IsQuirksMode
        {
            get { return _quirksFlag; }
            set { _quirksFlag = value; }
        }

        #endregion

        #region Tokenizer States

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
                    do
                    {
                        ReadNext();
                    }
                    while (Specification.IsSpaceCharacter(current));

                    ReadPrevious();
                    Emit(CssSpecialCharacter.Whitespace);
                    break;

                case Specification.DQ:
                    _state = StringDQ;
                    break;

                case Specification.NUM:
                    _state = HashStart;
                    break;

                case Specification.DOLLAR:
                    ReadNext();

                    if (current == Specification.EQ)
                    {
                        Emit(CssMatchToken.Suffix);
                    }
                    else
                    {
                        ReadPrevious();
                        Emit(new CssDelimToken(current));
                    }

                    break;

                case Specification.SQ:
                    _state = StringSQ;
                    break;

                case '(':
                    Emit(CssBracketToken.OpenRound);
                    break;

                case ')':
                    Emit(CssBracketToken.CloseRound);
                    break;

                case Specification.ASTERISK:
                    ReadNext();

                    if(current == Specification.EQ)
                    {
                        Emit(CssMatchToken.Substring);
                    }
                    else
                    {
                        ReadPrevious();
                        Emit(new CssDelimToken(current));
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
                            Emit(new CssDelimToken(current));
                    }

                    break;
                    
                case Specification.COMMA:
                    Emit(CssSpecialCharacter.Comma);
                    break;

                case Specification.FS:
                    {
                        ReadNext();
                        var c = current;
                        ReadPrevious();

                        if (Specification.IsDigit(c))
                            InstantSwitch(NumberStart);
                        else
                            Emit(new CssDelimToken(current));
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
                            Emit(CssCommentToken.Close);
                        }
                        else
                            Emit(new CssDelimToken(current));
                    }

                    break;

                case Specification.SOLIDUS:
                    ReadNext();

                    if (current == Specification.ASTERISK)
                    {
                        _state = Comment;
                    }
                    else
                    {
                        ReadPrevious();
                        Emit(new CssDelimToken(current));
                    }

                    break;

                case Specification.RSOLIDUS:
                    ReadNext();

                    if (Specification.IsLineBreak(current) || current == Specification.EOF)
                    {
                        RaiseErrorOccurred(current == Specification.EOF ? ErrorCode.EOF : ErrorCode.LineBreakUnexpected);
                        ReadPrevious();
                        Emit(new CssDelimToken(current));
                    }
                    else
                    {
                        ReadPrevious();
                        InstantSwitch(IdentStart);
                    }

                    break;

                case Specification.COL:
                    Emit(CssSpecialCharacter.Colon);
                    break;

                case Specification.SC:
                    Emit(CssSpecialCharacter.Semicolon);
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
                                Emit(CssCommentToken.Open);
                                break;
                            }

                            ReadPrevious();
                        }

                        ReadPrevious();
                    }

                    ReadPrevious();
                    Emit(new CssDelimToken(current));
                    break;

                case Specification.AT:
                    _state = AtKeywordStart;
                    break;

                case '[':
                    Emit(CssBracketToken.OpenSquare);
                    break;

                case ']':
                    Emit(CssBracketToken.CloseSquare);
                    break;

                case Specification.CA:
                    ReadNext();

                    if (current == Specification.EQ)
                    {
                        Emit(CssMatchToken.Prefix);
                    }
                    else
                    {
                        ReadPrevious();
                        Emit(new CssDelimToken(current));
                    }

                    break;

                case '{':
                    Emit(CssBracketToken.OpenCurly);
                    break;

                case '}':
                    Emit(CssBracketToken.CloseCurly);
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
                            _state = UnicodeRange;
                            break;
                        }
                    }

                    ReadPrevious();
                    InstantSwitch(IdentStart);

                    break;

                case Specification.PIPE:
                    ReadNext();

                    if (current == Specification.EQ)
                    {
                        Emit(CssMatchToken.Dash);
                    }
                    else if (current == Specification.PIPE)
                    {
                        Emit(new CssColumnToken());
                    }
                    else
                    {
                        ReadPrevious();
                        Emit(new CssDelimToken(current));
                    }

                    break;

                case Specification.TILDE:
                    ReadNext();

                    if (current == Specification.EQ)
                    {
                        Emit(CssMatchToken.Include);
                    }
                    else
                    {
                        ReadPrevious();
                        Emit(new CssDelimToken(current));
                    }

                    break;

                case Specification.EOF:
                    _state = null;
                    break;

                case Specification.EM:
                    ReadNext();

                    if (current == Specification.EQ)
                    {
                        Emit(CssMatchToken.Not);
                    }
                    else
                    {
                        ReadPrevious();
                        Emit(new CssDelimToken(current));
                    }

                    break;                    

                default:
                    if (Specification.IsNameStart(current))
                        InstantSwitch(IdentStart);
                    else
                        Emit(new CssDelimToken(current));

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
                    Emit(CssStringToken.Plain(buffer.ToString()));
                    buffer.Clear();
                    _state = Data;
                    break;

                case Specification.FF:
                case Specification.LF:
                    RaiseErrorOccurred(ErrorCode.LineBreakUnexpected);
                    Emit(CssStringToken.Plain(buffer.ToString(), true));
                    buffer.Clear();
                    InstantSwitch(Data);
                    break;

                case Specification.RSOLIDUS:
                    ReadNext();

                    if(Specification.IsLineBreak(current))
                        buffer.AppendLine();
                    else if (current != Specification.EOF)
                        buffer.Append(ConsumeEscape());
                    else
                    {
                        RaiseErrorOccurred(ErrorCode.EOF);
                        Emit(CssStringToken.Plain(buffer.ToString(), true));
                        buffer.Clear();
                        InstantSwitch(Data);
                    }

                    break;

                default:
                    buffer.Append(current);
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
                    Emit(CssStringToken.Plain(buffer.ToString()));
                    buffer.Clear();
                    _state = Data;
                    break;

                case Specification.FF:
                case Specification.LF:
                    RaiseErrorOccurred(ErrorCode.LineBreakUnexpected);
                    Emit(CssStringToken.Plain(buffer.ToString(), true));
                    buffer.Clear();
                    InstantSwitch(Data);
                    break;

                case Specification.RSOLIDUS:
                    ReadNext();

                    if (Specification.IsLineBreak(current))
                        buffer.AppendLine();
                    else if (current != Specification.EOF)
                        buffer.Append(ConsumeEscape());
                    else
                    {
                        RaiseErrorOccurred(ErrorCode.EOF);
                        Emit(CssStringToken.Plain(buffer.ToString(), true));
                        buffer.Clear();
                        InstantSwitch(Data);
                    }

                    break;

                default:
                    buffer.Append(current);
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
                buffer.Append(current);
                _state = HashRest;
            }
            else if (IsValidEscape())
            {
                ReadNext();
                buffer.Append(ConsumeEscape());
                _state = HashRest;
            }
            else if (current == Specification.RSOLIDUS)
            {
                RaiseErrorOccurred(ErrorCode.InvalidCharacter);
                Emit(new CssDelimToken(Specification.NUM));
                InstantSwitch(Data);
            }
            else
            {
                Emit(new CssDelimToken(Specification.NUM));
                InstantSwitch(Data);
            }
        }

        /// <summary>
        /// 4.4.5. Hash-rest state
        /// </summary>
        void HashRest()
        {
            if (Specification.IsName(current))
                buffer.Append(current);
            else if (IsValidEscape())
            {
                ReadNext();
                buffer.Append(ConsumeEscape());
            }
            else if (current == Specification.RSOLIDUS)
            {
                RaiseErrorOccurred(ErrorCode.InvalidCharacter);
                Emit(CssKeywordToken.Hash(buffer.ToString()));
                buffer.Clear();
                InstantSwitch(Data);
            }
            else
            {
                Emit(CssKeywordToken.Hash(buffer.ToString()));
                buffer.Clear();
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
                    _state = Data;
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
                    buffer.Append(Specification.DASH);
                    InstantSwitch(AtKeywordRest);
                }
                else
                {
                    ReadPrevious();
                    Emit(new CssDelimToken(Specification.AT));
                    InstantSwitch(Data);
                }
            }
            else if (Specification.IsNameStart(current))
            {
                buffer.Append(current);
                _state = AtKeywordRest;
            }
            else if (IsValidEscape())
            {
                ReadNext();
                buffer.Append(ConsumeEscape());
                _state = AtKeywordRest;
            }
            else
            {
                Emit(new CssDelimToken(Specification.AT));
                InstantSwitch(Data);
            }
        }

        /// <summary>
        /// 4.4.8. At-keyword-rest state
        /// </summary>
        void AtKeywordRest()
        {
            if (Specification.IsName(current))
                buffer.Append(current);
            else if (IsValidEscape())
            {
                ReadNext();
                buffer.Append(ConsumeEscape());
            }
            else
            {
                Emit(CssKeywordToken.At(buffer.ToString()));
                buffer.Clear();
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
                    buffer.Append(Specification.DASH);
                    InstantSwitch(IdentRest);
                }
                else
                {
                    Emit(new CssDelimToken(Specification.DASH));
                    InstantSwitch(Data);
                }
            }
            else if (Specification.IsNameStart(current))
            {
                buffer.Append(current);
                _state = IdentRest;
            }
            else if (current == Specification.RSOLIDUS)
            {
                if (IsValidEscape())
                {
                    ReadNext();
                    buffer.Append(ConsumeEscape());
                    _state = IdentRest;
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
                buffer.Append(current);
            else if (IsValidEscape())
            {
                ReadNext();
                buffer.Append(ConsumeEscape());
            }
            else if (current == '(')
            {
                if (buffer.ToString().Equals("url", StringComparison.InvariantCultureIgnoreCase))
                {
                    buffer.Clear();
                    _state = UrlStart;
                }
                else
                {
                    Emit(CssKeywordToken.Function(buffer.ToString()));
                    buffer.Clear();
                    _state = Data;
                }
            }
            //false could be replaced with a transform whitespace flag, which is set to true if in SVG transform mode.
            //else if (false && Specification.IsSpaceCharacter(current))
            //    InstantSwitch(TransformFunctionWhitespace);
            else
            {
                Emit(CssKeywordToken.Ident(buffer.ToString()));
                buffer.Clear();
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
                Emit(CssKeywordToken.Function(buffer.ToString()));
                buffer.Clear();
                InstantSwitch(Data);
            }
            else if (!Specification.IsSpaceCharacter(current))
            {
                ReadPrevious();
                Emit(CssKeywordToken.Ident(buffer.ToString()));
                buffer.Clear();
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
                buffer.Append(current);

                ReadNext();

                if (current == Specification.FS)
                {
                    buffer.Append(current);
                    ReadNext();
                    buffer.Append(current);
                    _state = NumberFraction;
                }
                else
                {
                    buffer.Append(current);
                    _state = NumberRest;
                }
            }
            else if (current == Specification.FS)
            {
                buffer.Append(current);
                ReadNext();
                buffer.Append(current);
                _state = NumberFraction;
            }
            else if (Specification.IsDigit(current))
            {
                buffer.Append(current);
                _state = NumberRest;
            }
        }

        /// <summary>
        /// 4.4.13. Number-rest state
        /// </summary>
        void NumberRest()
        {
            if (NumberCheck())
                return;

            switch(current)
            {
                case Specification.FS:
                    ReadNext();

                    if (Specification.IsDigit(current))
                    {
                        buffer.Append(Specification.FS).Append(current);
                        _state = NumberFraction;
                    }
                    else
                    {
                        Emit(new CssNumberToken(Interpret(buffer.ToString())));
                        buffer.Clear();
                        InstantSwitch(Data);
                    }

                    break;

                case '%':
                    Emit(CssUnitToken.Percentage(Interpret(buffer.ToString())));
                    buffer.Clear();
                    _state = Data;
                    break;

                case 'e':
                case 'E':
                    NumberExponential();
                    break;

                case Specification.DASH:
                    NumberDash();
                    break;

                default:
                    Emit(new CssNumberToken(Interpret(buffer.ToString())));
                    buffer.Clear();
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
                    Emit(CssUnitToken.Percentage(Interpret(buffer.ToString())));
                    buffer.Clear();
                    _state = Data;
                    break;

                case Specification.DASH:
                    NumberDash();
                    break;

                default:
                    Emit(new CssNumberToken(Interpret(buffer.ToString())));
                    buffer.Clear();
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
                buffer.Append(current);
            else if (IsValidEscape())
            {
                ReadNext();
                buffer.Append(ConsumeEscape());
            }
            else
            {
                Emit(CssUnitToken.Dimension(temp, buffer.ToString()));
                buffer.Clear();
                InstantSwitch(Data);
            }
        }

        /// <summary>
        /// 4.4.16. SciNotation state
        /// </summary>
        void SciNotation()
        {
            if (Specification.IsDigit(current))
                buffer.Append(current);
            else
            {
                Emit(new CssNumberToken(Interpret(buffer.ToString())));
                buffer.Clear();
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
                    Emit(CssStringToken.Url(string.Empty, true));
                    _state = Data;
                    break;

                case Specification.DQ:
                    _state = UrlDQ;
                    break;

                case Specification.SQ:
                    _state = UrlSQ;
                    break;

                case ')':
                    Emit(CssStringToken.Url(string.Empty, false));
                    _state = Data;
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
                _state = UrlBad;
            }
            else if (Specification.EOF == current)
            {
                Emit(CssStringToken.Url(buffer.ToString()));
                buffer.Clear();
                _state = Data;
            }
            else if (current == Specification.DQ)
            {
                _state = UrlEnd;
            }
            else if (current == Specification.RSOLIDUS)
            {
                ReadNext();

                if (current == Specification.EOF)
                {
                    ReadPrevious();
                    RaiseErrorOccurred(ErrorCode.EOF);
                    Emit(CssStringToken.Url(buffer.ToString(), true));
                    buffer.Clear();
                    InstantSwitch(Data);
                }
                else if (Specification.IsLineBreak(current))
                    buffer.AppendLine();
                else
                    buffer.Append(ConsumeEscape());
            }
            else
                buffer.Append(current);
        }

        /// <summary>
        /// 4.4.19. URL-single-quoted state
        /// </summary>
        void UrlSQ()
        {
            if (Specification.IsLineBreak(current))
            {
                RaiseErrorOccurred(ErrorCode.LineBreakUnexpected);
                _state = UrlBad;
            }
            else if (Specification.EOF == current)
            {
                Emit(CssStringToken.Url(buffer.ToString()));
                buffer.Clear();
                _state = Data;
            }
            else if (current == Specification.SQ)
            {
                _state = UrlEnd;
            }
            else if (current == Specification.RSOLIDUS)
            {
                ReadNext();

                if (current == Specification.EOF)
                {
                    ReadPrevious();
                    RaiseErrorOccurred(ErrorCode.EOF);
                    Emit(CssStringToken.Url(buffer.ToString(), true));
                    buffer.Clear();
                    InstantSwitch(Data);
                }
                else if (Specification.IsLineBreak(current))
                    buffer.AppendLine();
                else
                    buffer.Append(ConsumeEscape());
            }
            else
                buffer.Append(current);
        }

        /// <summary>
        /// 4.4.21. URL-unquoted state
        /// </summary>
        void UrlUQ()
        {
            if (Specification.IsSpaceCharacter(current))
            {
                _state = UrlEnd;
            }
            else if (current == ')' || current == Specification.EOF)
            {
                Emit(CssStringToken.Url(buffer.ToString()));
                buffer.Clear();
                _state = Data;
            }
            else if (current == Specification.DQ || current == Specification.SQ || current == '(' || Specification.IsNonPrintable(current))
            {
                RaiseErrorOccurred(ErrorCode.InvalidCharacter);
                _state = UrlBad;
            }
            else if (current == Specification.RSOLIDUS)
            {
                if (IsValidEscape())
                {
                    ReadNext();
                    buffer.Append(ConsumeEscape());
                }
                else
                {
                    RaiseErrorOccurred(ErrorCode.InvalidCharacter);
                    _state = UrlBad;
                }
            }
            else
                buffer.Append(current);
        }

        /// <summary>
        /// 4.4.20. URL-end state
        /// </summary>
        void UrlEnd()
        {
            if (current == ')')
            {
                Emit(CssStringToken.Url(buffer.ToString()));
                buffer.Clear();
                _state = Data;
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
                Emit(CssStringToken.Url(buffer.ToString(), true));
                buffer.ToString();
                _state = Data;
            }
            else if (current == ')')
            {
                Emit(CssStringToken.Url(buffer.ToString(), true));
                buffer.ToString();
                _state = Data;
            }
            else if (IsValidEscape())
            {
                ReadNext();
                buffer.Append(ConsumeEscape());
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
                
                buffer.Append(current);
                ReadNext();
            }

            if (buffer.Length != 6)
            {
                for (int i = 0; i < 6 - buffer.Length; i++)
                {
                    if (current != Specification.QM)
                    {
                        ReadPrevious();
                        break;
                    }

                    buffer.Append(current);
                    ReadNext();
                }

                var range = buffer.ToString();
                var start = range.Replace(Specification.QM, '0');
                var end = range.Replace(Specification.QM, 'F');
                Emit(new CssRangeToken().SetRange(start, end));
                buffer.Clear();
                _state = Data;
            }
            else if(current == Specification.DASH)
            {
                ReadNext();

                if (Specification.IsHex(current))
                {
                    var start = buffer.ToString();
                    buffer.Clear();

                    for (int i = 0; i < 6; i++)
                    {
                        if (!Specification.IsHex(current))
                        {
                            ReadPrevious();
                            break;
                        }

                        buffer.Append(current);
                        ReadNext();
                    }

                    var end = buffer.ToString();
                    buffer.Clear();
                    Emit(new CssRangeToken().SetRange(start, end));
                    _state = Data;
                }
                else
                {
                    ReadPrevious();
                    Emit(new CssRangeToken().SetRange(buffer.ToString(), null));
                    buffer.Clear();
                    InstantSwitch(Data);
                }
            }
            else
            {
                Emit(new CssRangeToken().SetRange(buffer.ToString(), null));
                buffer.Clear();
                InstantSwitch(Data);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Parses the given source code.
        /// </summary>
        /// <returns>A stylesheet based on the given CSS source.</returns>
        public CSSStyleSheet Parse()
        {
            var sheet = new CSSStyleSheet();
            var rules = ConsumeRules(Iterator);

            for (int i = 0; i < rules.Count; i++)
                sheet.CssRules.InsertAt(i, CssRuleConstructor.Create(this, rules[i]));

            return sheet;
        }

        #endregion

        #region Consumers

        /// <summary>
        /// Tries to consume a list of rules from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The list of rules.</returns>
        internal List<CssRule> ConsumeRules(IEnumerator<CssToken> source)
        {
            var rules = new List<CssRule>();

            while (source.MoveNext())
            {
                switch (source.Current.Type)
                {
                    case CssTokenType.Cdc:
                    case CssTokenType.Cdo:
                    case CssTokenType.Whitespace:
                        break;

                    case CssTokenType.AtKeyword:
                        rules.Add(ConsumeAtRule(source));
                        break;

                    default:
                        rules.Add(ConsumeQualifiedRule(source));
                        break;
                }
            }

            return rules;
        }

        /// <summary>
        /// Tries to consume a @-rule from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The @-rule or null.</returns>
        internal CssAtRule ConsumeAtRule(IEnumerator<CssToken> source)
        {
            var rule = new CssAtRule();
            var keyword = source.Current as CssKeywordToken;

            if (keyword == null)
                return null;

            rule.Name = keyword.Data;

            while (source.MoveNext())
                if (source.Current.Type != CssTokenType.Whitespace)
                    break;

            do
            {
                switch (source.Current.Type)
                {
                    case CssTokenType.Semicolon:
                        return rule;

                    case CssTokenType.CurlyBracketOpen:
                        rule.Value = ConsumeSimpleBlock(source);
                        return rule;

                    default:
                        rule.Prelude.Add(ConsumeComponentValue(source));
                        break;
                }
            }
            while (source.MoveNext());

            return rule;
        }

        /// <summary>
        /// Tries to consume a qualified rule from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The qualified rule.</returns>
        internal CssQualifiedRule ConsumeQualifiedRule(IEnumerator<CssToken> source)
        {
            var rule = new CssQualifiedRule();

            if (source.Current == null)
                return null;

            do
            {
                switch (source.Current.Type)
                {
                    case CssTokenType.CurlyBracketOpen:
                        source.MoveNext();
                        var tokens = Jump(source, CssTokenType.CurlyBracketClose);
                        var declarations = ConsumeDeclarations(tokens.GetEnumerator());
                        rule.Value.AddRange(declarations);
                        return rule;

                    default:
                        rule.Prelude.Add(ConsumeComponentValue(source));
                        break;
                }

            }
            while (source.MoveNext());

            return rule;
        }

        /// <summary>
        /// Tries to consume a list of declarations from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The list of declarations.</returns>
        internal List<CssNamedRule> ConsumeDeclarations(IEnumerator<CssToken> source)
        {
            var list = new List<CssNamedRule>();

            while (source.MoveNext())
            {
                switch (source.Current.Type)
                {
                    case CssTokenType.Whitespace:
                    case CssTokenType.Semicolon:
                        break;

                    case CssTokenType.AtKeyword:
                        list.Add(ConsumeAtRule(source));
                        break;

                    case CssTokenType.Ident:
                        var tokens = Jump(source, CssTokenType.Semicolon);
                        var it = tokens.GetEnumerator();
                        it.MoveNext();
                        var decl = ConsumeDeclaration(it);

                        if(decl != null)
                            list.Add(decl);

                        break;

                    default:
                        RaiseErrorOccurred(ErrorCode.InvalidCharacter);
                        Jump(source, CssTokenType.Semicolon);
                        break;
                }
            }

            return list;
        }

        /// <summary>
        /// Tries to consume a declaration from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The declaration or NULL.</returns>
        internal CssDeclaration ConsumeDeclaration(IEnumerator<CssToken> source)
        {
            var decl = new CssDeclaration();
            var keyword = source.Current as CssKeywordToken;

            if (keyword == null)
                return null;

            decl.Name = keyword.Data;

            while (source.MoveNext())
            {
                switch (source.Current.Type)
                {
                    case CssTokenType.Whitespace:
                        break;

                    case CssTokenType.Colon:
                        while (source.MoveNext())
                        {
                            var value = ConsumeComponentValue(source);
                            decl.Prelude.Add(value);
                        }

                        break;

                    default:
                        RaiseErrorOccurred(ErrorCode.InputUnexpected);
                        return null;
                }

            }

            if (decl.Prelude.Count > 1)
            {
                var prev = decl.Prelude[decl.Prelude.Count - 2].Preserved as CssDelimToken;
                var ident = decl.Prelude[decl.Prelude.Count - 1].Preserved as CssKeywordToken;

                if (prev != null && ident != null)
                {
                    decl.Important = prev.Data == Specification.EM && ident.Data.Equals("important", StringComparison.InvariantCultureIgnoreCase);
                    decl.Prelude.RemoveRange(decl.Prelude.Count - 2, 2);
                }
            }

            return decl;
        }

        /// <summary>
        /// Tries to consume a component value from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The value or NULL.</returns>
        internal CssComponentValue ConsumeComponentValue(IEnumerator<CssToken> source)
        {
            if (source.Current == null)
                return null;

            switch (source.Current.Type)
            {
                case CssTokenType.CurlyBracketOpen:
                case CssTokenType.SquareBracketOpen:
                case CssTokenType.RoundBracketOpen:
                    return new CssComponentValue { Block = ConsumeSimpleBlock(source) };

                case CssTokenType.Function:
                    return new CssComponentValue { Function = ConsumeFunction(source) };

                default:
                    return new CssComponentValue { Preserved = source.Current };
            }
        }

        /// <summary>
        /// Tries to consume a hashless component value (quirks-mode) from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The value or NULL.</returns>
        internal CssComponentValue ConsumeComponentValueHashless(IEnumerator<CssToken> source)
        {
            if (source.Current == null)
                return null;

            //background-color
            //border-color
            //border-top-color
            //border-right-color
            //border-bottom-color
            //border-left-color
            //color

            switch (source.Current.Type)
            {
                case CssTokenType.Number:
                case CssTokenType.Dimension:
                case CssTokenType.Ident:
                    var value = source.Current.ToValue();

                    if ((value.Length == 3 || value.Length == 6) && Specification.IsHex(value))
                        return new CssComponentValue() { Preserved = CssKeywordToken.Hash(value) };

                    break;
            }

            return ConsumeComponentValue(source);
        }

        /// <summary>
        /// Tries to consume a unitless component value (quirks-mode) from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The value or NULL.</returns>
        internal CssComponentValue ConsumeComponentValueUnitless(IEnumerator<CssToken> source)
        {
            if (source.Current == null)
                return null;

            //border-top-width
            //border-right-width
            //border-bottom-width
            //border-left-width
            //border-width
            //bottom
            //font-size
            //height
            //left
            //letter-spacing
            //margin
            //margin-right
            //margin-left
            //margin-top
            //margin-bottom
            //padding
            //padding-top
            //padding-bottom
            //padding-left
            //padding-right
            //right
            //top
            //width
            //word-spacing

            if (source.Current.Type == CssTokenType.Number)
            {
                var value = ((CssNumberToken)source.Current).Data;
                return new CssComponentValue { Preserved = CssUnitToken.Dimension(value, "px") };
            }

            return ConsumeComponentValue(source);
        }

        /// <summary>
        /// Tries to consume a block from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The block or NULL.</returns>
        internal CssBlock ConsumeSimpleBlock(IEnumerator<CssToken> source)
        {
            var block = new CssBlock();

            block.AssociatedToken = source.Current as CssBracketToken;

            if (block.AssociatedToken == null)
                return null;

            block.Tokens.Add(source.Current);

            while (source.MoveNext())
            {
                block.Tokens.Add(source.Current);

                if (source.Current.Type == block.AssociatedToken.Mirror)
                    break;

                var value = ConsumeComponentValue(source);

                if (value != null)
                    block.Value.Add(value);
            }

            return block;
        }

        /// <summary>
        /// Tries to consume a function from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The function or NULL.</returns>
        internal CssFunction ConsumeFunction(IEnumerator<CssToken> source)
        {
            var function = new CssFunction();
            var keyword = source.Current as CssKeywordToken;

            if (keyword == null)
                return null;

            function.Name = keyword.Data;
            var arg = new CssArg();
            function.Arguments.Add(arg);
            function.Tokens.Add(source.Current);

            while (source.MoveNext())
            {
                function.Tokens.Add(source.Current);

                switch (source.Current.Type)
                {
                    case CssTokenType.RoundBracketClose:
                        return function;

                    case CssTokenType.Comma:
                        arg = new CssArg();
                        function.Arguments.Add(arg);
                        break;

                    case CssTokenType.Number:
                        CssComponentValue value;

                        if (_quirksFlag && function.Name.Equals("rect", StringComparison.InvariantCultureIgnoreCase))
                            value = ConsumeComponentValueUnitless(source);
                        else
                            value = ConsumeComponentValue(source);

                        if (value != null)
                            arg.Values.Add(value);
                        else
                            function.IsValid = false;

                        break;

                    default:
                        arg.Values.Add(ConsumeComponentValue(source));
                        break;
                }
            }

            return function;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Gets the token enumerable.
        /// </summary>
        /// <returns>The enumerable csstoken list.</returns>
        internal IEnumerable<CssToken> GetTokens()
        {
            Reset();

            while (_state != null)
            {
                _state();
                ReadNext();

                while (tokens.Count > 0)
                    yield return tokens.Dequeue();
            }
        }

        /// <summary>
        /// Jumps in the given iterator to the specified token.
        /// </summary>
        /// <param name="source">The iterator to consider.</param>
        /// <param name="token">The type of the final (not consumed) token.</param>
        /// <returns>The (optional) list of skipped tokens.</returns>
        static List<CssToken> Jump(IEnumerator<CssToken> source, CssTokenType token)
        {
            var tokens = new List<CssToken>();

            do
            {
                if (source.Current.Type == token)
                    break;

                tokens.Add(source.Current);
            }
            while (source.MoveNext());

            return tokens;
        }

        /// <summary>
        /// Handles emitting the token.
        /// </summary>
        /// <param name="token"></param>
        void Emit(CssToken token)
        {
            tokens.Enqueue(token);
        }

        /// <summary>
        /// Resets the stream reader.
        /// </summary>
        void Reset()
        {
            buffer.Clear();
            _source.Reset();
            ReadNext();
            _state = Data;
        }

        /// <summary>
        /// Switches to the specified state and reconsumes the current character.
        /// </summary>
        /// <param name="state">The state to switch to.</param>
        void InstantSwitch(Action state)
        {
            _state = state;
            state();
        }

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
                buffer.Append('e').Append(current);
                _state = SciNotation;
                return;
            }
            else if (current == Specification.PLUS || current == Specification.DASH)
            {
                var op = current;
                ReadNext();

                if (Specification.IsDigit(current))
                {
                    buffer.Append('e').Append(op).Append(current);
                    _state = SciNotation;
                    return;
                }

                ReadPrevious();
            }

            ReadPrevious();
            temp = Interpret(buffer.ToString());
            buffer.Clear();
            buffer.Append(current);
            _state = Dimension;
        }

        /// <summary>
        /// Substate of several Number states.
        /// </summary>
        void NumberDash()
        {
            ReadNext();

            if (Specification.IsNameStart(current))
            {
                temp = Interpret(buffer.ToString());
                buffer.Clear();
                buffer.Append(Specification.DASH).Append(current);
                _state = Dimension;
            }
            else if (IsValidEscape())
            {
                ReadNext();
                temp = Interpret(buffer.ToString());
                buffer.Clear();
                buffer.Append(Specification.DASH).Append(ConsumeEscape());
                _state = Dimension;
            }
            else
            {
                ReadPrevious();
                Emit(new CssNumberToken(Interpret(buffer.ToString())));
                buffer.Clear();
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
                buffer.Append(current);
            else if (Specification.IsNameStart(current))
            {
                temp = Interpret(buffer.ToString());
                buffer.Clear();
                buffer.Append(current);
                _state = Dimension;
            }
            else if (IsValidEscape())
            {
                ReadNext();
                temp = Interpret(buffer.ToString());
                buffer.Clear();
                buffer.Append(ConsumeEscape());
                _state = Dimension;
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
            if(current != Specification.RSOLIDUS)
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
        /// Reads the next character.
        /// </summary>
        void ReadNext()
        {
            current = _source.Next();
        }

        /// <summary>
        /// Reads the previous character.
        /// </summary>
        void ReadPrevious()
        {
            current = _source.Previous();
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Takes a string and transforms it into a selector object.
        /// </summary>
        /// <param name="selector">The string to parse.</param>
        /// <returns>The Selector object.</returns>
        public static Selector ParseSelector(string selector)
        {
            var parser = new CssParser(selector);
            var tokens = parser.Iterator;
            var ctor = new CssSelectorConstructor();

            while (tokens.MoveNext())
                ctor.PickSelector(tokens);

            return ctor.Result;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS stylesheet.
        /// </summary>
        /// <param name="stylesheet">The string to parse.</param>
        /// <param name="quirksMode">Optional: The status of the quirks mode flag (usually not set).</param>
        /// <returns>The CSSStyleSheet object.</returns>
        public static CSSStyleSheet ParseStyleSheet(string stylesheet, bool quirksMode = false)
        {
            var parser = new CssParser(stylesheet);
            parser.IsQuirksMode = quirksMode;
            return parser.Parse();
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS rule.
        /// </summary>
        /// <param name="rule">The string to parse.</param>
        /// <param name="quirksMode">Optional: The status of the quirks mode flag (usually not set).</param>
        /// <returns>The CSSRule object.</returns>
        public static CSSRule ParseRule(string rule, bool quirksMode = false)
        {
            var parser = new CssParser(rule);
            parser.IsQuirksMode = quirksMode;
            var it = parser.Iterator;

            while (it.Current != null)
            {
                if (it.Current.Type != CssTokenType.Whitespace)
                    break;

                if (!it.MoveNext())
                    break;
            }

            if (it.Current == null)
                throw new DOMException(ErrorCode.SyntaxError);

            if (it.Current.Type == CssTokenType.Cdo || it.Current.Type == CssTokenType.Cdc)
                throw new DOMException(ErrorCode.SyntaxError);

            CSSRule myrule = null;

            if (it.Current.Type == CssTokenType.AtKeyword)
            {
                var atrule = parser.ConsumeAtRule(it);

                if (atrule == null)
                    throw new DOMException(ErrorCode.SyntaxError);

                myrule = CssRuleConstructor.Create(parser, atrule);
            }
            else
            {
                var qrule = parser.ConsumeQualifiedRule(it);

                if (qrule == null)
                    throw new DOMException(ErrorCode.SyntaxError);

                myrule = CssRuleConstructor.Create(parser, qrule);
            }

            while (it.MoveNext())
            {
                if (it.Current.Type != CssTokenType.Whitespace)
                    break;
            }

            if (it.Current.Type != CssTokenType.Whitespace)
                throw new DOMException(ErrorCode.SyntaxError);

            return myrule;
        }

        /// <summary>
        /// Takes a string and transforms it into CSS declarations.
        /// </summary>
        /// <param name="declarations">The string to parse.</param>
        /// <param name="quirksMode">Optional: The status of the quirks mode flag (usually not set).</param>
        /// <returns>The CSSStyleDeclaration object.</returns>
        public static CSSStyleDeclaration ParseDeclarations(string declarations, bool quirksMode = false)
        {
            var parser = new CssParser(declarations);
            parser.IsQuirksMode = quirksMode;
            var it = parser.Iterator;
            var decl = parser.ConsumeDeclarations(it);

            if (decl != null)
                return new CSSStyleDeclaration(decl);

            return null;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS value.
        /// </summary>
        /// <param name="value">The string to parse.</param>
        /// <param name="quirksMode">Optional: The status of the quirks mode flag (usually not set).</param>
        /// <returns>The CSSValue object.</returns>
        public static CSSValue ParseValue(string value, bool quirksMode = false)
        {
            var parser = new CssParser(value);
            parser.IsQuirksMode = quirksMode;
            var it = parser.Iterator;

            while (it.Current != null)
            {
                if (it.Current.Type != CssTokenType.Whitespace)
                    break;

                if (!it.MoveNext())
                    throw new DOMException(ErrorCode.SyntaxError);
            }

            var cmp = parser.ConsumeComponentValue(it);

            if (cmp == null)
                throw new DOMException(ErrorCode.SyntaxError);

            while (true)
            {
                if (it.Current.Type != CssTokenType.Whitespace)
                    throw new DOMException(ErrorCode.SyntaxError);

                if (!it.MoveNext())
                    return CSSValue.Create(cmp);
            }
        }

        /// <summary>
        /// Takes a string and transforms it into a list of CSS values.
        /// </summary>
        /// <param name="values">The string to parse.</param>
        /// <param name="quirksMode">Optional: The status of the quirks mode flag (usually not set).</param>
        /// <returns>The CSSValueList object.</returns>
        public static CSSValueList ParseValueList(string values, bool quirksMode = false)
        {
            var list = new List<CSSValue>();
            var parser = new CssParser(values);
            parser.IsQuirksMode = quirksMode;
            var it = parser.Iterator;

            while (it.Current != null)
            {
                var value = parser.ConsumeComponentValue(it);
                list.Add(CSSValue.Create(value));

                if (!it.MoveNext())
                    break;
            }

            return new CSSValueList(list);
        }

        /// <summary>
        /// Takes a comma separated string and transforms it into a list of CSS values.
        /// </summary>
        /// <param name="values">The string to parse.</param>
        /// <param name="quirksMode">Optional: The status of the quirks mode flag (usually not set).</param>
        /// <returns>The CSSValueList object.</returns>
        public static List<CSSValueList> ParseMultipleValues(string values, bool quirksMode = false)
        {
            var parser = new CssParser(values);
            parser.IsQuirksMode = quirksMode;
            var it = parser.Iterator;
            var val = new List<CSSValueList>();
            var temp = new List<CSSValue>();

            while (it.Current != null)
            {
                if (it.Current.Type == CssTokenType.Comma)
                {
                    val.Add(new CSSValueList(temp));
                    temp = new List<CSSValue>();
                }
                else
                {
                    var value = parser.ConsumeComponentValue(it);
                    temp.Add(CSSValue.Create(value));
                }

                if (!it.MoveNext())
                    break;
            }

            if (temp.Count > 0)
                val.Add(new CSSValueList(temp));

            return val;
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
                pck.Line = _source.Line;
                pck.Column = _source.Column;
                ErrorOccurred(this, pck);
            }
        }

        #endregion
    }
}
