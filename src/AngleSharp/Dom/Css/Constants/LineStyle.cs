namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// An enumeration with all possible line styles.
    /// </summary>
    public enum LineStyle : ushort
    {
        /// <summary>
        /// No outline (outline-width is 0).
        /// </summary>
        None,
        /// <summary>
        /// Same as 'none', except in terms of border conflict resolution for table elements.
        /// </summary>
        Hidden,
        /// <summary>
        /// The outline is a series of dots.
        /// </summary>
        Dotted,
        /// <summary>
        /// The outline is a series of short line segments.
        /// </summary>
        Dashed,
        /// <summary>
        /// The outline is a single line.
        /// </summary>
        Solid,
        /// <summary>
        /// The outline is two single lines. The outline-width is the sum of the two lines and the space between them.
        /// </summary>
        Double,
        /// <summary>
        /// The outline looks as though it were carved into the canvas.
        /// </summary>
        Groove,
        /// <summary>
        /// The opposite of groove: the outline looks as though it were coming out of the canvas.
        /// </summary>
        Ridge,
        /// <summary>
        /// The outline makes the box look as though it were embeded in the canvas.
        /// </summary>
        Inset,
        /// <summary>
        /// The opposite of inset: the outline makes the box look as though it were coming out of the canvas.
        /// </summary>
        Outset
    }
}
