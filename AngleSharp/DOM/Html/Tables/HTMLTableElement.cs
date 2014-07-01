namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Css;
    using System;

    /// <summary>
    /// Represents the HTML table element.
    /// </summary>
    [DomName("HTMLTableElement")]
    public sealed class HTMLTableElement : HTMLElement, IScopeElement, ITableScopeElement
    {
        #region Fields

        readonly HTMLCollection<IHtmlTableSectionElement> _bodies;
        readonly HTMLCollection<IHtmlTableRowElement> _rows;

        #endregion

        #region ctor

        internal HTMLTableElement()
        {
            _name = Tags.Table;
            _rows = new HTMLCollection<IHtmlTableRowElement>(this);
            _bodies = new HTMLCollection<IHtmlTableSectionElement>(this);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the assigned caption element.
        /// </summary>
        [DomName("caption")]
        public IHtmlTableCaptionElement Caption
        {
            get { return _children.QuerySelector<IHtmlTableCaptionElement>(SimpleSelector.Type(Tags.Caption)); }
        }

        /// <summary>
        /// Gets the assigned head section.
        /// </summary>
        [DomName("tHead")]
        public IHtmlTableSectionElement THead
        {
            get { return _children.QuerySelector<HTMLTableSectionElement>(SimpleSelector.Type(Tags.Thead)); }
        }

        /// <summary>
        /// Gets the assigned body sections.
        /// </summary>
        [DomName("tBodies")]
        public HTMLCollection<IHtmlTableSectionElement> TBodies
        {
            get { return _bodies; }
        }

        /// <summary>
        /// Gets the assigned foot section.
        /// </summary>
        [DomName("tFoot")]
        public IHtmlTableSectionElement TFoot
        {
            get { return _children.QuerySelector<HTMLTableSectionElement>(SimpleSelector.Type(Tags.Tfoot)); }
        }

        /// <summary>
        /// Gets the assigned table rows.
        /// </summary>
        [DomName("rows")]
        public HTMLCollection<IHtmlTableRowElement> Rows
        {
            get { return _rows; }
        }

        /// <summary>
        /// Gets or sets the value of the alignment attribute.
        /// </summary>
        [DomName("align")]
        public HorizontalAlignment Align
        {
            get { return ToEnum(GetAttribute(AttributeNames.Align), HorizontalAlignment.Left); }
            set { SetAttribute(AttributeNames.Align, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the background color attribute.
        /// </summary>
        [DomName("bgColor")]
        public String BgColor
        {
            get { return GetAttribute(AttributeNames.BgColor); }
            set { SetAttribute(AttributeNames.BgColor, value); }
        }

        /// <summary>
        /// Gets or sets the value of the border attribute.
        /// </summary>
        [DomName("border")]
        public UInt32 Border
        {
            get { return ToInteger(GetAttribute(AttributeNames.Border), 0u); }
            set { SetAttribute(AttributeNames.Border, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the cellpadding (padding within a cell) attribute.
        /// </summary>
        [DomName("cellPadding")]
        public Int32 CellPadding
        {
            get { return ToInteger(GetAttribute(AttributeNames.CellPadding), 0); }
            set { SetAttribute(AttributeNames.CellPadding, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the cellspacing (spacing between the cells) attribute.
        /// </summary>
        [DomName("cellSpacing")]
        public Int32 CellSpacing
        {
            get { return ToInteger(GetAttribute(AttributeNames.CellSpacing), 0); }
            set { SetAttribute(AttributeNames.CellSpacing, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the frame attribute.
        /// </summary>
        [DomName("frame")]
        public TableFrames Frame
        {
            get { return ToEnum(GetAttribute(AttributeNames.Frame), TableFrames.Void); }
            set { SetAttribute(AttributeNames.Frame, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the rules attribute.
        /// </summary>
        [DomName("rules")]
        public TableRules Rules
        {
            get { return ToEnum(GetAttribute(AttributeNames.Rules), TableRules.All); }
            set { SetAttribute(AttributeNames.Rules, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the summary attribute.
        /// </summary>
        [DomName("summary")]
        public String Summary
        {
            get { return GetAttribute(AttributeNames.Summary); }
            set { SetAttribute(AttributeNames.Summary, value); }
        }

        /// <summary>
        /// Gets or sets the value of the width attribute.
        /// </summary>
        [DomName("width")]
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
        [DomName("insertRow")]
        public IHtmlTableRowElement InsertRow(Int32 index)
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
                var bodies = TBodies;

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
        /// <returns>The current table.</returns>
        [DomName("deleteRow")]
        public HTMLTableElement DeleteRow(Int32 index)
        {
            var rows = Rows;

            if (index >= 0 && index < rows.Length)
                rows[index].Remove();

            return this;
        }

        /// <summary>
        /// Create a table header row or return an existing one.
        /// </summary>
        /// <returns>A new table header element.</returns>
        [DomName("createTHead")]
        public IHtmlTableSectionElement CreateTHead()
        {
            var head = THead;

            if (head == null)
            {
                head = Owner.CreateElement(Tags.Thead) as IHtmlTableSectionElement;
                AppendChild(head);
            }

            return head;
        }

        /// <summary>
        /// Delete the header from the table, if one exists. 
        /// </summary>
        /// <returns>The current table.</returns>
        [DomName("deleteTHead")]
        public HTMLTableElement DeleteTHead()
        {
            var head = THead;

            if (head != null)
                head.Remove();

            return this;
        }

        /// <summary>
        /// Create a table footer row or return an existing one.
        /// </summary>
        /// <returns>A footer element.</returns>
        [DomName("createTFoot")]
        public IHtmlTableSectionElement CreateTFoot()
        {
            var foot = TFoot;

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
        /// <returns>The current table.</returns>
        [DomName("deleteTFoot")]
        public HTMLTableElement DeleteTFoot()
        {
            var foot = TFoot;

            if (foot != null)
                foot.Remove();

            return this;
        }

        /// <summary>
        /// Create a new table caption object or return an existing one.
        /// </summary>
        /// <returns>A CAPTION element.</returns>
        [DomName("createCaption")]
        public IHtmlTableCaptionElement CreateCaption()
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
        /// <returns>The current table.</returns>
        [DomName("deleteCaption")]
        public HTMLTableElement DeleteCaption()
        {
            var caption = Caption;

            if (caption != null)
                caption.Remove();

            return this;
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
