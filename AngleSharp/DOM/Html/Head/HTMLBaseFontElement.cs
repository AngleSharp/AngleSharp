namespace AngleSharp.DOM.Html
{
    using AngleSharp.Attributes;
    using AngleSharp.Html;

    /// <summary>
    /// Represents an HTML basefont element.
    /// Deprecated in HTML 4.01.
    /// </summary>
    [DomHistorical]
    sealed class HTMLBaseFontElement : HTMLElement
    {
        internal HTMLBaseFontElement()
            : base(Tags.BaseFont, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }
    }
}
