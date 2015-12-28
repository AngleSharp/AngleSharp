namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a node in the CSS parse tree.
    /// </summary>
    abstract class CssNode : ICssNode
    {
        #region Fields

        TextView _source;
        IEnumerable<ICssNode> _children;

        #endregion

        #region ctor

        public CssNode()
        {
            _source = null;
            _children = Enumerable.Empty<ICssNode>();
        }

        #endregion

        #region Properties

        public TextView SourceCode
        {
            get { return _source; }
            internal set { _source = value; }
        }

        public IEnumerable<ICssNode> Children
        {
            get { return _children; }
            protected set { _children = value; }
        }

        #endregion

        #region Methods

        public abstract String ToCss(IStyleFormatter formatter);

        #endregion
    }
}
