namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents the CSS border-image-width property.
    /// </summary>
    public interface ICssBorderImageWidthProperty : ICssProperty
    {
    }

    /// <summary>
    /// Represents the CSS border-image-source property.
    /// </summary>
    public interface ICssBorderImageSourceProperty : ICssProperty
    {
    }

    /// <summary>
    /// Represents the CSS border-image-slice property.
    /// </summary>
    public interface ICssBorderImageSliceProperty : ICssProperty
    {
        /// <summary>
        /// Gets if the center patch should be filled.
        /// </summary>
        Boolean IsFilled { get; }
    }

    /// <summary>
    /// Represents the CSS border-image-repeat property.
    /// </summary>
    public interface ICssBorderImageRepeatProperty : ICssProperty
    {
        /// <summary>
        /// Gets the horizontal repeat value.
        /// </summary>
        BorderRepeat Horizontal { get; }

        /// <summary>
        /// Gets the vertical repeat value.
        /// </summary>
        BorderRepeat Vertical { get; }
    }

    /// <summary>
    /// Represents the CSS border-image-outset property.
    /// </summary>
    public interface ICssBorderImageOutsetProperty : ICssProperty
    {
    }

    /// <summary>
    /// Represents the CSS border-image shorthand property.
    /// </summary>
    public interface ICssBorderImageProperty : ICssProperty, ICssBorderImageWidthProperty, ICssBorderImageSourceProperty, ICssBorderImageSliceProperty, ICssBorderImageRepeatProperty, ICssBorderImageOutsetProperty
    {
    }
}
