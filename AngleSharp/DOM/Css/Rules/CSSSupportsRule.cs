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

        internal CSSSupportsRule()
        {
            _type = CssRule.Supports;
        }
    }
}
