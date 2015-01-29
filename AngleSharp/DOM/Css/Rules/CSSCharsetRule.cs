namespace AngleSharp.Dom.Css
{
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Represents the CSS @charset rule.
    /// </summary>
    sealed class CssCharsetRule : CssRule, ICssCharsetRule
    {
        #region ctor

        internal CssCharsetRule()
            : base(CssRuleType.Charset)
        {
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
            var newRule = rule as CssCharsetRule;
            CharacterSet = newRule.CharacterSet;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        protected override String ToCss()
        {
            return String.Concat("@charset ", CharacterSet.CssString(), ";");
        }

        #endregion
    }
}
