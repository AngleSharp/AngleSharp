namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// All possible values for taking an element out of its normal flow.
    /// </summary>
    public enum Floating : ushort
    {
        /// <summary>
        /// Indicates that the element must not float.
        /// </summary>
        None,
        /// <summary>
        /// Indicates that the element must float on the left side of its containing block.
        /// </summary>
        Left,
        /// <summary>
        /// Indicates that the element must float on the right side of its containing block.
        /// </summary>
        Right
    }
}
