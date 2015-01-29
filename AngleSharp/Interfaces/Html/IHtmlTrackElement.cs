namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom.Media;
    using System;

    /// <summary>
    /// Represents the track HTML element.
    /// </summary>
    [DomName("HTMLTrackElement")]
    public interface IHtmlTrackElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets the kind of the track.
        /// </summary>
        [DomName("kind")]
        String Kind { get; set; }

        /// <summary>
        /// Gets or sets the media source.
        /// </summary>
        [DomName("src")]
        String Source { get; set; }

        /// <summary>
        /// Gets or sets the language of the source.
        /// </summary>
        [DomName("srclang")]
        String SourceLanguage { get; set; }

        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        [DomName("label")]
        String Label { get; set; }

        /// <summary>
        /// Gets or sets if given track is the default track.
        /// </summary>
        [DomName("default")]
        Boolean IsDefault { get; set; }

        /// <summary>
        /// Gets the ready state of the given track.
        /// </summary>
        [DomName("readyState")]
        TrackReadyState ReadyState { get; }

        /// <summary>
        /// Gets the associated text track.
        /// </summary>
        [DomName("track")]
        ITextTrack Track { get; }
    }
}
