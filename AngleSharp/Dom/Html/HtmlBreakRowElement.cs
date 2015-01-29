namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML br element.
    /// </summary>
    sealed class HtmlBreakRowElement : HtmlElement, IHtmlBreakRowElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML br element
        /// </summary>
        public HtmlBreakRowElement(Document owner)
            : base(owner, Tags.Br, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }

        #endregion
    }
}
