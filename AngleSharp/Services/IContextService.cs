namespace AngleSharp.Services
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Defines methods to create or find browsing contexts.
    /// </summary>
    public interface IContextService : IService
    {
        /// <summary>
        /// Creates a new browsing context without any particular name.
        /// </summary>
        /// <param name="configuration">The configuration of the new context.</param>
        /// <param name="security">The sandboxing flag to use.</param>
        /// <returns>The created browsing context.</returns>
        IBrowsingContext Create(IConfiguration configuration, Sandboxes security);

        /// <summary>
        /// Creates a new browsing context with the given name,
        /// instructed by the specified document.
        /// </summary>
        /// <param name="configuration">The configuration of the new context.</param>
        /// <param name="name">The name of the new context.</param>
        /// <param name="creator">The creator of the context.</param>
        /// <param name="security">The sandboxing flag to use.</param>
        /// <returns>The created browsing context.</returns>
        IBrowsingContext Create(IConfiguration configuration, String name, IDocument creator, Sandboxes security);

        /// <summary>
        /// Tries to find a browsing context with the given name.
        /// </summary>
        /// <param name="name">The name of the context.</param>
        /// <returns>A context with the name, otherwise null.</returns>
        IBrowsingContext Find(String name);
    }
}
