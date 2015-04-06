namespace AngleSharp.Dom.Html
{
    using System;
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
        public HtmlBreakRowElement(Document owner, String prefix = null)
            : base(owner, Tags.Br, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }

        #endregion
    }
}
