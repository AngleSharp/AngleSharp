namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Css;
    using System;

    /// <summary>
    /// Represents the HTML tr element.
    /// </summary>
    sealed class HTMLTableRowElement : HTMLElement, IHtmlTableRowElement
    {
        #region Fields

        readonly HtmlCollection<HTMLTableCellElement> _cells;

        #endregion

        #region ctor

        internal HTMLTableRowElement()
            : base(Tags.Tr, NodeFlags.Special | NodeFlags.ImplicitelyClosed)
        {
            _cells = new HtmlCollection<HTMLTableCellElement>(this);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the alignment attribute.
        /// </summary>
        public HorizontalAlignment Align
        {
            get { return GetAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Left); }
            set { SetAttribute(AttributeNames.Align, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the vertical alignment attribute.
        /// </summary>
        public VerticalAlignment VAlign
        {
            get { return GetAttribute(AttributeNames.Valign).ToEnum(VerticalAlignment.Middle); }
            set { SetAttribute(AttributeNames.Valign, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the background color attribute.
        /// </summary>
        public String BgColor
        {
            get { return GetAttribute(AttributeNames.BgColor); }
            set { SetAttribute(AttributeNames.BgColor, value); }
        }

        /// <summary>
        /// Gets the assigned table cells.
        /// </summary>
        public IHtmlCollection Cells
        {
            get { return _cells; }
        }

        /// <summary>
        /// Gets the index in the logical order and not in document order. 
        /// </summary>
        public Int32 Index
        {
            get
            {
                var parent = ParentElement;

                while (parent != null && !(parent is HTMLTableElement))
                    parent = parent.ParentElement;

                var table = parent as IHtmlTableElement;

                if (table != null)
                    return table.Rows.Index(this);

                return 0;
            }
        }

        /// <summary>
        /// Gets the index of this row, relative to the current section starting from 0.
        /// </summary>
        public Int32 IndexInSection
        {
            get
            {
                var parent = ParentElement;

                while (parent != null && !(parent is IHtmlTableSectionElement))
                    parent = parent.ParentElement;

                var section = parent as IHtmlTableSectionElement;

                if (section != null)
                    return section.Rows.Index(this);

                return 0; 
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Insert an empty TD cell into this row. If index is -1 or equal to the number
        /// of cells, the new cell is appended.
        /// </summary>
        /// <param name="index">The place to insert the cell, starting from 0.</param>
        /// <returns>The inserted table cell.</returns>
        public IHtmlElement InsertCellAt(Int32 index = -1)
        {
            var cell = _cells[index];
            var newCell = Owner.CreateElement(Tags.Td) as HTMLTableCellElement;

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
        public void RemoveCellAt(Int32 index)
        {
            if (index == -1)
                index = _cells.Length - 1;

            var cell = _cells[index];

            if (cell != null)
                cell.Remove();
        }

        #endregion
    }
}
