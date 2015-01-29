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

        public HtmlQuoteElement(Document owner, String name)
            : base(owner, name, name.Equals(Tags.BlockQuote) ? NodeFlags.Special : NodeFlags.None)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the citation.
        /// </summary>
        public String Citation
        {
            get { return GetAttribute(AttributeNames.Cite); }
            set { SetAttribute(AttributeNames.Cite, value); }
        }

        #endregion
    }
}
