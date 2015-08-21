namespace AngleSharp.Css
{
    using AngleSharp.Parser.Css;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a node in the CSS parse tree.
    /// </summary>
    public abstract class CssNode
    {
        List<CssToken> _trivia;
        TextPosition _start;
        TextPosition _end;

        /// <summary>
        /// Gets or sets associated trivia, if any.
        /// </summary>
        internal List<CssToken> Trivia
        {
            get { return _trivia; }
            set { _trivia = value; }
        }

        /// <summary>
        /// Gets the start of the node.
        /// </summary>
        public TextPosition Start
        {
            get { return _start; }
            internal set { _start = value; }
        }

        /// <summary>
        /// Gets the end of the node.
        /// </summary>
        public TextPosition End
        {
            get { return _end; }
            internal set { _end = value; }
        }

        /// <summary>
        /// Gets the contained child nodes, if any.
        /// </summary>
        /// <returns>The iterator over the child nodes.</returns>
        public virtual IEnumerable<CssNode> GetChildren()
        {
            return Enumerable.Empty<CssNode>();
        }
    }
}
