namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Linq;
    using System;

    /// <summary>
    /// Represents the HTML tr element.
    /// </summary>
    sealed class HtmlTableRowElement : HtmlElement, IHtmlTableRowElement
    {
        #region Fields

        HtmlCollection<IHtmlTableCellElement> _cells;

        #endregion

        #region ctor

        public HtmlTableRowElement(Document owner, String prefix = null)
            : base(owner, Tags.Tr, prefix, NodeFlags.Special | NodeFlags.ImplicitelyClosed)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the alignment attribute.
        /// </summary>
        public HorizontalAlignment Align
        {
            get { return GetOwnAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Left); }
            set { SetOwnAttribute(AttributeNames.Align, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the vertical alignment attribute.
        /// </summary>
        public VerticalAlignment VAlign
        {
            get { return GetOwnAttribute(AttributeNames.Valign).ToEnum(VerticalAlignment.Middle); }
            set { SetOwnAttribute(AttributeNames.Valign, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the background color attribute.
        /// </summary>
        public String BgColor
        {
            get { return GetOwnAttribute(AttributeNames.BgColor); }
            set { SetOwnAttribute(AttributeNames.BgColor, value); }
        }

        /// <summary>
        /// Gets the assigned table cells.
        /// </summary>
        public IHtmlCollection<IHtmlTableCellElement> Cells
        {
            get { return _cells ?? (_cells = new HtmlCollection<IHtmlTableCellElement>(this, deep: false)); }
        }

        /// <summary>
        /// Gets the index in the logical order and not in document order. 
        /// </summary>
        public Int32 Index
        {
            get
            {
                var table = this.GetAncestor<IHtmlTableElement>();

                if (table != null)
                    return table.Rows.Index(this);

                return -1;
            }
        }

        /// <summary>
        /// Gets the index of this row, relative to the current section
        /// starting from 0.
        /// </summary>
        public Int32 IndexInSection
        {
            get
            {
                var parent = ParentElement as IHtmlTableSectionElement;

                if (parent != null)
                    return parent.Rows.Index(this);

                return Index; 
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Insert an empty TD cell into this row. If index is -1 or equal to
        /// the number of cells, the new cell is appended.
        /// </summary>
        /// <param name="index">
        /// The place to insert the cell, starting from 0.
        /// </param>
        /// <returns>The inserted table cell.</returns>
        public IHtmlTableCellElement InsertCellAt(Int32 index = -1)
        {
            var cell = Cells[index];
            var newCell = Owner.CreateElement(Tags.Td) as IHtmlTableCellElement;

            if (cell != null)
                InsertBefore(newCell, cell);
            else
                AppendChild(newCell);

            return newCell;
        }

        /// <summary>
        /// Deletes a cell from the current row.
        /// </summary>
        /// <param name="index">
        /// The index of the cell to delete, starting from 0. If the index is
        /// -1 the last cell in the row is deleted.
        /// </param>
        /// <returns>The current row.</returns>
        public void RemoveCellAt(Int32 index)
        {
            if (index == -1)
                index = Cells.Length - 1;

            var cell = Cells[index];

            if (cell != null)
                cell.Remove();
        }

        #endregion
    }
}
