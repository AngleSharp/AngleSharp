namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// Represents the CSS height property.
    /// </summary>
    public interface ICssHeightProperty : ICssProperty
    {
        /// <summary>
        /// Gets the height.
        /// </summary>
        Length? Height { get; }
    }
}
