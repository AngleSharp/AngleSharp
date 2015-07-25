namespace AngleSharp.Extensions
{
    using AngleSharp.Dom;
    using System;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Extensions for the list of attributes.
    /// </summary>
    [DebuggerStepThrough]
    static class AttrExtensions
    {
        /// <summary>
        /// Checks if an attribute with the provided local name is given.
        /// </summary>
        /// <param name="attributes">The list of attributes.</param>
        /// <param name="name">The local name to check for.</param>
        /// <returns>
        /// True if an attribute without a prefix and the given local name
        /// exists.
        /// </returns>
        public static Boolean Has(this INamedNodeMap attributes, String name)
        {
            return attributes.Any(attribute => attribute.Name == name);
        }

        /// <summary>
        /// Checks if an attribute with the provided local name and namespace
        /// URI is given.
        /// </summary>
        /// <param name="attributes">The list of attributes.</param>
        /// <param name="namespaceUri">
        /// The namespace URI of the attribute.
        /// </param>
        /// <param name="localName">The local name to check for.</param>
        /// <returns>
        /// True if an attribute with the provided namespace and the given
        /// local name exists.
        /// </returns>
        public static Boolean Has(this INamedNodeMap attributes, String namespaceUri, String localName)
        {
            return attributes.Any(attribute => attribute.NamespaceUri == namespaceUri && attribute.LocalName == localName);
        }
        
        /// <summary>
        /// Compares another attribute container to the current container.
        /// </summary>
        /// <param name="sourceAttributes">The original attribute list.</param>
        /// <param name="targetAttributes">
        /// The list of attributes to compare to.
        /// </param>
        /// <returns>True if both objects are equal, otherwise false.</returns>
        public static Boolean AreEqual(this INamedNodeMap sourceAttributes, INamedNodeMap targetAttributes)
        {
            if (sourceAttributes.Length != targetAttributes.Length)
                return false;

            foreach (var elA in sourceAttributes)
            {
                var found = false;

                foreach (var elB in targetAttributes)
                {
                    if (found = (elA.Name == elB.Name && elA.NamespaceUri == elB.NamespaceUri && elA.Value == elB.Value))
                        break;
                }

                if (!found)
                    return false;
            }

            return true;
        }
    }
}
