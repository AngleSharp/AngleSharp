namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;
    using System.Globalization;

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
            get => Int32.TryParse(this.GetOwnAttribute(AttributeNames.Value), NumberStyles.Integer, CultureInfo.InvariantCulture, out var i) ? i : new Int32?();
            set => this.SetOwnAttribute(AttributeNames.Value, value.HasValue ? value.Value.ToString() : null);
        }

        #endregion
    }
}
