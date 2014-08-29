namespace AngleSharp.DOM.Css
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CSS transition-duration property.
    /// </summary>
    public interface ICssTransitionDurationProperty : ICssProperty
    {
        /// <summary>
        /// Gets the durations for the transitions.
        /// </summary>
        IEnumerable<Time> Durations { get; }
    }
}
