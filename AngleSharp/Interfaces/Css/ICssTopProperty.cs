namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS top property.
    /// </summary>
    public interface ICssTopProperty : ICssProperty
    {
        /// <summary>
        /// Gets the position if a fixed position has been set.
        /// </summary>
        IDistance Top { get; }
    }
}
