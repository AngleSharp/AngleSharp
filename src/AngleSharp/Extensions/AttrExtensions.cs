namespace AngleSharp.Extensions
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Extensions for the list of attributes.
    /// </summary>
    static class AttrExtensions
    {
        /// <summary>
        /// Compares another attribute container to the current container.
        /// </summary>
        /// <param name="sourceAttributes">The original attribute list.</param>
        /// <param name="targetAttributes">The list to compare to.</param>
        /// <returns>True if both objects are equal, otherwise false.</returns>
        public static Boolean AreEqual(this INamedNodeMap sourceAttributes, INamedNodeMap targetAttributes)
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
    }
}
