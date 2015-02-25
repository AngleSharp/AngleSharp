namespace AngleSharp.Dom.Media
{
    using AngleSharp.Attributes;

    /// <summary>
    /// An enumeration over the various playback states.
    /// </summary>
    [DomName("MediaControllerPlaybackState")]
    public enum MediaControllerPlaybackState
    {
        /// <summary>
        /// Waiting for the media to be ready.
        /// </summary>
        [DomName("waiting")]
        Waiting,
        /// <summary>
        /// Playing the current media.
        /// </summary>
        [DomName("playing")]
        Playing,
        /// <summary>
        /// The media has already finished playing.
        /// </summary>
        [DomName("ended")]
        Ended
    }
}
