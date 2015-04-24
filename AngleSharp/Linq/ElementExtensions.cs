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
        #region Methods

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

            if (attributeName == null)
                throw new ArgumentNullException("attributeName");

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
        /// <param name="attributes">
        /// An enumeration of attributes in form of key-value pairs.
        /// </param>
        /// <returns>The collection itself.</returns>
        public static T Attr<T>(this T elements, IEnumerable<KeyValuePair<String, String>> attributes)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException("elements");

            if (attributes == null)
                throw new ArgumentNullException("attributes");

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
        /// <param name="attributes">
        /// An enumeration of attributes in form of an anonymous object, that
        /// carries key-value pairs.
        /// </param>
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
        /// Extends the CSS of the given elements with the specified 
        /// declarations.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="propertyName">The name of the property to set.</param>
        /// <param name="propertyValue">
        /// The value of the property to set.
        /// </param>
        /// <returns>The collection itself.</returns>
        public static T Css<T>(this T elements, String propertyName, String propertyValue)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException("elements");

            if (propertyName == null)
                throw new ArgumentNullException("propertyName");

            foreach (var element in elements.OfType<IHtmlElement>())
                element.Style.SetProperty(propertyName, propertyValue);

            return elements;
        }

        /// <summary>
        /// Extends the CSS of the given elements with the specified 
        /// declarations.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="properties">
        /// An enumeration of properties in form of key-value pairs.
        /// </param>
        /// <returns>The collection itself.</returns>
        public static T Css<T>(this T elements, IEnumerable<KeyValuePair<String, String>> properties)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException("elements");

            if (properties == null)
                throw new ArgumentNullException("properties");

            foreach (var element in elements.OfType<IHtmlElement>())
            {
                foreach (var property in properties)
                    element.Style.SetProperty(property.Key, property.Value);
            }

            return elements;
        }

        /// <summary>
        /// Extends the CSS of the given elements with the specified
        /// declarations.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="properties">
        /// An enumeration of properties in form of an anonymous object, that
        /// carries key-value pairs.
        /// </param>
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
        /// <param name="html">
        /// The source code of the inner HTML to set.
        /// </param>
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
        /// Adds the specified class name(s) for all elements in the given
        /// collection.
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

            if (className == null)
                throw new ArgumentNullException("className");

            var classes = className.SplitSpaces();

            foreach (var element in elements)
                element.ClassList.Add(classes);

            return elements;
        }

        /// <summary>
        /// Removes the specified class name(s) for all elements in the given
        /// collection.
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

            if (className == null)
                throw new ArgumentNullException("className");

            var classes = className.SplitSpaces();

            foreach (var element in elements)
                element.ClassList.Remove(classes);

            return elements;
        }

        /// <summary>
        /// Toggles the specified class name(s) for all elements in the given
        /// collection.
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

            if (className == null)
                throw new ArgumentNullException("className");

            var classes = className.SplitSpaces();

            foreach (var element in elements)
            {
                foreach (var @class in classes)
                    element.ClassList.Toggle(@class);
            }

            return elements;
        }

        /// <summary>
        /// Checks if any element in the given collection has the given
        /// class(es).
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="className">The name(s) of the class(es).</param>
        /// <returns>
        /// True if any element has the class(es), otherwise false.
        /// </returns>
        public static Boolean HasClass<T>(this T elements, String className)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException("elements");

            if (className == null)
                throw new ArgumentNullException("className");

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

        /// <summary>
        /// Inserts the nodes generated from the given HTML code before
        /// each element of the provided elements.
        /// </summary>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <param name="elements">The elements to iterate through.</param>
        /// <param name="html">The HTML code that generates the nodes.</param>
        /// <returns>The unchanged collection.</returns>
        public static T Before<T>(this T elements, String html)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException("elements");

            foreach (var element in elements)
            {
                var parent = element.ParentElement;

                if (parent != null)
                {
                    var fragment = parent.CreateFragment(html);
                    parent.InsertBefore(fragment, element);
                }
            }

            return elements;
        }

        /// <summary>
        /// Inserts the nodes generated from the given HTML code after
        /// each element of the provided elements.
        /// </summary>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <param name="elements">The elements to iterate through.</param>
        /// <param name="html">The HTML code that generates the nodes.</param>
        /// <returns>The unchanged collection.</returns>
        public static T After<T>(this T elements, String html)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException("elements");

            foreach (var element in elements)
            {
                var parent = element.ParentElement;

                if (parent != null)
                {
                    var fragment = parent.CreateFragment(html);
                    parent.InsertBefore(fragment, element.NextSibling);
                }
            }

            return elements;
        }

        /// <summary>
        /// Appends the nodes generated from the given HTML code to each
        /// element of the provided elements.
        /// </summary>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <param name="elements">The elements to iterate through.</param>
        /// <param name="html">The HTML code that generates the nodes.</param>
        /// <returns>The unchanged collection.</returns>
        public static T Append<T>(this T elements, String html)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException("elements");

            foreach (var element in elements)
            {
                var fragment = element.CreateFragment(html);
                element.Append(fragment);
            }

            return elements;
        }

        /// <summary>
        /// Prepends the nodes generated from the given HTML code to each
        /// element of the provided elements.
        /// </summary>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <param name="elements">The elements to iterate through.</param>
        /// <param name="html">The HTML code that generates the nodes.</param>
        /// <returns>The unchanged collection.</returns>
        public static T Prepend<T>(this T elements, String html)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException("elements");

            foreach (var element in elements)
            {
                var fragment = element.CreateFragment(html);
                element.InsertBefore(fragment, element.FirstChild);
            }

            return elements;
        }

        /// <summary>
        /// Wraps the given elements in the inner most element of the tree
        /// generated form the provided HTML code.
        /// </summary>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <param name="elements">The elements to iterate through.</param>
        /// <param name="html">The HTML code that generates the tree.</param>
        /// <returns>The unchanged collection.</returns>
        public static T Wrap<T>(this T elements, String html)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException("elements");

            foreach (var element in elements)
            {
                var fragment = element.CreateFragment(html);
                var newParent = fragment.GetInnerMostElement();
                var parent = element.Parent;

                if (parent != null)
                    parent.InsertBefore(fragment, element);

                newParent.AppendChild(element);
            }

            return elements;
        }

        /// <summary>
        /// Wraps the content of the given elements in the inner most element
        /// of the tree generated form the provided HTML code.
        /// </summary>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <param name="elements">The elements to iterate through.</param>
        /// <param name="html">The HTML code that generates the tree.</param>
        /// <returns>The unchanged collection.</returns>
        public static T WrapInner<T>(this T elements, String html)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException("elements");

            foreach (var element in elements)
            {
                var fragment = element.CreateFragment(html);
                var newParent = fragment.GetInnerMostElement();

                while (element.ChildNodes.Length > 0)
                    newParent.AppendChild(element.ChildNodes[0]);
                
                element.AppendChild(fragment);
            }

            return elements;
        }

        /// <summary>
        /// Wraps all elements in the inner most element of the tree
        /// generated form the provided HTML code. The tree is appended before
        /// the first element of the given list.
        /// </summary>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <param name="elements">The elements to wrap.</param>
        /// <param name="html">The HTML code that generates the tree.</param>
        /// <returns>The unchanged collection.</returns>
        public static T WrapAll<T>(this T elements, String html)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException("elements");

            var element = elements.FirstOrDefault();

            if (element != null)
            {
                var fragment = element.CreateFragment(html);
                var newParent = fragment.GetInnerMostElement();
                var parent = element.Parent;

                if (parent != null)
                    parent.InsertBefore(fragment, element);

                foreach (var child in elements)
                    newParent.AppendChild(child);
            }

            return elements;
        }

        #endregion

        #region Helpers

        static IDocumentFragment CreateFragment(this IElement context, String html)
        {
            return new DocumentFragment(context as Element, html ?? String.Empty);
        }

        static IElement GetInnerMostElement(this IDocumentFragment fragment)
        {
            if (fragment.ChildElementCount != 1)
                throw new InvalidOperationException("The provided HTML code did not result in any element.");

            var element = default(IElement);
            var child = fragment.FirstElementChild;

            do
            {
                element = child;
                child = element.FirstElementChild;
            }
            while (child != null);

            return element;
        }

        #endregion
    }
}
