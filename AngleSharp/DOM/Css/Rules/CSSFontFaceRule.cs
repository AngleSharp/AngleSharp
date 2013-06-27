using AngleSharp.DOM.Collections;
using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the @font-face rule.
    /// </summary>
    [DOM("CSSFontFaceRule")]
    public sealed class CSSFontFaceRule : CSSRule
    {
        #region Constants

        internal const String RuleName = "font-face";

        #endregion

        #region Members

        CSSStyleDeclaration _cssRules;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new @font-face rule.
        /// </summary>
        internal CSSFontFaceRule()
        {
            _cssRules = new CSSStyleDeclaration();
            _type = CssRule.FontFace;
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Appends the given rule to the list of rules.
        /// </summary>
        /// <param name="rule">The rule to append.</param>
        /// <returns>The current font-face rule.</returns>
        internal CSSFontFaceRule AppendRule(CSSProperty rule)
        {
            _cssRules.List.Add(rule);
            return this;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the declared CSS rules.
        /// </summary>
        [DOM("cssRules")]
        public CSSStyleDeclaration CssRules
        {
            get { return _cssRules; }
        }

        /// <summary>
        /// Gets or sets the font-family.
        /// </summary>
        [DOM("family")]
        public String Family
        {
            get { return _cssRules.GetPropertyValue("font-family"); }
            set { _cssRules.SetProperty("font-family", value); }
        }

        /// <summary>
        /// Gets or sets the source of the font.
        /// </summary>
        [DOM("src")]
        public String Src
        {
            get { return _cssRules.GetPropertyValue("src"); }
            set { _cssRules.SetProperty("src", value); }
        }

        /// <summary>
        /// Gets or sets the style of the font.
        /// </summary>
        [DOM("style")]
        public String Style
        {
            get { return _cssRules.GetPropertyValue("font-style"); }
            set { _cssRules.SetProperty("font-style", value); }
        }

        /// <summary>
        /// Gets or sets the weight of the font.
        /// </summary>
        [DOM("weight")]
        public String Weight
        {
            get { return _cssRules.GetPropertyValue("font-weight"); }
            set { _cssRules.SetProperty("font-weight", value); }
        }

        /// <summary>
        /// Gets or sets the stretch value of the font.
        /// </summary>
        [DOM("stretch")]
        public String Stretch
        {
            get { return _cssRules.GetPropertyValue("stretch"); }
            set { _cssRules.SetProperty("stretch", value); }
        }

        /// <summary>
        /// Gets or sets the unicode range of the font.
        /// </summary>
        [DOM("unicodeRange")]
        public String UnicodeRange
        {
            get { return _cssRules.GetPropertyValue("unicode-range"); }
            set { _cssRules.SetProperty("unicode-range", value); }
        }

        /// <summary>
        /// Gets or sets the variant of the font.
        /// </summary>
        [DOM("variant")]
        public String Variant
        {
            get { return _cssRules.GetPropertyValue("font-variant"); }
            set { _cssRules.SetProperty("font-variant", value); }
        }

        /// <summary>
        /// Gets or sets the feature settings of the font.
        /// </summary>
        [DOM("featureSettings")]
        public String FeatureSettings
        {
            get { return _cssRules.GetPropertyValue("font-feature-settings"); }
            set { _cssRules.SetProperty("font-feature-settings", value); }
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return "@font-face {" + Environment.NewLine + _cssRules.ToCss() + "}";
        }

        #endregion
    }
}
