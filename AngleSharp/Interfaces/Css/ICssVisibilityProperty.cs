namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS visibility property.
    /// </summary>
    public interface ICssVisibilityProperty : ICssProperty
    {
        /// <summary>
        /// Gets the visibility mode.
        /// </summary>
        Visibility Visibility { get; }
    }
}
