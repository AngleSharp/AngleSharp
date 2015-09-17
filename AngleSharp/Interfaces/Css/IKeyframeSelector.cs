namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css.Values;
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Represents a CSS key text.
    /// </summary>
    public interface IKeyframeSelector : ICssNode
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
