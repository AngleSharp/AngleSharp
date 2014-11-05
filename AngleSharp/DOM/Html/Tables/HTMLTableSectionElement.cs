namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the object for HTML table section (thead / tbody / tfoot) elements.
    /// </summary>
    sealed class HTMLTableSectionElement : HTMLElement, IHtmlTableSectionElement
    {
        #region Fields

        readonly HtmlCollection<IHtmlTableRowElement> _rows;

        #endregion

        #region ctor

        internal HTMLTableSectionElement(String name)
            : base(name, NodeFlags.Special | NodeFlags.ImplicitelyClosed | NodeFlags.HtmlTableSectionScoped)
        {
            _rows = new HtmlCollection<IHtmlTableRowElement>(this);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the horizontal alignment attribute.
        /// </summary>
        public HorizontalAlignment Align
        {
            get { return GetAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Center); }
            set { SetAttribute(AttributeNames.Align, value.ToString()); }
        }

        /// <summary>
        /// Gets the assigned table rows.
        /// </summary>
        public IHtmlCollection Rows
        {
            get { return _rows; }
        }

        /// <summary>
        /// Gets or sets the value of the vertical alignment attribute.
        /// </summary>
        public VerticalAlignment VAlign
        {
            get { return GetAttribute(AttributeNames.Valign).ToEnum(VerticalAlignment.Middle); }
            set { SetAttribute(AttributeNames.Valign, value.ToString()); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Inserts a row into this section. The new row is inserted immediately before the
        /// current indexth row in this section. If index is -1 or equal to the number of
        /// rows in this section, the new row is appended.
        /// </summary>
        /// <param name="index">The row number where to insert a new row. This index
        /// starts from 0 and is relative only to the rows contained inside this section,
        /// not all the rows in the table.</param>
        /// <returns>The inserted table row.</returns>
        public IHtmlElement InsertRowAt(Int32 index = -1)
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
        /// <param name="index">The index of the row to be deleted, or -1 to delete the last
        /// row. This index starts from 0 and is relative only to the rows contained inside
        /// this section, not all the rows in the table.</param>
        public void RemoveRowAt(Int32 index)
        {
            var row = Rows[index];

            if (row != null)
                row.Remove();
        }

        #endregion
    }
}
