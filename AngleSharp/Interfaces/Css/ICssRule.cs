namespace AngleSharp.Dom.Css
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the base interface for a CSS rule.
    /// </summary>
    [DomName("CSSRule")]
    public interface ICssRule
    {
        /// <summary>
        /// Gets the type constant indicating the type of CSS rule.
        /// </summary>
        [DomName("type")]
        CssRuleType Type { get; }

        /// <summary>
        /// Gets the textual representation of the rule.
        /// </summary>
        [DomName("cssText")]
        String CssText { get; set; }

        /// <summary>
        /// Gets the containing (parent) rule, if any.
        /// </summary>
        [DomName("parentRule")]
        ICssRule Parent { get; }

        /// <summary>
        /// Gets the CSSStyleSheet object that owns this rule, if any.
        /// </summary>
        [DomName("parentStyleSheet")]
        ICssStyleSheet Owner { get; }
    }
}
