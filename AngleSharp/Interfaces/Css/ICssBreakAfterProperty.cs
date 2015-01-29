namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// Represents the CSS break-after property.
    /// </summary>
    public interface ICssBreakAfterProperty  : ICssProperty
    {
        /// <summary>
        /// Gets the selected break mode.
        /// </summary>
        BreakMode State { get; }
    }
}
