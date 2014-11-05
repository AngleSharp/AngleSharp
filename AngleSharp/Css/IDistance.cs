namespace AngleSharp
{
    using System;

    /// <summary>
    /// Functionality for length computation.
    /// </summary>
    public interface IDistance
    {
        /// <summary>
        /// Returns the CSS representation of the object.
        /// </summary>
        /// <returns>The CSS value string.</returns>
        String ToCss();

        /// <summary>
        /// Converts the value to pixels.
        /// </summary>
        /// <returns>The number of pixels.</returns>
        Single ToPixel();
    }
}
