using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace AngleSharp.Html
{
    /// <summary>
    /// Performs the tokenization of the source code. Follows the tokenization algorithm at:
    /// http://www.w3.org/html/wg/drafts/html/master/syntax.html
    /// </summary>
    [DebuggerStepThrough]
    class HtmlTokenizer
    {
        #region Members

        SourceManager src;
        Boolean allowCdata;
        String lastStartTag;
        StringBuilder stringBuffer;
        Queue<HtmlToken> tokenBuffer;
        Boolean buffered;
        StringBuilder reference;
        HtmlParseMode model;

        #endregion

        #region Events

        /// <summary>
        /// The event will be fired once an error has been detected.
        /// </summary>
        public event EventHandler<ParseErrorEventArgs> ErrorOccurred;

        #endregion

        #region ctor

        /// <summary>
        /// See 8.2.4 Tokenization
        /// </summary>
        /// <param name="source">The source code manager.</param>
        public HtmlTokenizer(SourceManager source)
        {
            src = source;
            model = HtmlParseMode.PCData;
            buffered = false;
            allowCdata = true;
            reference = new StringBuilder();
            stringBuffer = new StringBuilder();
            tokenBuffer = new Queue<HtmlToken>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if CDATA sections are accepted.
        /// </summary>
        public Boolean AcceptsCDATA
        {
            get { return allowCdata; }
            set { allowCdata = value; }
        }

        /// <summary>
        /// Gets the underlying stream.
        /// </summary>
        public SourceManager Stream
        {
            get { return src; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the next available token.
        /// </summary>
        /// <returns>The next available token.</returns>
        public HtmlToken Get()
        {
            if (buffered) return DequeueToken();
            if (src.IsEnded) return HtmlToken.EOF;
            HtmlToken token = null;

            switch (model)
            {
                case HtmlParseMode.PCData:
                    token = Data(src.Current);
                    break;

                case HtmlParseMode.RCData:
                    token = RCData(src.Current);
                    break;

                case HtmlParseMode.Plaintext:
                    token = Plaintext(src.Current);
                    break;

                case HtmlParseMode.Rawtext:
                    token = Rawtext(src.Current);
                    break;

                case HtmlParseMode.Script:
                    token = ScriptData(src.Current);
                    break;
            }

            src.Advance();
            return token;
        }

        /// <summary>
        /// Switches the current tokenization state
        /// to the desired content model.
        /// </summary>
        /// <param name="state">The new state.</param>
        public void Switch(HtmlParseMode state)
        {
            model = state;
        }

        #endregion

        #region General

        /// <summary>
        /// See 8.2.4.7 PLAINTEXT state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken Plaintext(Char c)
        {
            switch (c)
            {
                case Specification.NULL:
                    RaiseErrorOccurred(ErrorCode.NULL);
                    return HtmlToken.Character(Specification.REPLACEMENT);

                case Specification.EOF:
                    return HtmlToken.EOF;

                default:
                    return HtmlToken.Character(c);
            }
        }

        /// <summary>
        /// See 8.2.4.1 Data state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken Data(Char c)
        {
            switch (c)
            {
                case Specification.AMPERSAND:
                    var value = CharacterReference(src.Next);

                    if (value == null)
                        return HtmlToken.Character(Specification.AMPERSAND);
                    
                    return HtmlToken.Characters(value);

                case Specification.LT:
                    return TagOpen(src.Next);

                case Specification.NULL:
                    RaiseErrorOccurred(ErrorCode.NULL);
                    return Data(src.Next);

                case Specification.EOF:
                    return HtmlToken.EOF;

                default:
                    return HtmlToken.Character(c);
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
            switch (c)
            {
                case Specification.AMPERSAND:
                    var value = CharacterReference(src.Next);

                    if (value == null)
                        return HtmlToken.Character(Specification.AMPERSAND);

                    return HtmlToken.Characters(value);

                case Specification.LT:
                    return RCDataLT(src.Next);

                case Specification.NULL:
                    RaiseErrorOccurred(ErrorCode.NULL);
                    return HtmlToken.Character(Specification.REPLACEMENT);

                case Specification.EOF:
                    return HtmlToken.EOF;

                default:
                    return HtmlToken.Character(c);
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
                stringBuffer.Clear();
                return RCDataEndTag(src.Next);
            }

            return HtmlToken.Character(Specification.LT);
        }

        /// <summary>
        /// See 8.2.4.12 RCDATA end tag open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken RCDataEndTag(Char c)
        {
            if (Specification.IsUppercaseAscii(c))
            {
                stringBuffer.Clear();
                stringBuffer.Append(c.ToLower());
                return RCDataNameEndTag(src.Next, HtmlToken.CloseTag());
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                stringBuffer.Clear();
                stringBuffer.Append(c);
                return RCDataNameEndTag(src.Next, HtmlToken.CloseTag());
            }
            else
            {
                src.Back();
                return HtmlToken.Characters(Specification.LT, Specification.SOLIDUS);
            }
        }

        /// <summary>
        /// See 8.2.4.13 RCDATA end tag name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken RCDataNameEndTag(Char c, HtmlTagToken tag)
        {
            var name = stringBuffer.ToString();
            var appropriateTag = name == lastStartTag;

            if (appropriateTag && Specification.IsSpaceCharacter(c))
            {
                tag.Name = name;
                return AttributeBeforeName(src.Next, tag);
            }
            else if (appropriateTag && c == Specification.SOLIDUS)
            {
                tag.Name = name;
                return TagSelfClosing(src.Next, tag);
            }
            else if (appropriateTag && c == Specification.GT)
            {
                tag.Name = name;
                return EmitTag(tag);
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                stringBuffer.Append(c.ToLower());
                return RCDataNameEndTag(src.Next, tag);
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                stringBuffer.Append(c);
                return RCDataNameEndTag(src.Next, tag);
            }
            else
            {
                src.Back();
                stringBuffer.Insert(0, Specification.LT).Insert(1, Specification.SOLIDUS);
                return HtmlToken.Characters(stringBuffer.ToString());
            }
        }

        #endregion

        #region Rawtext

        /// <summary>
        /// See 8.2.4.5 RAWTEXT state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken Rawtext(Char c)
        {
            switch (c)
            {
                case Specification.LT:
                    return RawtextLT(src.Next);

                case Specification.NULL:
                    RaiseErrorOccurred(ErrorCode.NULL);
                    return HtmlToken.Character(Specification.REPLACEMENT);

                case Specification.EOF:
                    return HtmlToken.EOF;

                default:
                    return HtmlToken.Character(c);
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
                stringBuffer.Clear();
                return RawtextEndTag(src.Next);
            }

            src.Back();
            return HtmlToken.Character(Specification.LT);
        }

        /// <summary>
        /// See 8.2.4.15 RAWTEXT end tag open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken RawtextEndTag(Char c)
        {
            if (Specification.IsUppercaseAscii(c))
            {
                stringBuffer.Clear();
                stringBuffer.Append(c.ToLower());
                return RawtextNameEndTag(src.Next, HtmlToken.CloseTag());
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                stringBuffer.Clear();
                stringBuffer.Append(c);
                return RawtextNameEndTag(src.Next, HtmlToken.CloseTag());
            }

            src.Back();
            return HtmlToken.Characters(Specification.LT, Specification.SOLIDUS);
        }

        /// <summary>
        /// See 8.2.4.16 RAWTEXT end tag name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken RawtextNameEndTag(Char c, HtmlTagToken tag)
        {
            var name = stringBuffer.ToString();
            var appropriateTag = name == lastStartTag;

            if (appropriateTag && Specification.IsSpaceCharacter(c))
            {
                tag.Name = name;
                return AttributeBeforeName(src.Next, tag);
            }
            else if (appropriateTag && c == Specification.SOLIDUS)
            {
                tag.Name = name;
                return TagSelfClosing(src.Next, tag);
            }
            else if (appropriateTag && c == Specification.GT)
            {
                tag.Name = name;
                return EmitTag(tag);
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                stringBuffer.Append(c.ToLower());
                return RawtextNameEndTag(src.Next, tag);
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                stringBuffer.Append(c);
                return RawtextNameEndTag(src.Next, tag);
            }
            else
            {
                src.Back();
                stringBuffer.Insert(0, Specification.LT).Insert(1, Specification.SOLIDUS);
                return HtmlToken.Characters(stringBuffer.ToString());

            }
        }

        #endregion

        #region CDATA

        /// <summary>
        /// See 8.2.4.68 CDATA section state
        /// </summary>
        HtmlToken CData(Char c)
        {
            stringBuffer.Clear();

            while (true)
            {
                if (c == Specification.EOF)
                {
                    src.Back();
                    break;
                }
                else if (c == ']' && src.ContinuesWith("]]>"))
                {
                    src.Advance(3);
                    break;
                }

                stringBuffer.Append(c);
                c = src.Next;
            }

            return HtmlToken.Characters(stringBuffer.ToString());
        }

        /// <summary>
        /// See 8.2.4.69 Tokenizing character references
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="allowedCharacter">The additionally allowed character if there is one.</param>
        char[] CharacterReference(char c, char allowedCharacter = Specification.NULL)
        {
            if (Specification.IsSpaceCharacter(c) || c == Specification.LT || c == Specification.AMPERSAND || c == allowedCharacter)
            {
                return null;
            }
            else if (c == Specification.NUM)
            {
                int num = 0;
                var nums = new List<int>();
                var isHex = src.Current == 'x' || src.Current == 'X';
                src.Advance();

                if (isHex)
                {
                    src.Advance();

                    while (Specification.IsHex(src.Current))
                    {
                        nums.Add(src.Current.FromHex());
                        src.Advance();
                    }

                    var basis = 1;

                    for (var i = nums.Count - 1; i >= 0; i--)
                    {
                        num += nums[i] * basis;
                        basis *= 16;
                    }
                }
                else
                {
                    while (Specification.IsDigit(src.Current))
                    {
                        nums.Add(src.Current.FromHex());
                        src.Advance();
                    }

                    var basis = 1;

                    for (var i = nums.Count - 1; i >= 0; i--)
                    {
                        num += nums[i] * basis;
                        basis *= 10;
                    }
                }

                if (nums.Count == 0)
                {
                    src.Back();

                    if (isHex)
                        src.Back();

                    RaiseErrorOccurred(ErrorCode.CharacterReferenceWrongNumber);
                    return null;
                }

                if (src.Current != Specification.SC)
                {
                    RaiseErrorOccurred(ErrorCode.CharacterReferenceSemicolonMissing);
                    src.Back();
                }

                if (Entities.IsInCharacterTable(num))
                {
                    RaiseErrorOccurred(ErrorCode.CharacterReferenceInvalidCode);
                    return Entities.GetSymbolFromTable(num);
                }

                if(Entities.IsInvalidNumber(num))
                {
                    RaiseErrorOccurred(ErrorCode.CharacterReferenceInvalidNumber);
                    return new char[]{ Specification.REPLACEMENT };
                }

                if (Entities.IsInInvalidRange(num))
                    RaiseErrorOccurred(ErrorCode.CharacterReferenceInvalidRange);

                return Entities.Convert(num);
            }

            /*
             * Consume the maximum number of characters possible, with the consume
             * characters matching one of the identifiers in the first column of the
             * named character references table (in a case-sensitive manner).
             * If no match can be made, then no characters are consumed, and nothing
             * is returned. In this case, if the characters after the U+0026 AMPERSAND
             * character (&) consist of a sequence of one or more alphanumeric ASCII
             * characters followed by a U+003B SEMICOLON character (;), then this is
             * a parse error.
             * 
             * If the character reference is being consumed as part of an attribute,
             * and the last character matched is not a ";" (U+003B) character, and
             * the next character is either a "=" (U+003D) character or an alphanumeric
             * ASCII character, then, for historical reasons, all the characters that
             * were matched after the U+0026 AMPERSAND character (&) must be unconsumed,
             * and nothing is returned.  However, if this next character is in fact a "="
             * (U+003D) character, then this is a parse error, because some legacy user
             * agents  will misinterpret the markup in those cases.
             * 
             * Otherwise, a character reference is parsed. If the last character matched
             * is not a ";" (U+003B) character, there is a parse error.
             * 
             * Return one or two character tokens for the character(s) corresponding to
             * the character reference name (as given by the second column of the named
             * character references table).
             */

            char[] last = null;
            int consumed = 0;
            int start = src.InsertionPoint;

            do
            {
                var chr = src.Current;

                if (chr == ';' || !Specification.IsAlphanumericAscii(chr))
                    break;

                reference.Append(chr);
                consumed++;
                var value = Entities.GetSymbol(reference.ToString());

                if (value != null)
                {
                    consumed = 0;
                    last = value;
                }

                src.Advance();
            }
            while (!src.IsEnded);

            reference.Clear();

            while (consumed != 0)
            {
                src.Back();
                consumed--;
            }

            if (src.Current != ';')
            {
                if (allowedCharacter != Specification.NULL)
                {
                    if (src.Current == Specification.EQ || Specification.IsAlphanumericAscii(src.Current))
                    {
                        if (src.Current == Specification.EQ)
                            RaiseErrorOccurred(ErrorCode.CharacterReferenceAttributeEqualsFound);

                        src.InsertionPoint = start;
                        return null;
                    }
                }

                src.Back();
                RaiseErrorOccurred(ErrorCode.CharacterReferenceNotTerminated);
            }

            return last;
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
                return MarkupDeclaration(src.Next);
            }
            else if (c == Specification.SOLIDUS)
            {
                return TagEnd(src.Next);
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                stringBuffer.Clear();
                stringBuffer.Append(c.ToLower());
                return TagName(src.Next, HtmlToken.OpenTag());
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                stringBuffer.Clear();
                stringBuffer.Append(c);
                return TagName(src.Next, HtmlToken.OpenTag());
            }
            else if (c == Specification.QM)
            {
                RaiseErrorOccurred(ErrorCode.BogusComment);
                return BogusComment(c);
            }
            else
            {
                model = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.AmbiguousOpenTag);
                return HtmlToken.Character(Specification.LT);
            }
        }

        /// <summary>
        /// See 8.2.4.9 End tag open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken TagEnd(Char c)
        {
            if (Specification.IsUppercaseAscii(c))
            {
                stringBuffer.Clear();
                stringBuffer.Append(c.ToLower());
                return TagName(src.Next, HtmlToken.CloseTag());
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                stringBuffer.Clear();
                stringBuffer.Append(c);
                return TagName(src.Next, HtmlToken.CloseTag());
            }
            else if (c == Specification.GT)
            {
                model = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return Data(src.Next);
            }
            else if (c == Specification.EOF)
            {
                src.Back();
                RaiseErrorOccurred(ErrorCode.EOF);
                return HtmlToken.Characters(Specification.LT, Specification.SOLIDUS);
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
            if (Specification.IsSpaceCharacter(c))
            {
                tag.Name = stringBuffer.ToString();
                return AttributeBeforeName(src.Next, tag);
            }
            else if (c == Specification.SOLIDUS)
            {
                tag.Name = stringBuffer.ToString();
                return TagSelfClosing(src.Next, tag);
            }
            else if (c == Specification.GT)
            {
                tag.Name = stringBuffer.ToString();
                return EmitTag(tag);
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                stringBuffer.Append(c.ToLower());
                return TagName(src.Next, tag);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                stringBuffer.Append(Specification.REPLACEMENT);
                return TagName(src.Next, tag);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return HtmlToken.EOF;
            }
            else
            {
                stringBuffer.Append(c);
                return TagName(src.Next, tag);
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
            if (src.ContinuesWith("--"))
            {
                src.Advance();
                return CommentStart(src.Next);
            }
            else if (src.ContinuesWith("doctype"))
            {
                src.Advance(6);
                return Doctype(src.Next);
            }
            else if (allowCdata && src.ContinuesWith("[CDATA[", false))
            {
                src.Advance(6);
                return CData(src.Next);
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
            stringBuffer.Clear();

            while(true)
            {

                if (c == Specification.GT)
                    break;
                else if (c == Specification.EOF)
                {
                    src.Back();
                    break;
                }
                else if (c == Specification.NULL)
                    stringBuffer.Append(Specification.REPLACEMENT);
                else
                    stringBuffer.Append(c);

                c = src.Next;
            }

            model = HtmlParseMode.PCData;
            return HtmlToken.Comment(stringBuffer.ToString());
        }

        /// <summary>
        /// See 8.2.4.46 Comment start state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken CommentStart(Char c)
        {
            stringBuffer.Clear();

            if (c == Specification.DASH)
                return CommentDashStart(src.Next);
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                stringBuffer.Append(Specification.REPLACEMENT);
                return Comment(src.Next);
            }
            else if (c == Specification.GT)
            {
                model = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return HtmlToken.Comment(stringBuffer.ToString());
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
                return HtmlToken.Comment(stringBuffer.ToString());
            }
            else
            {
                stringBuffer.Append(c);
                return Comment(src.Next);
            }
        }

        /// <summary>
        /// See 8.2.4.47 Comment start dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken CommentDashStart(Char c)
        {
            if (c == Specification.DASH)
                return CommentEnd(src.Next);
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                stringBuffer.Append(Specification.DASH);
                stringBuffer.Append(Specification.REPLACEMENT);
                return Comment(src.Next);
            }
            else if (c == Specification.GT)
            {
                model = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return HtmlToken.Comment(stringBuffer.ToString());
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
                return HtmlToken.Comment(stringBuffer.ToString());
            }

            stringBuffer.Append(Specification.DASH);
            stringBuffer.Append(c);
            return Comment(src.Next);
        }

        /// <summary>
        /// See 8.2.4.48 Comment state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken Comment(Char c)
        {
            while (true)
            {
                if (c == Specification.DASH)
                    return CommentDashEnd(src.Next);
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    src.Back();
                    return HtmlToken.Comment(stringBuffer.ToString());
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    c = Specification.REPLACEMENT;
                }

                stringBuffer.Append(c);
                c = src.Next;
            }
        }

        /// <summary>
        /// See 8.2.4.49 Comment end dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken CommentDashEnd(Char c)
        {
            if (c == Specification.DASH)
                return CommentEnd(src.Next);
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
                return HtmlToken.Comment(stringBuffer.ToString());
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                c = Specification.REPLACEMENT;
            }

            stringBuffer.Append(Specification.DASH);
            stringBuffer.Append(c);
            return Comment(src.Next);
        }

        /// <summary>
        /// See 8.2.4.50 Comment end state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken CommentEnd(Char c)
        {
            if (c == Specification.GT)
            {
                model = HtmlParseMode.PCData;
                return HtmlToken.Comment(stringBuffer.ToString());
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                stringBuffer.Append(Specification.DASH);
                stringBuffer.Append(Specification.REPLACEMENT);
                return Comment(src.Next);
            }
            else if (c == Specification.EM)
            {
                RaiseErrorOccurred(ErrorCode.CommentEndedWithEM);
                return CommentBangEnd(src.Next);
            }
            else if (c == Specification.DASH)
            {
                RaiseErrorOccurred(ErrorCode.CommentEndedWithDash);
                stringBuffer.Append(Specification.DASH);
                return CommentEnd(src.Next);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
                return HtmlToken.Comment(stringBuffer.ToString());
            }

            RaiseErrorOccurred(ErrorCode.CommentEndedUnexpected);
            stringBuffer.Append(Specification.DASH);
            stringBuffer.Append(Specification.DASH);
            stringBuffer.Append(c);
            return Comment(src.Next);
        }

        /// <summary>
        /// See 8.2.4.51 Comment end bang state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken CommentBangEnd(Char c)
        {
            if (c == Specification.DASH)
            {
                stringBuffer.Append(Specification.DASH);
                stringBuffer.Append(Specification.DASH);
                stringBuffer.Append(Specification.EM);
                return CommentDashEnd(src.Next);
            }
            else if (c == Specification.GT)
            {
                model = HtmlParseMode.PCData;
                return HtmlToken.Comment(stringBuffer.ToString());
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                stringBuffer.Append(Specification.DASH);
                stringBuffer.Append(Specification.DASH);
                stringBuffer.Append(Specification.EM);
                stringBuffer.Append(Specification.REPLACEMENT);
                return Comment(src.Next);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
                return HtmlToken.Comment(stringBuffer.ToString());
            }

            stringBuffer.Append(Specification.DASH);
            stringBuffer.Append(Specification.DASH);
            stringBuffer.Append(Specification.EM);
            stringBuffer.Append(c);
            return Comment(src.Next);
        }

        #endregion

        #region Doctype

        /// <summary>
        /// See 8.2.4.52 DOCTYPE state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken Doctype(Char c)
        {
            if (Specification.IsSpaceCharacter(c))
                return DoctypeNameBefore(src.Next);
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
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
            while (Specification.IsSpaceCharacter(c))
                c = src.Next;

            if (Specification.IsUppercaseAscii(c))
            {
                stringBuffer.Clear();
                stringBuffer.Append(c.ToLower());
                return DoctypeName(src.Next, HtmlToken.Doctype(false));
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                stringBuffer.Clear();
                stringBuffer.Append(Specification.REPLACEMENT);
                return DoctypeName(src.Next, HtmlToken.Doctype(false));
            }
            else if (c == Specification.GT)
            {
                model = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return HtmlToken.Doctype(true);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
                return HtmlToken.Doctype(true);
            }

            stringBuffer.Clear();
            stringBuffer.Append(c);
            return DoctypeName(src.Next, HtmlToken.Doctype(false));
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
                if (Specification.IsSpaceCharacter(c))
                {
                    doctype.Name = stringBuffer.ToString();
                    stringBuffer.Clear();
                    return DoctypeNameAfter(src.Next, doctype);
                }
                else if (c == Specification.GT)
                {
                    model = HtmlParseMode.PCData;
                    doctype.Name = stringBuffer.ToString();
                    return doctype;
                }
                else if (Specification.IsUppercaseAscii(c))
                    stringBuffer.Append(c.ToLower());
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    src.Back();
                    doctype.IsQuirksForced = true;
                    doctype.Name = stringBuffer.ToString();
                    return doctype;
                }
                else
                    stringBuffer.Append(c);

                c = src.Next;
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
            while (Specification.IsSpaceCharacter(c))
                c = src.Next;

            if (c == Specification.GT)
            {
                model = HtmlParseMode.PCData;
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
                doctype.IsQuirksForced = true;
                return doctype;
            }
            else if (src.ContinuesWith("public"))
            {
                src.Advance(5);
                return DoctypePublic(src.Next, doctype);
            }
            else if (src.ContinuesWith("system"))
            {
                src.Advance(5);
                return DoctypeSystem(src.Next, doctype);
            }

            RaiseErrorOccurred(ErrorCode.DoctypeUnexpectedAfterName);
            doctype.IsQuirksForced = true;
            return BogusDoctype(src.Next, doctype);
        }

        /// <summary>
        /// See 8.2.4.56 After DOCTYPE public keyword state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypePublic(Char c, HtmlDoctypeToken doctype)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                return DoctypePublicIdentifierBefore(src.Next, doctype);
            }
            else if (c == Specification.DQ)
            {
                RaiseErrorOccurred(ErrorCode.DoubleQuotationMarkUnexpected);
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierDoubleQuoted(src.Next, doctype);
            }
            else if (c == Specification.SQ)
            {
                RaiseErrorOccurred(ErrorCode.SingleQuotationMarkUnexpected);
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierSingleQuoted(src.Next, doctype);
            }
            else if (c == Specification.GT)
            {
                model = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.IsQuirksForced = true;
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypePublicInvalid);
            doctype.IsQuirksForced = true;
            return BogusDoctype(src.Next, doctype);
        }

        /// <summary>
        /// See 8.2.4.57 Before DOCTYPE public identifier state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypePublicIdentifierBefore(Char c, HtmlDoctypeToken doctype)
        {
            while (Specification.IsSpaceCharacter(c))
                c = src.Next;

            if (c == Specification.DQ)
            {
                stringBuffer.Clear();
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierDoubleQuoted(src.Next, doctype);
            }
            else if (c == Specification.SQ)
            {
                stringBuffer.Clear();
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierSingleQuoted(src.Next, doctype);
            }
            else if (c == Specification.GT)
            {
                model = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.IsQuirksForced = true;
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypePublicInvalid);
            doctype.IsQuirksForced = true;
            return BogusDoctype(src.Next, doctype);
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
                    doctype.PublicIdentifier = stringBuffer.ToString();
                    stringBuffer.Clear();
                    return DoctypePublicIdentifierAfter(src.Next, doctype); ;
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.GT)
                {
                    model = HtmlParseMode.PCData;
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = stringBuffer.ToString();
                    return doctype;
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    src.Back();
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = stringBuffer.ToString();
                    return doctype;
                }
                else
                    stringBuffer.Append(c);

                c = src.Next;
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
                    doctype.PublicIdentifier = stringBuffer.ToString();
                    stringBuffer.Clear();
                    return DoctypePublicIdentifierAfter(src.Next, doctype);
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.GT)
                {
                    model = HtmlParseMode.PCData;
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = stringBuffer.ToString();
                    return doctype;
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    doctype.IsQuirksForced = true;
                    doctype.PublicIdentifier = stringBuffer.ToString();
                    src.Back();
                    return doctype;
                }
                else
                    stringBuffer.Append(c);

                c = src.Next;
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
            if (Specification.IsSpaceCharacter(c))
            {
                stringBuffer.Clear();
                return DoctypeBetween(src.Next, doctype);
            }
            else if (c == Specification.GT)
            {
                model = HtmlParseMode.PCData;
                return doctype;
            }
            else if (c == Specification.DQ)
            {
                RaiseErrorOccurred(ErrorCode.DoubleQuotationMarkUnexpected);
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(src.Next, doctype);
            }
            else if (c == Specification.SQ)
            {
                RaiseErrorOccurred(ErrorCode.SingleQuotationMarkUnexpected);
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierSingleQuoted(src.Next, doctype);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
            doctype.IsQuirksForced = true;
            return BogusDoctype(src.Next, doctype);
        }

        /// <summary>
        /// See 8.2.4.61 Between DOCTYPE public and system identifiers state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypeBetween(Char c, HtmlDoctypeToken doctype)
        {
            while (Specification.IsSpaceCharacter(c))
                c = src.Next;

            if (c == Specification.GT)
            {
                model = HtmlParseMode.PCData;
                return doctype;
            }
            else if (c == Specification.DQ)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(src.Next, doctype);
            }
            else if (c == Specification.SQ)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierSingleQuoted(src.Next, doctype);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
            doctype.IsQuirksForced = true;
            return BogusDoctype(src.Next, doctype);
        }

        /// <summary>
        /// See 8.2.4.62 After DOCTYPE system keyword state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypeSystem(Char c, HtmlDoctypeToken doctype)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                model = HtmlParseMode.PCData;
                return DoctypeSystemIdentifierBefore(src.Next, doctype);
            }
            else if (c == Specification.DQ)
            {
                RaiseErrorOccurred(ErrorCode.DoubleQuotationMarkUnexpected);
                doctype.SystemIdentifier = string.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(src.Next, doctype);
            }
            else if (c == Specification.SQ)
            {
                RaiseErrorOccurred(ErrorCode.SingleQuotationMarkUnexpected);
                doctype.SystemIdentifier = string.Empty;
                return DoctypeSystemIdentifierSingleQuoted(src.Next, doctype);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.SystemIdentifier = stringBuffer.ToString();
                doctype.IsQuirksForced = true;
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypeSystemInvalid);
            doctype.IsQuirksForced = true;
            return BogusDoctype(src.Next, doctype);
        }

        /// <summary>
        /// See 8.2.4.63 Before DOCTYPE system identifier state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        HtmlToken DoctypeSystemIdentifierBefore(Char c, HtmlDoctypeToken doctype)
        {
            while (Specification.IsSpaceCharacter(c))
                c = src.Next;

            if (c == Specification.DQ)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(src.Next, doctype);
            }
            else if (c == Specification.SQ)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierSingleQuoted(src.Next, doctype);
            }
            else if (c == Specification.GT)
            {
                model = HtmlParseMode.PCData;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = stringBuffer.ToString();
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = stringBuffer.ToString();
                src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
            doctype.IsQuirksForced = true;
            return BogusDoctype(src.Next, doctype);
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
                    doctype.SystemIdentifier = stringBuffer.ToString();
                    stringBuffer.Clear();
                    return DoctypeSystemIdentifierAfter(src.Next, doctype);
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.GT)
                {
                    model = HtmlParseMode.PCData;
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    doctype.IsQuirksForced = true;
                    doctype.SystemIdentifier = stringBuffer.ToString();
                    return doctype;
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    doctype.IsQuirksForced = true;
                    doctype.SystemIdentifier = stringBuffer.ToString();
                    src.Back();
                    return doctype;
                }
                else
                    stringBuffer.Append(c);

                c = src.Next;
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
                    doctype.SystemIdentifier = stringBuffer.ToString();
                    stringBuffer.Clear();
                    return DoctypeSystemIdentifierAfter(src.Next, doctype);
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.GT)
                {
                    model = HtmlParseMode.PCData;
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    doctype.IsQuirksForced = true;
                    doctype.SystemIdentifier = stringBuffer.ToString();
                    return doctype;
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    doctype.IsQuirksForced = true;
                    doctype.SystemIdentifier = stringBuffer.ToString();
                    src.Back();
                    return doctype;
                }
                else
                    stringBuffer.Append(c);

                c = src.Next;
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
            while (Specification.IsSpaceCharacter(c))
                c = src.Next;

            if (c == Specification.GT)
            {
                model = HtmlParseMode.PCData;
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
            return BogusDoctype(src.Next, doctype);
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
                    src.Back();
                    return doctype;
                }
                else if (c == Specification.GT)
                {
                    model = HtmlParseMode.PCData;
                    return doctype;
                }

                c = src.Next;
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
            while (Specification.IsSpaceCharacter(c))
                c = src.Next;

            if (c == Specification.SOLIDUS)
            {
                return TagSelfClosing(src.Next, tag);
            }
            else if (c == Specification.GT)
            {
                return EmitTag(tag);
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                stringBuffer.Clear();
                stringBuffer.Append(c.ToLower());
                return AttributeName(src.Next, tag);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                stringBuffer.Clear();
                stringBuffer.Append(Specification.REPLACEMENT);
                return AttributeName(src.Next, tag);
            }
            else if (c == Specification.SQ || c == Specification.DQ || c == Specification.EQ || c == Specification.LT)
            {
                RaiseErrorOccurred(ErrorCode.AttributeNameInvalid);
                stringBuffer.Clear();
                stringBuffer.Append(c);
                return AttributeName(src.Next, tag);
            }
            else if (c == Specification.EOF)
            {
                return HtmlToken.EOF;
            }
            else
            {
                stringBuffer.Clear();
                stringBuffer.Append(c);
                return AttributeName(src.Next, tag);
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
                if (Specification.IsSpaceCharacter(c))
                {
                    tag.AddAttribute(stringBuffer.ToString());
                    return AttributeAfterName(src.Next, tag);
                }
                else if (c == Specification.SOLIDUS)
                {
                    tag.AddAttribute(stringBuffer.ToString());
                    return TagSelfClosing(src.Next, tag);
                }
                else if (c == Specification.EQ)
                {
                    tag.AddAttribute(stringBuffer.ToString());
                    return AttributeBeforeValue(src.Next, tag);
                }
                else if (c == Specification.GT)
                {
                    tag.AddAttribute(stringBuffer.ToString());
                    return EmitTag(tag);
                }
                else if (Specification.IsUppercaseAscii(c))
                    stringBuffer.Append(c.ToLower());
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.DQ || c == Specification.SQ || c == Specification.LT)
                {
                    RaiseErrorOccurred(ErrorCode.AttributeNameInvalid);
                    stringBuffer.Append(c);
                }
                else if (c == Specification.EOF)
                    return HtmlToken.EOF;
                else
                    stringBuffer.Append(c);

                c = src.Next;
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
            while (Specification.IsSpaceCharacter(c))
                c = src.Next;

            if (c == Specification.SOLIDUS)
            {
                return TagSelfClosing(src.Next, tag);
            }
            else if (c == Specification.EQ)
            {
                return AttributeBeforeValue(src.Next, tag);
            }
            else if (c == Specification.GT)
            {
                return EmitTag(tag);
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                stringBuffer.Clear();
                stringBuffer.Append(c.ToLower());
                return AttributeName(src.Next, tag);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                stringBuffer.Clear();
                stringBuffer.Append(Specification.REPLACEMENT);
                return AttributeName(src.Next, tag);
            }
            else if (c == Specification.DQ || c == Specification.SQ || c == Specification.LT)
            {
                RaiseErrorOccurred(ErrorCode.AttributeNameInvalid);
                stringBuffer.Clear();
                stringBuffer.Append(c);
                return AttributeName(src.Next, tag);
            }
            else if (c == Specification.EOF)
            {
                return HtmlToken.EOF;
            }
            else
            {
                stringBuffer.Clear();
                stringBuffer.Append(c);
                return AttributeName(src.Next, tag);
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
            while (Specification.IsSpaceCharacter(c))
                c = src.Next;

            if (c == Specification.DQ)
            {
                stringBuffer.Clear();
                return AttributeDoubleQuotedValue(src.Next, tag);
            }
            else if (c == Specification.AMPERSAND)
            {
                stringBuffer.Clear();
                return AttributeUnquotedValue(c, tag);
            }
            else if (c == Specification.SQ)
            {
                stringBuffer.Clear();
                return AttributeSingleQuotedValue(src.Next, tag);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                stringBuffer.Append(Specification.REPLACEMENT);
                return AttributeUnquotedValue(src.Next, tag);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return EmitTag(tag);
            }
            else if (c == Specification.LT || c == Specification.EQ || c == Specification.CQ)
            {
                RaiseErrorOccurred(ErrorCode.AttributeValueInvalid);
                stringBuffer.Clear().Append(c);
                return AttributeUnquotedValue(src.Next, tag);
            }
            else if (c == Specification.EOF)
            {
                return HtmlToken.EOF;
            }
            else
            {
                stringBuffer.Clear().Append(c);
                return AttributeUnquotedValue(src.Next, tag);
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
                    tag.SetAttributeValue(stringBuffer.ToString());
                    return AttributeAfterValue(src.Next, tag);
                }
                else if (c == Specification.AMPERSAND)
                {
                    var value = CharacterReference(src.Next, Specification.DQ);

                    if (value == null)
                        stringBuffer.Append(Specification.AMPERSAND);
                    else
                        stringBuffer.Append(value);
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.EOF)
                    return HtmlToken.EOF;
                else
                    stringBuffer.Append(c);

                c = src.Next;
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
                    tag.SetAttributeValue(stringBuffer.ToString());
                    return AttributeAfterValue(src.Next, tag);
                }
                else if (c == Specification.AMPERSAND)
                {
                    var value = CharacterReference(src.Next, Specification.SQ);

                    if (value == null)
                        stringBuffer.Append(Specification.AMPERSAND);
                    else
                        stringBuffer.Append(value);
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.EOF)
                    return HtmlToken.EOF;
                else
                    stringBuffer.Append(c);

                c = src.Next;
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
                if (Specification.IsSpaceCharacter(c))
                {
                    tag.SetAttributeValue(stringBuffer.ToString());
                    return AttributeBeforeName(src.Next, tag);
                }
                else if (c == Specification.AMPERSAND)
                {
                    var value = CharacterReference(src.Next, Specification.GT);

                    if (value == null)
                        value = new char[] { Specification.AMPERSAND };

                    tag.SetAttributeValue(new string(value));
                    return AttributeAfterValue(src.Next, tag);
                }
                else if (c == Specification.GT)
                {
                    tag.SetAttributeValue(stringBuffer.ToString());
                    return EmitTag(tag);
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.DQ || c == Specification.SQ || c == Specification.LT || c == Specification.EQ || c == Specification.CQ)
                {
                    RaiseErrorOccurred(ErrorCode.AttributeValueInvalid);
                    stringBuffer.Append(c);
                }
                else if (c == Specification.EOF)
                    return HtmlToken.EOF;
                else
                    stringBuffer.Append(c);

                c = src.Next;
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
            if (Specification.IsSpaceCharacter(c))
                return AttributeBeforeName(src.Next, tag);
            else if (c == Specification.SOLIDUS)
                return TagSelfClosing(src.Next, tag);
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
            switch (c)
            {
                case Specification.LT:
                    EnqueueToken(ScriptDataLT(src.Next));
                    break;

                case Specification.NULL:
                    RaiseErrorOccurred(ErrorCode.NULL);
                    EnqueueToken(HtmlToken.Character(Specification.REPLACEMENT));
                    break;

                case Specification.EOF:
                    EnqueueToken(HtmlToken.EOF);
                    break;

                default:
                    EnqueueToken(HtmlToken.Character(c));
                    break;
            }

            return DequeueToken();
        }
        
        /// <summary>
        /// See 8.2.4.17 Script data less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataLT(Char c)
        {
            if (c == Specification.SOLIDUS)
            {
                return ScriptDataEndTag(src.Next);
            }
            else if (c == Specification.EM)
            {
                EnqueueToken(HtmlToken.Characters(Specification.LT, Specification.EM));
                return ScriptDataStartEscape(src.Next);
            }

            EnqueueToken(HtmlToken.Character(Specification.LT));
            return ScriptData(c);
        }

        /// <summary>
        /// See 8.2.4.18 Script data end tag open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEndTag(Char c)
        {
            if (Specification.IsUppercaseAscii(c))
            {
                stringBuffer.Clear();
                stringBuffer.Append(c.ToLower());
                return ScriptDataNameEndTag(src.Next, HtmlToken.CloseTag());
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                stringBuffer.Clear();
                stringBuffer.Append(c);
                return ScriptDataNameEndTag(src.Next, HtmlToken.CloseTag());
            }

            EnqueueToken(HtmlToken.Characters(Specification.LT, Specification.SOLIDUS));
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
            var name = stringBuffer.ToString();
            var appropriateEndTag = name == lastStartTag;

            if (appropriateEndTag && Specification.IsSpaceCharacter(c))
            {
                tag.Name = name;
                return AttributeBeforeName(src.Next, tag);
            }
            else if (appropriateEndTag && c == Specification.SOLIDUS)
            {
                tag.Name = name;
                return TagSelfClosing(src.Next, tag);
            }
            else if (appropriateEndTag && c == Specification.GT)
            {
                tag.Name = name;
                return EmitTag(tag);
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                stringBuffer.Append(c.ToLower());
                return ScriptDataNameEndTag(src.Next, tag);
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                stringBuffer.Append(c);
                return ScriptDataNameEndTag(src.Next, tag);
            }

            stringBuffer.Insert(0, Specification.LT).Insert(1, Specification.SOLIDUS);
            EnqueueToken(HtmlToken.Characters(stringBuffer.ToString()));
            return ScriptData(c);
        }

        /// <summary>
        /// See 8.2.4.20 Script data escape start state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataStartEscape(Char c)
        {
            if (c == Specification.DASH)
            {
                EnqueueToken(HtmlToken.Character(Specification.DASH));
                return ScriptDataStartEscapeDash(src.Next);
            }

            return ScriptData(c);
        }

        /// <summary>
        /// See 8.2.4.22 Script data escaped state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscaped(Char c)
        {
            if (c == Specification.DASH)
            {
                EnqueueToken(HtmlToken.Character(Specification.DASH));
                return ScriptDataEscapedDash(src.Next);
            }
            else if (c == Specification.LT)
            {
                return ScriptDataEscapedLT(src.Next);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                EnqueueToken(HtmlToken.Character(Specification.REPLACEMENT));
                return ScriptDataEscaped(src.Next);
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
            if (c == Specification.DASH)
            {
                EnqueueToken(HtmlToken.Character(Specification.DASH));
                return ScriptDataEscapedDashDash(src.Next);
            }

            return ScriptData(c);
        }

        /// <summary>
        /// See 8.2.4.23 Script data escaped dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedDash(Char c)
        {
            if (c == Specification.DASH)
            {
                EnqueueToken(HtmlToken.Character(Specification.DASH));
                return ScriptDataEscapedDashDash(src.Next);
            }
            else if (c == Specification.LT)
            {
                return ScriptDataEscapedLT(src.Next);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                EnqueueToken(HtmlToken.Character(Specification.REPLACEMENT));
                return ScriptDataEscaped(src.Next);
            }
            else if (c == Specification.EOF)
            {
                return HtmlToken.EOF;
            }

            EnqueueToken(HtmlToken.Character(c));
            return ScriptDataEscaped(src.Next);
        }

        /// <summary>
        /// See 8.2.4.24 Script data escaped dash dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedDashDash(Char c)
        {
            if (c == Specification.DASH)
            {
                EnqueueToken(HtmlToken.Character(Specification.DASH));
                return ScriptDataEscapedDashDash(src.Next);
            }
            else if (c == Specification.LT)
            {
                return ScriptDataEscapedLT(src.Next);
            }
            else if (c == Specification.GT)
            {
                return HtmlToken.Character(Specification.GT);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                EnqueueToken(HtmlToken.Character(Specification.REPLACEMENT));
                return ScriptDataEscaped(src.Next);
            }
            else if (c == Specification.EOF)
            {
                return HtmlToken.EOF;
            }

            EnqueueToken(HtmlToken.Character(c));
            return ScriptDataEscaped(src.Next);
        }

        /// <summary>
        /// See 8.2.4.25 Script data escaped less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedLT(Char c)
        {
            if (c == Specification.SOLIDUS)
            {
                return ScriptDataEndTag(src.Next);
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                stringBuffer.Clear();
                stringBuffer.Append(c.ToLower());
                EnqueueToken(HtmlToken.Character(Specification.LT));
                EnqueueToken(HtmlToken.Character(c));
                return ScriptDataStartDoubleEscape(src.Next);
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                stringBuffer.Clear();
                stringBuffer.Clear();
                stringBuffer.Append(c);
                EnqueueToken(HtmlToken.Character(Specification.LT));
                EnqueueToken(HtmlToken.Character(c));
                return ScriptDataStartDoubleEscape(src.Next);
            }

            EnqueueToken(HtmlToken.Character(Specification.LT));
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
            if (Specification.IsUppercaseAscii(c))
            {
                stringBuffer.Clear();
                stringBuffer.Append(c.ToLower());
                return ScriptDataEscapedEndTag(src.Next, tag);
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                stringBuffer.Clear();
                stringBuffer.Append(c);
                return ScriptDataEscapedEndTag(src.Next, tag);
            }

            EnqueueToken(HtmlToken.Character(Specification.LT));
            EnqueueToken(HtmlToken.Character(Specification.SOLIDUS));
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
            var name = stringBuffer.ToString();
            var appropriateEndTag = name == lastStartTag;

            if (appropriateEndTag && Specification.IsSpaceCharacter(c))
            {
                tag.Name = name;
                return AttributeBeforeName(src.Next, tag);
            }
            else if (appropriateEndTag && c == Specification.SOLIDUS)
            {
                tag.Name = name;
                return TagSelfClosing(src.Next, tag);
            }
            else if (appropriateEndTag && c == Specification.GT)
            {
                tag.Name = name;
                return EmitTag(tag);
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                stringBuffer.Append(c.ToLower());
                return ScriptDataEscapedNameTag(src.Next, tag);
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                stringBuffer.Append(c);
                return ScriptDataEscapedNameTag(src.Next, tag);
            }

            EnqueueToken(HtmlToken.Character(Specification.LT));
            EnqueueToken(HtmlToken.Character(Specification.SOLIDUS));
            EnqueueToken(HtmlToken.Characters(stringBuffer.ToString()));
            return ScriptDataEscaped(c);
        }

        /// <summary>
        /// See 8.2.4.28 Script data double escape start state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataStartDoubleEscape(Char c)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                if (stringBuffer.ToString() == "script")
                    return ScriptDataEscapedDouble(src.Next);

                EnqueueToken(HtmlToken.Character(c));
                return ScriptDataEscaped(src.Next);
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                stringBuffer.Append(c.ToLower());
                EnqueueToken(HtmlToken.Character(c));
                return ScriptDataStartDoubleEscape(src.Next);
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                stringBuffer.Append(c);
                EnqueueToken(HtmlToken.Character(c));
                return ScriptDataStartDoubleEscape(src.Next);
            }

            return ScriptDataEscaped(c);
        }

        /// <summary>
        /// See 8.2.4.29 Script data double escaped state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedDouble(Char c)
        {
            if (c == Specification.DASH)
            {
                EnqueueToken(HtmlToken.Character(Specification.DASH));
                return ScriptDataEscapedDoubleDash(src.Next);
            }
            else if (c == Specification.LT)
            {
                EnqueueToken(HtmlToken.Character(Specification.LT));
                return ScriptDataEscapedDoubleLT(src.Next);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                EnqueueToken(HtmlToken.Character(Specification.REPLACEMENT));
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return HtmlToken.EOF;
            }

            EnqueueToken(HtmlToken.Character(c));
            return ScriptDataEscapedDouble(src.Next);
        }

        /// <summary>
        /// See 8.2.4.30 Script data double escaped dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedDoubleDash(Char c)
        {
            if (c == Specification.DASH)
            {
                EnqueueToken(HtmlToken.Character(Specification.DASH));
                return ScriptDataEscapedDoubleDashDash(src.Next);
            }
            else if (c == Specification.LT)
            {
                EnqueueToken(HtmlToken.Character(Specification.LT));
                return ScriptDataEscapedDoubleLT(src.Next);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                EnqueueToken(HtmlToken.Character(Specification.REPLACEMENT));
                return ScriptDataEscapedDouble(src.Next);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return HtmlToken.EOF;
            }
            else
            {
                EnqueueToken(HtmlToken.Character(c));
                return ScriptDataEscapedDouble(src.Next);
            }
        }

        /// <summary>
        /// See 8.2.4.31 Script data double escaped dash dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedDoubleDashDash(Char c)
        {
            if (c == Specification.DASH)
            {
                EnqueueToken(HtmlToken.Character(Specification.DASH));
                return ScriptDataEscapedDoubleDashDash(src.Next);
            }
            else if (c == Specification.LT)
            {
                EnqueueToken(HtmlToken.Character(Specification.LT));
                return ScriptDataEscapedDoubleLT(src.Next);
            }
            else if (c == Specification.GT)
            {
                return HtmlToken.Character(Specification.GT);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                EnqueueToken(HtmlToken.Character(Specification.REPLACEMENT));
                return ScriptDataEscapedDouble(src.Next);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return HtmlToken.EOF;
            }

            EnqueueToken(HtmlToken.Character(c));
            return ScriptDataEscapedDouble(src.Next);
        }

        /// <summary>
        /// See 8.2.4.32 Script data double escaped less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedDoubleLT(Char c)
        {
            if (c == Specification.SOLIDUS)
            {
                stringBuffer.Clear();
                EnqueueToken(HtmlToken.Character(Specification.SOLIDUS));
                return ScriptDataEndDoubleEscape(src.Next);
            }

            return ScriptDataEscapedDouble(c);
        }

        /// <summary>
        /// See 8.2.4.33 Script data double escape end state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEndDoubleEscape(Char c)
        {
            if (Specification.IsSpaceCharacter(c) || c == Specification.SOLIDUS || c == Specification.GT)
            {
                if (stringBuffer.ToString().Equals("script", StringComparison.OrdinalIgnoreCase))
                    return ScriptDataEscaped(src.Next);

                EnqueueToken(HtmlToken.Character(c));
                return ScriptDataEscapedDouble(src.Next);
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                stringBuffer.Append(c.ToLower());
                EnqueueToken(HtmlToken.Character(c));
                return ScriptDataEndDoubleEscape(src.Next);
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                stringBuffer.Append(c);
                EnqueueToken(HtmlToken.Character(c));
                return ScriptDataEndDoubleEscape(src.Next);
            }

            return ScriptDataEscapedDouble(c);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Enqueues a token to be emitted as soon as possible.
        /// </summary>
        /// <param name="token">The token to queue.</param>
        [DebuggerStepThrough]
        void EnqueueToken(HtmlToken token)
        {
            buffered = true;
            tokenBuffer.Enqueue(token);
        }

        /// <summary>
        /// Dequeues a token which has been saved in the buffer.
        /// </summary>
        /// <returns>The dequeued token.</returns>
        [DebuggerStepThrough]
        HtmlToken DequeueToken()
        {
            buffered = tokenBuffer.Count > 1;
            return tokenBuffer.Dequeue();
        }

        /// <summary>
        /// Emits the current token as a tag token.
        /// </summary>
        HtmlTagToken EmitTag(HtmlTagToken tag)
        {
            model = HtmlParseMode.PCData;

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

                lastStartTag = tag.Name;
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
