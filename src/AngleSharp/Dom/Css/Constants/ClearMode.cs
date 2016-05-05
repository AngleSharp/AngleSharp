namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// The enumeration with all possible clear modes.
    /// </summary>
    public enum ClearMode : ushort
    {
        /// <summary>
        /// The element is not moved down to clear past floating elements.
        /// </summary>
        None,
        /// <summary>
        /// The element is moved down to clear past left floats.
        /// </summary>
        Left,
        /// <summary>
        /// The element is moved down to clear past right floats.
        /// </summary>
        Right,
        /// <summary>
        /// The element is moved down to clear past both left and right floats.
        /// </summary>
        Both
    }
}
