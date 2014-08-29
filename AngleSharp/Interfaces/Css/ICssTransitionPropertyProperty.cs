namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

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
}
