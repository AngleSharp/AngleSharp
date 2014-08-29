namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CSS font-family property.
    /// </summary>
    public interface ICssFontFamilyProperty : ICssProperty
    {
        /// <summary>
        /// Gets an enumeration over all font names.
        /// </summary>
        IEnumerable<String> Families { get; }
    }
}
