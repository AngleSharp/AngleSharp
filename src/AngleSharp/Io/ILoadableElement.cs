namespace AngleSharp.Io
{
    using AngleSharp.Attributes;

    /// <summary>
    /// The interface implemented by elements that may load resources.
    /// </summary>
    [DomNoInterfaceObject]
    public interface ILoadableElement
    {
        /// <summary>
        /// Gets the current download or resource, if any.
        /// </summary>
        /// <remarks>
        /// The download signals when the resource has been fetched. Its response belongs to the
        /// element's request processor, which disposes it right after processing, so its content
        /// and headers must not be read from here. To get at the content of a loaded resource,
        /// register an <c>IResourceService&lt;T&gt;</c> for its content type, which receives the
        /// response while it is still open.
        /// </remarks>
        IDownload? CurrentDownload { get; }
    }
}
