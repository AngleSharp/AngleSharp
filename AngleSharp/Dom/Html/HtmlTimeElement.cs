namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// The time HTML element.
    /// </summary>
    sealed class HtmlTimeElement : HtmlElement, IHtmlTimeElement
    {
        #region ctor

        public HtmlTimeElement(Document owner, String prefix = null)
            : base(owner, Tags.Time, prefix, NodeFlags.Special)
        {
        }

        #endregion

        #region Properties

        public String DateTime
        {
            get { return GetOwnAttribute(AttributeNames.Datetime); }
            set { SetOwnAttribute(AttributeNames.Datetime, value); }
        }

        #endregion
    }
}
