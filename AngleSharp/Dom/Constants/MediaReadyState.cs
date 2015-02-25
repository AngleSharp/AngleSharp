namespace AngleSharp.Dom.Media
{
    using AngleSharp.Attributes;

    /// <summary>
    /// An enumeration of media ready states.
    /// </summary>
    public enum MediaReadyState : ushort
    {
        /// <summary>
        /// No information is available.
        /// </summary>
        [DomName("HAVE_NOTHING")]
        Nothing = 0,
        /// <summary>
        /// Enough information obtained such that the duration of the
        /// resource is available.
        /// </summary>
        [DomName("HAVE_METADATA")]
        Metadata = 1,
        /// <summary>
        /// Data for immediate playback is available, but not enough
        /// to advance.
        /// </summary>
        [DomName("HAVE_CURRENT_DATA")]
        CurrentData = 2,
        /// <summary>
        /// Enough data for the current and further positions are 
        /// available.
        /// </summary>
        [DomName("HAVE_FUTURE_DATA")]
        FutureData = 3,
        /// <summary>
        /// All conditions are met and playback should immediately
        /// execute.
        /// </summary>
        [DomName("HAVE_ENOUGH_DATA")]
        EnoughData = 4
    }
}
