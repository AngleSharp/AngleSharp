namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents the HTML table element.
    /// </summary>
    sealed class HtmlTableElement : HtmlElement, IHtmlTableElement
    {
        #region Fields

        HtmlCollection<IHtmlTableSectionElement> _bodies;
        HtmlCollection<IHtmlTableRowElement> _rows;

        #endregion

        #region ctor

        public HtmlTableElement(Document owner, String prefix = null)
            : base(owner, Tags.Table, prefix, NodeFlags.Special | NodeFlags.Scoped | NodeFlags.HtmlTableScoped | NodeFlags.HtmlTableSectionScoped)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the assigned caption element.
        /// </summary>
        public IHtmlTableCaptionElement Caption
        {
            get { return ChildNodes.OfType<IHtmlTableCaptionElement>().FirstOrDefault(m => m.LocalName == Tags.Caption); }
            set { DeleteCaption(); InsertChild(0, value); }
        }

        /// <summary>
        /// Gets or sets the assigned head section.
        /// </summary>
        public IHtmlTableSectionElement Head
        {
            get { return ChildNodes.OfType<IHtmlTableSectionElement>().FirstOrDefault(m => m.LocalName == Tags.Thead); }
            set { DeleteHead(); AppendChild(value); }
        }

        /// <summary>
        /// Gets the assigned body sections.
        /// </summary>
        public IHtmlCollection<IHtmlTableSectionElement> Bodies
        {
            get { return _bodies ?? (_bodies = new HtmlCollection<IHtmlTableSectionElement>(this, deep: false, predicate: m => m.LocalName == Tags.Tbody)); }
        }

        /// <summary>
        /// Gets or sets the assigned foot section.
        /// </summary>
        public IHtmlTableSectionElement Foot
        {
            get { return ChildNodes.OfType<IHtmlTableSectionElement>().FirstOrDefault(m => m.LocalName == Tags.Tfoot); }
            set { DeleteFoot(); AppendChild(value); }
        }

        /// <summary>
        /// Gets an enumeration over all rows of the table.
        /// </summary>
        public IEnumerable<IHtmlTableRowElement> AllRows
        {
            get
            {
                var heads = ChildNodes.OfType<IHtmlTableSectionElement>().Where(m => m.LocalName == Tags.Thead);
                var foots = ChildNodes.OfType<IHtmlTableSectionElement>().Where(m => m.LocalName == Tags.Tfoot);

                foreach (var head in heads)
                {
                    foreach (var row in head.Rows)
                        yield return row;
                }

                foreach (var child in ChildNodes)
                {
                    if (child is IHtmlTableSectionElement)
                    {
                        var body = (IHtmlTableSectionElement)child;

                        if (body.LocalName == Tags.Tbody)
                        {
                            foreach (var row in body.Rows)
                                yield return row;
                        }
                    }
                    else if (child is IHtmlTableRowElement)
                        yield return (IHtmlTableRowElement)child;
                }

                foreach (var foot in foots)
                {
                    foreach (var row in foot.Rows)
                        yield return row;
                }
            }
        }

        /// <summary>
        /// Gets the assigned table rows.
        /// </summary>
        public IHtmlCollection<IHtmlTableRowElement> Rows
        {
            get { return _rows ?? (_rows = new HtmlCollection<IHtmlTableRowElement>(AllRows)); }
        }

        /// <summary>
        /// Gets or sets the value of the alignment attribute.
        /// </summary>
        public HorizontalAlignment Align
        {
            get { return GetOwnAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Left); }
            set { SetOwnAttribute(AttributeNames.Align, value.ToString()); }
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
        /// Gets or sets the value of the border attribute.
        /// </summary>
        public UInt32 Border
        {
            get { return GetOwnAttribute(AttributeNames.Border).ToInteger(0u); }
            set { SetOwnAttribute(AttributeNames.Border, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the cellpadding (padding within a cell)
        /// attribute.
        /// </summary>
        public Int32 CellPadding
        {
            get { return GetOwnAttribute(AttributeNames.CellPadding).ToInteger(0); }
            set { SetOwnAttribute(AttributeNames.CellPadding, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the cellspacing (spacing between the
        /// cells) attribute.
        /// </summary>
        public Int32 CellSpacing
        {
            get { return GetOwnAttribute(AttributeNames.CellSpacing).ToInteger(0); }
            set { SetOwnAttribute(AttributeNames.CellSpacing, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the frame attribute.
        /// </summary>
        public TableFrames Frame
        {
            get { return GetOwnAttribute(AttributeNames.Frame).ToEnum(TableFrames.Void); }
            set { SetOwnAttribute(AttributeNames.Frame, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the rules attribute.
        /// </summary>
        public TableRules Rules
        {
            get { return GetOwnAttribute(AttributeNames.Rules).ToEnum(TableRules.All); }
            set { SetOwnAttribute(AttributeNames.Rules, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the summary attribute.
        /// </summary>
        public String Summary
        {
            get { return GetOwnAttribute(AttributeNames.Summary); }
            set { SetOwnAttribute(AttributeNames.Summary, value); }
        }

        /// <summary>
        /// Gets or sets the value of the width attribute.
        /// </summary>
        public String Width
        {
            get { return GetOwnAttribute(AttributeNames.Width); }
            set { SetOwnAttribute(AttributeNames.Width, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Inserts a new empty row in the table. The new row is inserted
        /// immediately before and in the same section as the current index-th
        /// row in the table. If index is -1 or equal to the number of rows,
        /// the new row is appended. In addition, when the table is empty the
        /// row is inserted into a TBODY which is created and inserted into the
        /// table.
        /// </summary>
        /// <param name="index">
        /// The row number where to insert a new row. This index starts from 0
        /// and is relative to the logical order (not document order) of all
        /// the rows  contained inside the table.
        /// </param>
        /// <returns>The inserted table row.</returns>
        public IHtmlTableRowElement InsertRowAt(Int32 index = -1)
        {
            var rows = Rows;
            var newRow = Owner.CreateElement(Tags.Tr) as IHtmlTableRowElement;

            if (index >= 0 && index < rows.Length)
            {
                var row = rows[index];
                row.ParentElement.InsertBefore(newRow, row);
            }
            else if (rows.Length == 0)
            {
                var bodies = Bodies;

                if (bodies.Length == 0)
                {
                    var tbody = Owner.CreateElement(Tags.Tbody);
                    AppendChild(tbody);
                }

                bodies[bodies.Length - 1].AppendChild(newRow);
            }
            else
            {
                rows[rows.Length - 1].ParentElement.AppendChild(newRow);
            }

            return newRow;
        }

        /// <summary>
        /// Deletes a table row.
        /// </summary>
        /// <param name="index">
        /// The index of the row to be deleted. This index starts from 0 and is
        /// relative to the logical order (not document order) of all the rows
        /// contained inside the table. If the index is -1 the last row in the
        /// table is deleted.
        /// </param>
        public void RemoveRowAt(Int32 index)
        {
            var rows = Rows;

            if (index >= 0 && index < rows.Length)
                rows[index].Remove();
        }

        /// <summary>
        /// Create a table header row or return an existing one.
        /// </summary>
        /// <returns>A new table header element.</returns>
        public IHtmlTableSectionElement CreateHead()
        {
            var head = Head;

            if (head == null)
            {
                head = Owner.CreateElement(Tags.Thead) as IHtmlTableSectionElement;
                AppendChild(head);
            }

            return head;
        }

        /// <summary>
        /// Creates a new table body and appends it.
        /// </summary>
        /// <returns>The created table body.</returns>
        public IHtmlTableSectionElement CreateBody()
        {
            var lastBody = Bodies.LastOrDefault();
            var body = Owner.CreateElement(Tags.Tbody) as IHtmlTableSectionElement;
            var index = lastBody != null ? lastBody.Index() + 1 : ChildNodes.Length;
            InsertChild(index, body);
            return body;
        }

        /// <summary>
        /// Delete the header from the table, if one exists. 
        /// </summary>
        public void DeleteHead()
        {
            var head = Head;

            if (head != null)
                head.Remove();
        }

        /// <summary>
        /// Create a table footer row or return an existing one.
        /// </summary>
        /// <returns>A footer element.</returns>
        public IHtmlTableSectionElement CreateFoot()
        {
            var foot = Foot;

            if (foot == null)
            {
                foot = Owner.CreateElement(Tags.Tfoot) as IHtmlTableSectionElement;
                AppendChild(foot);
            }

            return foot;
        }

        /// <summary>
        /// Delete the footer from the table, if one exists.
        /// </summary>
        public void DeleteFoot()
        {
            var foot = Foot;

            if (foot != null)
                foot.Remove();
        }

        /// <summary>
        /// Create a new table caption object or return an existing one.
        /// </summary>
        /// <returns>A caption element.</returns>
        public IHtmlTableCaptionElement CreateCaption()
        {
            var caption = Caption;

            if (caption == null)
            {
                caption = Owner.CreateElement(Tags.Caption) as IHtmlTableCaptionElement;
                InsertChild(0, caption);
            }

            return caption;
        }

        /// <summary>
        /// Delete the table caption, if one exists.
        /// </summary>
        public void DeleteCaption()
        {
            var caption = Caption;

            if (caption != null)
                caption.Remove();
        }


        #endregion
    }
}
