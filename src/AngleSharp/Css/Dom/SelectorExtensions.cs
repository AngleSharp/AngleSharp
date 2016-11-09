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
        public static IElement MatchAny(this ISelector selector, IEnumerable<IElement> elements, IElement scope)
        {
            foreach (var element in elements)
            {
                if (selector.Match(element, scope))
                {
                    return element;
                }

                if (element.HasChildNodes)
                {
                    var child = selector.MatchAny(element.Children, scope);

                    if (child != null)
                    {
                        return child;
                    }
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
        public static IHtmlCollection<IElement> MatchAll(this ISelector selector, IEnumerable<IElement> elements, IElement scope)
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
        public static Boolean Match(this ISelector selector, IElement element)
        {
            return selector.Match(element, element?.Owner.DocumentElement);
        }

        private static void MatchAll(this ISelector selector, IEnumerable<IElement> elements, IElement scope, List<IElement> result)
        {
            foreach (var element in elements)
            {
                if (selector.Match(element, scope))
                {
                    result.Add(element);
                }

                if (element.HasChildNodes)
                {
                    selector.MatchAll(element.Children, scope, result);
                }
            }
        }
    }
}
