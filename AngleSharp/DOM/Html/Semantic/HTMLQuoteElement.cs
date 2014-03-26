namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Rperesents the HTML quote element.
    /// </summary>
    [DOM("HTMLQuoteElement")]
    public sealed class HTMLQuoteElement : HTMLElement
    {
        internal HTMLQuoteElement()
        { }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return _name.Equals(Tags.BLOCKQUOTE); }
        }
    }
}
