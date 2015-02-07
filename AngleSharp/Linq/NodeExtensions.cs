namespace AngleSharp.Linq
{
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines a set of extension methods for nodes.
    /// </summary>
    public static class NodeExtensions
    {
        /// <summary>
        /// Gets the content text of the given DOM element.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="element">The element to stringify.</param>
        /// <returns>The text of the element and its children.</returns>
        public static String Text<T>(this T element)
            where T : INode
        {
            if (element == null)
                throw new ArgumentNullException("element");

            return element.TextContent;
        }

        /// <summary>
        /// Sets the text content of the given elements.
        /// </summary>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="text">The text that should be set.</param>
        /// <returns>The collection itself.</returns>
        public static T Text<T>(this T elements, String text)
            where T : IEnumerable<INode>
        {
            if (elements == null)
                throw new ArgumentNullException("elements");

            foreach (var element in elements)
                element.TextContent = text;

            return elements;
        }

        /// <summary>
        /// Gets the index of the given item in the list of elements.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="elements">The source list of elements.</param>
        /// <param name="item">The item to search for.</param>
        /// <returns>The index of the item or -1 if not found.</returns>
        public static Int32 Index<T>(this IEnumerable<T> elements, T item)
            where T : INode
        {
            if (elements == null)
                throw new ArgumentNullException("elements");

            if (item != null)
            {
                int i = 0;

                foreach (var element in elements)
                {
                    if (Object.ReferenceEquals(element, item))
                        return i;

                    i++;
                }
            }

            return -1;
        }
    }
}
