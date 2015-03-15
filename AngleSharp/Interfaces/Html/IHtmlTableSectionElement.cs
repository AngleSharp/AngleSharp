namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represent the base of table section (tbody, thead, tfoot) elements.
    /// </summary>
    [DomName("HTMLTableSectionElement")]
    public interface IHtmlTableSectionElement : IHtmlElement
    {
        /// <summary>
        /// Gets the assigned table rows.
        /// </summary>
        [DomName("rows")]
        IHtmlCollection<IHtmlTableRowElement> Rows { get; }

        /// <summary>
        /// Inserts a row into this section. The new row is inserted immediately before the
        /// current indexth row in this section. If index is -1 or equal to the number of
        /// rows in this section, the new row is appended.
        /// </summary>
        /// <param name="index">
        /// The row number where to insert a new row. This index starts from 0 and is relative
        /// only to the rows contained inside this section, not all the rows in the table.
        /// </param>
        /// <returns>The inserted table row.</returns>
        [DomName("insertRow")]
        IHtmlTableRowElement InsertRowAt(Int32 index = -1);

        /// <summary>
        /// Deletes a row from this section.
        /// </summary>
        /// <param name="index">
        /// The index of the row to be deleted, or -1 to delete the last row.
        /// This index starts from 0 and is relative only to the rows contained
        /// inside this section, not all the rows in the table.
        /// </param>
        [DomName("deleteRow")]
        void RemoveRowAt(Int32 index);
    }
}
