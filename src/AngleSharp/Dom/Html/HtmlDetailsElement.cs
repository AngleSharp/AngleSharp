namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML details element.
    /// </summary>
    sealed class HtmlDetailsElement : HtmlElement, IHtmlDetailsElement
    {
        #region ctor

        public HtmlDetailsElement(Document owner, String prefix = null)
            : base(owner, TagNames.Details, prefix, NodeFlags.Special)
        {
        }

        #endregion

        #region Properties

        public Boolean IsOpen
        {
            get { return this.GetBoolAttribute(AttributeNames.Open); }
            set { this.SetBoolAttribute(AttributeNames.Open, value); }
        }

        #endregion
    }
}
