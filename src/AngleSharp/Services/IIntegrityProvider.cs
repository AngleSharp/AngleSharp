namespace AngleSharp.Services
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the methods to perform an integrity check.
    /// </summary>
    public interface IIntegrityProvider
    {
        /// <summary>
        /// Gets the prioritized hash function from the given hash functions.
        /// </summary>
        /// <param name="functions">The functions to examine.</param>
        /// <returns>The prioritized hash function if any.</returns>
        String GetPrioritizedHashFunction(IEnumerable<String> functions);

        /// <summary>
        /// Checks if the given content satisfies the provided integrity
        /// attribute.
        /// </summary>
        /// <param name="content">The content to hash.</param>
        /// <param name="integrity">The attribute of the integrity.</param>
        /// <returns>True if integrity is preserved, otherwise false.</returns>
        Boolean IsSatisfied(Byte[] content, String integrity);
    }
}
