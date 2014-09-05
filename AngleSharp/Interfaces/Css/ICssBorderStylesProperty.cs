namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS border-style property.
    /// </summary>
    public interface ICssBorderStylesProperty : ICssProperty
    {
        /// <summary>
        /// Gets the value for the style of the top border.
        /// </summary>
        LineStyle Top { get; }

        /// <summary>
        /// Gets the value for the style of the right border.
        /// </summary>
        LineStyle Right { get; }

        /// <summary>
        /// Gets the value for the style of the bottom border.
        /// </summary>
        LineStyle Bottom { get; }

        /// <summary>
        /// Gets the value for the style of the left border.
        /// </summary>
        LineStyle Left { get; }
    }
}
