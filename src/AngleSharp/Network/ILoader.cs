namespace AngleSharp.Network
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the basic interface for all loaders.
    /// </summary>
    public interface ILoader
    {
        /// <summary>
        /// Gets the currently active downloads.
        /// </summary>
        /// <returns>The downloads in progress.</returns>
        IEnumerable<IDownload> GetDownloads();
    }
}
