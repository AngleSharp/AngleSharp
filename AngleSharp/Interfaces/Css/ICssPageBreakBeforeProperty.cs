namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS page-break-before property.
    /// </summary>
    public interface ICssPageBreakBeforeProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected break mode.
        /// </summary>
        BreakMode State { get; }
    }
}
