namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CSS counter-reset property.
    /// </summary>
    public interface ICssCounterResetProperty : ICssProperty
    {
        /// <summary>
        /// Gets the reset-value of the specified counter.
        /// </summary>
        /// <param name="counter">The name of the counter.</param>
        /// <returns>The reset-value of the counter.</returns>
        Int32 this[String counter] { get; }

        /// <summary>
        /// Gets an enumeration over all counters.
        /// </summary>
        IEnumerable<String> Counters { get; }
    }
}
