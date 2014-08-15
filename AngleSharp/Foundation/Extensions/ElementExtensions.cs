namespace AngleSharp
{
    using AngleSharp.DOM;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Useful methods for element objects.
    /// </summary>
    [DebuggerStepThrough]
    static class ElementExtensions
    {
        /// <summary>
        /// Locates the prefix of the given namespace.
        /// </summary>
        /// <param name="element">The element that might contain the namespace information.</param>
        /// <param name="namespaceUri">The url of the namespace.</param>
        /// <returns>The prefix or null, if the namespace could not be found.</returns>
        public static String LocatePrefix(this IElement element, String namespaceUri)
        {
            if (element.NamespaceUri == namespaceUri && element.Prefix != null)
                return element.Prefix;

            foreach (var attr in element.Attributes)
            {
                if (attr.Prefix == Namespaces.XmlNsPrefix && attr.Value == namespaceUri)
                    return attr.LocalName;
            }

            var parent = element.ParentElement;

            if (parent != null)
                return parent.LocatePrefix(namespaceUri);

            return null;
        }

        /// <summary>
        /// Locates the namespace of the given prefix.
        /// </summary>
        /// <param name="element">The element that might contain the namespace information.</param>
        /// <param name="prefix">The prefix of the namespace to find.</param>
        /// <returns>The url of the namespace or null, if the prefix could not be found.</returns>
        public static String LocateNamespace(this IElement element, String prefix)
        {
            var ns = element.NamespaceUri;
            var px = element.Prefix;

            if (!String.IsNullOrEmpty(ns) && px == prefix)
                return ns;

            var predicate = prefix == null ? (Predicate<IAttr>)
                (attr => (attr.NamespaceUri == Namespaces.XmlNsUri && attr.Prefix == null && attr.LocalName == Namespaces.XmlNsPrefix)) :
                (attr => (attr.NamespaceUri == Namespaces.XmlNsUri && attr.Prefix == Namespaces.XmlNsPrefix && attr.LocalName == prefix));

            foreach (var attr in element.Attributes)
            {
                if (predicate(attr))
                {
                    var value = attr.Value;

                    if (String.IsNullOrEmpty(value))
                        value = null;

                    return value;
                }
            }

            var parent = element.ParentElement;

            if (parent != null)
                return parent.LocateNamespace(prefix);

            return null;
        }
    }
}
