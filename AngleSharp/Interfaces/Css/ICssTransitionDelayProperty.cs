namespace AngleSharp.DOM.Css
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CSS transition-delay property.
    /// </summary>
    public interface ICssTransitionDelayProperty : ICssProperty
    {
        /// <summary>
        /// Gets the delays for the transitions.
        /// </summary>
        IEnumerable<Time> Delays { get; }
    }
}
