namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS perspective-origin property.
    /// </summary>
    public interface ICssPerspectiveOriginProperty : ICssProperty
    {
        /// <summary>
        /// Gets the position of the abscissa of the vanishing point.
        /// </summary>
        Length X { get; }

        /// <summary>
        /// Gets the position of the ordinate of the vanishing point.
        /// </summary>
        Length Y { get; }
    }
}
