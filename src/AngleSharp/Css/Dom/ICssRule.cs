namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the base interface for a CSS rule.
    /// </summary>
    [DomName("CSSRule")]
    public interface ICssRule : IStyleFormattable
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

        /// <summary>
        /// Sets the parent rule. This implies using the
        /// owner from the given rule.
        /// </summary>
        /// <param name="rule">The rule to use as parent.</param>
        void SetParent(ICssRule rule);

        /// <summary>
        /// Sets the owning sheet. This implies using no parent.
        /// </summary>
        /// <param name="sheet">The owning sheet.</param>
        void SetOwner(ICssStyleSheet sheet);
    }
}
