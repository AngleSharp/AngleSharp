namespace AngleSharp.Network
{
    using AngleSharp.Dom;

    /// <summary>
    /// Basic contract for a currently active download.
    /// </summary>
    public interface IDownload : ICancellable<IResponse>
    {
        /// <summary>
        /// Gets the target of the download.
        /// </summary>
        Url Target { get; }

        /// <summary>
        /// Gets the originator of the download, if any.
        /// </summary>
        INode Originator { get; }
    }
}
