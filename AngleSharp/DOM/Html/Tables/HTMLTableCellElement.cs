using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the object for HTML table cell (td / th) elements.
    /// </summary>
    public sealed class HTMLTableCellElement : HTMLElement
    {
        #region Constants

        /// <summary>
        /// The th tag.
        /// </summary>
        internal const String HeadTag = "th";

        /// <summary>
        /// The td tag.
        /// </summary>
        internal const String NormalTag = "td";

        #endregion

        #region ctor

        internal HTMLTableCellElement()
        {
            _name = NormalTag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the index of this cell in the row, starting from 0.
        /// This index is in document tree order and not display order.
        /// </summary>
        [DOM("cellIndex")]
        public Int32 CellIndex
        {
            get
            {
                var parent = ParentElement;

                while (parent != null && !(parent is HTMLTableRowElement))
                    parent = parent.ParentElement;

                if (parent is HTMLTableRowElement)
                    return ((HTMLTableRowElement)parent).Cells.IndexOf(this);

                return 0;
            }
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
        /// Gets or sets the value of the vertical alignment attribute.
        /// </summary>
        [DOM("vAlign")]
        public VerticalAlignment VAlign
        {
            get { return ToEnum(GetAttribute("valign"), VerticalAlignment.Middle); }
            set { SetAttribute("valign", value.ToString()); }
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
        /// Gets or sets the value of the width attribute.
        /// </summary>
        [DOM("width")]
        public String Width
        {
            get { return GetAttribute("width"); }
            set { SetAttribute("width", value); }
        }

        /// <summary>
        /// Gets or sets the value of the height attribute.
        /// </summary>
        [DOM("height")]
        public String Height
        {
            get { return GetAttribute("height"); }
            set { SetAttribute("height", value); }
        }

        /// <summary>
        /// Gets or sets the number of columns spanned by cell. 
        /// </summary>
        [DOM("colSpan")]
        public UInt32 ColSpan
        {
            get { return ToInteger(GetAttribute("colspan"), 0u); }
            set { SetAttribute("colspan", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the number of rows spanned by cell. 
        /// </summary>
        [DOM("rowSpan")]
        public UInt32 RowSpan
        {
            get { return ToInteger(GetAttribute("rowspan"), 0u); }
            set { SetAttribute("rowspan", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets if word wrapping is suppressed.
        /// </summary>
        [DOM("noWrap")]
        public Boolean NoWrap
        {
            get { return ToBoolean(GetAttribute("nowrap"), false); }
            set { SetAttribute("nowrap", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the abbreviation for header cells.
        /// </summary>
        [DOM("abbr")]
        public String Abbr
        {
            get { return GetAttribute("abbr"); }
            set { SetAttribute("abbr", value); }
        }

        /// <summary>
        /// Gets or sets the scope covered by header cells.
        /// </summary>
        [DOM("scope")]
        public String Scope
        {
            get { return GetAttribute("scope"); }
            set { SetAttribute("scope", value); }
        }

        /// <summary>
        /// Gets or sets the list of id attribute values for header cells. 
        /// </summary>
        [DOM("headers")]
        public String Headers
        {
            get { return GetAttribute("headers"); }
            set { SetAttribute("headers", value); }
        }

        /// <summary>
        /// Gets or sets the names group of related headers. 
        /// </summary>
        [DOM("axis")]
        public String Axis
        {
            get { return GetAttribute("axis"); }
            set { SetAttribute("axis", value); }
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
