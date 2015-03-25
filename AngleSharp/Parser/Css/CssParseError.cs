namespace AngleSharp.Parser.Css
{
    using AngleSharp.Attributes;

    /// <summary>
    /// A collection of CSS parse error codes.
    /// </summary>
    enum CssParseError
    {
        /// <summary>
        /// Unexpected end of file detected.
        /// </summary>
        [DomDescription("Unexpected end of the given file.")]
        EOF = 0,
        /// <summary>
        /// The provided character is not valid at the given position.
        /// </summary>
        [DomDescription("The provided character is not valid at the given position.")]
        InvalidCharacter = 0x10,
        /// <summary>
        /// No block can start at the current position.
        /// </summary>
        [DomDescription("No block can start at the current position.")]
        InvalidBlockStart = 0x11,
        /// <summary>
        /// The given token is not valid at the current position.
        /// </summary>
        [DomDescription("The given token is not valid at the current position.")]
        InvalidToken = 0x12,
        /// <summary>
        /// An expected colon is missing.
        /// </summary>
        [DomDescription("An expected colon is missing.")]
        ColonMissing = 0x13,
        /// <summary>
        /// An expected identifier could not be found.
        /// </summary>
        [DomDescription("An expected identifier could not be found.")]
        IdentExpected = 0x14,
        /// <summary>
        /// An given input has not been expected.
        /// </summary>
        [DomDescription("An given input has not been expected.")]
        InputUnexpected = 0x15,
        /// <summary>
        /// This position does not support a linebreak (LF, FF).
        /// </summary>
        [DomDescription("This position does not support a linebreak (LF, FF).")]
        LineBreakUnexpected = 0x16,
        /// <summary>
        /// The name of the @-rule is unknown.
        /// </summary>
        [DomDescription("The name of the @-rule is unknown.")]
        UnknownAtRule = 0x20,
        /// <summary>
        /// The provided selector is invalid.
        /// </summary>
        [DomDescription("The provided selector is invalid.")]
        InvalidSelector = 0x30,
        /// <summary>
        /// The value of the declaration could not be found.
        /// </summary>
        [DomDescription("The value of the declaration could not be found.")]
        ValueMissing = 0x40,
        /// <summary>
        /// The name of the declaration is unknown.
        /// </summary>
        [DomDescription("The name of the declaration is unknown.")]
        UnknownDeclarationName = 0x50,
    }
}
