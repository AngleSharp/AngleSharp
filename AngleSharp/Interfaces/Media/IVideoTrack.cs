namespace AngleSharp.Dom.Media
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents an video track.
    /// </summary>
    [DomName("VideoTrack")]
    public interface IVideoTrack
    {
        /// <summary>
        /// Gets the id of the video track.
        /// </summary>
        [DomName("id")]
        String Id { get; }

        /// <summary>
        /// Gets the kind of video track.
        /// </summary>
        [DomName("kind")]
        String Kind { get; }

        /// <summary>
        /// Gets the label of the track.
        /// </summary>
        [DomName("label")]
        String Label { get; }

        /// <summary>
        /// Gets the language of the track.
        /// </summary>
        [DomName("language")]
        String Language { get; }

        /// <summary>
        /// Gets or sets if the track is selected.
        /// </summary>
        [DomName("selected")]
        Boolean IsSelected { get; set; }
    }
}
