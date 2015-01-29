namespace AngleSharp.Services.Media
{
    using AngleSharp.Dom.Media;

    /// <summary>
    /// Contains information about a media resource.
    /// </summary>
    public interface IMediaInfo : IResourceInfo
    {
        /// <summary>
        /// Gets the controller responsible for the media.
        /// </summary>
        IMediaController Controller { get; }
    }
}
