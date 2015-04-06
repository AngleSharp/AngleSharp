namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the object for HTML table cell elements.
    /// </summary>
    abstract class HtmlTableCellElement : HtmlElement, IHtmlTableCellElement
    {
        #region Fields

        SettableTokenList _headers;

        #endregion

        #region ctor

        public HtmlTableCellElement(Document owner, String name, String prefix)
            : base(owner, name, prefix, NodeFlags.Special | NodeFlags.ImplicitelyClosed | NodeFlags.Scoped)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the index of this cell in the row, starting from 0.
        /// This index is in document tree order and not display order.
        /// </summary>
        public Int32 Index
        {
            get
            {
                var parent = ParentElement;

                while (parent != null && !(parent is IHtmlTableRowElement))
                    parent = parent.ParentElement;

                var row = parent as HtmlTableRowElement;

                if (row != null)
                    return row.IndexOf(this);

                return -1;
            }
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
        /// Gets or sets the value of the vertical alignment attribute.
        /// </summary>
        public VerticalAlignment VAlign
        {
            get { return GetOwnAttribute(AttributeNames.Valign).ToEnum(VerticalAlignment.Middle); }
            set { SetOwnAttribute(AttributeNames.Valign, value.ToString()); }
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
        /// Gets or sets the value of the width attribute.
        /// </summary>
        public String Width
        {
            get { return GetOwnAttribute(AttributeNames.Width); }
            set { SetOwnAttribute(AttributeNames.Width, value); }
        }

        /// <summary>
        /// Gets or sets the value of the height attribute.
        /// </summary>
        public String Height
        {
            get { return GetOwnAttribute(AttributeNames.Height); }
            set { SetOwnAttribute(AttributeNames.Height, value); }
        }

        /// <summary>
        /// Gets or sets the number of columns spanned by cell. 
        /// </summary>
        public Int32 ColumnSpan
        {
            get { return GetOwnAttribute(AttributeNames.ColSpan).ToInteger(0); }
            set { SetOwnAttribute(AttributeNames.ColSpan, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the number of rows spanned by cell. 
        /// </summary>
        public Int32 RowSpan
        {
            get { return GetOwnAttribute(AttributeNames.RowSpan).ToInteger(0); }
            set { SetOwnAttribute(AttributeNames.RowSpan, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets if word wrapping is suppressed.
        /// </summary>
        public Boolean NoWrap
        {
            get { return GetOwnAttribute(AttributeNames.NoWrap).ToBoolean(false); }
            set { SetOwnAttribute(AttributeNames.NoWrap, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the abbreviation for header cells.
        /// </summary>
        public String Abbr
        {
            get { return GetOwnAttribute(AttributeNames.Abbr); }
            set { SetOwnAttribute(AttributeNames.Abbr, value); }
        }

        /// <summary>
        /// Gets or sets the scope covered by header cells.
        /// </summary>
        public String Scope
        {
            get { return GetOwnAttribute(AttributeNames.Scope); }
            set { SetOwnAttribute(AttributeNames.Scope, value); }
        }

        /// <summary>
        /// Gets or sets the list of id attribute values for header cells. 
        /// </summary>
        public ISettableTokenList Headers
        {
            get
            { 
                if (_headers == null)
                {
                    _headers = new SettableTokenList(GetOwnAttribute(AttributeNames.Headers));
                    _headers.Changed += (s, ev) => UpdateAttribute(AttributeNames.Headers, _headers.Value);
                }

                return _headers; 
            }
        }

        /// <summary>
        /// Gets or sets the names group of related headers. 
        /// </summary>
        public String Axis
        {
            get { return GetOwnAttribute(AttributeNames.Axis); }
            set { SetOwnAttribute(AttributeNames.Axis, value); }
        }

        #endregion
    }
}
