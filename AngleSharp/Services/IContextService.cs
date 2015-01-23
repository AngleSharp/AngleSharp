namespace AngleSharp.Services
{
    using AngleSharp.DOM;
    using System;

    /// <summary>
    /// Defines methods to create or find browsing contexts.
    /// </summary>
    public interface IContextService : IService
    {
        /// <summary>
        /// Creates a new browsing context without any particular name.
        /// </summary>
        /// <returns>The created browsing context.</returns>
        IBrowsingContext Create();

        /// <summary>
        /// Creates a new browsing context with the given name,
        /// instructed by the specified document.
        /// </summary>
        /// <param name="name">The name of the new context.</param>
        /// <param name="creator">The creator of the context.</param>
        /// <returns>The created browsing context.</returns>
        IBrowsingContext Create(String name, IDocument creator);

        /// <summary>
        /// Tries to find a browsing context with the given name.
        /// </summary>
        /// <param name="name">The name of the context.</param>
        /// <returns>A context with the name, otherwise null.</returns>
        IBrowsingContext Find(String name);
    }
}
