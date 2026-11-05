namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Element extensions exposed to DOM.
    /// </summary>
    [DomExposed("Element")]
    public static class DomElementExtensions
    {
        private const String InertAttribute = "inert";

        /// <summary>
        /// Gets the names of the attributes of the element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The qualified names in the order of the attributes.</returns>
        [DomName("getAttributeNames")]
        public static String[] GetAttributeNames(this IElement element)
        {
            var names = new String[element.Attributes.Length];

            for (var i = 0; i < names.Length; i++)
            {
                names[i] = element.Attributes[i]!.Name;
            }

            return names;
        }

        /// <summary>
        /// Gets if the element has any attributes.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>True if it has at least one.</returns>
        [DomName("hasAttributes")]
        public static Boolean HasAttributes(this IElement element) => element.Attributes.Length > 0;

        /// <summary>
        /// Inserts an element relative to this one.
        /// </summary>
        /// <param name="element">The element to insert relative to.</param>
        /// <param name="where">beforebegin, afterbegin, beforeend or afterend.</param>
        /// <param name="inserted">The element to insert.</param>
        /// <returns>The inserted element, or null if there is no parent to insert it to.</returns>
        [DomName("insertAdjacentElement")]
        public static IElement? InsertAdjacentElement(this IElement element, String where, IElement inserted) =>
            InsertAdjacent(element, where, inserted) ? inserted : null;

        /// <summary>
        /// Inserts text relative to this element.
        /// </summary>
        /// <param name="element">The element to insert relative to.</param>
        /// <param name="where">beforebegin, afterbegin, beforeend or afterend.</param>
        /// <param name="text">The text to insert.</param>
        [DomName("insertAdjacentText")]
        public static void InsertAdjacentText(this IElement element, String where, String text) =>
            InsertAdjacent(element, where, element.Owner!.CreateTextNode(text));

        /// <summary>
        /// Gets if the element is inert.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>True if it has the attribute.</returns>
        [DomName("inert")]
        [DomAccessor(Accessors.Getter)]
        public static Boolean GetInert(this IElement element) => element.HasAttribute(InertAttribute);

        /// <summary>
        /// Sets if the element is inert.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">True to add the attribute.</param>
        [DomName("inert")]
        [DomAccessor(Accessors.Setter)]
        public static void SetInert(this IElement element, Boolean value)
        {
            if (value)
            {
                element.SetAttribute(InertAttribute, String.Empty);
            }
            else
            {
                element.RemoveAttribute(InertAttribute);
            }
        }

        private static Boolean InsertAdjacent(IElement element, String where, INode node)
        {
            switch (where?.ToLowerInvariant())
            {
                case "beforebegin":
                    if (element.Parent is null)
                    {
                        return false;
                    }

                    element.Parent.InsertBefore(node, element);
                    return true;
                case "afterbegin":
                    element.InsertBefore(node, element.FirstChild);
                    return true;
                case "beforeend":
                    element.AppendChild(node);
                    return true;
                case "afterend":
                    if (element.Parent is null)
                    {
                        return false;
                    }

                    element.Parent.InsertBefore(node, element.NextSibling);
                    return true;
                default:
                    throw new DomException(DomError.Syntax);
            }
        }

        /// <summary>
        /// Returns the named attribute on the specified element.
        /// </summary>
        /// <param name="element">
        /// Current element
        /// </param>
        /// <param name="name">
        /// The name of the attribute you want to get.
        /// </param>
        /// <returns>
        /// Named attribute or null if it doesn't exist.
        /// </returns>
        [DomName("getAttributeNode")]
        public static IAttr? GetAttributeNode(this IElement element, String name)
        {
            if (element.GivenNamespaceUri.Is(NamespaceNames.HtmlUri))
            {
                name = name.HtmlLower();
            }

            return element.Attributes.GetNamedItem(name);
        }

        /// <summary>
        /// Returns the named attribute on the specified element.
        /// </summary>
        /// <param name="element">
        /// Current element
        /// </param>
        /// <param name="namespaceUri">
        /// A string specifying the namespace of the attribute.
        /// </param>
        /// <param name="localName">
        /// The name of the attribute you want to get.
        /// </param>
        /// <returns>
        /// Named attribute or null if it doesn't exist.
        /// </returns>
        [DomName("getAttributeNodeNS")]
        public static IAttr? GetAttributeNode(this IElement element, String? namespaceUri, String localName)
        {
            if (String.IsNullOrEmpty(namespaceUri))
            {
                namespaceUri = null;
            }

            return element.Attributes.GetNamedItem(namespaceUri, localName);
        }
    }
}
