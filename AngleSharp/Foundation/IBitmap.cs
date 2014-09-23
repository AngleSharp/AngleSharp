namespace AngleSharp
{
    using System;

    /// <summary>
    /// Functionality for image computation.
    /// </summary>
    public interface IBitmap
    {
        /// <summary>
        /// Returns the CSS representation of the object.
        /// </summary>
        /// <returns>The CSS value string.</returns>
        String ToCss();
    }
}
