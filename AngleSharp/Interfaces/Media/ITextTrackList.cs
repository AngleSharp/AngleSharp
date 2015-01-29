namespace AngleSharp.Dom.Media
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a list of text tracks.
    /// </summary>
    [DomName("TextTrackList")]
    public interface ITextTrackList : IEventTarget, IEnumerable<ITextTrack>
    {
        /// <summary>
        /// Gets the number of tracks.
        /// </summary>
        [DomName("length")]
        Int32 Length { get; }

        /// <summary>
        /// Gets the track at the given index.
        /// </summary>
        /// <param name="index">The 0-based track index.</param>
        /// <returns>The track at the position.</returns>
        [DomAccessor(Accessors.Getter)]
        ITextTrack this[Int32 index] { get; }

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
