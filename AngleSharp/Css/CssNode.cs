namespace AngleSharp.Css
{
    using AngleSharp.Parser.Css;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a node in the CSS parse tree.
    /// </summary>
    public class CssNode
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
    }
}
