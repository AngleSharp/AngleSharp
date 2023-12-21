namespace AngleSharp.ReadOnly;

using Common;
using Dom;

internal class ReadOnlyHtmlOptionsGroupElement : ReadOnlyHtmlElement
{
    public ReadOnlyHtmlOptionsGroupElement(ReadOnlyHtmlDocument document, StringOrMemory prefix = default)
        : base(document, TagNames.Optgroup, prefix, NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd | NodeFlags.HtmlSelectScoped)
    {
    }
}