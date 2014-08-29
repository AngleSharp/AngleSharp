namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS clear property.
    /// </summary>
    public interface ICssClearProperty : ICssProperty
    {
        /// <summary>
        /// Gets the value of the clear mode.
        /// </summary>
        ClearMode State { get; }
    }
}
