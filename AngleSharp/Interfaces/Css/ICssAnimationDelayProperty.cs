namespace AngleSharp.DOM.Css
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CSS animation-delay property.
    /// </summary>
    public interface ICssAnimationDelayProperty : ICssProperty
    {
        /// <summary>
        /// Gets the delays for the animations.
        /// </summary>
        IEnumerable<Time> Delays { get; }
    }
}
