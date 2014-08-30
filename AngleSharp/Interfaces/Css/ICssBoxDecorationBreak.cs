namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents the CSS box-decoration-break property.
    /// </summary>
    public interface ICssBoxDecorationBreak : ICssProperty
    {
        /// <summary>
        /// Gets if each box is independently wrapped with the border
        /// and padding.
        /// </summary>
        Boolean IsCloned { get; }
    }
}
