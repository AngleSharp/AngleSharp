namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Css.Values;
    
    /// <summary>
    /// Represents a CSS key text.
    /// </summary>
    public interface IKeyframeSelector : IStyleFormattable
    {
        /// <summary>
        /// Gets an enumeration over all stops.
        /// </summary>
        IEnumerable<Percent> Stops { get; }

        /// <summary>
        /// Gets the text representation of the keyframe selector.
        /// </summary>
        String Text { get; }
    }
}
