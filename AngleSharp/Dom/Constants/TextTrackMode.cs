namespace AngleSharp.Dom.Media
{
    using AngleSharp.Attributes;

    /// <summary>
    /// The modes of a text track.
    /// </summary>
    [DomName("TextTrackMode")]
    public enum TextTrackMode
    {
        /// <summary>
        /// The text track is disabled.
        /// </summary>
        [DomName("disabled")]
        Disabled,
        /// <summary>
        /// The text track is hidden.
        /// </summary>
        [DomName("hidden")]
        Hidden,
        /// <summary>
        /// The text track is actually shown.
        /// </summary>
        [DomName("showing")]
        Showing
    }
}
