using AngleSharp.DTD;
using System;
using System.Diagnostics;

namespace AngleSharp.Xml
{
    /// <summary>
    /// Performs the tokenization of the source code. Most of
    /// the information is taken from http://www.w3.org/TR/REC-xml/.
    /// </summary>
    [DebuggerStepThrough]
    sealed class XmlTokenizer : BaseTokenizer
    {
        #region Constants

        const String CDATA = "[CDATA[";
        const String PUBLIC = "PUBLIC";
        const String SYSTEM = "SYSTEM";

        #endregion

        #region Members

        DtdContainer _dtd;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new tokenizer for XML documents.
        /// </summary>
        /// <param name="source">The source code manager.</param>
        public XmlTokenizer(SourceManager source)
            : base(source)
        {
            _dtd = new DtdContainer();
            _dtd.AddEntity(new DOM.Entity
            {
                NodeName = "amp",
                NodeValue = "&"
            });
            _dtd.AddEntity(new DOM.Entity
            {
                NodeName = "lt",
                NodeValue = "<"
            });
            _dtd.AddEntity(new DOM.Entity
            {
                NodeName = "gt",
                NodeValue = ">"
            });
            _dtd.AddEntity(new DOM.Entity
            {
                NodeName = "apos",
                NodeValue = "'"
            });
            _dtd.AddEntity(new DOM.Entity
            {
                NodeName = "quot",
                NodeValue = "\""
            });
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
        /// Gets the used DTD.
        /// </summary>
        public DtdContainer DTD
        {
            get { return _dtd; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Resolves the given entity token.
        /// </summary>
        /// <param name="entityToken">The entity token to resolve.</param>
        /// <returns>The string that is contained in the entity token.</returns>
        public String GetEntity(XmlEntityToken entityToken)
        {
            if (entityToken.IsNumeric)
            {
                var num = entityToken.IsHex ? entityToken.Value.FromHex() : entityToken.Value.FromDec();

                if (!num.IsValidAsCharRef())
                    throw Errors.GetException(ErrorCode.CharacterReferenceInvalidNumber);

                return Char.ConvertFromUtf32(num);
            }
            else
            {
                var entity = _dtd.GetEntity(entityToken.Value);

                if (entity == null)
                    throw Errors.GetException(ErrorCode.CharacterReferenceInvalidCode);

                return entity.NodeValue;
            }
        }

        /// <summary>
        /// Gets the next available token.
        /// </summary>
        /// <returns>The next available token.</returns>
        public XmlToken Get()
        {
            if (_src.IsEnded) 
                return XmlToken.EOF;

            XmlToken token = Data(_src.Current);
            _src.Advance();
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
                    return CharacterReference(_src.Next);

                case Specification.LT:
                    return TagOpen(_src.Next);

                case Specification.NULL:
                    RaiseErrorOccurred(ErrorCode.NULL);
                    return Data(_src.Next);

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
            _stringBuffer.Clear();

            while (true)
            {
                if (c == Specification.EOF)
                {
                    _src.Back();
                    break;
                }
                else if (c == Specification.SBC && _src.ContinuesWith("]]>"))
                {
                    _src.Advance(2);
                    break;
                }

                _stringBuffer.Append(c);
                c = _src.Next;
            }

            return XmlToken.CData(_stringBuffer.ToString());
        }

        XmlEntityToken CharacterReference(Char c)
        {
            _stringBuffer.Clear();

            if (c == Specification.NUM)
            {
                c = _src.Next;
                var hex = c == 'x' || c == 'X';

                if (hex)
                {
                    c = _src.Next;

                    while (c.IsHex())
                    {
                        _stringBuffer.Append(c);
                        c = _src.Next;
                    }
                }
                else
                {
                    while (c.IsDigit())
                    {
                        _stringBuffer.Append(c);
                        c = _src.Next;
                    }
                }

                if (_stringBuffer.Length > 0 && c == Specification.SC)
                    return new XmlEntityToken { Value = _stringBuffer.ToString(), IsNumeric = true, IsHex = hex };
            }
            else if (c.IsXmlNameStart())
            {
                do
                {
                    _stringBuffer.Append(c);
                    c = _src.Next;
                }
                while (c.IsXmlName());

                if (c == Specification.SC)
                    return new XmlEntityToken { Value = _stringBuffer.ToString() };
            }

            throw Errors.GetException(ErrorCode.CharacterReferenceNotTerminated);
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
                return MarkupDeclaration(_src.Next);
            }
            else if (c == Specification.QM)
            {
                c = _src.Next;

                if (_src.ContinuesWith(Tags.XML, false))
                {
                    _src.Advance(2);
                    return DeclarationStart(_src.Next);
                }

                return ProcessingStart(c);
            }
            else if (c == Specification.SOLIDUS)
            {
                return TagEnd(_src.Next);
            }
            else if (c.IsXmlNameStart())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return TagName(_src.Next, XmlToken.OpenTag());
            }

            throw Errors.GetException(ErrorCode.XmlInvalidStartTag);
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
                    c = _src.Next;
                }
                while (c.IsXmlName());

                while (c.IsSpaceCharacter())
                    c = _src.Next;

                if (c == Specification.GT)
                {
                    var tag = XmlToken.CloseTag();
                    tag.Name = _stringBuffer.ToString();
                    return tag;
                }
            }
            else if (c == Specification.EOF)
            {
                _src.Back();
                RaiseErrorOccurred(ErrorCode.EOF);
                return XmlToken.EOF;
            }

