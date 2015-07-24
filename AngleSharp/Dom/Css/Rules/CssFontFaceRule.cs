namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// Represents the @font-face rule.
    /// </summary>
    sealed class CssFontFaceRule : CssDeclarationRule, ICssFontFaceRule
    {
        #region ctor

        /// <summary>
        /// Creates a new @font-face rule.
        /// </summary>
        internal CssFontFaceRule(CssParser parser)
            : base(CssRuleType.FontFace, RuleNames.FontFace, parser)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the font-family.
        /// </summary>
        String ICssFontFaceRule.Family
        {
            get { return GetValue(PropertyNames.FontFamily); }
            set { SetValue(PropertyNames.FontFamily, value); }
        }

        /// <summary>
        /// Gets or sets the source of the font.
        /// </summary>
        String ICssFontFaceRule.Source
        {
            get { return GetValue(PropertyNames.Src); }
            set { SetValue(PropertyNames.Src, value); }
        }

        /// <summary>
        /// Gets or sets the style of the font.
        /// </summary>
        String ICssFontFaceRule.Style
        {
            get { return GetValue(PropertyNames.FontStyle); }
            set { SetValue(PropertyNames.FontStyle, value); }
        }

        /// <summary>
        /// Gets or sets the weight of the font.
        /// </summary>
        String ICssFontFaceRule.Weight
        {
            get { return GetValue(PropertyNames.FontWeight); }
            set { SetValue(PropertyNames.FontWeight, value); }
        }

        /// <summary>
        /// Gets or sets the stretch value of the font.
        /// </summary>
        String ICssFontFaceRule.Stretch
        {
            get { return GetValue(PropertyNames.FontStretch); }
            set { SetValue(PropertyNames.FontStretch, value); }
        }

        /// <summary>
        /// Gets or sets the unicode range of the font.
        /// </summary>
        String ICssFontFaceRule.Range
        {
            get { return GetValue(PropertyNames.UnicodeRange); }
            set { SetValue(PropertyNames.UnicodeRange, value); }
        }

        /// <summary>
        /// Gets or sets the variant of the font.
        /// </summary>
        String ICssFontFaceRule.Variant
        {
            get { return GetValue(PropertyNames.FontVariant); }
            set { SetValue(PropertyNames.FontVariant, value); }
        }

        /// <summary>
        /// Gets or sets the feature settings of the font.
        /// </summary>
        String ICssFontFaceRule.Features
        {
            get { return String.Empty; }
            set { }
        }

        #endregion
    }
}
