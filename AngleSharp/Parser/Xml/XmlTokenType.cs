namespace AngleSharp.Parser.Xml
{
    /// <summary>
    /// An enumation of all possible tokens.
    /// </summary>
    enum XmlTokenType
    {
        /// <summary>
        /// The DOCTYPE token.
        /// </summary>
        Doctype,
        /// <summary>
        /// The XML declaration.
        /// </summary>
        Declaration,
        /// <summary>
        /// The start tag token to mark open tags.
        /// </summary>
        StartTag,
        /// <summary>
        /// The end tag token to mark ending tags.
        /// </summary>
        EndTag,
        /// <summary>
        /// The comment tag to mark comments.
        /// </summary>
        Comment,
        /// <summary>
        /// The CData token for such regions.
        /// </summary>
        CData,
        /// <summary>
        /// The character token to mark a single character.
        /// </summary>
        Character,
        /// <summary>
        /// A charref token to mark character references.
        /// </summary>
        CharacterReference,
        /// <summary>
        /// An entity token to mark entity elements.
        /// </summary>
        Entity,
        /// <summary>
        /// A processing instruction token to mark such elements.
        /// </summary>
        ProcessingInstruction,
        /// <summary>
        /// The End-Of-File token to mark the end.
        /// </summary>
        EndOfFile
    }
}
