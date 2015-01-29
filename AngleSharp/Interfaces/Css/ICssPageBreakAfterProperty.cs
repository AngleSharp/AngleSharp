namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// Represents the CSS page-break-after property.
    /// </summary>
    public interface ICssPageBreakAfterProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected break mode.
        /// </summary>
        BreakMode State { get; }
    }
}
