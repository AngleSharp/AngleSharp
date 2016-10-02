namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML optgroup element.
    /// </summary>
    sealed class HtmlOptionsGroupElement : HtmlElement, IHtmlOptionsGroupElement
    {
        #region ctor

        public HtmlOptionsGroupElement(Document owner, String prefix = null)
            : base(owner, TagNames.Optgroup, prefix, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd | NodeFlags.HtmlSelectScoped)
        {
        }

        #endregion

        #region Properties

        public String Label
        {
            get { return this.GetOwnAttribute(AttributeNames.Label); }
            set { this.SetOwnAttribute(AttributeNames.Label, value); }
        }
        public Boolean IsDisabled
        {
            get { return this.GetBoolAttribute(AttributeNames.Disabled); }
            set { this.SetBoolAttribute(AttributeNames.Disabled, value); }
        }

        #endregion
    }
}
