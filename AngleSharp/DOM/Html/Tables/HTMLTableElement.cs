namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Css;
    using System;

    /// <summary>
    /// Represents the HTML table element.
    /// </summary>
    sealed class HTMLTableElement : HTMLElement, IScopeElement, ITableScopeElement, IHtmlTableElement
    {
        #region Fields

        readonly HtmlCollection<IHtmlTableSectionElement> _bodies;
        readonly HtmlCollection<IHtmlTableRowElement> _rows;

        #endregion

        #region ctor

        internal HTMLTableElement()
        {
            _name = Tags.Table;
            _rows = new HtmlCollection<IHtmlTableRowElement>(this);
            _bodies = new HtmlCollection<IHtmlTableSectionElement>(this);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the assigned caption element.
        /// </summary>
        public IHtmlTableCaptionElement Caption
        {
            get { return _children.QuerySelector<IHtmlTableCaptionElement>(SimpleSelector.Type(Tags.Caption)); }
            set { DeleteCaption(); AppendChild(value); }
        }

        /// <summary>
        /// Gets or sets the assigned head section.
        /// </summary>
        public IHtmlTableSectionElement Head
        {
            get { return _children.QuerySelector<HTMLTableSectionElement>(SimpleSelector.Type(Tags.Thead)); }
            set { DeleteHead(); AppendChild(value); }
        }

        /// <summary>
        /// Gets the assigned body sections.
        /// </summary>
        public IHtmlCollection Bodies
        {
            get { return _bodies; }
        }

        /// <summary>
        /// Gets or sets the assigned foot section.
        /// </summary>
        public IHtmlTableSectionElement Foot
        {
            get { return _children.QuerySelector<HTMLTableSectionElement>(SimpleSelector.Type(Tags.Tfoot)); }
            set { DeleteFoot(); AppendChild(value); }
        }

        /// <summary>
        /// Gets the assigned table rows.
        /// </summary>
        public IHtmlCollection Rows
        {
            get { return _rows; }
        }

        /// <summary>
        /// Gets or sets the value of the alignment attribute.
        /// </summary>
        public HorizontalAlignment Align
        {
            get { return GetAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Left); }
            set { SetAttribute(AttributeNames.Align, value.ToString()); }
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
        /// Gets or sets the value of the border attribute.
        /// </summary>
        public UInt32 Border
        {
            get { return GetAttribute(AttributeNames.Border).ToInteger(0u); }
            set { SetAttribute(AttributeNames.Border, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the cellpadding (padding within a cell) attribute.
        /// </summary>
        public Int32 CellPadding
        {
            get { return GetAttribute(AttributeNames.CellPadding).ToInteger(0); }
            set { SetAttribute(AttributeNames.CellPadding, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the cellspacing (spacing between the cells) attribute.
        /// </summary>
        public Int32 CellSpacing
        {
            get { return GetAttribute(AttributeNames.CellSpacing).ToInteger(0); }
            set { SetAttribute(AttributeNames.CellSpacing, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the frame attribute.
        /// </summary>
        public TableFrames Frame
        {
            get { return GetAttribute(AttributeNames.Frame).ToEnum(TableFrames.Void); }
            set { SetAttribute(AttributeNames.Frame, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the rules attribute.
        /// </summary>
        public TableRules Rules
        {
            get { return GetAttribute(AttributeNames.Rules).ToEnum(TableRules.All); }
            set { SetAttribute(AttributeNames.Rules, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the summary attribute.
        /// </summary>
        public String Summary
        {
            get { return GetAttribute(AttributeNames.Summary); }
            set { SetAttribute(AttributeNames.Summary, value); }
        }

        /// <summary>
        /// Gets or sets the value of the width attribute.
        /// </summary>
        public String Width
        {
            get { return GetAttribute(AttributeNames.Width); }
            set { SetAttribute(AttributeNames.Width, value); }
        }

        #endregion

        #region Internal Properties

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
        /// Inserts a new empty row in the table. The new row is inserted immediately before
        /// and in the same section as the current indexth row in the table. If index is -1
        /// or equal to the number of rows, the new row is appended. In addition, when the
        /// table is empty the row is inserted into a TBODY which is created and inserted
        /// into the table.
        /// </summary>
        /// <param name="index">The row number where to insert a new row. This index starts
        /// from 0 and is relative to the logical order (not document order) of all the rows
        /// contained inside the table.</param>
        /// <returns>The inserted table row.</returns>
        public IHtmlElement InsertRowAt(Int32 index = -1)
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
        /// <param name="index">The index of the row to be deleted. This index starts from
        /// 0 and is relative to the logical order (not document order) of all the rows
        /// contained inside the table. If the index is -1 the last row in the table is
        /// deleted.</param>
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
        public IHtmlElement CreateHead()
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
        public IHtmlElement CreateBody()
        {
            var body = Owner.CreateElement(Tags.Tbody) as IHtmlTableSectionElement;
            AppendChild(body);
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
        public IHtmlElement CreateFoot()
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
        public IHtmlElement CreateCaption()
        {
            var caption = Caption;

            if (caption == null)
            {
                caption = Owner.CreateElement(Tags.Caption) as IHtmlTableCaptionElement;
                AppendChild(caption);
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

        #region Enumeration

        /// <summary>
        /// The list of possible table rules.
        /// </summary>
        public enum TableRules
        {
            /// <summary>
            /// No rules. This is the default value.
            /// </summary>
            None,
            /// <summary>
            /// Rules will appear between rows only.
            /// </summary>
            Rows,
            /// <summary>
            /// Rules will appear between columns only.
            /// </summary>
            Cols,
            /// <summary>
            /// Rules will appear between row groups and column groups only.
            /// </summary>
            Groups,
            /// <summary>
            /// Rules will appear between all rows and columns.
            /// </summary>
            All
        }

        /// <summary>
        /// The list of possible table frame directives.
        /// </summary>
        public enum TableFrames
        {
            /// <summary>
            /// No sides. This is the default value.
            /// </summary>
            Void,
            /// <summary>
            /// All four sides.
            /// </summary>
            Box,
            /// <summary>
            /// The top side only.
            /// </summary>
            Above,
            /// <summary>
            /// The bottom side only.
            /// </summary>
            Below,
            /// <summary>
            /// The top and bottom sides only.
            /// </summary>
            HSides,
            /// <summary>
            /// The right and left sides only.
            /// </summary>
            VSides,
            /// <summary>
            /// The left-hand side only.
            /// </summary>
            LHS,
            /// <summary>
            /// The right-hand side only.
            /// </summary>
            RHS,
            /// <summary>
            /// All four sides.
            /// </summary>
            Border
        }

        #endregion
    }
}
