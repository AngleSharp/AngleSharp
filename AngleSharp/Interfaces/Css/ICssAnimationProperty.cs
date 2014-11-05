namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CSS animation-name property.
    /// </summary>
    public interface ICssAnimationNameProperty : ICssProperty
    {
        /// <summary>
        /// Gets the names of the animations to trigger.
        /// </summary>
        IEnumerable<String> Names { get; }
    }

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

    /// <summary>
    /// Represents the CSS animation-iteration-count property.
    /// </summary>
    public interface ICssAnimationIterationCountProperty : ICssProperty
    {
        /// <summary>
        /// Gets the iteration count of the covered animations.
        /// </summary>
        IEnumerable<Int32> Iterations { get; }
    }

    /// <summary>
    /// Represents the CSS anmation-timing-function property.
    /// </summary>
    public interface ICssAnimationTimingFunctionProperty : ICssProperty
    {
        /// <summary>
        /// Gets the enumeration over all timing functions.
        /// </summary>
        IEnumerable<TransitionFunction> TimingFunctions { get; }
    }

    /// <summary>
    /// Represents the CSS animation shorthand property.
    /// </summary>
    public interface ICssAnimationProperty : ICssProperty, ICssAnimationDelayProperty, ICssAnimationDirectionProperty, ICssAnimationDurationProperty, ICssAnimationFillModeProperty, ICssAnimationIterationCountProperty, ICssAnimationNameProperty, ICssAnimationTimingFunctionProperty, ICssAnimationPlayStateProperty
    {
    }
}
