namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// Available device update frequencies.
    /// </summary>
    public enum UpdateFrequency
    {
        /// <summary>
        /// Once it has been rendered, the layout can no longer
        /// be updated. Example: documents printed on paper.
        /// </summary>
        None,
        /// <summary>
        /// The layout may change dynamically according to the
        /// usual rules of CSS, but the output device is not
        /// able to render or display changes quickly enough for
        /// them to be percieved as a smooth animation.
        /// Example: E-ink screens or severely under-powered
        /// devices.
        /// </summary>
        Slow,
        /// <summary>
        /// The layout may change dynamically according to the
        /// usual rules of CSS, and the output device is not
        /// unusually constrained in speed, so regularly-updating
        /// things like CSS Animations can be used.
        /// Example: computer screens.
        /// </summary>
        Normal
    }
}
