namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Represents the CSS width property.
    /// </summary>
    public interface ICssWidthProperty : ICssProperty
    {
        /// <summary>
        /// Gets the width.
        /// </summary>
        IDistance Width { get; }
    }
}
