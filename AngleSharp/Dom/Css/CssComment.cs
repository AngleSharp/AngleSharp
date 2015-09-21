namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a comment in the CSS node tree.
    /// </summary>
    sealed class CssComment : ICssComment
    {
        #region Fields

        readonly String _data;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new comment node.
        /// </summary>
        public CssComment(String data)
        {
            _data = data;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the comment's text.
        /// </summary>
        public String Data
        {
            get { return _data; }
        }

        /// <summary>
        /// Gets the contained nodes.
        /// </summary>
        public IEnumerable<ICssNode> Children
        {
            get { return Enumerable.Empty<ICssNode>(); }
        }

        #endregion

        #region String Representation

        public String ToCss()
        {
            return ToCss(CssStyleFormatter.Instance);
        }

        public String ToCss(IStyleFormatter formatter)
        {
            return formatter.Comment(_data);
        }

        #endregion
    }
}