            throw Errors.GetException(ErrorCode.XmlInvalidEndTag);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-Name.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        /// <returns>The emitted token.</returns>
        XmlToken TagName(Char c, XmlTagToken tag)
        {
            while (true)
            {
                if (c.IsSpaceCharacter())
                {
                    tag.Name = _stringBuffer.ToString();
                    return AttributeBeforeName(_src.Next, tag);
                }
                else if (c == Specification.SOLIDUS)
                {
                    tag.Name = _stringBuffer.ToString();
                    return TagSelfClosing(_src.Next, tag);
                }
                else if (c == Specification.GT)
                {
                    tag.Name = _stringBuffer.ToString();
                    return EmitTag(tag);
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    _stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    return XmlToken.EOF;
                }
                else
                    _stringBuffer.Append(c);

                c = _src.Next;
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
            if (_src.ContinuesWith("--"))
            {
                _src.Advance();
                return CommentStart(_src.Next);
            }
            else if (_src.ContinuesWith(Tags.DOCTYPE, false))
            {
                _src.Advance(6);
                return Doctype(_src.Next);
            }
            else if (_src.ContinuesWith(CDATA, false))
            {
                _src.Advance(6);
                return CData(_src.Next);
            }

            throw Errors.GetException(ErrorCode.UndefinedMarkupDeclaration);
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
                _stringBuffer.Append(Tags.XML);
                return ProcessingTarget(c, XmlToken.Processing());
            }

            do c = _src.Next;
            while (c.IsSpaceCharacter());

            if (_src.ContinuesWith(AttributeNames.VERSION, false))
            {
                _src.Advance(6);
                return DeclarationVersionAfterName(_src.Next, XmlToken.Declaration());
            }

            throw Errors.GetException(ErrorCode.XmlDeclarationInvalid);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-VersionInfo.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationVersionAfterName(Char c, XmlDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = _src.Next;

            if (c == Specification.EQ)
                return DeclarationVersionBeforeValue(_src.Next, decl);

            throw Errors.GetException(ErrorCode.XmlDeclarationInvalid);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-VersionInfo.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationVersionBeforeValue(Char c, XmlDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = _src.Next;

            if (c == Specification.DQ)
            {
                _stringBuffer.Clear();
                return DeclarationVersionValueDQ(_src.Next, decl);
            }
            else if(c == Specification.SQ)
            {
                _stringBuffer.Clear();
                return DeclarationVersionValueSQ(_src.Next, decl);
            }

            throw Errors.GetException(ErrorCode.XmlDeclarationInvalid);
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
                _stringBuffer.Append(c);
                c = _src.Next;
            }

            if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return XmlToken.EOF;
            }

            decl.Version = _stringBuffer.ToString();
            return DeclarationAfterVersion(_src.Next, decl);
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
                _stringBuffer.Append(c);
                c = _src.Next;
            }

