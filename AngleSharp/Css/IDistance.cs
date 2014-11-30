namespace AngleSharp.Css
{
    using System;

    /// <summary>
    /// Functionality for length computation.
    /// </summary>
    public interface IDistance : ICssObject
    {
        /// <summary>
        /// Converts the value to pixels.
        /// </summary>
        /// <returns>The number of pixels.</returns>
        Single ToPixel();
    }
}
