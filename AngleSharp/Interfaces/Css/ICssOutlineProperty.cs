namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS outline-width property.
    /// </summary>
    public interface ICssOutlineWidthProperty : ICssProperty
    {
        /// <summary>
        /// Gets the width of the outline of an element.
        /// </summary>
        Length Width { get; }
    }

    /// <summary>
    /// Repesents the CSS outline-style property.
    /// </summary>
    public interface ICssOutlineStyleProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected outline style.
        /// </summary>
        LineStyle Style { get; }
    }

    /// <summary>
    /// Represents the CSS outline-color property.
    /// </summary>
    public interface ICssOutlineColorProperty : ICssProperty
    {
        /// <summary>
        /// Gets the color of the outline.
        /// </summary>
        Color Color { get; }
    }

    /// <summary>
    /// Represents the CSS outline shorthand property.
    /// </summary>
    public interface ICssOutlineProperty : ICssProperty, ICssOutlineColorProperty, ICssOutlineStyleProperty, ICssOutlineWidthProperty
    {
    }
}
