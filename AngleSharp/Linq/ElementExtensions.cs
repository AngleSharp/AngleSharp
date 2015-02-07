namespace AngleSharp.Linq
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
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
            if (elements == null)
                throw new ArgumentNullException("elements");

            foreach (var element in elements)
                element.SetAttribute(attributeName, attributeValue);

            return elements;
        }

        /// <summary>
        /// Sets the specified attribute name to the specified value for all
        /// elements in the given collection.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="attributes">An enumeration of attributes in form of key-value pairs.</param>
        /// <returns>The collection itself.</returns>
        public static T Attr<T>(this T elements, IEnumerable<KeyValuePair<String, String>> attributes)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException("elements");

            foreach (var element in elements)
            {
                foreach (var attribute in attributes)
                    element.SetAttribute(attribute.Key, attribute.Value);
            }

            return elements;
        }

        /// <summary>
        /// Sets the specified attribute name to the specified value for all
        /// elements in the given collection.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="attributes">An enumeration of attributes in form of an anonymous object, that carries key-value pairs.</param>
        /// <returns>The collection itself.</returns>
        public static T Attr<T>(this T elements, Object attributes)
            where T : IEnumerable<IElement>
        {
            var realAttributes = attributes.ToDictionary(m => m.ToString());
            return elements.Attr(realAttributes);
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
            if (elements == null)
                throw new ArgumentNullException("elements");

            foreach (var element in elements)
                element.InnerHtml = String.Empty;

            return elements;
        }

        /// <summary>
        /// Extends the CSS of the given elements with the specified declarations.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="propertyName">The name of the property to set.</param>
        /// <param name="propertyValue">The value of the property to set.</param>
        /// <returns>The collection itself.</returns>
        public static T Css<T>(this T elements, String propertyName, String propertyValue)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException("elements");

            foreach (var element in elements.OfType<IHtmlElement>())
                element.Style.SetProperty(propertyName, propertyValue);

            return elements;
        }

        /// <summary>
        /// Extends the CSS of the given elements with the specified declarations.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="properties">An enumeration of properties in form of key-value pairs.</param>
        /// <returns>The collection itself.</returns>
        public static T Css<T>(this T elements, IEnumerable<KeyValuePair<String, String>> properties)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException("elements");

            foreach (var element in elements.OfType<IHtmlElement>())
            {
                foreach (var property in properties)
                    element.Style.SetProperty(property.Key, property.Value);
            }

            return elements;
        }

        /// <summary>
        /// Extends the CSS of the given elements with the specified declarations.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="properties">An enumeration of properties in form of an anonymous object, that carries key-value pairs.</param>
        /// <returns>The collection itself.</returns>
        public static T Css<T>(this T elements, Object properties)
            where T : IEnumerable<IElement>
        {
            var realProperties = properties.ToDictionary(m => m.ToString());
            return elements.Css(realProperties);
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
            if (element == null)
                throw new ArgumentNullException("element");

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
            if (elements == null)
                throw new ArgumentNullException("elements");

            foreach (var element in elements)
                element.InnerHtml = html;

            return elements;
        }

        /// <summary>
        /// Adds the specified class name(s) for all elements in the given collection.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="className">The name(s) of the class(es).</param>
        /// <returns>The collection itself.</returns>
        public static T AddClass<T>(this T elements, String className)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException("elements");

            var classes = className.SplitSpaces();

            foreach (var element in elements)
                element.ClassList.Add(classes);

            return elements;
        }

        /// <summary>
        /// Removes the specified class name(s) for all elements in the given collection.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="className">The name(s) of the class(es).</param>
        /// <returns>The collection itself.</returns>
        public static T RemoveClass<T>(this T elements, String className)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException("elements");

            var classes = className.SplitSpaces();

            foreach (var element in elements)
                element.ClassList.Remove(classes);

            return elements;
        }

        /// <summary>
        /// Toggles the specified class name(s) for all elements in the given collection.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="className">The name(s) of the class(es).</param>
        /// <returns>The collection itself.</returns>
        public static T ToggleClass<T>(this T elements, String className)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException("elements");

            var classes = className.SplitSpaces();

            foreach (var element in elements)
            {
                foreach (var @class in classes)
                    element.ClassList.Toggle(@class);
            }

            return elements;
        }

        /// <summary>
        /// Checks if any element in the given collection has the given class(es).
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="className">The name(s) of the class(es).</param>
        /// <returns>True if any element has the class(es), otherwise false.</returns>
        public static Boolean HasClass<T>(this T elements, String className)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException("elements");

            var found = false;
            var classes = className.SplitSpaces();

            foreach (var element in elements)
            {
                found = true;

                foreach (var @class in classes)
                {
                    if (!element.ClassList.Contains(@class))
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                    break;
            }

            return found;
        }
    }
}
