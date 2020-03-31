namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents a param element.
    /// </summary>
    sealed class HtmlParamElement : HtmlElement, IHtmlParamElement
    {
        #region ctor

        public HtmlParamElement(Document owner, String prefix = null)
            : base(owner, TagNames.Param, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }

        #endregion

        #region Properties

        public String Value
        {
            get => this.GetOwnAttribute(AttributeNames.Value);
            set => this.SetOwnAttribute(AttributeNames.Value, value);
        }

        public String Name
        {
            get => this.GetOwnAttribute(AttributeNames.Name);
            set => this.SetOwnAttribute(AttributeNames.Name, value);
        }

        #endregion
    }
}
