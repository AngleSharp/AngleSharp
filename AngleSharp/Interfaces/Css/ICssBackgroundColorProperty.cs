namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS background-color property.
    /// </summary>
    public interface ICssBackgroundColorProperty : ICssProperty
    {
        /// <summary>
        /// Gets the color of the background.
        /// </summary>
        /// <returns></returns>
        Color Color { get; }
    }
}