            if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return XmlToken.EOF;
            }

            decl.Version = _stringBuffer.ToString();
            c = _src.Next;

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
                c = _src.Next;

            if (_src.ContinuesWith(AttributeNames.ENCODING, false))
            {
                _src.Advance(7);
                return DeclarationEncodingAfterName(_src.Next, decl);
            }
            else if (_src.ContinuesWith(AttributeNames.STANDALONE, false))
            {
                _src.Advance(9);
                return DeclarationStandaloneAfterName(_src.Next, decl);
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
                c = _src.Next;

            if (c == Specification.EQ)
                return DeclarationEncodingBeforeValue(_src.Next, decl);

            throw Errors.GetException(ErrorCode.XmlDeclarationInvalid);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-EncodingDecl.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationEncodingBeforeValue(Char c, XmlDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = _src.Next;

            if (c == Specification.DQ || c == Specification.SQ)
            {
                var q = c;
                _stringBuffer.Clear();
                c = _src.Next;

                if (c.IsLetter())
                    return DeclarationEncodingValue(c, q, decl);
            }

            throw Errors.GetException(ErrorCode.XmlDeclarationInvalid);
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
                if (c.IsAlphanumericAscii() || c == Specification.DOT || c == Specification.UNDERSCORE || c == Specification.MINUS)
                {
                    _stringBuffer.Append(c);
                    c = _src.Next;
                }
                else
                    throw Errors.GetException(ErrorCode.XmlDeclarationInvalid);
            }
            while (c != q);

            decl.Encoding = _stringBuffer.ToString();
            c = _src.Next;

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
                c = _src.Next;

            if (_src.ContinuesWith(AttributeNames.STANDALONE, false))
            {
                _src.Advance(9);
                return DeclarationStandaloneAfterName(_src.Next, decl);
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
                c = _src.Next;

            if (c == Specification.EQ)
                return DeclarationStandaloneBeforeValue(_src.Next, decl);

            throw Errors.GetException(ErrorCode.XmlDeclarationInvalid);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-SDDecl.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlToken DeclarationStandaloneBeforeValue(Char c, XmlDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = _src.Next;

            if (c == Specification.DQ)
            {
                _stringBuffer.Clear();
                return DeclarationStandaloneValueDQ(_src.Next, decl);
            }
            else if (c == Specification.SQ)
            {
                _stringBuffer.Clear();
                return DeclarationStandaloneValueSQ(_src.Next, decl);
            }

            throw Errors.GetException(ErrorCode.XmlDeclarationInvalid);
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
                _stringBuffer.Append(c);
                c = _src.Next;
            }

            if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return XmlToken.EOF;
            }

            var s = _stringBuffer.ToString();

            if (s.Equals("yes"))
                decl.Standalone = true;
            else if (s.Equals("no"))
                decl.Standalone = false;
            else
                throw Errors.GetException(ErrorCode.XmlDeclarationInvalid);

            return DeclarationEnd(_src.Next, decl);
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
                _stringBuffer.Append(c);
                c = _src.Next;
            }

