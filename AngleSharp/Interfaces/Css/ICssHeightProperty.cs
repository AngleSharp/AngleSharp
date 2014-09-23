namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS height property.
    /// </summary>
    public interface ICssHeightProperty : ICssProperty
    {
        /// <summary>
        /// Gets the height.
        /// </summary>
        IDistance Height { get; }
    }
}
