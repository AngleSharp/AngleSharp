namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents the CSS word-spacing property.
    /// </summary>
    public interface ICssWordSpacingProperty : ICssProperty
    {
        /// <summary>
        /// Gets if normal inter-word space, as defined by the current
        /// font and/or the browser, is active.
        /// </summary>
        Boolean IsNormal { get; }

        /// <summary>
        /// Gets the defined custom spacing, if any.
        /// </summary>
        Length? Spacing { get; }
    }
}
