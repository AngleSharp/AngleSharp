namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS border-bottom-right-radius property.
    /// </summary>
    public interface ICssBorderBottomRightRadiusProperty : ICssProperty
    {
    }

    /// <summary>
    /// Represents the CSS border-top-right-radius property.
    /// </summary>
    public interface ICssBorderTopRightRadiusProperty : ICssProperty
    {
    }

    /// <summary>
    /// Represents the CSS border-bottom-left-radius property.
    /// </summary>
    public interface ICssBorderBottomLeftRadiusProperty : ICssProperty
    {
    }

    /// <summary>
    /// Represents the CSS border-top-left-radius property.
    /// </summary>
    public interface ICssBorderTopLeftRadiusProperty : ICssProperty
    {
    }

    /// <summary>
    /// Represents the CSS border-radius shorthand property.
    /// </summary>
    public interface ICssBorderRadiusProperty : ICssProperty, ICssBorderBottomLeftRadiusProperty, ICssBorderBottomRightRadiusProperty, ICssBorderTopLeftRadiusProperty, ICssBorderTopRightRadiusProperty
    {
    }
}
