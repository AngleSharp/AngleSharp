namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS text-decoration-color property.
    /// </summary>
    public interface ICssTextDecorationColorProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected text-decoration color.
        /// </summary>
        Color Color { get; }
    }
}
