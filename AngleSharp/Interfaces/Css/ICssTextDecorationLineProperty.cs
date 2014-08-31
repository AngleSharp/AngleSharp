namespace AngleSharp.DOM.Css
{
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
}