            if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                return XmlToken.EOF;
            }

            var s = _stringBuffer.ToString();

            if (s.Equals("yes"))
                decl.Standalone = true;
            else if (s.Equals("no"))
                decl.Standalone = false;
            else
                throw Errors.GetException(ErrorCode.XmlDeclarationInvalid);

            return DeclarationEnd(_src.Next, decl);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-XMLDecl.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="decl">The current declaration token.</param>
        XmlDeclarationToken DeclarationEnd(Char c, XmlDeclarationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = _src.Next;

            if (c != Specification.QM || _src.Next != Specification.GT)
                throw Errors.GetException(ErrorCode.XmlDeclarationInvalid);

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
                return DoctypeNameBefore(_src.Next);
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
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
                c = _src.Next;

            if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _stringBuffer.Clear();
                _stringBuffer.Append(Specification.REPLACEMENT);
                return DoctypeName(_src.Next, XmlToken.Doctype());
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return XmlToken.Doctype();
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return XmlToken.Doctype();
            }

            _stringBuffer.Clear();
            _stringBuffer.Append(c);
            return DoctypeName(_src.Next, XmlToken.Doctype());
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
                    doctype.Name = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return DoctypeNameAfter(_src.Next, doctype);
                }
                else if (c == Specification.GT)
                {
                    doctype.Name = _stringBuffer.ToString();
                    return doctype;
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    _stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    _src.Back();
                    doctype.Name = _stringBuffer.ToString();
                    return doctype;
                }
                else
                    _stringBuffer.Append(c);

                c = _src.Next;
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
                c = _src.Next;

            if (c == Specification.GT)
            {
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return doctype;
            }
            else if (_src.ContinuesWith(PUBLIC, false))
            {
                _src.Advance(5);
                return DoctypePublic(_src.Next, doctype);
            }
            else if (_src.ContinuesWith(SYSTEM, false))
            {
                _src.Advance(5);
                return DoctypeSystem(_src.Next, doctype);
            }
            else if (c == Specification.SBO)
            {
                _src.Advance();
                ScanInternalSubset(doctype);
                return DoctypeAfter(_src.Next, doctype);
            }

            RaiseErrorOccurred(ErrorCode.DoctypeUnexpectedAfterName);
            return BogusDoctype(_src.Next, doctype);
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
                return DoctypePublicIdentifierBefore(_src.Next, doctype);
            }
            else if (c == Specification.DQ)
            {
                RaiseErrorOccurred(ErrorCode.DoubleQuotationMarkUnexpected);
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierDoubleQuoted(_src.Next, doctype);
            }
            else if (c == Specification.SQ)
            {
                RaiseErrorOccurred(ErrorCode.SingleQuotationMarkUnexpected);
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierSingleQuoted(_src.Next, doctype);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypePublicInvalid);
            return BogusDoctype(_src.Next, doctype);
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
                c = _src.Next;

            if (c == Specification.DQ)
            {
                _stringBuffer.Clear();
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierDoubleQuoted(_src.Next, doctype);
            }
            else if (c == Specification.SQ)
            {
                _stringBuffer.Clear();
                doctype.PublicIdentifier = String.Empty;
                return DoctypePublicIdentifierSingleQuoted(_src.Next, doctype);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypePublicInvalid);
            return BogusDoctype(_src.Next, doctype);
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
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    return DoctypePublicIdentifierAfter(_src.Next, doctype); ;
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    _stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.GT)
                {
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    return doctype;
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    _src.Back();
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    return doctype;
                }
                else
                    _stringBuffer.Append(c);

                c = _src.Next;
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
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return DoctypePublicIdentifierAfter(_src.Next, doctype);
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    _stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.GT)
                {
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    return doctype;
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    doctype.PublicIdentifier = _stringBuffer.ToString();
                    _src.Back();
                    return doctype;
                }
                else
                    _stringBuffer.Append(c);

                c = _src.Next;
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
                _stringBuffer.Clear();
                return DoctypeBetween(_src.Next, doctype);
            }
            else if (c == Specification.GT)
            {
                return doctype;
            }
            else if (c == Specification.DQ)
            {
                RaiseErrorOccurred(ErrorCode.DoubleQuotationMarkUnexpected);
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(_src.Next, doctype);
            }
            else if (c == Specification.SQ)
            {
                RaiseErrorOccurred(ErrorCode.SingleQuotationMarkUnexpected);
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierSingleQuoted(_src.Next, doctype);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
            return BogusDoctype(_src.Next, doctype);
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
                c = _src.Next;

            if (c == Specification.GT)
            {
                return doctype;
            }
            else if (c == Specification.DQ)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(_src.Next, doctype);
            }
            else if (c == Specification.SQ)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierSingleQuoted(_src.Next, doctype);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
            return BogusDoctype(_src.Next, doctype);
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
                return DoctypeSystemIdentifierBefore(_src.Next, doctype);
            }
            else if (c == Specification.DQ)
            {
                RaiseErrorOccurred(ErrorCode.DoubleQuotationMarkUnexpected);
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(_src.Next, doctype);
            }
            else if (c == Specification.SQ)
            {
                RaiseErrorOccurred(ErrorCode.SingleQuotationMarkUnexpected);
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierSingleQuoted(_src.Next, doctype);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.SystemIdentifier = _stringBuffer.ToString();
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypeSystemInvalid);
            return BogusDoctype(_src.Next, doctype);
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
                c = _src.Next;

            if (c == Specification.DQ)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierDoubleQuoted(_src.Next, doctype);
            }
            else if (c == Specification.SQ)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierSingleQuoted(_src.Next, doctype);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                doctype.SystemIdentifier = _stringBuffer.ToString();
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                doctype.SystemIdentifier = _stringBuffer.ToString();
                _src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
            return BogusDoctype(_src.Next, doctype);
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
                    doctype.SystemIdentifier = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return DoctypeSystemIdentifierAfter(_src.Next, doctype);
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    _stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.GT)
                {
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    doctype.SystemIdentifier = _stringBuffer.ToString();
                    return doctype;
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    doctype.SystemIdentifier = _stringBuffer.ToString();
                    _src.Back();
                    return doctype;
                }
                else
                    _stringBuffer.Append(c);

                c = _src.Next;
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
                    doctype.SystemIdentifier = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return DoctypeSystemIdentifierAfter(_src.Next, doctype);
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    _stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.GT)
                {
                    RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                    doctype.SystemIdentifier = _stringBuffer.ToString();
                    return doctype;
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    doctype.SystemIdentifier = _stringBuffer.ToString();
                    _src.Back();
                    return doctype;
                }
                else
                    _stringBuffer.Append(c);

                c = _src.Next;
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
                c = _src.Next;

            if (c == Specification.SBO)
            {
                _src.Advance();
                ScanInternalSubset(doctype);
                c = _src.Next;
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
                c = _src.Next;

            if (c == Specification.GT)
            {
                return doctype;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return doctype;
            }

            RaiseErrorOccurred(ErrorCode.DoctypeInvalidCharacter);
            return BogusDoctype(_src.Next, doctype);
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
                    _src.Back();
                    return doctype;
                }
                else if (c == Specification.GT)
                {
                    return doctype;
                }

                c = _src.Next;
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
                c = _src.Next;

            if (c == Specification.SOLIDUS)
            {
                return TagSelfClosing(_src.Next, tag);
            }
            else if (c == Specification.GT)
            {
                return EmitTag(tag);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _stringBuffer.Clear();
                _stringBuffer.Append(Specification.REPLACEMENT);
                return AttributeName(_src.Next, tag);
            }
            else if (c.IsXmlNameStart())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return AttributeName(_src.Next, tag);
            }
            else if (c == Specification.EOF)
                throw Errors.GetException(ErrorCode.EOF);

            throw Errors.GetException(ErrorCode.XmlInvalidStartTag);
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
                    tag.AddAttribute(_stringBuffer.ToString());
                    return AttributeAfterName(_src.Next, tag);
                }
                else if (c == Specification.SOLIDUS)
                {
                    tag.AddAttribute(_stringBuffer.ToString());
                    return TagSelfClosing(_src.Next, tag);
                }
                else if (c == Specification.EQ)
                {
                    tag.AddAttribute(_stringBuffer.ToString());
                    return AttributeBeforeValue(_src.Next, tag);
                }
                else if (c == Specification.GT)
                {
                    tag.AddAttribute(_stringBuffer.ToString());
                    return EmitTag(tag);
                }
                else if (c == Specification.EOF)
                    throw Errors.GetException(ErrorCode.EOF);
                else if (!c.IsXmlName())
                    throw Errors.GetException(ErrorCode.AttributeNameInvalid);

                _stringBuffer.Append(c);
                c = _src.Next;
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
                c = _src.Next;

            if (c == Specification.SOLIDUS)
            {
                return TagSelfClosing(_src.Next, tag);
            }
            else if (c == Specification.EQ)
            {
                return AttributeBeforeValue(_src.Next, tag);
            }
            else if (c == Specification.GT)
            {
                return EmitTag(tag);
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _stringBuffer.Clear();
                _stringBuffer.Append(Specification.REPLACEMENT);
                return AttributeName(_src.Next, tag);
            }
            else if (c == Specification.DQ || c == Specification.SQ || c == Specification.LT)
            {
                RaiseErrorOccurred(ErrorCode.AttributeNameInvalid);
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return AttributeName(_src.Next, tag);
            }
            else if (c == Specification.EOF)
            {
                return XmlToken.EOF;
            }
            else
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return AttributeName(_src.Next, tag);
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
                c = _src.Next;

            if (c == Specification.DQ || c== Specification.SQ)
            {
                _stringBuffer.Clear();
                return AttributeValue(_src.Next, c, tag);
            }

            throw Errors.GetException(ErrorCode.AttributeValueInvalid);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#NT-Attribute.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="q">The quote character.</param>
        /// <param name="tag">The current tag token.</param>
        XmlToken AttributeValue(Char c, Char q, XmlTagToken tag)
        {
            while (true)
            {
                if (c == q)
                {
                    tag.SetAttributeValue(_stringBuffer.ToString());
                    return AttributeAfterValue(_src.Next, tag);
                }
                else if (c == Specification.AMPERSAND)
                {
                    var value = CharacterReference(_src.Next);
                    _stringBuffer.Append(GetEntity(value));
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    _stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.LT)
                    throw Errors.GetException(ErrorCode.XmlLtInAttributeValue);
                else if (c == Specification.EOF)
                    return XmlToken.EOF;
                else
                    _stringBuffer.Append(c);

                c = _src.Next;
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
                return AttributeBeforeName(_src.Next, tag);
            else if (c == Specification.SOLIDUS)
                return TagSelfClosing(_src.Next, tag);
            else if (c == Specification.GT)
                return EmitTag(tag);
            else if (c == Specification.EOF)
                return XmlTagToken.EOF;

            throw Errors.GetException(ErrorCode.XmlInvalidStartTag);
        }

        #endregion

        #region Processing Instruction

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-pi.
        /// </summary>
        /// <param name="c">The next input character.</param>
        protected XmlToken ProcessingStart(Char c)
        {
            if (c.IsLetter())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return ProcessingTarget(_src.Next, XmlToken.Processing());
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
        protected XmlToken ProcessingTarget(Char c, XmlPIToken pi)
        {
            while (true)
            {
                if (c.IsSpaceCharacter())
                {
                    pi.Target = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return ProcessingContent(_src.Next, pi);
                }
                else if (c == Specification.QM)
                {
                    pi.Target = _stringBuffer.ToString();
                    _stringBuffer.Clear();
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

                _stringBuffer.Append(c);
                c = _src.Next;
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
                    c = _src.Next;

                    if (c == Specification.GT)
                    {
                        pi.Content = _stringBuffer.ToString();
                        return pi;
                    }

                    _stringBuffer.Append(Specification.QM);
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    return XmlToken.EOF;
                }
                else
                {
                    _stringBuffer.Append(c);
                    c = _src.Next;
                }
            }
        }

        #endregion

        #region Comments

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        protected XmlToken CommentStart(Char c)
        {
            _stringBuffer.Clear();

            if (c == Specification.MINUS)
                return CommentDashStart(_src.Next);
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _stringBuffer.Append(Specification.REPLACEMENT);
                return Comment(_src.Next);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return XmlToken.Comment(_stringBuffer.ToString());
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return XmlToken.Comment(_stringBuffer.ToString());
            }
            else
            {
                _stringBuffer.Append(c);
                return Comment(_src.Next);
            }
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken CommentDashStart(Char c)
        {
            if (c == Specification.MINUS)
                return CommentEnd(_src.Next);
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _stringBuffer.Append(Specification.MINUS);
                _stringBuffer.Append(Specification.REPLACEMENT);
                return Comment(_src.Next);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return XmlToken.Comment(_stringBuffer.ToString());
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return XmlToken.Comment(_stringBuffer.ToString());
            }

            _stringBuffer.Append(Specification.MINUS);
            _stringBuffer.Append(c);
            return Comment(_src.Next);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken Comment(Char c)
        {
            while (true)
            {
                if (c == Specification.MINUS)
                    return CommentDashEnd(_src.Next);
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    _src.Back();
                    return XmlToken.Comment(_stringBuffer.ToString());
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    c = Specification.REPLACEMENT;
                }

                _stringBuffer.Append(c);
                c = _src.Next;
            }
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken CommentDashEnd(Char c)
        {
            if (c == Specification.MINUS)
                return CommentEnd(_src.Next);
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return XmlToken.Comment(_stringBuffer.ToString());
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                c = Specification.REPLACEMENT;
            }

            _stringBuffer.Append(Specification.MINUS);
            _stringBuffer.Append(c);
            return Comment(_src.Next);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken CommentEnd(Char c)
        {
            if (c == Specification.GT)
            {
                return XmlToken.Comment(_stringBuffer.ToString());
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _stringBuffer.Append(Specification.MINUS);
                _stringBuffer.Append(Specification.REPLACEMENT);
                return Comment(_src.Next);
            }
            else if (c == Specification.EM)
            {
                RaiseErrorOccurred(ErrorCode.CommentEndedWithEM);
                return CommentBangEnd(_src.Next);
            }
            else if (c == Specification.MINUS)
            {
                RaiseErrorOccurred(ErrorCode.CommentEndedWithDash);
                _stringBuffer.Append(Specification.MINUS);
                return CommentEnd(_src.Next);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return XmlToken.Comment(_stringBuffer.ToString());
            }

            RaiseErrorOccurred(ErrorCode.CommentEndedUnexpected);
            _stringBuffer.Append(Specification.MINUS);
            _stringBuffer.Append(Specification.MINUS);
            _stringBuffer.Append(c);
            return Comment(_src.Next);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken CommentBangEnd(Char c)
        {
            if (c == Specification.MINUS)
            {
                _stringBuffer.Append(Specification.MINUS);
                _stringBuffer.Append(Specification.MINUS);
                _stringBuffer.Append(Specification.EM);
                return CommentDashEnd(_src.Next);
            }
            else if (c == Specification.GT)
            {
                return XmlToken.Comment(_stringBuffer.ToString());
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _stringBuffer.Append(Specification.MINUS);
                _stringBuffer.Append(Specification.MINUS);
                _stringBuffer.Append(Specification.EM);
                _stringBuffer.Append(Specification.REPLACEMENT);
                return Comment(_src.Next);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return XmlToken.Comment(_stringBuffer.ToString());
            }

            _stringBuffer.Append(Specification.MINUS);
            _stringBuffer.Append(Specification.MINUS);
            _stringBuffer.Append(Specification.EM);
            _stringBuffer.Append(c);
            return Comment(_src.Next);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Emits the current token as a tag token.
        /// </summary>
        XmlTagToken EmitTag(XmlTagToken tag)
        {
            for (var i = tag.Attributes.Count - 1; i > 0; i--)
            {
                for (var j = i - 1; j >= 0; j--)
                {
                    if (tag.Attributes[j].Key == tag.Attributes[i].Key)
                        throw Errors.GetException(ErrorCode.XmlUniqueAttribute);
                }
            }

            return tag;
        }

        void ScanInternalSubset(XmlDoctypeToken doctype)
        {
            var dtd = new DtdParser(_dtd, _src);
            dtd.IsInternal = true;
            dtd.ErrorOccurred += (s, e) => RaiseErrorOccurred(s, e);
            dtd.Parse();
            doctype.InternalSubset = dtd.Result.Text;
        }

        #endregion
    }
}
