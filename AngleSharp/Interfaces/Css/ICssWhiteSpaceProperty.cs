namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS white-space property.
    /// </summary>
    public interface ICssWhiteSpaceProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected whitespace handling mode.
        /// </summary>
        Whitespace Mode { get; }
    }
}
