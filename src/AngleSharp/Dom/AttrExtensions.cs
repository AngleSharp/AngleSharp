namespace AngleSharp.Dom
{
    using System;

    /// <summary>
    /// Extensions for the list of attributes.
    /// </summary>
    public static class AttrExtensions
    {
        /// <summary>
        /// Compares another attribute container to the current container.
        /// </summary>
        /// <param name="sourceAttributes">The original attribute list.</param>
        /// <param name="targetAttributes">The list to compare to.</param>
        /// <returns>True if both objects are equal, otherwise false.</returns>
        public static Boolean SameAs(this INamedNodeMap sourceAttributes, INamedNodeMap targetAttributes)
        {
            if (sourceAttributes.Length == targetAttributes.Length)
            {
                foreach (var elA in sourceAttributes)
                {
                    var found = false;

                    foreach (var elB in targetAttributes)
                    {
                        found = elA.GetHashCode() == elB.GetHashCode();

                        if (found)
                            break;
                    }

                    if (!found)
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Clears the given attribute collection.
        /// </summary>
        /// <param name="attributes">The collection to clear.</param>
        /// <returns>The collection itself.</returns>
        public static INamedNodeMap Clear(this INamedNodeMap attributes)
        {
            if (attributes == null)
                throw new ArgumentNullException(nameof(attributes));

            while (attributes.Length > 0)
            {
                var name = attributes[attributes.Length - 1].Name;
                attributes.RemoveNamedItem(name);
            }

            return attributes;
        }
    }
}
