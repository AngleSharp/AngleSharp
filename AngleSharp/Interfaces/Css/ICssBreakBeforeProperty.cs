namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS break-before property.
    /// </summary>
    public interface ICssBreakBeforeProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected break mode.
        /// </summary>
        BreakMode Mode { get; }
    }
}
