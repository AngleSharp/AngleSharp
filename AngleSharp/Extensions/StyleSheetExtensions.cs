namespace AngleSharp.Extensions
{
    using AngleSharp.Css;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Css;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Defines a set of extension methods for style sheets.
    /// </summary>
    [DebuggerStepThrough]
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
            {
                throw new ArgumentNullException("sheets");
            }

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
            {
                throw new ArgumentNullException("selector");
            }

            var selectorText = selector.Text;
            return sheets.RulesOf<ICssStyleRule>().Where(m => m.SelectorText == selectorText);
        }

        /// <summary>
        /// Gets the associated document of the sheet if any.
        /// </summary>
        /// <param name="sheet">The sheet.</param>
        /// <returns>The associated document, if any.</returns>
        public static IDocument GetDocument(this IStyleSheet sheet)
        {
            return sheet != null && sheet.OwnerNode != null ? sheet.OwnerNode.Owner : null;
        }

        /// <summary>
        /// Gets the comments contained in the sheet, if any.
        /// </summary>
        /// <param name="node">The node to examine.</param>
        /// <returns>An iterator over all comments.</returns>
        public static IEnumerable<ICssComment> GetComments(this ICssNode node)
        {
            return node.GetAll<ICssComment>();
        }

        /// <summary>
        /// Gets all descendents of the provided node.
        /// </summary>
        /// <param name="node">The node to examine.</param>
        /// <returns>An iterator over all contained nodes.</returns>
        public static IEnumerable<ICssNode> GetAllDescendents(this ICssNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            return node.Children.SelectMany(m => m.GetAllDescendents());
        }

        /// <summary>
        /// Gets all descendents of the provided node.
        /// </summary>
        /// <param name="node">The node to examine.</param>
        /// <returns>An iterator over all contained nodes.</returns>
        public static IEnumerable<T> GetAll<T>(this ICssNode node)
            where T : IStyleFormattable
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            if (node is T)
            {
                yield return (T)node;
            }

            foreach (var entity in node.Children.SelectMany(m => m.GetAll<T>()))
            {
                yield return entity;
            }
        }
    }
}
