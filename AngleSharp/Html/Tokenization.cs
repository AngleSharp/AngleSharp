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
    class Tokenization
    {
        #region Members

        HtmlSource _;
        bool pause;
        Action<char> state;
        HtmlToken token;
        string lastStartTag;
        StringBuilder buffer;

        #endregion

        #region Events

        /// <summary>
        /// The event will be fired once an error has been detected.
        /// </summary>
        public event EventHandler<ParseErrorEventArgs> ErrorOccurred;

        /// <summary>
        /// The event will be fired when a token is emitted.
        /// </summary>
        public event EventHandler<TokenEventArgs> TokenEmitted;

        #endregion

        #region ctor

        /// <summary>
        /// See 8.2.4 Tokenization
        /// </summary>
        /// <param name="source">The source code manager.</param>
        public Tokenization(HtmlSource source)
        {
            _ = source;
            IsCurrentNodeNotInHtmlNS = true;
            state = Data;
            buffer = new StringBuilder();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if the invocation should be blocked.
        /// </summary>
        public bool Block { get; set; }

        /// <summary>
        /// Gets or sets if the current node is not in the HTML
        /// namespace.
        /// </summary>
        public bool IsCurrentNodeNotInHtmlNS
        {
            get;
            set;
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

        /// <summary>
        /// Stops the tokenization process.
        /// </summary>
        public void Stop()
        {
            pause = true;
        }

        /// <summary>
        /// Starts the tokenization process.
        /// </summary>
        public void Start()
        {
            if (Block)
                return;

            pause = false;

            do
            {
                if (pause)
                    break;

#if DEBUG

                if (_.Current == Specification.CR)
                    throw new Exception("According to the specification (8.2.2.4) the Tokenizer should never see a CR character.");

#endif

                state(_.Current);
                _.Advance();
            }
            while (!_.IsEnded);
        }

        /// <summary>
        /// Switches the current tokenization state
        /// to the desired content model.
        /// </summary>
        /// <param name="state">The new state.</param>
        public void Switch(ContentModel state)
        {
            switch (state)
            {
                case ContentModel.PCData:
                    this.state = Data;
                    break;

                case ContentModel.RCData:
                    this.state = RCData;
                    break;

                case ContentModel.Plaintext:
                    this.state = Plaintext;
                    break;

                case ContentModel.Rawtext:
                    this.state = Rawtext;
                    break;

                case ContentModel.Script:
                    this.state = ScriptData;
                    break;
            }
        }

        #endregion

        #region General

        /// <summary>
        /// See 8.2.4.7 PLAINTEXT state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void Plaintext(char c)
        {
            switch (c)
            {
                case Specification.NULL:
                    RaiseErrorOccurred(ErrorCode.NULL);
                    RaiseTokenEmitted(new HtmlCharacterToken(Specification.REPLACEMENT));
                    //buffer.Append(Specification.REPLACEMENT);
                    break;

                case Specification.EOF:
                    //RaiseTokenEmitted(HtmlToken.Characters(buffer.ToString()));
                    //buffer.Clear();
                    RaiseTokenEmitted(HtmlEndOfFileToken.Token);
                    break;

                default:
                    //buffer.Append(c);
                    RaiseTokenEmitted(new HtmlCharacterToken(c));
                    break;
            }
        }

        /// <summary>
        /// See 8.2.4.1 Data state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void Data(char c)
        {
            switch (c)
            {
                case Specification.AMPERSAND:
                    _.Advance();
                    var value = CharacterReference(_.Current);

                    if (value == null)
                    {
                        RaiseTokenEmitted(new HtmlCharacterToken(Specification.AMPERSAND));
                    }
                    else
                    {
                        for (var i = 0; i < value.Length; i++)
                            RaiseTokenEmitted(new HtmlCharacterToken(value[i]));
                    }

                    break;

                case Specification.LT:
                    state = TagOpen;
                    break;

                case Specification.NULL:
                    RaiseErrorOccurred(ErrorCode.NULL);
                    break;

                case Specification.EOF:
                    RaiseTokenEmitted(HtmlEndOfFileToken.Token);
                    break;

                default:
                    RaiseTokenEmitted(new HtmlCharacterToken(c));
                    break;
            }
        }

        #endregion

        #region RCData

        /// <summary>
        /// See 8.2.4.3 RCDATA state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void RCData(char c)
        {
            switch (c)
            {
                case Specification.AMPERSAND:
                    _.Advance();
                    var value = CharacterReference(_.Current);

                    if (value == null)
                    {
                        _.Back();
                        RaiseTokenEmitted(new HtmlCharacterToken(Specification.AMPERSAND));
                    }
                    else
                    {
                        for (var i = 0; i < value.Length; i++)
                            RaiseTokenEmitted(new HtmlCharacterToken(value[i]));
                    }
                    
                    break;

                case Specification.LT:
                    state = RCDataLT;
                    break;

                case Specification.NULL:
                    RaiseErrorOccurred(ErrorCode.NULL);
                    RaiseTokenEmitted(new HtmlCharacterToken(Specification.REPLACEMENT));
                    break;

                case Specification.EOF:
                    RaiseTokenEmitted(HtmlEndOfFileToken.Token);
                    break;

                default:
                    RaiseTokenEmitted(new HtmlCharacterToken(c));
                    break;
            }
        }

        /// <summary>
        /// See 8.2.4.11 RCDATA less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void RCDataLT(char c)
        {
            if (c == Specification.SOLIDUS)
            {
                buffer.Clear();
                state = RCDataEndTag;
            }
            else
            {
                state = RCData;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.LT));
            }
        }

        /// <summary>
        /// See 8.2.4.12 RCDATA end tag open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void RCDataEndTag(char c)
        {
            if (Specification.IsUppercaseAscii(c))
            {
                token = HtmlTagToken.Close;
                state = RCDataNameEndTag;
                buffer.Clear();
                buffer.Append(c.ToLower());
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                token = HtmlTagToken.Close;
                state = RCDataNameEndTag;
                buffer.Clear();
                buffer.Append(c);
            }
            else
            {
                state = RCData;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.LT));
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.SOLIDUS));
                _.Back();
            }
        }

        /// <summary>
        /// See 8.2.4.13 RCDATA end tag name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void RCDataNameEndTag(char c)
        {
            var name = buffer.ToString();
            var appropriateTag = name == lastStartTag;

            if (appropriateTag && Specification.IsSpaceCharacter(c))
            {
                ((HtmlTagToken)token).Name = name;
                state = AttributeBeforeName;
            }
            else if (appropriateTag && c == Specification.SOLIDUS)
            {
                ((HtmlTagToken)token).Name = name;
                state = TagSelfClosing;
            }
            else if (appropriateTag && c == Specification.GT)
            {
                state = Data;
                ((HtmlTagToken)token).Name = name;
                RaiseTokenEmitted(token);
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
                state = RCData;
                token = null;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.LT));
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.SOLIDUS));
                
                for(var i = 0; i < buffer.Length; i++)
                    RaiseTokenEmitted(new HtmlCharacterToken(buffer[i]));

                _.Back();
            }
        }

        #endregion

        #region Rawtext

        /// <summary>
        /// See 8.2.4.5 RAWTEXT state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void Rawtext(char c)
        {
            switch (c)
            {
                case Specification.LT:
                    state = RawtextLT;
                    break;

                case Specification.NULL:
                    RaiseErrorOccurred(ErrorCode.NULL);
                    RaiseTokenEmitted(new HtmlCharacterToken(Specification.REPLACEMENT));
                    break;

                case Specification.EOF:
                    RaiseTokenEmitted(HtmlEndOfFileToken.Token);
                    break;

                default:
                    RaiseTokenEmitted(new HtmlCharacterToken(c));
                    break;
            }
        }

        /// <summary>
        /// See 8.2.4.14 RAWTEXT less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void RawtextLT(char c)
        {
            if (c == Specification.SOLIDUS)
            {
                buffer.Clear();
                state = RawtextEndTag;
            }
            else
            {
                state = Rawtext;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.LT));
                _.Back();
            }
        }

        /// <summary>
        /// See 8.2.4.15 RAWTEXT end tag open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void RawtextEndTag(char c)
        {
            if (Specification.IsUppercaseAscii(c))
            {
                token = HtmlTagToken.Close;
                state = RawtextNameEndTag;
                buffer.Clear();
                buffer.Append(c.ToLower());
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                token = HtmlTagToken.Close;
                state = RawtextNameEndTag;
                buffer.Clear();
                buffer.Append(c);
            }
            else
            {
                state = Rawtext;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.LT));
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.SOLIDUS));
                _.Back();
            }
        }

        /// <summary>
        /// See 8.2.4.16 RAWTEXT end tag name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void RawtextNameEndTag(char c)
        {
            var name = buffer.ToString();
            var appropriateTag = name == lastStartTag;

            if (appropriateTag && Specification.IsSpaceCharacter(c))
            {
                ((HtmlTagToken)token).Name = name;
                state = AttributeBeforeName;
            }
            else if (appropriateTag && c == Specification.SOLIDUS)
            {
                ((HtmlTagToken)token).Name = name;
                state = TagSelfClosing;
            }
            else if (appropriateTag && c == Specification.GT)
            {
                state = Data;
                ((HtmlTagToken)token).Name = name;
                RaiseTokenEmitted(token);
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
                state = Rawtext;
                token = null;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.LT));
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.SOLIDUS));

                for (var i = 0; i < buffer.Length; i++)
                    RaiseTokenEmitted(new HtmlCharacterToken(buffer[i]));

                _.Back();
            }
        }

        #endregion

        #region CDATA

        /// <summary>
        /// See 8.2.4.68 CDATA section state
        /// </summary>
        void CData(char c)
        {
            var local = new StringBuilder();

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

                local.Append(c);
                _.Advance();
                c = _.Current;
            }

            for(var i = 0; i != local.Length; i++)
                RaiseTokenEmitted(new HtmlCharacterToken(local[i]));

            state = Data;
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
            var local = new StringBuilder();

            do
            {
                var chr = _.Current;

                if (chr == ';' || !Specification.IsAlphanumericAscii(chr))
                    break;

                local.Append(chr);
                consumed++;
                var value = Entities.GetSymbol(local.ToString());

                if (value != null)
                {
                    consumed = 0;
                    last = value;
                }

                _.Advance();
            }
            while (!_.IsEnded);

            local.Clear();

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
        void TagOpen(char c)
        {
            if (c == Specification.EM)
            {
                state = MarkupDeclaration;
            }
            else if (c == Specification.SOLIDUS)
            {
                state = TagEnd;
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                state = TagName;
                token = HtmlTagToken.Open;
                buffer.Clear();
                buffer.Append(c.ToLower());
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                state = TagName;
                token = HtmlTagToken.Open;
                buffer.Clear();
                buffer.Append(c);
            }
            else if (c == '?')
            {
                RaiseErrorOccurred(ErrorCode.BogusComment);
                state = BogusComment;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.AmbiguousOpenTag);
                state = Data;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.LT));
            }
        }

        /// <summary>
        /// See 8.2.4.9 End tag open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void TagEnd(char c)
        {
            if (Specification.IsUppercaseAscii(c))
            {
                state = TagName;
                token = HtmlTagToken.Close;
                buffer.Clear();
                buffer.Append(c.ToLower());
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                state = TagName;
                token = HtmlTagToken.Close;
                buffer.Clear();
                buffer.Append(c);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                state = Data;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.LT));
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.SOLIDUS));
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.BogusComment);
                state = BogusComment;
            }
        }

        /// <summary>
        /// See 8.2.4.10 Tag name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void TagName(char c)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                state = AttributeBeforeName;
                ((HtmlTagToken)token).Name = buffer.ToString();
            }
            else if (c == Specification.SOLIDUS)
            {
                state = TagSelfClosing;
                ((HtmlTagToken)token).Name = buffer.ToString();
            }
            else if (c == Specification.GT)
            {
                state = Data;
                ((HtmlTagToken)token).Name = buffer.ToString();
                EmitTag();
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                buffer.Append(c.ToLower());
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Append(Specification.REPLACEMENT);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
            }
            else
            {
                buffer.Append(c);
            }
        }

        /// <summary>
        /// See 8.2.4.43 Self-closing start tag state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void TagSelfClosing(char c)
        {
            if (c == Specification.GT)
            {
                state = Data;
                ((HtmlTagToken)token).IsSelfClosing = true;
                EmitTag();
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.ClosingSlashMisplaced);
                state = AttributeBeforeName;
                _.Back();
            }
        }

        /// <summary>
        /// See 8.2.4.45 Markup declaration open state
        /// </summary>
        void MarkupDeclaration(char c)
        {
            if (_.ContinuesWith("--"))
            {
                _.Advance();
                state = CommentStart;
            }
            else if (_.ContinuesWith("doctype"))
            {
                _.Advance(6);
                state = Doctype;
            }
            else if (IsCurrentNodeNotInHtmlNS && _.ContinuesWith("[CDATA[", false))
            {
                _.Advance(6);
                state = CData;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.UndefinedMarkupDeclaration);
                state = BogusComment;
            }
        }

        #endregion

        #region Comments

        /// <summary>
        /// See 8.2.4.44 Bogus comment state
        /// </summary>
        void BogusComment(char c)
        {
            token = new HtmlCommentToken();
            buffer.Clear();
            buffer.Append(_.Back(3).Current);
            buffer.Append(_.Advance().Current);
            buffer.Append(_.Advance().Current);
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

            EmitComment();
            state = Data;
        }

        /// <summary>
        /// See 8.2.4.46 Comment start state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void CommentStart(char c)
        {
            buffer.Clear();
            token = new HtmlCommentToken();

            if (c == Specification.DASH)
            {
                state = CommentDashStart;
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                state = Comment;
                buffer.Append(Specification.REPLACEMENT);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                state = Data;
                EmitComment();
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
                EmitComment();
            }
            else
            {
                state = Comment;
                buffer.Append(c);
            }
        }

        /// <summary>
        /// See 8.2.4.47 Comment start dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void CommentDashStart(char c)
        {
            if (c == Specification.DASH)
            {
                state = CommentEnd;
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                state = Comment;
                buffer.Append(Specification.DASH);
                buffer.Append(Specification.REPLACEMENT);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                state = Data;
                EmitComment();
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
                EmitComment();
            }
            else
            {
                state = Comment;
                buffer.Append(Specification.DASH);
                buffer.Append(c);
            }
        }

        /// <summary>
        /// See 8.2.4.48 Comment state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void Comment(char c)
        {
            if (c == Specification.DASH)
            {
                state = CommentDashEnd;
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Append(Specification.REPLACEMENT);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
                EmitComment();
            }
            else
            {
                buffer.Append(c);
            }
        }

        /// <summary>
        /// See 8.2.4.49 Comment end dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void CommentDashEnd(char c)
        {
            if (c == Specification.DASH)
            {
                state = CommentEnd;
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                state = Comment;
                buffer.Append(Specification.DASH);
                buffer.Append(Specification.REPLACEMENT);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
                EmitComment();
            }
            else
            {
                state = Comment;
                buffer.Append(Specification.DASH);
                buffer.Append(c);
            }
        }

        /// <summary>
        /// See 8.2.4.50 Comment end state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void CommentEnd(char c)
        {
            if (c == Specification.GT)
            {
                state = Data;
                EmitComment();
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Comment;
                buffer.Append(Specification.DASH);
                buffer.Append(Specification.REPLACEMENT);
            }
            else if (c == Specification.EM)
            {
                RaiseErrorOccurred(ErrorCode.CommentEndedWithEM);
                state = CommentBangEnd;
            }
            else if (c == Specification.DASH)
            {
                RaiseErrorOccurred(ErrorCode.CommentEndedWithDash);
                buffer.Append(Specification.DASH);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
                EmitComment();
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.CommentEndedUnexpected);
                state = Comment;
                buffer.Append(Specification.DASH);
                buffer.Append(Specification.DASH);
                buffer.Append(c);
            }
        }

        /// <summary>
        /// See 8.2.4.51 Comment end bang state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void CommentBangEnd(char c)
        {
            if (c == Specification.DASH)
            {
                state = CommentDashEnd;
                buffer.Append(Specification.DASH);
                buffer.Append(Specification.DASH);
                buffer.Append(Specification.EM);
            }
            else if (c == Specification.GT)
            {
                state = Data;
                EmitComment();
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                state = Comment;
                buffer.Append(Specification.DASH);
                buffer.Append(Specification.DASH);
                buffer.Append(Specification.EM);
                buffer.Append(Specification.REPLACEMENT);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
                EmitComment();
            }
            else
            {
                state = Comment;
                buffer.Append(Specification.DASH);
                buffer.Append(Specification.DASH);
                buffer.Append(Specification.EM);
                buffer.Append(c);
            }
        }

        #endregion

        #region Doctype

        /// <summary>
        /// See 8.2.4.52 DOCTYPE state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void Doctype(char c)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                state = DoctypeNameBefore;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
                RaiseTokenEmitted(new HtmlDoctypeToken(true));
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.DoctypeUnexpected);
                state = DoctypeNameBefore;
                _.Back();
            }
        }

        /// <summary>
        /// See 8.2.4.53 Before DOCTYPE name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void DoctypeNameBefore(char c)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                //Ignore
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                state = DoctypeName;
                token = new HtmlDoctypeToken();
                buffer.Clear();
                buffer.Append(c.ToLower());
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                state = DoctypeName;
                token = new HtmlDoctypeToken();
                buffer.Clear();
                buffer.Append(Specification.REPLACEMENT);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                state = Data;
                RaiseTokenEmitted(new HtmlDoctypeToken(true));
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
                RaiseTokenEmitted(new HtmlDoctypeToken(true));
            }
            else
            {
                state = DoctypeName;
                token = new HtmlDoctypeToken();
                buffer.Clear();
                buffer.Append(c);
            }
        }

        /// <summary>
        /// See 8.2.4.54 DOCTYPE name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void DoctypeName(char c)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                ((HtmlDoctypeToken)token).Name = buffer.ToString();
                state = DoctypeNameAfter;
            }
            else if (c == Specification.GT)
            {
                state = Data;
                ((HtmlDoctypeToken)token).Name = buffer.ToString();
                RaiseTokenEmitted(token);
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                buffer.Append(c.ToLower());
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Append(Specification.REPLACEMENT);
            }
            else if (c == Specification.EOF)
            {
                var doctype = (HtmlDoctypeToken)token;
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
                doctype.IsQuirksForced = true;
                doctype.Name = buffer.ToString();
                RaiseTokenEmitted(token);
            }
            else
            {
                buffer.Append(c);
            }
        }

        /// <summary>
        /// See 8.2.4.55 After DOCTYPE name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void DoctypeNameAfter(char c)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                //Ignore
            }
            else if (c == Specification.GT)
            {
                state = Data;
                RaiseTokenEmitted(token);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
                ((HtmlDoctypeToken)token).IsQuirksForced = true;
                RaiseTokenEmitted(token);
            }
            else if (_.ContinuesWith("public"))
            {
                _.Advance(5);
                state = DoctypePublic;
            }
            else if (_.ContinuesWith("system"))
            {
                _.Advance(5);
                state = DoctypeSystem;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.DoctypeUnexpectedAfterName);
                state = BogusDoctype;
                ((HtmlDoctypeToken)token).IsQuirksForced = true;
            }
        }

        /// <summary>
        /// See 8.2.4.56 After DOCTYPE public keyword state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void DoctypePublic(char c)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                state = DoctypePublicIdentifierBefore;
            }
            else if (c == Specification.DQ)
            {
                RaiseErrorOccurred(ErrorCode.DoubleQuotationMarkUnexpected);
                ((HtmlDoctypeToken)token).PublicIdentifier = string.Empty;
                state = DoctypePublicIdentifierDoubleQuoted;
            }
            else if (c == Specification.SQ)
            {
                RaiseErrorOccurred(ErrorCode.SingleQuotationMarkUnexpected);
                ((HtmlDoctypeToken)token).PublicIdentifier = string.Empty;
                state = DoctypePublicIdentifierSingleQuoted;
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                ((HtmlDoctypeToken)token).IsQuirksForced = true;
                state = Data;
                RaiseTokenEmitted(token);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                ((HtmlDoctypeToken)token).IsQuirksForced = true;
                state = Data;
                RaiseTokenEmitted(token);
                _.Back();
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.DoctypePublicInvalid);
                ((HtmlDoctypeToken)token).IsQuirksForced = true;
                state = BogusDoctype;
            }
        }

        /// <summary>
        /// See 8.2.4.57 Before DOCTYPE public identifier state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void DoctypePublicIdentifierBefore(char c)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                //Ignore
            }
            else if (c == Specification.DQ)
            {
                buffer.Clear();
                ((HtmlDoctypeToken)token).PublicIdentifier = string.Empty;
                state = DoctypePublicIdentifierDoubleQuoted;
            }
            else if (c == Specification.SQ)
            {
                buffer.Clear();
                ((HtmlDoctypeToken)token).PublicIdentifier = string.Empty;
                state = DoctypePublicIdentifierSingleQuoted;
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                ((HtmlDoctypeToken)token).IsQuirksForced = true;
                RaiseTokenEmitted(token);
                state = Data;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                ((HtmlDoctypeToken)token).IsQuirksForced = true;
                RaiseTokenEmitted(token);
                state = Data;
                _.Back();
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.DoctypePublicInvalid);
                ((HtmlDoctypeToken)token).IsQuirksForced = true;
                state = BogusDoctype;
            }
        }

        /// <summary>
        /// See 8.2.4.58 DOCTYPE public identifier (double-quoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void DoctypePublicIdentifierDoubleQuoted(char c)
        {
            if (c == Specification.DQ)
            {
                ((HtmlDoctypeToken)token).PublicIdentifier = buffer.ToString();
                state = DoctypePublicIdentifierAfter;
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Append(Specification.REPLACEMENT);
            }
            else if (c == Specification.GT)
            {
                var doctype = (HtmlDoctypeToken)token;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.IsQuirksForced = true;
                doctype.PublicIdentifier = buffer.ToString();
                state = Data;
                RaiseTokenEmitted(token);
            }
            else if (c == Specification.EOF)
            {
                var doctype = (HtmlDoctypeToken)token;
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
                doctype.IsQuirksForced = true;
                doctype.PublicIdentifier = buffer.ToString();
                RaiseTokenEmitted(token);
            }
            else
            {
                buffer.Append(c);
            }
        }

        /// <summary>
        /// See 8.2.4.59 DOCTYPE public identifier (single-quoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void DoctypePublicIdentifierSingleQuoted(char c)
        {
            if (c == Specification.SQ)
            {
                ((HtmlDoctypeToken)token).PublicIdentifier = buffer.ToString();
                state = DoctypePublicIdentifierAfter;
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Append(Specification.REPLACEMENT);
            }
            else if (c == Specification.GT)
            {
                var doctype = (HtmlDoctypeToken)token;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.IsQuirksForced = true;
                doctype.PublicIdentifier = buffer.ToString();
                state = Data;
                RaiseTokenEmitted(token);
            }
            else if (c == Specification.EOF)
            {
                var doctype = (HtmlDoctypeToken)token;
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                doctype.IsQuirksForced = true;
                doctype.PublicIdentifier = buffer.ToString();
                RaiseTokenEmitted(token);
                _.Back();
            }
            else
            {
                buffer.Append(c);
            }
        }

        /// <summary>
        /// See 8.2.4.60 After DOCTYPE public identifier state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void DoctypePublicIdentifierAfter(char c)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                buffer.Clear();
                state = DoctypeBetween;
            }
            else if (c == Specification.GT)
            {
                state = Data;
                RaiseTokenEmitted(token);
            }
            else if (c == Specification.DQ)
            {
                RaiseErrorOccurred(ErrorCode.DoubleQuotationMarkUnexpected);
                ((HtmlDoctypeToken)token).SystemIdentifier = string.Empty;
                RaiseTokenEmitted(token);
                state = DoctypeSystemIdentifierDoubleQuoted;
            }
            else if (c == Specification.SQ)
            {
                RaiseErrorOccurred(ErrorCode.SingleQuotationMarkUnexpected);
                ((HtmlDoctypeToken)token).SystemIdentifier = string.Empty;
                RaiseTokenEmitted(token);
                state = DoctypeSystemIdentifierSingleQuoted;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                ((HtmlDoctypeToken)token).IsQuirksForced = true;
                RaiseTokenEmitted(token);
                _.Back();
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
                state = BogusDoctype;
                ((HtmlDoctypeToken)token).IsQuirksForced = true;
            }
        }

        /// <summary>
        /// See 8.2.4.61 Between DOCTYPE public and system identifiers state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void DoctypeBetween(char c)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                //Ignore
            }
            else if (c == Specification.GT)
            {
                state = Data;
                RaiseTokenEmitted(token);
            }
            else if (c == Specification.DQ)
            {
                ((HtmlDoctypeToken)token).SystemIdentifier = string.Empty;
                state = DoctypeSystemIdentifierDoubleQuoted;
            }
            else if (c == Specification.SQ)
            {
                ((HtmlDoctypeToken)token).SystemIdentifier = string.Empty;
                state = DoctypeSystemIdentifierSingleQuoted;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                ((HtmlDoctypeToken)token).IsQuirksForced = true;
                RaiseTokenEmitted(token);
                _.Back();
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
                ((HtmlDoctypeToken)token).IsQuirksForced = true;
                state = BogusDoctype;
            }
        }

        /// <summary>
        /// See 8.2.4.62 After DOCTYPE system keyword state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void DoctypeSystem(char c)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                state = DoctypeSystemIdentifierBefore;
            }
            else if (c == Specification.DQ)
            {
                RaiseErrorOccurred(ErrorCode.DoubleQuotationMarkUnexpected);
                ((HtmlDoctypeToken)token).SystemIdentifier = string.Empty;
                state = DoctypeSystemIdentifierDoubleQuoted;
            }
            else if (c == Specification.SQ)
            {
                RaiseErrorOccurred(ErrorCode.SingleQuotationMarkUnexpected);
                ((HtmlDoctypeToken)token).SystemIdentifier = string.Empty;
                state = DoctypeSystemIdentifierSingleQuoted;
            }
            else if (c == Specification.GT)
            {
                var doctype = (HtmlDoctypeToken)token;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                state = Data;
                doctype.SystemIdentifier = buffer.ToString();
                doctype.IsQuirksForced = true;
                RaiseTokenEmitted(token);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                ((HtmlDoctypeToken)token).IsQuirksForced = true;
                RaiseTokenEmitted(token);
                _.Back();
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.DoctypeSystemInvalid);
                ((HtmlDoctypeToken)token).IsQuirksForced = true;
                state = BogusDoctype;
            }
        }

        /// <summary>
        /// See 8.2.4.63 Before DOCTYPE system identifier state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void DoctypeSystemIdentifierBefore(char c)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                //Ignore
            }
            else if (c == Specification.DQ)
            {
                state = DoctypeSystemIdentifierDoubleQuoted;
                ((HtmlDoctypeToken)token).SystemIdentifier = string.Empty;
            }
            else if (c == Specification.SQ)
            {
                state = DoctypeSystemIdentifierSingleQuoted;
                ((HtmlDoctypeToken)token).SystemIdentifier = string.Empty;
            }
            else if (c == Specification.GT)
            {
                var doctype = (HtmlDoctypeToken)token;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                state = Data;
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = buffer.ToString();
                RaiseTokenEmitted(token);
            }
            else if (c == Specification.EOF)
            {
                var doctype = (HtmlDoctypeToken)token;
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = buffer.ToString();
                RaiseTokenEmitted(token);
                _.Back();
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
                state = BogusDoctype;
                ((HtmlDoctypeToken)token).IsQuirksForced = true;
            }
        }

        /// <summary>
        /// See 8.2.4.64 DOCTYPE system identifier (double-quoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void DoctypeSystemIdentifierDoubleQuoted(char c)
        {
            if (c == Specification.DQ)
            {
                ((HtmlDoctypeToken)token).SystemIdentifier = buffer.ToString();
                state = DoctypeSystemIdentifierAfter;
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Append(Specification.REPLACEMENT);
            }
            else if (c == Specification.GT)
            {
                var doctype = (HtmlDoctypeToken)token;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                state = Data;
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = buffer.ToString();
                RaiseTokenEmitted(token);
            }
            else if (c == Specification.EOF)
            {
                var doctype = (HtmlDoctypeToken)token;
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = buffer.ToString();
                RaiseTokenEmitted(token);
                _.Back();
            }
            else
            {
                buffer.Append(c);
            }
        }

        /// <summary>
        /// See 8.2.4.65 DOCTYPE system identifier (single-quoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void DoctypeSystemIdentifierSingleQuoted(char c)
        {
            if (c == Specification.SQ)
            {
                ((HtmlDoctypeToken)token).SystemIdentifier = buffer.ToString();
                state = DoctypeSystemIdentifierAfter;
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Append(Specification.REPLACEMENT);
            }
            else if (c == Specification.GT)
            {
                var doctype = (HtmlDoctypeToken)token;
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                state = Data;
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = buffer.ToString();
                RaiseTokenEmitted(token);
            }
            else if (c == Specification.EOF)
            {
                var doctype = (HtmlDoctypeToken)token;
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                doctype.IsQuirksForced = true;
                doctype.SystemIdentifier = buffer.ToString();
                RaiseTokenEmitted(token);
                _.Back();
            }
            else
            {
                buffer.Append(c);
            }
        }

        /// <summary>
        /// See 8.2.4.66 After DOCTYPE system identifier state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void DoctypeSystemIdentifierAfter(char c)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                //Ignore
            }
            else if (c == Specification.GT)
            {
                state = Data;
                RaiseTokenEmitted(token);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                ((HtmlDoctypeToken)token).IsQuirksForced = true;
                RaiseTokenEmitted(token);
                _.Back();
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
                state = BogusDoctype;
            }
        }

        /// <summary>
        /// See 8.2.4.67 Bogus DOCTYPE state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void BogusDoctype(char c)
        {
            if (c == Specification.GT)
            {
                state = Data;
                RaiseTokenEmitted(token);
            }
            else if (c == Specification.EOF)
            {
                state = Data;
                RaiseTokenEmitted(token);
                _.Back();
            }
        }

        #endregion

        #region Attributes

        /// <summary>
        /// See 8.2.4.34 Before attribute name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void AttributeBeforeName(char c)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                //Ignore
            }
            else if (c == Specification.SOLIDUS)
            {
                state = TagSelfClosing;
            }
            else if (c == Specification.GT)
            {
                state = Data;
                EmitTag();
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                buffer.Clear();
                buffer.Append(c.ToLower());
                state = AttributeName;
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Clear();
                buffer.Append(Specification.REPLACEMENT);
                state = AttributeName;
            }
            else if (c == Specification.SQ || c == Specification.DQ || c == Specification.EQ || c == Specification.LT)
            {
                RaiseErrorOccurred(ErrorCode.AttributeNameInvalid);
                buffer.Clear();
                buffer.Append(c);
                state = AttributeName;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
            }
            else
            {
                buffer.Clear();
                buffer.Append(c);
                state = AttributeName;
            }
        }

        /// <summary>
        /// See 8.2.4.35 Attribute name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void AttributeName(char c)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                ((HtmlTagToken)token).AddAttribute(buffer.ToString());
                state = AttributeAfterName;
            }
            else if (c == Specification.SOLIDUS)
            {
                ((HtmlTagToken)token).AddAttribute(buffer.ToString());
                state = TagSelfClosing;
            }
            else if (c == Specification.EQ)
            {
                ((HtmlTagToken)token).AddAttribute(buffer.ToString());
                state = AttributeBeforeValue;
            }
            else if (c == Specification.GT)
            {
                state = Data;
                ((HtmlTagToken)token).AddAttribute(buffer.ToString());
                EmitTag();
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                buffer.Append(c.ToLower());
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Append(Specification.REPLACEMENT);
            }
            else if (c == Specification.DQ || c == Specification.SQ || c == Specification.LT)
            {
                RaiseErrorOccurred(ErrorCode.AttributeNameInvalid);
                buffer.Append(c);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
            }
            else
            {
                buffer.Append(c);
            }
        }

        /// <summary>
        /// See 8.2.4.36 After attribute name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void AttributeAfterName(char c)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                //Ignore
            }
            else if (c == Specification.SOLIDUS)
            {
                state = TagSelfClosing;
            }
            else if (c == Specification.EQ)
            {
                state = AttributeBeforeValue;
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                buffer.Clear();
                buffer.Append(c.ToLower());
                state = AttributeName;
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Clear();
                buffer.Append(Specification.REPLACEMENT);
                state = AttributeName;
            }
            else if (c == Specification.DQ || c == Specification.SQ || c == Specification.LT)
            {
                RaiseErrorOccurred(ErrorCode.AttributeNameInvalid);
                buffer.Clear();
                buffer.Append(c);
                state = AttributeName;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
            }
            else
            {
                buffer.Clear();
                buffer.Append(c);
                state = AttributeName;
            }
        }

        /// <summary>
        /// See 8.2.4.37 Before attribute value state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void AttributeBeforeValue(char c)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                //Ignore
            }
            else if (c == Specification.DQ)
            {
                buffer.Clear();
                state = AttributeDoubleQuotedValue;
            }
            else if (c == Specification.AMPERSAND)
            {
                buffer.Clear();
                state = AttributeUnquotedValue;
                _.Back();
            }
            else if (c == Specification.SQ)
            {
                buffer.Clear();
                state = AttributeSingleQuotedValue;
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Append(Specification.REPLACEMENT);
                state = AttributeUnquotedValue;
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                state = Data;
                EmitTag();
            }
            else if (c == Specification.LT || c == Specification.EQ || c == Specification.CQ)
            {
                RaiseErrorOccurred(ErrorCode.AttributeValueInvalid);
                buffer.Clear().Append(c);
                state = AttributeUnquotedValue;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
            }
            else
            {
                buffer.Clear().Append(c);
                state = AttributeUnquotedValue;
            }
        }

        /// <summary>
        /// See 8.2.4.38 Attribute value (double-quoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void AttributeDoubleQuotedValue(char c)
        {
            if (c == Specification.DQ)
            {
                ((HtmlTagToken)token).SetAttributeValue(buffer.ToString());
                state = AttributeAfterValue;
            }
            else if (c == Specification.AMPERSAND)
            {
                _.Advance();
                var value = CharacterReference(_.Current, Specification.DQ);

                if (value == null)
                {
                    buffer.Append(Specification.AMPERSAND);
                    _.Back();
                }
                else
                    buffer.Append(value);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Append(Specification.REPLACEMENT);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
            }
            else
            {
                buffer.Append(c);
            }
        }

        /// <summary>
        /// See 8.2.4.39 Attribute value (single-quoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void AttributeSingleQuotedValue(char c)
        {
            if (c == Specification.SQ)
            {
                ((HtmlTagToken)token).SetAttributeValue(buffer.ToString());
                state = AttributeAfterValue;
            }
            else if (c == Specification.AMPERSAND)
            {
                _.Advance();
                var value = CharacterReference(_.Current, Specification.SQ);

                if (value == null)
                {
                    buffer.Append(Specification.AMPERSAND);
                    _.Back();
                }
                else
                    buffer.Append(value);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Append(Specification.REPLACEMENT);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
            }
            else
            {
                buffer.Append(c);
            }
        }

        /// <summary>
        /// See 8.2.4.40 Attribute value (unquoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void AttributeUnquotedValue(char c)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                state = AttributeBeforeName;
                ((HtmlTagToken)token).SetAttributeValue(buffer.ToString());
            }
            else if (c == Specification.AMPERSAND)
            {
                state = AttributeAfterValue;
                _.Advance();
                var value = CharacterReference(_.Current, Specification.GT);

                if (value == null)
                {
                    value = new char[] { Specification.AMPERSAND };
                    _.Back();
                }

                ((HtmlTagToken)token).SetAttributeValue(new string(value));
            }
            else if (c == Specification.GT)
            {
                state = Data;
                ((HtmlTagToken)token).SetAttributeValue(buffer.ToString());
                EmitTag();
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                buffer.Append(Specification.REPLACEMENT);
            }
            else if (c == Specification.DQ || c == Specification.SQ || c == Specification.LT || c == Specification.EQ || c == Specification.CQ)
            {
                RaiseErrorOccurred(ErrorCode.AttributeValueInvalid);
                buffer.Append(c);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
            }
            else
            {
                buffer.Append(c);
            }
        }

        /// <summary>
        /// See 8.2.4.42 After attribute value (quoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void AttributeAfterValue(char c)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                state = AttributeBeforeName;
            }
            else if (c == Specification.SOLIDUS)
            {
                state = TagSelfClosing;
            }
            else if (c == Specification.GT)
            {
                state = Data;
                EmitTag();
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.AttributeNameExpected);
                state = AttributeBeforeName;
                _.Back();
            }
        }

        #endregion

        #region Script

        /// <summary>
        /// See 8.2.4.6 Script data state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void ScriptData(char c)
        {
            switch (c)
            {
                case Specification.LT:
                    state = ScriptDataLT;
                    break;

                case Specification.NULL:
                    RaiseErrorOccurred(ErrorCode.NULL);
                    RaiseTokenEmitted(new HtmlCharacterToken(Specification.REPLACEMENT));
                    break;

                case Specification.EOF:
                    RaiseTokenEmitted(HtmlEndOfFileToken.Token);
                    break;

                default:
                    RaiseTokenEmitted(new HtmlCharacterToken(c));
                    break;
            }
        }

        /// <summary>
        /// See 8.2.4.17 Script data less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void ScriptDataLT(char c)
        {
            if (c == Specification.SOLIDUS)
            {
                state = ScriptDataEndTag;
            }
            else if (c == Specification.EM)
            {
                state = ScriptDataStartEscape;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.LT));
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.EM));
            }
            else
            {
                state = ScriptData;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.LT));
                _.Back();
            }
        }

        /// <summary>
        /// See 8.2.4.18 Script data end tag open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void ScriptDataEndTag(char c)
        {
            if (Specification.IsUppercaseAscii(c))
            {
                state = ScriptDataNameEndTag;
                token = HtmlTagToken.Close;
                buffer.Clear();
                buffer.Append(c.ToLower());
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                state = ScriptDataNameEndTag;
                token = HtmlTagToken.Close;
                buffer.Clear();
                buffer.Append(c);
            }
            else
            {
                state = ScriptData;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.LT));
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.SOLIDUS));
                _.Back();
            }
        }

        /// <summary>
        /// See 8.2.4.19 Script data end tag name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void ScriptDataNameEndTag(char c)
        {
            var name = buffer.ToString();
            var appropriateEndTag = name == lastStartTag;

            if (appropriateEndTag && Specification.IsSpaceCharacter(c))
            {
                ((HtmlTagToken)token).Name = name;
                state = AttributeBeforeName;
            }
            else if (appropriateEndTag && c == Specification.SOLIDUS)
            {
                ((HtmlTagToken)token).Name = name;
                state = TagSelfClosing;
            }
            else if (appropriateEndTag && c == Specification.GT)
            {
                ((HtmlTagToken)token).Name = name;
                state = Data;
                EmitTag();
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
                state = ScriptData;
                token = null;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.LT));
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.SOLIDUS));

                for (var i = 0; i < buffer.Length; i++)
                    RaiseTokenEmitted(new HtmlCharacterToken(buffer[i]));

                _.Back();
            }
        }

        /// <summary>
        /// See 8.2.4.20 Script data escape start state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void ScriptDataStartEscape(char c)
        {
            if (c == Specification.DASH)
            {
                state = ScriptDataStartEscapeDash;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.DASH));
            }
            else
            {
                state = ScriptData;
                _.Back();
            }
        }

        /// <summary>
        /// See 8.2.4.22 Script data escaped state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void ScriptDataEscaped(char c)
        {
            if (c == Specification.DASH)
            {
                state = ScriptDataEscapedDash;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.DASH));
            }
            else if (c == Specification.LT)
            {
                state = ScriptDataEscapedLT;
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.REPLACEMENT));
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
            }
            else
            {
                state = ScriptData;
                _.Back();
            }
        }

        /// <summary>
        /// See 8.2.4.21 Script data escape start dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void ScriptDataStartEscapeDash(char c)
        {
            if (c == Specification.DASH)
            {
                state = ScriptDataEscapedDashDash;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.DASH));
            }
            else
            {
                state = ScriptData;
                _.Back();
            }
        }

        /// <summary>
        /// See 8.2.4.23 Script data escaped dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void ScriptDataEscapedDash(char c)
        {
            if (c == Specification.DASH)
            {
                state = ScriptDataEscapedDashDash;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.DASH));
            }
            else if (c == Specification.LT)
            {
                state = ScriptDataEscapedLT;
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                state = ScriptDataEscaped;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.REPLACEMENT));
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
            }
            else
            {
                state = ScriptDataEscaped;
                RaiseTokenEmitted(new HtmlCharacterToken(c));
            }
        }

        /// <summary>
        /// See 8.2.4.24 Script data escaped dash dash state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void ScriptDataEscapedDashDash(char c)
        {
            if (c == Specification.DASH)
            {
                state = ScriptDataEscapedDashDash;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.DASH));
            }
            else if (c == Specification.LT)
            {
                state = ScriptDataEscapedLT;
            }
            else if (c == Specification.GT)
            {
                state = ScriptData;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.GT));
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                state = ScriptDataEscaped;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.REPLACEMENT));
            }
            else if (c == Specification.EOF)
            {
                state = Data;
                RaiseErrorOccurred(ErrorCode.EOF);
                _.Back();
            }
            else
            {
                state = ScriptDataEscaped;
                RaiseTokenEmitted(new HtmlCharacterToken(c));
            }
        }

        /// <summary>
        /// See 8.2.4.25 Script data escaped less-than sign state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void ScriptDataEscapedLT(char c)
        {
            if (c == Specification.SOLIDUS)
            {
                state = ScriptDataEndTag;
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                buffer.Clear();
                buffer.Append(c.ToLower());
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.LT));
                RaiseTokenEmitted(new HtmlCharacterToken(c));
                state = ScriptDataStartDoubleEscape;
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                buffer.Clear();
                buffer.Clear();
                buffer.Append(c);
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.LT));
                RaiseTokenEmitted(new HtmlCharacterToken(c));
                state = ScriptDataStartDoubleEscape;
            }
            else
            {
                state = ScriptDataEscaped;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.LT));
                _.Back();
            }
        }

        /// <summary>
        /// See 8.2.4.26 Script data escaped end tag open state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void ScriptDataEscapedEndTag(char c)
        {
            if (Specification.IsUppercaseAscii(c))
            {
                token = HtmlTagToken.Close;
                buffer.Clear();
                buffer.Append(c.ToLower());
                state = ScriptDataEscapedEndTag;
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                token = HtmlTagToken.Close;
                buffer.Clear();
                buffer.Append(c);
                state = ScriptDataEscapedEndTag;
            }
            else
            {
                state = ScriptDataEscaped;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.LT));
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.SOLIDUS));
                _.Back();
            }
        }

        /// <summary>
        /// See 8.2.4.27 Script data escaped end tag name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void ScriptDataEscapedNameTag(char c)
        {
            var name = buffer.ToString();
            var appropriateEndTag = name == lastStartTag;

            if (appropriateEndTag && Specification.IsSpaceCharacter(c))
            {
                ((HtmlTagToken)token).Name = name;
                state = AttributeBeforeName;
            }
            else if (appropriateEndTag && c == Specification.SOLIDUS)
            {
                ((HtmlTagToken)token).Name = name;
                state = TagSelfClosing;
            }
            else if (appropriateEndTag && c == Specification.GT)
            {
                ((HtmlTagToken)token).Name = name;
                state = Data;
                EmitTag();
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
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.LT));
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.SOLIDUS));

                for (var i = 0; i < buffer.Length; i++)
                    RaiseTokenEmitted(new HtmlCharacterToken(buffer[i]));

                _.Back();
            }
        }

        /// <summary>
        /// See 8.2.4.28 Script data double escape start state
        /// </summary>
        /// <param name="c">The next input character.</param>
        void ScriptDataStartDoubleEscape(char c)
        {
            if (Specification.IsSpaceCharacter(c))
            {
                if (buffer.ToString() == "script")
                    state = ScriptDataEscapedDouble;
                else
                {
                    state = ScriptDataEscaped;
                    RaiseTokenEmitted(new HtmlCharacterToken(c));
                }
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                buffer.Append(c.ToLower());
                RaiseTokenEmitted(new HtmlCharacterToken(c));
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                buffer.Append(c);
                RaiseTokenEmitted(new HtmlCharacterToken(c));
            }
            else
            {
                state = ScriptDataEscaped;
                _.Back();
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
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.DASH));
            }
            else if (c == Specification.LT)
            {
                state = ScriptDataEscapedDoubleLT;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.LT));
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.REPLACEMENT));
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                state = Data;
                _.Back();
            }
            else
            {
                RaiseTokenEmitted(new HtmlCharacterToken(c));
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
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.DASH));
            }
            else if (c == Specification.LT)
            {
                state = ScriptDataEscapedDoubleLT;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.LT));
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                state = ScriptDataEscapedDouble;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.REPLACEMENT));
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
                RaiseTokenEmitted(new HtmlCharacterToken(c));
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
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.DASH));
            }
            else if (c == Specification.LT)
            {
                state = ScriptDataEscapedDoubleLT;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.LT));
            }
            else if (c == Specification.GT)
            {
                state = ScriptData;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.GT));
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                state = ScriptDataEscapedDouble;
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.REPLACEMENT));
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
                RaiseTokenEmitted(new HtmlCharacterToken(c));
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
                RaiseTokenEmitted(new HtmlCharacterToken(Specification.SOLIDUS));
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
                    RaiseTokenEmitted(new HtmlCharacterToken(c));
                }
            }
            else if (Specification.IsUppercaseAscii(c))
            {
                buffer.Append(c.ToLower());
                RaiseTokenEmitted(new HtmlCharacterToken(c));
            }
            else if (Specification.IsLowercaseAscii(c))
            {
                buffer.Append(c);
                RaiseTokenEmitted(new HtmlCharacterToken(c));
            }
            else
            {
                state = ScriptDataEscapedDouble;
                _.Back();
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Emits the current token as a comment token.
        /// </summary>
        void EmitComment()
        {
            ((HtmlCommentToken)token).Data = buffer.ToString();
            RaiseTokenEmitted(token);
        }

        /// <summary>
        /// Emits the current token as a tag token.
        /// </summary>
        void EmitTag()
        {
            var tag = (HtmlTagToken)token;

            if (token.Type == HtmlTokenType.StartTag)
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

            RaiseTokenEmitted(token);
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

        /// <summary>
        /// Fires a token emitted event.
        /// </summary>
        /// <param name="token">The token to emit.</param>
        void RaiseTokenEmitted(HtmlToken token)
        {
            if (TokenEmitted != null)
            {
                var pck = new TokenEventArgs(token);
                pck.Line = _.Line;
                pck.Column = _.Column;
                TokenEmitted(this, pck);
            }
        }

        #endregion
    }
}
