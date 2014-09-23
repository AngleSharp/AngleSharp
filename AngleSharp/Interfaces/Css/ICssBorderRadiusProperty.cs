namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS border-bottom-right-radius property.
    /// </summary>
    public interface ICssBorderBottomRightRadiusProperty : ICssProperty
    {
        /// <summary>
        /// Gets the value of the horizontal bottom-right radius.
        /// </summary>
        IDistance HorizontalBottomRight { get; }

        /// <summary>
        /// Gets the value of the vertical bottom-right radius.
        /// </summary>
        IDistance VerticalBottomRight { get; }
    }

    /// <summary>
    /// Represents the CSS border-top-right-radius property.
    /// </summary>
    public interface ICssBorderTopRightRadiusProperty : ICssProperty
    {
        /// <summary>
        /// Gets the value of the horizontal top-right radius.
        /// </summary>
        IDistance HorizontalTopRight { get; }

        /// <summary>
        /// Gets the value of the vertical top-right radius.
        /// </summary>
        IDistance VerticalTopRight { get; }
    }

    /// <summary>
    /// Represents the CSS border-bottom-left-radius property.
    /// </summary>
    public interface ICssBorderBottomLeftRadiusProperty : ICssProperty
    {
        /// <summary>
        /// Gets the value of the horizontal bottom-left radius.
        /// </summary>
        IDistance HorizontalBottomLeft { get; }

        /// <summary>
        /// Gets the value of the vertical bottom-left radius.
        /// </summary>
        IDistance VerticalBottomLeft { get; }
    }

    /// <summary>
    /// Represents the CSS border-top-left-radius property.
    /// </summary>
    public interface ICssBorderTopLeftRadiusProperty : ICssProperty
    {
        /// <summary>
        /// Gets the value of the horizontal top-left radius.
        /// </summary>
        IDistance HorizontalTopLeft { get; }

        /// <summary>
        /// Gets the value of the vertical top-left radius.
        /// </summary>
        IDistance VerticalTopLeft { get; }
    }

    /// <summary>
    /// Represents the CSS border-radius shorthand property.
    /// </summary>
    public interface ICssBorderRadiusProperty : ICssProperty, ICssBorderBottomLeftRadiusProperty, ICssBorderBottomRightRadiusProperty, ICssBorderTopLeftRadiusProperty, ICssBorderTopRightRadiusProperty
    {
    }
}
