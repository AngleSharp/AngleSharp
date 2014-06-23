namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the an HTML heading element (h1, h2, h3, h4, h5, h6).
    /// </summary>
    sealed class HTMLHeadingElement : HTMLElement, IHtmlHeadingElement
    {
        internal HTMLHeadingElement()
        { }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }
    }
}
