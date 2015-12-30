namespace AngleSharp
{
    using System;

    /// <summary>
    /// Allows basic serialization.
    /// </summary>
    public interface IStyleFormattable
    {
        /// <summary>
        /// Returns the serialization of the node guided by the formatter.
        /// </summary>
        /// <param name="formatter">The formatter to use.</param>
        /// <returns>The source code snippet.</returns>
        String ToCss(IStyleFormatter formatter);
    }
}
