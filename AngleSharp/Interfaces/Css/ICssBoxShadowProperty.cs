namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css.Values;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CSS box-shadow property.
    /// </summary>
    public interface ICssBoxShadowProperty : ICssProperty
    {
        /// <summary>
        /// Gets an enumeration over all the set shadows.
        /// </summary>
        IEnumerable<Shadow> Shadows { get; }
    }
}
