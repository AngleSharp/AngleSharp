namespace AngleSharp.ReadOnly;

using Common;
using Dom;

internal class ReadOnlyHtmlTableColgroupElement : ReadOnlyElement
{
    public ReadOnlyHtmlTableColgroupElement(ReadOnlyHtmlDocument document) : base(document, StringOrMemory.Empty, NodeType.Element)
    {
    }
}