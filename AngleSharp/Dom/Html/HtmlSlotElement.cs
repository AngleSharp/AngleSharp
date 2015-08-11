namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents an HTML slot element.
    /// </summary>
    sealed class HtmlSlotElement : HtmlElement, IHtmlSlotElement
    {
        #region ctor

        public HtmlSlotElement(Document owner, String prefix = null)
            : base(owner, Tags.Slot, prefix)
        {
        }

        #endregion

        #region Properties

        public String Name
        {
            get { return GetOwnAttribute(AttributeNames.Name); }
            set { SetOwnAttribute(AttributeNames.Name, value); }
        }

        #endregion

        #region Methods

        public IEnumerable<INode> GetDistributedNodes()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
