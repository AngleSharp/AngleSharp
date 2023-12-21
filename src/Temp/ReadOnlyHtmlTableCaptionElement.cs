namespace AngleSharp.ReadOnly;

using Common;
using Dom;

internal class ReadOnlyHtmlTableCaptionElement : ReadOnlyHtmlElement
{
    // public HtmlTableCaptionElement(Document owner, String? prefix = null)
    //     : base(owner, TagNames.Caption, prefix, NodeFlags.Special | NodeFlags.Scoped)
    // {
    // }

    public ReadOnlyHtmlTableCaptionElement(ReadOnlyHtmlDocument document, StringOrMemory prefix = default)
        : base(document, TagNames.Caption, prefix, NodeFlags.Special | NodeFlags.Scoped)
    {
    }
}