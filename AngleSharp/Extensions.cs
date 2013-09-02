using AngleSharp.Css;
using AngleSharp.DOM;
using AngleSharp.DOM.Collections;
using System;
using System.Collections.Generic;

namespace AngleSharp
{
    /// <summary>
    /// A set of useful extension methods when dealing with the DOM.
    /// </summary>
    public static class Extensions
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
            where T : IEnumerable<Element>
        {
            foreach (var element in elements)
                element.SetAttribute(attributeName, attributeValue);

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
            where T : IEnumerable<Element>
        {
            var decls = CssParser.ParseDeclarations(declarations);

            foreach (var element in elements)
            {
                for (int i = 0; i < decls.Length; i++)
			    {
                    var name = decls[i];
                    var value = decls.GetPropertyValue(name);
                    element.Style.SetProperty(name, value);
			    }
            }

            return elements;
        }

        /// <summary>
        /// Sets the inner HTML of the given elements.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="html">The source code of the inner HTML to set.</param>
        /// <returns>The collection itself.</returns>
        public static T Html<T>(this T elements, String html)
            where T : IEnumerable<Element>
        {
            foreach (var element in elements)
                element.InnerHTML = html;

            return elements;
        }

        /// <summary>
        /// Sets the text content of the given elements.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="text">The text that should be set.</param>
        /// <returns>The collection itself.</returns>
        public static T Text<T>(this T elements, String text)
            where T : IEnumerable<Element>
        {
            foreach (var element in elements)
                element.TextContent = text;

            return elements;
        }
    }
}
