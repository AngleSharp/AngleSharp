namespace AngleSharp.ReadOnly;

using Common;
using Dom;

internal class ReadOnlyHtmlOptionElement : ReadOnlyHtmlElement
{
    public ReadOnlyHtmlOptionElement(ReadOnlyHtmlDocument document, StringOrMemory prefix = default)
        : base(document, TagNames.Option, prefix, NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd | NodeFlags.HtmlSelectScoped)
    {
    }
}