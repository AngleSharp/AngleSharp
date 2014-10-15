namespace AngleSharp.DOM.Css
{
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// Represents a CSS style rule.
    /// </summary>
	sealed class CSSStyleRule : CSSRule, ICssStyleRule
    {
        #region Fields

        CSSStyleDeclaration _style;
        String _selectorText;
        ISelector _selector;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS style rule.
        /// </summary>
        internal CSSStyleRule()
            : this(new CSSStyleDeclaration())
        {
        }

		/// <summary>
		/// Creates a new CSS style rule with the given declaration.
		/// </summary>
		/// <param name="style">The declaration to use.</param>
		internal CSSStyleRule(CSSStyleDeclaration style)
		{
            _style = style;
            _style.ParentRule = this;
            _type = CssRuleType.Style;
		}

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selector for matching elements.
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
        /// Gets or sets the textual representation of the selector for this rule, e.g. "h1,h2".
        /// </summary>
        public String SelectorText
        {
            get { return _selectorText; }
            set
            {
                var selector = CssParser.ParseSelector(value);

                if (selector != null)
                {
                    _selector = selector;
                    _selectorText = value;
                }
            }
        }

        /// <summary>
        /// Gets the CSSStyleDeclaration object for the rule.
        /// </summary>
        public ICssStyleDeclaration Style
        {
            get { return _style; }
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = rule as CSSStyleRule;
            _style = newRule._style;
            _selectorText = newRule._selectorText;
            _selector = newRule._selector;
        }

        internal override void ComputeStyle(CssPropertyBag style, IWindow window, IElement element)
        {
            if (_selector.Match(element))
                style.ExtendWith(_style, _selector.Specifity);
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return String.Concat(_selectorText, " { ", _style.ToCss(), _style.Length > 0 ? " }" : "}");
        }

        #endregion
	}
}
