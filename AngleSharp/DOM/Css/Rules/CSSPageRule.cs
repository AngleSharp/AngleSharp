using AngleSharp.Css;
using AngleSharp.DOM.Collections;
using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the @page rule.
    /// </summary>
    public sealed class CSSPageRule : CSSRule
    {
        #region Constants

        internal const String RuleName = "page";

        #endregion

        #region Members

        CSSStyleDeclaration _style;
        Selector _selector;
        String _selectorText;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new @page rule.
        /// </summary>
        internal CSSPageRule()
        {
            _style = new CSSStyleDeclaration();
            _type = CssRule.Page;
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
            _style.List.Add(rule);
            return this;
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets or sets the selector.
        /// </summary>
        internal Selector Selector
        {
            get { return _selector; }
            set
            {
                _selector = value;
                _selectorText = value.ToCss();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the parsable textual representation of the page selector for the rule.
        /// </summary>
        public String SelectorText
        {
            get { return _selectorText; }
            set
            {
                _selector = CssParser.ParseSelector(value);
                _selectorText = value;
            }
        }

        /// <summary>
        /// Gets the  declaration-block of this rule.
        /// </summary>
        public CSSStyleDeclaration Style
        {
            get { return _style; }
        }

        #endregion
    }
}
