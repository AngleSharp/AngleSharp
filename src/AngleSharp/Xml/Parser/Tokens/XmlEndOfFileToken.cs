namespace AngleSharp.Xml.Parser.Tokens
{
    using AngleSharp.Text;

    /// <summary>
    /// Represents the final token to mark the EOF.
    /// </summary>
    sealed class XmlEndOfFileToken : XmlToken
    {
        /// <summary>
        /// Creates a new EOF token.
        /// </summary>
        public XmlEndOfFileToken(TextPosition position)
            : base(XmlTokenType.EndOfFile, position)
        {
        }
    }
}
