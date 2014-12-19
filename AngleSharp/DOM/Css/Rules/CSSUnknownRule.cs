namespace AngleSharp.DOM.Css
{
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Represents an unknown CSS rule.
    /// </summary>
    sealed class CSSUnknownRule : CSSGroupingRule
    {
        #region Fields

        String _keyText;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new unknown rule.
        /// </summary>
        public CSSUnknownRule()
            : base(CssRuleType.Unknown)
        {
            _keyText = String.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the key text of the unknown rule.
        /// </summary>
        public String KeyText
        {
            get { return _keyText; }
            set { _keyText = value; }
        }

        #endregion

        #region Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = rule as CSSUnknownRule;
            _keyText = newRule._keyText;
            base.ReplaceWith(rule);
        }

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        protected override String ToCss()
        {
            return String.Concat(_keyText, " ", Rules.ToCssBlock());
        }

        #endregion
    }
}
