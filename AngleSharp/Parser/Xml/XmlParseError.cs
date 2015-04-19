namespace AngleSharp.Parser.Xml
{
    /// <summary>
    /// A collection of error codes.
    /// </summary>
    enum XmlParseError : ushort
    {
        /// <summary>
        /// Unexpected end of file detected.
        /// </summary>
        EOF = 0x00,
        /// <summary>
        /// Undefined markup declaration found.
        /// </summary>
        UndefinedMarkupDeclaration = 0x1e,
        /// <summary>
        /// Character reference is an invalid number.
        /// </summary>
        CharacterReferenceInvalidNumber = 0x38,
        /// <summary>
        /// Character reference is an invalid code.
        /// </summary>
        CharacterReferenceInvalidCode = 0x39,
        /// <summary>
        /// Character reference is not terminated by a semicolon.
        /// </summary>
        CharacterReferenceNotTerminated = 0x3a,
        /// <summary>
        /// The given doctype tag is invalid.
        /// </summary>
        DoctypeInvalid = 0x45,
        /// <summary>
        /// The closing tag and the currently open tag do not match.
        /// </summary>
        TagClosingMismatch = 0x76,
        /// <summary>
        /// (0x200) Missing root element.
        /// </summary>
        XmlMissingRoot = 0x200,
        /// <summary>
        /// (0x201) Document type declaration after content.
        /// </summary>
        XmlDoctypeAfterContent = 0x201,
        /// <summary>
        /// (0x202) Invalid XML declaration.
        /// </summary>
        XmlDeclarationInvalid = 0x202,
        /// <summary>
        /// (0x203) XML declaration not at beginning of document.
        /// </summary>
        XmlDeclarationMisplaced = 0x203,
        /// <summary>
        /// (0x204) The given version number is not supported.
        /// </summary>
        XmlDeclarationVersionUnsupported = 0x204,
        /// <summary>
        /// (0x205) Invalid start-tag.
        /// </summary>
        XmlInvalidStartTag = 0x205,
        /// <summary>
        /// (0x206) Invalid end-tag.
        /// </summary>
        XmlInvalidEndTag = 0x206,
        /// <summary>
        /// (0x207) Well-formedness constraint: No &lt; in Attribute Values.
        /// </summary>
        XmlLtInAttributeValue = 0x207,
        /// <summary>
        /// (0x208) Well-formedness constraint: Unique Att Spec.
        /// </summary>
        XmlUniqueAttribute = 0x208,
        /// <summary>
        /// (0x209) Invalid processing instruction.
        /// </summary>
        XmlInvalidPI = 0x209,
        /// <summary>
        /// (0x210) XML validation for the current document failed.
        /// </summary>
        XmlValidationFailed = 0x210,
        /// <summary>
        /// (0x211) XML invalid character data detected.
        /// </summary>
        XmlInvalidCharData = 0x211,
        /// <summary>
        /// (0x212) XML invalid name found.
        /// </summary>
        XmlInvalidName = 0x212,
        /// <summary>
        /// (0x213) XML invalid public identifier character.
        /// </summary>
        XmlInvalidPubId = 0x213,
        /// <summary>
        /// (0x214) XML invalid attribute seen.
        /// </summary>
        XmlInvalidAttribute = 0x214,
        /// <summary>
        /// (0x215) XML invalid comment detected.
        /// </summary>
        XmlInvalidComment = 0x215,
        /// <summary>
        /// (0x300) Invalid document type declaration.
        /// </summary>
        DtdInvalid = 0x300,
        /// <summary>
        /// (0x301) Invalid parameter entity reference.
        /// </summary>
        DtdPEReferenceInvalid = 0x301,
        /// <summary>
        /// (0x302) Invalid name in entity declaration.
        /// </summary>
        DtdNameInvalid = 0x302,
        /// <summary>
        /// (0x303) Declaration invalid.
        /// </summary>
        DtdDeclInvalid = 0x303,
        /// <summary>
        /// (0x304) Invalid element type declaration.
        /// </summary>
        DtdTypeInvalid = 0x304,
        /// <summary>
        /// (0x305) Invalid entity declaration.
        /// </summary>
        DtdEntityInvalid = 0x305,
        /// <summary>
        /// (0x306) Invalid element name in attribute-list declaration.
        /// </summary>
        DtdAttListInvalid = 0x306,
        /// <summary>
        /// (0x307) Invalid content specification in element type declaration.
        /// </summary>
        DtdTypeContent = 0x307,
        /// <summary>
        /// (0x308) An element type must not be declared more than once.
        /// </summary>
        DtdUniqueElementViolated = 0x308,
        /// <summary>
        /// (0x309) The DTD conditional section is invalid.
        /// </summary>
        DtdConditionInvalid = 0x309,
        /// <summary>
        /// (0x310) The given text declaration contains errors.s
        /// </summary>
        DtdTextDeclInvalid = 0x310,
        /// <summary>
        /// (0x311) The notation declaration is invalid.
        /// </summary>
        DtdNotationInvalid = 0x311,
        /// <summary>
        /// (0x312) No parameter reference recursion allowed.
        /// </summary>
        DtdPEReferenceRecursion = 0x312,
    }
}
