namespace AngleSharp.Dom
{
    using System;

    /// <summary>
    /// Defines methods to create a custom reverse entity service.
    /// </summary>
    public interface IReverseEntityProvider
    {
        /// <summary>
        /// Gets the name of a symbol specified by its value.
        /// </summary>
        /// <param name="symbol">The symbol's value.</param>
        /// <returns>The name of the symbol or null.</returns>
        String? GetName(String symbol);
    }
}
