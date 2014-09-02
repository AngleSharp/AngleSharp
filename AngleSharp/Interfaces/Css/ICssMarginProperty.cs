namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS margin-top property.
    /// </summary>
    public interface ICssMarginTopProperty : ICssProperty
    {
    }

    /// <summary>
    /// Represents the CSS margin-right property.
    /// </summary>
    public interface ICssMarginRightProperty : ICssProperty
    {
    }

    /// <summary>
    /// Represents the CSS margin-left property.
    /// </summary>
    public interface ICssMarginLeftProperty : ICssProperty
    {
    }

    /// <summary>
    /// Represents the CSS margin-bottom property.
    /// </summary>
    public interface ICssMarginBottomProperty : ICssProperty
    {
    }

    /// <summary>
    /// Represents the CSS margin shorthand property.
    /// </summary>
    public interface ICssMarginProperty : ICssProperty, ICssMarginBottomProperty, ICssMarginLeftProperty, ICssMarginRightProperty, ICssMarginTopProperty
    {
    }
}
