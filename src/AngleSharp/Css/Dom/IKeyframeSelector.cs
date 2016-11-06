namespace AngleSharp.Css.Dom
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a CSS key text.
    /// </summary>
    public interface IKeyframeSelector : IStyleFormattable
    {
        /// <summary>
        /// Gets an enumeration over all stops.
        /// </summary>
        IEnumerable<String> Stops { get; }

        /// <summary>
        /// Gets the text representation of the keyframe selector.
        /// </summary>
        String Text { get; }
    }
}
