namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS border-spacing property.
    /// </summary>
    public interface ICssBorderSpacingProperty : ICssProperty
    {
        /// <summary>
        /// Gets the horizontal spacing between cells, that is the space
        /// between cells in adjacent columns.
        /// </summary>
        Length Horizontal { get; }

        /// <summary>
        /// Gets the vertical spacing between cells, that is the space
        /// between cells in adjacent rows.
        /// </summary>
        Length Vertical { get; }
    }
}
