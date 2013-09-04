using AngleSharp.DOM;
using AngleSharp.Xml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AngleSharp.DTD
{
    /// <summary>
    /// The tokenizer class for Document Type Definitions.
    /// </summary>
    [DebuggerStepThrough]
    sealed class DtdTokenizer : XmlBaseTokenizer
    {
        #region Members

        IntermediateStream _stream;
        List<Entity> _parameters;

        #endregion

        #region ctor

        public DtdTokenizer(SourceManager src)
            : base(src)
        {
            _stream = new IntermediateStream(src);
            _parameters = new List<Entity>();
            End = Specification.EOF;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sends the final character.
        /// </summary>
        public Char End
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public void AddParameter(Entity entity)
        {
            _parameters.Add(entity);
        }

        /// <summary>
        /// Scans the DTD in the doctype as specified in the
        /// official XML spec and (in German) here:
        /// http://www.stefanheymann.de/xml/dtdxml.htm
        /// </summary>
        public DtdToken Get()
        {
            var c = _stream.Current;

            if (c == End)
                return DtdToken.EOF;

            var element = GetElement(c);
            c = SkipSpaces(c);
            return element;
        }

        #endregion

        #region General

        /// <summary>
        /// Gets the next found DTD element by advancing
        /// and applying the rules for DTD.
        /// </summary>
        /// <param name="c">The current character.</param>
        /// <returns>The found declaration.</returns>
        DtdToken GetElement(Char c)
        {
            while (c.IsSpaceCharacter())
                c = _stream.Next;

            if (c == End)
            {
                return DtdToken.EOF;
            }
            else if (c == Specification.LT)
            {
                c = _stream.Next;

                if (c == Specification.QM)
                {
                    return Rework(ProcessingStart(_stream.Next));
                }
                else if (c == Specification.EM)
                {
                    _stream.Advance();

                    if (_stream.ContinuesWith("ENTITY"))
                    {
                        _stream.Advance(5);
                        return EntityDeclaration(_stream.Next);
                    }
                    else if (_stream.ContinuesWith("ELEMENT"))
                    {
                        _stream.Advance(6);
                        return TypeDeclaration(_stream.Next);
                    }
                    else if (_stream.ContinuesWith("ATTLIST"))
                    {
                        _stream.Advance(6);
                        return AttributeDeclaration(_stream.Next);
                    }
                    else if (_stream.ContinuesWith("NOTATION"))
                    {
                        _stream.Advance(7);
                        return NotationDeclaration(_stream.Next);
                    }
                    else if (_stream.ContinuesWith("--"))
                    {
                        _stream.Advance();
                        return Rework(CommentStart(_stream.Next));
                    }
                }
            }
            else if (c == Specification.PERCENT)
            {
                return PEReference(_stream.Next);
            }

            throw new ArgumentException("Invalid document type declaration.");
        }

        DtdToken Rework(XmlToken xmlToken)
        {
            if (xmlToken is XmlPIToken)
                return new DtdPIToken((XmlPIToken)xmlToken);
            else if (xmlToken is XmlCommentToken)
                return new DtdCommentToken((XmlCommentToken)xmlToken);
            else if (xmlToken is XmlEndOfFileToken)
                return DtdToken.EOF;

            throw new ArgumentException("The received token is not valid for a DTD.");
        }

        #endregion

        #region Declaration Name

        Boolean DeclarationNameBefore(Char c, DtdToken decl)
        {
            while (c.IsSpaceCharacter())
                c = _stream.Next;

            if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _stringBuffer.Clear();
                _stringBuffer.Append(Specification.REPLACEMENT);
                return DeclarationName(_stream.Next, decl);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return false;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _stream.Back();
                return false;
            }

            _stringBuffer.Clear();
            _stringBuffer.Append(c);
            return DeclarationName(_stream.Next, decl);
        }

        Boolean DeclarationName(Char c, DtdToken decl)
        {
            while (true)
            {
                if (c.IsSpaceCharacter())
                {
                    decl.Name = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return DeclarationNameAfter(_stream.Next);
                }
                else if (c == Specification.GT)
                {
                    decl.Name = _stringBuffer.ToString();
                    return false;
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    _stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    _stream.Back();
                    decl.Name = _stringBuffer.ToString();
                    return false;
                }
                else
                    _stringBuffer.Append(c);

                c = _stream.Next;
            }
        }

        Boolean DeclarationNameAfter(Char c)
        {
            while (c.IsSpaceCharacter())
                c = _stream.Next;

            if (c == Specification.GT)
            {
                return false;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _stream.Back();
                return false;
            }

            return true;
        }

        #endregion

        #region Parameter Entity

        DtdToken PEReference(Char c)
        {
            _stringBuffer.Clear();

            if (c.IsXmlNameStart())
            {
                do
                {
                    _stringBuffer.Append(c);
                    c = _stream.Next;
                }
                while (c.IsXmlName());

                if (c == Specification.SC)
                {
                    var temp = _stringBuffer.ToString();

                    foreach (var parameter in _parameters)
                    {
                        if (parameter.NodeName == temp)
                        {
                            _stream.Push(temp.Length + 2, parameter.NodeValue);
                            return GetElement(_stream.Next);
                        }
                    }
                }
            }

            throw new ArgumentException("Invalid parameter entity reference.");
        }

        #endregion

        #region Entity Declaration

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-entity-decl.
        /// </summary>
        /// <param name="c">The next input character.</param>
        DtdToken EntityDeclaration(Char c)
        {
            var decl = new DtdEntityToken();
            var canContinue = false;

            if (c.IsSpaceCharacter())
                canContinue = DeclarationNameBefore(_stream.Next, decl);
            else if (c == Specification.EOF)
                throw new ArgumentException("The document ended unexpectedly.");
            else
            {
                RaiseErrorOccurred(ErrorCode.UndefinedMarkupDeclaration);
                canContinue = DeclarationNameBefore(c, decl);
            }

            if (canContinue && decl.Name == "%")
            {
                decl.IsParameter = true;
                canContinue = DeclarationNameBefore(_stream.Current, decl);
            }

            c = _stream.Current;

            if (canContinue)
            {
                if (_stream.ContinuesWith("SYSTEM"))
                {
                    decl.IsExtern = true;
                    _stream.Advance(5);
                    return EntityDeclarationBeforeValue(_stream.Next, decl);
                }
                else if (_stream.ContinuesWith("PUBLIC"))
                {
                    decl.IsExtern = true;
                    _stream.Advance(5);
                    return EntityDeclarationBeforeSystem(_stream.Next, decl);
                }

                return EntityDeclarationBeforeValue(c, decl);
            }

            return EntityDeclarationAfter(c, decl);
        }

        DtdToken EntityDeclarationBeforeValue(Char c, DtdEntityToken decl)
        {
            while (c.IsSpaceCharacter())
                c = _stream.Next;

            _stringBuffer.Clear();

            switch (c)
            {
                case Specification.DQ:
                case Specification.SQ:
                    return EntityDeclarationValue(_stream.Next, c, decl);
                default:
                    throw new ArgumentException("Declaration invalid.");
            }
        }

        DtdToken EntityDeclarationValue(Char c, Char end, DtdEntityToken decl)
        {
            while (c != end)
            {
                if (c == Specification.EOF)
                    throw new ArgumentException("The document ended unexpectedly.");

                _stringBuffer.Append(c);
                c = _stream.Next;
            }

            decl.Value = _stringBuffer.ToString();
            return EntityDeclarationAfter(_stream.Next, decl);
        }

        DtdToken EntityDeclarationBeforeSystem(Char c, DtdEntityToken decl)
        {
            while (c.IsSpaceCharacter())
                c = _stream.Next;

            _stringBuffer.Clear();

            switch (c)
            {
                case Specification.DQ:
                case Specification.SQ:
                    return EntityDeclarationSystem(_stream.Next, c, decl);
                default:
                    throw new ArgumentException("Declaration invalid.");
            }
        }

        DtdToken EntityDeclarationSystem(Char c, Char quote, DtdEntityToken decl)
        {
            while (c != quote)
            {
                if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    _stream.Back();
                    decl.PublicIdentifier = _stringBuffer.ToString();
                    return decl;
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    _stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c.IsPubidChar())
                    _stringBuffer.Append(c);
                else
                    RaiseErrorOccurred(ErrorCode.InvalidCharacter);

                c = _stream.Next;
            }

            decl.PublicIdentifier = _stringBuffer.ToString();
            return EntityDeclarationBeforeValue(_stream.Next, decl);
        }

        DtdToken EntityDeclarationAfter(Char c, DtdEntityToken decl)
        {
            while (c.IsSpaceCharacter())
                c = _stream.Next;

            if (c == Specification.EOF)
                throw new ArgumentException("The document ended unexpectedly.");
            else if (c == Specification.GT)
                return decl;
            else if (decl.IsExtern && String.IsNullOrEmpty(decl.ExternNotation))
            {
                if (_stream.ContinuesWith("NDATA"))
                {
                    _stream.Advance(4);
                    c = _stream.Next;

                    while (c.IsSpaceCharacter())
                        c = _stream.Next;

                    if (c.IsXmlNameStart())
                    {
                        _stringBuffer.Clear();

                        do
                        {
                            _stringBuffer.Append(c);
                            c = _stream.Next;
                        }
                        while (c.IsXmlName());

                        decl.ExternNotation = _stringBuffer.ToString();
                        return EntityDeclarationAfter(c, decl);
                    }
                }
            }

            throw new ArgumentException("Declaration invalid.");
        }

        #endregion

        #region Attribute Declaration

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#attdecls.
        /// </summary>
        /// <param name="c">The next input character.</param>
        DtdAttributeToken AttributeDeclaration(Char c)
        {
            var decl = new DtdAttributeToken();
            var canContinue = false;

            if (c.IsSpaceCharacter())
                canContinue = DeclarationNameBefore(_stream.Next, decl);
            else if (c == Specification.EOF)
                throw new ArgumentException("The document ended unexpectedly.");
            else
            {
                RaiseErrorOccurred(ErrorCode.UndefinedMarkupDeclaration);
                canContinue = DeclarationNameBefore(c, decl);
            }

            c = _stream.Current;

            if (canContinue)
            {
                while (true)
                {
                    while (c.IsSpaceCharacter())
                        c = _stream.Next;

                    if (c.IsXmlNameStart())
                    {
                        _stringBuffer.Clear();
                        decl.Attributes.Add(AttributeDeclarationName(c));
                        c = _stream.Current;
                        continue;
                    }

                    break;
                }
            }

            return AttributeDeclarationAfter(c, decl);
        }

        AttributeDeclarationEntry AttributeDeclarationName(Char c)
        {
            var value = new AttributeDeclarationEntry();

            while (true)
            {
                if (c.IsSpaceCharacter())
                {
                    value.Name = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    break;
                }
                else if (c == Specification.GT)
                    throw new ArgumentException("Declaration invalid.");
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    _stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == Specification.EOF)
                    throw new ArgumentException("The document ended unexpectedly.");
                else
                    _stringBuffer.Append(c);

                c = _stream.Next;
            }

            return AttributeDeclarationType(_stream.Next, value);
        }

        AttributeDeclarationEntry AttributeDeclarationType(Char c, AttributeDeclarationEntry value)
        {
            while (c.IsSpaceCharacter())
                c = _stream.Next;

            if (c == Specification.RBO)
            {
                var type = new AttributeEnumeratedType();
                value.ValueType = type;
                AttributeDeclarationTypeEnumeration(_stream.Next, type);
            }
            else if (c.IsUppercaseAscii())
            {
                var id = String.Empty;

                while (true)
                {
                    if (c.IsSpaceCharacter())
                    {
                        id = _stringBuffer.ToString();
                        _stringBuffer.Clear();
                        break;
                    }
                    else if (c == Specification.GT)
                        throw new ArgumentException("Declaration invalid.");
                    else if (c == Specification.NULL)
                    {
                        RaiseErrorOccurred(ErrorCode.NULL);
                        _stringBuffer.Append(Specification.REPLACEMENT);
                    }
                    else if (c == Specification.EOF)
                        throw new ArgumentException("The document ended unexpectedly.");
                    else
                        _stringBuffer.Append(c);

                    c = _stream.Next;
                }

                switch (id)
                {
                    case "CDATA":
                        value.ValueType = new AttributeStringType();
                        break;
                    case "ID":
                        value.ValueType = new AttributeTokenizedType { Value = AttributeTokenizedType.TokenizedType.ID };
                        break;
                    case "IDREF":
                        value.ValueType = new AttributeTokenizedType { Value = AttributeTokenizedType.TokenizedType.IDREF };
                        break;
                    case "IDREFS":
                        value.ValueType = new AttributeTokenizedType { Value = AttributeTokenizedType.TokenizedType.IDREFS };
                        break;
                    case "ENTITY":
                        value.ValueType = new AttributeTokenizedType { Value = AttributeTokenizedType.TokenizedType.ENTITY };
                        break;
                    case "ENTITIES":
                        value.ValueType = new AttributeTokenizedType { Value = AttributeTokenizedType.TokenizedType.ENTITIES };
                        break;
                    case "NMTOKEN":
                        value.ValueType = new AttributeTokenizedType { Value = AttributeTokenizedType.TokenizedType.NMTOKEN };
                        break;
                    case "NMTOKENS":
                        value.ValueType = new AttributeTokenizedType { Value = AttributeTokenizedType.TokenizedType.NMTOKENS };
                        break;
                    case "NOTATION":
                        var type = new AttributeEnumeratedType { IsNotation = true };
                        value.ValueType = type;

                        while (c.IsSpaceCharacter())
                            c = _stream.Next;

                        if (c != Specification.RBO)
                            throw new ArgumentException("Declaration invalid.");

                        AttributeDeclarationTypeEnumeration(_stream.Next, type);
                        break;
                    default:
                        throw new ArgumentException("Declaration invalid.");
                }
            }

            return AttributeDeclarationValue(_stream.Next, value);
        }

        void AttributeDeclarationTypeEnumeration(Char c, AttributeEnumeratedType parent)
        {
            while (true)
            {
                while (c.IsSpaceCharacter())
                    c = _stream.Next;

                if (c == Specification.EOF)
                    throw new ArgumentException("The document ended unexpectedly.");

                if (!c.IsXmlName())
                    throw new ArgumentException("Declaration invalid.");

                do
                {
                    _stringBuffer.Append(c);
                    c = _stream.Next;
                }
                while (c.IsXmlName());

                while (c.IsSpaceCharacter())
                    c = _stream.Next;

                parent.Names.Add(_stringBuffer.ToString());
                _stringBuffer.Clear();

                if (c == Specification.RBC)
                    break;
                else if (c == Specification.PIPE)
                    c = _stream.Next;
                else
                    throw new ArgumentException("Declaration invalid.");
            }
        }

        AttributeDeclarationEntry AttributeDeclarationValue(Char c, AttributeDeclarationEntry value)
        {
            while (c.IsSpaceCharacter())
                c = _stream.Next;

            var isfixed = false;

            if (c == Specification.NUM)
            {
                do
                {
                    _stringBuffer.Append(c);
                    c = _stream.Next;

                    if (c == Specification.EOF)
                        throw new ArgumentException("The document ended unexpectedly.");
                    else if (c == Specification.GT)
                        break;
                }
                while (!c.IsSpaceCharacter());

                var tag = _stringBuffer.ToString();
                _stringBuffer.Clear();

                switch (tag)
                {
                    case "#REQUIRED":
                        value.ValueDefault = new AttributeRequiredValue();
                        return value;
                    case "#IMPLIED":
                        value.ValueDefault = new AttributeImpliedValue();
                        return value;
                    case "#FIXED":
                        isfixed = true;
                        break;
                }

                while (c.IsSpaceCharacter())
                    c = _stream.Next;
            }

            var defvalue = AttributeDeclarationBeforeDefaultValue(c);
            _stringBuffer.Clear();
            _stream.Advance();

            value.ValueDefault = new AttributeCustomValue
            {
                Value = defvalue,
                IsFixed = isfixed
            };
            return value;
        }

        String AttributeDeclarationBeforeDefaultValue(Char c)
        {
            switch (c)
            {
                case Specification.DQ:
                    return AttributeDeclarationDefaultValue(_stream.Next, Specification.DQ);
                case Specification.SQ:
                    return AttributeDeclarationDefaultValue(_stream.Next, Specification.SQ);
                default:
                    throw new ArgumentException("Declaration invalid.");
            }
        }

        String AttributeDeclarationDefaultValue(Char c, Char end)
        {
            while (c != end)
            {
                //TODO Exclude angle brackets and such

                if (c == Specification.EOF)
                    throw new ArgumentException("The document ended unexpectedly.");

                _stringBuffer.Append(c);
                c = _stream.Next;
            }

            return _stringBuffer.ToString();
        }

        DtdAttributeToken AttributeDeclarationAfter(Char c, DtdAttributeToken decl)
        {
            var hasError = false;

            while (true)
            {
                if (c == Specification.GT)
                {
                    return decl;
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    _stream.Back();
                    return decl;
                }
                else if (!c.IsSpaceCharacter())
                {
                    if (!hasError)
                        RaiseErrorOccurred(ErrorCode.InputUnexpected);

                    hasError = true;
                }

                c = _stream.Next;
            }
        }

        #endregion

        #region Notation Declaration

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#Notations.
        /// </summary>
        /// <param name="c">The next input character.</param>
        DtdNotationToken NotationDeclaration(Char c)
        {
            var decl = new DtdNotationToken();
            var canContinue = false;

            if (c.IsSpaceCharacter())
                canContinue = DeclarationNameBefore(_stream.Next, decl);
            else if (c == Specification.EOF)
                throw new ArgumentException("The document ended unexpectedly.");
            else
            {
                RaiseErrorOccurred(ErrorCode.UndefinedMarkupDeclaration);
                canContinue = DeclarationNameBefore(c, decl);
            }

            if (canContinue)
            {
                if (_stream.ContinuesWith("PUBLIC"))
                {
                    _stream.Advance(5);
                    return NotationDeclarationBeforePublic(_stream.Next, decl);
                }
                else if (_stream.ContinuesWith("SYSTEM"))
                {
                    _stream.Advance(5);
                    return NotationDeclarationBeforeSystem(_stream.Next, decl);
                }

                return NotationDeclarationAfterSystem(c, decl);
            }

            RaiseErrorOccurred(ErrorCode.NotationPublicInvalid);
            return decl;
        }

        DtdNotationToken NotationDeclarationBeforePublic(Char c, DtdNotationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = _stream.Next;

            if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return decl;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _stream.Back();
                return decl;
            }
            else if (c == Specification.SQ)
            {
                return NotationDeclarationPublic(_stream.Next, Specification.SQ, decl);
            }
            else if (c == Specification.DQ)
            {
                return NotationDeclarationPublic(_stream.Next, Specification.DQ, decl);
            }
            else
            {
                return NotationDeclarationAfterSystem(c, decl);
            }
        }

        DtdNotationToken NotationDeclarationPublic(Char c, Char quote, DtdNotationToken decl)
        {
            _stringBuffer.Clear();

            while (true)
            {
                if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    _stream.Back();
                    decl.PublicIdentifier = _stringBuffer.ToString();
                    return decl;
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    _stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == quote)
                {
                    decl.PublicIdentifier = _stringBuffer.ToString();
                    return NotationDeclarationAfterPublic(_stream.Next, decl);
                }
                else
                    _stringBuffer.Append(c);

                c = _stream.Next;
            }
        }

        DtdNotationToken NotationDeclarationAfterPublic(Char c, DtdNotationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = _stream.Next;

            if (c == Specification.GT)
            {
                return decl;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _stream.Back();
                return decl;
            }
            else if (c == Specification.SQ)
            {
                return NotationDeclarationSystem(_stream.Next, Specification.SQ, decl);
            }
            else if (c == Specification.DQ)
            {
                return NotationDeclarationSystem(_stream.Next, Specification.DQ, decl);
            }
            else
            {
                return NotationDeclarationAfterSystem(c, decl);
            }
        }

        DtdNotationToken NotationDeclarationBeforeSystem(Char c, DtdNotationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = _stream.Next;

            if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return decl;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _stream.Back();
                return decl;
            }
            else if (c == Specification.SQ)
            {
                return NotationDeclarationSystem(_stream.Next, Specification.SQ, decl);
            }
            else if (c == Specification.DQ)
            {
                return NotationDeclarationSystem(_stream.Next, Specification.DQ, decl);
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.NotationSystemInvalid);
                return decl;
            }
        }

        DtdNotationToken NotationDeclarationSystem(Char c, Char quote, DtdNotationToken decl)
        {
            _stringBuffer.Clear();

            while (true)
            {
                if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    _stream.Back();
                    decl.SystemIdentifier = _stringBuffer.ToString();
                    return decl;
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    _stringBuffer.Append(Specification.REPLACEMENT);
                }
                else if (c == quote)
                {
                    decl.SystemIdentifier = _stringBuffer.ToString();
                    return NotationDeclarationAfterSystem(_stream.Next, decl);
                }
                else if (c.IsPubidChar())
                    _stringBuffer.Append(c);
                else
                    RaiseErrorOccurred(ErrorCode.InvalidCharacter);

                c = _stream.Next;
            }
        }

        DtdNotationToken NotationDeclarationAfterSystem(Char c, DtdNotationToken decl)
        {
            var hasError = false;

            while (true)
            {
                if (c == Specification.GT)
                {
                    return decl;
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    _stream.Back();
                    return decl;
                }
                else if (!c.IsSpaceCharacter())
                {
                    if (!hasError)
                        RaiseErrorOccurred(ErrorCode.InputUnexpected);

                    hasError = true;
                }

                c = _stream.Next;
            }
        }

        #endregion

        #region Type Declaration

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#elemdecls.
        /// </summary>
        /// <param name="c">The next input character.</param>
        DtdElementToken TypeDeclaration(Char c)
        {
            var decl = new DtdElementToken();
            var canContinue = false;

            if (c.IsSpaceCharacter())
                canContinue = DeclarationNameBefore(_stream.Next, decl);
            else if (c == Specification.EOF)
                throw new ArgumentException("The document ended unexpectedly.");
            else
            {
                RaiseErrorOccurred(ErrorCode.UndefinedMarkupDeclaration);
                canContinue = DeclarationNameBefore(c, decl);
            }

            if (canContinue)
            {
                c = _stream.Current;

                while (c.IsSpaceCharacter())
                    c = _stream.Next;

                if (c == Specification.RBO)
                    return TypeDeclarationBeforeContent(_stream.Next, decl);
                else if (_stream.ContinuesWith("ANY"))
                {
                    _stream.Advance(2);
                    decl.CType = ElementDeclarationEntry.ContentType.Any;
                    return TypeDeclarationAfterContent(_stream.Next, decl);
                }
                else if (_stream.ContinuesWith("EMPTY"))
                {
                    _stream.Advance(4);
                    decl.CType = ElementDeclarationEntry.ContentType.Empty;
                    return TypeDeclarationAfterContent(_stream.Next, decl);
                }

                return TypeDeclarationAfterContent(c, decl);
            }

            RaiseErrorOccurred(ErrorCode.TypeDeclarationUndefined);
            return decl;
        }

        DtdElementToken TypeDeclarationBeforeContent(Char c, DtdElementToken decl)
        {
            while (c.IsSpaceCharacter())
                c = _stream.Next;

            if (_stream.ContinuesWith("#PCDATA"))
            {
                _stream.Advance(6);
                decl.CType = ElementDeclarationEntry.ContentType.Mixed;
                decl.Entry = TypeDeclarationMixed(_stream.Next);
            }
            else
            {
                decl.CType = ElementDeclarationEntry.ContentType.Children;
                decl.Entry = TypeDeclarationChildren(c);
            }

            return TypeDeclarationAfterContent(_stream.Current, decl);
        }

        ElementDeclarationEntry TypeDeclarationChildren(Char c)
        {
            var entries = new List<ElementDeclarationEntry>();
            var connection = Specification.NULL;

            while (true)
            {
                if (entries.Count > 0)
                {
                    if (c != Specification.PIPE && c != Specification.COMMA)
                        throw new ArgumentException("Invalid content specification in element type declaration.");

                    if (entries.Count == 1)
                        connection = c;
                    else if (connection != c)
                        throw new ArgumentException("Invalid content specification in element type declaration.");

                    c = _stream.Next;
                }

                while (c.IsSpaceCharacter())
                    c = _stream.Next;

                if (c.IsXmlNameStart())
                {
                    var name = TypeDeclarationName(c);
                    entries.Add(name);
                }
                else if (c == Specification.RBO)
                    entries.Add(TypeDeclarationChildren(_stream.Next));
                else
                    throw new ArgumentException("Invalid content specification in element type declaration.");

                c = _stream.Current;

                while (c.IsSpaceCharacter())
                    c = _stream.Next;

                if (c == Specification.RBC)
                    break;
            }

            c = _stream.Next;

            if (entries.Count == 1)
                return entries[0];
            else if (entries.Count == 0)
                throw new ArgumentException("Invalid content specification in element type declaration.");
            else if (connection == Specification.COMMA)
            {
                var sequence = new ElementSequenceDeclarationEntry();
                sequence.Sequence.AddRange(entries);
                sequence.Quantifier = TypeDeclarationQuantifier(c);
                return sequence;
            }

            var choice = new ElementChoiceDeclarationEntry();
            choice.Choice.AddRange(entries);
            choice.Quantifier = TypeDeclarationQuantifier(c);
            return choice;
        }

        ElementNameDeclarationEntry TypeDeclarationName(Char c)
        {
            _stringBuffer.Clear();
            _stringBuffer.Append(c);

            while ((c = _stream.Next).IsXmlName())
                _stringBuffer.Append(c);

            return new ElementNameDeclarationEntry
            {
                Name = _stringBuffer.ToString(),
                Quantifier = TypeDeclarationQuantifier(c)
            };
        }

        ElementDeclarationEntry.ElementQuantifier TypeDeclarationQuantifier(Char c)
        {
            switch (c)
            {
                case Specification.ASTERISK:
                    _stream.Advance();
                    return ElementDeclarationEntry.ElementQuantifier.ZeroOrMore;

                case Specification.QM:
                    _stream.Advance();
                    return ElementDeclarationEntry.ElementQuantifier.ZeroOrOne;

                case Specification.PLUS:
                    _stream.Advance();
                    return ElementDeclarationEntry.ElementQuantifier.OneOrMore;

                default:
                    return ElementDeclarationEntry.ElementQuantifier.One;
            }
        }

        ElementMixedDeclarationEntry TypeDeclarationMixed(Char c)
        {
            var entry = new ElementMixedDeclarationEntry();

            while (true)
            {
                while (c.IsSpaceCharacter())
                    c = _stream.Next;

                if (c == Specification.RBC)
                {
                    c = _stream.Next;

                    if (c == Specification.ASTERISK)
                    {
                        entry.Quantifier = ElementDeclarationEntry.ElementQuantifier.ZeroOrMore;
                        _stream.Advance();
                        return entry;
                    }

                    if (entry.Names.Count > 0)
                        RaiseErrorOccurred(ErrorCode.QuantifierMissing);

                    break;
                }
                else if (c == Specification.PIPE)
                {
                    c = _stream.Next;

                    while (c.IsSpaceCharacter())
                        c = _stream.Next;

                    _stringBuffer.Clear();

                    if (c.IsXmlNameStart())
                    {
                        _stringBuffer.Append(c);

                        while ((c = _stream.Next).IsXmlName())
                            _stringBuffer.Append(c);

                        entry.Names.Add(_stringBuffer.ToString());
                    }
                    else
                        throw new ArgumentException("Invalid content specification in element type declaration.");
                }
            }

            return entry;
        }

        DtdElementToken TypeDeclarationAfterContent(Char c, DtdElementToken decl)
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
                    _stream.Back();
                    return decl;
                }
                else if (!c.IsSpaceCharacter())
                {
                    if (!hasError)
                        RaiseErrorOccurred(ErrorCode.InputUnexpected);

                    hasError = true;
                }

                c = _stream.Next;
            }
        }

        #endregion

        #region Intermediate Stream

        sealed class IntermediateStream
        {
            #region Members

            SourceManager _base;
            StringBuilder _buffer;
            Int32 _head;

            #endregion

            #region ctor

            public IntermediateStream(SourceManager src)
            {
                _head = 0;
                _buffer = new StringBuilder();
                _base = src;
            }

            #endregion

            #region Properties

            public Char Next
            {
                get 
                {
                    if (_head == _buffer.Length)
                    {
                        var chr = _base.Next;
                        _buffer.Append(chr);
                        _head++;
                        return chr;
                    }

                    return _buffer[_head++];
                }
            }

            public Char Current 
            {
                get { return _buffer.Length == _head ? _base.Current : _buffer[_head]; }
            }

            #endregion

            #region Methods

            public void Push(Int32 remove, String text)
            {
                var index = _head - remove;
                _buffer.Remove(index, remove);
                _buffer.Insert(index, text);
                _head = index + text.Length;
            }

            public void Advance()
            {
                if (_head == _buffer.Length)
                    _buffer.Append(_base.Next);

                _head++;
            }

            public void Back()
            {
                _head--;
            }

            public void Advance(Int32 n)
            {
                for (int i = 0; i < n; i++)
                    Advance();
            }

            public Boolean ContinuesWith(String word)
            {
                if (_head == _buffer.Length)
                    return _base.ContinuesWith(word, false);

                for (int i = 0; i < word.Length; i++)
                {
                    if (Current != word[i])
                    {
                        _head -= i;
                        return false;
                    }

                    Advance();
                }

                return true;
            }

            #endregion
        }

        #endregion

        #region Helper

        Char SkipSpaces(Char c)
        {
            do
            {
                c = _stream.Next;
            }
            while (c.IsSpaceCharacter());

            return c;
        }

        #endregion
    }
}
