namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;

    /// <summary>
    /// List of possible shadow root mode states.
    /// </summary>
    [DomName("ShadowRootMode")]
    [DomLiterals]
    public enum ShadowRootMode : byte
    {
        /// <summary>
        /// Specifies open encapsulation mode.
        /// </summary>
        [DomName("open")]
        Open = 0,
        /// <summary>
        /// Specifies closed encapsulation mode.
        /// </summary>
        [DomName("closed")]
        Closed = 1
    }
}
