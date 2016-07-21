namespace AngleSharp.Dom.Css
{
    using AngleSharp.Extensions;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an array like structure containing CSS rules.
    /// </summary>
    sealed class CssRuleList : ICssRuleList
    {
        #region Fields

        readonly CssNode _parent;

        #endregion

        #region ctor

        internal CssRuleList(CssNode parent)
        {
            _parent = parent;
        }

        #endregion

        #region Index

        public CssRule this[Int32 index]
        {
            get { return Nodes.GetItemByIndex(index); }
        }

        ICssRule ICssRuleList.this[Int32 index]
        {
            get { return this[index]; }
        }

        #endregion

        #region Properties

        public Boolean HasDeclarativeRules
        {
            get { return Nodes.Any(m => IsDeclarativeRule(m)); }
        }

        public IEnumerable<CssRule> Nodes
        {
            get { return _parent.Children.OfType<CssRule>(); }
        }

        public Int32 Length
        {
            get { return Nodes.Count(); }
        }

        #endregion

        #region Internal Methods

        internal void RemoveAt(Int32 index)
        {
            if (index < 0 || index >= Length)
                throw new DomException(DomError.IndexSizeError);

            var rule = this[index];

            if (rule.Type == CssRuleType.Namespace && HasDeclarativeRules)
                throw new DomException(DomError.InvalidState);

            Remove(rule);
        }

        internal void Remove(CssRule rule)
        {
            if (rule != null)
            {
                _parent.RemoveChild(rule);
            }
        }

        internal void Insert(Int32 index, CssRule rule)
        {
            if (rule == null)
                throw new DomException(DomError.Syntax);

            if (rule.Type == CssRuleType.Charset)
                throw new DomException(DomError.Syntax);

            if (index > Length || index < 0)
                throw new DomException(DomError.IndexSizeError);

            if (rule.Type == CssRuleType.Namespace && HasDeclarativeRules)
                throw new DomException(DomError.InvalidState);

            if (index == Length)
            {
                _parent.AppendChild(rule);
            }
            else
            {
                _parent.InsertBefore(this[index], rule);
            }
        }

        internal void Add(CssRule rule)
        {
            if (rule != null)
            {
                _parent.AppendChild(rule);
            }
        }

        #endregion

        #region Implemented Interface

        public IEnumerator<ICssRule> GetEnumerator()
        {
            return Nodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Helper

        static Boolean IsDeclarativeRule(CssRule rule)
        {
            var type = rule.Type;
            return type != CssRuleType.Import && type != CssRuleType.Charset && type != CssRuleType.Namespace;
        }

        #endregion
    }
}
