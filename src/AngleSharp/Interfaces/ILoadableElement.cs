namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Network;

    /// <summary>
    /// The interface implemented by elements that may load resources.
    /// </summary>
    [DomNoInterfaceObject]
    public interface ILoadableElement
    {
        /// <summary>
        /// Gets the current download or resource, if any.
        /// </summary>
        IDownload CurrentDownload { get; }
    }
}
