﻿namespace AngleSharp.Html.Dom
{
    using AngleSharp.Attributes;

    /// <summary>
    /// An enumeration with track ready state values.
    /// </summary>
    [DomName("HTMLTrackElement")]
    public enum TrackReadyState : byte
    {
        /// <summary>
        /// Not initialized yet.
        /// </summary>
        [DomName("NONE")]
        None = 0,
        /// <summary>
        /// Currently loading.
        /// </summary>
        [DomName("LOADING")]
        Loading = 1,
        /// <summary>
        /// Loading finished.
        /// </summary>
        [DomName("LOADED")]
        Loaded = 2,
        /// <summary>
        /// An error occured.
        /// </summary>
        [DomName("ERROR")]
        Error = 3
    }
}