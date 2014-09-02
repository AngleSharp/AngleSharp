namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS padding-right property.
    /// </summary>
    public interface ICssPaddingRightProperty : ICssProperty
    {
    }

    /// <summary>
    /// Represents the CSS padding-top property.
    /// </summary>
    public interface ICssPaddingTopProperty : ICssProperty
    {
    }

    /// <summary>
    /// Represents the CSS padding-bottom property.
    /// </summary>
    public interface ICssPaddingBottomProperty : ICssProperty
    {
    }

    /// <summary>
    /// Represents the CSS padding-left property.
    /// </summary>
    public interface ICssPaddingLeftProperty : ICssProperty
    {
    }

    /// <summary>
    /// Represents the CSS padding shorthand property.
    /// </summary>
    public interface ICssPaddingProperty : ICssProperty, ICssPaddingBottomProperty, ICssPaddingRightProperty, ICssPaddingTopProperty, ICssPaddingLeftProperty
    {
    }
}
