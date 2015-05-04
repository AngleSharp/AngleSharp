namespace AngleSharp.Parser.Xml
{
    using System;
    using System.Diagnostics;
    using AngleSharp.Events;
    using AngleSharp.Extensions;
    using AngleSharp.Html;

    /// <summary>
    /// Performs the tokenization of the source code. Most of
    /// the information is taken from http://www.w3.org/TR/REC-xml/.
    /// </summary>
    [DebuggerStepThrough]
    sealed class XmlTokenizer : BaseTokenizer
    {
        #region Constants

        static readonly String CDataOpening = "[CDATA[";
        static readonly String PublicIdentifier = "PUBLIC";
        static readonly String SystemIdentifier = "SYSTEM";
        static readonly String YesIdentifier = "yes";
        static readonly String NoIdentifier = "no";

        #endregion

        #region Fields

        TextPosition _position;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new tokenizer for XML documents.
        /// </summary>
        /// <param name="source">The source code manager.</param>
        /// <param name="events">The event aggregator to use.</param>
        public XmlTokenizer(TextSource source, IEventAggregator events)
            : base(source, events)
        {
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

            if (current == Symbols.EndOfFile) 
                return NewEof();

            return Data(current);
        }

        #endregion

        #region General

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-logical-struct.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken Data(Char c)
        {
            _position = GetCurrentPosition();

            switch (c)
            {
                case Symbols.Ampersand:
                    return CharacterReference(GetNext());

                case Symbols.LessThan:
                    return TagOpen(GetNext());

                case Symbols.EndOfFile:
                    return NewEof();

                case Symbols.SquareBracketClose:
                    return CheckCharacter(GetNext());

                default:
                    return NewCharacter(c);
            }
        }

        #endregion

        #region CDATA

        /// <summary>
        /// Checks if the character sequence is equal to ]]&gt;.
        /// </summary>
        /// <param name="ch">The character to examine.</param>
        /// <returns>The token if everything is alright.</returns>
        XmlToken CheckCharacter(Char ch)
        {
            if (ch == Symbols.SquareBracketClose)
            {
                if (GetNext() == Symbols.GreaterThan)
                    throw XmlParseError.XmlInvalidCharData.At(GetCurrentPosition());

                Back();
            }

            Back();
            return NewCharacter(Symbols.SquareBracketClose);
        }

        /// <summary>
        /// See http://www.w3.org/TR/REC-xml/#NT-CData.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlCDataToken CData(Char c)
        {
            _stringBuffer.Clear();

            while (true)
            {
                if (c == Symbols.EndOfFile)
                    throw XmlParseError.EOF.At(GetCurrentPosition());
                
                if (c == Symbols.SquareBracketClose && ContinuesWith("]]>"))
                {
                    Advance(2);
                    break;
                }

                _stringBuffer.Append(c);
                c = GetNext();
            }

            return NewCData(_stringBuffer.ToString());
        }

        /// <summary>
        /// Called once an &amp; character is being seen.
        /// </summary>
        /// <param name="c">The next character after the &amp; character.</param>
        /// <returns>The entity token.</returns>
        XmlEntityToken CharacterReference(Char c)
        {
            var buffer = Pool.NewStringBuilder();

            if (c == Symbols.Num)
            {
                c = GetNext();
                var hex = c == 'x' || c == 'X';

                if (hex)
                {
                    c = GetNext();

                    while (c.IsHex())
                    {
                        buffer.Append(c);
                        c = GetNext();
                    }
                }
                else
                {
                    while (c.IsDigit())
                    {
                        buffer.Append(c);
                        c = GetNext();
                    }
                }

                if (buffer.Length > 0 && c == Symbols.Semicolon)
                    return NewEntity(buffer.ToPool(), true, hex);
            }
            else if (c.IsXmlNameStart())
            {
                do
                {
                    buffer.Append(c);
                    c = GetNext();
                }
                while (c.IsXmlName());

                if (c == Symbols.Semicolon)
                    return NewEntity(buffer.ToPool());
            }

            buffer.ToPool();
            throw XmlParseError.CharacterReferenceNotTerminated.At(GetCurrentPosition());
        }

        #endregion

        #region Tags

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-starttags.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken TagOpen(Char c)
        {
            if (c == Symbols.ExclamationMark)
                return MarkupDeclaration(GetNext());

            if (c == Symbols.QuestionMark)
            {
                c = GetNext();

                if (ContinuesWith(Tags.Xml, false))
                {
                    Advance(2);
                    return DeclarationStart(GetNext());
                }

                return ProcessingStart(c);
            }

            if (c == Symbols.Solidus)
                return TagEnd(GetNext());
            
            if (c.IsXmlNameStart())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return TagName(GetNext(), NewOpenTag());
            }

            throw XmlParseError.XmlInvalidStartTag.At(GetCurrentPosition());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#dt-etag.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken TagEnd(Char c)
        {
            if (c.IsXmlNameStart())
            {
                _stringBuffer.Clear();

                do
                {
                    _stringBuffer.Append(c);
                    c = GetNext();
                }
                while (c.IsXmlName());

                while (c.IsSpaceCharacter())
                    c = GetNext();

                if (c == Symbols.GreaterThan)
                {
                    var tag = NewCloseTag();
                    tag.Name = _stringBuffer.ToString();
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
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        XmlToken TagName(Char c, XmlTagToken tag)
        {
            while (c.IsXmlName())
            {
                _stringBuffer.Append(c);
                c = GetNext();
            }

            tag.Name = _stringBuffer.ToString();

            if (c == Symbols.EndOfFile)
                throw XmlParseError.EOF.At(GetCurrentPosition());

            if (c == Symbols.GreaterThan)
                return tag;
            else if (c.IsSpaceCharacter())
                return AttributeBeforeName(GetNext(), tag);
            else if (c == Symbols.Solidus)
                return TagSelfClosing(GetNext(), tag);

            throw XmlParseError.XmlInvalidName.At(GetCurrentPosition());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#d0e2480.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        XmlToken TagSelfClosing(Char c, XmlTagToken tag)
        {
            tag.IsSelfClosing = true;

            if (c == Symbols.GreaterThan)
                return tag;
            
            if (c == Symbols.EndOfFile)
                throw XmlParseError.EOF.At(GetCurrentPosition());

            throw XmlParseError.XmlInvalidName.At(GetCurrentPosition());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#dt-markup.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken MarkupDeclaration(Char c)
        {
            if (ContinuesWith("--"))
            {
                Advance();
                return CommentStart(GetNext());
            }
            else if (ContinuesWith(Tags.Doctype, false))
            {
                Advance(6);
                return Doctype(GetNext());
            }
            else if (ContinuesWith(CDataOpening, false))
            {
                Advance(6);
                return CData(GetNext());
            }

            throw XmlParseError.UndefinedMarkupDeclaration.At(GetCurrentPosition());
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
                _stringBuffer.Clear();
                _stringBuffer.Append(Tags.Xml);
                return ProcessingTarget(c, NewProcessing());
            }

            do c = GetNext();
            while (c.IsSpaceCharacter());

            if (ContinuesWith(AttributeNames.Version, false))
            {
                Advance(6);
                return DeclarationVersionAfterName(GetNext(), NewDeclaration());
            }

            throw XmlParseError.XmlDeclarationInvalid.At(GetCurrentPosition());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-VersionInfo.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationVersionAfterName(Char c, XmlDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = GetNext();

            if (c == Symbols.Equality)
                return DeclarationVersionBeforeValue(GetNext(), decl);

            throw XmlParseError.XmlDeclarationInvalid.At(GetCurrentPosition());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-VersionInfo.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationVersionBeforeValue(Char c, XmlDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = GetNext();

            if (c == Symbols.DoubleQuote || c == Symbols.SingleQuote)
            {
                _stringBuffer.Clear();
                return DeclarationVersionValue(GetNext(), c, decl);
            }

            throw XmlParseError.XmlDeclarationInvalid.At(GetCurrentPosition());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-VersionInfo.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="q">The quote character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationVersionValue(Char c, Char q, XmlDeclarationToken decl)
        {
            while (c != q)
            {
                if (c == Symbols.EndOfFile)
                    throw XmlParseError.EOF.At(GetCurrentPosition());

                _stringBuffer.Append(c);
                c = GetNext();
            }

            decl.Version = _stringBuffer.ToString();
            c = GetNext();

            if (c.IsSpaceCharacter())
                return DeclarationAfterVersion(c, decl);

            return DeclarationEnd(c, decl);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-VersionNum.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationAfterVersion(Char c, XmlDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = GetNext();

            if (ContinuesWith(AttributeNames.Encoding, false))
            {
                Advance(7);
                return DeclarationEncodingAfterName(GetNext(), decl);
            }
            else if (ContinuesWith(AttributeNames.Standalone, false))
            {
                Advance(9);
                return DeclarationStandaloneAfterName(GetNext(), decl);
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
                c = GetNext();

            if (c == Symbols.Equality)
                return DeclarationEncodingBeforeValue(GetNext(), decl);

            throw XmlParseError.XmlDeclarationInvalid.At(GetCurrentPosition());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-EncodingDecl.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationEncodingBeforeValue(Char c, XmlDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = GetNext();

            if (c == Symbols.DoubleQuote || c == Symbols.SingleQuote)
            {
                var q = c;
                _stringBuffer.Clear();
                c = GetNext();

                if (c.IsLetter())
                    return DeclarationEncodingValue(c, q, decl);
            }

            throw XmlParseError.XmlDeclarationInvalid.At(GetCurrentPosition());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-EncodingDecl.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="q">The quote character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationEncodingValue(Char c, Char q, XmlDeclarationToken decl)
        {
            do
            {
                if (c.IsAlphanumericAscii() || c == Symbols.Dot || c == Symbols.Underscore || c == Symbols.Minus)
                {
                    _stringBuffer.Append(c);
                    c = GetNext();
                }
                else
                    throw XmlParseError.XmlDeclarationInvalid.At(GetCurrentPosition());
            }
            while (c != q);

            decl.Encoding = _stringBuffer.ToString();
            c = GetNext();

            if(c.IsSpaceCharacter())
                return DeclarationAfterEncoding(c, decl);

            return DeclarationEnd(c, decl);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-SDDecl.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationAfterEncoding(Char c, XmlDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = GetNext();

            if (ContinuesWith(AttributeNames.Standalone, false))
            {
                Advance(9);
                return DeclarationStandaloneAfterName(GetNext(), decl);
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
                c = GetNext();

            if (c == Symbols.Equality)
                return DeclarationStandaloneBeforeValue(GetNext(), decl);

            throw XmlParseError.XmlDeclarationInvalid.At(GetCurrentPosition());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-SDDecl.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationStandaloneBeforeValue(Char c, XmlDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = GetNext();

            if (c == Symbols.DoubleQuote || c == Symbols.SingleQuote)
            {
                _stringBuffer.Clear();
                return DeclarationStandaloneValue(GetNext(), c, decl);
            }

            throw XmlParseError.XmlDeclarationInvalid.At(GetCurrentPosition());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-SDDecl.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="q">The quote character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationStandaloneValue(Char c, Char q, XmlDeclarationToken decl)
        {
            while (c != q)
            {
                if (c == Symbols.EndOfFile)
                    throw XmlParseError.EOF.At(GetCurrentPosition());

                _stringBuffer.Append(c);
                c = GetNext();
            }

            var s = _stringBuffer.ToString();

            if (s.Equals(YesIdentifier))
                decl.Standalone = true;
            else if (s.Equals(NoIdentifier))
                decl.Standalone = false;
            else
                throw XmlParseError.XmlDeclarationInvalid.At(GetCurrentPosition());

            return DeclarationEnd(GetNext(), decl);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-XMLDecl.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlDeclarationToken DeclarationEnd(Char c, XmlDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = GetNext();

            if (c != Symbols.QuestionMark || GetNext() != Symbols.GreaterThan)
                throw XmlParseError.XmlDeclarationInvalid.At(GetCurrentPosition());

            return decl;
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
                return DoctypeNameBefore(GetNext());

            throw XmlParseError.DoctypeInvalid.At(GetCurrentPosition());
        }

        /// <summary>
        /// See 8.2.4.53 Before DOCTYPE name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken DoctypeNameBefore(Char c)
        {
            while (c.IsSpaceCharacter())
                c = GetNext();

            if (c.IsXmlNameStart())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return DoctypeName(GetNext(), NewDoctype());
            }

            throw XmlParseError.DoctypeInvalid.At(GetCurrentPosition());
        }

        /// <summary>
        /// See 8.2.4.54 DOCTYPE name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        XmlToken DoctypeName(Char c, XmlDoctypeToken doctype)
        {
            while (c.IsXmlName())
            {
                _stringBuffer.Append(c);
                c = GetNext();
            }

            doctype.Name = _stringBuffer.ToString();
            _stringBuffer.Clear();

            if (c == Symbols.GreaterThan)
                return doctype;
            else if(c.IsSpaceCharacter())
                return DoctypeNameAfter(GetNext(), doctype);

            throw XmlParseError.DoctypeInvalid.At(GetCurrentPosition());
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
                c = GetNext();

            if (c == Symbols.GreaterThan)
                return doctype;

            if (ContinuesWith(PublicIdentifier, false))
            {
                Advance(5);
                return DoctypePublic(GetNext(), doctype);
            }
            else if (ContinuesWith(SystemIdentifier, false))
            {
                Advance(5);
                return DoctypeSystem(GetNext(), doctype);
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
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        XmlToken DoctypePublic(Char c, XmlDoctypeToken doctype)
        {
            if (c.IsSpaceCharacter())
            {
                while (c.IsSpaceCharacter())
                    c = GetNext();

                if (c == Symbols.DoubleQuote || c == Symbols.SingleQuote)
                {
                    doctype.PublicIdentifier = String.Empty;
                    return DoctypePublicIdentifierValue(GetNext(), c, doctype);
                }
            }

            throw XmlParseError.DoctypeInvalid.At(GetCurrentPosition());
        }

        /// <summary>
        /// See 8.2.4.58 DOCTYPE public identifier (double-quoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="q">The closing character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        XmlToken DoctypePublicIdentifierValue(Char c, Char q, XmlDoctypeToken doctype)
        {
            while (c != q)
            {
                if (!c.IsPubidChar())
                    throw XmlParseError.XmlInvalidPubId.At(GetCurrentPosition());

                _stringBuffer.Append(c);
                c = GetNext();
            }

            doctype.PublicIdentifier = _stringBuffer.ToString();
            _stringBuffer.Clear();
            return DoctypePublicIdentifierAfter(GetNext(), doctype);
        }

        /// <summary>
        /// See 8.2.4.60 After DOCTYPE public identifier state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        XmlToken DoctypePublicIdentifierAfter(Char c, XmlDoctypeToken doctype)
        {
            if (c == Symbols.GreaterThan)
                return doctype;
            else if (c.IsSpaceCharacter())
                return DoctypeBetween(GetNext(), doctype);

            throw XmlParseError.DoctypeInvalid.At(GetCurrentPosition());
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
                c = GetNext();

            if (c == Symbols.GreaterThan)
                return doctype;
            
            if (c == Symbols.DoubleQuote || c == Symbols.SingleQuote)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierValue(GetNext(), c, doctype);
            }

            throw XmlParseError.DoctypeInvalid.At(GetCurrentPosition());
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
                while (c.IsSpaceCharacter())
                    c = GetNext();

                if (c == Symbols.DoubleQuote || c == Symbols.SingleQuote)
                {
                    doctype.SystemIdentifier = String.Empty;
                    return DoctypeSystemIdentifierValue(GetNext(), c, doctype);
                }
            }

            throw XmlParseError.DoctypeInvalid.At(GetCurrentPosition());
        }

        /// <summary>
        /// See 8.2.4.64 DOCTYPE system identifier (double-quoted) state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="q">The quote character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        XmlToken DoctypeSystemIdentifierValue(Char c, Char q, XmlDoctypeToken doctype)
        {
            while (c != q)
            {
                if (c == Symbols.EndOfFile)
                    throw XmlParseError.EOF.At(GetCurrentPosition());

                _stringBuffer.Append(c);
                c = GetNext();
            }

            doctype.SystemIdentifier = _stringBuffer.ToString();
            _stringBuffer.Clear();
            return DoctypeSystemIdentifierAfter(GetNext(), doctype);
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
                c = GetNext();

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
        XmlToken DoctypeAfter(Char c, XmlDoctypeToken doctype)
        {
            while (c.IsSpaceCharacter())
                c = GetNext();

            if (c == Symbols.GreaterThan)
                return doctype;

            throw XmlParseError.DoctypeInvalid.At(GetCurrentPosition());
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
                c = GetNext();

            if (c == Symbols.Solidus)
                return TagSelfClosing(GetNext(), tag);
            else if (c == Symbols.GreaterThan)
                return tag;
            else if (c == Symbols.EndOfFile)
                throw XmlParseError.EOF.At(GetCurrentPosition());

            if (c.IsXmlNameStart())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return AttributeName(GetNext(), tag);
            }

            throw XmlParseError.XmlInvalidAttribute.At(GetCurrentPosition());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-Attribute.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        XmlToken AttributeName(Char c, XmlTagToken tag)
        {
            while (c.IsXmlName())
            {
                _stringBuffer.Append(c);
                c = GetNext();
            }

            var name = _stringBuffer.ToString();

            if(!String.IsNullOrEmpty(tag.GetAttribute(name)))
                throw XmlParseError.XmlUniqueAttribute.At(GetCurrentPosition());

            tag.AddAttribute(name);

            if (c.IsSpaceCharacter())
            {
                do c = GetNext();
                while (c.IsSpaceCharacter());
            }
            
            if (c == Symbols.Equality)
                return AttributeBeforeValue(GetNext(), tag);

            throw XmlParseError.XmlInvalidAttribute.At(GetCurrentPosition());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-Attribute.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        XmlToken AttributeBeforeValue(Char c, XmlTagToken tag)
        {
            while (c.IsSpaceCharacter())
                c = GetNext();

            if (c == Symbols.DoubleQuote || c== Symbols.SingleQuote)
            {
                _stringBuffer.Clear();
                return AttributeValue(GetNext(), c, tag);
            }

            throw XmlParseError.XmlInvalidAttribute.At(GetCurrentPosition());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-Attribute.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="q">The quote character.</param>
        /// <param name="tag">The current tag token.</param>
        XmlToken AttributeValue(Char c, Char q, XmlTagToken tag)
        {
            while (c != q)
            {
                if (c == Symbols.EndOfFile)
                    throw XmlParseError.EOF.At(GetCurrentPosition());

                if (c == Symbols.Ampersand)
                    _stringBuffer.Append(CharacterReference(GetNext()).GetEntity());
                else if (c == Symbols.LessThan)
                    throw XmlParseError.XmlLtInAttributeValue.At(GetCurrentPosition());
                else 
                    _stringBuffer.Append(c);

                c = GetNext();
            }

            tag.SetAttributeValue(_stringBuffer.ToString());
            return AttributeAfterValue(GetNext(), tag);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-Attribute.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        XmlToken AttributeAfterValue(Char c, XmlTagToken tag)
        {
            if (c.IsSpaceCharacter())
                return AttributeBeforeName(GetNext(), tag);
            else if (c == Symbols.Solidus)
                return TagSelfClosing(GetNext(), tag);
            else if (c == Symbols.GreaterThan)
                return tag;

            throw XmlParseError.XmlInvalidAttribute.At(GetCurrentPosition());
        }

        #endregion

        #region Processing Instruction

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-pi.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken ProcessingStart(Char c)
        {
            if (c.IsXmlNameStart())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return ProcessingTarget(GetNext(), NewProcessing());
            }

            throw XmlParseError.XmlInvalidPI.At(GetCurrentPosition());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-pi.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="pi">The processing instruction token.</param>
        XmlToken ProcessingTarget(Char c, XmlPIToken pi)
        {
            while (c.IsXmlName())
            {
                _stringBuffer.Append(c);
                c = GetNext();
            }

            pi.Target = _stringBuffer.ToString();
            _stringBuffer.Clear();

            if (String.Compare(pi.Target, Tags.Xml, StringComparison.OrdinalIgnoreCase) == 0)
                throw XmlParseError.XmlInvalidPI.At(GetCurrentPosition());

            if (c == Symbols.QuestionMark)
            {
                c = GetNext();

                if (c == Symbols.GreaterThan)
                    return pi;
            }
            else if (c.IsSpaceCharacter())
                return ProcessingContent(GetNext(), pi);

            throw XmlParseError.XmlInvalidPI.At(GetCurrentPosition());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-pi.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="pi">The processing instruction token.</param>
        XmlToken ProcessingContent(Char c, XmlPIToken pi)
        {
            while (c != Symbols.EndOfFile)
            {
                if (c == Symbols.QuestionMark)
                {
                    c = GetNext();

                    if (c == Symbols.GreaterThan)
                    {
                        pi.Content = _stringBuffer.ToString();
                        return pi;
                    }

                    _stringBuffer.Append(Symbols.QuestionMark);
                }
                else
                {
                    _stringBuffer.Append(c);
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
        /// <param name="c">The next input character.</param>
        XmlToken CommentStart(Char c)
        {
            _stringBuffer.Clear();
            return Comment(c);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken Comment(Char c)
        {
            while (c.IsXmlChar())
            {
                if (c == Symbols.Minus)
                    return CommentDash(GetNext());

                _stringBuffer.Append(c);
                c = GetNext();
            }

            throw XmlParseError.XmlInvalidComment.At(GetCurrentPosition());
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken CommentDash(Char c)
        {
            if (c == Symbols.Minus)
                return CommentEnd(GetNext());
            
            return Comment(c);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken CommentEnd(Char c)
        {
            if (c == Symbols.GreaterThan)
                return NewComment(_stringBuffer.ToString());

            throw XmlParseError.XmlInvalidComment.At(GetCurrentPosition());
        }

        #endregion

        #region Tokens

        XmlEndOfFileToken NewEof()
        {
            return new XmlEndOfFileToken(GetCurrentPosition());
        }

        XmlCharacterToken NewCharacter(Char c)
        {
            return new XmlCharacterToken(_position, c);
        }

        XmlCommentToken NewComment(String s)
        {
            return new XmlCommentToken(_position, s);
        }

        XmlPIToken NewProcessing()
        {
            return new XmlPIToken(_position);
        }

        XmlDoctypeToken NewDoctype()
        {
            return new XmlDoctypeToken(_position);
        }

        XmlDeclarationToken NewDeclaration()
        {
            return new XmlDeclarationToken(_position);
        }

        XmlTagToken NewOpenTag()
        {
            return new XmlTagToken(XmlTokenType.StartTag, _position);
        }

        XmlTagToken NewCloseTag()
        {
            return new XmlTagToken(XmlTokenType.EndTag, _position);
        }

        XmlCDataToken NewCData(String s)
        {
            return new XmlCDataToken(_position, s);
        }

        XmlEntityToken NewEntity(String value, Boolean numeric = false, Boolean hex = false)
        {
            return new XmlEntityToken(_position, value, numeric, hex);
        }

        #endregion
    }
}
