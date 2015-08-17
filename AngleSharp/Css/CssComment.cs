namespace AngleSharp.Css
{
    using System;

    /// <summary>
    /// Represents a comment in the CSS AST.
    /// </summary>
    public class CssComment : CssNode
    {
        /// <summary>
        /// Creates a new comment for the CSS AST.
        /// </summary>
        /// <param name="content">The contained content.</param>
        /// <param name="range">The covered range in the source.</param>
        public CssComment(String content, TextRange range)
            : base(range)
        {
            Content = content;
        }

        /// <summary>
        /// Gets the associated trivia content.
        /// </summary>
        public String Content
        {
            get;
            private set;
        }
    }
}
