namespace AngleSharp.DOM.Css
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CSS animation-direction property.
    /// </summary>
    public interface ICssAnimationDirectionProperty : ICssProperty
    {
        /// <summary>
        /// Gets an iteration over all defined directions.
        /// </summary>
        IEnumerable<AnimationDirection> Directions { get; }
    }
}
