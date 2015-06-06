namespace AngleSharp.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using AngleSharp.Dom.Css;

    /// <summary>
    /// A set of useful extension methods for CSS rules.
    /// </summary>
    [DebuggerStepThrough]
    static class RuleExtensions
    {
        /// <summary>
        /// Converts the list of CSS rules to a CSS block string.
        /// </summary>
        /// <param name="rules">The list of CSS rules.</param>
        /// <returns>The block string representation.</returns>
        public static String ToCssBlock(this CssRuleList rules)
        {
            return String.Concat("{ ", rules.CssText, rules.Length > 0 ? " }" : "}");
        }

        /// <summary>
        /// Converts the list of CSS declarations to a CSS block string.
        /// </summary>
        /// <param name="style">The list of CSS declarations.</param>
        /// <returns>The block string representation.</returns>
        public static String ToCssBlock(this CssStyleDeclaration style)
        {
            return String.Concat("{ ", style.CssText, style.Length > 0 ? " }" : "}");
        }

        /// <summary>
        /// Converts the enumeration of CSS declarations to a CSS block string.
        /// </summary>
        /// <param name="declarations">The enumeration of declarations.</param>
        /// <returns>The block string representation.</returns>
        public static String ToCssBlock(this IEnumerable<CssProperty> declarations)
        {
            var list = new List<String>();

            foreach (var declaration in declarations)
            {
                if (declaration.HasValue)
                    list.Add(declaration.CssText);
            }

            var content = String.Join(" ", list.ToArray());
            return String.Concat("{ ", content, list.Count > 0 ? " }" : "}");
        }
    }
}
