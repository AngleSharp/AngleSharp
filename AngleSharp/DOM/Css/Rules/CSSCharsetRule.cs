namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents the CSS @charset rule.
    /// </summary>
    sealed class CSSCharsetRule : CSSRule, ICssCharsetRule
    {
        #region ctor

        internal CSSCharsetRule()
        {
            _type = CssRuleType.Charset;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the encoding information set by this rule.
        /// </summary>
        public String CharacterSet
        {
            get;
            set;
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = rule as CSSCharsetRule;
            CharacterSet = newRule.CharacterSet;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return String.Format("@charset '{0}';", CharacterSet);
        }

        #endregion
    }
}
