namespace AngleSharp
{
    using System;

    /// <summary>
    /// Allows basic serialization.
    /// </summary>
    public interface IMarkupFormattable
    {
        /// <summary>
        /// Returns the (complete) HTML markup representation of the node.
        /// </summary>
        /// <returns>The source code snippet.</returns>
        String ToHtml();

        /// <summary>
        /// Returns the serialization of the node guided by the formatter.
        /// </summary>
        /// <param name="formatter">The formatter to use.</param>
        /// <returns>The source code snippet.</returns>
        String ToHtml(IMarkupFormatter formatter);
    }
}
