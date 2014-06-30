namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the object for HTML table cell (td / th) elements.
    /// </summary>
    [DomName("HTMLTableCellElement")]
    public sealed class HTMLTableCellElement : HTMLElement, IScopeElement, IImplClosed
    {
        #region ctor

        internal HTMLTableCellElement()
        {
            _name = Tags.Td;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the index of this cell in the row, starting from 0.
        /// This index is in document tree order and not display order.
        /// </summary>
        [DomName("cellIndex")]
        public Int32 CellIndex
        {
            get
            {
                var parent = ParentElement;

                while (parent != null && !(parent is IHtmlTableRowElement))
                    parent = parent.ParentElement;

                var row = parent as HTMLTableRowElement;

                if (row != null)
                    return row.IndexOf(this);

                return 0;
            }
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
        /// Gets or sets the value of the vertical alignment attribute.
        /// </summary>
        [DomName("vAlign")]
        public VerticalAlignment VAlign
        {
            get { return ToEnum(GetAttribute(AttributeNames.Valign), VerticalAlignment.Middle); }
            set { SetAttribute(AttributeNames.Valign, value.ToString()); }
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
        /// Gets or sets the value of the width attribute.
        /// </summary>
        [DomName("width")]
        public String Width
        {
            get { return GetAttribute(AttributeNames.Width); }
            set { SetAttribute(AttributeNames.Width, value); }
        }

        /// <summary>
        /// Gets or sets the value of the height attribute.
        /// </summary>
        [DomName("height")]
        public String Height
        {
            get { return GetAttribute(AttributeNames.Height); }
            set { SetAttribute(AttributeNames.Height, value); }
        }

        /// <summary>
        /// Gets or sets the number of columns spanned by cell. 
        /// </summary>
        [DomName("colSpan")]
        public UInt32 ColSpan
        {
            get { return ToInteger(GetAttribute(AttributeNames.ColSpan), 0u); }
            set { SetAttribute(AttributeNames.ColSpan, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the number of rows spanned by cell. 
        /// </summary>
        [DomName("rowSpan")]
        public UInt32 RowSpan
        {
            get { return ToInteger(GetAttribute(AttributeNames.RowSpan), 0u); }
            set { SetAttribute(AttributeNames.RowSpan, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets if word wrapping is suppressed.
        /// </summary>
        [DomName("noWrap")]
        public Boolean NoWrap
        {
            get { return ToBoolean(GetAttribute(AttributeNames.NoWrap), false); }
            set { SetAttribute(AttributeNames.NoWrap, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the abbreviation for header cells.
        /// </summary>
        [DomName("abbr")]
        public String Abbr
        {
            get { return GetAttribute(AttributeNames.Abbr); }
            set { SetAttribute(AttributeNames.Abbr, value); }
        }

        /// <summary>
        /// Gets or sets the scope covered by header cells.
        /// </summary>
        [DomName("scope")]
        public String Scope
        {
            get { return GetAttribute(AttributeNames.Scope); }
            set { SetAttribute(AttributeNames.Scope, value); }
        }

        /// <summary>
        /// Gets or sets the list of id attribute values for header cells. 
        /// </summary>
        [DomName("headers")]
        public String Headers
        {
            get { return GetAttribute(AttributeNames.Headers); }
            set { SetAttribute(AttributeNames.Headers, value); }
        }

        /// <summary>
        /// Gets or sets the names group of related headers. 
        /// </summary>
        [DomName("axis")]
        public String Axis
        {
            get { return GetAttribute(AttributeNames.Axis); }
            set { SetAttribute(AttributeNames.Axis, value); }
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
    }
}
