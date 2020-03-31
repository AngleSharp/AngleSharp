namespace AngleSharp
{
    using AngleSharp.Browser;
    using AngleSharp.Browser.Dom;
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the browsing context interface.
    /// </summary>
    public interface IBrowsingContext : IEventTarget, IDisposable
    {
        /// <summary>
        /// Gets the current window proxy.
        /// </summary>
        IWindow Current { get; }

        /// <summary>
        /// Gets or sets the currently active document.
        /// </summary>
        IDocument Active { get; set; }

        /// <summary>
        /// Gets the session history of the given browsing context.
        /// </summary>
        IHistory SessionHistory { get; }

        /// <summary>
        /// Gets the sandboxing flag of the context.
        /// </summary>
        Sandboxes Security { get; }

        /// <summary>
        /// Gets the parent of the current context, if any. If a parent is
        /// available, then the current context contains only embedded
        /// documents.
        /// </summary>
        IBrowsingContext Parent { get; }

        /// <summary>
        /// Gets the document that created the current context, if any. The
        /// creator is the active document of the parent at the time of
        /// creation.
        /// </summary>
        IDocument Creator { get; }

        /// <summary>
        /// Gets the original services for the browsing context.
        /// </summary>
        IEnumerable<Object> OriginalServices { get; }

        /// <summary>
        /// Gets an instance of the given service.
        /// </summary>
        /// <typeparam name="T">The type of service to resolve.</typeparam>
        /// <returns>The instance of the service or null.</returns>
        T GetService<T>() where T : class;

        /// <summary>
        /// Gets all registered instances of the given service.
        /// </summary>
        /// <typeparam name="T">The type of service to resolve.</typeparam>
        /// <returns>An enumerable with all service instances.</returns>
        IEnumerable<T> GetServices<T>() where T : class;

        /// <summary>
        /// Creates a new browsing context with the given name, instructed by
        /// the specified document.
        /// </summary>
        /// <param name="name">The name of the new context.</param>
        /// <param name="security">The sandboxing flag to use.</param>
        /// <returns>The created browsing context.</returns>
        IBrowsingContext CreateChild(String name, Sandboxes security);

        /// <summary>
        /// Tries to find a browsing context with the given name.
        /// </summary>
        /// <param name="name">The name of the context.</param>
        /// <returns>A context with the name, otherwise null.</returns>
        IBrowsingContext FindChild(String name);
    }
}
