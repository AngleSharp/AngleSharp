namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
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
        public HtmlListItemElement(Document owner, String name = null, String prefix = null)
            : base(owner, name ?? TagNames.Li, prefix, NodeFlags.Special | NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
        {
        }

        #endregion

        #region Properties

        public Int32? Value
        {
            get { var i = 0; return Int32.TryParse(this.GetOwnAttribute(AttributeNames.Value), out i) ? i : new Int32?(); }
            set { this.SetOwnAttribute(AttributeNames.Value, value.HasValue ? value.Value.ToString() : null); }
        }

        #endregion
    }
}
