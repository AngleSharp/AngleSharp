namespace AngleSharp.DOM.Css
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CSS background-clip property.
    /// </summary>
    public interface ICssBackgroundClipProperty : ICssProperty
    {
        /// <summary>
        /// Gets an enumeration with the desired clip settings.
        /// </summary>
        IEnumerable<BoxModel> Clips { get; }
    }
}
