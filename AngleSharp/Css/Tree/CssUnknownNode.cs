namespace AngleSharp.Css.Tree
{
    using System;

    /// <summary>
    /// Represents an unknown part in the CSS AST.
    /// </summary>
    public class CssUnknownNode : CssNode
    {
        /// <summary>
        /// Creates a new unknown node for the CSS AST.
        /// </summary>
        /// <param name="text">The contained text.</param>
        /// <param name="range">The covered range in the source.</param>
        public CssUnknownNode(String text, TextRange range)
            : base(range)
        {
            Text = text;
        }

        /// <summary>
        /// Gets the associated unknown text.
        /// </summary>
        public String Text
        {
            get;
            private set;
        }
    }
}
