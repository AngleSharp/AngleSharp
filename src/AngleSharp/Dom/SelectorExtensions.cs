namespace AngleSharp.Dom
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines a set of extension methods involving selectors.
    /// </summary>
    public static class SelectorExtensions
    {
        #region General Methods
        
        /// <summary>
        /// Reduces the elements to the one at the given index, if any.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="index">The index of the element.</param>
        /// <returns>The element, or its default value.</returns>
        public static T Eq<T>(this IEnumerable<T> elements, Int32 index)
            where T : IElement
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            return elements.Skip(index).FirstOrDefault();
        }

        /// <summary>
        /// Reduces the elements to the ones above the given index.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="index">The minimum exclusive index.</param>
        /// <returns>The remaining elements.</returns>
        public static IEnumerable<T> Gt<T>(this IEnumerable<T> elements, Int32 index)
            where T : IElement
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            return elements.Skip(index + 1);
        }

        /// <summary>
        /// Reduces the elements to the ones below the given index.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="index">The maximum exclusive index.</param>
        /// <returns>The remaining elements.</returns>
        public static IEnumerable<T> Lt<T>(this IEnumerable<T> elements, Int32 index)
            where T : IElement
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            return elements.Take(index);
        }

        /// <summary>
        /// Reduces the elements to the ones with even index.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <returns>The remaining elements.</returns>
        public static IEnumerable<T> Even<T>(this IEnumerable<T> elements)
            where T : IElement
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            var even = true;

            foreach (var element in elements)
            {
                if (even)
                {
                    yield return element;
                }

                even = !even;
            }
        }

        /// <summary>
        /// Reduces the elements to the ones with odd index.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <returns>The remaining elements.</returns>
        public static IEnumerable<T> Odd<T>(this IEnumerable<T> elements)
            where T : IElement
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            var odd = false;

            foreach (var element in elements)
            {
                if (odd)
                {
                    yield return element;
                }

                odd = !odd;
            }
        }

        #endregion

        #region Methods for Text Selectors

        /// <summary>
        /// Keeps elements that are matched by the provided selector.
        /// </summary>
        /// <param name="elements">The elements to be filtered.</param>
        /// <param name="selectorText">The CSS selector to use.</param>
        /// <returns>The filtered list of elements.</returns>
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> elements, String selectorText)
            where T : IElement
        {
            return elements.Filter(selectorText, true);
        }

        /// <summary>
        /// Keeps elements that are not matched by the provided selector.
        /// </summary>
        /// <param name="elements">The elements to be filtered.</param>
        /// <param name="selectorText">The CSS selector to use.</param>
        /// <returns>The filtered list of elements.</returns>
        public static IEnumerable<T> Not<T>(this IEnumerable<T> elements, String selectorText)
            where T : IElement
        {
            return elements.Filter(selectorText, false);
        }

        /// <summary>
        /// Gets the children of the provided elements. Optionally uses a CSS
        /// selector to filter the results.
        /// </summary>
        /// <param name="elements">The elements owning the children.</param>
        /// <param name="selectorText">The CSS selector to use, if any.</param>
        /// <returns>A filtered list containing the children.</returns>
        public static IEnumerable<IElement> Children(this IEnumerable<IElement> elements, String selectorText = null)
        {
            return elements.GetMany(m => m.Children, selectorText);
        }

        /// <summary>
        /// Gets the siblings of the provided elements. Optionally uses a CSS
        /// selector to filter the results.
        /// </summary>
        /// <param name="elements">The elements with siblings.</param>
        /// <param name="selectorText">The CSS selector to use, if any.</param>
        /// <returns>A filtered list containing the siblings.</returns>
        public static IEnumerable<IElement> Siblings(this IEnumerable<IElement> elements, String selectorText = null)
        {
            return elements.GetMany(m => m.Parent.ChildNodes.OfType<IElement>().Except(m), selectorText);
        }

        /// <summary>
        /// Gets the parents of the provided elements. Optionally uses a CSS
        /// selector to filter the results.
        /// </summary>
        /// <param name="elements">The elements with parents.</param>
        /// <param name="selectorText">The CSS selector to use, if any.</param>
        /// <returns>A filtered list containing the parents.</returns>
        public static IEnumerable<IElement> Parent(this IEnumerable<IElement> elements, String selectorText = null)
        {
            return elements.Get(m => m.ParentElement, selectorText);
        }

        /// <summary>
        /// Gets the following siblings of the provided elements. Optionally
        /// uses a CSS selector to filter the results.
        /// </summary>
        /// <param name="elements">The elements with siblings.</param>
        /// <param name="selectorText">The CSS selector to use, if any.</param>
        /// <returns>A filtered list containing the next siblings.</returns>
        public static IEnumerable<IElement> Next(this IEnumerable<IElement> elements, String selectorText = null)
        {
            return elements.Get(m => m.NextElementSibling, selectorText);
        }

        /// <summary>
        /// Gets the preceding siblings of the provided elements. Optionally
        /// uses a CSS selector to filter the results.
        /// </summary>
        /// <param name="elements">The elements with siblings.</param>
        /// <param name="selectorText">The CSS selector to use, if any.</param>
        /// <returns>A filtered list containing the previous siblings.</returns>
        public static IEnumerable<IElement> Previous(this IEnumerable<IElement> elements, String selectorText = null)
        {
            return elements.Get(m => m.PreviousElementSibling, selectorText);
        }

        #endregion

        #region Methods for Object Selectors

        /// <summary>
        /// Keeps elements that are matched by the provided selector.
        /// </summary>
        /// <param name="elements">The elements to be filtered.</param>
        /// <param name="selector">The CSS selector to use.</param>
        /// <returns>The filtered list of elements.</returns>
        public static IEnumerable<T> Is<T>(this IEnumerable<T> elements, ISelector selector)
            where T : IElement
        {
            return elements.Filter(selector, true);
        }

        /// <summary>
        /// Keeps elements that are not matched by the provided selector.
        /// </summary>
        /// <param name="elements">The elements to be filtered.</param>
        /// <param name="selector">The CSS selector to use.</param>
        /// <returns>The filtered list of elements.</returns>
        public static IEnumerable<T> Not<T>(this IEnumerable<T> elements, ISelector selector)
            where T : IElement
        {
            return elements.Filter(selector, false);
        }

        /// <summary>
        /// Gets the children of the provided elements. Optionally uses a CSS
        /// selector to filter the results.
        /// </summary>
        /// <param name="elements">The elements owning the children.</param>
        /// <param name="selector">The CSS selector to use, if any.</param>
        /// <returns>A filtered list containing the children.</returns>
        public static IEnumerable<IElement> Children(this IEnumerable<IElement> elements, ISelector selector = null)
        {
            return elements.GetMany(m => m.Children, selector);
        }

        /// <summary>
        /// Gets the siblings of the provided elements. Optionally uses a CSS
        /// selector to filter the results.
        /// </summary>
        /// <param name="elements">The elements with siblings.</param>
        /// <param name="selector">The CSS selector to use, if any.</param>
        /// <returns>A filtered list containing the siblings.</returns>
        public static IEnumerable<IElement> Siblings(this IEnumerable<IElement> elements, ISelector selector = null)
        {
            return elements.GetMany(m => m.Parent.ChildNodes.OfType<IElement>().Except(m), selector);
        }

        /// <summary>
        /// Gets the parents of the provided elements. Optionally uses a CSS
        /// selector to filter the results.
        /// </summary>
        /// <param name="elements">The elements with parents.</param>
        /// <param name="selector">The CSS selector to use, if any.</param>
        /// <returns>A filtered list containing the parents.</returns>
        public static IEnumerable<IElement> Parent(this IEnumerable<IElement> elements, ISelector selector = null)
        {
            return elements.Get(m => m.ParentElement, selector);
        }

        /// <summary>
        /// Gets the following siblings of the provided elements. Optionally
        /// uses a CSS selector to filter the results.
        /// </summary>
        /// <param name="elements">The elements with siblings.</param>
        /// <param name="selector">The CSS selector to use, if any.</param>
        /// <returns>A filtered list containing the next siblings.</returns>
        public static IEnumerable<IElement> Next(this IEnumerable<IElement> elements, ISelector selector = null)
        {
            return elements.Get(m => m.NextElementSibling, selector);
        }

        /// <summary>
        /// Gets the preceding siblings of the provided elements. Optionally
        /// uses a CSS selector to filter the results.
        /// </summary>
        /// <param name="elements">The elements with siblings.</param>
        /// <param name="selector">The CSS selector to use, if any.</param>
        /// <returns>A filtered list containing the previous siblings.</returns>
        public static IEnumerable<IElement> Previous(this IEnumerable<IElement> elements, ISelector selector = null)
        {
            return elements.Get(m => m.PreviousElementSibling, selector);
        }

        #endregion

        #region Helpers

        private static IEnumerable<IElement> GetMany(this IEnumerable<IElement> elements, Func<IElement, IEnumerable<IElement>> getter, ISelector selector)
        {
            if (selector == null)
            {
                selector = AllSelector.Instance;
            }

            foreach (var element in elements)
            {
                var children = getter(element);

                foreach (var child in children)
                {
                    if (selector.Match(child))
                    {
                        yield return child;
                    }
                }
            }
        }

        private static IEnumerable<IElement> GetMany(this IEnumerable<IElement> elements, Func<IElement, IEnumerable<IElement>> getter, String selectorText)
        {
            var selector = CreateSelector(elements, selectorText);
            return elements.GetMany(getter, selector);
        }

        private static IEnumerable<IElement> Get(this IEnumerable<IElement> elements, Func<IElement, IElement> getter, ISelector selector)
        {
            if (selector == null)
            {
                selector = AllSelector.Instance;
            }

            foreach (var element in elements)
            {
                var child = getter(element);

                while (child != null)
                {
                    if (selector.Match(child))
                    {
                        yield return child;
                        break;
                    }

                    child = getter(child);
                }
            }
        }

        private static IEnumerable<IElement> Get(this IEnumerable<IElement> elements, Func<IElement, IElement> getter, String selectorText)
        {
            var selector = CreateSelector(elements, selectorText);
            return elements.Get(getter, selector);
        }

        private static IEnumerable<IElement> Except(this IEnumerable<IElement> elements, IElement excluded)
        {
            foreach (var element in elements)
            {
                if (!Object.ReferenceEquals(element, excluded))
                {
                    yield return element;
                }
            }
        }

        private static IEnumerable<T> Filter<T>(this IEnumerable<T> elements, ISelector selector, Boolean result)
            where T : IElement
        {
            if (selector == null)
            {
                selector = AllSelector.Instance;
            }

            foreach (var element in elements)
            {
                if (selector.Match(element) == result)
                {
                    yield return element;
                }
            }
        }

        private static IEnumerable<T> Filter<T>(this IEnumerable<T> elements, String selectorText, Boolean result)
            where T : IElement
        {
            var selector = CreateSelector(elements, selectorText);
            return elements.Filter(selector, result);
        }

        private static ISelector CreateSelector<T>(IEnumerable<T> elements, String selector)
            where T : IElement
        {
            if (selector != null)
            {
                var element = elements.FirstOrDefault();

                if (element != null)
                {
                    var parser = element.Owner.Context.GetService<ICssSelectorParser>();
                    return parser.ParseSelector(selector);
                }
            }

            return AllSelector.Instance;
        }

        #endregion
    }
}
