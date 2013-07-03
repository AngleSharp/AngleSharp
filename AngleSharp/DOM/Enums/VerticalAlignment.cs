using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// The list of possible vertical alignments.
    /// </summary>
    public enum VerticalAlignment
    {
        /// <summary>
        /// Cell data is flush with the top of the cell.
        /// </summary>
        Top,
        /// <summary>
        /// Cell data is centered vertically within the cell. This is the default value.
        /// </summary>
        Middle,
        /// <summary>
        /// Cell data is flush with the bottom of the cell.
        /// </summary>
        Bottom,
        /// <summary>
        /// All cells in the same row as a cell whose valign attribute has this value should
        /// have their textual data positioned so that the first text line occurs on a baseline
        /// common to all cells in the row. This constraint does not apply to subsequent text
        /// lines in these cells.
        /// </summary>
        Baseline
    }
}
