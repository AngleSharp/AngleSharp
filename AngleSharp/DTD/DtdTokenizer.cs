using AngleSharp.Xml;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AngleSharp.DTD
{
    /// <summary>
    /// The tokenizer class for Document Type Definitions.
    /// </summary>
    [DebuggerStepThrough]
    sealed class DtdTokenizer : XmlBaseTokenizer
    {
        #region ctor

        public DtdTokenizer(SourceManager src)
            : base(src)
        {
            End = Specification.EOF;
        }

        #endregion

        #region Properties

        public Char End
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Scans the DTD in the doctype as specified in the
        /// official XML spec and (in German) here:
        /// http://www.stefanheymann.de/xml/dtdxml.htm
        /// </summary>
        public DtdToken Get()
        {
            var c = _src.Current;

            if (c == End)
                return DtdToken.EOF;

            var element = GetElement(c);
            c = _src.Next;

            while (c.IsSpaceCharacter())
                c = _src.Next;

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
                c = _src.Next;

            if (c == Specification.LT)
            {
                c = _src.Next;

                if (c == Specification.QM)
                {
                    return Rework(ProcessingStart(_src.Next));
                }
                else if (c == Specification.EM)
                {
                    _src.Advance();

                    if (_src.ContinuesWith("ENTITY", false))
                    {
                        _src.Advance(5);
                        return EntityDeclaration(_src.Next);
                    }
                    else if (_src.ContinuesWith("ELEMENT", false))
                    {
                        _src.Advance(6);
                        return TypeDeclaration(_src.Next);
                    }
                    else if (_src.ContinuesWith("ATTLIST", false))
                    {
                        _src.Advance(6);
                        return AttributeDeclaration(_src.Next);
                    }
                    else if (_src.ContinuesWith("NOTATION", false))
                    {
                        _src.Advance(7);
                        return NotationDeclaration(_src.Next);
                    }
                    else if (_src.ContinuesWith("--", false))
                    {
                        _src.Advance();
                        return Rework(CommentStart(_src.Next));
                    }
                }
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
                c = _src.Next;

            if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _stringBuffer.Clear();
                _stringBuffer.Append(Specification.REPLACEMENT);
                return DeclarationName(_src.Next, decl);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return false;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return false;
            }

            _stringBuffer.Clear();
            _stringBuffer.Append(c);
            return DeclarationName(_src.Next, decl);
        }

        Boolean DeclarationName(Char c, DtdToken decl)
        {
            while (true)
            {
                if (c.IsSpaceCharacter())
                {
                    decl.Name = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return DeclarationNameAfter(_src.Next);
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
                    _src.Back();
                    decl.Name = _stringBuffer.ToString();
                    return false;
                }
                else
                    _stringBuffer.Append(c);

                c = _src.Next;
            }
        }

        Boolean DeclarationNameAfter(Char c)
        {
            while (c.IsSpaceCharacter())
                c = _src.Next;

            if (c == Specification.GT)
            {
                return false;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
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
        DtdToken EntityDeclaration(Char c)
        {
            var decl = new DtdEntityToken();
            var canContinue = false;

            if (c.IsSpaceCharacter())
                canContinue = DeclarationNameBefore(_src.Next, decl);
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
                canContinue = DeclarationNameBefore(_src.Next, decl);
            }

            c = _src.Current;

            if (canContinue)
            {
                if (_src.ContinuesWith("SYSTEM"))
                {
                    _src.Advance(5);
                    decl.IsExtern = true;

                    do
                    {
                        c = _src.Next;
                    }
                    while (c.IsSpaceCharacter());
                }

                _stringBuffer.Clear();
                decl.Value = EntityDeclarationBeforeValue(c);
                c = _src.Next;
            }

            return EntityDeclarationAfter(c, decl);
        }

        String EntityDeclarationBeforeValue(Char c)
        {
            switch (c)
            {
                case Specification.DQ:
                    return EntityDeclarationValue(_src.Next, Specification.DQ);
                case Specification.SQ:
                    return EntityDeclarationValue(_src.Next, Specification.SQ);
                default:
                    throw new ArgumentException("Declaration invalid.");
            }
        }

        String EntityDeclarationValue(Char c, Char end)
        {
            while (c != end)
            {
                if (c == Specification.EOF)
                    throw new ArgumentException("The document ended unexpectedly.");

                _stringBuffer.Append(c);
                c = _src.Next;
            }

            return _stringBuffer.ToString();
        }

        DtdToken EntityDeclarationAfter(Char c, DtdEntityToken decl)
        {
            while (c.IsSpaceCharacter())
                c = _src.Next;

            if (c == Specification.EOF)
                throw new ArgumentException("The document ended unexpectedly.");
            else if (c == Specification.GT)
                return decl;
            else if (decl.IsExtern && String.IsNullOrEmpty(decl.ExternNotation))
            {
                if (_src.ContinuesWith("NDATA"))
                {
                    _src.Advance(4);
                    c = _src.Next;

                    while (c.IsSpaceCharacter())
                        c = _src.Next;

                    if (c.IsNameStart())
                    {
                        _stringBuffer.Clear();

                        do
                        {
                            _stringBuffer.Append(c);
                            c = _src.Next;
                        }
                        while (c.IsName());

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
                canContinue = DeclarationNameBefore(_src.Next, decl);
            else if (c == Specification.EOF)
                throw new ArgumentException("The document ended unexpectedly.");
            else
            {
                RaiseErrorOccurred(ErrorCode.UndefinedMarkupDeclaration);
                canContinue = DeclarationNameBefore(c, decl);
            }

            c = _src.Current;

            if (canContinue)
            {
                while (true)
                {
                    while (c.IsSpaceCharacter())
                        c = _src.Next;

                    if (c.IsNameStart())
                    {
                        _stringBuffer.Clear();
                        decl.Attributes.Add(AttributeDeclarationName(c));
                        c = _src.Current;
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

                c = _src.Next;
            }

            return AttributeDeclarationType(_src.Next, value);
        }

        AttributeDeclarationEntry AttributeDeclarationType(Char c, AttributeDeclarationEntry value)
        {
            while (c.IsSpaceCharacter())
                c = _src.Next;

            if (c == Specification.RBO)
            {
                var type = new AttributeEnumeratedType();
                value.ValueType = type;
                AttributeDeclarationTypeEnumeration(_src.Next, type);
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

                    c = _src.Next;
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
                            c = _src.Next;

                        if (c != Specification.RBO)
                            throw new ArgumentException("Declaration invalid.");

                        AttributeDeclarationTypeEnumeration(_src.Next, type);
                        break;
                    default:
                        throw new ArgumentException("Declaration invalid.");
                }
            }

            return AttributeDeclarationValue(_src.Next, value);
        }

        void AttributeDeclarationTypeEnumeration(Char c, AttributeEnumeratedType parent)
        {
            while (true)
            {
                while (c.IsSpaceCharacter())
                    c = _src.Next;

                if (c == Specification.EOF)
                    throw new ArgumentException("The document ended unexpectedly.");

                if (!c.IsName())
                    throw new ArgumentException("Declaration invalid.");

                do
                {
                    _stringBuffer.Append(c);
                    c = _src.Next;
                }
                while (c.IsName());

                while (c.IsSpaceCharacter())
                    c = _src.Next;

                parent.Names.Add(_stringBuffer.ToString());
                _stringBuffer.Clear();

                if (c == Specification.RBC)
                    break;
                else if (c == Specification.PIPE)
                    c = _src.Next;
                else
                    throw new ArgumentException("Declaration invalid.");
            }
        }

        AttributeDeclarationEntry AttributeDeclarationValue(Char c, AttributeDeclarationEntry value)
        {
            while (c.IsSpaceCharacter())
                c = _src.Next;

            var isfixed = false;

            if (c == Specification.NUM)
            {
                do
                {
                    _stringBuffer.Append(c);
                    c = _src.Next;

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
                    c = _src.Next;
            }

            var defvalue = AttributeDeclarationBeforeDefaultValue(c);
            _stringBuffer.Clear();
            _src.Advance();

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
                    return AttributeDeclarationDefaultValue(_src.Next, Specification.DQ);
                case Specification.SQ:
                    return AttributeDeclarationDefaultValue(_src.Next, Specification.SQ);
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
                c = _src.Next;
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
                    _src.Back();
                    return decl;
                }
                else if (!c.IsSpaceCharacter())
                {
                    if (!hasError)
                        RaiseErrorOccurred(ErrorCode.InputUnexpected);

                    hasError = true;
                }

                c = _src.Next;
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
                canContinue = DeclarationNameBefore(_src.Next, decl);
            else if (c == Specification.EOF)
                throw new ArgumentException("The document ended unexpectedly.");
            else
            {
                RaiseErrorOccurred(ErrorCode.UndefinedMarkupDeclaration);
                canContinue = DeclarationNameBefore(c, decl);
            }

            if (canContinue)
            {
                if (_src.ContinuesWith("PUBLIC", false))
                {
                    _src.Advance(5);
                    return NotationDeclarationBeforePublic(_src.Next, decl);
                }
                else if (_src.ContinuesWith("SYSTEM", false))
                {
                    _src.Advance(5);
                    return NotationDeclarationBeforeSystem(_src.Next, decl);
                }

                return NotationDeclarationAfterSystem(c, decl);
            }

            RaiseErrorOccurred(ErrorCode.NotationPublicInvalid);
            return decl;
        }

        DtdNotationToken NotationDeclarationBeforePublic(Char c, DtdNotationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = _src.Next;

            if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return decl;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return decl;
            }
            else if (c == Specification.SQ)
            {
                return NotationDeclarationPublic(_src.Next, Specification.SQ, decl);
            }
            else if (c == Specification.DQ)
            {
                return NotationDeclarationPublic(_src.Next, Specification.DQ, decl);
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
                    _src.Back();
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
                    return NotationDeclarationAfterPublic(_src.Next, decl);
                }
                else
                    _stringBuffer.Append(c);

                c = _src.Next;
            }
        }

        DtdNotationToken NotationDeclarationAfterPublic(Char c, DtdNotationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = _src.Next;

            if (c == Specification.GT)
            {
                return decl;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return decl;
            }
            else if (c == Specification.SQ)
            {
                return NotationDeclarationSystem(_src.Next, Specification.SQ, decl);
            }
            else if (c == Specification.DQ)
            {
                return NotationDeclarationSystem(_src.Next, Specification.DQ, decl);
            }
            else
            {
                return NotationDeclarationAfterSystem(c, decl);
            }
        }

        DtdNotationToken NotationDeclarationBeforeSystem(Char c, DtdNotationToken decl)
        {
            while (c.IsSpaceCharacter())
                c = _src.Next;

            if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return decl;
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return decl;
            }
            else if (c == Specification.SQ)
            {
                return NotationDeclarationSystem(_src.Next, Specification.SQ, decl);
            }
            else if (c == Specification.DQ)
            {
                return NotationDeclarationSystem(_src.Next, Specification.DQ, decl);
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
                    _src.Back();
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
                    return NotationDeclarationAfterSystem(_src.Next, decl);
                }
                else if (c.IsPubidChar())
                    _stringBuffer.Append(c);
                else
                    RaiseErrorOccurred(ErrorCode.InvalidCharacter);

                c = _src.Next;
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
                    _src.Back();
                    return decl;
                }
                else if (!c.IsSpaceCharacter())
                {
                    if (!hasError)
                        RaiseErrorOccurred(ErrorCode.InputUnexpected);

                    hasError = true;
                }

                c = _src.Next;
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
                canContinue = DeclarationNameBefore(_src.Next, decl);
            else if (c == Specification.EOF)
                throw new ArgumentException("The document ended unexpectedly.");
            else
            {
                RaiseErrorOccurred(ErrorCode.UndefinedMarkupDeclaration);
                canContinue = DeclarationNameBefore(c, decl);
            }

            if (canContinue)
            {
                c = _src.Current;

                while (c.IsSpaceCharacter())
                    c = _src.Next;

                if (c == Specification.RBO)
                    return TypeDeclarationBeforeContent(_src.Next, decl);
                else if (_src.ContinuesWith("ANY", false))
                {
                    _src.Advance(2);
                    decl.CType = ElementDeclarationEntry.ContentType.Any;
                    return TypeDeclarationAfterContent(_src.Next, decl);
                }
                else if (_src.ContinuesWith("EMPTY", false))
                {
                    _src.Advance(4);
                    decl.CType = ElementDeclarationEntry.ContentType.Empty;
                    return TypeDeclarationAfterContent(_src.Next, decl);
                }

                return TypeDeclarationAfterContent(c, decl);
            }

            RaiseErrorOccurred(ErrorCode.TypeDeclarationUndefined);
            return decl;
        }

        DtdElementToken TypeDeclarationBeforeContent(Char c, DtdElementToken decl)
        {
            while (c.IsSpaceCharacter())
                c = _src.Next;

            if (_src.ContinuesWith("#PCDATA", false))
            {
                _src.Advance(6);
                decl.CType = ElementDeclarationEntry.ContentType.Mixed;
                decl.Entry = TypeDeclarationMixed(_src.Next);
            }
            else
            {
                decl.CType = ElementDeclarationEntry.ContentType.Children;
                decl.Entry = TypeDeclarationChildren(c);
            }

            return TypeDeclarationAfterContent(_src.Current, decl);
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

                    c = _src.Next;
                }

                while (c.IsSpaceCharacter())
                    c = _src.Next;

                if (c.IsNameStart())
                {
                    var name = TypeDeclarationName(c);
                    entries.Add(name);
                }
                else if (c == Specification.RBO)
                    entries.Add(TypeDeclarationChildren(_src.Next));
                else
                    throw new ArgumentException("Invalid content specification in element type declaration.");

                c = _src.Current;

                while (c.IsSpaceCharacter())
                    c = _src.Next;

                if (c == Specification.RBC)
                    break;
            }

            c = _src.Next;

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

            while ((c = _src.Next).IsName())
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
                    _src.Advance();
                    return ElementDeclarationEntry.ElementQuantifier.ZeroOrMore;

                case Specification.QM:
                    _src.Advance();
                    return ElementDeclarationEntry.ElementQuantifier.ZeroOrOne;

                case Specification.PLUS:
                    _src.Advance();
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
                    c = _src.Next;

                if (c == Specification.RBC)
                {
                    c = _src.Next;

                    if (c == Specification.ASTERISK)
                    {
                        entry.Quantifier = ElementDeclarationEntry.ElementQuantifier.ZeroOrMore;
                        _src.Advance();
                        return entry;
                    }

                    if (entry.Names.Count > 0)
                        RaiseErrorOccurred(ErrorCode.QuantifierMissing);

                    break;
                }
                else if (c == Specification.PIPE)
                {
                    c = _src.Next;

                    while (c.IsSpaceCharacter())
                        c = _src.Next;

                    _stringBuffer.Clear();

                    if (c.IsNameStart())
                    {
                        _stringBuffer.Append(c);

                        while ((c = _src.Next).IsName())
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
                    _src.Back();
                    return decl;
                }
                else if (!c.IsSpaceCharacter())
                {
                    if (!hasError)
                        RaiseErrorOccurred(ErrorCode.InputUnexpected);

                    hasError = true;
                }

                c = _src.Next;
            }
        }

        #endregion
    }
}
