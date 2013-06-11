using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AngleSharp.Html
{
    /// <summary>
    /// Performs the tokenization of the source code. Follows the tokenization algorithm at:
    /// http://www.w3.org/html/wg/drafts/html/master/syntax.html
    /// </summary>
    class HtmlTokenizer
    {
        #region Members

        HtmlSource _;
        Boolean allowCdata;
        String lastStartTag;
        StringBuilder buffer;
        StringBuilder reference;
        ContentModel model;

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
        public HtmlTokenizer(HtmlSource source)
        {
            _ = source;
            model = ContentModel.PCData;
            allowCdata = true;
            buffer = new StringBuilder();
            reference = new StringBuilder();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if CDATA sections are accepted.
        /// </summary>
        public bool AcceptsCDATA
        {
            get { return allowCdata; }
            set { allowCdata = value; }
        }

        /// <summary>
        /// Gets the underlying stream.
        /// </summary>
        public HtmlSource Stream
        {
            get { return _; }
        }

        #endregion

        #region Methods

        public HtmlToken Get()
        {
            if (_.IsEnded)
                return HtmlToken.EOF;

            HtmlToken token = null;

            switch (model)
            {
                case ContentModel.PCData:
                    token = Data(_.Current);
                    break;
                case ContentModel.RCData:
                    token = RCData(_.Current);
                    break;
                case ContentModel.Plaintext:
                    token = Plaintext(_.Current);
                    break;
                case ContentModel.Rawtext:
                    token = Rawtext(_.Current);
                    break;
                case ContentModel.Script:
                    token = ScriptData(_.Current);
                    break;
            }

            _.Advance();
            return token;
        }

        /// <summary>
        /// Switches the current tokenization state
        /// to the desired content model.
        /// </summary>
        /// <param name="state">The new state.</param>
        public void Switch(ContentModel state)
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
                    var value = CharacterReference(_.Next);

                    if (value == null)
                        return HtmlToken.Character(Specification.AMPERSAND);
                    
                    return HtmlToken.Characters(value);

                case Specification.LT:
                    return TagOpen(_.Next);

                case Specification.NULL:
                    RaiseErrorOccurred(ErrorCode.NULL);
                    return Data(_.Next);

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
                    var value = CharacterReference(_.Next);

                    if (value == null)
                        return HtmlToken.Character(Specification.AMPERSAND);

                    return HtmlToken.Characters(value);

                case Specification.LT:
                    return RCDataLT(_.Next);

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
                buffer.Clear();
                return RCDataEndTag(_.Next);
            }

            return HtmlToken.Character(Specification.LT);
        }

        /// <summary>
        /// See 8.2.4.12 RCDATA end tag open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken RCDataEndTag(Char c)
        {
            if (Specification.IsUppercaseAscii(c))
            {
                buffer.Clear();
                buffer.Append(c.ToLower());
                return RCDataNameEndTag(_.Next, HtmlToken.CloseTag());
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                buffer.Clear();
                buffer.Append(c);
                return RCDataNameEndTag(_.Next, HtmlToken.CloseTag());
            }
            else
            {
                _.Back();
                return HtmlToken.Characters(Specification.LT, Specification.SOLIDUS);
            }
        }

        /// <summary>
        /// See 8.2.4.13 RCDATA end tag name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken RCDataNameEndTag(Char c, HtmlTagToken tag)
        {
            var name = buffer.ToString();
            var appropriateTag = name == lastStartTag;

            if (appropriateTag && Specification.IsSpaceCharacter(c))
            {
                tag.Name = name;
                return AttributeBeforeName(_.Next, tag);
            }
            else if (appropriateTag && c == Specification.SOLIDUS)
            {
                tag.Name = name;
                return TagSelfClosing(_.Next, tag);
            }
            else if (appropriateTag && c == Specification.GT)
            {
                tag.Name = name;
                model = ContentModel.PCData;
                return tag;
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                buffer.Append(c.ToLower());
                return RCDataNameEndTag(_.Next, tag);
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                buffer.Append(c);
                return RCDataNameEndTag(_.Next, tag);
            }
            else
            {
                _.Back();
                buffer.Insert(0, Specification.LT).Insert(1, Specification.SOLIDUS);
                return HtmlToken.Characters(buffer.ToString());
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
                    return RawtextLT(_.Next);

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
                buffer.Clear();
                return RawtextEndTag(_.Next);
            }

            _.Back();
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
                buffer.Clear();
                buffer.Append(c.ToLower());
                return RawtextNameEndTag(_.Next, HtmlToken.CloseTag());
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                buffer.Clear();
                buffer.Append(c);
                return RawtextNameEndTag(_.Next, HtmlToken.CloseTag());
            }
            else
            {
                _.Back();
                return HtmlToken.Characters(Specification.LT, Specification.SOLIDUS);
            }
        }

        /// <summary>
        /// See 8.2.4.16 RAWTEXT end tag name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken RawtextNameEndTag(Char c, HtmlTagToken tag)
        {
            var name = buffer.ToString();
            var appropriateTag = name == lastStartTag;

            if (appropriateTag && Specification.IsSpaceCharacter(c))
            {
                tag.Name = name;
                return AttributeBeforeName(_.Next, tag);
            }
            else if (appropriateTag && c == Specification.SOLIDUS)
            {
                tag.Name = name;
                return TagSelfClosing(_.Next, tag);
            }
            else if (appropriateTag && c == Specification.GT)
            {
                tag.Name = name;
                model = ContentModel.PCData;
                return tag;
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                buffer.Append(c.ToLower());
                return RawtextNameEndTag(_.Next, tag);
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                buffer.Append(c);
                return RawtextNameEndTag(_.Next, tag);
            }
            else
            {
                _.Back();
                buffer.Insert(0, Specification.LT).Insert(1, Specification.SOLIDUS);
                return HtmlToken.Characters(buffer.ToString());

            }
        }

        #endregion

        #region CDATA

        /// <summary>
        /// See 8.2.4.68 CDATA section state
        /// </summary>
        HtmlToken CData(Char c)
        {
            buffer.Clear();

            while (true)
            {
                if (c == Specification.EOF)
                {
                    _.Back();
                    break;
                }
                else if (c == ']' && _.ContinuesWith("]]>"))
                {
                    _.Advance(3);
                    break;
                }

                buffer.Append(c);
                c = _.Next;
            }

            return HtmlToken.Characters(buffer.ToString());
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
                var isHex = _.Current == 'x' || _.Current == 'X';
                _.Advance();

                if (isHex)
                {
                    _.Advance();

                    while (Specification.IsHex(_.Current))
                    {
                        nums.Add(_.Current.FromHex());
                        _.Advance();
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
                    while (Specification.IsDigit(_.Current))
                    {
                        nums.Add(_.Current.FromHex());
                        _.Advance();
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
                    _.Back();

                    if (isHex)
                        _.Back();

                    RaiseErrorOccurred(ErrorCode.CharacterReferenceWrongNumber);
                    return null;
                }

                if (_.Current != Specification.SC)
                {
                    RaiseErrorOccurred(ErrorCode.CharacterReferenceSemicolonMissing);
                    _.Back();
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
            int start = _.InsertionPoint;

            do
            {
                var chr = _.Current;

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

                _.Advance();
            }
            while (!_.IsEnded);

            reference.Clear();

            while (consumed != 0)
            {
                _.Back();
                consumed--;
            }

            if (_.Current != ';')
            {
                if (allowedCharacter != Specification.NULL)
                {
                    if (_.Current == Specification.EQ || Specification.IsAlphanumericAscii(_.Current))
                    {
                        if (_.Current == Specification.EQ)
                            RaiseErrorOccurred(ErrorCode.CharacterReferenceAttributeEqualsFound);

                        _.InsertionPoint = start;
                        return null;
                    }
                }

                _.Back();
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
                return MarkupDeclaration(_.Next);
            }
            else if (c == Specification.SOLIDUS)
            {
                return TagEnd(_.Next);
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                buffer.Clear();
                buffer.Append(c.ToLower());
                return TagName(_.Next, HtmlToken.OpenTag());
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                buffer.Clear();
                buffer.Append(c);
                return TagName(_.Next, HtmlToken.OpenTag());
            }
            else if (c == '?')
            {
                RaiseErrorOccurred(ErrorCode.BogusComment);
                return BogusComment(c);
            }
            else
            {
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
                buffer.Clear();
                buffer.Append(c.ToLower());
                return TagName(_.Next, HtmlToken.CloseTag());
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                buffer.Clear();
                buffer.Append(c);
                return TagName(_.Next, HtmlToken.CloseTag());
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return Data(_.Next);
            }
            else if (c == Specification.EOF)
            {
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
        HtmlToken TagName(Char c, HtmlTagToken tag)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                tag.Name = buffer.ToString();
                return AttributeBeforeName(_.Next, tag);
            }
            else if (c == Specification.SOLIDUS)
            {
                tag.Name = buffer.ToString();
                return TagSelfClosing(_.Next, tag);
            }
            else if (c == Specification.GT)
            {
                tag.Name = buffer.ToString();
                return tag;
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                buffer.Append(c.ToLower());
                return TagName(_.Next, tag);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Append(Specification.REPLACEMENT);
                return TagName(_.Next, tag);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return HtmlToken.EOF;
            }
            else
            {
                buffer.Append(c);
                return TagName(_.Next, tag);
            }
        }

        /// <summary>
        /// See 8.2.4.43 Self-closing start tag state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken TagSelfClosing(Char c, HtmlTagToken tag)
        {
            if (c == Specification.GT)
            {
                model = ContentModel.PCData;
                tag.IsSelfClosing = true;
                return tag;
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
            if (_.ContinuesWith("--"))
            {
                _.Advance();
                return CommentStart(_.Next);
            }
            else if (_.ContinuesWith("doctype"))
            {
                _.Advance(6);
                return Doctype(_.Next);
            }
            else if (allowCdata && _.ContinuesWith("[CDATA[", false))
            {
                _.Advance(6);
                return CData(_.Next);
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
            buffer.Clear();
            buffer.Append(_.Back(2).Current);
            buffer.Append(_.Next);
            buffer.Append(_.Next);
            _.Advance();

            while(true)
            {
                if (c == Specification.NULL)
                    buffer.Append(Specification.REPLACEMENT);
                else
                    buffer.Append(c);

                if (c == Specification.GT)
                    break;
                else if (c == Specification.EOF)
                {
                    _.Back();
                    break;
                }

                _.Advance();
                c = _.Current;
            }

            return HtmlToken.Comment(buffer.ToString());
        }

        /// <summary>
        /// See 8.2.4.46 Comment start state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken CommentStart(Char c)
        {
            buffer.Clear();

            if (c == Specification.DASH)
            {
                return CommentDashStart(_.Next);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Append(Specification.REPLACEMENT);
                return Comment(_.Next);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return HtmlToken.Comment(buffer.ToString());
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _.Back();
                return HtmlToken.Comment(buffer.ToString());
            }
            else
            {
                buffer.Append(c);
                return Comment(_.Next);
            }
        }

        /// <summary>
        /// See 8.2.4.47 Comment start dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken CommentDashStart(Char c)
        {
            if (c == Specification.DASH)
            {
                return CommentEnd(_.Next);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Append(Specification.DASH);
                buffer.Append(Specification.REPLACEMENT);
                return Comment(_.Next);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return HtmlToken.Comment(buffer.ToString());
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _.Back();
                return HtmlToken.Comment(buffer.ToString());
            }
            else
            {
                buffer.Append(Specification.DASH);
                buffer.Append(c);
                return Comment(_.Next);
            }
        }

        /// <summary>
        /// See 8.2.4.48 Comment state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken Comment(Char c)
        {
            if (c == Specification.DASH)
            {
                return CommentDashEnd(_.Next);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _.Back();
                return HtmlToken.Comment(buffer.ToString());
            }
            else
            {
                if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    c = Specification.REPLACEMENT;
                }

                buffer.Append(c);
                return Comment(_.Next);
            }
        }

        /// <summary>
        /// See 8.2.4.49 Comment end dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken CommentDashEnd(Char c)
        {
            if (c == Specification.DASH)
            {
                return CommentEnd(_.Next);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _.Back();
                return HtmlToken.Comment(buffer.ToString());
            }
            else
            {
                if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    c = Specification.REPLACEMENT;
                }

                buffer.Append(Specification.DASH);
                buffer.Append(c);
                return Comment(_.Next);
            }
        }

        /// <summary>
        /// See 8.2.4.50 Comment end state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken CommentEnd(Char c)
        {
            if (c == Specification.GT)
            {
                return HtmlToken.Comment(buffer.ToString());
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                buffer.Append(Specification.DASH);
                buffer.Append(Specification.REPLACEMENT);
                return Comment(_.Next);
            }
            else if (c == Specification.EM)
            {
                RaiseErrorOccurred(ErrorCode.CommentEndedWithEM);
                return CommentBangEnd(_.Next);
            }
            else if (c == Specification.DASH)
            {
                RaiseErrorOccurred(ErrorCode.CommentEndedWithDash);
                buffer.Append(Specification.DASH);
                return CommentEnd(_.Next);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _.Back();
                return HtmlToken.Comment(buffer.ToString());
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.CommentEndedUnexpected);
                buffer.Append(Specification.DASH);
                buffer.Append(Specification.DASH);
                buffer.Append(c);
                return Comment(_.Next);
            }
        }

        /// <summary>
        /// See 8.2.4.51 Comment end bang state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken CommentBangEnd(Char c)
        {
            if (c == Specification.DASH)
            {
                buffer.Append(Specification.DASH);
                buffer.Append(Specification.DASH);
                buffer.Append(Specification.EM);
                return CommentDashEnd(_.Next);
            }
            else if (c == Specification.GT)
            {
                return HtmlToken.Comment(buffer.ToString());
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Append(Specification.DASH);
                buffer.Append(Specification.DASH);
                buffer.Append(Specification.EM);
                buffer.Append(Specification.REPLACEMENT);
                return Comment(_.Next);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _.Back();
                return HtmlToken.Comment(buffer.ToString());
            }
            else
            {
                buffer.Append(Specification.DASH);
                buffer.Append(Specification.DASH);
                buffer.Append(Specification.EM);
                buffer.Append(c);
                return Comment(_.Next);
            }
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
            {
                return DoctypeNameBefore(_.Next);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _.Back();
                return HtmlToken.Doctype(true);
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.DoctypeUnexpected);
                return DoctypeNameBefore(c);
            }
        }

        /// <summary>
        /// See 8.2.4.53 Before DOCTYPE name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken DoctypeNameBefore(Char c)
        {
            while (Specification.IsSpaceCharacter(c))
                c = _.Next;

            if (Specification.IsUppercaseAscii(c))
            {
                buffer.Clear();
                buffer.Append(c.ToLower());
                return DoctypeName(_.Next, HtmlToken.Doctype(false));
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Clear();
                buffer.Append(Specification.REPLACEMENT);
                return DoctypeName(_.Next, HtmlToken.Doctype(false));
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return HtmlToken.Doctype(true);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _.Back();
                return HtmlToken.Doctype(true);
            }
            else
            {
                buffer.Clear();
                buffer.Append(c);
                return DoctypeName(_.Next, HtmlToken.Doctype(false));
            }
        }

        /// <summary>
        /// See 8.2.4.54 DOCTYPE name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken DoctypeName(Char c, HtmlDoctypeToken doctype)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                doctype.Name = buffer.ToString();
                return DoctypeNameAfter(_.Next, doctype);
            }
            else if (c == Specification.GT)
            {
                doctype.Name = buffer.ToString();
                return doctype;
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                buffer.Append(c.ToLower());
                return DoctypeName(_.Next, doctype);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Append(Specification.REPLACEMENT);
                return DoctypeName(_.Next, doctype);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _.Back();
                doctype.IsQuirksForced = true;
                doctype.Name = buffer.ToString();
                return doctype;
            }
            else
            {
                buffer.Append(c);
                return DoctypeName(c, doctype);
            }
        }

        /// <summary>
        /// See 8.2.4.55 After DOCTYPE name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken DoctypeNameAfter(Char c, HtmlDoctypeToken doctype)
        {
            while (Specification.IsSpaceCharacter(c))
                c = _.Next;

            if (c == Specification.GT)
            {
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _.Back();
                doctype.IsQuirksForced = true;
                return doctype;
            }
            else if (_.ContinuesWith("public"))
            {
                _.Advance(5);
                return DoctypePublic(_.Next, doctype);
            }
            else if (_.ContinuesWith("system"))
            {
                _.Advance(5);
                return DoctypeSystem(_.Next, doctype);
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.DoctypeUnexpectedAfterName);
                doctype.IsQuirksForced = true;
                return BogusDoctype(_.Next, doctype);
            }
        }

        /// <summary>
        /// See 8.2.4.56 After DOCTYPE public keyword state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken DoctypePublic(Char c, HtmlDoctypeToken doctype)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                return DoctypePublicIdentifierBefore(_.Next, doctype);
            }
            else if (c == Specification.DQ)
            {
                RaiseErrorOccurred(ErrorCode.DoubleQuotationMarkUnexpected);
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierDoubleQuoted(_.Next, doctype);
            }
            else if (c == Specification.SQ)
            {
                RaiseErrorOccurred(ErrorCode.SingleQuotationMarkUnexpected);
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierSingleQuoted(_.Next, doctype);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.IsQuirksForced = true;
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                _.Back();
                return doctype;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.DoctypePublicInvalid);
                doctype.IsQuirksForced = true;
                return BogusDoctype(_.Next, doctype);
            }
        }

        /// <summary>
        /// See 8.2.4.57 Before DOCTYPE public identifier state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken DoctypePublicIdentifierBefore(Char c, HtmlDoctypeToken doctype)
        {
            while (Specification.IsSpaceCharacter(c))
                c = _.Next;

            if (c == Specification.DQ)
            {
                buffer.Clear();
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierDoubleQuoted(_.Next, doctype);
            }
            else if (c == Specification.SQ)
            {
                buffer.Clear();
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierSingleQuoted(_.Next, doctype);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.IsQuirksForced = true;
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                _.Back();
                return doctype;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.DoctypePublicInvalid);
                doctype.IsQuirksForced = true;
                return BogusDoctype(_.Next, doctype);
            }
        }

        /// <summary>
        /// See 8.2.4.58 DOCTYPE public identifier (double-quoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken DoctypePublicIdentifierDoubleQuoted(Char c, HtmlDoctypeToken doctype)
        {
            if (c == Specification.DQ)
            {
                doctype.PublicIdentifier = buffer.ToString();
                return DoctypePublicIdentifierAfter(_.Next, doctype); ;
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Append(Specification.REPLACEMENT);
                return DoctypePublicIdentifierDoubleQuoted(_.Next, doctype);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.IsQuirksForced = true;
                doctype.PublicIdentifier = buffer.ToString();
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _.Back();
                doctype.IsQuirksForced = true;
                doctype.PublicIdentifier = buffer.ToString();
                return doctype;
            }
            else
            {
                buffer.Append(c);
                return DoctypePublicIdentifierDoubleQuoted(_.Next, doctype);
            }
        }

        /// <summary>
        /// See 8.2.4.59 DOCTYPE public identifier (single-quoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken DoctypePublicIdentifierSingleQuoted(Char c, HtmlDoctypeToken doctype)
        {
            if (c == Specification.SQ)
            {
                doctype.PublicIdentifier = buffer.ToString();
                return DoctypePublicIdentifierAfter(_.Next, doctype);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Append(Specification.REPLACEMENT);
                return DoctypePublicIdentifierSingleQuoted(_.Next, doctype);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.IsQuirksForced = true;
                doctype.PublicIdentifier = buffer.ToString();
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                doctype.PublicIdentifier = buffer.ToString();
                _.Back();
                return doctype;
            }
            else
            {
                buffer.Append(c);
                return DoctypePublicIdentifierSingleQuoted(_.Next, doctype);
            }
        }

        /// <summary>
        /// See 8.2.4.60 After DOCTYPE public identifier state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken DoctypePublicIdentifierAfter(Char c, HtmlDoctypeToken doctype)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                buffer.Clear();
                return DoctypeBetween(_.Next, doctype);
            }
            else if (c == Specification.GT)
            {
                return doctype;
            }
            else if (c == Specification.DQ)
            {
                RaiseErrorOccurred(ErrorCode.DoubleQuotationMarkUnexpected);
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(_.Next, doctype);
            }
            else if (c == Specification.SQ)
            {
                RaiseErrorOccurred(ErrorCode.SingleQuotationMarkUnexpected);
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierSingleQuoted(_.Next, doctype);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                _.Back();
                return doctype;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
                doctype.IsQuirksForced = true;
                return BogusDoctype(_.Next, doctype);
            }
        }

        /// <summary>
        /// See 8.2.4.61 Between DOCTYPE public and system identifiers state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken DoctypeBetween(Char c, HtmlDoctypeToken doctype)
        {
            while (Specification.IsSpaceCharacter(c))
                c = _.Next;

            if (c == Specification.GT)
            {
                return doctype;
            }
            else if (c == Specification.DQ)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(_.Next, doctype);
            }
            else if (c == Specification.SQ)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierSingleQuoted(_.Next, doctype);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                _.Back();
                return doctype;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
                doctype.IsQuirksForced = true;
                return BogusDoctype(_.Next, doctype);
            }
        }

        /// <summary>
        /// See 8.2.4.62 After DOCTYPE system keyword state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken DoctypeSystem(Char c, HtmlDoctypeToken doctype)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                return DoctypeSystemIdentifierBefore(_.Next, doctype);
            }
            else if (c == Specification.DQ)
            {
                RaiseErrorOccurred(ErrorCode.DoubleQuotationMarkUnexpected);
                doctype.SystemIdentifier = string.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(_.Next, doctype);
            }
            else if (c == Specification.SQ)
            {
                RaiseErrorOccurred(ErrorCode.SingleQuotationMarkUnexpected);
                doctype.SystemIdentifier = string.Empty;
                return DoctypeSystemIdentifierSingleQuoted(_.Next, doctype);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.SystemIdentifier = buffer.ToString();
                doctype.IsQuirksForced = true;
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                _.Back();
                return doctype;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.DoctypeSystemInvalid);
                doctype.IsQuirksForced = true;
                return BogusDoctype(_.Next, doctype);
            }
        }

        /// <summary>
        /// See 8.2.4.63 Before DOCTYPE system identifier state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken DoctypeSystemIdentifierBefore(Char c, HtmlDoctypeToken doctype)
        {
            while (Specification.IsSpaceCharacter(c))
                c = _.Next;

            if (c == Specification.DQ)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(_.Next, doctype);
            }
            else if (c == Specification.SQ)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierSingleQuoted(_.Next, doctype);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = buffer.ToString();
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = buffer.ToString();
                _.Back();
                return doctype;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
                doctype.IsQuirksForced = true;
                return BogusDoctype(_.Next, doctype);
            }
        }

        /// <summary>
        /// See 8.2.4.64 DOCTYPE system identifier (double-quoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken DoctypeSystemIdentifierDoubleQuoted(Char c, HtmlDoctypeToken doctype)
        {
            if (c == Specification.DQ)
            {
                doctype.SystemIdentifier = buffer.ToString();
                return DoctypeSystemIdentifierAfter(_.Next, doctype);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Append(Specification.REPLACEMENT);
                return DoctypeSystemIdentifierDoubleQuoted(_.Next, doctype);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = buffer.ToString();
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = buffer.ToString();
                _.Back();
                return doctype;
            }
            else
            {
                buffer.Append(c);
                return DoctypeSystemIdentifierDoubleQuoted(_.Next, doctype);
            }
        }

        /// <summary>
        /// See 8.2.4.65 DOCTYPE system identifier (single-quoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken DoctypeSystemIdentifierSingleQuoted(Char c, HtmlDoctypeToken doctype)
        {
            if (c == Specification.SQ)
            {
                doctype.SystemIdentifier = buffer.ToString();
                return DoctypeSystemIdentifierAfter(_.Next, doctype);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Append(Specification.REPLACEMENT);
                return DoctypeSystemIdentifierSingleQuoted(_.Next, doctype);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = buffer.ToString();
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = buffer.ToString();
                _.Back();
                return doctype;
            }
            else
            {
                buffer.Append(c);
                return DoctypeSystemIdentifierSingleQuoted(_.Next, doctype);
            }
        }

        /// <summary>
        /// See 8.2.4.66 After DOCTYPE system identifier state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken DoctypeSystemIdentifierAfter(Char c, HtmlDoctypeToken doctype)
        {
            while (Specification.IsSpaceCharacter(c))
                c = _.Next;

            if (c == Specification.GT)
            {
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.IsQuirksForced = true;
                _.Back();
                return doctype;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
                return BogusDoctype(_.Next, doctype);
            }
        }

        /// <summary>
        /// See 8.2.4.67 Bogus DOCTYPE state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken BogusDoctype(Char c, HtmlDoctypeToken doctype)
        {
            while (true)
            {
                if (c == Specification.EOF)
                {
                    _.Back();
                    return doctype;
                }
                else if (c == Specification.GT)
                    return doctype;

                c = _.Next;
            }
        }

        #endregion

        #region Attributes

        /// <summary>
        /// See 8.2.4.34 Before attribute name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken AttributeBeforeName(Char c, HtmlTagToken tag)
        {
            while (Specification.IsSpaceCharacter(c))
                c = _.Next;

            if (c == Specification.SOLIDUS)
            {
                return TagSelfClosing(_.Next, tag);
            }
            else if (c == Specification.GT)
            {
                return EmitTag(tag);
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                buffer.Clear();
                buffer.Append(c.ToLower());
                return AttributeName(_.Next, tag);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Clear();
                buffer.Append(Specification.REPLACEMENT);
                return AttributeName(_.Next, tag);
            }
            else if (c == Specification.SQ || c == Specification.DQ || c == Specification.EQ || c == Specification.LT)
            {
                RaiseErrorOccurred(ErrorCode.AttributeNameInvalid);
                buffer.Clear();
                buffer.Append(c);
                return AttributeName(_.Next, tag);
            }
            else if (c == Specification.EOF)
            {
                return HtmlToken.EOF;
            }
            else
            {
                buffer.Clear();
                buffer.Append(c);
                return AttributeName(_.Next, tag);
            }
        }

        /// <summary>
        /// See 8.2.4.35 Attribute name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken AttributeName(Char c, HtmlTagToken tag)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                tag.AddAttribute(buffer.ToString());
                return AttributeAfterName(_.Next, tag);
            }
            else if (c == Specification.SOLIDUS)
            {
                tag.AddAttribute(buffer.ToString());
                return TagSelfClosing(_.Next, tag);
            }
            else if (c == Specification.EQ)
            {
                tag.AddAttribute(buffer.ToString());
                return AttributeBeforeValue(_.Next, tag);
            }
            else if (c == Specification.GT)
            {
                tag.AddAttribute(buffer.ToString());
                return EmitTag(tag);
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                buffer.Append(c.ToLower());
                return AttributeName(_.Next, tag);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Append(Specification.REPLACEMENT);
                return AttributeName(_.Next, tag);
            }
            else if (c == Specification.DQ || c == Specification.SQ || c == Specification.LT)
            {
                RaiseErrorOccurred(ErrorCode.AttributeNameInvalid);
                buffer.Append(c);
                return AttributeName(_.Next, tag);
            }
            else if (c == Specification.EOF)
            {
                return HtmlToken.EOF;
            }
            else
            {
                buffer.Append(c);
                return AttributeName(_.Next, tag);
            }
        }

        /// <summary>
        /// See 8.2.4.36 After attribute name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken AttributeAfterName(Char c, HtmlTagToken tag)
        {
            while (Specification.IsSpaceCharacter(c))
                c = _.Next;

            if (c == Specification.SOLIDUS)
            {
                return TagSelfClosing(_.Next, tag);
            }
            else if (c == Specification.EQ)
            {
                return AttributeBeforeValue(_.Next, tag);
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                buffer.Clear();
                buffer.Append(c.ToLower());
                return AttributeName(_.Next, tag);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Clear();
                buffer.Append(Specification.REPLACEMENT);
                return AttributeName(_.Next, tag);
            }
            else if (c == Specification.DQ || c == Specification.SQ || c == Specification.LT)
            {
                RaiseErrorOccurred(ErrorCode.AttributeNameInvalid);
                buffer.Clear();
                buffer.Append(c);
                return AttributeName(_.Next, tag);
            }
            else if (c == Specification.EOF)
            {
                return HtmlToken.EOF;
            }
            else
            {
                buffer.Clear();
                buffer.Append(c);
                return AttributeName(_.Next, tag);
            }
        }

        /// <summary>
        /// See 8.2.4.37 Before attribute value state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken AttributeBeforeValue(Char c, HtmlTagToken tag)
        {
            while (Specification.IsSpaceCharacter(c))
                c = _.Next;

            if (c == Specification.DQ)
            {
                buffer.Clear();
                return AttributeDoubleQuotedValue(_.Next, tag);
            }
            else if (c == Specification.AMPERSAND)
            {
                buffer.Clear();
                return AttributeUnquotedValue(c, tag);
            }
            else if (c == Specification.SQ)
            {
                buffer.Clear();
                return AttributeSingleQuotedValue(_.Next, tag);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Append(Specification.REPLACEMENT);
                return AttributeUnquotedValue(_.Next, tag);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return EmitTag(tag);
            }
            else if (c == Specification.LT || c == Specification.EQ || c == Specification.CQ)
            {
                RaiseErrorOccurred(ErrorCode.AttributeValueInvalid);
                buffer.Clear().Append(c);
                return AttributeUnquotedValue(_.Next, tag);
            }
            else if (c == Specification.EOF)
            {
                return HtmlToken.EOF;
            }
            else
            {
                buffer.Clear().Append(c);
                return AttributeUnquotedValue(_.Next, tag);
            }
        }

        /// <summary>
        /// See 8.2.4.38 Attribute value (double-quoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken AttributeDoubleQuotedValue(Char c, HtmlTagToken tag)
        {
            if (c == Specification.DQ)
            {
                tag.SetAttributeValue(buffer.ToString());
                return AttributeAfterValue(_.Next, tag);
            }
            else if (c == Specification.AMPERSAND)
            {
                var value = CharacterReference(_.Next, Specification.DQ);

                if (value == null)
                    buffer.Append(Specification.AMPERSAND);
                else
                    buffer.Append(value);

                return AttributeDoubleQuotedValue(_.Next, tag);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Append(Specification.REPLACEMENT);
                return AttributeDoubleQuotedValue(_.Next, tag);
            }
            else if (c == Specification.EOF)
            {
                return HtmlToken.EOF;
            }
            else
            {
                buffer.Append(c);
                return AttributeDoubleQuotedValue(_.Next, tag);
            }
        }

        /// <summary>
        /// See 8.2.4.39 Attribute value (single-quoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken AttributeSingleQuotedValue(Char c, HtmlTagToken tag)
        {
            if (c == Specification.SQ)
            {
                tag.SetAttributeValue(buffer.ToString());
                return AttributeAfterValue(_.Next, tag);
            }
            else if (c == Specification.AMPERSAND)
            {
                var value = CharacterReference(_.Next, Specification.SQ);

                if (value == null)
                    buffer.Append(Specification.AMPERSAND);
                else
                    buffer.Append(value);

                return AttributeSingleQuotedValue(_.Next, tag);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Append(Specification.REPLACEMENT);
                return AttributeSingleQuotedValue(_.Next, tag);
            }
            else if (c == Specification.EOF)
            {
                return HtmlToken.EOF;
            }
            else
            {
                buffer.Append(c);
                return AttributeSingleQuotedValue(_.Next, tag);
            }
        }

        /// <summary>
        /// See 8.2.4.40 Attribute value (unquoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken AttributeUnquotedValue(Char c, HtmlTagToken tag)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                tag.SetAttributeValue(buffer.ToString());
                return AttributeBeforeName(_.Next, tag);
            }
            else if (c == Specification.AMPERSAND)
            {
                var value = CharacterReference(_.Next, Specification.GT);

                if (value == null)
                    value = new char[] { Specification.AMPERSAND };

                tag.SetAttributeValue(new string(value));
                return AttributeAfterValue(_.Next, tag);
            }
            else if (c == Specification.GT)
            {
                tag.SetAttributeValue(buffer.ToString());
                return EmitTag(tag);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Append(Specification.REPLACEMENT);
                return AttributeUnquotedValue(_.Next, tag);
            }
            else if (c == Specification.DQ || c == Specification.SQ || c == Specification.LT || c == Specification.EQ || c == Specification.CQ)
            {
                RaiseErrorOccurred(ErrorCode.AttributeValueInvalid);
                buffer.Append(c);
                return AttributeUnquotedValue(_.Next, tag);
            }
            else if (c == Specification.EOF)
            {
                return HtmlToken.EOF;
            }
            else
            {
                buffer.Append(c);
                return AttributeUnquotedValue(_.Next, tag);
            }
        }

        /// <summary>
        /// See 8.2.4.42 After attribute value (quoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken AttributeAfterValue(Char c, HtmlTagToken tag)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                return AttributeBeforeName(_.Next, tag);
            }
            else if (c == Specification.SOLIDUS)
            {
                return TagSelfClosing(_.Next, tag);
            }
            else if (c == Specification.GT)
            {
                return EmitTag(tag);
            }
            else if (c == Specification.EOF)
            {
                return HtmlTagToken.EOF;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.AttributeNameExpected);
                return AttributeBeforeName(c, tag);
            }
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
                    return HtmlToken.EOF;//TODO ScriptDataLT(_.Next);

                case Specification.NULL:
                    RaiseErrorOccurred(ErrorCode.NULL);
                    return HtmlToken.Character(Specification.REPLACEMENT);

                case Specification.EOF:
                    return HtmlToken.EOF;

                default:
                    return HtmlToken.Character(c);
            }
        }
        /*
        /// <summary>
        /// See 8.2.4.17 Script data less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataLT(Char c)
        {
            if (c == Specification.SOLIDUS)
            {
                return ScriptDataEndTag(_.Next);
            }
            else if (c == Specification.EM)
            {
                RaiseTokenEmitted(HtmlToken.Character(Specification.LT));
                RaiseTokenEmitted(HtmlToken.Character(Specification.EM));
                return ScriptDataStartEscape(_.Next);
            }
            else
            {
                _.Back();
                return HtmlToken.Character(Specification.LT);
            }
        }

        /// <summary>
        /// See 8.2.4.18 Script data end tag open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEndTag(Char c)
        {
            if (Specification.IsUppercaseAscii(c))
            {
                buffer.Clear();
                buffer.Append(c.ToLower());
                return ScriptDataNameEndTag(_.Next, HtmlToken.CloseTag());
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                buffer.Clear();
                buffer.Append(c);
                return ScriptDataNameEndTag(_.Next, HtmlToken.CloseTag());
            }
            else
            {
                _.Back();
                return HtmlToken.Characters(Specification.LT, Specification.SOLIDUS);
            }
        }

        /// <summary>
        /// See 8.2.4.19 Script data end tag name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataNameEndTag(Char c, HtmlTagToken tag)
        {
            var name = buffer.ToString();
            var appropriateEndTag = name == lastStartTag;

            if (appropriateEndTag && Specification.IsSpaceCharacter(c))
            {
                tag.Name = name;
                return AttributeBeforeName(_.Next, tag);
            }
            else if (appropriateEndTag && c == Specification.SOLIDUS)
            {
                tag.Name = name;
                return TagSelfClosing(_.Next, tag);
            }
            else if (appropriateEndTag && c == Specification.GT)
            {
                tag.Name = name;
                model = ContentModel.PCData;
                return EmitTag(tag);
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                buffer.Append(c.ToLower());
                return ScriptDataNameEndTag(_.Next, tag);
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                buffer.Append(c);
                return ScriptDataNameEndTag(_.Next, tag);
            }
            else
            {
                _.Back();
                buffer.Insert(0, Specification.LT).Insert(1, Specification.SOLIDUS);
                return HtmlToken.Characters(buffer.ToString());
            }
        }

        /// <summary>
        /// See 8.2.4.20 Script data escape start state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataStartEscape(Char c)
        {
            if (c == Specification.DASH)
            {
                state = ScriptDataStartEscapeDash;
                RaiseTokenEmitted(HtmlToken.Character(Specification.DASH));
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
                state = ScriptDataEscapedDash;
                RaiseTokenEmitted(HtmlToken.Character(Specification.DASH));
            }
            else if (c == Specification.LT)
            {
                return ScriptDataEscapedLT(_.Next);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                return HtmlToken.Character(Specification.REPLACEMENT);
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
                state = ScriptDataEscapedDashDash;
                RaiseTokenEmitted(HtmlToken.Character(Specification.DASH));
            }

            return ScriptData(c);
        }

        /// <summary>
        /// See 8.2.4.23 Script data escaped dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedDash(char c)
        {
            if (c == Specification.DASH)
            {
                state = ScriptDataEscapedDashDash;
                RaiseTokenEmitted(HtmlToken.Character(Specification.DASH));
            }
            else if (c == Specification.LT)
            {
                return ScriptDataEscapedLT(_.Next);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                state = ScriptDataEscaped;
                RaiseTokenEmitted(HtmlToken.Character(Specification.REPLACEMENT));
            }
            else if (c == Specification.EOF)
            {
                return HtmlToken.EOF;
            }
            else
            {
                state = ScriptDataEscaped;
                RaiseTokenEmitted(HtmlToken.Character(c));
            }
        }

        /// <summary>
        /// See 8.2.4.24 Script data escaped dash dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedDashDash(Char c)
        {
            if (c == Specification.DASH)
            {
                state = ScriptDataEscapedDashDash;
                RaiseTokenEmitted(HtmlToken.Character(Specification.DASH));
            }
            else if (c == Specification.LT)
            {
                return ScriptDataEscapedLT(_.Next);
            }
            else if (c == Specification.GT)
            {
                return HtmlToken.Character(Specification.GT);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                state = ScriptDataEscaped;
                RaiseTokenEmitted(HtmlToken.Character(Specification.REPLACEMENT));
            }
            else if (c == Specification.EOF)
            {
                return HtmlToken.EOF;
            }
            else
            {
                state = ScriptDataEscaped;
                RaiseTokenEmitted(HtmlToken.Character(c));
            }
        }

        /// <summary>
        /// See 8.2.4.25 Script data escaped less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedLT(Char c)
        {
            if (c == Specification.SOLIDUS)
            {
                return ScriptDataEndTag(_.Next);
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                buffer.Clear();
                buffer.Append(c.ToLower());
                RaiseTokenEmitted(HtmlToken.Character(Specification.LT));
                RaiseTokenEmitted(HtmlToken.Character(c));
                state = ScriptDataStartDoubleEscape;
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                buffer.Clear();
                buffer.Clear();
                buffer.Append(c);
                RaiseTokenEmitted(HtmlToken.Character(Specification.LT));
                RaiseTokenEmitted(HtmlToken.Character(c));
                state = ScriptDataStartDoubleEscape;
            }
            else
            {
                state = ScriptDataEscaped;
                RaiseTokenEmitted(HtmlToken.Character(Specification.LT));
                _.Back();
            }
        }

        /// <summary>
        /// See 8.2.4.26 Script data escaped end tag open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedEndTag(Char c)
        {
            if (Specification.IsUppercaseAscii(c))
            {
                buffer.Clear();
                buffer.Append(c.ToLower());
                return ScriptDataEscapedEndTag(_.Next, HtmlToken.CloseTag());
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                buffer.Clear();
                buffer.Append(c);
                return ScriptDataEscapedEndTag(_.Next, HtmlToken.CloseTag());
            }
            else
            {
                state = ScriptDataEscaped;
                RaiseTokenEmitted(HtmlToken.Character(Specification.LT));
                RaiseTokenEmitted(HtmlToken.Character(Specification.SOLIDUS));
                _.Back();
            }
        }

        /// <summary>
        /// See 8.2.4.27 Script data escaped end tag name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataEscapedNameTag(Char c, HtmlTagToken tag)
        {
            var name = buffer.ToString();
            var appropriateEndTag = name == lastStartTag;

            if (appropriateEndTag && Specification.IsSpaceCharacter(c))
            {
                tag.Name = name;
                return AttributeBeforeName(_.Next, tag);
            }
            else if (appropriateEndTag && c == Specification.SOLIDUS)
            {
                tag.Name = name;
                return TagSelfClosing(_.Next, tag);
            }
            else if (appropriateEndTag && c == Specification.GT)
            {
                tag.Name = name;
                model = ContentModel.PCData;
                EmitTag(tag);
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                buffer.Append(c.ToLower());
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                buffer.Append(c);
            }
            else
            {
                state = ScriptDataEscaped;
                token = null;
                RaiseTokenEmitted(HtmlToken.Character(Specification.LT));
                RaiseTokenEmitted(HtmlToken.Character(Specification.SOLIDUS));

                for (var i = 0; i < buffer.Length; i++)
                    RaiseTokenEmitted(HtmlToken.Character(buffer[i]));

                _.Back();
            }
        }

        /// <summary>
        /// See 8.2.4.28 Script data double escape start state
        /// </summary>
        /// <param name="c">The next input character.</param>
        HtmlToken ScriptDataStartDoubleEscape(char c)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                if (buffer.ToString() == "script")
                    return ScriptDataEscapedDouble(_.Next);
                else
                {
                    state = ScriptDataEscaped;
                    RaiseTokenEmitted(HtmlToken.Character(c));
                }
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                buffer.Append(c.ToLower());
                return HtmlToken.Character(c);
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                buffer.Append(c);
                return HtmlToken.Character(c);
            }
            else
            {
                return ScriptDataEscaped(c);
            }
        }

        /// <summary>
        /// See 8.2.4.29 Script data double escaped state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void ScriptDataEscapedDouble(char c)
        {
            if (c == Specification.DASH)
            {
                state = ScriptDataEscapedDoubleDash;
                RaiseTokenEmitted(HtmlToken.Character(Specification.DASH));
            }
            else if (c == Specification.LT)
            {
                state = ScriptDataEscapedDoubleLT;
                RaiseTokenEmitted(HtmlToken.Character(Specification.LT));
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                RaiseTokenEmitted(HtmlToken.Character(Specification.REPLACEMENT));
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
            }
            else
            {
                RaiseTokenEmitted(HtmlToken.Character(c));
            }
        }

        /// <summary>
        /// See 8.2.4.30 Script data double escaped dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void ScriptDataEscapedDoubleDash(char c)
        {
            if (c == Specification.DASH)
            {
                state = ScriptDataEscapedDoubleDashDash;
                RaiseTokenEmitted(HtmlToken.Character(Specification.DASH));
            }
            else if (c == Specification.LT)
            {
                state = ScriptDataEscapedDoubleLT;
                RaiseTokenEmitted(HtmlToken.Character(Specification.LT));
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                state = ScriptDataEscapedDouble;
                RaiseTokenEmitted(HtmlToken.Character(Specification.REPLACEMENT));
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
            }
            else
            {
                state = ScriptDataEscapedDouble;
                RaiseTokenEmitted(HtmlToken.Character(c));
            }
        }

        /// <summary>
        /// See 8.2.4.31 Script data double escaped dash dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void ScriptDataEscapedDoubleDashDash(char c)
        {
            if (c == Specification.DASH)
            {
                RaiseTokenEmitted(HtmlToken.Character(Specification.DASH));
            }
            else if (c == Specification.LT)
            {
                state = ScriptDataEscapedDoubleLT;
                RaiseTokenEmitted(HtmlToken.Character(Specification.LT));
            }
            else if (c == Specification.GT)
            {
                state = ScriptData;
                RaiseTokenEmitted(HtmlToken.Character(Specification.GT));
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                state = ScriptDataEscapedDouble;
                RaiseTokenEmitted(HtmlToken.Character(Specification.REPLACEMENT));
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
            }
            else
            {
                state = ScriptDataEscapedDouble;
                RaiseTokenEmitted(HtmlToken.Character(c));
            }
        }

        /// <summary>
        /// See 8.2.4.32 Script data double escaped less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void ScriptDataEscapedDoubleLT(char c)
        {
            if (c == Specification.SOLIDUS)
            {
                buffer.Clear();
                state = ScriptDataEndDoubleEscape;
                RaiseTokenEmitted(HtmlToken.Character(Specification.SOLIDUS));
            }
            else
            {
                state = ScriptDataEscapedDouble;
                _.Back();
            }
        }

        /// <summary>
        /// See 8.2.4.33 Script data double escape end state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void ScriptDataEndDoubleEscape(char c)
        {
            if (Specification.IsSpaceCharacter(c) || c == Specification.SOLIDUS || c == Specification.GT)
            {
                if (buffer.ToString() == "script")
                    state = ScriptDataEscaped;
                else
                {
                    state = ScriptDataEscapedDouble;
                    RaiseTokenEmitted(HtmlToken.Character(c));
                }
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                buffer.Append(c.ToLower());
                RaiseTokenEmitted(HtmlToken.Character(c));
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                buffer.Append(c);
                RaiseTokenEmitted(HtmlToken.Character(c));
            }
            else
            {
                state = ScriptDataEscapedDouble;
                _.Back();
            }
        }*/

        #endregion

        #region Helpers

        /// <summary>
        /// Emits the current token as a tag token.
        /// </summary>
        HtmlTagToken EmitTag(HtmlTagToken tag)
        {
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
                pck.Line = _.Line;
                pck.Column = _.Column;
                ErrorOccurred(this, pck);
            }
        }

        #endregion
    }
}
