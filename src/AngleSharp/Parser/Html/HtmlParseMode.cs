namespace AngleSharp.Parser.Html
{
    /// <summary>
    /// Defines the different tokenization content models.
    /// </summary>
    enum HtmlParseMode
    {
        /// <summary>
        /// Initial state: Parsed Character Data (characters will be parsed).
        /// </summary>
        PCData,
        /// <summary>
        /// Optional state: Raw character data (characters will be parsed from a special table).
        /// </summary>
        RCData,
        /// <summary>
        /// Optional state: Just plain text data (chracters will be parsed matching the given ones).
        /// </summary>
        Plaintext,
        /// <summary>
        /// Optional state: Rawtext data (characters will not be parsed).
        /// </summary>
        Rawtext,
        /// <summary>
        /// Optional state: Script data.
        /// </summary>
        Script
    }
}
