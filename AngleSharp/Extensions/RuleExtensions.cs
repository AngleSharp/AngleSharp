namespace AngleSharp.Extensions
{
    using AngleSharp.DOM.Css;
    using System;

    /// <summary>
    /// A set of useful extension methods for CSS rules.
    /// </summary>
    static class RuleExtensions
    {
        /// <summary>
        /// Converts the list of CSS rules to a CSS block string.
        /// </summary>
        /// <param name="rules">The list of CSS rules.</param>
        /// <returns>The block string representation.</returns>
        public static String ToCssBlock(this CSSRuleList rules)
        {
            return String.Concat("{ ", rules.CssText, rules.Length > 0 ? " }" : "}");
        }

        /// <summary>
        /// Converts the list of CSS declarations to a CSS block string.
        /// </summary>
        /// <param name="style">The list of CSS declarations.</param>
        /// <returns>The block string representation.</returns>
        public static String ToCssBlock(this CSSStyleDeclaration style)
        {
            return String.Concat("{ ", style.CssText, style.Length > 0 ? " }" : "}");
        }
    }
}
