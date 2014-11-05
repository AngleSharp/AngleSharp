namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Represents the CSS border-width property.
    /// </summary>
    public interface ICssBorderWidthProperty : ICssProperty
    {
        /// <summary>
        /// Gets the width of the given border property.
        /// </summary>
        Length Width { get; }
    }

    /// <summary>
    /// Represents the CSS border-color property.
    /// </summary>
    public interface ICssBorderColorProperty : ICssProperty
    {
        /// <summary>
        /// Gets the color of the given border property.
        /// </summary>
        Color Color { get; }
    }

    /// <summary>
    /// Represents the CSS border-style property.
    /// </summary>
    public interface ICssBorderStyleProperty : ICssProperty
    {
        /// <summary>
        /// Gets the style of the given border property.
        /// </summary>
        LineStyle Style { get; }
    }

    /// <summary>
    /// Represents the CSS border shorthand property.
    /// </summary>
    public interface ICssBorderProperty : ICssProperty, ICssBorderWidthProperty, ICssBorderStyleProperty, ICssBorderColorProperty
    {
    }
}
