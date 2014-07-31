namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// The class for an unknown HTML element.
    /// </summary>
    sealed class HTMLUnknownElement : HTMLElement, IHtmlUnknownElement
    {
        internal HTMLUnknownElement(String name)
            : base(name)
        { }
    }
}
