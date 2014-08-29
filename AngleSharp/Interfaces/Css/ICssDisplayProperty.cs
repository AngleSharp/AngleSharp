namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS display property.
    /// </summary>
    public interface ICssDisplayProperty : ICssProperty
    {
        /// <summary>
        /// Gets the value of the display mode.
        /// </summary>
        DisplayMode State { get; }
    }
}
