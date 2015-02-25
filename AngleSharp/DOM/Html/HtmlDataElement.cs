namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML data element.
    /// </summary>
    sealed class HtmlDataElement : HtmlElement, IHtmlDataElement
    {
        #region ctor

        public HtmlDataElement(Document owner)
            : base(owner, Tags.Data)
        {
            Owner = owner;
        }

        #endregion

        #region Properties

        public String Value
        {
            get { return GetAttribute(AttributeNames.Value); }
            set { SetAttribute(AttributeNames.Value, value); }
        }

        #endregion
    }
}
