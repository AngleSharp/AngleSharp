namespace AngleSharp.Css
{
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// Represents a comment in the CSS node tree.
    /// </summary>
    public sealed class CssComment : CssNode
    {
        /// <summary>
        /// Creates a new comment node.
        /// </summary>
        internal CssComment(CssToken token)
        {
            Tokens.Add(token);
        }

        /// <summary>
        /// Gets the comment's text.
        /// </summary>
        public String Text
        {
            get { return Tokens[0].Data; }
        }
    }
}
