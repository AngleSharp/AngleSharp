namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;

    /// <summary>
    /// A list of possible quirks mode states.
    /// </summary>
    public enum QuirksMode : System.Byte
    {
        /// <summary>
        /// The quirks mode is deactivated.
        /// </summary>
        Off,
        /// <summary>
        /// The quirks mode is partly activated.
        /// </summary>
        Limited,
        /// <summary>
        /// The quirks mode is activated.
        /// </summary>
        [DomDescription("BackCompat")]
        On
    }
}
