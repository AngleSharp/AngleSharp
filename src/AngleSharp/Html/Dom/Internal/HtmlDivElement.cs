﻿namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents the HTML div element.
    /// </summary>
    sealed class HtmlDivElement : HtmlElement, IHtmlDivElement
    {
        public HtmlDivElement(Document owner, String prefix = null)
            : base(owner, TagNames.Div, prefix, NodeFlags.Special)
        {
        }
    }
}
