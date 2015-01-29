namespace AngleSharp.Dom
{
    /// <summary>
    /// The list of possible horizontal alignments.
    /// </summary>
    public enum HorizontalAlignment : ushort
    {
        /// <summary>
        /// The inline contents are aligned to the left edge of the line box.
        /// This is the default value for table data.
        /// </summary>
        Left,
        /// <summary>
        /// The inline contents are centered within the line box. This is
        /// the default value for table headers.
        /// </summary>
        Center,
        /// <summary>
        /// The inline contents are aligned to the right edge of the line box.
        /// </summary>
        Right,
        /// <summary>
        /// The text is justified. Text should line up their left and right
        /// edges to the left and right content edges of the paragraph.
        /// </summary>
        Justify
    }
}
