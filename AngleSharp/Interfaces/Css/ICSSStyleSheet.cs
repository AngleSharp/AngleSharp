namespace AngleSharp.Dom.Css
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the CSS style sheet for storing CSS styles.
    /// </summary>
    [DomName("CSSStyleSheet")]
    public interface ICssStyleSheet : IStyleSheet
    {
        /// <summary>
        /// Gets the @import rule if the stylesheet was importated otherwise it returns null.
        /// </summary>
        [DomName("ownerRule")]
        ICssRule OwnerRule { get; }

        /// <summary>
        /// Gets a CSSRuleList of the CSS rules in the style sheet.
        /// </summary>
        [DomName("cssRules")]
        ICssRuleList Rules { get; }

        /// <summary>
        /// Inserts a new style rule into the current style sheet.
        /// </summary>
        /// <param name="rule">A string containing the rule to be inserted (selector and declaration).</param>
        /// <param name="index">The index representing the position to be inserted.</param>
        /// <returns>The index of insertation.</returns>
        [DomName("insertRule")]
        Int32 Insert(String rule, Int32 index);

        /// <summary>
        /// Removes a style rule from the current style sheet object.
        /// </summary>
        /// <param name="index">The index representing the position to be removed.</param>
        [DomName("deleteRule")]
        void RemoveAt(Int32 index);

        /// <summary>
        /// Gets a CSS code representation of the stylesheet.
        /// </summary>
        String CssText { get; }
    }
}
