namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Represents the CSS border-color property.
    /// </summary>
    public interface ICssBorderColorsProperty : ICssProperty
    {
        /// <summary>
        /// Gets the value for the color of the top border.
        /// </summary>
        Color Top { get; }

        /// <summary>
        /// Gets the value for the color of the right border.
        /// </summary>
        Color Right { get; }

        /// <summary>
        /// Gets the value for the color of the bottom border.
        /// </summary>
        Color Bottom { get; }

        /// <summary>
        /// Gets the value for the color of the left border.
        /// </summary>
        Color Left { get; }
    }
}
