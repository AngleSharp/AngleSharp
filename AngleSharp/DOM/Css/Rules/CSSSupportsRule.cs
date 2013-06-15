using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents an @supports rule.
    /// </summary>
    public sealed class CSSSupportsRule : CSSConditionRule
    {
        internal CSSSupportsRule()
        {
            _type = CssRule.Supports;
        }
    }
}
