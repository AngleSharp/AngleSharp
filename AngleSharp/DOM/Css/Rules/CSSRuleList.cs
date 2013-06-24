using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents an array like structure containing CSS rules.
    /// </summary>
    [DOM("CSSRuleList")]
    public sealed class CSSRuleList : IEnumerable<CSSRule>, ICSSObject
    {
        #region Members

        List<CSSRule> _rules;

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
        [DOM("length")]
        public Int32 Length
        {
            get { return _rules.Count; }
        }

        /// <summary>
        /// Gets the CSS rule at the specified index.
        /// </summary>
        /// <param name="index">The index of the rule.</param>
        /// <returns>The CSS rule or null, if the index has been invalid.</returns>
        [DOM("item")]
        public CSSRule this[Int32 index]
        {
            get { return index >= 0 && index < _rules.Count ? _rules[index] : null; }
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets the list with css rules.
        /// </summary>
        internal List<CSSRule> List
        {
            get { return _rules; }
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Removes the rule at the given index.
        /// </summary>
        /// <param name="index">The specified index.</param>
        internal void RemoveAt(Int32 index)
        {
            _rules.RemoveAt(index);
        }

        /// <summary>
        /// Inserts a rule at the specified index.
        /// </summary>
        /// <param name="index">The index where to insert.</param>
        /// <param name="rule">The rule that should be inserted.</param>
        internal void InsertAt(Int32 index, CSSRule rule)
        {
            _rules.Insert(index, rule);
        }

        #endregion

        #region Implemented interface

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<CSSRule> GetEnumerator()
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
            var sb = new StringBuilder();

            for (int i = 0; i < _rules.Count; i++)
                sb.AppendLine(_rules[i].ToCss());

            return sb.ToString();
        }

        #endregion
    }
}
