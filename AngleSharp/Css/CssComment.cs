namespace AngleSharp.Css
{
    using System;

    /// <summary>
    /// Represents a comment in the CSS node tree.
    /// </summary>
    public sealed class CssComment : CssNode
    {
        readonly String _text;

        /// <summary>
        /// Creates a new comment node.
        /// </summary>
        /// <param name="text">The text of the comment.</param>
        /// <param name="position">The comment's position.</param>
        public CssComment(String text, TextPosition position)
        {
            _text = text;
            Start = position;
            End = position.Shift(2).After(text).Shift(1);
        }

        /// <summary>
        /// Gets the comment's text.
        /// </summary>
        public String Text
        {
            get { return _text; }
        }
    }
}
