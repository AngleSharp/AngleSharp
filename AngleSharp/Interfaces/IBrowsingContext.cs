namespace AngleSharp.DOM
{
    /// <summary>
    /// Represents the browsing context interface.
    /// </summary>
    public interface IBrowsingContext
    {
        /// <summary>
        /// Gets the current window proxy.
        /// </summary>
        IWindowProxy Current { get; }

        /// <summary>
        /// Gets the session history of the given browsing context.
        /// </summary>
        IHistory SessionHistory { get; }

        /// <summary>
        /// Gets the configuration for the browsing context.
        /// </summary>
        IConfiguration Configuration { get; }

        /// <summary>
        /// Gets the parent of the current context, if any.
        /// If a parent is available, then the current context
        /// contains only embedded documents.
        /// </summary>
        IBrowsingContext Parent { get; }

        /// <summary>
        /// Gets the document that created the current context, if any.
        /// The creator is the active document of the parent at the time
        /// of creation.
        /// </summary>
        IDocument Creator { get; }
    }
}
