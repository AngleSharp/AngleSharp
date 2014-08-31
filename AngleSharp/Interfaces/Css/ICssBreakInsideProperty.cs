namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS break-inside property.
    /// </summary>
    public interface ICssBreakInsideProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected break mode.
        /// </summary>
        BreakMode Mode { get; }
    }
}
