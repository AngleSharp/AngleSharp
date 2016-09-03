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

        private SettableTokenList _headers;

        #endregion

        #region ctor

        static HtmlTableCellElement()
        {
            RegisterCallback<HtmlTableCellElement>(AttributeNames.Headers, (element, value) => element._headers?.Update(value));
        }

        public HtmlTableCellElement(Document owner, String name, String prefix)
            : base(owner, name, prefix, NodeFlags.Special | NodeFlags.ImplicitelyClosed | NodeFlags.Scoped)
        {
        }

        #endregion

        #region Properties

        public Int32 Index
        {
            get
            {
                var parent = ParentElement;

                while (parent != null && !(parent is IHtmlTableRowElement))
                {
                    parent = parent.ParentElement;
                }

                var row = parent as HtmlTableRowElement;
                return row?.IndexOf(this) ?? -1;
            }
        }

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

        public String Width
        {
            get { return this.GetOwnAttribute(AttributeNames.Width); }
            set { this.SetOwnAttribute(AttributeNames.Width, value); }
        }

        public String Height
        {
            get { return this.GetOwnAttribute(AttributeNames.Height); }
            set { this.SetOwnAttribute(AttributeNames.Height, value); }
        }

        public Int32 ColumnSpan
        {
            get { return this.GetOwnAttribute(AttributeNames.ColSpan).ToInteger(0); }
            set { this.SetOwnAttribute(AttributeNames.ColSpan, value.ToString()); }
        }

        public Int32 RowSpan
        {
            get { return this.GetOwnAttribute(AttributeNames.RowSpan).ToInteger(0); }
            set { this.SetOwnAttribute(AttributeNames.RowSpan, value.ToString()); }
        }

        public Boolean NoWrap
        {
            get { return this.GetOwnAttribute(AttributeNames.NoWrap).ToBoolean(false); }
            set { this.SetOwnAttribute(AttributeNames.NoWrap, value.ToString()); }
        }

        public String Abbr
        {
            get { return this.GetOwnAttribute(AttributeNames.Abbr); }
            set { this.SetOwnAttribute(AttributeNames.Abbr, value); }
        }

        public String Scope
        {
            get { return this.GetOwnAttribute(AttributeNames.Scope); }
            set { this.SetOwnAttribute(AttributeNames.Scope, value); }
        }

        public ISettableTokenList Headers
        {
            get
            { 
                if (_headers == null)
                {
                    _headers = new SettableTokenList(this.GetOwnAttribute(AttributeNames.Headers));
                    _headers.Changed += value => UpdateAttribute(AttributeNames.Headers, value);
                }

                return _headers; 
            }
        }

        public String Axis
        {
            get { return this.GetOwnAttribute(AttributeNames.Axis); }
            set { this.SetOwnAttribute(AttributeNames.Axis, value); }
        }

        #endregion
    }
}
