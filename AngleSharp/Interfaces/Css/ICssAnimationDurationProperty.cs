namespace AngleSharp.DOM.Css
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CSS animation-duration property.
    /// </summary>
    public interface ICssAnimationDurationProperty : ICssProperty
    {
        /// <summary>
        /// Gets the durations for the animations.
        /// </summary>
        IEnumerable<Time> Durations { get; }
    }
}
