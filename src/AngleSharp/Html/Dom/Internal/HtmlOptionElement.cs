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
        private Boolean _isDirty;

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

                if (Parent is HtmlOptionsGroupElement group)
                {
                    var i = 0;

                    foreach (var child in group.ChildNodes)
                    {
                        if (Object.ReferenceEquals(child, this))
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
                _selected = value;
                _isDirty = true;
                GetSelect()?.NormalizeSelectedness(value ? this : null);
            }
        }

        #endregion

        #region Internal Methods

        internal void SetSelectedness(Boolean value, Boolean resetDirtiness = false)
        {
            _selected = value;
            if (resetDirtiness)
            {
                _isDirty = false;
            }
        }

        internal void UpdateDefaultSelectedness()
        {
            if (!_isDirty)
            {
                _selected = IsDefaultSelected;
                GetSelect()?.NormalizeSelectedness(_selected.Value ? this : null);
            }
        }

        private HtmlSelectElement? GetSelect()
        {
            for (var parent = ParentElement; parent is not null; parent = parent.ParentElement)
            {
                if (parent is HtmlSelectElement select)
                {
                    return select;
                }
            }
            return null;
        }

        #endregion
    }
}
