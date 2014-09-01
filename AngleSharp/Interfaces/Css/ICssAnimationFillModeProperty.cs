namespace AngleSharp.DOM.Css
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CSS animation-fill-mode property.
    /// </summary>
    public interface ICssAnimationFillModeProperty : ICssProperty
    {
        /// <summary>
        /// Gets an iteration over all defined fill modes.
        /// </summary>
        IEnumerable<AnimationFillStyle> FillModes { get; }
    }
}
