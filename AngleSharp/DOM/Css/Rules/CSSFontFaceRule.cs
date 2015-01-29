namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the @font-face rule.
    /// </summary>
	sealed class CssFontFaceRule : CssRule, ICssFontFaceRule, IPropertyCreator
    {
        #region Fields

        static Dictionary<String, Func<CssStyleDeclaration, CssProperty>> _creators = new Dictionary<String, Func<CssStyleDeclaration, CssProperty>>(StringComparer.OrdinalIgnoreCase);
        readonly CssStyleDeclaration _style;

        #endregion

        #region ctor

        static CssFontFaceRule()
        {
            _creators.Add(PropertyNames.FontFamily, style => new CssFontFamilyProperty(style));
            _creators.Add(PropertyNames.FontStyle, style => new CssFontStyleProperty(style));
            _creators.Add(PropertyNames.FontVariant, style => new CssFontVariantProperty(style));
            _creators.Add(PropertyNames.FontWeight, style => new CssFontWeightProperty(style));
            _creators.Add(PropertyNames.FontStretch, style => new CssFontStretchProperty(style));
            //_creators.Add(PropertyNames.FontFeatureSettings, style => new CSSFontFeatureSettingsProperty(style));
            _creators.Add(PropertyNames.UnicodeRange, style => new CssUnicodeRangeProperty(style));
            _creators.Add(PropertyNames.Src, style => new CssSrcProperty(style));
        }

        /// <summary>
        /// Creates a new @font-face rule.
        /// </summary>
        internal CssFontFaceRule()
            : base(CssRuleType.FontFace)
        {
            _style = new CssStyleDeclaration(this);
        }

        #endregion

        #region Properties

        public CssStyleDeclaration Style
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
            var newRule = (CssFontFaceRule)rule;
            _style.Clear();
            _style.SetDeclarations(newRule._style);
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

        CssProperty IPropertyCreator.Create(String name, CssStyleDeclaration style)
        {
            Func<CssStyleDeclaration, CssProperty> creator;

            if (_creators.TryGetValue(name, out creator))
                return creator(style);

            return null;
        }

        #endregion
    }
}
