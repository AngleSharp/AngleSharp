namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// The time HTML element.
    /// </summary>
    sealed class HTMLTimeElement : HTMLElement, IHtmlTimeElement
    {
        #region ctor

        public HTMLTimeElement(Document owner)
            : base(owner, Tags.Time, NodeFlags.Special)
        {
        }

        #endregion

        #region Properties

        public String DateTime
        {
            get { return GetAttribute(AttributeNames.Datetime); }
            set { SetAttribute(AttributeNames.Datetime, value); }
        }

        #endregion
    }
}
