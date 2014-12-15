namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Represents the CSS border-bottom-right-radius property.
    /// </summary>
    public interface ICssBorderBottomRightRadiusProperty : ICssProperty
    {
        /// <summary>
        /// Gets the value of the horizontal bottom-right radius.
        /// </summary>
        Length HorizontalBottomRight { get; }

        /// <summary>
        /// Gets the value of the vertical bottom-right radius.
        /// </summary>
        Length VerticalBottomRight { get; }
    }

    /// <summary>
    /// Represents the CSS border-top-right-radius property.
    /// </summary>
    public interface ICssBorderTopRightRadiusProperty : ICssProperty
    {
        /// <summary>
        /// Gets the value of the horizontal top-right radius.
        /// </summary>
        Length HorizontalTopRight { get; }

        /// <summary>
        /// Gets the value of the vertical top-right radius.
        /// </summary>
        Length VerticalTopRight { get; }
    }

    /// <summary>
    /// Represents the CSS border-bottom-left-radius property.
    /// </summary>
    public interface ICssBorderBottomLeftRadiusProperty : ICssProperty
    {
        /// <summary>
        /// Gets the value of the horizontal bottom-left radius.
        /// </summary>
        Length HorizontalBottomLeft { get; }

        /// <summary>
        /// Gets the value of the vertical bottom-left radius.
        /// </summary>
        Length VerticalBottomLeft { get; }
    }

    /// <summary>
    /// Represents the CSS border-top-left-radius property.
    /// </summary>
    public interface ICssBorderTopLeftRadiusProperty : ICssProperty
    {
        /// <summary>
        /// Gets the value of the horizontal top-left radius.
        /// </summary>
        Length HorizontalTopLeft { get; }

        /// <summary>
        /// Gets the value of the vertical top-left radius.
        /// </summary>
        Length VerticalTopLeft { get; }
    }

    /// <summary>
    /// Represents the CSS border-radius shorthand property.
    /// </summary>
    public interface ICssBorderRadiusProperty : ICssProperty, ICssBorderBottomLeftRadiusProperty, ICssBorderBottomRightRadiusProperty, ICssBorderTopLeftRadiusProperty, ICssBorderTopRightRadiusProperty
    {
    }
}
