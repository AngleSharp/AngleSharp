namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS font-stretch property.
    /// </summary>
    public interface ICssFontStretchProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected font stretch setting.
        /// </summary>
        FontStretch Stretch { get; }
    }
}
