namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Rperesents the HTML quote element.
    /// </summary>
    sealed class HtmlQuoteElement : HtmlElement, IHtmlQuoteElement
    {
        #region ctor

        public HtmlQuoteElement(Document owner)
            : this(owner, Tags.Quote)
        {
        }

        public HtmlQuoteElement(Document owner, String name, String prefix = null)
            : base(owner, name, prefix, name.Equals(Tags.BlockQuote) ? NodeFlags.Special : NodeFlags.None)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the citation.
        /// </summary>
        public String Citation
        {
            get { return GetOwnAttribute(AttributeNames.Cite); }
            set { SetOwnAttribute(AttributeNames.Cite, value); }
        }

        #endregion
    }
}
