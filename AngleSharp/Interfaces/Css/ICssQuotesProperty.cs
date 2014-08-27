namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CSS quotes property.
    /// </summary>
    public interface ICssQuotesProperty : ICssProperty
    {
        /// <summary>
        /// Gets the enumeration with pairs of values for open-quote and
        /// close-quote.
        /// </summary>
        IEnumerable<Tuple<String, String>> Quotes { get; }
    }
}
