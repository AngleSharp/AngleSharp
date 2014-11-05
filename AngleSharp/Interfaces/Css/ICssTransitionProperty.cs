namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;
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

    /// <summary>
    /// Represents the CSS transition-property property.
    /// </summary>
    public interface ICssTransitionPropertyProperty : ICssProperty
    {
        /// <summary>
        /// Gets the names of the selected properties.
        /// </summary>
        IEnumerable<String> Properties { get; }
    }

    /// <summary>
    /// Represents the CSS timing-function property.
    /// </summary>
    public interface ICssTransitionTimingFunctionProperty : ICssProperty
    {
        /// <summary>
        /// Gets the enumeration over all timing functions.
        /// </summary>
        IEnumerable<TransitionFunction> TimingFunctions { get; }
    }

    /// <summary>
    /// Represents the CSS transition shorthand property.
    /// </summary>
    public interface ICssTransitionProperty : ICssProperty, ICssTransitionDelayProperty, ICssTransitionDurationProperty, ICssTransitionPropertyProperty, ICssTransitionTimingFunctionProperty
    {
    }
}
