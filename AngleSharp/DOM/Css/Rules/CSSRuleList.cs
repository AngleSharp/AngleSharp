namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

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
        public ICssRule this[Int32 index]
        {
            get { return index >= 0 && index < _rules.Count ? _rules[index] : null; }
        }

        #endregion

        #region Internal Properties

        internal List<CSSRule> List
        {
            get { return _rules; }
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
