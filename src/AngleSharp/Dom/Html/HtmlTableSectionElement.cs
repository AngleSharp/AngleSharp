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

        private HtmlCollection<IHtmlTableRowElement> _rows;

        #endregion

        #region ctor

        public HtmlTableSectionElement(Document owner, String name = null, String prefix = null)
            : base(owner, name ?? TagNames.Tbody, prefix, NodeFlags.Special | NodeFlags.ImplicitelyClosed | NodeFlags.HtmlTableSectionScoped)
        {
        }

        #endregion

        #region Properties

        public HorizontalAlignment Align
        {
            get { return this.GetOwnAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Center); }
            set { this.SetOwnAttribute(AttributeNames.Align, value.ToString()); }
        }

        public IHtmlCollection<IHtmlTableRowElement> Rows
        {
            get { return _rows ?? (_rows = new HtmlCollection<IHtmlTableRowElement>(this, deep: false)); }
        }

        public VerticalAlignment VAlign
        {
            get { return this.GetOwnAttribute(AttributeNames.Valign).ToEnum(VerticalAlignment.Middle); }
            set { this.SetOwnAttribute(AttributeNames.Valign, value.ToString()); }
        }

        #endregion

        #region Methods

        public IHtmlTableRowElement InsertRowAt(Int32 index = -1)
        {
            var rows = Rows;
            var newRow = Owner.CreateElement(TagNames.Tr) as IHtmlTableRowElement;

            if (index >= 0 && index < rows.Length)
            {
                InsertBefore(newRow, rows[index]);
            }
            else
            {
                AppendChild(newRow);
            }

            return newRow;
        }

        public void RemoveRowAt(Int32 index)
        {
            var rows = Rows;

            if (index >= 0 && index < rows.Length)
            {
                rows[index].Remove();
            }
        }

        #endregion
    }
}
