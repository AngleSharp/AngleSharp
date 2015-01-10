namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the an HTML heading element (h1, h2, h3, h4, h5, h6).
    /// </summary>
    sealed class HTMLHeadingElement : HTMLElement, IHtmlHeadingElement
    {
        public HTMLHeadingElement(Document owner, String name)
            : base(name, NodeFlags.Special)
        {
            Owner = owner;
        }
    }
}
