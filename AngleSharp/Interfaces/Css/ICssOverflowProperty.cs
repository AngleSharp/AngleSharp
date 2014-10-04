namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS overflow property.
    /// </summary>
    public interface ICssOverflowProperty : ICssProperty
    {
        /// <summary>
        /// Gets the desired overflow mode.
        /// </summary>
        OverflowMode State { get; }
    }
}
