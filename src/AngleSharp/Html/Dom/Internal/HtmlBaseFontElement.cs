namespace AngleSharp.Html.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents an HTML basefont element.
    /// Deprecated in HTML 4.01.
    /// </summary>
    [DomHistorical]
    sealed class HtmlBaseFontElement : HtmlElement
    {
        public HtmlBaseFontElement(Document owner, String prefix = null)
            : base(owner, TagNames.BaseFont, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }
    }
}
