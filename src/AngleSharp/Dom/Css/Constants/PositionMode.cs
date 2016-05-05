namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// An enumeration with possible position modes.
    /// </summary>
    public enum PositionMode : ushort
    {
        /// <summary>
        /// The position is determined by the renderer.
        /// </summary>
        Static,
        /// <summary>
        /// The position is relative to its determined one.
        /// </summary>
        Relative,
        /// <summary>
        /// The position is relative to the upper drawing context.
        /// </summary>
        Absolute,
        /// <summary>
        /// The position is relative to the top drawing context.
        /// </summary>
        Fixed,
        /// <summary>
        /// The position is either fixed or static depending on the viewport.
        /// </summary>
        Sticky
    }
}
