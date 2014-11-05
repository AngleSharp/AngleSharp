namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Represents the CSS right property.
    /// </summary>
    public interface ICssRightProperty : ICssProperty
    {
        /// <summary>
        /// Gets the position if a fixed position has been set.
        /// </summary>
        IDistance Right { get; }
    }
}
