using AngleSharp.DOM;
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
        const String YES = "yes";
        const String NO = "no";

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
            _dtd.AddEntity(new Entity
            {
                NodeName = "amp",
                NodeValue = "&"
            });
            _dtd.AddEntity(new Entity
            {
                NodeName = "lt",
                NodeValue = "<"
            });
            _dtd.AddEntity(new Entity
            {
                NodeName = "gt",
                NodeValue = ">"
            });
            _dtd.AddEntity(new Entity
            {
                NodeName = "apos",
                NodeValue = "'"
            });
            _dtd.AddEntity(new Entity
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
                    throw Errors.Xml(ErrorCode.CharacterReferenceInvalidNumber);

                return Char.ConvertFromUtf32(num);
            }
            else
            {
                var entity = _dtd.GetEntity(entityToken.Value);

                if (entity == null)
                    throw Errors.Xml(ErrorCode.CharacterReferenceInvalidCode);

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

                case Specification.EOF:
                    return XmlToken.EOF;

                case Specification.SBC:
                    return CheckCharacter(_src.Next);

                default:
                    return XmlToken.Character(c);
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
            if (ch == Specification.SBC)
            {
                if (_src.Next == Specification.GT)
                    throw Errors.Xml(ErrorCode.XmlInvalidCharData);

                _src.Back();
            }

            _src.Back();
            return XmlToken.Character(Specification.SBC);
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
                if (c == Specification.EOF)
                    throw Errors.Xml(ErrorCode.EOF);
                
                if (c == Specification.SBC && _src.ContinuesWith("]]>"))
                {
                    _src.Advance(2);
                    break;
                }

                _stringBuffer.Append(c);
                c = _src.Next;
            }

            return XmlToken.CData(_stringBuffer.ToString());
        }

        /// <summary>
        /// Called once an &amp; character is being seen.
        /// </summary>
        /// <param name="c">The next character after the &amp; character.</param>
        /// <returns>The entity token.</returns>
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

            throw Errors.Xml(ErrorCode.CharacterReferenceNotTerminated);
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
                return MarkupDeclaration(_src.Next);

            if (c == Specification.QM)
            {
                c = _src.Next;

                if (_src.ContinuesWith(Tags.XML, false))
                {
                    _src.Advance(2);
                    return DeclarationStart(_src.Next);
                }

                return ProcessingStart(c);
            }

            if (c == Specification.SOLIDUS)
                return TagEnd(_src.Next);
            
            if (c.IsXmlNameStart())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return TagName(_src.Next, XmlToken.OpenTag());
            }

            throw Errors.Xml(ErrorCode.XmlInvalidStartTag);
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
            
            if (c == Specification.EOF)
                throw Errors.Xml(ErrorCode.EOF);

            throw Errors.Xml(ErrorCode.XmlInvalidEndTag);
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
                c = _src.Next;
            }

            tag.Name = _stringBuffer.ToString();

            if (c == Specification.EOF)
                throw Errors.Xml(ErrorCode.EOF);

            if (c == Specification.GT)
                return tag;
            else if (c.IsSpaceCharacter())
                return AttributeBeforeName(_src.Next, tag);
            else if (c == Specification.SOLIDUS)
                return TagSelfClosing(_src.Next, tag);

            throw Errors.Xml(ErrorCode.XmlInvalidName);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#d0e2480.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="tag">The current tag token.</param>
        XmlToken TagSelfClosing(Char c, XmlTagToken tag)
        {
            tag.IsSelfClosing = true;

            if (c == Specification.GT)
                return tag;
            
            if (c == Specification.EOF)
                throw Errors.Xml(ErrorCode.EOF);

            throw Errors.Xml(ErrorCode.XmlInvalidName);
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

            throw Errors.Xml(ErrorCode.UndefinedMarkupDeclaration);
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

            throw Errors.Xml(ErrorCode.XmlDeclarationInvalid);
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

            throw Errors.Xml(ErrorCode.XmlDeclarationInvalid);
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

            if (c == Specification.DQ || c == Specification.SQ)
            {
                _stringBuffer.Clear();
                return DeclarationVersionValue(_src.Next, c, decl);
            }

            throw Errors.Xml(ErrorCode.XmlDeclarationInvalid);
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
                if (c == Specification.EOF)
                    throw Errors.Xml(ErrorCode.EOF);

                _stringBuffer.Append(c);
                c = _src.Next;
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

            throw Errors.Xml(ErrorCode.XmlDeclarationInvalid);
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

            throw Errors.Xml(ErrorCode.XmlDeclarationInvalid);
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
                    throw Errors.Xml(ErrorCode.XmlDeclarationInvalid);
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

            throw Errors.Xml(ErrorCode.XmlDeclarationInvalid);
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

            if (c == Specification.DQ || c == Specification.SQ)
            {
                _stringBuffer.Clear();
                return DeclarationStandaloneValue(_src.Next, c, decl);
            }

            throw Errors.Xml(ErrorCode.XmlDeclarationInvalid);
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
                if (c == Specification.EOF)
                    throw Errors.Xml(ErrorCode.EOF);

                _stringBuffer.Append(c);
                c = _src.Next;
            }

            var s = _stringBuffer.ToString();

            if (s.Equals(YES))
                decl.Standalone = true;
            else if (s.Equals(NO))
                decl.Standalone = false;
            else
                throw Errors.Xml(ErrorCode.XmlDeclarationInvalid);

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
                throw Errors.Xml(ErrorCode.XmlDeclarationInvalid);

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

            throw Errors.Xml(ErrorCode.DoctypeInvalid);
        }

        /// <summary>
        /// See 8.2.4.53 Before DOCTYPE name state
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken DoctypeNameBefore(Char c)
        {
            while (c.IsSpaceCharacter())
                c = _src.Next;

            if (c.IsXmlNameStart())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return DoctypeName(_src.Next, XmlToken.Doctype());
            }

            throw Errors.Xml(ErrorCode.DoctypeInvalid);
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
                c = _src.Next;
            }

            doctype.Name = _stringBuffer.ToString();
            _stringBuffer.Clear();

            if (c == Specification.GT)
                return doctype;
            else if(c.IsSpaceCharacter())
                return DoctypeNameAfter(_src.Next, doctype);

            throw Errors.Xml(ErrorCode.DoctypeInvalid);
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
                return doctype;

            if (_src.ContinuesWith(PUBLIC, false))
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

            throw Errors.Xml(ErrorCode.DoctypeInvalid);
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
                    c = _src.Next;

                if (c == Specification.DQ || c == Specification.SQ)
                {
                    doctype.PublicIdentifier = String.Empty;
                    return DoctypePublicIdentifierValue(_src.Next, c, doctype);
                }
            }

            throw Errors.Xml(ErrorCode.DoctypeInvalid);
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
                    throw Errors.Xml(ErrorCode.XmlInvalidPubId);

                _stringBuffer.Append(c);
                c = _src.Next;
            }

            doctype.PublicIdentifier = _stringBuffer.ToString();
            _stringBuffer.Clear();
            return DoctypePublicIdentifierAfter(_src.Next, doctype);
        }

        /// <summary>
        /// See 8.2.4.60 After DOCTYPE public identifier state
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="doctype">The current doctype token.</param>
        /// <returns>The emitted token.</returns>
        XmlToken DoctypePublicIdentifierAfter(Char c, XmlDoctypeToken doctype)
        {
            if (c == Specification.GT)
                return doctype;
            else if (c.IsSpaceCharacter())
                return DoctypeBetween(_src.Next, doctype);

            throw Errors.Xml(ErrorCode.DoctypeInvalid);
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
                return doctype;
            
            if (c == Specification.DQ || c == Specification.SQ)
            {
                doctype.SystemIdentifier = String.Empty;
                return DoctypeSystemIdentifierValue(_src.Next, c, doctype);
            }

            throw Errors.Xml(ErrorCode.DoctypeInvalid);
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
                    c = _src.Next;

                if (c == Specification.DQ || c == Specification.SQ)
                {
                    doctype.SystemIdentifier = String.Empty;
                    return DoctypeSystemIdentifierValue(_src.Next, c, doctype);
                }
            }

            throw Errors.Xml(ErrorCode.DoctypeInvalid);
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
                if (c == Specification.EOF)
                    throw Errors.Xml(ErrorCode.EOF);

                _stringBuffer.Append(c);
                c = _src.Next;
            }

            doctype.SystemIdentifier = _stringBuffer.ToString();
            _stringBuffer.Clear();
            return DoctypeSystemIdentifierAfter(_src.Next, doctype);
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
                return doctype;

            throw Errors.Xml(ErrorCode.DoctypeInvalid);
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
                return TagSelfClosing(_src.Next, tag);
            else if (c == Specification.GT)
                return tag;
            else if (c == Specification.EOF)
                throw Errors.Xml(ErrorCode.EOF);

            if (c.IsXmlNameStart())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return AttributeName(_src.Next, tag);
            }

            throw Errors.Xml(ErrorCode.XmlInvalidAttribute);
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
                c = _src.Next;
            }

            var name = _stringBuffer.ToString();

            if(!String.IsNullOrEmpty(tag.GetAttribute(name)))
                throw Errors.Xml(ErrorCode.XmlUniqueAttribute);

            tag.AddAttribute(name);

            if (c.IsSpaceCharacter())
            {
                do c = _src.Next;
                while (c.IsSpaceCharacter());
            }
            
            if (c == Specification.EQ)
                return AttributeBeforeValue(_src.Next, tag);

            throw Errors.Xml(ErrorCode.XmlInvalidAttribute);
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

            throw Errors.Xml(ErrorCode.XmlInvalidAttribute);
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
                if (c == Specification.EOF)
                    throw Errors.Xml(ErrorCode.EOF);

                if (c == Specification.AMPERSAND)
                    _stringBuffer.Append(GetEntity(CharacterReference(_src.Next)));
                else if (c == Specification.LT)
                    throw Errors.Xml(ErrorCode.XmlLtInAttributeValue);
                else 
                    _stringBuffer.Append(c);

                c = _src.Next;
            }

            tag.SetAttributeValue(_stringBuffer.ToString());
            return AttributeAfterValue(_src.Next, tag);
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
                return tag;

            throw Errors.Xml(ErrorCode.XmlInvalidAttribute);
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
                return ProcessingTarget(_src.Next, XmlToken.Processing());
            }

            throw Errors.Xml(ErrorCode.XmlInvalidPI);
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
                c = _src.Next;
            }

            pi.Target = _stringBuffer.ToString();
            _stringBuffer.Clear();

            if (String.Compare(pi.Target, Tags.XML, StringComparison.OrdinalIgnoreCase) == 0)
                throw Errors.Xml(ErrorCode.XmlInvalidPI);

            if (c == Specification.QM)
            {
                c = _src.Next;

                if (c == Specification.GT)
                    return pi;
            }
            else if (c.IsSpaceCharacter())
                return ProcessingContent(_src.Next, pi);

            throw Errors.Xml(ErrorCode.XmlInvalidPI);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-pi.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="pi">The processing instruction token.</param>
        XmlToken ProcessingContent(Char c, XmlPIToken pi)
        {
            while (c != Specification.EOF)
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
                else
                {
                    _stringBuffer.Append(c);
                    c = _src.Next;
                }
            }

            throw Errors.Xml(ErrorCode.EOF);
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
                if (c == Specification.MINUS)
                    return CommentDash(_src.Next);

                _stringBuffer.Append(c);
                c = _src.Next;
            }

            throw Errors.Xml(ErrorCode.XmlInvalidComment);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken CommentDash(Char c)
        {
            if (c == Specification.MINUS)
                return CommentEnd(_src.Next);
            
            return Comment(c);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken CommentEnd(Char c)
        {
            if (c == Specification.GT)
                return XmlToken.Comment(_stringBuffer.ToString());

            throw Errors.Xml(ErrorCode.XmlInvalidComment);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Scans the internal subset, i.e. the DTD in [] of the current source.
        /// </summary>
        /// <param name="doctype">The doctype which contains the subset.</param>
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
