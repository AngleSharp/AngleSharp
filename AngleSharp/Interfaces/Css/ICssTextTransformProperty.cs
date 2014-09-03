namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS text-transform property.
    /// </summary>
    public interface ICssTextTransformProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected text transformation mode.
        /// </summary>
        TextTransform Transform { get; }
    }
}
