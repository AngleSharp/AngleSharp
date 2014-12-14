namespace AngleSharp.Css.Values
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The common interface for all CSS gradients.
    /// </summary>
    public interface IGradient : IImageSource
    {
        /// <summary>
        /// Gets an enumeration of all stops.
        /// </summary>
        IEnumerable<GradientStop> Stops { get; }

        /// <summary>
        /// Gets if the gradient is repeating.
        /// </summary>
        Boolean IsRepeating { get; }
    }
}
