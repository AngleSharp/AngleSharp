namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS bottom property.
    /// </summary>
    public interface ICssBottomProperty : ICssProperty
    {
        /// <summary>
        /// Gets the position if a fixed position has been set.
        /// </summary>
        Length? Bottom { get; }
    }
}
