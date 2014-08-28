namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS perspective property.
    /// </summary>
    public interface ICssPerspectiveProperty : ICssProperty
    {
        /// <summary>
        /// Gets the distance from the user to the z=0 plane.
        /// </summary>
        Length Distance { get; }
    }
}
