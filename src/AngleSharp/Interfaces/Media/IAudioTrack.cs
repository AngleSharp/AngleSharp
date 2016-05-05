namespace AngleSharp.Dom.Media
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents an audio track.
    /// </summary>
    [DomName("AudioTrack")]
    public interface IAudioTrack
    {
        /// <summary>
        /// Gets the id of the audio track.
        /// </summary>
        [DomName("id")]
        String Id { get; }

        /// <summary>
        /// Gets the kind of audio track.
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
        /// Gets or sets if the track is enabled.
        /// </summary>
        [DomName("enabled")]
        Boolean IsEnabled { get; set; }
    }
}
