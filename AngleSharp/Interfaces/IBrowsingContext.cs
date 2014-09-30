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
    }
}
