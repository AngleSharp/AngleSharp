using System;

namespace AngleSharp.Html
{
    /// <summary>
    /// Represents the final token to mark the EOF.
    /// </summary>
    sealed class HtmlEndOfFileToken : HtmlToken
    {
        /// <summary>
        /// Creates a new EOF token.
        /// </summary>
        public HtmlEndOfFileToken()
        {
            _type = HtmlTokenType.EOF;
        }
    }
}
