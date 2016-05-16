namespace AngleSharp
{
    using AngleSharp.Dom;
    using AngleSharp.Network;

    /// <summary>
    /// Represents the browsing context interface.
    /// </summary>
    public interface IBrowsingContext : IEventTarget
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
        /// Gets the configuration for the browsing context.
        /// </summary>
        IConfiguration Configuration { get; }

        /// <summary>
        /// Gets the assigned document loader, if any.
        /// </summary>
        IDocumentLoader Loader { get; }

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
        /// Fired when a parser is starting.
        /// </summary>
        event DomEventHandler Parsing;

        /// <summary>
        /// Fired when a parser is finished.
        /// </summary>
        event DomEventHandler Parsed;

        /// <summary>
        /// Fired when a parse error is encountered.
        /// </summary>
        event DomEventHandler ParseError;

        /// <summary>
        /// Fired when a requester is starting.
        /// </summary>
        event DomEventHandler Requesting;

        /// <summary>
        /// Fired when a requester is finished.
        /// </summary>
        event DomEventHandler Requested;
    }
}
