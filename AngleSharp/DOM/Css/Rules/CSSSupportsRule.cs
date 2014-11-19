namespace AngleSharp.DOM.Css
{
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Represents an @supports rule.
    /// </summary>
    sealed class CSSSupportsRule : CSSConditionRule, ICssSupportsRule
    {
        #region Fields

        String _condition;
        Boolean _used;

        #endregion

        #region ctor

        internal CSSSupportsRule()
        {
            _type = CssRuleType.Supports;
            _condition = String.Empty;
            _used = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the text of the condition of the support rule.
        /// </summary>
        public String ConditionText
        {
            get { return _condition; }
            set { _condition = value; }
        }

        /// <summary>
        /// Gets if the rule is used.
        /// </summary>
        public Boolean IsSupported
        {
            get { return _used; }
            set { _used = value; }
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            base.ReplaceWith(rule);
            var newRule = rule as CSSSupportsRule;
            _condition = newRule._condition;
            _used = newRule._used;
        }

        internal override Boolean IsValid(IWindow window)
        {
            //TODO
            return true;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return String.Concat("@supports ", _condition, " ", Rules.ToCssBlock());
        }

        #endregion
    }
}
