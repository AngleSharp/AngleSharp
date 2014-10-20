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
        internal CSSFontFaceRule(CSSStyleDeclaration style)
        {
            _style = style;
            _style.ParentRule = this;
            _type = CssRuleType.FontFace;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the font-family.
        /// </summary>
        public String Family
        {
            get { return _style.GetPropertyText(PropertyNames.FontFamily); }
            set { _style.SetProperty(PropertyNames.FontFamily, value); }
        }

        /// <summary>
        /// Gets or sets the source of the font.
        /// </summary>
        public String Source
        {
            get { return _style.GetPropertyText(AttributeNames.Src); }
            set { _style.SetProperty(AttributeNames.Src, value); }
        }

        /// <summary>
        /// Gets or sets the style of the font.
        /// </summary>
        public String Style
        {
            get { return _style.GetPropertyText(PropertyNames.FontStyle); }
            set { _style.SetProperty(PropertyNames.FontStyle, value); }
        }

        /// <summary>
        /// Gets or sets the weight of the font.
        /// </summary>
        public String Weight
        {
            get { return _style.GetPropertyText(PropertyNames.FontWeight); }
            set { _style.SetProperty(PropertyNames.FontWeight, value); }
        }

        /// <summary>
        /// Gets or sets the stretch value of the font.
        /// </summary>
        public String Stretch
        {
            get { return _style.GetPropertyText(PropertyNames.FontStretch); }
            set { _style.SetProperty(PropertyNames.FontStretch, value); }
        }

        /// <summary>
        /// Gets or sets the unicode range of the font.
        /// </summary>
        public String Range
        {
            get { return _style.GetPropertyText(PropertyNames.UnicodeRange); }
            set { _style.SetProperty(PropertyNames.UnicodeRange, value); }
        }

        /// <summary>
        /// Gets or sets the variant of the font.
        /// </summary>
        public String Variant
        {
            get { return _style.GetPropertyText(PropertyNames.FontVariant); }
            set { _style.SetProperty(PropertyNames.FontVariant, value); }
        }

        /// <summary>
        /// Gets or sets the feature settings of the font.
        /// </summary>
        public String Features
        {
            get { return _style.GetPropertyText(PropertyNames.FontFeatureSettings); }
            set { _style.SetProperty(PropertyNames.FontFeatureSettings, value); }
        }

        #endregion

        #region Internal methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = rule as CSSFontFaceRule;
            _style = newRule._style;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return String.Concat("@font-face { ", _style.ToCss(), _style.Length > 0 ? " }" : "}");
        }

        #endregion
	}
}
