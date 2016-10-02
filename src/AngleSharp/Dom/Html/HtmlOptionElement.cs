namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
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
            get { return this.GetBoolAttribute(AttributeNames.Disabled); }
            set { this.SetBoolAttribute(AttributeNames.Disabled, value); }
        }

        public IHtmlFormElement Form
        {
            get { return GetAssignedForm(); }
        }

        public String Label
        {
            get { return this.GetOwnAttribute(AttributeNames.Label) ?? Text; }
            set { this.SetOwnAttribute(AttributeNames.Label, value); }
        }

        public String Value
        {
            get { return this.GetOwnAttribute(AttributeNames.Value) ?? Text; }
            set { this.SetOwnAttribute(AttributeNames.Value, value); }
        }

        public Int32 Index
        {
            get
            {
                var group = Parent as HtmlOptionsGroupElement;

                if (group != null)
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
            get { return TextContent.CollapseAndStrip(); }
            set { TextContent = value; }
        }

        public Boolean IsDefaultSelected
        {
            get { return this.GetBoolAttribute(AttributeNames.Selected); }
            set { this.SetBoolAttribute(AttributeNames.Selected, value); }
        }

        public Boolean IsSelected
        {
            get { return _selected.HasValue ? _selected.Value : IsDefaultSelected; }
            set { _selected = value; }
        }

        #endregion
    }
}
