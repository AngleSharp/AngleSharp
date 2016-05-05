namespace AngleSharp.Dom.Media
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contains a list of text cues.
    /// </summary>
    [DomName("TextTrackCueList")]
    public interface ITextTrackCueList : IEnumerable<ITextTrackCue>
    {
        /// <summary>
        /// Gets the number of cues.
        /// </summary>
        [DomName("length")]
        Int32 Length { get; }

        /// <summary>
        /// Gets the cue at the given index.
        /// </summary>
        /// <param name="index">The 0-based cue index.</param>
        /// <returns>The cue at the position.</returns>
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
