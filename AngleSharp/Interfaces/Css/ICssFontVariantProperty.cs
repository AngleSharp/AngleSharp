namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS font-variant property.
    /// </summary>
    public interface ICssFontVariantProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected font variant transformation, if any.
        /// </summary>
        FontVariant Variant { get; }
    }
}
