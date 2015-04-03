namespace AngleSharp.Services
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Defines the methods to access available style engines.
    /// </summary>
    public interface IStylingService : IService
    {
        /// <summary>
        /// Gets the registered engine for the provided mime-type.
        /// </summary>
        /// <param name="mimeType">The type of the engine.</param>
        /// <returns>The engine for the mime-type, if any.</returns>
        IStyleEngine GetEngine(String mimeType);
    }
}
