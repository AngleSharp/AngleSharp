namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The time HTML element.
    /// </summary>
    sealed class HtmlTimeElement : HtmlElement, IHtmlTimeElement
    {
        #region ctor

        public HtmlTimeElement(Document owner, String prefix = null)
            : base(owner, TagNames.Time, prefix, NodeFlags.Special)
        {
        }

        #endregion

        #region Properties

        public String DateTime
        {
            get => this.GetOwnAttribute(AttributeNames.Datetime);
            set => this.SetOwnAttribute(AttributeNames.Datetime, value);
        }

        #endregion
    }
}
