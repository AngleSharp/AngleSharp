namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CSS counter-increment property.
    /// </summary>
    public interface ICssCounterIncrementProperty : ICssProperty
    {
        /// <summary>
        /// Gets the increment of the specified counter.
        /// </summary>
        /// <param name="counter">The name of the counter.</param>
        /// <returns>The increment of the counter.</returns>
        Int32 this[String counter] { get; }

        /// <summary>
        /// Gets an enumeration over all counters.
        /// </summary>
        IEnumerable<String> Counters { get; }
    }
}
