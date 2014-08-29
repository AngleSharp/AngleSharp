namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS font-style property.
    /// </summary>
    public interface ICssFontStyleProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected font style.
        /// </summary>
        FontStyle Style { get; }
    }
}
