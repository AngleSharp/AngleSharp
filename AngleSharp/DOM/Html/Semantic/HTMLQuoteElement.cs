namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Rperesents the HTML quote element.
    /// </summary>
    sealed class HTMLQuoteElement : HTMLElement, IHtmlQuoteElement
    {
        #region ctor

        internal HTMLQuoteElement(String name)
            : base(name, name.Equals(Tags.BlockQuote) ? NodeFlags.Special : NodeFlags.None)
        { }

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
