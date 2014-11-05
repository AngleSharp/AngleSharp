namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Represents the CSS max-height property.
    /// </summary>
    public interface ICssMaxHeightProperty : ICssProperty
    {
        /// <summary>
        /// Gets the specified max-height of the element, if any.
        /// </summary>
        IDistance Limit { get; }
    }
}
