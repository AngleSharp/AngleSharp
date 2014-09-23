namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents the CSS max-width property.
    /// </summary>
    public interface ICssMaxWidthProperty : ICssProperty
    {
        /// <summary>
        /// Gets if a limit has been specified, otherwise the value is none.
        /// </summary>
        Boolean IsLimited { get; }

        /// <summary>
        /// Gets the specified max-width of the element. A percentage is calculated
        /// with respect to the width of the containing block.
        /// </summary>
        IDistance Limit { get; }
    }
}
