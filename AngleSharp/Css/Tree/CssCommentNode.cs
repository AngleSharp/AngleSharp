namespace AngleSharp.Css.Tree
{
    using System;

    /// <summary>
    /// Represents a comment in the CSS AST.
    /// </summary>
    public class CssCommentNode : CssNode
    {
        /// <summary>
        /// Creates a new comment for the CSS AST.
        /// </summary>
        /// <param name="content">The contained content.</param>
        /// <param name="range">The covered range in the source.</param>
        public CssCommentNode(String content, TextRange range)
            : base(range)
        {
            Content = content;
        }

        /// <summary>
        /// Gets the associated comment.
        /// </summary>
        public String Content
        {
            get;
            private set;
        }
    }
}
