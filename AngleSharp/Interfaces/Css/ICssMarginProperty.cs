namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Represents the CSS margin-top property.
    /// </summary>
    public interface ICssMarginTopProperty : ICssProperty
    {
        /// <summary>
        /// Gets the margin relative to the width of the containing block or
        /// a fixed width, if any.
        /// </summary>
        Length? Top { get; }
    }

    /// <summary>
    /// Represents the CSS margin-right property.
    /// </summary>
    public interface ICssMarginRightProperty : ICssProperty
    {
        /// <summary>
        /// Gets the margin relative to the width of the containing block or
        /// a fixed width, if any.
        /// </summary>
        Length? Right { get; }
    }

    /// <summary>
    /// Represents the CSS margin-left property.
    /// </summary>
    public interface ICssMarginLeftProperty : ICssProperty
    {
        /// <summary>
        /// Gets the margin relative to the width of the containing block or
        /// a fixed width, if any.
        /// </summary>
        Length? Left { get; }
    }

    /// <summary>
    /// Represents the CSS margin-bottom property.
    /// </summary>
    public interface ICssMarginBottomProperty : ICssProperty
    {
        /// <summary>
        /// Gets the margin relative to the width of the containing block or
        /// a fixed width, if any.
        /// </summary>
        Length? Bottom { get; }
    }

    /// <summary>
    /// Represents the CSS margin shorthand property.
    /// </summary>
    public interface ICssMarginProperty : ICssProperty, ICssMarginBottomProperty, ICssMarginLeftProperty, ICssMarginRightProperty, ICssMarginTopProperty
    {
    }
}
