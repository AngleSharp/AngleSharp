using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents an @supports rule.
    /// </summary>
    [DOM("CSSSupportsRule")]
    public sealed class CSSSupportsRule : CSSConditionRule
    {
        #region Constants

        internal const String RuleName = "supports";

        #endregion

        #region Members

        String _conditionText;

        #endregion

        #region ctor

        internal CSSSupportsRule()
        {
            _type = CssRule.Supports;
            _conditionText = String.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the text of the condition of the support rule.
        /// </summary>
        [DOM("conditionText")]
        public override String ConditionText
        {
            get { return _conditionText; }
            set { _conditionText = value; }
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return String.Format("@supports {0} {{{1}{2}}}", _conditionText, Environment.NewLine, CssRules.ToCss());
        }

        #endregion
    }
}
