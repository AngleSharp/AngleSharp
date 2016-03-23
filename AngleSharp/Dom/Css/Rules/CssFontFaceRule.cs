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

        internal CssFontFaceRule(CssParser parser)
            : base(CssRuleType.FontFace, RuleNames.FontFace, parser)
        {
        }

        #endregion

        #region Properties

        String ICssFontFaceRule.Family
        {
            get { return GetValue(PropertyNames.FontFamily); }
            set { SetValue(PropertyNames.FontFamily, value); }
        }

        String ICssFontFaceRule.Source
        {
            get { return GetValue(PropertyNames.Src); }
            set { SetValue(PropertyNames.Src, value); }
        }

        String ICssFontFaceRule.Style
        {
            get { return GetValue(PropertyNames.FontStyle); }
            set { SetValue(PropertyNames.FontStyle, value); }
        }

        String ICssFontFaceRule.Weight
        {
            get { return GetValue(PropertyNames.FontWeight); }
            set { SetValue(PropertyNames.FontWeight, value); }
        }

        String ICssFontFaceRule.Stretch
        {
            get { return GetValue(PropertyNames.FontStretch); }
            set { SetValue(PropertyNames.FontStretch, value); }
        }

        String ICssFontFaceRule.Range
        {
            get { return GetValue(PropertyNames.UnicodeRange); }
            set { SetValue(PropertyNames.UnicodeRange, value); }
        }

        String ICssFontFaceRule.Variant
        {
            get { return GetValue(PropertyNames.FontVariant); }
            set { SetValue(PropertyNames.FontVariant, value); }
        }

        String ICssFontFaceRule.Features
        {
            get { return String.Empty; }
            set { }
        }

        #endregion

        #region Methods

        protected override CssProperty CreateNewProperty(String name)
        {
            return Factory.Properties.CreateFont(name);
        }

        #endregion
    }
}
