namespace AngleSharp.Services
{
    using AngleSharp.Dom.Css;
    using System;

    /// <summary>
    /// Represents a factory for pseudo-class selectors.
    /// </summary>
    public interface IPseudoClassSelectorFactory
    {
        /// <summary>
        /// Creates a new pseudo-class selector for the given name.
        /// </summary>
        /// <param name="name">The name of the pseudo-class.</param>
        /// <returns>The created selector, if any.</returns>
        ISelector Create(String name);
    }
}
