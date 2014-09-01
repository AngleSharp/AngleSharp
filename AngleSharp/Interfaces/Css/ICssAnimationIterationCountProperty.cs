namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CSS animation-iteration-count property.
    /// </summary>
    public interface ICssAnimationIterationCountProperty : ICssProperty
    {
        /// <summary>
        /// Gets the iteration count of the covered animations.
        /// </summary>
        IEnumerable<Single> Iterations { get; }
    }
}
