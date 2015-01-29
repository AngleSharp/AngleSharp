namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents an HTML li, dd or dt tag.
    /// </summary>
    sealed class HtmlListItemElement : HtmlElement, IHtmlListItemElement
    {
        #region ctor

        /// <summary>
        /// Creates a new item tag.
        /// </summary>
        public HtmlListItemElement(Document owner, String name)
            : base(owner, name, NodeFlags.Special | NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
        {
        }

        #endregion

        #region Properties

        public Int32? Value
        {
            get { var i = 0; return Int32.TryParse(GetAttribute(AttributeNames.Value), out i) ? i : new Int32?(); }
            set { SetAttribute(AttributeNames.Value, value.HasValue ? value.Value.ToString() : null); }
        }

        #endregion
    }
}
