namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css.Values;
    using System;

    /// <summary>
    /// Represents the CSS border-image-width property.
    /// </summary>
    public interface ICssBorderImageWidthProperty : ICssProperty
    {
        /// <summary>
        /// Gets the top length of the image slice, if any.
        /// </summary>
        IDistance WidthTop { get; }

        /// <summary>
        /// Gets the bottom length of the image slice, if any.
        /// </summary>
        IDistance WidthBottom { get; }

        /// <summary>
        /// Gets the left length of the image slice, if any.
        /// </summary>
        IDistance WidthLeft { get; }

        /// <summary>
        /// Gets the right length of the image slice, if any.
        /// </summary>
        IDistance WidthRight { get; }
    }

    /// <summary>
    /// Represents the CSS border-image-source property.
    /// </summary>
    public interface ICssBorderImageSourceProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected image.
        /// </summary>
        IImageSource Image { get; }
    }

    /// <summary>
    /// Represents the CSS border-image-slice property.
    /// </summary>
    public interface ICssBorderImageSliceProperty : ICssProperty
    {
        /// <summary>
        /// Gets the position of the top slicing line.
        /// </summary>
        IDistance SliceTop { get; }

        /// <summary>
        /// Gets the position of the right slicing line.
        /// </summary>
        IDistance SliceRight { get; }

        /// <summary>
        /// Gets the position of the bottom slicing line.
        /// </summary>
        IDistance SliceBottom { get; }

        /// <summary>
        /// Gets the position of the left slicing line.
        /// </summary>
        IDistance SliceLeft { get; }

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
        /// <summary>
        /// Gets the length or percentage for the outset of the top border.
        /// </summary>
        IDistance OutsetTop { get; }

        /// <summary>
        /// Gets the length or percentage for the outset of the right border.
        /// </summary>
        IDistance OutsetRight { get; }

        /// <summary>
        /// Gets the length or percentage for the outset of the bottom border.
        /// </summary>
        IDistance OutsetBottom { get; }

        /// <summary>
        /// Gets the length or percentage for the outset of the left border.
        /// </summary>
        IDistance OutsetLeft { get; }
    }

    /// <summary>
    /// Represents the CSS border-image shorthand property.
    /// </summary>
    public interface ICssBorderImageProperty : ICssProperty, ICssBorderImageWidthProperty, ICssBorderImageSourceProperty, ICssBorderImageSliceProperty, ICssBorderImageRepeatProperty, ICssBorderImageOutsetProperty
    {
    }
}
