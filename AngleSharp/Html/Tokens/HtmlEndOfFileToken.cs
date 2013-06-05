using System;

namespace AngleSharp.Html
{
    /// <summary>
    /// Represents the final token to mark the EOF.
    /// </summary>
    class HtmlEndOfFileToken : HtmlToken
    {
        readonly static HtmlEndOfFileToken token = new HtmlEndOfFileToken();

        /// <summary>
        /// Creates a new EOF token.
        /// </summary>
        private HtmlEndOfFileToken()
        {
            _type = HtmlTokenType.EOF;
        }

        /// <summary>
        /// Gets the actual EOF token.
        /// </summary>
        public static HtmlEndOfFileToken Token
        {
            get { return token; }
        }
    }
}
