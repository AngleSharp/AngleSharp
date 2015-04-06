namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Attributes;
    using AngleSharp.Html;

    /// <summary>
    /// Represents an HTML basefont element.
    /// Deprecated in HTML 4.01.
    /// </summary>
    [DomHistorical]
    sealed class HtmlBaseFontElement : HtmlElement
    {
        public HtmlBaseFontElement(Document owner, String prefix = null)
            : base(owner, Tags.BaseFont, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }
    }
}
