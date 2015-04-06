namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the object for HTML table section (thead / tbody / tfoot) elements.
    /// </summary>
    sealed class HtmlTableSectionElement : HtmlElement, IHtmlTableSectionElement
    {
        #region Fields

        HtmlCollection<IHtmlTableRowElement> _rows;

        #endregion

        #region ctor

        public HtmlTableSectionElement(Document owner)
            : this(owner, Tags.Tbody)
        {
        }

        public HtmlTableSectionElement(Document owner, String name, String prefix = null)
            : base(owner, name, prefix, NodeFlags.Special | NodeFlags.ImplicitelyClosed | NodeFlags.HtmlTableSectionScoped)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the horizontal alignment attribute.
        /// </summary>
        public HorizontalAlignment Align
        {
            get { return GetOwnAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Center); }
            set { SetOwnAttribute(AttributeNames.Align, value.ToString()); }
        }

        /// <summary>
        /// Gets the assigned table rows.
        /// </summary>
        public IHtmlCollection<IHtmlTableRowElement> Rows
        {
            get { return _rows ?? (_rows = new HtmlCollection<IHtmlTableRowElement>(this, deep: false)); }
        }

        /// <summary>
        /// Gets or sets the value of the vertical alignment attribute.
        /// </summary>
        public VerticalAlignment VAlign
        {
            get { return GetOwnAttribute(AttributeNames.Valign).ToEnum(VerticalAlignment.Middle); }
            set { SetOwnAttribute(AttributeNames.Valign, value.ToString()); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Inserts a row into this section. The new row is inserted
        /// immediately before the current indexth row in this section. If
        /// index is -1 or equal to the number of rows in this section, the new
        /// row is appended. 
        /// </summary>
        /// <param name="index">
        /// The row number where to insert a new row. This index starts from 0
        /// and is relative only to the rows contained inside this section, not
        /// all the rows in the table.
        /// </param> 
        /// <returns>The inserted table row.</returns>
        public IHtmlTableRowElement InsertRowAt(Int32 index = -1)
        {
            var row = Rows[index];
            var newRow = Owner.CreateElement(Tags.Tr) as IHtmlTableRowElement;

            if (row != null)
                InsertBefore(newRow, row);
            else
                AppendChild(newRow);

            return newRow;
        }

        /// <summary>
        /// Deletes a row from this section.
        /// </summary>
        /// <param name="index">
        /// The index of the row to be deleted, or -1 to delete the last row.
        /// This index starts from 0 and is relative only to the rows contained
        /// inside this section, not all the rows in the table.
        /// </param>
        public void RemoveRowAt(Int32 index)
        {
            var row = Rows[index];

            if (row != null)
                row.Remove();
        }

        #endregion
    }
}
