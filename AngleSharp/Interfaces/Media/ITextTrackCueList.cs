namespace AngleSharp.DOM.Media
{
    using System;

    /// <summary>
    /// Contains a list of text tracks.
    /// </summary>
    [DomName("TextTrackCueList")]
    public interface ITextTrackCueList
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
        ITextTrackCue this[Int32 index] { get; }

        /// <summary>
        /// Gets the cue with the specified id.
        /// </summary>
        /// <param name="id">The HTML id of the cue.</param>
        /// <returns>The cue with the given id, if any.</returns>
        [DomName("getCueById")]
        IVideoTrack GetCueById(String id);
    }
}
