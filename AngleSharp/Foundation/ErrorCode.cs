using System;

namespace AngleSharp
{
    /// <summary>
    /// A collection of error codes.
    /// </summary>
    enum ErrorCode : uint
    {
        /// <summary>
        /// (0x1) The index is not in the allowed range.
        /// </summary>
        IndexSizeError = 0x1,
        /// <summary>
        /// (0x2) End of file.
        /// </summary>
        EOF = 0x2,
        /// <summary>
        /// (0x3) Hierarchy request error.
        /// </summary>
        HierarchyRequestError = 0x3,
        /// <summary>
        /// (0x4) The object is in the wrong document.
        /// </summary>
        WrongDocumentError = 0x4,
        /// <summary>
        /// (0x5) Invalid character detected.
        /// </summary>
        InvalidCharacter = 0x5,
        /// <summary>
        /// (0x6) The specified item has not been found.
        /// </summary>
        ItemNotFound = 0x6,
        /// <summary>
        /// (0x7) No modification allowed.
        /// </summary>
        NoModificationAllowed = 0x7,
        /// <summary>
        /// (0x8) The object can not be found here.
        /// </summary>
        NotFoundError = 0x8,
        /// <summary>
        /// (0x9) The operation is not supported.
        /// </summary>
        NotSupportedError = 0x9,
        /// <summary>
        /// (0xA) The element is already in-use.
        /// </summary>
        InUse = 0xA,
        /// <summary>
        /// (0xB) The object is in an invalid state.
        /// </summary>
        InvalidStateError = 0xB,
        /// <summary>
        /// (0xC) A syntax error makes the string unparsable.
        /// </summary>
        SyntaxError = 0xC,
        /// <summary>
        /// (0xD) The object can not be modified in this way.
        /// </summary>
        InvalidModificationError = 0xD,
        /// <summary>
        /// (0xE) The operation is not allowed by Namespaces in XML.
        /// </summary>
        NamespaceError = 0xE,
        /// <summary>
        /// (0xF) The object does not support the operation or argument.
        /// </summary>
        InvalidAccessError = 0xF,
        /// <summary>
        /// (0x11) The encoding operation (either encoded or decoding) failed.
        /// </summary>
        EncodingError = 0x11,
        /// <summary>
        /// (0x12) The operation is insecure.
        /// </summary>
        SecurityError = 0x12,
        /// <summary>
        /// (0x13) A network error occurred.
        /// </summary>
        NetworkError = 0x13,
        /// <summary>
        /// (0x14) The operation was aborted.
        /// </summary>
        AbortError = 0x14,
        /// <summary>
        /// (0x15) The given URL does not match another URL.
        /// </summary>
        URLMismatchError = 0x15,
        /// <summary>
        /// (0x16) The quota has been exceeded.
        /// </summary>
        QuotaExceededError = 0x16,
        /// <summary>
        /// (0x17) The operation timed out.
        /// </summary>
        TimeoutError = 0x17,
        /// <summary>
        /// (0x18) The supplied node is incorrect or has an incorrect ancestor for this operation.
        /// </summary>
        InvalidNodeTypeError = 0x18,
        /// <summary>
        /// (0x19) The object can not be cloned.
        /// </summary>
        DataCloneError = 0x19,
        /// <summary>
        /// (0x1a) Bogus comment.
        /// </summary>
        BogusComment = 0x1a,
        /// <summary>
        /// (0x1b) Ambiguous open tag.
        /// </summary>
        AmbiguousOpenTag = 0x1b,
        /// <summary>
        /// (0x1c) The tag has been closed unexpectedly.
        /// </summary>
        TagClosedWrong = 0x1c,
        /// <summary>
        /// (0x1d) The closing slash has been misplaced.
        /// </summary>
        ClosingSlashMisplaced = 0x1d,
        /// <summary>
        /// (0x1e) Undefined markup declaration found.
        /// </summary>
        UndefinedMarkupDeclaration = 0x1e,
        /// <summary>
        /// (0x1f) Comment ended with an exclamation mark.
        /// </summary>
        CommentEndedWithEM = 0x1f,
        /// <summary>
        /// (0x20) Comment ended with a dash.
        /// </summary>
        CommentEndedWithDash = 0x20,
        /// <summary>
        /// (0x21) Comment ended with an unexpected character.
        /// </summary>
        CommentEndedUnexpected = 0x21,
        /// <summary>
        /// (0x22) The given tag cannot be self-closed.
        /// </summary>
        TagCannotBeSelfClosed = 0x22,
        /// <summary>
        /// (0x23) End tags can never be self-closed.
        /// </summary>
        EndTagCannotBeSelfClosed = 0x23,
        /// <summary>
        /// (0x24) End tags cannot carry attributes.
        /// </summary>
        EndTagCannotHaveAttributes = 0x24,
        /// <summary>
        /// (0x25) No caption has been found within the local scope.
        /// </summary>
        CaptionNotInScope = 0x25,
        /// <summary>
        /// (0x26) No select has been found within the local scope.
        /// </summary>
        SelectNotInScope = 0x26,
        /// <summary>
        /// (0x27) No table row has been found within the local scope.
        /// </summary>
        TableRowNotInScope = 0x27,
        /// <summary>
        /// (0x28) No table has been found within the local scope.
        /// </summary>
        TableNotInScope = 0x28,
        /// <summary>
        /// (0x29) No paragraph has been found within the local scope.
        /// </summary>
        ParagraphNotInScope = 0x29,
        /// <summary>
        /// (0x2a) No body has been found within the local scope.
        /// </summary>
        BodyNotInScope = 0x2a,
        /// <summary>
        /// (0x2b) No block element has been found within the local scope.
        /// </summary>
        BlockNotInScope = 0x2b,
        /// <summary>
        /// (0x2c) No table cell has been found within the local scope.
        /// </summary>
        TableCellNotInScope = 0x2c,
        /// <summary>
        /// (0x2d) No table section has been found within the local scope.
        /// </summary>
        TableSectionNotInScope = 0x2d,
        /// <summary>
        /// (0x2e) No object element has been found within the local scope.
        /// </summary>
        ObjectNotInScope = 0x2e,
        /// <summary>
        /// (0x2f) No heading element has been found within the local scope.
        /// </summary>
        HeadingNotInScope = 0x2f,
        /// <summary>
        /// (0x30) No list item has been found within the local scope.
        /// </summary>
        ListItemNotInScope = 0x30,
        /// <summary>
        /// (0x31) No form has been found within the local scope.
        /// </summary>
        FormNotInScope = 0x31,
        /// <summary>
        /// (0x32) No button has been found within the local scope.
        /// </summary>
        ButtonInScope = 0x32,
        /// <summary>
        /// (0x33) No nobr element has been found within the local scope.
        /// </summary>
        NobrInScope = 0x33,
        /// <summary>
        /// (0x34) No element has been found within the local scope.
        /// </summary>
        ElementNotInScope = 0x34,
        /// <summary>
        /// (0x35) Character reference found no numbers.
        /// </summary>
        CharacterReferenceWrongNumber = 0x35,
        /// <summary>
        /// (0x36) Character reference found no semicolon.
        /// </summary>
        CharacterReferenceSemicolonMissing = 0x36,
        /// <summary>
        /// (0x37) Character reference within an invalid range.
        /// </summary>
        CharacterReferenceInvalidRange = 0x37,
        /// <summary>
        /// (0x38) Character reference is an invalid number.
        /// </summary>
        CharacterReferenceInvalidNumber = 0x38,
        /// <summary>
        /// (0x39) Character reference is an invalid code.
        /// </summary>
        CharacterReferenceInvalidCode = 0x39,
        /// <summary>
        /// (0x3a) Character reference is not terminated by a semicolon.
        /// </summary>
        CharacterReferenceNotTerminated = 0x3a,
        /// <summary>
        /// (0x3b) Character reference in attribute contains an invalid character (=).
        /// </summary>
        CharacterReferenceAttributeEqualsFound = 0x3b,
        /// <summary>
        /// (0x40) Doctype unexpected character after the name detected.
        /// </summary>
        DoctypeUnexpectedAfterName = 0x40,
        /// <summary>
        /// (0x41) Invalid character in the public identifier detected.
        /// </summary>
        DoctypePublicInvalid = 0x41,
        /// <summary>
        /// (0x42) Invalid character in the doctype detected.
        /// </summary>
        DoctypeInvalidCharacter = 0x42,
        /// <summary>
        /// (0x43) Invalid character in the system identifier detected.
        /// </summary>
        DoctypeSystemInvalid = 0x43,
        /// <summary>
        /// (0x44) The doctype tag is misplaced and ignored.
        /// </summary>
        DoctypeTagInappropriate = 0x44,
        /// <summary>
        /// (0x45) The given doctype tag is invalid.
        /// </summary>
        DoctypeInvalid = 0x45,
        /// <summary>
        /// (0x46) Doctype encountered unexpected character.
        /// </summary>
        DoctypeUnexpected = 0x46,
        /// <summary>
        /// (0x47) The doctype tag is missing.
        /// </summary>
        DoctypeMissing = 0x47,
        /// <summary>
        /// (0x50) The double quotation marks have been misplaced.
        /// </summary>
        DoubleQuotationMarkUnexpected = 0x50,
        /// <summary>
        /// (0x51) The single quotation marks have been misplaced.
        /// </summary>
        SingleQuotationMarkUnexpected = 0x51,
        /// <summary>
        /// (0x60) The attribute's name contains an invalid character.
        /// </summary>
        AttributeNameInvalid = 0x60,
        /// <summary>
        /// (0x61) The attribute's value contains an invalid character.
        /// </summary>
        AttributeValueInvalid = 0x61,
        /// <summary>
        /// (0x62) The beginning of a new attribute has been expected.
        /// </summary>
        AttributeNameExpected = 0x62,
        /// <summary>
        /// (0x63) The attribute has already been added.
        /// </summary>
        AttributeDuplicateOmitted = 0x63,
        /// <summary>
        /// (0x70) The given tag must be placed in head tag.
        /// </summary>
        TagMustBeInHead = 0x70,
        /// <summary>
        /// (0x71) The given tag is not appropriate for the current position.
        /// </summary>
        TagInappropriate = 0x71,
        /// <summary>
        /// (0x72) The given tag cannot end at the current position.
        /// </summary>
        TagCannotEndHere = 0x72,
        /// <summary>
        /// (0x73) The given tag cannot start at the current position.
        /// </summary>
        TagCannotStartHere = 0x73,
        /// <summary>
        /// (0x74) The given form cannot be placed at the current position.
        /// </summary>
        FormInappropriate = 0x74,
        /// <summary>
        /// (0x75) The given input cannot be placed at the current position.
        /// </summary>
        InputUnexpected = 0x75,
        /// <summary>
        /// (0x76) The closing tag and the currently open tag do not match.
        /// </summary>
        TagClosingMismatch = 0x76,
        /// <summary>
        /// (0x77) The given end tag does not match the current node.
        /// </summary>
        TagDoesNotMatchCurrentNode = 0x77,
        /// <summary>
        /// (0x78) This position does not support a linebreak (LF, FF).
        /// </summary>
        LineBreakUnexpected = 0x78,
        /// <summary>
        /// (0x80) The head tag can only be placed once inside the html tag.
        /// </summary>
        HeadTagMisplaced = 0x80,
        /// <summary>
        /// (0x81) The html tag can only be placed once as the root element.
        /// </summary>
        HtmlTagMisplaced = 0x81,
        /// <summary>
        /// (0x82) The body tag can only be placed once inside the html tag.
        /// </summary>
        BodyTagMisplaced = 0x82,
        /// <summary>
        /// (0x83) The image tag has been named image instead of img.
        /// </summary>
        ImageTagNamedWrong = 0x83,
        /// <summary>
        /// (0x84) Tables cannot be nested.
        /// </summary>
        TableNesting = 0x84,
        /// <summary>
        /// (0x85) An illegal element has been detected in a table.
        /// </summary>
        IllegalElementInTableDetected = 0x85,
        /// <summary>
        /// (0x86) Selects cannot be nested.
        /// </summary>
        SelectNesting = 0x86,
        /// <summary>
        /// (0x87) An illegal element has been detected in a select.
        /// </summary>
        IllegalElementInSelectDetected = 0x87,
        /// <summary>
        /// (0x88) The frameset element has been misplaced.
        /// </summary>
        FramesetMisplaced = 0x88,
        /// <summary>
        /// (0x89) Headings cannot be nested.
        /// </summary>
        HeadingNested = 0x89,
        /// <summary>
        /// (0x8a) Anchor elements cannot be nested.
        /// </summary>
        AnchorNested = 0x8a,
        /// <summary>
        /// (0x90) The given token cannot be inserted here.
        /// </summary>
        TokenNotPossible = 0x90,
        /// <summary>
        /// (0x91) The current node is not the root element.
        /// </summary>
        CurrentNodeIsNotRoot = 0x91,
        /// <summary>
        /// (0x92) The current node is the root element.
        /// </summary>
        CurrentNodeIsRoot = 0x92,
        /// <summary>
        /// (0x93) This tag is invalid in fragment mode.
        /// </summary>
        TagInvalidInFragmentMode = 0x93,
        /// <summary>
        /// (0x94) There is already an open form.
        /// </summary>
        FormAlreadyOpen = 0x94,
        /// <summary>
        /// (0x95) The form has been closed wrong.
        /// </summary>
        FormClosedWrong = 0x95,
        /// <summary>
        /// (0x96) The body has been closed wrong.
        /// </summary>
        BodyClosedWrong = 0x96,
        /// <summary>
        /// (0x97) An expected formatting element has not been found.
        /// </summary>
        FormattingElementNotFound = 0x97,
        /// <summary>
        /// (0x100) NULL character replaced by repl. character.
        /// </summary>
        NULL = 0x100,
        /// <summary>
        /// (0x101) The action is not supported in the current context.
        /// </summary>
        NotSupported = 0x101
    }
}
