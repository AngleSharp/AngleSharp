namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Represents a node in the CSS parse tree.
    /// </summary>
    abstract class CssNode : ICssNode
    {
        #region Fields

        readonly List<ICssNode> _children;
        TextView _source;

        #endregion

        #region ctor

        public CssNode()
        {
            _children = new List<ICssNode>();
            _source = null;
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
            get { return _children.AsEnumerable(); }
        }

        #endregion

        #region Methods

        public abstract void ToCss(TextWriter writer, IStyleFormatter formatter);

        public void AppendChild(ICssNode child)
        {
            Setup(child);
            _children.Add(child);
        }

        public void ReplaceChild(ICssNode oldChild, ICssNode newChild)
        {
            for (var i = 0; i < _children.Count; i++)
            {
                if (Object.ReferenceEquals(oldChild, _children[i]))
                {
                    Teardown(oldChild);
                    Setup(newChild);
                    _children[i] = newChild;
                    return;
                }
            }
        }

        public void InsertBefore(ICssNode referenceChild, ICssNode child)
        {
            if (referenceChild != null)
            {
                var index = _children.IndexOf(referenceChild);
                InsertChild(index, child);
            }
            else
            {
                AppendChild(child);
            }
        }

        public void InsertChild(Int32 index, ICssNode child)
        {
            Setup(child);
            _children.Insert(index, child);
        }

        public void RemoveChild(ICssNode child)
        {
            Teardown(child);
            _children.Remove(child);
        }

        public void Clear()
        {
            for (int i = _children.Count - 1; i >= 0; i--)
            {
                var child = _children[i];
                RemoveChild(child);
            }
        }

        #endregion

        #region Internal Methods

        protected void ReplaceAll(ICssNode node)
        {
            Clear();
            _source = node.SourceCode;

            foreach (var child in node.Children)
            {
                AppendChild(child);
            }
        }

        #endregion

        #region Helper

        void Setup(ICssNode child)
        {
            var rule = child as CssRule;

            if (rule != null)
            {
                rule.Owner = this as ICssStyleSheet;
                rule.Parent = this as ICssRule;
            }
        }

        void Teardown(ICssNode child)
        {
            var rule = child as CssRule;

            if (rule != null)
            {
                rule.Parent = null;
                rule.Owner = null;
            }
        }

        #endregion
    }
}
