namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML tr element.
    /// </summary>
    sealed class HtmlTableRowElement : HtmlElement, IHtmlTableRowElement
    {
        #region Fields

        private HtmlCollection<IHtmlTableCellElement> _cells;

        #endregion

        #region ctor

        public HtmlTableRowElement(Document owner, String prefix = null)
            : base(owner, TagNames.Tr, prefix, NodeFlags.Special | NodeFlags.ImplicitelyClosed)
        {
        }

        #endregion

        #region Properties

        public HorizontalAlignment Align
        {
            get { return this.GetOwnAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Left); }
            set { this.SetOwnAttribute(AttributeNames.Align, value.ToString()); }
        }

        public VerticalAlignment VAlign
        {
            get { return this.GetOwnAttribute(AttributeNames.Valign).ToEnum(VerticalAlignment.Middle); }
            set { this.SetOwnAttribute(AttributeNames.Valign, value.ToString()); }
        }

        public String BgColor
        {
            get { return this.GetOwnAttribute(AttributeNames.BgColor); }
            set { this.SetOwnAttribute(AttributeNames.BgColor, value); }
        }

        public IHtmlCollection<IHtmlTableCellElement> Cells
        {
            get { return _cells ?? (_cells = new HtmlCollection<IHtmlTableCellElement>(this, deep: false)); }
        }

        public Int32 Index
        {
            get
            {
                var table = this.GetAncestor<IHtmlTableElement>();
                return table?.Rows.Index(this) ?? -1;
            }
        }

        public Int32 IndexInSection
        {
            get
            {
                var parent = ParentElement as IHtmlTableSectionElement;
                return parent?.Rows.Index(this) ?? Index;
            }
        }

        #endregion

        #region Methods

        public IHtmlTableCellElement InsertCellAt(Int32 index = -1)
        {
            var cells = Cells;
            var newCell = Owner.CreateElement(TagNames.Td) as IHtmlTableCellElement;

            if (index >= 0 && index < cells.Length)
            {
                InsertBefore(newCell, cells[index]);
            }
            else
            {
                AppendChild(newCell);
            }

            return newCell;
        }

        public void RemoveCellAt(Int32 index)
        {
            var cells = Cells;

            if (index < 0)
            {
                index = cells.Length + index;
            }

            if (index >= 0 && index < cells.Length)
            {
                cells[index].Remove();
            }
        }

        #endregion
    }
}
