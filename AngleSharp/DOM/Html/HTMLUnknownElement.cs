namespace AngleSharp.Dom.Html
{
    using System;

    /// <summary>
    /// The class for an unknown HTML element.
    /// </summary>
    sealed class HTMLUnknownElement : HTMLElement, IHtmlUnknownElement
    {
        public HTMLUnknownElement(Document owner, String name)
            : base(owner, name)
        {
        }
    }
}
