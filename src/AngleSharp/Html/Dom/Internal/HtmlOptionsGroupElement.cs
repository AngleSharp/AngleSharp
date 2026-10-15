namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents the HTML optgroup element.
    /// </summary>
    sealed class HtmlOptionsGroupElement : HtmlElement, IHtmlOptionsGroupElement
    {
        #region ctor

        public HtmlOptionsGroupElement(Document owner, String? prefix = null)
            : base(owner, TagNames.Optgroup, prefix, NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd | NodeFlags.HtmlSelectScoped)
        {
        }

        #endregion

        #region Properties

        public String? Label
        {
            get => this.GetOwnAttribute(AttributeNames.Label);
            set => this.SetOwnAttribute(AttributeNames.Label, value);
        }
        public Boolean IsDisabled
        {
            get => this.GetBoolAttribute(AttributeNames.Disabled);
            set => this.SetBoolAttribute(AttributeNames.Disabled, value);
        }

        #endregion

        protected override void NodeIsInserted(Node newNode)
        {
            base.NodeIsInserted(newNode);
            if (newNode is HtmlOptionElement)
            {
                (Parent as HtmlSelectElement)?.UpdateOptions(newNode, true);
            }
        }

        protected override void NodeIsRemoved(Node removedNode, Node? oldPreviousSibling)
        {
            base.NodeIsRemoved(removedNode, oldPreviousSibling);
            if (removedNode is HtmlOptionElement)
            {
                (Parent as HtmlSelectElement)?.UpdateOptions(removedNode, false);
            }
        }
    }
}
