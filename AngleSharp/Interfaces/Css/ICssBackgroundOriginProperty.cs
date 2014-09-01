namespace AngleSharp.DOM.Css
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CSS background-origin property.
    /// </summary>
    public interface ICssBackgroundOriginProperty : ICssProperty
    {
        /// <summary>
        /// Gets an enumeration with the desired origin settings.
        /// </summary>
        IEnumerable<BoxModel> Origins { get; }
    }
}
