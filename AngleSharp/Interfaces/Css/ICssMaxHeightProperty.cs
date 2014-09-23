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

        /// <summary>
        /// Gets the specified max-height of the element. A percentage is calculated
        /// with respect to the height of the containing block. If the height of the
        /// containing block is not specified explicitly, the percentage value is
        /// treated as none.
        /// </summary>
        IDistance Limit { get; }
    }
}
