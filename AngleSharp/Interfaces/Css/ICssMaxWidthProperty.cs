namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// Represents the CSS max-width property.
    /// </summary>
    public interface ICssMaxWidthProperty : ICssProperty
    {
        /// <summary>
        /// Gets the specified max-width of the element, if any.
        /// </summary>
        Length? Limit { get; }
    }
}
