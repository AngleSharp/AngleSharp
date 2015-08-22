namespace AngleSharp.Extensions
{
    using AngleSharp.Css;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Css;
    using AngleSharp.Parser.Css;
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

        /// <summary>
        /// Gets the comments contained in the stylesheet, if any.
        /// </summary>
        /// <param name="sheet">The stylesheet to examine.</param>
        /// <returns>An iterator over all comments.</returns>
        public static IEnumerable<CssComment> GetComments(this ICssStyleSheet sheet)
        {
            var root = sheet as CssNode;

            if (root != null)
                return root.GetComments();

            return Enumerable.Empty<CssComment>();
        }

        static IEnumerable<CssComment> GetComments(this CssNode node)
        {
            var tokens = node.Trivia;
            var comments = Enumerable.Empty<CssComment>();

            if (tokens != null)
            {
                comments = tokens.Where(m => m.Type == CssTokenType.Comment).Select(m => new CssComment(m.Data, m.Position));
            }

            return comments.Concat(node.GetChildren().SelectMany(m => m.GetComments()));
        }
    }
}
