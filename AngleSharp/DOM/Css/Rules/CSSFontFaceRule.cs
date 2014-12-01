namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the @font-face rule.
    /// </summary>
	sealed class CSSFontFaceRule : CSSRule, ICssFontFaceRule, IPropertyCreator
    {
        #region Fields

        static Dictionary<String, Func<CSSStyleDeclaration, CSSProperty>> _creators = new Dictionary<String, Func<CSSStyleDeclaration, CSSProperty>>(StringComparer.OrdinalIgnoreCase);
        readonly CSSStyleDeclaration _style;

        #endregion

        #region ctor

        static CSSFontFaceRule()
        {
            _creators.Add(PropertyNames.FontFamily, style => new CSSFontFamilyProperty(style));
            _creators.Add(PropertyNames.FontStyle, style => new CSSFontStyleProperty(style));
            _creators.Add(PropertyNames.FontVariant, style => new CSSFontVariantProperty(style));
            _creators.Add(PropertyNames.FontWeight, style => new CSSFontWeightProperty(style));
            _creators.Add(PropertyNames.FontStretch, style => new CSSFontStretchProperty(style));
            //_creators.Add(PropertyNames.FontFeatureSettings, style => new CSSFontFeatureSettingsProperty(style));
            _creators.Add(PropertyNames.UnicodeRange, style => new CSSUnicodeRangeProperty(style));
            _creators.Add(PropertyNames.Src, style => new CSSSrcProperty(style));
        }

        /// <summary>
        /// Creates a new @font-face rule.
        /// </summary>
        internal CSSFontFaceRule()
        {
            _style = new CSSStyleDeclaration(this);
            _type = CssRuleType.FontFace;
        }

        #endregion

        #region Properties

        public CSSStyleDeclaration Style
        {
            get { return _style; }
        }

        /// <summary>
        /// Gets or sets the font-family.
        /// </summary>
        String ICssFontFaceRule.Family
        {
            get { return _style.GetPropertyValue(PropertyNames.FontFamily); }
            set { _style.SetProperty(PropertyNames.FontFamily, value); }
        }

        /// <summary>
        /// Gets or sets the source of the font.
        /// </summary>
        String ICssFontFaceRule.Source
        {
            get { return _style.GetPropertyValue(PropertyNames.Src); }
            set { _style.SetProperty(PropertyNames.Src, value); }
        }

        /// <summary>
        /// Gets or sets the style of the font.
        /// </summary>
        String ICssFontFaceRule.Style
        {
            get { return _style.GetPropertyValue(PropertyNames.FontStyle); }
            set { _style.SetProperty(PropertyNames.FontStyle, value); }
        }

        /// <summary>
        /// Gets or sets the weight of the font.
        /// </summary>
        String ICssFontFaceRule.Weight
        {
            get { return _style.GetPropertyValue(PropertyNames.FontWeight); }
            set { _style.SetProperty(PropertyNames.FontWeight, value); }
        }

        /// <summary>
        /// Gets or sets the stretch value of the font.
        /// </summary>
        String ICssFontFaceRule.Stretch
        {
            get { return _style.GetPropertyValue(PropertyNames.FontStretch); }
            set { _style.SetProperty(PropertyNames.FontStretch, value); }
        }

        /// <summary>
        /// Gets or sets the unicode range of the font.
        /// </summary>
        String ICssFontFaceRule.Range
        {
            get { return _style.GetPropertyValue(PropertyNames.UnicodeRange); }
            set { _style.SetProperty(PropertyNames.UnicodeRange, value); }
        }

        /// <summary>
        /// Gets or sets the variant of the font.
        /// </summary>
        String ICssFontFaceRule.Variant
        {
            get { return _style.GetPropertyValue(PropertyNames.FontVariant); }
            set { _style.SetProperty(PropertyNames.FontVariant, value); }
        }

        /// <summary>
        /// Gets or sets the feature settings of the font.
        /// </summary>
        String ICssFontFaceRule.Features
        {
            get { return _style.GetPropertyValue(PropertyNames.FontFeatureSettings); }
            set { _style.SetProperty(PropertyNames.FontFeatureSettings, value); }
        }

        #endregion

        #region Internal methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = rule as CSSFontFaceRule;
            _style.TakeFrom(newRule._style);
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        protected override String ToCss()
        {
            return String.Concat("@font-face ", _style.ToCssBlock());
        }

        #endregion

        #region Property Creator

        CSSProperty IPropertyCreator.Create(String name, CSSStyleDeclaration style)
        {
            Func<CSSStyleDeclaration, CSSProperty> creator;

            if (_creators.TryGetValue(name, out creator))
                return creator(style);

            return null;
        }

        #endregion
    }
}
