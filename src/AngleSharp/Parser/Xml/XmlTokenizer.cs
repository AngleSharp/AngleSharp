namespace AngleSharp.Parser.Xml
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Services;
    using System;

    /// <summary>
    /// Performs the tokenization of the source code. Most of
    /// the information is taken from http://www.w3.org/TR/REC-xml/.
    /// </summary>
    sealed class XmlTokenizer : BaseTokenizer
    {
        #region Fields

        private readonly IEntityProvider _resolver;
        private TextPosition _position;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new tokenizer for XML documents.
        /// </summary>
        /// <param name="source">The source code manager.</param>
        /// <param name="resolver">The entity resolver to use.</param>
        public XmlTokenizer(TextSource source, IEntityProvider resolver)
            : base(source)
        {
            _resolver = resolver;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if some errors should be suppressed.
        /// </summary>
        public Boolean IsSuppressingErrors
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the next available token.
        /// </summary>
        /// <returns>The next available token.</returns>
        public XmlToken Get()
        {
            var current = GetNext();

            if (current != Symbols.EndOfFile)
            {
                _position = GetCurrentPosition();
                return Data(current);
            }

            return NewEof();
        }

        #endregion

        #region General

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-logical-struct.
        /// </summary>
        /// <param name="c">The next input character.</param>
        private XmlToken Data(Char c)
        {
            switch (c)
            {
                case Symbols.LessThan:
                    return TagOpen();

                case Symbols.EndOfFile:
                    return NewEof();

                default:
                    return DataText(c);
            }
        }

        private XmlToken DataText(Char c)
        {
            while (true)
            {
                switch (c)
                {
                    case Symbols.LessThan:
                    case Symbols.EndOfFile:
                        Back();
                        return NewCharacters();

                    case Symbols.Ampersand:
                        StringBuffer.Append(CharacterReference());
                        c = GetNext();
                        break;

                    case Symbols.SquareBracketClose:
                        StringBuffer.Append(c);
                        c = CheckNextCharacter();
                        break;

                    default:
                        StringBuffer.Append(c);
                        c = GetNext();
                        break;
                }
            }
        }

        #endregion

        #region CDATA

        /// <summary>
        /// Checks if the character sequence is equal to ]]&gt;.
        /// </summary>
        /// <returns>The given character.</returns>
        private Char CheckNextCharacter()
        {
            var c = GetNext();

            if (c == Symbols.SquareBracketClose)
            {
                if (GetNext() == Symbols.GreaterThan)
                    throw XmlParseError.XmlInvalidCharData.At(GetCurrentPosition());

                Back();
            }

            return c;
        }

        /// <summary>
        /// See http://www.w3.org/TR/REC-xml/#NT-CData.
        /// </summary>
        private XmlCDataToken CData()
        {
            var c = GetNext();

            while (true)
            {
                if (c == Symbols.EndOfFile)
                    throw XmlParseError.EOF.At(GetCurrentPosition());

                if (c == Symbols.SquareBracketClose && ContinuesWithSensitive("]]>"))
                {
                    Advance(2);
                    break;
                }

                StringBuffer.Append(c);
                c = GetNext();
            }

            return NewCharacterData();
        }

        /// <summary>
        /// Called once an &amp; character is being seen.
        /// </summary>
        /// <returns>The entity token.</returns>
        private String CharacterReference()
        {
            var c = GetNext();
            var start = StringBuffer.Length;
            var hex = false;
            var numeric = c == Symbols.Num;

            if (numeric)
            {
                c = GetNext();
                hex = c == 'x' || c == 'X';

                if (hex)
                {
                    c = GetNext();

                    while (c.IsHex())
                    {
                        StringBuffer.Append(c);
                        c = GetNext();
                    }
                }
                else
                {
                    while (c.IsDigit())
                    {
                        StringBuffer.Append(c);
                        c = GetNext();
                    }
                }
            }
            else if (c.IsXmlNameStart())
            {
                do
                {
                    StringBuffer.Append(c);
                    c = GetNext();
                }
                while (c.IsXmlName());
            }

            if (c == Symbols.Semicolon && StringBuffer.Length > start)
            {
                var length = StringBuffer.Length - start;
                var content = StringBuffer.ToString(start, length);

                if (numeric)
                {
                    var number = numeric ? content.FromHex() : content.FromDec();

                    if (number.IsValidAsCharRef())
                    {
                        StringBuffer.Remove(start, length);
                        return number.ConvertFromUtf32();
                    }
                }
                else
                {
                    var entity = _resolver.GetSymbol(content);

                    if (!String.IsNullOrEmpty(entity))
                    {
                        StringBuffer.Remove(start, length);
                        return entity;
                    }
                }

                if (!IsSuppressingErrors)
                    throw XmlParseError.CharacterReferenceInvalidCode.At(_position);

                StringBuffer.Append(c);
            }

            if (!IsSuppressingErrors)
                throw XmlParseError.CharacterReferenceNotTerminated.At(GetCurrentPosition());

            StringBuffer.Insert(start, Symbols.Ampersand);
            return String.Empty;
        }

        #endregion

        #region Tags

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-starttags.
        /// </summary>
        private XmlToken TagOpen()
        {
            var c = GetNext();

            if (c == Symbols.ExclamationMark)
            {
                return MarkupDeclaration();
            }
            else if (c == Symbols.QuestionMark)
            {
                c = GetNext();

                if (ContinuesWithSensitive(TagNames.Xml))
                {
                    Advance(2);
                    return DeclarationStart();
                }

                return ProcessingStart(c);
            }
            else if (c == Symbols.Solidus)
            {
                return TagEnd();
            }
            else if (c.IsXmlNameStart())
            {
                StringBuffer.Append(c);
                return TagName(NewOpenTag());
            }

            throw XmlParseError.XmlInvalidStartTag.At(GetCurrentPosition());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#dt-etag.
        /// </summary>
        private XmlToken TagEnd()
        {
            var c = GetNext();

            if (c.IsXmlNameStart())
            {
                do
                {
                    StringBuffer.Append(c);
                    c = GetNext();
                }
                while (c.IsXmlName());

                while (c.IsSpaceCharacter())
                {
                    c = GetNext();
                }

                if (c == Symbols.GreaterThan)
                {
                    var tag = NewCloseTag();
                    tag.Name = FlushBuffer();
                    return tag;
                }
            }

            if (c == Symbols.EndOfFile)
                throw XmlParseError.EOF.At(GetCurrentPosition());

            throw XmlParseError.XmlInvalidEndTag.At(GetCurrentPosition());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-Name.
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        private XmlToken TagName(XmlTagToken tag)
        {
            var c = GetNext();

            while (c.IsXmlName())
            {
                StringBuffer.Append(c);
                c = GetNext();
            }

            tag.Name = FlushBuffer();

            if (c == Symbols.EndOfFile)
                throw XmlParseError.EOF.At(GetCurrentPosition());

            if (c == Symbols.GreaterThan)
            {
                return tag;
            }
            else if (c.IsSpaceCharacter())
            {
                return AttributeBeforeName(tag);
            }
            else if (c == Symbols.Solidus)
            {
                return TagSelfClosing(tag);
            }

            throw XmlParseError.XmlInvalidName.At(GetCurrentPosition());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#d0e2480.
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        private XmlToken TagSelfClosing(XmlTagToken tag)
        {
            var c = GetNext();
            tag.IsSelfClosing = true;

            if (c == Symbols.GreaterThan)
            {
                return tag;
            }

            if (c == Symbols.EndOfFile)
                throw XmlParseError.EOF.At(GetCurrentPosition());

            throw XmlParseError.XmlInvalidName.At(GetCurrentPosition());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#dt-markup.
        /// </summary>
        private XmlToken MarkupDeclaration()
        {
            var c = GetNext();

            if (ContinuesWithSensitive("--"))
            {
                Advance();
                return CommentStart();
            }
            else if (ContinuesWithSensitive(TagNames.Doctype))
            {
                Advance(6);
                return Doctype();
            }
            else if (ContinuesWithSensitive(Keywords.CData))
            {
                Advance(6);
                return CData();
            }

            throw XmlParseError.UndefinedMarkupDeclaration.At(GetCurrentPosition());
        }

        #endregion

        #region XML Declaration

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-XMLDecl.
        /// </summary>
        private XmlToken DeclarationStart()
        {
            var c = GetNext();

            if (!c.IsSpaceCharacter())
            {
                StringBuffer.Append(TagNames.Xml);
                return ProcessingTarget(c, NewProcessing());
            }

            do c = GetNext();
            while (c.IsSpaceCharacter());

            if (ContinuesWithSensitive(AttributeNames.Version))
            {
                Advance(6);
                return DeclarationVersionAfterName(NewDeclaration());
            }

            throw XmlParseError.XmlDeclarationInvalid.At(GetCurrentPosition());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-VersionInfo.
        /// </summary>
        /// <param name="decl">The current declaration token.</param>
        private XmlToken DeclarationVersionAfterName(XmlDeclarationToken decl)
        {
            var c = SkipSpaces();

            if (c != Symbols.Equality)
                throw XmlParseError.XmlDeclarationInvalid.At(GetCurrentPosition());

            return DeclarationVersionBeforeValue(decl);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-VersionInfo.
        /// </summary>
        /// <param name="decl">The current declaration token.</param>
        private XmlToken DeclarationVersionBeforeValue(XmlDeclarationToken decl)
        {
            var c = SkipSpaces();

            if (c != Symbols.DoubleQuote && c != Symbols.SingleQuote)
                throw XmlParseError.XmlDeclarationInvalid.At(GetCurrentPosition());

            return DeclarationVersionValue(decl, c);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-VersionInfo.
        /// </summary>
        /// <param name="decl">The current declaration token.</param>
        /// <param name="quote">The quote character.</param>
        private XmlToken DeclarationVersionValue(XmlDeclarationToken decl, Char quote)
        {
            var c = GetNext();

            while (c != quote)
            {
                if (c == Symbols.EndOfFile)
                    throw XmlParseError.EOF.At(GetCurrentPosition());

                StringBuffer.Append(c);
                c = GetNext();
            }

            decl.Version = FlushBuffer();
            c = GetNext();

            if (c.IsSpaceCharacter())
            {
                return DeclarationAfterVersion(decl);
            }

            return DeclarationEnd(c, decl);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-VersionNum.
        /// </summary>
        /// <param name="decl">The current declaration token.</param>
        private XmlToken DeclarationAfterVersion(XmlDeclarationToken decl)
        {
            var c = SkipSpaces();

            if (ContinuesWithSensitive(AttributeNames.Encoding))
            {
                Advance(7);
                return DeclarationEncodingAfterName(decl);
            }
            else if (ContinuesWithSensitive(AttributeNames.Standalone))
            {
                Advance(9);
                return DeclarationStandaloneAfterName(decl);
            }

            return DeclarationEnd(c, decl);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-EncodingDecl.
        /// </summary>
        /// <param name="decl">The current declaration token.</param>
        private XmlToken DeclarationEncodingAfterName(XmlDeclarationToken decl)
        {
            var c = SkipSpaces();

            if (c != Symbols.Equality)
                throw XmlParseError.XmlDeclarationInvalid.At(GetCurrentPosition());

            return DeclarationEncodingBeforeValue(decl);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-EncodingDecl.
        /// </summary>
        /// <param name="decl">The current declaration token.</param>
        private XmlToken DeclarationEncodingBeforeValue(XmlDeclarationToken decl)
        {
            var c = SkipSpaces();

            if (c == Symbols.DoubleQuote || c == Symbols.SingleQuote)
            {
                var q = c;
                c = GetNext();

                if (c.IsLetter())
                {
                    StringBuffer.Append(c);
                    return DeclarationEncodingValue(decl, q);
                }
            }

            throw XmlParseError.XmlDeclarationInvalid.At(GetCurrentPosition());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-EncodingDecl.
        /// </summary>
        /// <param name="decl">The current declaration token.</param>
        /// <param name="quote">The quote character.</param>
        private XmlToken DeclarationEncodingValue(XmlDeclarationToken decl, Char quote)
        {
            var c = GetNext();

            while (c != quote)
            {
                if (!c.IsAlphanumericAscii() && c != Symbols.Dot && c != Symbols.Underscore && c != Symbols.Minus)
                    throw XmlParseError.XmlDeclarationInvalid.At(GetCurrentPosition());

                StringBuffer.Append(c);
                c = GetNext();
            }

            decl.Encoding = FlushBuffer();
            c = GetNext();

            if (c.IsSpaceCharacter())
            {
                return DeclarationAfterEncoding(decl);
            }

            return DeclarationEnd(c, decl);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-SDDecl.
        /// </summary>
        /// <param name="decl">The current declaration token.</param>
        private XmlToken DeclarationAfterEncoding(XmlDeclarationToken decl)
        {
            var c = SkipSpaces();

            if (ContinuesWithSensitive(AttributeNames.Standalone))
            {
                Advance(9);
                return DeclarationStandaloneAfterName(decl);
            }

            return DeclarationEnd(c, decl);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-SDDecl.
        /// </summary>
        /// <param name="decl">The current declaration token.</param>
        private XmlToken DeclarationStandaloneAfterName(XmlDeclarationToken decl)
        {
            var c = SkipSpaces();

            if (c != Symbols.Equality)
                throw XmlParseError.XmlDeclarationInvalid.At(GetCurrentPosition());

            return DeclarationStandaloneBeforeValue(decl);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-SDDecl.
        /// </summary>
        /// <param name="decl">The current declaration token.</param>
        private XmlToken DeclarationStandaloneBeforeValue(XmlDeclarationToken decl)
        {
            var c = SkipSpaces();

            if (c != Symbols.DoubleQuote && c != Symbols.SingleQuote)
                throw XmlParseError.XmlDeclarationInvalid.At(GetCurrentPosition());

            return DeclarationStandaloneValue(decl, c);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-SDDecl.
        /// </summary>
        /// <param name="decl">The current declaration token.</param>
        /// <param name="quote">The quote character.</param>
        private XmlToken DeclarationStandaloneValue(XmlDeclarationToken decl, Char quote)
        {
            var c = GetNext();

            while (c != quote)
            {
                if (c == Symbols.EndOfFile)
                    throw XmlParseError.EOF.At(GetCurrentPosition());

                StringBuffer.Append(c);
                c = GetNext();
            }

            var s = FlushBuffer();

            if (s.Is(Keywords.Yes))
            {
                decl.Standalone = true;
            }
            else if (s.Is(Keywords.No))
            {
                decl.Standalone = false;
            }
            else
            {
                throw XmlParseError.XmlDeclarationInvalid.At(GetCurrentPosition());
            }

            return DeclarationEnd(GetNext(), decl);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-XMLDecl.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        private XmlDeclarationToken DeclarationEnd(Char c, XmlDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
            {
                c = GetNext();
            }

            if (c != Symbols.QuestionMark || GetNext() != Symbols.GreaterThan)
                throw XmlParseError.XmlDeclarationInvalid.At(GetCurrentPosition());

            return decl;
        }

        #endregion

        #region Doctype

        /// <summary>
        /// See 8.2.4.52 DOCTYPE state
        /// </summary>
        private XmlToken Doctype()
        {
            var c = GetNext();

            if (c.IsSpaceCharacter())
            {
                return DoctypeNameBefore();
            }

            throw XmlParseError.DoctypeInvalid.At(GetCurrentPosition());
        }

        /// <summary>
        /// See 8.2.4.53 Before DOCTYPE name state
        /// </summary>
        private XmlToken DoctypeNameBefore()
        {
            var c = SkipSpaces();

            if (c.IsXmlNameStart())
            {
                StringBuffer.Append(c);
                return DoctypeName(NewDoctype());
            }

            throw XmlParseError.DoctypeInvalid.At(GetCurrentPosition());
        }

        /// <summary>
        /// See 8.2.4.54 DOCTYPE name state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        private XmlToken DoctypeName(XmlDoctypeToken doctype)
        {
            var c = GetNext();

            while (c.IsXmlName())
            {
                StringBuffer.Append(c);
                c = GetNext();
            }

            doctype.Name = FlushBuffer();

            if (c == Symbols.GreaterThan)
            {
                return doctype;
            }
            else if (c.IsSpaceCharacter())
            {
                return DoctypeNameAfter(doctype);
            }

            throw XmlParseError.DoctypeInvalid.At(GetCurrentPosition());
        }

        /// <summary>
        /// See 8.2.4.55 After DOCTYPE name state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        private XmlToken DoctypeNameAfter(XmlDoctypeToken doctype)
        {
            var c = SkipSpaces();

            if (c == Symbols.GreaterThan)
            {
                return doctype;
            }
            else if (ContinuesWithSensitive(Keywords.Public))
            {
                Advance(5);
                return DoctypePublic(doctype);
            }
            else if (ContinuesWithSensitive(Keywords.System))
            {
                Advance(5);
                return DoctypeSystem(doctype);
            }
            else if (c == Symbols.SquareBracketOpen)
            {
                Advance();
                return DoctypeAfter(GetNext(), doctype);
            }

            throw XmlParseError.DoctypeInvalid.At(GetCurrentPosition());
        }

        /// <summary>
        /// See 8.2.4.56 After DOCTYPE public keyword state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        private XmlToken DoctypePublic(XmlDoctypeToken doctype)
        {
            var c = GetNext();

            if (c.IsSpaceCharacter())
            {
                c = SkipSpaces();

                if (c == Symbols.DoubleQuote || c == Symbols.SingleQuote)
                {
                    doctype.PublicIdentifier = String.Empty;
                    return DoctypePublicIdentifierValue(doctype, c);
                }
            }

            throw XmlParseError.DoctypeInvalid.At(GetCurrentPosition());
        }

        /// <summary>
        /// See 8.2.4.58 DOCTYPE public identifier (double-quoted) state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <param name="quote">The closing character.</param>
        /// <returns>The emitted token.</returns>
        private XmlToken DoctypePublicIdentifierValue(XmlDoctypeToken doctype, Char quote)
        {
            var c = GetNext();

            while (c != quote)
            {
                if (!c.IsPubidChar())
                    throw XmlParseError.XmlInvalidPubId.At(GetCurrentPosition());

                StringBuffer.Append(c);
                c = GetNext();
            }

            doctype.PublicIdentifier = FlushBuffer();
            return DoctypePublicIdentifierAfter(doctype);
        }

        /// <summary>
        /// See 8.2.4.60 After DOCTYPE public identifier state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        private XmlToken DoctypePublicIdentifierAfter(XmlDoctypeToken doctype)
        {
            var c = GetNext();

            if (c == Symbols.GreaterThan)
            {
                return doctype;
            }
            else if (c.IsSpaceCharacter())
            {
                return DoctypeBetween(doctype);
            }

            throw XmlParseError.DoctypeInvalid.At(GetCurrentPosition());
        }

        /// <summary>
        /// See 8.2.4.61 Between DOCTYPE public and system identifiers state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        private XmlToken DoctypeBetween(XmlDoctypeToken doctype)
        {
            var c = SkipSpaces();

            if (c == Symbols.GreaterThan)
            {
                return doctype;
            }
            else if (c == Symbols.DoubleQuote || c == Symbols.SingleQuote)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierValue(doctype, c);
            }

            throw XmlParseError.DoctypeInvalid.At(GetCurrentPosition());
        }

        /// <summary>
        /// See 8.2.4.62 After DOCTYPE system keyword state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        private XmlToken DoctypeSystem(XmlDoctypeToken doctype)
        {
            var c = GetNext();

            if (c.IsSpaceCharacter())
            {
                c = SkipSpaces();

                if (c == Symbols.DoubleQuote || c == Symbols.SingleQuote)
                {
                    doctype.SystemIdentifier = String.Empty;
                    return DoctypeSystemIdentifierValue(doctype, c);
                }
            }

            throw XmlParseError.DoctypeInvalid.At(GetCurrentPosition());
        }

        /// <summary>
        /// See 8.2.4.64 DOCTYPE system identifier (double-quoted) state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <param name="quote">The quote character.</param>
        /// <returns>The emitted token.</returns>
        private XmlToken DoctypeSystemIdentifierValue(XmlDoctypeToken doctype, Char quote)
        {
            var c = GetNext();

            while (c != quote)
            {
                if (c == Symbols.EndOfFile)
                    throw XmlParseError.EOF.At(GetCurrentPosition());

                StringBuffer.Append(c);
                c = GetNext();
            }

            doctype.SystemIdentifier = FlushBuffer();
            return DoctypeSystemIdentifierAfter(doctype);
        }

        /// <summary>
        /// See 8.2.4.66 After DOCTYPE system identifier state
        /// </summary>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        private XmlToken DoctypeSystemIdentifierAfter(XmlDoctypeToken doctype)
        {
            var c = SkipSpaces();

            if (c == Symbols.SquareBracketOpen)
            {
                Advance();
                c = GetNext();
            }

            return DoctypeAfter(c, doctype);
        }

        /// <summary>
        /// The doctype finalizer.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        private XmlToken DoctypeAfter(Char c, XmlDoctypeToken doctype)
        {
            while (c.IsSpaceCharacter())
            {
                c = GetNext();
            }

            if (c == Symbols.GreaterThan)
            {
                return doctype;
            }

            throw XmlParseError.DoctypeInvalid.At(GetCurrentPosition());
        }

        #endregion

        #region Attributes

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-Attribute.
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        private XmlToken AttributeBeforeName(XmlTagToken tag)
        {
            var c = SkipSpaces();

            if (c == Symbols.Solidus)
            {
                return TagSelfClosing(tag);
            }
            else if (c == Symbols.GreaterThan)
            {
                return tag;
            }

            if (c == Symbols.EndOfFile)
                throw XmlParseError.EOF.At(GetCurrentPosition());

            if (c.IsXmlNameStart())
            {
                StringBuffer.Append(c);
                return AttributeName(tag);
            }

            throw XmlParseError.XmlInvalidAttribute.At(GetCurrentPosition());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-Attribute.
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        private XmlToken AttributeName(XmlTagToken tag)
        {
            var c = GetNext();

            while (c.IsXmlName())
            {
                StringBuffer.Append(c);
                c = GetNext();
            }

            var name = FlushBuffer();

            if (!String.IsNullOrEmpty(tag.GetAttribute(name)))
                throw XmlParseError.XmlUniqueAttribute.At(GetCurrentPosition());

            tag.AddAttribute(name);

            if (c.IsSpaceCharacter())
            {
                do c = GetNext();
                while (c.IsSpaceCharacter());
            }
            
            if (c != Symbols.Equality)
                throw XmlParseError.XmlInvalidAttribute.At(GetCurrentPosition());

            return AttributeBeforeValue(tag);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-Attribute.
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        private XmlToken AttributeBeforeValue(XmlTagToken tag)
        {
            var c = SkipSpaces();

            if (c != Symbols.DoubleQuote && c != Symbols.SingleQuote)
                throw XmlParseError.XmlInvalidAttribute.At(GetCurrentPosition());

            return AttributeValue(tag, c);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-Attribute.
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        /// <param name="quote">The quote character.</param>
        private XmlToken AttributeValue(XmlTagToken tag, Char quote)
        {
            var c = GetNext();

            while (c != quote)
            {
                if (c == Symbols.EndOfFile)
                    throw XmlParseError.EOF.At(GetCurrentPosition());

                if (c == Symbols.LessThan)
                    throw XmlParseError.XmlLtInAttributeValue.At(GetCurrentPosition());

                if (c == Symbols.Ampersand)
                {
                    StringBuffer.Append(CharacterReference());
                }
                else
                {
                    StringBuffer.Append(c);
                }

                c = GetNext();
            }

            tag.SetAttributeValue(FlushBuffer());
            return AttributeAfterValue(tag);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-Attribute.
        /// </summary>
        /// <param name="tag">The current tag token.</param>
        private XmlToken AttributeAfterValue(XmlTagToken tag)
        {
            var c = GetNext();

            if (c.IsSpaceCharacter())
            {
                return AttributeBeforeName(tag);
            }
            else if (c == Symbols.Solidus)
            {
                return TagSelfClosing(tag);
            }
            else if (c == Symbols.GreaterThan)
            {
                return tag;
            }

            throw XmlParseError.XmlInvalidAttribute.At(GetCurrentPosition());
        }

        #endregion

        #region Processing Instruction

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-pi.
        /// </summary>
        /// <param name="c">The next input character.</param>
        private XmlToken ProcessingStart(Char c)
        {
            if (c.IsXmlNameStart())
            {
                StringBuffer.Append(c);
                return ProcessingTarget(GetNext(), NewProcessing());
            }

            throw XmlParseError.XmlInvalidPI.At(GetCurrentPosition());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-pi.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="pi">The processing instruction token.</param>
        private XmlToken ProcessingTarget(Char c, XmlPIToken pi)
        {
            while (c.IsXmlName())
            {
                StringBuffer.Append(c);
                c = GetNext();
            }

            pi.Target = FlushBuffer();

            if (pi.Target.Isi(TagNames.Xml))
                throw XmlParseError.XmlInvalidPI.At(GetCurrentPosition());

            if (c == Symbols.QuestionMark)
            {
                c = GetNext();

                if (c == Symbols.GreaterThan)
                {
                    return pi;
                }
            }
            else if (c.IsSpaceCharacter())
            {
                return ProcessingContent(pi);
            }

            throw XmlParseError.XmlInvalidPI.At(GetCurrentPosition());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-pi.
        /// </summary>
        /// <param name="pi">The processing instruction token.</param>
        private XmlToken ProcessingContent(XmlPIToken pi)
        {
            var c = GetNext();

            while (c != Symbols.EndOfFile)
            {
                if (c == Symbols.QuestionMark)
                {
                    c = GetNext();

                    if (c == Symbols.GreaterThan)
                    {
                        pi.Content = FlushBuffer();
                        return pi;
                    }

                    StringBuffer.Append(Symbols.QuestionMark);
                }
                else
                {
                    StringBuffer.Append(c);
                    c = GetNext();
                }
            }

            throw XmlParseError.EOF.At(GetCurrentPosition());
        }

        #endregion

        #region Comments

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        private XmlToken CommentStart()
        {
            return Comment(GetNext());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        private XmlToken Comment(Char c)
        {
            while (c.IsXmlChar())
            {
                if (c == Symbols.Minus)
                {
                    return CommentDash();
                }

                StringBuffer.Append(c);
                c = GetNext();
            }

            throw XmlParseError.XmlInvalidComment.At(GetCurrentPosition());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        private XmlToken CommentDash()
        {
            var c = GetNext();

            if (c == Symbols.Minus)
            {
                return CommentEnd();
            }
            
            return Comment(c);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        private XmlToken CommentEnd()
        {
            var c = GetNext();

            if (c == Symbols.GreaterThan)
            {
                return NewComment();
            }

            throw XmlParseError.XmlInvalidComment.At(GetCurrentPosition());
        }

        #endregion

        #region Tokens

        private XmlEndOfFileToken NewEof()
        {
            return new XmlEndOfFileToken(GetCurrentPosition());
        }

        private XmlCharacterToken NewCharacters()
        {
            var content = FlushBuffer();
            return new XmlCharacterToken(_position, content);
        }

        private XmlCommentToken NewComment()
        {
            var comment = FlushBuffer();
            return new XmlCommentToken(_position, comment);
        }

        private XmlPIToken NewProcessing()
        {
            return new XmlPIToken(_position);
        }

        private XmlDoctypeToken NewDoctype()
        {
            return new XmlDoctypeToken(_position);
        }

        private XmlDeclarationToken NewDeclaration()
        {
            return new XmlDeclarationToken(_position);
        }

        private XmlTagToken NewOpenTag()
        {
            return new XmlTagToken(XmlTokenType.StartTag, _position);
        }

        private XmlTagToken NewCloseTag()
        {
            return new XmlTagToken(XmlTokenType.EndTag, _position);
        }

        private XmlCDataToken NewCharacterData()
        {
            var content = FlushBuffer();
            return new XmlCDataToken(_position, content);
        }

        #endregion
    }
}
