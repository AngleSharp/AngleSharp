namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS list-style-position property.
    /// </summary>
    public interface ICssListStylePositionProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected position.
        /// </summary>
        ListPosition Position { get; }
    }
}
