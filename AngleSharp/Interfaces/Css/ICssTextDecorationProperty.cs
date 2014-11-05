namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CSS text-decoration-line property.
    /// </summary>
    public interface ICssTextDecorationLineProperty : ICssProperty
    {
        /// <summary>
        /// Gets the enumeration over all selected styles
        /// for text decoration lines.
        /// </summary>
        IEnumerable<TextDecorationLine> Line { get; }
    }

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

    /// <summary>
    /// Represents the CSS text-decoration shorthand property.
    /// </summary>
    public interface ICssTextDecorationProperty : ICssProperty, ICssTextDecorationColorProperty, ICssTextDecorationLineProperty, ICssTextDecorationStyleProperty
    {
    }
}
