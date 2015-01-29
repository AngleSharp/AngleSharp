namespace AngleSharp.Services
{
    using AngleSharp.Dom.Media;
    using System;

    /// <summary>
    /// Represents a service for creating rendering contexts.
    /// </summary>
    public interface IRenderingService : IService
    {
        /// <summary>
        /// Checks if the given context is supported.
        /// </summary>
        /// <param name="contextId">The ID of the context.</param>
        /// <returns>True if the context is supported, otherwise false.</returns>
        Boolean IsSupportingContext(String contextId);

        /// <summary>
        /// Creates the given context or returns null, if this is not possible.
        /// </summary>
        /// <param name="contextId">The ID of the context.</param>
        /// <returns>The instance of the rendering context.</returns>
        IRenderingContext CreateContext(String contextId);
    }
}
