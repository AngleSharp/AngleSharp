namespace AngleSharp.Dom.Events
{
    using AngleSharp.Attributes;

    /// <summary>
    /// Enumeration with the various mouse wheel modes.
    /// </summary>
    public enum WheelMode
    {
        /// <summary>
        /// The unit of change is pixels.
        /// </summary>
        [DomName("DOM_DELTA_PIXEL")]
        Pixel = 0x0,
        /// <summary>
        /// The unit of change is lines.
        /// </summary>
        [DomName("DOM_DELTA_LINE")]
        Line = 0x1,
        /// <summary>
        /// The unit of change is pages.
        /// </summary>
        [DomName("DOM_DELTA_PAGE")]
        Page = 0x2
    }
}
