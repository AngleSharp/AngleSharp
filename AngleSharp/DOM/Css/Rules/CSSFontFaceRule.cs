namespace AngleSharp.DOM.Css
{
    using AngleSharp.DOM.Collections;
    using System;

    /// <summary>
    /// Represents the @font-face rule.
    /// </summary>
    [DomName("CSSFontFaceRule")]
	public sealed class CSSFontFaceRule : CSSRule
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

        #region Properties

        /// <summary>
        /// Gets the declared CSS rules.
        /// </summary>
        [DomName("cssRules")]
        public CSSStyleDeclaration CssRules
        {
            get { return _style; }
        }

        /// <summary>
        /// Gets or sets the font-family.
        /// </summary>
        [DomName("family")]
        public String Family
        {
            get { return _style.GetPropertyValue("font-family"); }
            set { _style.SetProperty("font-family", value); }
        }

        /// <summary>
        /// Gets or sets the source of the font.
        /// </summary>
        [DomName("src")]
        public String Src
        {
            get { return _style.GetPropertyValue("src"); }
            set { _style.SetProperty("src", value); }
        }

        /// <summary>
        /// Gets or sets the style of the font.
        /// </summary>
        [DomName("style")]
        public String Style
        {
            get { return _style.GetPropertyValue("font-style"); }
            set { _style.SetProperty("font-style", value); }
        }

        /// <summary>
        /// Gets or sets the weight of the font.
        /// </summary>
        [DomName("weight")]
        public String Weight
        {
            get { return _style.GetPropertyValue("font-weight"); }
            set { _style.SetProperty("font-weight", value); }
        }

        /// <summary>
        /// Gets or sets the stretch value of the font.
        /// </summary>
        [DomName("stretch")]
        public String Stretch
        {
            get { return _style.GetPropertyValue("stretch"); }
            set { _style.SetProperty("stretch", value); }
        }

        /// <summary>
        /// Gets or sets the unicode range of the font.
        /// </summary>
        [DomName("unicodeRange")]
        public String UnicodeRange
        {
            get { return _style.GetPropertyValue("unicode-range"); }
            set { _style.SetProperty("unicode-range", value); }
        }

        /// <summary>
        /// Gets or sets the variant of the font.
        /// </summary>
        [DomName("variant")]
        public String Variant
        {
            get { return _style.GetPropertyValue("font-variant"); }
            set { _style.SetProperty("font-variant", value); }
        }

        /// <summary>
        /// Gets or sets the feature settings of the font.
        /// </summary>
        [DomName("featureSettings")]
        public String FeatureSettings
        {
            get { return _style.GetPropertyValue("font-feature-settings"); }
            set { _style.SetProperty("font-feature-settings", value); }
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
