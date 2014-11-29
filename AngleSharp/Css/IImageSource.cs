namespace AngleSharp.Css
{
    using System;

    /// <summary>
    /// Basic interface for all image sources
    /// (linear-gradient, radial-gradient, URL, solid, ...).
    /// </summary>
    public interface IImageSource
    {
        /// <summary>
        /// Returns the CSS representation of the object.
        /// </summary>
        /// <returns>The CSS value string.</returns>
        String ToCss();
    }
}
