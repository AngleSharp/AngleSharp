using System;
using System.Collections;
using System.Collections.Generic;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents an array like structure containing CSS rules.
    /// </summary>
    public sealed class CSSRuleList : IEnumerable<CSSRule>
    {
        #region Members

        List<CSSRule> _rules;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new list of CSS rules.
        /// </summary>
        public CSSRuleList()
        {
            _rules = new List<CSSRule>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of rules stored.
        /// </summary>
        public int Length
        {
            get { return _rules.Count; }
        }

        /// <summary>
        /// Gets the CSS rule at the specified index.
        /// </summary>
        /// <param name="index">The index of the rule.</param>
        /// <returns>The CSS rule or null, if the index has been invalid.</returns>
        public CSSRule this[int index]
        {
            get { return index >= 0 && index < _rules.Count ? _rules[index] : null; }
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Removes the rule at the given index.
        /// </summary>
        /// <param name="index">The specified index.</param>
        internal void RemoveAt(int index)
        {
            _rules.RemoveAt(index);
        }

        /// <summary>
        /// Inserts a rule at the specified index.
        /// </summary>
        /// <param name="index">The index where to insert.</param>
        /// <param name="rule">The rule that should be inserted.</param>
        internal void InsertAt(int index, CSSRule rule)
        {
            _rules.Insert(index, rule);
        }

        #endregion

        #region Implemented interface

        public IEnumerator<CSSRule> GetEnumerator()
        {
            return _rules.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_rules).GetEnumerator();
        }

        #endregion
    }
}
