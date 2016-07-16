namespace AngleSharp.Services
{
    using AngleSharp.Dom.Css;
    using System;

    /// <summary>
    /// Represents a factory for pseudo-element selectors.
    /// </summary>
    public interface IPseudoElementSelectorFactory
    {
        /// <summary>
        /// Creates a new pseudo-element selector for the given name.
        /// </summary>
        /// <param name="name">The name of the pseudo-element.</param>
        /// <returns>The created selector, if any.</returns>
        ISelector Create(String name);
    }
}
