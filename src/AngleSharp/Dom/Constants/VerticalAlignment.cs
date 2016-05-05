namespace AngleSharp.Dom
{
    /// <summary>
    /// The list of possible vertical alignments (extended by CSS).
    /// </summary>
    public enum VerticalAlignment : ushort
    {
        /// <summary>
        /// Aligns the baseline of the element with the baseline of its parent.
        /// The baseline of some replaced elements, like textarea is not specified
        /// by the HTML specification, meaning that their behavior with this keyword
        /// may change from one browser to the other.
        /// </summary>
        Baseline,
        /// <summary>
        /// Aligns the baseline of the element with the subscript-baseline
        /// of its parent.
        /// </summary>
        Sub,
        /// <summary>
        /// Aligns the baseline of the element with the superscript-baseline
        /// of its parent.
        /// </summary>
        Super,
        /// <summary>
        /// Aligns the top of the element with the top of the parent
        /// element's font.
        /// </summary>
        TextTop,
        /// <summary>
        /// Aligns the bottom of the element with the bottom of the parent
        /// element's font.
        /// </summary>
        TextBottom,
        /// <summary>
        /// Aligns the middle of the element with the middle of lowercase
        /// letters in the parent.
        /// </summary>
        Middle,
        /// <summary>
        /// Align the top of the element and its descendants with the top
        /// of the entire line.
        /// </summary>
        Top,
        /// <summary>
        /// Align the bottom of the element and its descendants with the
        /// bottom of the entire line.
        /// </summary>
        Bottom,
    }
}
