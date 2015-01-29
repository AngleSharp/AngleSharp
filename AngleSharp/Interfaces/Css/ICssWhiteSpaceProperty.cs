namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// Represents the CSS white-space property.
    /// </summary>
    public interface ICssWhitespaceProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected whitespace handling mode.
        /// </summary>
        Whitespace State { get; }
    }
}
