namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Represents the CSS letter-spacing property.
    /// </summary>
    public interface ICssLetterSpacingProperty : ICssProperty
    {
        /// <summary>
        /// Gets if the spacing is the normal spacing for the current font.
        /// </summary>
        Boolean IsNormal { get; }

        /// <summary>
        /// Gets the defined custom spacing, if any.
        /// </summary>
        Length? Spacing { get; }
    }
}
