namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents the HTML option element.
    /// </summary>
    sealed class HtmlOptionElement : HtmlElement, IHtmlOptionElement
    {
        #region Fields

        private Boolean? _selected;
        private Boolean _dirty;

        #endregion

        #region ctor

        public HtmlOptionElement(Document owner, String? prefix = null)
            : base(owner, TagNames.Option, prefix, NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd | NodeFlags.HtmlSelectScoped)
        {
        }

        #endregion           
        
        #region Properties

        public Boolean IsDisabled
        {
            get => this.GetBoolAttribute(AttributeNames.Disabled);
            set => this.SetBoolAttribute(AttributeNames.Disabled, value);
        }

        public IHtmlFormElement? Form => GetAssignedForm();

        [AllowNull]
        public String Label
        {
            get => this.GetOwnAttribute(AttributeNames.Label) ?? Text;
            set => this.SetOwnAttribute(AttributeNames.Label, value);
        }

        public String Value
        {
            get => this.GetOwnAttribute(AttributeNames.Value) ?? Text;
            set => this.SetOwnAttribute(AttributeNames.Value, value);
        }

        public Int32 Index
        {
            get
            {

                var select = this.GetAncestor<HtmlSelectElement>();

                if (select is not null)
                {
                    var i = 0;

                    foreach (var option in select.Options)
                    {
                        if (Object.ReferenceEquals(option, this))
                        {
                            return i;
                        }

                        i++;
                    }
                }

                return 0;
            }
        }

        public String Text
        {
            get => TextContent.CollapseAndStrip();
            set => TextContent = value;
        }

        public Boolean IsDefaultSelected
        {
            get => this.GetBoolAttribute(AttributeNames.Selected);
            set => this.SetBoolAttribute(AttributeNames.Selected, value);
        }

        public Boolean IsSelected
        {
            get => _selected ?? IsDefaultSelected;
            set
            {
                _dirty = true;
                SetSelectedness(value);
                SelectionOwner?.NormalizeSelectedness();
            }
        }

        #endregion

        #region Internal Methods

        internal HtmlSelectElement? SelectionOwner =>
            Parent as HtmlSelectElement ?? (Parent as HtmlOptionsGroupElement)?.Parent as HtmlSelectElement;

        /// <summary>
        /// Changes selectedness without making an implicit peer or reset operation dirty.
        /// </summary>
        internal void SetSelectedness(Boolean value)
        {
            _selected = value;
            SelectionOwner?.UpdateSelectedOption(this, value);
        }

        internal void UpdateSelectedness()
        {
            if (!_dirty)
            {
                SetSelectedness(IsDefaultSelected);
            }

            SelectionOwner?.NormalizeSelectedness();
        }

        internal void ResetSelectedness()
        {
            _dirty = false;
            SetSelectedness(IsDefaultSelected);
        }

        internal override void SetupElement()
        {
            base.SetupElement();
            SelectionOwner?.UpdateOptions(this, true);
        }

        /// <inheritdoc />
        public override Node Clone(Document owner, Boolean deep)
        {
            var node = (HtmlOptionElement)base.Clone(owner, deep);
            node._selected = _selected;
            node._dirty = _dirty;
            return node;
        }

        #endregion
    }
}
