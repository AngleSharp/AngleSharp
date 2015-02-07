namespace AngleSharp.Linq
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines a set of extension methods for style sheets.
    /// </summary>
    public static class StyleSheetExtensions
    {
        /// <summary>
        /// Gets all rules that are of the provided type.
        /// </summary>
        /// <typeparam name="TRule">The type of rules to get.</typeparam>
        /// <param name="sheets">The list of stylesheets to consider.</param>
        /// <returns>The list of rules.</returns>
        public static IEnumerable<TRule> RulesOf<TRule>(this IEnumerable<IStyleSheet> sheets)
            where TRule : ICssRule
        {
            if (sheets == null)
                throw new ArgumentNullException("sheets");

            return sheets.Where(m => !m.IsDisabled).OfType<ICssStyleSheet>().SelectMany(m => m.Rules).OfType<TRule>();
        }

        /// <summary>
        /// Gets all style rules that have the same selector text.
        /// </summary>
        /// <param name="sheets">The list of stylesheets to consider.</param>
        /// <param name="selector">The selector to compare to.</param>
        /// <returns>The list of style rules.</returns>
        public static IEnumerable<ICssStyleRule> StylesWith(this IEnumerable<IStyleSheet> sheets, ISelector selector)
        {
            if (selector == null)
                throw new ArgumentNullException("selector");

            var selectorText = selector.Text;
            return sheets.RulesOf<ICssStyleRule>().Where(m => m.SelectorText == selectorText);
        }
    }
}
