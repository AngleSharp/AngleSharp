namespace AngleSharp
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// A collection of error codes.
    /// </summary>
    enum ErrorCode : ushort
    {
        /// <summary>
        /// Unexpected end of file detected.
        /// </summary>
        [DomDescription("Unexpected end of the given file.")]
        EOF = 0,
        /// <summary>
        /// The index is not in the allowed range.
        /// </summary>
        [DomDescription("The index is not in the allowed range.")]
        [DomName("INDEX_SIZE_ERR")]
        IndexSizeError = 0x1,
        /// <summary>
        /// The size of the string is invalid.
        /// </summary>
        [DomDescription("The size of the string is invalid.")]
        [DomName("DOMSTRING_SIZE_ERR")]
        [DomHistorical]
        DomStringSize = 0x2,
        /// <summary>
        /// The operation would yield an incorrect node tree.
        /// </summary>
        [DomDescription("The operation would yield an incorrect node tree.")]
        [DomName("HIERARCHY_REQUEST_ERR")]
        HierarchyRequest = 0x3,
        /// <summary>
        /// The object is in the wrong document.
        /// </summary>
        [DomDescription("The object is in the wrong document.")]
        [DomName("WRONG_DOCUMENT_ERR")]
        WrongDocument = 0x4,
        /// <summary>
        /// Invalid character detected.
        /// </summary>
        [DomDescription("Invalid character detected.")]
        [DomName("INVALID_CHARACTER_ERR")]
        InvalidCharacter = 0x5,
        /// <summary>
        /// The data is allowed for this object.
        /// </summary>
        [DomDescription("The data is allowed for this object.")]
        [DomName("NO_DATA_ALLOWED_ERR")]
        [DomHistorical]
        NoDataAllowed = 0x6,
        /// <summary>
        /// The object can not be modified.
        /// </summary>
        [DomDescription("The object can not be modified.")]
        [DomName("NO_MODIFICATION_ALLOWED_ERR")]
        NoModificationAllowed = 0x7,
        /// <summary>
        /// The object can not be found here.
        /// </summary>
        [DomDescription("The object can not be found here.")]
        [DomName("NOT_FOUND_ERR")]
        NotFound = 0x8,
        /// <summary>
        /// The operation is not supported.
        /// </summary>
        [DomDescription("The operation is not supported.")]
        [DomName("NOT_SUPPORTED_ERR")]
        NotSupported = 0x9,
        /// <summary>
        /// The element is already in-use.
        /// </summary>
        [DomDescription("The element is already in-use.")]
        [DomName("INUSE_ATTRIBUTE_ERR")]
        [DomHistorical]
        InUse = 0xA,
        /// <summary>
        /// The object is in an invalid state.
        /// </summary>
        [DomDescription("The object is in an invalid state.")]
        [DomName("INVALID_STATE_ERR")]
        InvalidState = 0xB,
        /// <summary>
        /// The string did not match the expected pattern.
        /// </summary>
        [DomDescription("The string did not match the expected pattern.")]
        [DomName("SYNTAX_ERR")]
        Syntax = 0xC,
        /// <summary>
        /// The object can not be modified in this way.
        /// </summary>
        [DomDescription("The object can not be modified in this way.")]
        [DomName("INVALID_MODIFICATION_ERR")]
        InvalidModification = 0xD,
        /// <summary>
        /// The operation is not allowed by namespaces in XML.
        /// </summary>
        [DomDescription("The operation is not allowed by namespaces in XML.")]
        [DomName("NAMESPACE_ERR")]
        Namespace = 0xE,
        /// <summary>
        /// The object does not support the operation or argument.
        /// </summary>
        [DomDescription("The object does not support the operation or argument.")]
        [DomName("INVALID_ACCESS_ERR")]
        InvalidAccess = 0xF,
        /// <summary>
        /// The validation failed.
        /// </summary>
        [DomDescription("The validation failed.")]
        [DomName("VALIDATION_ERR")]
        Validation = 0xF,
        /// <summary>
        /// The provided argument type is invalid.
        /// </summary>
        [DomDescription("The provided argument type is invalid.")]
        [DomName("TYPE_MISMATCH_ERR")]
        [DomHistorical]
        TypeMismatch = 0x11,
        /// <summary>
        /// The operation is insecure.
        /// </summary>
        [DomDescription("The operation is insecure.")]
        [DomName("SECURITY_ERR")]
        Security = 0x12,
        /// <summary>
        /// A network error occurred.
        /// </summary>
        [DomDescription("A network error occurred.")]
        [DomName("NETWORK_ERR")]
        Network = 0x13,
        /// <summary>
        /// The operation was aborted.
        /// </summary>
        [DomDescription("The operation was aborted.")]
        [DomName("ABORT_ERR")]
        Abort = 0x14,
        /// <summary>
        /// The given URL does not match another URL.
        /// </summary>
        [DomDescription("The given URL does not match another URL.")]
        [DomName("URL_MISMATCH_ERR")]
        UrlMismatch = 0x15,
        /// <summary>
        /// The quota has been exceeded.
        /// </summary>
        [DomDescription("The quota has been exceeded.")]
        [DomName("QUOTA_EXCEEDED_ERR")]
        QuotaExceeded = 0x16,
        /// <summary>
        /// The operation timed out.
        /// </summary>
        [DomDescription("The operation timed out.")]
        [DomName("TIMEOUT_ERR")]
        Timeout = 0x17,
        /// <summary>
        /// The supplied node is incorrect or has an incorrect ancestor for this operation.
        /// </summary>
        [DomDescription("The supplied node is incorrect or has an incorrect ancestor for this operation.")]
        [DomName("INVALID_NODE_TYPE_ERR")]
        InvalidNodeType = 0x18,
        /// <summary>
        /// The object can not be cloned.
        /// </summary>
        [DomDescription("The object can not be cloned.")]
        [DomName("DATA_CLONE_ERR")]
        DataClone = 0x19,
        /// <summary>
        /// Bogus comment.
        /// </summary>
        [DomDescription("Bogus comment detected.")]
        BogusComment = 0x1a,
        /// <summary>
        /// Ambiguous open tag.
        /// </summary>
        [DomDescription("Ambiguous open tag.")]
        AmbiguousOpenTag = 0x1b,
        /// <summary>
        /// The tag has been closed unexpectedly.
        /// </summary>
        [DomDescription("The tag has been closed unexpectedly.")]
        TagClosedWrong = 0x1c,
        /// <summary>
        /// The closing slash has been misplaced.
        /// </summary>
        [DomDescription("The closing slash has been misplaced.")]
        ClosingSlashMisplaced = 0x1d,
        /// <summary>
        /// Undefined markup declaration found.
        /// </summary>
        [DomDescription("Undefined markup declaration found.")]
        UndefinedMarkupDeclaration = 0x1e,
        /// <summary>
        /// Comment ended with an exclamation mark.
        /// </summary>
        [DomDescription("Comment ended with an exclamation mark.")]
        CommentEndedWithEM = 0x1f,
        /// <summary>
        /// Comment ended with a dash.
        /// </summary>
        [DomDescription("Comment ended with a dash.")]
        CommentEndedWithDash = 0x20,
        /// <summary>
        /// Comment ended with an unexpected character.
        /// </summary>
        [DomDescription("Comment ended with an unexpected character.")]
        CommentEndedUnexpected = 0x21,
        /// <summary>
        /// The given tag cannot be self-closed.
        /// </summary>
        [DomDescription("The given tag cannot be self-closed.")]
        TagCannotBeSelfClosed = 0x22,
        /// <summary>
        /// End tags can never be self-closed.
        /// </summary>
        [DomDescription("End tags can never be self-closed.")]
        EndTagCannotBeSelfClosed = 0x23,
        /// <summary>
        /// End tags cannot carry attributes.
        /// </summary>
        [DomDescription("End tags cannot carry attributes.")]
        EndTagCannotHaveAttributes = 0x24,
        /// <summary>
        /// No caption tag has been found within the local scope.
        /// </summary>
        [DomDescription("No caption tag has been found within the local scope.")]
        CaptionNotInScope = 0x25,
        /// <summary>
        /// No select tag has been found within the local scope.
        /// </summary>
        [DomDescription("No select tag has been found within the local scope.")]
        SelectNotInScope = 0x26,
        /// <summary>
        /// No table row has been found within the local scope.
        /// </summary>
        [DomDescription("No table row has been found within the local scope.")]
        TableRowNotInScope = 0x27,
        /// <summary>
        /// No table has been found within the local scope.
        /// </summary>
        [DomDescription("No table has been found within the local scope.")]
        TableNotInScope = 0x28,
        /// <summary>
        /// No paragraph has been found within the local scope.
        /// </summary>
        [DomDescription("No paragraph has been found within the local scope.")]
        ParagraphNotInScope = 0x29,
        /// <summary>
        /// No body has been found within the local scope.
        /// </summary>
        [DomDescription("No body has been found within the local scope.")]
        BodyNotInScope = 0x2a,
        /// <summary>
        /// No block element has been found within the local scope.
        /// </summary>
        [DomDescription("No block element has been found within the local scope.")]
        BlockNotInScope = 0x2b,
        /// <summary>
        /// No table cell has been found within the local scope.
        /// </summary>
        [DomDescription("No table cell has been found within the local scope.")]
        TableCellNotInScope = 0x2c,
        /// <summary>
        /// No table section has been found within the local scope.
        /// </summary>
        [DomDescription("No table section has been found within the local scope.")]
        TableSectionNotInScope = 0x2d,
        /// <summary>
        /// No object element has been found within the local scope.
        /// </summary>
        [DomDescription("No object element has been found within the local scope.")]
        ObjectNotInScope = 0x2e,
        /// <summary>
        /// No heading element has been found within the local scope.
        /// </summary>
        [DomDescription("No heading element has been found within the local scope.")]
        HeadingNotInScope = 0x2f,
        /// <summary>
        /// No list item has been found within the local scope.
        /// </summary>
        [DomDescription("No list item has been found within the local scope.")]
        ListItemNotInScope = 0x30,
        /// <summary>
        /// No form has been found within the local scope.
        /// </summary>
        [DomDescription("No form has been found within the local scope.")]
        FormNotInScope = 0x31,
        /// <summary>
        /// No button has been found within the local scope.
        /// </summary>
        [DomDescription("No button has been found within the local scope.")]
        ButtonInScope = 0x32,
        /// <summary>
        /// No nobr element has been found within the local scope.
        /// </summary>
        [DomDescription("No nobr element has been found within the local scope.")]
        NobrInScope = 0x33,
        /// <summary>
        /// No element has been found within the local scope.
        /// </summary>
        [DomDescription("No element has been found within the local scope.")]
        ElementNotInScope = 0x34,
        /// <summary>
        /// Character reference found no numbers.
        /// </summary>
        [DomDescription("Character reference found no numbers.")]
        CharacterReferenceWrongNumber = 0x35,
        /// <summary>
        /// Character reference found no semicolon.
        /// </summary>
        [DomDescription("Character reference found no semicolon.")]
        CharacterReferenceSemicolonMissing = 0x36,
        /// <summary>
        /// Character reference within an invalid range.
        /// </summary>
        [DomDescription("Character reference within an invalid range.")]
        CharacterReferenceInvalidRange = 0x37,
        /// <summary>
        /// Character reference is an invalid number.
        /// </summary>
        [DomDescription("Character reference is an invalid number.")]
        CharacterReferenceInvalidNumber = 0x38,
        /// <summary>
        /// Character reference is an invalid code.
        /// </summary>
        [DomDescription("Character reference is an invalid code.")]
        CharacterReferenceInvalidCode = 0x39,
        /// <summary>
        /// Character reference is not terminated by a semicolon.
        /// </summary>
        [DomDescription("Character reference is not terminated by a semicolon.")]
        CharacterReferenceNotTerminated = 0x3a,
        /// <summary>
        /// Character reference in attribute contains an invalid character (=).
        /// </summary>
        [DomDescription("Character reference in attribute contains an invalid character (=).")]
        CharacterReferenceAttributeEqualsFound = 0x3b,
        /// <summary>
        /// The specified item has not been found.
        /// </summary>
        [DomDescription("The specified item has not been found.")]
        ItemNotFound = 0x3c,
        /// <summary>
        /// The encoding operation (either encoded or decoding) failed.
        /// </summary>
        [DomDescription("The encoding operation (either encoded or decoding) failed.")]
        EncodingError = 0x3d,
        /// <summary>
        /// Doctype unexpected character after the name detected.
        /// </summary>
        [DomDescription("Doctype unexpected character after the name detected.")]
        DoctypeUnexpectedAfterName = 0x40,
        /// <summary>
        /// Invalid character in the public identifier detected.
        /// </summary>
        [DomDescription("Invalid character in the public identifier detected.")]
        DoctypePublicInvalid = 0x41,
        /// <summary>
        /// Invalid character in the doctype detected.
        /// </summary>
        [DomDescription("Invalid character in the doctype detected.")]
        DoctypeInvalidCharacter = 0x42,
        /// <summary>
        /// Invalid character in the system identifier detected.
        /// </summary>
        [DomDescription("Invalid character in the system identifier detected.")]
        DoctypeSystemInvalid = 0x43,
        /// <summary>
        /// The doctype tag is misplaced and ignored.
        /// </summary>
        [DomDescription("The doctype tag is misplaced and ignored.")]
        DoctypeTagInappropriate = 0x44,
        /// <summary>
        /// The given doctype tag is invalid.
        /// </summary>
        [DomDescription("The given doctype tag is invalid.")]
        DoctypeInvalid = 0x45,
        /// <summary>
        /// Doctype encountered unexpected character.
        /// </summary>
        [DomDescription("Doctype encountered unexpected character.")]
        DoctypeUnexpected = 0x46,
        /// <summary>
        /// The doctype tag is missing.
        /// </summary>
        [DomDescription("The doctype tag is missing.")]
        DoctypeMissing = 0x47,
        /// <summary>
        /// The given public identifier for the notation declaration is invalid.
        /// </summary>
        [DomDescription("The given public identifier for the notation declaration is invalid.")]
        NotationPublicInvalid = 0x48,
        /// <summary>
        /// The given system identifier for the notation declaration is invalid.
        /// </summary>
        [DomDescription("The given system identifier for the notation declaration is invalid.")]
        NotationSystemInvalid = 0x49,
        /// <summary>
        /// The type declaration is missing a valid definition.
        /// </summary>
        [DomDescription("The type declaration is missing a valid definition.")]
        TypeDeclarationUndefined = 0x4a,
        /// <summary>
        /// A required quantifier is missing in the provided expression.
        /// </summary>
        [DomDescription("A required quantifier is missing in the provided expression.")]
        QuantifierMissing = 0x4b,
        /// <summary>
        /// The double quotation marks have been misplaced.
        /// </summary>
        [DomDescription("The double quotation marks have been misplaced.")]
        DoubleQuotationMarkUnexpected = 0x50,
        /// <summary>
        /// The single quotation marks have been misplaced.
        /// </summary>
        [DomDescription("The single quotation marks have been misplaced.")]
        SingleQuotationMarkUnexpected = 0x51,
        /// <summary>
        /// The attribute's name contains an invalid character.
        /// </summary>
        [DomDescription("The attribute's name contains an invalid character.")]
        AttributeNameInvalid = 0x60,
        /// <summary>
        /// The attribute's value contains an invalid character.
        /// </summary>
        [DomDescription("The attribute's value contains an invalid character.")]
        AttributeValueInvalid = 0x61,
        /// <summary>
        /// The beginning of a new attribute has been expected.
        /// </summary>
        [DomDescription("The beginning of a new attribute has been expected.")]
        AttributeNameExpected = 0x62,
        /// <summary>
        /// The attribute has already been added.
        /// </summary>
        [DomDescription("The attribute has already been added.")]
        AttributeDuplicateOmitted = 0x63,
        /// <summary>
        /// The given tag must be placed in head tag.
        /// </summary>
        [DomDescription("The given tag must be placed in head tag.")]
        TagMustBeInHead = 0x70,
        /// <summary>
        /// The given tag is not appropriate for the current position.
        /// </summary>
        [DomDescription("The given tag is not appropriate for the current position.")]
        TagInappropriate = 0x71,
        /// <summary>
        /// The given tag cannot end at the current position.
        /// </summary>
        [DomDescription("The given tag cannot end at the current position.")]
        TagCannotEndHere = 0x72,
        /// <summary>
        /// The given tag cannot start at the current position.
        /// </summary>
        [DomDescription("The given tag cannot start at the current position.")]
        TagCannotStartHere = 0x73,
        /// <summary>
        /// The given form cannot be placed at the current position.
        /// </summary>
        [DomDescription("The given form cannot be placed at the current position.")]
        FormInappropriate = 0x74,
        /// <summary>
        /// The given input cannot be placed at the current position.
        /// </summary>
        [DomDescription("The given input cannot be placed at the current position.")]
        InputUnexpected = 0x75,
        /// <summary>
        /// The closing tag and the currently open tag do not match.
        /// </summary>
        [DomDescription("The closing tag and the currently open tag do not match.")]
        TagClosingMismatch = 0x76,
        /// <summary>
        /// The given end tag does not match the current node.
        /// </summary>
        [DomDescription("The given end tag does not match the current node.")]
        TagDoesNotMatchCurrentNode = 0x77,
        /// <summary>
        /// This position does not support a linebreak (LF, FF).
        /// </summary>
        [DomDescription("This position does not support a linebreak (LF, FF).")]
        LineBreakUnexpected = 0x78,
        /// <summary>
        /// The head tag can only be placed once inside the html tag.
        /// </summary>
        [DomDescription("The head tag can only be placed once inside the html tag.")]
        HeadTagMisplaced = 0x80,
        /// <summary>
        /// The html tag can only be placed once as the root element.
        /// </summary>
        [DomDescription("The html tag can only be placed once as the root element.")]
        HtmlTagMisplaced = 0x81,
        /// <summary>
        /// The body tag can only be placed once inside the html tag.
        /// </summary>
        [DomDescription("The body tag can only be placed once inside the html tag.")]
        BodyTagMisplaced = 0x82,
        /// <summary>
        /// The image tag has been named image instead of img.
        /// </summary>
        [DomDescription("The image tag has been named image instead of img.")]
        ImageTagNamedWrong = 0x83,
        /// <summary>
        /// Tables cannot be nested.
        /// </summary>
        [DomDescription("Tables cannot be nested.")]
        TableNesting = 0x84,
        /// <summary>
        /// An illegal element has been detected in a table.
        /// </summary>
        [DomDescription("An illegal element has been detected in a table.")]
        IllegalElementInTableDetected = 0x85,
        /// <summary>
        /// Select elements cannot be nested.
        /// </summary>
        [DomDescription("Select elements cannot be nested.")]
        SelectNesting = 0x86,
        /// <summary>
        /// An illegal element has been detected in a select.
        /// </summary>
        [DomDescription("An illegal element has been detected in a select.")]
        IllegalElementInSelectDetected = 0x87,
        /// <summary>
        /// The frameset element has been misplaced.
        /// </summary>
        [DomDescription("The frameset element has been misplaced.")]
        FramesetMisplaced = 0x88,
        /// <summary>
        /// Headings cannot be nested.
        /// </summary>
        [DomDescription("Headings cannot be nested.")]
        HeadingNested = 0x89,
        /// <summary>
        /// Anchor elements cannot be nested.
        /// </summary>
        [DomDescription("Anchor elements cannot be nested.")]
        AnchorNested = 0x8a,
        /// <summary>
        /// The given token cannot be inserted here.
        /// </summary>
        [DomDescription("The given token cannot be inserted here.")]
        TokenNotPossible = 0x90,
        /// <summary>
        /// The current node is not the root element.
        /// </summary>
        [DomDescription("The current node is not the root element.")]
        CurrentNodeIsNotRoot = 0x91,
        /// <summary>
        /// The current node is the root element.
        /// </summary>
        [DomDescription("The current node is the root element.")]
        CurrentNodeIsRoot = 0x92,
        /// <summary>
        /// This tag is invalid in fragment mode.
        /// </summary>
        [DomDescription("This tag is invalid in fragment mode.")]
        TagInvalidInFragmentMode = 0x93,
        /// <summary>
        /// There is already an open form.
        /// </summary>
        [DomDescription("There is already an open form.")]
        FormAlreadyOpen = 0x94,
        /// <summary>
        /// The form has been closed wrong.
        /// </summary>
        [DomDescription("The form has been closed wrong.")]
        FormClosedWrong = 0x95,
        /// <summary>
        /// The body has been closed wrong.
        /// </summary>
        [DomDescription("The body has been closed wrong.")]
        BodyClosedWrong = 0x96,
        /// <summary>
        /// An expected formatting element has not been found.
        /// </summary>
        [DomDescription("An expected formatting element has not been found.")]
        FormattingElementNotFound = 0x97,
        /// <summary>
        /// NULL character replaced by repl. character.
        /// </summary>
        [DomDescription("NULL character replaced by repl. character.")]
        Null = 0x100,
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
        /// <summary>
        /// (0x401) The name of the @-rule is unknown.
        /// </summary>
        UnknownAtRule = 0x401,
        /// <summary>
        /// (0x402) No block can start at the current position.
        /// </summary>
        InvalidBlockStart = 0x402,
        /// <summary>
        /// (0x403) The given token is not valid at the current position.
        /// </summary>
        InvalidToken = 0x403,
        /// <summary>
        /// (0x404) The provided selector is invalid.
        /// </summary>
        InvalidSelector = 0x404,
        /// <summary>
        /// (0x405) An expected colon is missing.
        /// </summary>
        ColonMissing = 0x405,
        /// <summary>
        /// (0x406) The value of the declaration could not be found.
        /// </summary>
        ValueMissing = 0x406,
        /// <summary>
        /// (0x407) The name of the declaration is unknown.
        /// </summary>
        UnknownDeclarationName = 0x407,
        /// <summary>
        /// (0x408) An expected identifier could not be found.
        /// </summary>
        IdentExpected = 0x408,
    }
}
