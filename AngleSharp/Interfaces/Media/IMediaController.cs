namespace AngleSharp.DOM.Media
{
    using System;

    /// <summary>
    /// The MediaController for controlling a media.
    /// </summary>
    [DomName("MediaController")]
    public interface IMediaController
    {
        /// <summary>
        /// Gets the currently buffered time range.
        /// </summary>
        [DomName("buffered")]
        ITimeRanges BufferedTime { get; }

        /// <summary>
        /// Gets the currently seekable time range.
        /// </summary>
        [DomName("seekable")]
        ITimeRanges SeekableTime { get; }

        /// <summary>
        /// Gets the currently played time range.
        /// </summary>
        [DomName("played")]
        ITimeRanges PlayedTime { get; }

        /// <summary>
        /// Gets the duration of the controlled media.
        /// </summary>
        [DomName("duration")]
        Double Duration { get; }

        /// <summary>
        /// Gets the current time of the controlled media.
        /// </summary>
        [DomName("currentTime")]
        Double CurrentTime { get; }

        /// <summary>
        /// Gets or sets the default playback rate.
        /// </summary>
        [DomName("defaultPlaybackRate")]
        Double DefaultPlaybackRate { get; set; }

        /// <summary>
        /// Gets or sets the current playback rate.
        /// </summary>
        [DomName("playbackRate")]
        Double PlaybackRate { get; set; }

        /// <summary>
        /// Gets or sets the volume of the controlled media.
        /// </summary>
        [DomName("volume")]
        Double Volume { get; set; }

        /// <summary>
        /// Gets or sets if the controlled media is muted.
        /// </summary>
        [DomName("muted")]
        Boolean IsMuted { get; set; }

        /// <summary>
        /// Gets if the media is currently paused.
        /// </summary>
        [DomName("paused")]
        Boolean IsPaused { get; }

        /// <summary>
        /// Plays the underlying media.
        /// </summary>
        [DomName("play")]
        void Play();

        /// <summary>
        /// Pauses the underlying media.
        /// </summary>
        [DomName("pause")]
        void Pause();

        /// <summary>
        /// Event triggered after being emptied.
        /// </summary>
        [DomName("onemptied")]
        event EventListener Emptied;

        /// <summary>
        /// Event triggered after the meta data has been received.
        /// </summary>
        [DomName("onloadedmetadata")]
        event EventListener LoadedMetadata;

        /// <summary>
        /// Event triggered after the data has been loaded.
        /// </summary>
        [DomName("onloadeddata")]
        event EventListener LoadedData;

        /// <summary>
        /// Event triggered when the media can be played.
        /// </summary>
        [DomName("oncanplay")]
        event EventListener CanPlay;

        /// <summary>
        /// Event triggered when the media can be fully played.
        /// </summary>
        [DomName("oncanplaythrough")]
        event EventListener CanPlayThrough;

        /// <summary>
        /// Event triggered after the media ended.
        /// </summary>
        [DomName("onended")]
        event EventListener Ended;

        /// <summary>
        /// Event triggered when waiting for input.
        /// </summary>
        [DomName("onwaiting")]
        event EventListener Waiting;

        /// <summary>
        /// Event triggered when the media cursor changed.
        /// </summary>
        [DomName("ondurationchange")]
        event EventListener DurationChanged;

        /// <summary>
        /// Event triggered after the time updated.
        /// </summary>
        [DomName("ontimeupdate")]
        event EventListener TimeUpdated;

        /// <summary>
        /// Event triggered after the media paused.
        /// </summary>
        [DomName("onpause")]
        event EventListener Paused;

        /// <summary>
        /// Event triggered after the media started.
        /// </summary>
        [DomName("onplay")]
        event EventListener Played;

        /// <summary>
        /// Event triggered before the media started.
        /// </summary>
        [DomName("onplaying")]
        event EventListener Playing;

        /// <summary>
        /// Event triggered after the rate changed.
        /// </summary>
        [DomName("onratechange")]
        event EventListener RateChanged;

        /// <summary>
        /// Event triggered after the volume changed.
        /// </summary>
        [DomName("onvolumechange")]
        event EventListener VolumeChanged;
    }
}
