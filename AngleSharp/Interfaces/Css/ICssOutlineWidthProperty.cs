namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS outline-width property.
    /// </summary>
    public interface ICssOutlineWidthProperty : ICssProperty
    {
        /// <summary>
        /// Gets the width of the outline of an element.
        /// </summary>
        Length Width { get; }
    }
}
