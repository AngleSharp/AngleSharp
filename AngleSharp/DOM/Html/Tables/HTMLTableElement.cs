using AngleSharp.DOM.Collections;
using AngleSharp.DOM.Css;
using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML table element.
    /// </summary>
    public sealed class HTMLTableElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The table tag.
        /// </summary>
        internal const String Tag = "table";

        #endregion

        #region ctor

        internal HTMLTableElement()
        {
            _name = Tag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the assigned caption element.
        /// </summary>
        [DOM("caption")]
        public HTMLTableCaptionElement Caption
        {
            get { return _children.QuerySelector<HTMLTableCaptionElement>(SimpleSelector.Type(HTMLTableCaptionElement.Tag)); }
        }

        /// <summary>
        /// Gets the assigned head section.
        /// </summary>
        [DOM("tHead")]
        public HTMLTableSectionElement THead
        {
            get { return _children.QuerySelector<HTMLTableSectionElement>(SimpleSelector.Type(HTMLTableSectionElement.HeadTag)); }
        }

        /// <summary>
        /// Gets the assigned body sections.
        /// </summary>
        [DOM("tBodies")]
        public HTMLCollection TBodies
        {
            get { return _children.QuerySelectorAll(SimpleSelector.Type(HTMLTableSectionElement.BodyTag)); }
        }

        /// <summary>
        /// Gets the assigned foot section.
        /// </summary>
        [DOM("tFoot")]
        public HTMLTableSectionElement TFoot
        {
            get { return _children.QuerySelector<HTMLTableSectionElement>(SimpleSelector.Type(HTMLTableSectionElement.FootTag)); }
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
        /// Gets or sets the value of the alignment attribute.
        /// </summary>
        [DOM("align")]
        public HorizontalAlignment Align
        {
            get { return ToEnum(GetAttribute("align"), HorizontalAlignment.Left); }
            set { SetAttribute("align", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the background color attribute.
        /// </summary>
        [DOM("bgColor")]
        public String BgColor
        {
            get { return GetAttribute("bgcolor"); }
            set { SetAttribute("bgcolor", value); }
        }

        /// <summary>
        /// Gets or sets the value of the border attribute.
        /// </summary>
        [DOM("border")]
        public UInt32 Border
        {
            get { return ToInteger(GetAttribute("border"), 0u); }
            set { SetAttribute("border", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the cellpadding (padding within a cell) attribute.
        /// </summary>
        [DOM("cellPadding")]
        public Int32 CellPadding
        {
            get { return ToInteger(GetAttribute("cellpadding"), 0); }
            set { SetAttribute("cellpadding", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the cellspacing (spacing between the cells) attribute.
        /// </summary>
        [DOM("cellSpacing")]
        public Int32 CellSpacing
        {
            get { return ToInteger(GetAttribute("cellspacing"), 0); }
            set { SetAttribute("cellspacing", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the frame attribute.
        /// </summary>
        [DOM("frame")]
        public TableFrames Frame
        {
            get { return ToEnum(GetAttribute("frame"), TableFrames.Void); }
            set { SetAttribute("frame", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the rules attribute.
        /// </summary>
        [DOM("rules")]
        public TableRules Rules
        {
            get { return ToEnum(GetAttribute("rules"), TableRules.All); }
            set { SetAttribute("rules", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the summary attribute.
        /// </summary>
        [DOM("summary")]
        public String Summary
        {
            get { return GetAttribute("summary"); }
            set { SetAttribute("summary", value); }
        }

        /// <summary>
        /// Gets or sets the value of the width attribute.
        /// </summary>
        [DOM("width")]
        public String Width
        {
            get { return GetAttribute("width"); }
            set { SetAttribute("width", value); }
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
        [DOM("insertRow")]
        public HTMLTableRowElement InsertRow(Int32 index)
        {
            var rows = Rows;
            var newRow = OwnerDocument.CreateElement(HTMLTableRowElement.Tag) as HTMLTableRowElement;

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
                    var tbody = OwnerDocument.CreateElement(HTMLTableSectionElement.BodyTag);
                    AppendChild(tbody);
                    bodies.Add(tbody);
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
        [DOM("deleteRow")]
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
        [DOM("createTHead")]
        public HTMLTableSectionElement CreateTHead()
        {
            var head = THead;

            if (head == null)
            {
                head = OwnerDocument.CreateElement(HTMLTableSectionElement.HeadTag) as HTMLTableSectionElement;
                AppendChild(head);
            }

            return head;
        }

        /// <summary>
        /// Delete the header from the table, if one exists. 
        /// </summary>
        /// <returns>The current table.</returns>
        [DOM("deleteTHead")]
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
        [DOM("createTFoot")]
        public HTMLTableSectionElement CreateTFoot()
        {
            var foot = TFoot;

            if (foot == null)
            {
                foot = OwnerDocument.CreateElement(HTMLTableSectionElement.FootTag) as HTMLTableSectionElement;
                AppendChild(foot);
            }

            return foot;
        }

        /// <summary>
        /// Delete the footer from the table, if one exists.
        /// </summary>
        /// <returns>The current table.</returns>
        [DOM("deleteTFoot")]
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
        [DOM("createCaption")]
        public HTMLTableCaptionElement CreateCaption()
        {
            var caption = Caption;

            if (caption == null)
            {
                caption = OwnerDocument.CreateElement(HTMLTableCaptionElement.Tag) as HTMLTableCaptionElement;
                AppendChild(caption);
            }

            return caption;
        }

        /// <summary>
        /// Delete the table caption, if one exists.
        /// </summary>
        /// <returns>The current table.</returns>
        [DOM("deleteCaption")]
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
