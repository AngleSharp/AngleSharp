namespace AngleSharp.Extensions
{
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

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
        public static Boolean Has(this List<IAttr> attributes, String name)
        {
            for (int i = 0; i < attributes.Count; i++)
            {
                if (attributes[i].Name == name)
                    return true;
            }

            return false;
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
        public static Boolean Has(this List<IAttr> attributes, String namespaceUri, String localName)
        {
            for (int i = 0; i < attributes.Count; i++)
            {
                if (attributes[i].NamespaceUri == namespaceUri && attributes[i].LocalName == localName)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the attribute with the provided name.
        /// </summary>
        /// <param name="attributes">The list of attributes.</param>
        /// <param name="name">The name to get.</param>
        /// <returns>The attribute with the name or null.</returns>
        public static IAttr Get(this List<IAttr> attributes, String name)
        {
            for (int i = 0; i < attributes.Count; i++)
            {
                if (attributes[i].Name == name)
                    return attributes[i];
            }

            return null;
        }

        /// <summary>
        /// Gets the an attribute with the provided local name and namespace
        /// URI.
        /// </summary>
        /// <param name="attributes">The list of attributes.</param>
        /// <param name="namespaceUri">
        /// The namespace URI of the attribute.
        /// </param>
        /// <param name="localName">The local name to get.</param>
        /// <returns>
        /// The attribute with the local name and namespace or null.
        /// </returns>
        public static IAttr Get(this List<IAttr> attributes, String namespaceUri, String localName)
        {
            for (int i = 0; i < attributes.Count; i++)
            {
                if (attributes[i].NamespaceUri == namespaceUri && attributes[i].LocalName == localName)
                    return attributes[i];
            }

            return null;
        }

        /// <summary>
        /// Compares another attribute container to the current container.
        /// </summary>
        /// <param name="sourceAttributes">The original attribute list.</param>
        /// <param name="targetAttributes">
        /// The list of attributes to compare to.
        /// </param>
        /// <returns>True if both objects are equal, otherwise false.</returns>
        public static Boolean AreEqual(this List<IAttr> sourceAttributes, List<IAttr> targetAttributes)
        {
            if (sourceAttributes.Count != targetAttributes.Count)
                return false;

            for (int i = 0; i < sourceAttributes.Count; i++)
            {
                var elA = sourceAttributes[i];
                var found = false;

                for (int j = 0; j < targetAttributes.Count; j++)
                {
                    var elB = targetAttributes[j];

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
