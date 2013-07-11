using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AngleSharp.Xml
{
    /// <summary>
    /// Performs the tokenization of the source code. Most of
    /// the information is taken from http://www.w3.org/TR/REC-xml/.
    /// </summary>
    [DebuggerStepThrough]
    sealed class XmlTokenizer
    {
        #region Members

        SourceManager src;
        StringBuilder stringBuffer;

        #endregion

        #region Events

        /// <summary>
        /// The event will be fired once an error has been detected.
        /// </summary>
        public event EventHandler<ParseErrorEventArgs> ErrorOccurred;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new tokenizer for XML documents.
        /// </summary>
        /// <param name="source">The source code manager.</param>
        public XmlTokenizer(SourceManager source)
        {
            src = source;
            stringBuffer = new StringBuilder();
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

        #endregion

        #region Methods

        /// <summary>
        /// Gets the next available token.
        /// </summary>
        /// <returns>The next available token.</returns>
        public XmlToken Get()
        {
            if (src.IsEnded) return XmlToken.EOF;
            XmlToken token = Data(src.Current);
            src.Advance();
            return token;
        }

        #endregion

        #region General

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-logical-struct.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken Data(Char c)
        {
            switch (c)
            {
                case Specification.AMPERSAND:
                    var value = CharacterReference(src.Next);

                    if (value == null)
                        return XmlToken.Character(Specification.AMPERSAND);

                    //return XmlToken.Characters(value);
                    throw new NotImplementedException();

                case Specification.LT:
                    return TagOpen(src.Next);

                case Specification.NULL:
                    RaiseErrorOccurred(ErrorCode.NULL);
                    return Data(src.Next);

                case Specification.EOF:
                    return XmlToken.EOF;

                default:
                    return XmlToken.Character(c);
            }
        }

        #endregion

        #region CDATA

        /// <summary>
        /// See http://www.w3.org/TR/REC-xml/#NT-CData.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlCDataToken CData(Char c)
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

            return XmlToken.CData(stringBuffer.ToString());
        }

        Char[] CharacterReference(Char c)
        {
            //TODO
            throw new NotImplementedException();
        }

        #endregion

        #region Tags

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-starttags.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken TagOpen(Char c)
        {
            if (c == Specification.EM)
            {
                return MarkupDeclaration(src.Next);
            }
            else if (c == Specification.QM)
            {
                return ProcessingStart(src.Next);
            }
            else if (c == Specification.SOLIDUS)
            {
                return TagEnd(src.Next);
            }
            else if (c.IsLetter())
            {
                stringBuffer.Clear();
                stringBuffer.Append(c);
                return TagName(src.Next, XmlToken.OpenTag());
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.AmbiguousOpenTag);
                return XmlToken.Character(Specification.LT);
            }
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#dt-etag.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken TagEnd(Char c)
        {
            if (c.IsLetter())
            {
                stringBuffer.Clear();
                stringBuffer.Append(c);
                return TagName(src.Next, XmlToken.CloseTag());
            }
            else if (c == Specification.EOF)
            {
                src.Back();
                RaiseErrorOccurred(ErrorCode.EOF);
                return XmlToken.EOF;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return Data(src.Next);
            }
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-Name.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        XmlToken TagName(Char c, XmlTagToken tag)
        {
            if (c.IsSpaceCharacter())
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
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                stringBuffer.Append(Specification.REPLACEMENT);
                return TagName(src.Next, tag);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return XmlToken.EOF;
            }
            else
            {
                stringBuffer.Append(c);
                return TagName(src.Next, tag);
            }
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#d0e2480.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        XmlToken TagSelfClosing(Char c, XmlTagToken tag)
        {
            if (c == Specification.GT)
            {
                tag.IsSelfClosing = true;
                return EmitTag(tag);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return XmlToken.EOF;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.ClosingSlashMisplaced);
                return AttributeBeforeName(c, tag);
            }
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#dt-markup.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken MarkupDeclaration(Char c)
        {
            if (src.ContinuesWith("--"))
            {
                src.Advance();
                return CommentStart(src.Next);
            }
            else if (src.ContinuesWith("DOCTYPE", false))
            {
                src.Advance(6);
                return Doctype(src.Next);
            }
            else if (src.ContinuesWith("[CDATA[", false))
            {
                src.Advance(6);
                return CData(src.Next);
            }
            else if (src.ContinuesWith("ENTITY", false))
            {
                src.Advance(5);
                return EntityDeclaration(src.Next);
            }
            else if (src.ContinuesWith("ELEMENT", false))
            {
                src.Advance(6);
                return TypeDeclaration(src.Next);
            }
            else if (src.ContinuesWith("ATTLIST", false))
            {
                src.Advance(6);
                return AttributeDeclaration(src.Next);
            }
            else if (src.ContinuesWith("NOTATION", false))
            {
                src.Advance(7);
                return NotationDeclaration(src.Next);
            }

            return null;
        }

        #endregion

        #region Declaration Name

        Boolean DeclarationNameBefore(Char c, XmlBaseDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = src.Next;

            if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                stringBuffer.Clear();
                stringBuffer.Append(Specification.REPLACEMENT);
                return DeclarationName(src.Next, decl);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return false;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
                return false;
            }

            stringBuffer.Clear();
            stringBuffer.Append(c);
            return DeclarationName(src.Next, decl);
        }

        Boolean DeclarationName(Char c, XmlBaseDeclarationToken decl)
        {
            while (true)
            {
                if (c.IsSpaceCharacter())
                {
                    decl.Name = stringBuffer.ToString();
                    stringBuffer.Clear();
                    return DeclarationNameAfter(src.Next);
                }
                else if (c == Specification.GT)
                {
                    decl.Name = stringBuffer.ToString();
                    return false;
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    src.Back();
                    decl.Name = stringBuffer.ToString();
                    return false;
                }
                else
                    stringBuffer.Append(c);

                c = src.Next;
            }
        }

        Boolean DeclarationNameAfter(Char c)
        {
            while (c.IsSpaceCharacter())
                c = src.Next;

            if (c == Specification.GT)
            {
                return false;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
                return false;
            }

            return true;
        }

        #endregion

        #region Entity Declaration

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-entity-decl.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken EntityDeclaration(Char c)
        {
            var decl = XmlToken.EntityDeclaration();
            var canContinue = false;

            if (c.IsSpaceCharacter())
                canContinue = DeclarationNameBefore(src.Next, decl);
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return XmlToken.EOF;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.UndefinedMarkupDeclaration);
                canContinue = DeclarationNameBefore(c, decl);
            }

            if (canContinue)
            {
                //TODO
            }

            return decl;
        }

        #endregion

        #region Attribute Declaration

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#attdecls.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken AttributeDeclaration(Char c)
        {
            var decl = XmlToken.AttributeDeclaration();
            var canContinue = false;

            if (c.IsSpaceCharacter())
                canContinue = DeclarationNameBefore(src.Next, decl);
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return XmlToken.EOF;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.UndefinedMarkupDeclaration);
                canContinue = DeclarationNameBefore(c, decl);
            }

            if (canContinue)
            {
                //TODO
            }

            return decl;
        }

        #endregion

        #region Notation Declaration

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#Notations.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken NotationDeclaration(Char c)
        {
            var decl = XmlToken.NotationDeclaration();
            var canContinue = false;

            if (c.IsSpaceCharacter())
                canContinue = DeclarationNameBefore(src.Next, decl);
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return XmlToken.EOF;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.UndefinedMarkupDeclaration);
                canContinue = DeclarationNameBefore(c, decl);
            }

            if (canContinue)
            {
                if (src.ContinuesWith("PUBLIC", false))
                {
                    src.Advance(5);
                    return NotationDeclarationBeforePublic(src.Next, decl);
                }
                else if (src.ContinuesWith("SYSTEM", false))
                {
                    src.Advance(5);
                    return NotationDeclarationBeforeSystem(src.Next, decl);
                }

                return NotationDeclarationAfterSystem(c, decl);
            }

            RaiseErrorOccurred(ErrorCode.NotationPublicInvalid);
            return decl;
        }

        XmlToken NotationDeclarationBeforePublic(Char c, XmlNotationDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = src.Next;

            if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return decl;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
                return decl;
            }
            else if (c == Specification.SQ)
            {
                return NotationDeclarationPublic(src.Next, Specification.SQ, decl);
            }
            else if (c == Specification.DQ)
            {
                return NotationDeclarationPublic(src.Next, Specification.DQ, decl);
            }
            else
            {
                return NotationDeclarationAfterSystem(c, decl);
            }
        }

        XmlToken NotationDeclarationPublic(Char c, Char quote, XmlNotationDeclarationToken decl)
        {
            stringBuffer.Clear();

            while (true)
            {
                if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    src.Back();
                    decl.PublicIdentifier = stringBuffer.ToString();
                    return decl;
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == quote)
                {
                    decl.PublicIdentifier = stringBuffer.ToString();
                    return NotationDeclarationAfterPublic(src.Next, decl);
                }
                else
                    stringBuffer.Append(c);

                c = src.Next;
            }
        }

        XmlToken NotationDeclarationAfterPublic(Char c, XmlNotationDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = src.Next;

            if (c == Specification.GT)
            {
                return decl;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
                return decl;
            }
            else if (c == Specification.SQ)
            {
                return NotationDeclarationSystem(src.Next, Specification.SQ, decl);
            }
            else if (c == Specification.DQ)
            {
                return NotationDeclarationSystem(src.Next, Specification.DQ, decl);
            }
            else
            {
                return NotationDeclarationAfterSystem(c, decl);
            }
        }

        XmlToken NotationDeclarationBeforeSystem(Char c, XmlNotationDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = src.Next;

            if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return decl;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
                return decl;
            }
            else if (c == Specification.SQ)
            {
                return NotationDeclarationSystem(src.Next, Specification.SQ, decl);
            }
            else if (c == Specification.DQ)
            {
                return NotationDeclarationSystem(src.Next, Specification.DQ, decl);
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.NotationSystemInvalid);
                return decl;
            }
        }

        XmlToken NotationDeclarationSystem(Char c, Char quote, XmlNotationDeclarationToken decl)
        {
            stringBuffer.Clear();

            while (true)
            {
                if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    src.Back();
                    decl.SystemIdentifier = stringBuffer.ToString();
                    return decl;
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == quote)
                {
                    decl.SystemIdentifier = stringBuffer.ToString();
                    return NotationDeclarationAfterSystem(src.Next, decl);
                }
                else if (c.IsPubidChar())
                    stringBuffer.Append(c);
                else
                    RaiseErrorOccurred(ErrorCode.InvalidCharacter);

                c = src.Next;
            }
        }

        XmlToken NotationDeclarationAfterSystem(Char c, XmlNotationDeclarationToken decl)
        {
            Boolean hasError = false;

            while (true)
            {
                if (c == Specification.GT)
                {
                    return decl;
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    src.Back();
                    return decl;
                }
                else if (!c.IsSpaceCharacter())
                {
                    if (!hasError)
                        RaiseErrorOccurred(ErrorCode.InputUnexpected);

                    hasError = true;
                }

                c = src.Next;
            }
        }

        #endregion

        #region Type Declaration

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#elemdecls.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken TypeDeclaration(Char c)
        {
            var decl = XmlToken.ElementDeclaration();
            var canContinue = false;

            if (c.IsSpaceCharacter())
                canContinue = DeclarationNameBefore(src.Next, decl);
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return XmlToken.EOF;
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.UndefinedMarkupDeclaration);
                canContinue = DeclarationNameBefore(c, decl);
            }

            if (canContinue)
            {
                if (c == '(')
                    return TypeDeclarationBeforeContent(src.Next, decl);
                else if (src.ContinuesWith("ANY", false))
                {
                    src.Advance(2);
                    decl.CType = XmlElementDeclarationToken.ContentType.Any;
                    return TypeDeclarationAfterContent(src.Next, decl);
                }
                else if (src.ContinuesWith("EMPTY", false))
                {
                    src.Advance(4);
                    decl.CType = XmlElementDeclarationToken.ContentType.Empty;
                    return TypeDeclarationAfterContent(src.Next, decl);
                }

                return TypeDeclarationAfterContent(c, decl);
            }

            RaiseErrorOccurred(ErrorCode.TypeDeclarationUndefined);
            return decl;
        }

        XmlToken TypeDeclarationBeforeContent(Char c, XmlElementDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = src.Next;

            if (src.ContinuesWith("#PCDATA", false))
            {
                src.Advance(6);
                decl.CType = XmlElementDeclarationToken.ContentType.Mixed;
                return TypeDeclarationMixed(src.Next, decl);
            }

            decl.CType = XmlElementDeclarationToken.ContentType.Children;
            decl.Entry = TypeDeclarationChildren(c);
            return TypeDeclarationAfterContent(src.Next, decl);
        }

        XmlElementDeclarationToken.ElementDeclarationEntry TypeDeclarationChildren(Char c)
        {
            var entries = new List<XmlElementDeclarationToken.ElementDeclarationEntry>();

            while (true)
            {
                while (c.IsSpaceCharacter())
                    c = src.Next;

                if (c.IsNameStart())
                {
                    var name = TypeDeclarationName(c);
                    entries.Add(name);

                    while (c.IsSpaceCharacter())
                        c = src.Next;

                    if (c == Specification.BC)
                    {
                        //TODO
                        throw new NotImplementedException();
                    }
                }
                else if (c == Specification.BO)
                {
                    //TODO
                    throw new NotImplementedException();
                }

                c = src.Next;
            }
        }

        XmlElementDeclarationToken.ElementNameDeclarationEntry TypeDeclarationName(Char c)
        {
            stringBuffer.Clear();
            stringBuffer.Append(c);

            while ((c = src.Next).IsName())
                stringBuffer.Append(c);

            return new XmlElementDeclarationToken.ElementNameDeclarationEntry {
                Name = c.ToString(),
                Quantifier = TypeDeclarationQuantifier(c)
            };
        }

        XmlElementDeclarationToken.ElementQuantifier TypeDeclarationQuantifier(Char c)
        {
            switch (c)
            {
                case Specification.ASTERISK:
                    src.Advance();
                    return XmlElementDeclarationToken.ElementQuantifier.ZeroOrMore;

                case Specification.QM:
                    src.Advance();
                    return XmlElementDeclarationToken.ElementQuantifier.ZeroOrOne;

                case Specification.PLUS:
                    src.Advance();
                    return XmlElementDeclarationToken.ElementQuantifier.OneOrMore;

                default:
                    return XmlElementDeclarationToken.ElementQuantifier.One;
            }
        }

        XmlToken TypeDeclarationMixed(Char c, XmlElementDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = src.Next;

            if (c == Specification.BC)
            {
                c = src.Next;

                if (c == Specification.ASTERISK)
                {
                    decl.Quantifier = XmlElementDeclarationToken.ElementQuantifier.ZeroOrMore;
                    return TypeDeclarationAfterContent(src.Next, decl);
                }

                if (decl.Names.Count > 0)
                    RaiseErrorOccurred(ErrorCode.QuantifierMissing);
            }
            else if (c == Specification.PIPE)
            {
                c = src.Next;

                while (c.IsSpaceCharacter())
                    c = src.Next;

                stringBuffer.Clear();

                if (c.IsNameStart())
                {
                    stringBuffer.Append(c);

                    while ((c = src.Next).IsName())
                        stringBuffer.Append(c);

                    decl.Names.Add(stringBuffer.ToString());
                    return TypeDeclarationMixed(c, decl);
                }
            }

            return TypeDeclarationAfterContent(c, decl);
        }

        XmlToken TypeDeclarationAfterContent(Char c, XmlElementDeclarationToken decl)
        {
            Boolean hasError = false;

            while (true)
            {
                if (c == Specification.GT)
                {
                    return decl;
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    src.Back();
                    return decl;
                }
                else if (!c.IsSpaceCharacter())
                {
                    if (!hasError)
                        RaiseErrorOccurred(ErrorCode.InputUnexpected);

                    hasError = true;
                }

                c = src.Next;
            }
        }

        #endregion

        #region Processing Instruction

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-pi.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken ProcessingStart(Char c)
        {
            if (src.ContinuesWith("xml", false))
            {
                src.Advance(2);
                return DeclarationStart(src.Next);
            }
            else if (c.IsLetter())
            {
                stringBuffer.Clear();
                stringBuffer.Append(c);
                return ProcessingTarget(src.Next, XmlToken.Processing());
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.AmbiguousOpenTag);
                throw new ArgumentException("Invalid processing instruction.");
            }
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-pi.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="pi">The processing instruction token.</param>
        XmlToken ProcessingTarget(Char c, XmlPIToken pi)
        {
            while (true)
            {
                if (c.IsSpaceCharacter())
                {
                    pi.Target = stringBuffer.ToString();
                    stringBuffer.Clear();
                    return ProcessingContent(src.Next, pi);
                }
                else if (c == Specification.QM)
                {
                    pi.Target = stringBuffer.ToString();
                    stringBuffer.Clear();
                    return ProcessingContent(c, pi);
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    c = Specification.REPLACEMENT;
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    return XmlToken.EOF;
                }

                stringBuffer.Append(c);
                c = src.Next;
            }
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-pi.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="pi">The processing instruction token.</param>
        XmlToken ProcessingContent(Char c, XmlPIToken pi)
        {
            while (true)
            {
                if (c == Specification.QM)
                {
                    c = src.Next;

                    if (c == Specification.GT)
                    {
                        pi.Content = stringBuffer.ToString();
                        return pi;
                    }

                    stringBuffer.Append(Specification.QM);
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    return XmlToken.EOF;
                }
                else
                {
                    stringBuffer.Append(c);
                    c = src.Next;
                }
            }
        }

        #endregion

        #region XML Declaration

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-XMLDecl.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken DeclarationStart(Char c)
        {
            if (!c.IsSpaceCharacter())
            {
                stringBuffer.Clear();
                stringBuffer.Append("xml");
                return ProcessingTarget(c, XmlToken.Processing());
            }

            do c = src.Next;
            while (c.IsSpaceCharacter());

            if (src.ContinuesWith("version", false))
            {
                src.Advance(6);
                return DeclarationVersionAfterName(src.Next, XmlToken.Declaration());
            }

            throw new ArgumentException("Invalid XML declaration.");
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-VersionInfo.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationVersionAfterName(Char c, XmlDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = src.Next;

            if (c == Specification.EQ)
                return DeclarationVersionBeforeValue(src.Next, decl);

            throw new ArgumentException("Invalid XML declaration.");
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-VersionInfo.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationVersionBeforeValue(Char c, XmlDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = src.Next;

            if (c == Specification.DQ)
            {
                stringBuffer.Clear();
                return DeclarationVersionValueDQ(src.Next, decl);
            }
            else if(c == Specification.SQ)
            {
                stringBuffer.Clear();
                return DeclarationVersionValueSQ(src.Next, decl);
            }

            throw new ArgumentException("Invalid XML declaration.");
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-VersionInfo.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationVersionValueSQ(Char c, XmlDeclarationToken decl)
        {
            while (c != Specification.EOF && c != Specification.SQ)
            {
                stringBuffer.Append(c);
                c = src.Next;
            }

            if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return XmlToken.EOF;
            }

            decl.Version = stringBuffer.ToString();
            return DeclarationAfterVersion(src.Next, decl);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-VersionInfo.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationVersionValueDQ(Char c, XmlDeclarationToken decl)
        {
            while (c != Specification.EOF && c != Specification.DQ)
            {
                stringBuffer.Append(c);
                c = src.Next;
            }

            if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return XmlToken.EOF;
            }

            decl.Version = stringBuffer.ToString();
            return DeclarationAfterVersion(src.Next, decl);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-VersionNum.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationAfterVersion(Char c, XmlDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = src.Next;

            if (src.ContinuesWith("encoding", false))
            {
                src.Advance(7);
                return DeclarationEncodingAfterName(src.Next, decl);
            }

            return DeclarationEnd(c, decl);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-EncodingDecl.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationEncodingAfterName(Char c, XmlDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = src.Next;

            if (c == Specification.EQ)
                return DeclarationEncodingBeforeValue(src.Next, decl);

            throw new ArgumentException("Invalid XML declaration.");
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-EncodingDecl.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationEncodingBeforeValue(Char c, XmlDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = src.Next;

            if (c == Specification.DQ)
            {
                stringBuffer.Clear();
                return DeclarationEncodingValueDQ(src.Next, decl);
            }
            else if (c == Specification.SQ)
            {
                stringBuffer.Clear();
                return DeclarationEncodingValueSQ(src.Next, decl);
            }

            throw new ArgumentException("Invalid XML declaration.");
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-EncodingDecl.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationEncodingValueSQ(Char c, XmlDeclarationToken decl)
        {
            while (c != Specification.EOF && c != Specification.SQ)
            {
                stringBuffer.Append(c);
                c = src.Next;
            }

            if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return XmlToken.EOF;
            }

            decl.Encoding = stringBuffer.ToString();
            return DeclarationAfterEncoding(src.Next, decl);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-EncodingDecl.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationEncodingValueDQ(Char c, XmlDeclarationToken decl)
        {
            while (c != Specification.EOF && c != Specification.DQ)
            {
                stringBuffer.Append(c);
                c = src.Next;
            }

            if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return XmlToken.EOF;
            }

            decl.Encoding = stringBuffer.ToString();
            return DeclarationAfterEncoding(src.Next, decl);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-SDDecl.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationAfterEncoding(Char c, XmlDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = src.Next;

            if (src.ContinuesWith("standalone", false))
            {
                src.Advance(9);
                return DeclarationStandaloneAfterName(src.Next, decl);
            }

            return DeclarationEnd(c, decl);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-SDDecl.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationStandaloneAfterName(Char c, XmlDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = src.Next;

            if (c == Specification.EQ)
                return DeclarationStandaloneBeforeValue(src.Next, decl);

            throw new ArgumentException("Invalid XML declaration.");
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-SDDecl.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationStandaloneBeforeValue(Char c, XmlDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = src.Next;

            if (c == Specification.DQ)
            {
                stringBuffer.Clear();
                return DeclarationStandaloneValueDQ(src.Next, decl);
            }
            else if (c == Specification.SQ)
            {
                stringBuffer.Clear();
                return DeclarationStandaloneValueSQ(src.Next, decl);
            }

            throw new ArgumentException("Invalid XML declaration.");
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-SDDecl.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationStandaloneValueSQ(Char c, XmlDeclarationToken decl)
        {
            while (c != Specification.EOF && c != Specification.SQ)
            {
                stringBuffer.Append(c);
                c = src.Next;
            }

            if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return XmlToken.EOF;
            }

            var s = stringBuffer.ToString();

            if (s.Equals("yes"))
                decl.Standalone = true;
            else if (s.Equals("no"))
                decl.Standalone = false;
            else
                throw new ArgumentException("Invalid XML declaration.");

            return DeclarationEnd(src.Next, decl);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-SDDecl.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationStandaloneValueDQ(Char c, XmlDeclarationToken decl)
        {
            while (c != Specification.EOF && c != Specification.DQ)
            {
                stringBuffer.Append(c);
                c = src.Next;
            }

            if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return XmlToken.EOF;
            }

            var s = stringBuffer.ToString();

            if (s.Equals("yes"))
                decl.Standalone = true;
            else if (s.Equals("no"))
                decl.Standalone = false;
            else
                throw new ArgumentException("Invalid XML declaration.");

            return DeclarationEnd(src.Next, decl);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-XMLDecl.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlDeclarationToken DeclarationEnd(Char c, XmlDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = src.Next;

            if (c != Specification.QM || src.Next != Specification.GT)
                throw new ArgumentException("Invalid XML declaration.");

            return decl;
        }

        #endregion

        #region Comments

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken CommentStart(Char c)
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
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return XmlToken.Comment(stringBuffer.ToString());
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
                return XmlToken.Comment(stringBuffer.ToString());
            }
            else
            {
                stringBuffer.Append(c);
                return Comment(src.Next);
            }
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken CommentDashStart(Char c)
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
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return XmlToken.Comment(stringBuffer.ToString());
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
                return XmlToken.Comment(stringBuffer.ToString());
            }

            stringBuffer.Append(Specification.DASH);
            stringBuffer.Append(c);
            return Comment(src.Next);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken Comment(Char c)
        {
            while (true)
            {
                if (c == Specification.DASH)
                    return CommentDashEnd(src.Next);
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    src.Back();
                    return XmlToken.Comment(stringBuffer.ToString());
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
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken CommentDashEnd(Char c)
        {
            if (c == Specification.DASH)
                return CommentEnd(src.Next);
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
                return XmlToken.Comment(stringBuffer.ToString());
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
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken CommentEnd(Char c)
        {
            if (c == Specification.GT)
            {
                return XmlToken.Comment(stringBuffer.ToString());
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
                return XmlToken.Comment(stringBuffer.ToString());
            }

            RaiseErrorOccurred(ErrorCode.CommentEndedUnexpected);
            stringBuffer.Append(Specification.DASH);
            stringBuffer.Append(Specification.DASH);
            stringBuffer.Append(c);
            return Comment(src.Next);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken CommentBangEnd(Char c)
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
                return XmlToken.Comment(stringBuffer.ToString());
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
                return XmlToken.Comment(stringBuffer.ToString());
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
        XmlToken Doctype(Char c)
        {
            if (c.IsSpaceCharacter())
                return DoctypeNameBefore(src.Next);
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
                return XmlToken.Doctype();
            }

            RaiseErrorOccurred(ErrorCode.DoctypeUnexpected);
            return DoctypeNameBefore(c);
        }

        /// <summary>
        /// See 8.2.4.53 Before DOCTYPE name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken DoctypeNameBefore(Char c)
        {
            while (c.IsSpaceCharacter())
                c = src.Next;

            if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                stringBuffer.Clear();
                stringBuffer.Append(Specification.REPLACEMENT);
                return DoctypeName(src.Next, XmlToken.Doctype());
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return XmlToken.Doctype();
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
                return XmlToken.Doctype();
            }

            stringBuffer.Clear();
            stringBuffer.Append(c);
            return DoctypeName(src.Next, XmlToken.Doctype());
        }

        /// <summary>
        /// See 8.2.4.54 DOCTYPE name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        XmlToken DoctypeName(Char c, XmlDoctypeToken doctype)
        {
            while (true)
            {
                if (c.IsSpaceCharacter())
                {
                    doctype.Name = stringBuffer.ToString();
                    stringBuffer.Clear();
                    return DoctypeNameAfter(src.Next, doctype);
                }
                else if (c == Specification.GT)
                {
                    doctype.Name = stringBuffer.ToString();
                    return doctype;
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    src.Back();
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
        XmlToken DoctypeNameAfter(Char c, XmlDoctypeToken doctype)
        {
            while (c.IsSpaceCharacter())
                c = src.Next;

            if (c == Specification.GT)
            {
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
                return doctype;
            }
            else if (src.ContinuesWith("PUBLIC", false))
            {
                src.Advance(5);
                return DoctypePublic(src.Next, doctype);
            }
            else if (src.ContinuesWith("SYSTEM", false))
            {
                src.Advance(5);
                return DoctypeSystem(src.Next, doctype);
            }

            RaiseErrorOccurred(ErrorCode.DoctypeUnexpectedAfterName);
            return BogusDoctype(src.Next, doctype);
        }

        /// <summary>
        /// See 8.2.4.56 After DOCTYPE public keyword state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        XmlToken DoctypePublic(Char c, XmlDoctypeToken doctype)
        {
            if (c.IsSpaceCharacter())
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
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypePublicInvalid);
            return BogusDoctype(src.Next, doctype);
        }

        /// <summary>
        /// See 8.2.4.57 Before DOCTYPE public identifier state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        XmlToken DoctypePublicIdentifierBefore(Char c, XmlDoctypeToken doctype)
        {
            while (c.IsSpaceCharacter())
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
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypePublicInvalid);
            return BogusDoctype(src.Next, doctype);
        }

        /// <summary>
        /// See 8.2.4.58 DOCTYPE public identifier (double-quoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        XmlToken DoctypePublicIdentifierDoubleQuoted(Char c, XmlDoctypeToken doctype)
        {
            while (true)
            {
                if (c == Specification.DQ)
                {
                    doctype.PublicIdentifier = stringBuffer.ToString();
                    return DoctypePublicIdentifierAfter(src.Next, doctype); ;
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.GT)
                {
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    doctype.PublicIdentifier = stringBuffer.ToString();
                    return doctype;
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    src.Back();
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
        XmlToken DoctypePublicIdentifierSingleQuoted(Char c, XmlDoctypeToken doctype)
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
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    doctype.PublicIdentifier = stringBuffer.ToString();
                    return doctype;
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
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
        XmlToken DoctypePublicIdentifierAfter(Char c, XmlDoctypeToken doctype)
        {
            if (c.IsSpaceCharacter())
            {
                stringBuffer.Clear();
                return DoctypeBetween(src.Next, doctype);
            }
            else if (c == Specification.GT)
            {
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
                src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
            return BogusDoctype(src.Next, doctype);
        }

        /// <summary>
        /// See 8.2.4.61 Between DOCTYPE public and system identifiers state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        XmlToken DoctypeBetween(Char c, XmlDoctypeToken doctype)
        {
            while (c.IsSpaceCharacter())
                c = src.Next;

            if (c == Specification.GT)
            {
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
                src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
            return BogusDoctype(src.Next, doctype);
        }

        /// <summary>
        /// See 8.2.4.62 After DOCTYPE system keyword state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        XmlToken DoctypeSystem(Char c, XmlDoctypeToken doctype)
        {
            if (c.IsSpaceCharacter())
            {
                return DoctypeSystemIdentifierBefore(src.Next, doctype);
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
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.SystemIdentifier = stringBuffer.ToString();
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypeSystemInvalid);
            return BogusDoctype(src.Next, doctype);
        }

        /// <summary>
        /// See 8.2.4.63 Before DOCTYPE system identifier state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        XmlToken DoctypeSystemIdentifierBefore(Char c, XmlDoctypeToken doctype)
        {
            while (c.IsSpaceCharacter())
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
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.SystemIdentifier = stringBuffer.ToString();
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.SystemIdentifier = stringBuffer.ToString();
                src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
            return BogusDoctype(src.Next, doctype);
        }

        /// <summary>
        /// See 8.2.4.64 DOCTYPE system identifier (double-quoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        XmlToken DoctypeSystemIdentifierDoubleQuoted(Char c, XmlDoctypeToken doctype)
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
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    doctype.SystemIdentifier = stringBuffer.ToString();
                    return doctype;
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
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
        XmlToken DoctypeSystemIdentifierSingleQuoted(Char c, XmlDoctypeToken doctype)
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
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    doctype.SystemIdentifier = stringBuffer.ToString();
                    return doctype;
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
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
        XmlToken DoctypeSystemIdentifierAfter(Char c, XmlDoctypeToken doctype)
        {
            while (c.IsSpaceCharacter())
                c = src.Next;

            if (c == Specification.GT)
            {
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
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
        XmlToken BogusDoctype(Char c, XmlDoctypeToken doctype)
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
                    return doctype;
                }

                c = src.Next;
            }
        }

        #endregion

        #region Attributes

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-Attribute.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        XmlToken AttributeBeforeName(Char c, XmlTagToken tag)
        {
            while (c.IsSpaceCharacter())
                c = src.Next;

            if (c == Specification.SOLIDUS)
            {
                return TagSelfClosing(src.Next, tag);
            }
            else if (c == Specification.GT)
            {
                return EmitTag(tag);
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
                return XmlToken.EOF;
            }
            else
            {
                stringBuffer.Clear();
                stringBuffer.Append(c);
                return AttributeName(src.Next, tag);
            }
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-Attribute.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        XmlToken AttributeName(Char c, XmlTagToken tag)
        {
            while (true)
            {
                if (c.IsSpaceCharacter())
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
                {
                    return XmlToken.EOF;
                }
                else
                    stringBuffer.Append(c);

                c = src.Next;
            }
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-Attribute.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        XmlToken AttributeAfterName(Char c, XmlTagToken tag)
        {
            while (c.IsSpaceCharacter())
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
                return XmlToken.EOF;
            }
            else
            {
                stringBuffer.Clear();
                stringBuffer.Append(c);
                return AttributeName(src.Next, tag);
            }
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-Attribute.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        XmlToken AttributeBeforeValue(Char c, XmlTagToken tag)
        {
            while (c.IsSpaceCharacter())
                c = src.Next;

            if (c == Specification.DQ)
            {
                stringBuffer.Clear();
                return AttributeDoubleQuotedValue(src.Next, tag);
            }
            else if (c == Specification.SQ)
            {
                stringBuffer.Clear();
                return AttributeSingleQuotedValue(src.Next, tag);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return EmitTag(tag);
            }
            else if (c == Specification.EOF)
            {
                return XmlToken.EOF;
            }

            throw new ArgumentException("Argument values have to be double or single quoted.");
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-Attribute.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        XmlToken AttributeDoubleQuotedValue(Char c, XmlTagToken tag)
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
                    var value = CharacterReference(src.Next);

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
                    return XmlToken.EOF;
                else
                    stringBuffer.Append(c);

                c = src.Next;
            }
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-Attribute.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        XmlToken AttributeSingleQuotedValue(Char c, XmlTagToken tag)
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
                    var value = CharacterReference(src.Next);

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
                    return XmlToken.EOF;
                else
                    stringBuffer.Append(c);

                c = src.Next;
            }
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-Attribute.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        XmlToken AttributeAfterValue(Char c, XmlTagToken tag)
        {
            if (c.IsSpaceCharacter())
                return AttributeBeforeName(src.Next, tag);
            else if (c == Specification.SOLIDUS)
                return TagSelfClosing(src.Next, tag);
            else if (c == Specification.GT)
                return EmitTag(tag);
            else if (c == Specification.EOF)
                return XmlTagToken.EOF;

            RaiseErrorOccurred(ErrorCode.AttributeNameExpected);
            return AttributeBeforeName(c, tag);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Emits the current token as a tag token.
        /// </summary>
        XmlTagToken EmitTag(XmlTagToken tag)
        {
            if (tag.Type == XmlTokenType.StartTag)
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
            }
            else
            {
                if (tag.IsSelfClosing) RaiseErrorOccurred(ErrorCode.EndTagCannotBeSelfClosed);
                if (tag.Attributes.Count > 0) RaiseErrorOccurred(ErrorCode.EndTagCannotHaveAttributes);
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
