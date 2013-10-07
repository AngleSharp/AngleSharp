namespace AngleSharp.DOM
{
    /// <summary>
    /// Enumeration with possible values for the adjacent position insertation.
    /// </summary>
    public enum AdjacentPosition
    {
        /// <summary>
        /// Before the element itself.
        /// </summary>
        BeforeBegin,
        /// <summary>
        /// Just inside the element, before its first child.
        /// </summary>
        AfterBegin,
        /// <summary>
        /// Just inside the element, after its last child.
        /// </summary>
        BeforeEnd,
        /// <summary>
        /// After the element itself.
        /// </summary>
        AfterEnd
    }
}
