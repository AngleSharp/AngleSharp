namespace AngleSharp.DOM.Css
{
    using AngleSharp.Parser.Css;
    using AngleSharp.DOM.Collections;
    using System;

    /// <summary>
    /// Represents the @page rule.
    /// </summary>
    [DOM("CSSPageRule")]
	public sealed class CSSPageRule : CSSRule, ICssSelector, IStyleDeclaration
    {
        #region Fields

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
            _type = CssRuleType.Page;
        }

        #endregion

        #region Internal Methods

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
		Selector ICssSelector.Selector
		{
			get
			{
				return _selector;
			}
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
        [DOM("selectorText")]
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
        [DOM("style")]
        public CSSStyleDeclaration Style
        {
            get { return _style; }
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return String.Format("@page {0} {{{1}{2}}}", _selectorText, Environment.NewLine, _style.ToCss());
        }

        #endregion
	}
}
