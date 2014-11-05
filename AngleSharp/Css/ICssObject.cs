namespace AngleSharp
{
    using System;

    /// <summary>
    /// Represents an object that can be represented in CSS code.
    /// </summary>
    interface ICssObject
    {
        /// <summary>
        /// Returns the CSS code representation of the object.
        /// </summary>
        /// <returns>The source code snippet.</returns>
        String ToCss();
    }
}
