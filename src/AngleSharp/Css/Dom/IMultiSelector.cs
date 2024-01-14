namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;

    /// <summary>
    /// Represents a CSS selector containing multiple
    /// CSS selectors.
    /// </summary>
    public interface IMultiSelector
    {
        /// <summary>
        /// Gets the contained selector that matches the provided element / scope, if any.
        /// </summary>
        /// <param name="element">The element to be matched.</param>
        /// <param name="scope">The selector scope.</param>
        /// <returns>The contained selector if available, otherwise null.</returns>
        ISelector? GetMatchingSelector(IElement element, IElement? scope = null);
    }
}
