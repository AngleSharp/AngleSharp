namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS width property.
    /// </summary>
    public interface ICssWidthProperty : ICssProperty
    {
        /// <summary>
        /// Gets the width.
        /// </summary>
        Length? Width { get; }
    }
}
