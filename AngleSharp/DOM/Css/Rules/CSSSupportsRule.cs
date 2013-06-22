using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents an @supports rule.
    /// </summary>
    public sealed class CSSSupportsRule : CSSConditionRule
    {
        #region Constants

        internal const String RuleName = "supports";

        #endregion

        #region Members

        String conditionText;

        #endregion

        #region ctor

        internal CSSSupportsRule()
        {
            _type = CssRule.Supports;
            conditionText = String.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the text of the condition of the support rule.
        /// </summary>
        public override String ConditionText
        {
            get { return conditionText; }
            set { conditionText = value; }
        }

        #endregion
    }
}
