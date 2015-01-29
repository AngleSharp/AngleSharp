namespace AngleSharp.Dom.Css
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a list of CSS rules.
    /// </summary>
    [DomName("CSSRuleList")]
    public interface ICssRuleList : IEnumerable<ICssRule>
    {
        /// <summary>
        /// Gets a CSS rule at the specified index.
        /// </summary>
        /// <param name="index">The 0-based index of the rule.</param>
        /// <returns>The CSS rule at the given index, if any.</returns>
        [DomName("item")]
        ICssRule this[Int32 index] { get; }

        /// <summary>
        /// Gets the number of elements in the list of rules.
        /// </summary>
        [DomName("length")]
        Int32 Length { get; }

        /// <summary>
        /// Gets the text representation of the rule list.
        /// </summary>
        String CssText { get; }
    }
}
