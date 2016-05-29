namespace AngleSharp.Services
{
    using System;

    /// <summary>
    /// Defines methods to create a custom entity service.
    /// </summary>
    public interface IEntityProvider
    {
        /// <summary>
        /// Gets a symbol specified by its entity name.
        /// </summary>
        /// <param name="name">The name of the entity in the markup.</param>
        /// <returns>The string with the symbol or null.</returns>
        String GetSymbol(String name);
    }
}
