namespace AngleSharp.DOM.Css
{
    using System;
    using AngleSharp.DOM.Collections;
    using AngleSharp.Parser.Css;

    /// <summary>
    /// Represents a CSS style rule.
    /// </summary>
    [DOM("CSSStyleRule")]
	public sealed class CSSStyleRule : CSSRule
    {
        #region Fields

        readonly CSSStyleDeclaration _style;
        String _selectorText;
        Selector _selector;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS style rule.
        /// </summary>
        internal CSSStyleRule()
        {
            _type = CssRuleType.Style;
            _style = new CSSStyleDeclaration();
        }

		/// <summary>
		/// Creates a new CSS style rule with the given declaration.
		/// </summary>
		/// <param name="style">The declaration to use.</param>
		internal CSSStyleRule(CSSStyleDeclaration style)
		{
			_type = CssRuleType.Style;
			_style = style;
		}

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selector for matching elements.
        /// </summary>
        public Selector Selector
        {
            get { return _selector; }
            internal set
            {
                _selector = value;
                _selectorText = value.ToCss();
            }
        }

        /// <summary>
        /// Gets or sets the textual representation of the selector for this rule, e.g. "h1,h2".
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
        /// Gets the CSSStyleDeclaration object for the rule.
        /// </summary>
        [DOM("style")]
        public CSSStyleDeclaration Style
        {
            get { return _style; }
        }

        #endregion

        #region Internal Methods

        internal override void ComputeStyle(CSSStyleDeclaration style, IWindow window, Element element)
        {
            if (_selector.Match(element))
                style.ExtendWith(_style);
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return _selectorText + " {" + Environment.NewLine + _style.ToCss() + "}";
        }

        #endregion
	}
}
