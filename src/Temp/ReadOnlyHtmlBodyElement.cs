namespace AngleSharp.ReadOnly;

using Common;
using Dom;

internal class ReadOnlyHtmlBodyElement : ReadOnlyHtmlElement
{
    public ReadOnlyHtmlBodyElement(ReadOnlyHtmlDocument document, StringOrMemory prefix = default)
        : base(document, TagNames.Body, prefix, NodeFlags.HtmlFormatting)
    {
    }
}