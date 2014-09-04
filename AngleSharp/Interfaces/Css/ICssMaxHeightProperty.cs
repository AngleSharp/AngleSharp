namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents the CSS max-height property.
    /// </summary>
    public interface ICssMaxHeightProperty : ICssProperty
    {
        /// <summary>
        /// Gets if a limit has been specified, otherwise the value is none.
        /// </summary>
        Boolean IsLimited { get; }
    }
}
