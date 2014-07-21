namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a list of CSS rules.
    /// </summary>
    [DomName("CSSRuleList")]
    interface ICssRuleList : IEnumerable<CSSRule>
    {
        /// <summary>
        /// Gets a CSS rule at the specified index.
        /// </summary>
        /// <param name="index">The 0-based index of the rule.</param>
        /// <returns>The CSS rule at the given index, if any.</returns>
        [DomName("item")]
        CSSRule this[Int32 index] { get; }

        /// <summary>
        /// Gets the number of elements in the list of rules.
        /// </summary>
        [DomName("length")]
        Int32 Length { get; }
    }
}
