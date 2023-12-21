namespace AngleSharp.ReadOnly;

using Common;
using Dom;

internal class ReadOnlyHtmlTableSectionElement : ReadOnlyElement
{
    public ReadOnlyHtmlTableSectionElement(ReadOnlyHtmlDocument document, StringOrMemory tagName) : base(document, tagName, NodeType.Element)
    {
    }
}