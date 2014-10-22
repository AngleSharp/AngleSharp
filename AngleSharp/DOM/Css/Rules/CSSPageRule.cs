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

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new @page rule.
        /// </summary>
        /// <param name="style">The style declaration to use.</param>
        internal CSSPageRule(CSSStyleDeclaration style)
        {
            _style = style;
            _style.Parent = this;
            _type = CssRuleType.Page;
            _selector = SimpleSelector.All;
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            base.ReplaceWith(rule);
            var newRule = rule as CSSPageRule;
            _style = newRule._style;
            _selector = newRule._selector;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selector for matching pages.
        /// </summary>
        public ISelector Selector
        {
            get { return _selector; }
            set { _selector = value; }
        }

        /// <summary>
        /// Gets the parsable textual representation of the page selector for the rule.
        /// </summary>
        public String SelectorText
        {
            get { return _selector.Text; }
            set
            {
                var selector = CssParser.ParseSelector(value);

                if (selector != null)
                    _selector = selector;
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
            var inner = String.Concat(" { ", _style.ToCss(), _style.Length > 0 ? " }" : "}");
            return String.Concat("@page ", _selector.Text, inner);
        }

        #endregion
	}
}
