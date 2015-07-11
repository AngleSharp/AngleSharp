namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Parser.Css;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents the @font-face rule.
    /// </summary>
	sealed class CssFontFaceRule : CssRule, ICssFontFaceRule
    {
        #region Fields

        readonly CssProperty[] _declarations;
        readonly CssParserOptions _options;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new @font-face rule.
        /// </summary>
        internal CssFontFaceRule(CssParserOptions options)
            : base(CssRuleType.FontFace)
        {
            _options = options;
            _declarations = new CssProperty[]
            {
                new CssFontFamilyProperty(),
                new CssSrcProperty(),
                new CssFontStyleProperty(),
                new CssFontWeightProperty(),
                new CssFontStretchProperty(),
                new CssUnicodeRangeProperty(),
                new CssFontVariantProperty()
            };
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

        #region Internal methods

        internal void SetProperty(CssProperty property)
        {
            for (int i = 0; i < _declarations.Length; i++)
            {
                if (_declarations[i].Name == property.Name)
                {
                    _declarations[i] = property;
                    break;
                }
            }
        }

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = (CssFontFaceRule)rule;

            for (int i = 0; i < _declarations.Length; i++)
            {
                _declarations[i] = newRule._declarations[i];
            }
        }

        #endregion

        #region String representation

        public override String ToCss(IStyleFormatter formatter)
        {
            var rules = formatter.Block(_declarations.Where(m => m.HasValue));
            return formatter.Rule("@font-face", null, rules);
        }

        #endregion

        #region Helpers

        String GetValue(String propertyName)
        {
            foreach (var declaration in _declarations)
            {
                if (declaration.HasValue && declaration.Name == propertyName)
                    return declaration.Value;
            }

            return String.Empty;
        }

        void SetValue(String propertyName, String valueText)
        {
            foreach (var declaration in _declarations)
            {
                if (declaration.Name == propertyName)
                {
                    var value = CssParser.ParseValue(valueText, _options);
                    declaration.TrySetValue(value);
                    break;
                }
            }
        }

        #endregion
    }
}
