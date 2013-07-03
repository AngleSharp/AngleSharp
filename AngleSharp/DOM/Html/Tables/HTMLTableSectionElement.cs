using AngleSharp.DOM.Collections;
using AngleSharp.DOM.Css;
using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the object for HTML table section (thead / tbody / tfoot) elements.
    /// </summary>
    public sealed class HTMLTableSectionElement : HTMLElement
    {
        #region Constants

        /// <summary>
        /// The dialog tag.
        /// </summary>
        internal const String HeadTag = "thead";

        /// <summary>
        /// The dialog tag.
        /// </summary>
        internal const String BodyTag = "tbody";

        /// <summary>
        /// The dialog tag.
        /// </summary>
        internal const String FootTag = "tfoot";

        #endregion

        #region ctor

        internal HTMLTableSectionElement()
        {
            _name = BodyTag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the horizontal alignment attribute.
        /// </summary>
        [DOM("align")]
        public HorizontalAlignment Align
        {
            get { return ToEnum(GetAttribute("align"), HorizontalAlignment.Center); }
            set { SetAttribute("align", value.ToString()); }
        }

        /// <summary>
        /// Gets the assigned table rows.
        /// </summary>
        [DOM("rows")]
        public HTMLCollection Rows
        {
            get { return _children.QuerySelectorAll(SimpleSelector.Type(HTMLTableRowElement.Tag)); }
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
        /// Inserts a row into this section. The new row is inserted immediately before the
        /// current indexth row in this section. If index is -1 or equal to the number of
        /// rows in this section, the new row is appended.
        /// </summary>
        /// <param name="index">The row number where to insert a new row. This index
        /// starts from 0 and is relative only to the rows contained inside this section,
        /// not all the rows in the table.</param>
        /// <returns>The inserted table row.</returns>
        [DOM("insertRow")]
        public HTMLTableRowElement InsertRow(Int32 index)
        {
            var row = Rows[index];
            var newRow = OwnerDocument.CreateElement(HTMLTableRowElement.Tag) as HTMLTableRowElement;

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
        /// <returns>The current table.</returns>
        [DOM("deleteRow")]
        public HTMLTableSectionElement DeleteRow(Int32 index)
        {
            var row = Rows[index];

            if (row != null)
                row.Remove();

            return this;
        }

        #endregion
    }
}
