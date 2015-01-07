namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// Represents a CSS style rule.
    /// </summary>
	sealed class CssStyleRule : CssRule, ICssStyleRule
    {
        #region Fields

        readonly CssStyleDeclaration _style;
        ISelector _selector;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS style rule.
        /// </summary>
        internal CssStyleRule()
            : base(CssRuleType.Style)
        {
            _style = new CssStyleDeclaration(this);
            _selector = SimpleSelector.All;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the selector for matching elements.
        /// </summary>
        public ISelector Selector
        {
            get { return _selector; }
            set { _selector = value; }
        }

        /// <summary>
        /// Gets or sets the textual representation of the selector for this rule, e.g. "h1,h2".
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
        /// Gets the CSSStyleDeclaration object for the rule.
        /// </summary>
        ICssStyleDeclaration ICssStyleRule.Style
        {
            get { return _style; }
        }

        public CssStyleDeclaration Style
        {
            get { return _style; }
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = rule as CssStyleRule;
            _style.TakeFrom(newRule._style);
            _selector = newRule._selector;
        }

        internal override void ComputeStyle(PropertyBag style, RenderDevice device, IElement element)
        {
            if (_selector.Match(element))
                _style.ApplyTo(style, _selector.Specifity);
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        protected override String ToCss()
        {
            return String.Concat(_selector.Text, " ", _style.ToCssBlock());
        }

        #endregion
	}
}
