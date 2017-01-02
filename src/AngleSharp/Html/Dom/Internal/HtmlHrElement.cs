﻿namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents the hr element.
    /// </summary>
    sealed class HtmlHrElement : HtmlElement, IHtmlHrElement
    {
        public HtmlHrElement(Document owner, String prefix = null)
            : base(owner, TagNames.Hr, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }
    }
}
