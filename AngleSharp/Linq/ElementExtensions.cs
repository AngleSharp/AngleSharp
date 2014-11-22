namespace AngleSharp.Linq
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Html;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines a set of extension methods for elements.
    /// </summary>
    public static class ElementExtensions
    {
        /// <summary>
        /// Sets the specified attribute name to the specified value for all
        /// elements in the given collection.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <param name="attributeValue">The value of the attribute.</param>
        /// <returns>The collection itself.</returns>
        public static T Attr<T>(this T elements, String attributeName, String attributeValue)
            where T : IEnumerable<IElement>
        {
            foreach (var element in elements)
                element.SetAttribute(attributeName, attributeValue);

            return elements;
        }

        /// <summary>
        /// Empties all provided elements.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <returns>The collection itself.</returns>
        public static T Empty<T>(this T elements)
            where T : IEnumerable<IElement>
        {
            foreach (var element in elements)
                element.InnerHtml = String.Empty;

            return elements;
        }

        /// <summary>
        /// Extends the CSS of the given elements with the specified declarations.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="declarations">The declarations to apply in the inline CSS.</param>
        /// <returns>The collection itself.</returns>
        public static T Css<T>(this T elements, String declarations)
            where T : IEnumerable<IElement>
        {
            var decls = CssParser.ParseDeclarations(declarations);

            foreach (var element in elements.OfType<IHtmlElement>())
            {
                foreach (var decl in decls)
                    element.Style.SetProperty(decl.Name, decl.Value.CssText);
            }

            return elements;
        }

        /// <summary>
        /// Gets the inner HTML of the given element.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="element">The element.</param>
        /// <returns>The source code of the inner HTML.</returns>
        public static String Html<T>(this T element)
            where T : IElement
        {
            return element.InnerHtml;
        }

        /// <summary>
        /// Sets the inner HTML of the given elements.
        /// </summary>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="html">The source code of the inner HTML to set.</param>
        /// <returns>The collection itself.</returns>
        public static T Html<T>(this T elements, String html)
            where T : IEnumerable<IElement>
        {
            foreach (var element in elements)
                element.InnerHtml = html;

            return elements;
        }
    }
}
