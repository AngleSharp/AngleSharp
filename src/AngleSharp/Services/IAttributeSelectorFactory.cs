namespace AngleSharp.Services
{
    using AngleSharp.Dom.Css;
    using System;

    /// <summary>
    /// Represents a factory for attribute selectors.
    /// </summary>
    public interface IAttributeSelectorFactory
    {
        /// <summary>
        /// Creates a new attribute selector from the given arguments.
        /// </summary>
        /// <param name="combinator">The used combinator.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The provided value.</param>
        /// <param name="prefix">The prefix, if any.</param>
        /// <param name="insensitive">Should be evaluated insensitive.</param>
        /// <returns>The created selector, if possible.</returns>
        ISelector Create(String combinator, String name, String value, String prefix, Boolean insensitive);
    }
}
