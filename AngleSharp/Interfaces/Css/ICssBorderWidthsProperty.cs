namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS border-width property.
    /// </summary>
    public interface ICssBorderWidthsProperty : ICssProperty
    {
        /// <summary>
        /// Gets the value for the width of the top border.
        /// </summary>
        Length Top { get; }

        /// <summary>
        /// Gets the value for the width of the right border.
        /// </summary>
        Length Right { get; }

        /// <summary>
        /// Gets the value for the width of the bottom border.
        /// </summary>
        Length Bottom { get; }

        /// <summary>
        /// Gets the value for the width of the left border.
        /// </summary>
        Length Left { get; }
    }
}
