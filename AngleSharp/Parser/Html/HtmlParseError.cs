namespace AngleSharp.Parser.Html
{
    using AngleSharp.Attributes;

    /// <summary>
    /// A collection of HTML parse error codes.
    /// </summary>
    enum HtmlParseError
    {
        /// <summary>
        /// Unexpected end of file detected.
        /// </summary>
        [DomDescription("Unexpected end of the given file.")]
        EOF = 0x00,
        /// <summary>
        /// NULL character replaced by repl. character.
        /// </summary>
        [DomDescription("NULL character replaced by repl. character.")]
        Null = 0x01,
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
    }
}
