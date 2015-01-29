namespace AngleSharp.Dom.Css
{
    using System;

    /// <summary>
    /// Represents the unicode-bidi CSS property.
    /// </summary>
    public interface ICssUnicodeBidiProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected unicode mode.
        /// </summary>
        UnicodeMode State { get; }
    }
}
