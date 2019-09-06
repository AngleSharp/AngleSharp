namespace AngleSharp.Html.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents the tr HTML element.
    /// </summary>
    [DomName("HTMLTableRowElement")]
    public interface IHtmlTableRowElement : IHtmlElement
    {
        /// <summary>
        /// Gets the index in the logical order and not in document order. 
        /// </summary>
        [DomName("rowIndex")]
        Int32 Index { get; }

        /// <summary>
        /// Gets the index of this row, relative to the current section starting
        /// from 0.
        /// </summary>
        [DomName("sectionRowIndex")]
        Int32 IndexInSection { get; }

        /// <summary>
        /// Gets the assigned table cells.
        /// </summary>
        [DomName("cells")]
        IHtmlCollection<IHtmlTableCellElement> Cells { get; }

        /// <summary>
        /// Insert an empty TD or TH cell into this row. If index is -1 or equal to
        /// the number of cells, the new cell is appended.
        /// </summary>
        /// <param name="index">
        /// [Optional] The place to insert the cell, starting from 0. A negative
        /// value indicates that the cell should be appended to the row.
        /// </param>
        /// <param name="tableCellKind">
        /// [Optional] The kind of table cell to insert.
        /// </param>
        /// <returns>The inserted table cell.</returns>
        [DomName("insertCell")]
        IHtmlTableCellElement InsertCellAt(Int32 index = -1, TableCellKind tableCellKind = TableCellKind.Td);

        /// <summary>
        /// Deletes a cell from the current row.
        /// </summary>
        /// <param name="index">
        /// The index of the cell to delete, starting from 0. If the index is
        /// -1 the last cell in the row is deleted.
        /// </param>
        /// <returns>The current row.</returns>
        [DomName("deleteCell")]
        void RemoveCellAt(Int32 index);
    }
}
