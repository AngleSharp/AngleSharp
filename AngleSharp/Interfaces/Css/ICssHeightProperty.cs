namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

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
