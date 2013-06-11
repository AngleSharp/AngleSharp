using AngleSharp.DOM.Collections;
using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the @page rule.
    /// </summary>
    sealed class CSSPageRule : CSSRule
    {
        #region Members

        CSSStyleDeclaration style;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new @page rule.
        /// </summary>
        public CSSPageRule()
        {
            style = new CSSStyleDeclaration();
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Appends the given rule to the list of rules.
        /// </summary>
        /// <param name="rule">The rule to append.</param>
        /// <returns>The current font-face rule.</returns>
        internal CSSPageRule AppendRule(CSSProperty rule)
        {
            style.AppendProperty(rule);
            return this;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the parsable textual representation of the page selector for the rule.
        /// </summary>
        public string SelectorText
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the  declaration-block of this rule.
        /// </summary>
        public CSSStyleDeclaration Style
        {
            get { return style; }
        }

        #endregion
    }
}
