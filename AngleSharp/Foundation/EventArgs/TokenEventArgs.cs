using System;

namespace AngleSharp.Html
{
    /// <summary>
    /// The TokenEventArgs package.
    /// </summary>
    class TokenEventArgs : ParserEventArgs
    {
        /// <summary>
        /// Creates a new TokenEventArgs package.
        /// </summary>
        /// <param name="token">The token to pass.</param>
        public TokenEventArgs(HtmlToken token)
        {
            Token = token;
        }

        /// <summary>
        /// Gets the emitted token.
        /// </summary>
        public HtmlToken Token { get; private set; }
    }
}
