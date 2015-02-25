namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// An enumeration with possible overflow modes.
    /// </summary>
    public enum OverflowMode : ushort
    {
        /// <summary>
        /// The overflow-mode is determined by the renderer.
        /// </summary>
        Auto,
        /// <summary>
        /// The content is allowed to overflow.
        /// </summary>
        Visible,
        /// <summary>
        /// The content is cut to prevent overflowing.
        /// </summary>
        Hidden,
        /// <summary>
        /// The content can be scrolled.
        /// </summary>
        Scroll
    }
}
