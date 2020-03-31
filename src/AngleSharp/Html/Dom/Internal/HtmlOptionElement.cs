namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the HTML option element.
    /// </summary>
    sealed class HtmlOptionElement : HtmlElement, IHtmlOptionElement
    {
        #region Fields

        private Boolean? _selected;

        #endregion

        #region ctor

        public HtmlOptionElement(Document owner, String prefix = null)
            : base(owner, TagNames.Option, prefix, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd | NodeFlags.HtmlSelectScoped)
        {
        }

        #endregion           
        
        #region Properties

        public Boolean IsDisabled
        {
            get => this.GetBoolAttribute(AttributeNames.Disabled);
            set => this.SetBoolAttribute(AttributeNames.Disabled, value);
        }

        public IHtmlFormElement Form => GetAssignedForm();

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
                            return i;

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
            get => _selected.HasValue ? _selected.Value : IsDefaultSelected;
            set => _selected = value;
        }

        #endregion
    }
}
