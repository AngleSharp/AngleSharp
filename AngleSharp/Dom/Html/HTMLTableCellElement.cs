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

        public HtmlTableCellElement(Document owner, String name)
            : base(owner, name, NodeFlags.Special | NodeFlags.ImplicitelyClosed | NodeFlags.Scoped)
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

                return 0;
            }
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
        /// Gets or sets the value of the vertical alignment attribute.
        /// </summary>
        public VerticalAlignment VAlign
        {
            get { return GetAttribute(AttributeNames.Valign).ToEnum(VerticalAlignment.Middle); }
            set { SetAttribute(AttributeNames.Valign, value.ToString()); }
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
        /// Gets or sets the value of the width attribute.
        /// </summary>
        public String Width
        {
            get { return GetAttribute(AttributeNames.Width); }
            set { SetAttribute(AttributeNames.Width, value); }
        }

        /// <summary>
        /// Gets or sets the value of the height attribute.
        /// </summary>
        public String Height
        {
            get { return GetAttribute(AttributeNames.Height); }
            set { SetAttribute(AttributeNames.Height, value); }
        }

        /// <summary>
        /// Gets or sets the number of columns spanned by cell. 
        /// </summary>
        public Int32 ColumnSpan
        {
            get { return GetAttribute(AttributeNames.ColSpan).ToInteger(0); }
            set { SetAttribute(AttributeNames.ColSpan, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the number of rows spanned by cell. 
        /// </summary>
        public Int32 RowSpan
        {
            get { return GetAttribute(AttributeNames.RowSpan).ToInteger(0); }
            set { SetAttribute(AttributeNames.RowSpan, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets if word wrapping is suppressed.
        /// </summary>
        public Boolean NoWrap
        {
            get { return GetAttribute(AttributeNames.NoWrap).ToBoolean(false); }
            set { SetAttribute(AttributeNames.NoWrap, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the abbreviation for header cells.
        /// </summary>
        public String Abbr
        {
            get { return GetAttribute(AttributeNames.Abbr); }
            set { SetAttribute(AttributeNames.Abbr, value); }
        }

        /// <summary>
        /// Gets or sets the scope covered by header cells.
        /// </summary>
        public String Scope
        {
            get { return GetAttribute(AttributeNames.Scope); }
            set { SetAttribute(AttributeNames.Scope, value); }
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
                    _headers = new SettableTokenList(GetAttribute(AttributeNames.Headers));
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
            get { return GetAttribute(AttributeNames.Axis); }
            set { SetAttribute(AttributeNames.Axis, value); }
        }

        #endregion
    }
}
