namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS vertical-align property.
    /// </summary>
    public interface ICssVerticalAlignProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected vertical alignment mode.
        /// </summary>
        VerticalAlignment Align { get; }
    }
}
