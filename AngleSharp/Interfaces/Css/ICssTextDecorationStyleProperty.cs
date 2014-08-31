namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS text-decoration-style property.
    /// </summary>
    public interface ICssTextDecorationStyleProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected decoration style.
        /// </summary>
        TextDecorationStyle DecorationStyle { get; }
    }
}
