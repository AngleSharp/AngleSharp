namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the an HTML heading element (h1, h2, h3, h4, h5, h6).
    /// </summary>
    sealed class HtmlHeadingElement : HtmlElement, IHtmlHeadingElement
    {
        public HtmlHeadingElement(Document owner)
            : this(owner, Tags.H1)
        {
        }

        public HtmlHeadingElement(Document owner, String name, String prefix = null)
            : base(owner, name, prefix, NodeFlags.Special)
        {
        }
    }
}
