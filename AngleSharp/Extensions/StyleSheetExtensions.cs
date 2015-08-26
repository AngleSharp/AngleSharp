namespace AngleSharp.Extensions
{
    using AngleSharp.Css;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Css;
    using AngleSharp.Parser.Css;
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
        /// Gets the comments contained in the CSS node, if any.
        /// </summary>
        /// <param name="node">The node to examine.</param>
        /// <returns>An iterator over all comments.</returns>
        public static IEnumerable<CssComment> GetComments(this CssNode node)
        {
            var tokens = node.Tokens;
            var comments = tokens.Where(m => m.Type == CssTokenType.Comment).Select(m => new CssComment(m));
            return comments.Concat(node.Children.SelectMany(m => m.GetComments()));
        }

        /// <summary>
        /// Gets all descendents of the provided node.
        /// </summary>
        /// <param name="node">The node to examine.</param>
        /// <returns>An iterator over all contained nodes.</returns>
        public static IEnumerable<CssNode> GetAllDescendents(this CssNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            return node.Children.SelectMany(m => m.GetAllDescendents());
        }

        /// <summary>
        /// Gets all descendents of the provided node.
        /// </summary>
        /// <param name="node">The node to examine.</param>
        /// <returns>An iterator over all contained nodes.</returns>
        public static IEnumerable<T> GetAll<T>(this CssNode node)
            where T : IStyleFormattable
        {
            if (node == null)
                throw new ArgumentNullException("node");

            if (node.Entity is T)
                yield return (T)node.Entity;

            foreach (var entity in node.Children.SelectMany(m => m.GetAll<T>()))
                yield return entity;
        }

        /// <summary>
        /// Gets the associated for the provided entity, if any.
        /// </summary>
        /// <param name="node">The node to start the retrieval.</param>
        /// <param name="entity">The entity to look for.</param>
        /// <returns>The associated node, or null if there is none..</returns>
        public static CssNode GetAssociatedNode(this CssNode node, IStyleFormattable entity)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            if (entity == null)
                throw new ArgumentNullException("entity");

            var target = default(CssNode);

            if (Object.ReferenceEquals(node.Entity, entity))
            {
                return node;
            }

            foreach (var child in node.Children)
            {
                target = child.GetAssociatedNode(entity);

                if (target != null)
                    break;
            }

            return target;
        }

        /// <summary>
        /// Gets the original source code of the CSS node.
        /// </summary>
        /// <param name="node">The node to examine.</param>
        /// <returns>The original text representation.</returns>
        public static String GetSource(this CssNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            var tokens = node.Tokens;
            var childs = node.Children;
            var total = tokens.Count + childs.Count;
            var str = new String[total];
            var pos = new TextPosition[total];

            for (int i = 0; i < tokens.Count; i++)
            {
                str[i] = tokens[i].ToValue();
                pos[i] = tokens[i].Position;
            }

            for (int i = tokens.Count, k = 0; i < total; i++, k++)
            {
                str[i] = childs[k].GetSource();
                pos[i] = childs[k].Start;

                for (int j = i - 1; j >= 0; j--)
                {
                    if (pos[j] < pos[j + 1])
                        break;

                    Swap(pos, j);
                    Swap(str, j);
                }
            }

            return String.Join(String.Empty, str);
        }

        static void Swap<T>(T[] array, Int32 i)
        {
            var j = i + 1;
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
