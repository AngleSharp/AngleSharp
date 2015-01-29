namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom.Media;
    using System;

    /// <summary>
    /// Represents the base for all HTML media elements.
    /// </summary>
    [DomName("HTMLMediaElement")]
    public interface IHtmlMediaElement : IHtmlElement, IMediaController
    {
        /// <summary>
        /// Gets or sets the media source.
        /// </summary>
        [DomName("src")]
        String Source { get; set; }

        /// <summary>
        /// Gets or sets the cross-origin attribute.
        /// </summary>
        [DomName("crossOrigin")]
        String CrossOrigin { get; set; }

        /// <summary>
        /// Gets or sets the preload attribute.
        /// </summary>
        [DomName("preload")]
        String Preload { get; set; }

        /// <summary>
        /// Gets or sets the id of the assigned media group.
        /// </summary>
        [DomName("mediaGroup")]
        String MediaGroup { get; set; }

        /// <summary>
        /// Gets the current network state.
        /// </summary>
        [DomName("networkState")]
        MediaNetworkState NetworkState { get; }

        /// <summary>
        /// Gets if seeking is currently active.
        /// </summary>
        [DomName("seeking")]
        Boolean IsSeeking { get; }

        /// <summary>
        /// Gets the current media source.
        /// </summary>
        [DomName("currentSrc")]
        String CurrentSource { get; }

        /// <summary>
        /// Gets the current media error, if any.
        /// </summary>
        [DomName("error")]
        IMediaError Error { get; }

        /// <summary>
        /// Gets the current media's controller, if any.
        /// </summary>
        [DomName("controller")]
        IMediaController Controller { get; }

        /// <summary>
        /// Gets if the media has ended.
        /// </summary>
        [DomName("ended")]
        Boolean IsEnded { get; }

        /// <summary>
        /// Gets or sets if the media is automatically played.
        /// </summary>
        [DomName("autoplay")]
        Boolean IsAutoplay { get; set; }

        /// <summary>
        /// Gets or sets if the media should loop.
        /// </summary>
        [DomName("loop")]
        Boolean IsLoop { get; set; }

        /// <summary>
        /// Gets or sets if the controls should be shown to the user.
        /// </summary>
        [DomName("controls")]
        Boolean IsShowingControls { get; set; }

        /// <summary>
        /// Gets or sets if the media is muted by default.
        /// </summary>
        [DomName("defaultMuted")]
        Boolean IsDefaultMuted { get; set; }

        /// <summary>
        /// Loads the currently assigned media source.
        /// </summary>
        [DomName("load")]
        void Load();

        /// <summary>
        /// Checks if the given type can be played.
        /// </summary>
        /// <param name="type">The type to check for.</param>
        /// <returns>One of the following values: probably, maybe or an empty string.</returns>
        [DomName("canPlayType")]
        String CanPlayType(String type);

        /// <summary>
        /// Gets the datetime when the download started.
        /// </summary>
        [DomName("startDate")]
        DateTime StartDate { get; }

        /// <summary>
        /// Gets a list of contained audio tracks.
        /// </summary>
        [DomName("audioTracks")]
        IAudioTrackList AudioTracks { get; }

        /// <summary>
        /// Gets a list of contained video tracks.
        /// </summary>
        [DomName("videoTracks")]
        IVideoTrackList VideoTracks { get; }

        /// <summary>
        /// Gets a list of contained text tracks.
        /// </summary>
        [DomName("textTracks")]
        ITextTrackList TextTracks { get; }

        /// <summary>
        /// Adds a new text track to the media element.
        /// </summary>
        /// <param name="kind">The kind of text track to create.</param>
        /// <param name="label">The optional label of the track.</param>
        /// <param name="language">The optional language of the track.</param>
        /// <returns>The freshly created text track.</returns>
        [DomName("addTextTrack")]
        ITextTrack AddTextTrack(String kind, String label = null, String language = null);
    }
}
