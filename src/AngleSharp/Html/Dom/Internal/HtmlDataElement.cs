namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents the HTML data element.
    /// </summary>
    sealed class HtmlDataElement : HtmlElement, IHtmlDataElement
    {
        #region ctor

        public HtmlDataElement(Document owner, String prefix = null)
            : base(owner, TagNames.Data, prefix)
        {
        }

        #endregion

        #region Properties

        public String Value
        {
            get => this.GetOwnAttribute(AttributeNames.Value);
            set => this.SetOwnAttribute(AttributeNames.Value, value);
        }

        #endregion
    }
}
