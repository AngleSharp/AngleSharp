namespace AngleSharp.ReadOnly;

using Common;
using Dom;

internal class ReadOnlyHtmlTableRowElement : ReadOnlyElement
{
    public ReadOnlyHtmlTableRowElement(ReadOnlyHtmlDocument document) : base(document, StringOrMemory.Empty, NodeType.Element)
    {
    }
}