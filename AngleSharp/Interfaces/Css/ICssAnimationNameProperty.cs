namespace AngleSharp.DOM.Css
{
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
}
