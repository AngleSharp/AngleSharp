namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents the @font-face rule.
    /// </summary>
	sealed class CSSFontFaceRule : CSSRule, ICssFontFaceRule
    {
        #region Fields

        CSSStyleDeclaration _style;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new @font-face rule.
        /// </summary>
        internal CSSFontFaceRule()
        {
            _style = new CSSStyleDeclaration();
            _type = CssRuleType.FontFace;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the font-family.
        /// </summary>
        public String Family
        {
            get { return _style.GetPropertyValue(PropertyNames.FontFamily); }
            set { _style.SetProperty(PropertyNames.FontFamily, value); }
        }

        /// <summary>
        /// Gets or sets the source of the font.
        /// </summary>
        public String Source
        {
            get { return _style.GetPropertyValue(AttributeNames.Src); }
            set { _style.SetProperty(AttributeNames.Src, value); }
        }

        /// <summary>
        /// Gets or sets the style of the font.
        /// </summary>
        public String Style
        {
            get { return _style.GetPropertyValue(PropertyNames.FontStyle); }
            set { _style.SetProperty(PropertyNames.FontStyle, value); }
        }

        /// <summary>
        /// Gets or sets the weight of the font.
        /// </summary>
        public String Weight
        {
            get { return _style.GetPropertyValue(PropertyNames.FontWeight); }
            set { _style.SetProperty(PropertyNames.FontWeight, value); }
        }

        /// <summary>
        /// Gets or sets the stretch value of the font.
        /// </summary>
        public String Stretch
        {
            get { return _style.GetPropertyValue(PropertyNames.FontStretch); }
            set { _style.SetProperty(PropertyNames.FontStretch, value); }
        }

        /// <summary>
        /// Gets or sets the unicode range of the font.
        /// </summary>
        public String Range
        {
            get { return _style.GetPropertyValue(PropertyNames.UnicodeRange); }
            set { _style.SetProperty(PropertyNames.UnicodeRange, value); }
        }

        /// <summary>
        /// Gets or sets the variant of the font.
        /// </summary>
        public String Variant
        {
            get { return _style.GetPropertyValue(PropertyNames.FontVariant); }
            set { _style.SetProperty(PropertyNames.FontVariant, value); }
        }

        /// <summary>
        /// Gets or sets the feature settings of the font.
        /// </summary>
        public String Features
        {
            get { return _style.GetPropertyValue(PropertyNames.FontFeatureSettings); }
            set { _style.SetProperty(PropertyNames.FontFeatureSettings, value); }
        }

        #endregion

        #region Internal methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = rule as CSSFontFaceRule;
            _style = newRule._style;
        }

        /// <summary>
        /// Appends the given rule to the list of rules.
        /// </summary>
        /// <param name="rule">The rule to append.</param>
        /// <returns>The current font-face rule.</returns>
        internal CSSFontFaceRule AppendRule(CSSProperty rule)
        {
            _style.Set(rule);
            return this;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return "@font-face {" + Environment.NewLine + _style.ToCss() + "}";
        }

        #endregion

		#region Style Declaration

		/// <summary>
		/// Gets the style declaration.
		/// </summary>
		public CSSStyleDeclaration Styles
		{
			get { return _style; }
		}

		#endregion
	}
}
