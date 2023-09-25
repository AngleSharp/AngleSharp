namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A set of extension methods for selectors.
    /// </summary>
    public static class SelectorExtensions
    {
        /// <summary>
        /// Returns the first element within the given elements (using depth-first
        /// pre-order traversal) that match the selectors with the given scope.
        /// </summary>
        /// <param name="selector">A selector object.</param>
        /// <param name="elements">The elements to take as source.</param>
        /// <param name="scope">The element to take as scope.</param>
        /// <returns>The resulting element or null.</returns>
        public static IElement? MatchAny(this ISelector selector, IEnumerable<IElement> elements, IElement? scope)
        {
            var stack = new Stack<INode>();
            foreach (var element in elements)
            {
                stack.Clear();
                var nodes = element.GetDescendantsAndSelf(
                    stack,
                    filter: static (node, state) => node is IElement e && state.Selector.Match(e, state.Scope),
                    state: new SelectorState(selector, scope));

                var enumerator = nodes.GetEnumerator();
                if (enumerator.MoveNext())
                {
                    return (IElement?) enumerator.Current;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the elements within the given elements (using depth-first
        /// pre-order traversal) that match the selectors with the given scope.
        /// </summary>
        /// <param name="selector">A selector object.</param>
        /// <param name="elements">The elements to take as source.</param>
        /// <param name="scope">The element to take as scope.</param>
        /// <returns>The collection containing the resulting elements.</returns>
        public static IHtmlCollection<IElement> MatchAll(this ISelector selector, IEnumerable<IElement> elements, IElement? scope)
        {
            var result = new List<IElement>();
            selector.MatchAll(elements, scope, result);
            return new HtmlCollection<IElement>(result);
        }

        /// <summary>
        /// Provides an alternate to <see cref="ISelector.Match(IElement, IElement)" /> that sets the
        /// scope to the owning document element (if there is one).
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="element">The element to match against.</param>
        /// <returns>The result of the match.</returns>
        public static Boolean Match(this ISelector selector, IElement element) => selector.Match(element, element?.Owner!.DocumentElement);

        private static void MatchAll(this ISelector selector, IEnumerable<IElement> elements, IElement? scope, List<IElement> result)
        {
            var stack = new Stack<INode>();
            foreach (var element in elements)
            {
                stack.Clear();
                var nodes = element.GetDescendantsAndSelf(
                    stack,
                    filter: static (node, state) => node is IElement e && state.Selector.Match(e, state.Scope),
                    state: new SelectorState(selector, scope));

                foreach (var descendantAndSelf in nodes)
                {
                    result.Add((IElement) descendantAndSelf);
                }
            }
        }

        private readonly struct SelectorState
        {
            public readonly ISelector Selector;
            public readonly IElement? Scope;

            public SelectorState(ISelector selector, IElement? scope)
            {
                Selector = selector;
                Scope = scope;
            }
        }
    }
}
