namespace AngleSharp.Media
{
    using AngleSharp.DOM.Media;

    /// <summary>
    /// Contains information about a sound file.
    /// </summary>
    public interface IAudioInfo : IResourceInfo
    {
        /// <summary>
        /// Gets the controller responsible for the media.
        /// </summary>
        IMediaController Controller { get; }
    }
}
