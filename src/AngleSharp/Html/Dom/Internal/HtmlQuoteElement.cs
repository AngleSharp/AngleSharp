namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Rperesents the HTML quote element.
    /// </summary>
    sealed class HtmlQuoteElement : HtmlElement, IHtmlQuoteElement
    {
        #region ctor

        public HtmlQuoteElement(Document owner, String name = null, String prefix = null)
            : base(owner, name ?? TagNames.Quote, prefix, name.Is(TagNames.BlockQuote) ? NodeFlags.Special : NodeFlags.None)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the citation.
        /// </summary>
        public String Citation
        {
            get => this.GetOwnAttribute(AttributeNames.Cite);
            set => this.SetOwnAttribute(AttributeNames.Cite, value);
        }

        #endregion
    }
}
