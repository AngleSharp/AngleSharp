namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Rperesents the HTML quote element.
    /// </summary>
    sealed class HTMLQuoteElement : HTMLElement, IHtmlQuoteElement
    {
        internal HTMLQuoteElement()
        { }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return _name.Equals(Tags.BlockQuote); }
        }

        public String Citation
        {
            get { return GetAttribute(AttributeNames.Cite); }
            set { SetAttribute(AttributeNames.Cite, value); }
        }
    }
}
