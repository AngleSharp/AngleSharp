namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS list-style-type property.
    /// </summary>
    public interface ICssListStyleTypeProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected style for the list.
        /// </summary>
        ListStyle Style { get; }
    }
}
