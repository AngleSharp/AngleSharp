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

        readonly CSSStyleDeclaration _style;
        ISelector _selector;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new @page rule.
        /// </summary>
        internal CSSPageRule()
        {
            _style = new CSSStyleDeclaration(this);
            _type = CssRuleType.Page;
            _selector = SimpleSelector.All;
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            base.ReplaceWith(rule);
            var newRule = rule as CSSPageRule;
            _style.TakeFrom(newRule._style);
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
        ICssStyleDeclaration ICssPageRule.Style
        {
            get { return _style; }
        }

        /// <summary>
        /// Gets the  declaration-block of this rule.
        /// </summary>
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
        protected override String ToCss()
        {
            var inner = String.Concat(" { ", _style.CssText, _style.Length > 0 ? " }" : "}");
            return String.Concat("@page ", _selector.Text, inner);
        }

        #endregion
	}
}
