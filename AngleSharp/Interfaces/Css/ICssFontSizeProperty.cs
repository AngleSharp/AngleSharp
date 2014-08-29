namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS font-size property.
    /// </summary>
    public interface ICssFontSizeProperty : ICssProperty
    {
        /// <summary>
        /// Gets the font-size mode.
        /// </summary>
        FontSize Mode { get; }
    }
}
