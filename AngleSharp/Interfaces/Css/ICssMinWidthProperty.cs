namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS min-width property.
    /// </summary>
    public interface ICssMinWidthProperty : ICssProperty
    {
        /// <summary>
        /// Gets the minimum height of the element.
        /// </summary>
        IDistance Limit { get; }
    }
}
