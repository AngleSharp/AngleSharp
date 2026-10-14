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
        private HtmlSelectElement? _select;

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
                _select?.NormalizeSelectedness();
            }
        }

        #endregion

        #region Internal Methods

        internal HtmlSelectElement? SelectionOwner => _select;

        /// <summary>
        /// Changes selectedness without making an implicit peer or reset operation dirty.
        /// </summary>
        internal void SetSelectedness(Boolean value)
        {
            _selected = value;

            if (value && _select is { IsMultiple: false } select)
            {
                foreach (var option in select.Options)
                {
                    if (option is HtmlOptionElement other && !ReferenceEquals(this, other) && other._select == select)
                    {
                        other._selected = false;
                    }
                }
            }
        }

        internal void UpdateSelectedness()
        {
            if (!_dirty)
            {
                SetSelectedness(IsDefaultSelected);
            }

            _select?.NormalizeSelectedness();
        }

        internal void ResetSelectedness()
        {
            _dirty = false;
            SetSelectedness(IsDefaultSelected);
        }

        /// <summary>
        /// Runs option insertion and removal steps for both the previous and current select.
        /// </summary>
        internal void UpdateSelect()
        {
            var previous = _select;
            _select = Parent as HtmlSelectElement ?? (Parent as HtmlOptionsGroupElement)?.Parent as HtmlSelectElement;

            if (previous != _select)
            {
                if (IsSelected)
                {
                    SetSelectedness(true);
                }

                previous?.NormalizeSelectedness();
                _select?.NormalizeSelectedness();
            }
        }

        internal override void SetupElement()
        {
            base.SetupElement();
            UpdateSelect();
        }

        protected override void OnParentChanged()
        {
            base.OnParentChanged();
            UpdateSelect();
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
