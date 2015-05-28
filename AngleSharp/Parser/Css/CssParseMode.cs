namespace AngleSharp.Parser.Css
{
    /// <summary>
    /// Defines the different tokenization content models.
    /// </summary>
    enum CssParseMode
    {
        /// <summary>
        /// Initial state: Ordinary data state (drops whitespace).
        /// </summary>
        Data,
        /// <summary>
        /// Optional state: Raw character data (closed consistently).
        /// </summary>
        Text,
        /// <summary>
        /// Optional state: Selector (includes whitespace, emits hashs, ...).
        /// </summary>
        Selector,
        /// <summary>
        /// Optional state: Value (includes whitespaces, emits colors, ...).
        /// </summary>
        Value
    }
}
