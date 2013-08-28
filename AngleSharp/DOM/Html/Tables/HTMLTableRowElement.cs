using AngleSharp.DOM.Collections;
using AngleSharp.DOM.Css;
using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML tr element.
    /// </summary>
    [DOM("HTMLTableRowElement")]
    public sealed class HTMLTableRowElement : HTMLElement, IImplClosed
    {
        #region Members

        HTMLLiveCollection<HTMLTableCellElement> _cells;

        #endregion

        #region ctor

        internal HTMLTableRowElement()
        {
            _name = Tags.TR;
            _cells = new HTMLLiveCollection<HTMLTableCellElement>(this);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the alignment attribute.
        /// </summary>
        [DOM("align")]
        public HorizontalAlignment Align
        {
            get { return ToEnum(GetAttribute("align"), HorizontalAlignment.Left); }
            set { SetAttribute("align", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the vertical alignment attribute.
        /// </summary>
        [DOM("vAlign")]
        public VerticalAlignment VAlign
        {
            get { return ToEnum(GetAttribute("valign"), VerticalAlignment.Middle); }
            set { SetAttribute("valign", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the background color attribute.
        /// </summary>
        [DOM("bgColor")]
        public String BgColor
        {
            get { return GetAttribute("bgcolor"); }
            set { SetAttribute("bgcolor", value); }
        }

        /// <summary>
        /// Gets the assigned table cells.
        /// </summary>
        [DOM("cells")]
        public HTMLCollection Cells
        {
            get { return _cells; }
        }

        /// <summary>
        /// Gets the index in the logical order and not in document order. 
        /// </summary>
        [DOM("rowIndex")]
        public Int32 RowIndex
        {
            get
            {
                var parent = ParentElement;

                while (parent != null && !(parent is HTMLTableElement))
                    parent = parent.ParentElement;

                if (parent is HTMLTableElement)
                    return ((HTMLTableElement)parent).Rows.IndexOf(this);

                return 0;
            }
        }

        /// <summary>
        /// Gets the index of this row, relative to the current section starting from 0.
        /// </summary>
        [DOM("sectionRowIndex")]
        public Int32 SectionRowIndex
        {
            get
            {
                var parent = ParentElement;

                while (parent != null && !(parent is HTMLTableSectionElement))
                    parent = parent.ParentElement;

                if (parent is HTMLTableSectionElement)
                    return ((HTMLTableSectionElement)parent).Rows.IndexOf(this);

                return 0; 
            }
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Insert an empty TD cell into this row. If index is -1 or equal to the number
        /// of cells, the new cell is appended.
        /// </summary>
        /// <param name="index">The place to insert the cell, starting from 0.</param>
        /// <returns>The inserted table cell.</returns>
        [DOM("insertCell")]
        public HTMLTableCellElement InsertCell(Int32 index)
        {
            var cell = _cells[index];
            var newCell = OwnerDocument.CreateElement(Tags.TD) as HTMLTableCellElement;

            if (cell != null)
                InsertBefore(newCell, cell);
            else
                AppendChild(newCell);

            return newCell;
        }

        /// <summary>
        /// Deletes a cell from the current row.
        /// </summary>
        /// <param name="index">The index of the cell to delete, starting from 0. If the index is
        /// -1 the last cell in the row is deleted.</param>
        /// <returns>The current row.</returns>
        [DOM("deleteCell")]
        public HTMLTableRowElement DeleteCell(Int32 index)
        {
            if (index == -1)
                index = _cells.Length - 1;

            var cell = _cells[index];

            if (cell != null)
                cell.Remove();

            return this;
        }

        #endregion
    }
}
