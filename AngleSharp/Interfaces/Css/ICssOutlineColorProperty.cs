namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS outline-color property.
    /// </summary>
    public interface ICssOutlineColorProperty : ICssProperty
    {
        /// <summary>
        /// Gets the color of the outline.
        /// </summary>
        Color Color { get; }
    }
}
