namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
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
            get => this.GetOwnAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Left);
            set => this.SetOwnAttribute(AttributeNames.Align, value.ToString());
        }

        public VerticalAlignment VAlign
        {
            get => this.GetOwnAttribute(AttributeNames.Valign).ToEnum(VerticalAlignment.Middle);
            set => this.SetOwnAttribute(AttributeNames.Valign, value.ToString());
        }

        public String BgColor
        {
            get => this.GetOwnAttribute(AttributeNames.BgColor);
            set => this.SetOwnAttribute(AttributeNames.BgColor, value);
        }

        public String Width
        {
            get => this.GetOwnAttribute(AttributeNames.Width);
            set => this.SetOwnAttribute(AttributeNames.Width, value);
        }

        public String Height
        {
            get => this.GetOwnAttribute(AttributeNames.Height);
            set => this.SetOwnAttribute(AttributeNames.Height, value);
        }

        public Int32 ColumnSpan
        {
            get => LimitColSpan(this.GetOwnAttribute(AttributeNames.ColSpan).ToInteger(1));
            set => this.SetOwnAttribute(AttributeNames.ColSpan, value.ToString());
        }

        public Int32 RowSpan
        {
            get => LimitRowSpan(this.GetOwnAttribute(AttributeNames.RowSpan).ToInteger(1));
            set => this.SetOwnAttribute(AttributeNames.RowSpan, value.ToString());
        }

        public Boolean NoWrap
        {
            get => this.GetOwnAttribute(AttributeNames.NoWrap).ToBoolean(false);
            set => this.SetOwnAttribute(AttributeNames.NoWrap, value.ToString());
        }

        public String Abbr
        {
            get => this.GetOwnAttribute(AttributeNames.Abbr);
            set => this.SetOwnAttribute(AttributeNames.Abbr, value);
        }

        public String Scope
        {
            get => this.GetOwnAttribute(AttributeNames.Scope);
            set => this.SetOwnAttribute(AttributeNames.Scope, value);
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
            get => this.GetOwnAttribute(AttributeNames.Axis);
            set => this.SetOwnAttribute(AttributeNames.Axis, value);
        }

        #endregion

        #region Internal Methods

        internal void UpdateHeaders(String value)
        {
            _headers?.Update(value);
        }

        #endregion

        #region Helpers

        private static Int32 LimitColSpan(Int32 value)
        {
            return value >= 1 && value <= 1000 ? value : 1;
        }

        private static Int32 LimitRowSpan(Int32 value)
        {
            return value >= 0 ? Math.Min(value, 65534) : 1;
        }

        #endregion
    }
}
