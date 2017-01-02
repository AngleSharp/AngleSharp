﻿namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The class for an unknown HTML element.
    /// </summary>
    sealed class HtmlUnknownElement : HtmlElement, IHtmlUnknownElement
    {
        public HtmlUnknownElement(Document owner, String localName, String prefix = null)
            : base(owner, localName, prefix)
        {
        }
    }
}
