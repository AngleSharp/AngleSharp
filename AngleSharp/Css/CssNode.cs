namespace AngleSharp.Css
{
    using AngleSharp.Parser.Css;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a node in the CSS parse tree.
    /// </summary>
    public class CssNode
    {
        #region Fields

        readonly List<CssToken> _tokens;
        readonly List<CssNode> _children;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new node in the CSS tree.
        /// </summary>
        public CssNode()
        {
            _tokens = new List<CssToken>();
            _children = new List<CssNode>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the associated entity.
        /// </summary>
        public IStyleFormattable Entity
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the node's children.
        /// </summary>
        public List<CssNode> Children
        {
            get { return _children; }
        }

        /// <summary>
        /// Gets the start of the node.
        /// </summary>
        public TextPosition Start
        {
            get { return _tokens.Count > 0 ? _tokens[0].Position : TextPosition.Empty; }
        }

        /// <summary>
        /// Gets the end of the node.
        /// </summary>
        public TextPosition End
        {
            get { return _tokens.Count > 0 ? _tokens[_tokens.Count - 1].Position : TextPosition.Empty; }
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets the associated tokens.
        /// </summary>
        internal List<CssToken> Tokens
        {
            get { return _tokens; }
        }

        #endregion
    }
}
