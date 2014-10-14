namespace AngleSharp.DOM.Css
{
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// Represents the @page rule.
    /// </summary>
    sealed class CSSPageRule : CSSGroupingRule, ICssPageRule
    {
        #region Fields

        CSSStyleDeclaration _style;
        ISelector _selector;
        String _selectorText;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new @page rule.
        /// </summary>
        /// <param name="style">The style declaration to use.</param>
        internal CSSPageRule(CSSStyleDeclaration style)
        {
            _style = style;
            _style.ParentRule = this;
            _type = CssRuleType.Page;
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            base.ReplaceWith(rule);
            var newRule = rule as CSSPageRule;
            _style = newRule._style;
            _selector = newRule._selector;
            _selectorText = newRule._selectorText;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selector for matching pages.
        /// </summary>
        public ISelector Selector
        {
            get { return _selector; }
            set
            {
                _selector = value;
                _selectorText = value.ToCss();
            }
        }

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
        public ICssStyleDeclaration Style
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
