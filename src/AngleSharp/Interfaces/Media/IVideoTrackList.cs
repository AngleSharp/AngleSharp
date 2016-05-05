namespace AngleSharp.Dom.Media
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a list of video tracks.
    /// </summary>
    [DomName("VideoTrackList")]
    public interface IVideoTrackList : IEventTarget, IEnumerable<IVideoTrack>
    {
        /// <summary>
        /// Gets the number of tracks.
        /// </summary>
        [DomName("length")]
        Int32 Length { get; }

        /// <summary>
        /// Gets the currently selected index.
        /// </summary>
        [DomName("selectedIndex")]
        Int32 SelectedIndex { get; }

        /// <summary>
        /// Gets the track at the given index.
        /// </summary>
        /// <param name="index">The 0-based track index.</param>
        /// <returns>The track at the position.</returns>
        [DomAccessor(Accessors.Getter)]
        IVideoTrack this[Int32 index] { get; }

        /// <summary>
        /// Gets the track with the specified id.
        /// </summary>
        /// <param name="id">The HTML id of the track.</param>
        /// <returns>The track with the given id, if any.</returns>
        [DomName("getTrackById")]
        IVideoTrack GetTrackById(String id);

        /// <summary>
        /// Event triggered after changing contents.
        /// </summary>
        [DomName("onchange")]
        event DomEventHandler Changed;

        /// <summary>
        /// Event triggered after adding a track.
        /// </summary>
        [DomName("onaddtrack")]
        event DomEventHandler TrackAdded;

        /// <summary>
        /// Event triggered after removing a track.
        /// </summary>
        [DomName("onremovetrack")]
        event DomEventHandler TrackRemoved;
    }
}
