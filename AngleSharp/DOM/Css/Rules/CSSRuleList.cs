namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an array like structure containing CSS rules.
    /// </summary>
    sealed class CSSRuleList : ICssRuleList, ICssObject
    {
        #region Fields

        readonly List<CSSRule> _rules;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new list of CSS rules.
        /// </summary>
        internal CSSRuleList()
        {
            _rules = new List<CSSRule>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of rules stored.
        /// </summary>
        public Int32 Length
        {
            get { return _rules.Count; }
        }

        /// <summary>
        /// Gets the CSS rule at the specified index.
        /// </summary>
        /// <param name="index">The index of the rule.</param>
        /// <returns>The CSS rule or null, if the index has been invalid.</returns>
        ICssRule ICssRuleList.this[Int32 index]
        {
            get { return index >= 0 && index < _rules.Count ? _rules[index] : null; }
        }

        #endregion

        #region Internal Methods

        internal CSSRule this[Int32 index]
        {
            get { return _rules[index]; }
        }

        internal void ComputeStyle(CssPropertyBag style, IWindow window, IElement element)
        {
            foreach (var rule in _rules)
                rule.ComputeStyle(style, window, element);
        }

        internal void Clear()
        {
            for (var i = _rules.Count - 1; i >= 0; i--)
            {
                var oldRule = _rules[i];
                _rules.RemoveAt(i);
                oldRule.Parent = null;
                oldRule.Owner = null;
            }
        }

        internal void Import(CSSRuleList rules, ICssStyleSheet owner, ICssRule parent)
        {
            while (rules._rules.Count > 0)
            {
                var newRule = rules._rules[0];
                rules._rules.RemoveAt(0);
                newRule.Parent = parent;
                newRule.Owner = owner;
                _rules.Add(newRule);
            }
        }

        internal void RemoveAt(Int32 index)
        {
            if (index >= _rules.Count)
                throw new DomException(ErrorCode.IndexSizeError);

            var oldRule = _rules[index];

            if (oldRule.Type == CssRuleType.Namespace && _rules.Any(m => (m.Type != CssRuleType.Import && m.Type != CssRuleType.Charset && m.Type != CssRuleType.Namespace)))
                throw new DomException(ErrorCode.InvalidState);

            _rules.RemoveAt(index);
            oldRule.Parent = null;
            oldRule.Owner = null;
        }

        internal void Insert(CSSRule value, Int32 index)
        {
            if (value == null)
                throw new DomException(ErrorCode.Syntax);
            else if (value.Type == CssRuleType.Charset)
                throw new DomException(ErrorCode.Syntax);
            else if (index > _rules.Count)
                throw new DomException(ErrorCode.IndexSizeError);
            else if (value.Type == CssRuleType.Namespace && _rules.Any(m => (m.Type != CssRuleType.Import && m.Type != CssRuleType.Charset && m.Type != CssRuleType.Namespace)))
                throw new DomException(ErrorCode.InvalidState);

            _rules.Insert(index, value);
        }

        internal void Add(CSSRule value)
        {
            _rules.Add(value);
        }

        #endregion

        #region Implemented interface

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<ICssRule> GetEnumerator()
        {
            foreach (var entry in _rules)
                yield return entry;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rulelist.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public String ToCss()
        {
            var sb = Pool.NewStringBuilder();

            for (int i = 0; i < _rules.Count; i++)
                sb.AppendLine(_rules[i].ToCss());

            return sb.ToPool();
        }

        #endregion
    }
}
