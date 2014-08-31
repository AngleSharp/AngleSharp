namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS page-break-inside property.
    /// </summary>
    public interface ICssPageBreakInsideProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected break mode.
        /// </summary>
        BreakMode Mode { get; }
    }
}
