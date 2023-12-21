namespace AngleSharp.ReadOnly;

using Common;
using Dom;

internal class ReadOnlyHtmlTableColElement : ReadOnlyElement
{
    public ReadOnlyHtmlTableColElement(ReadOnlyHtmlDocument document) : base(document, StringOrMemory.Empty, NodeType.Element)
    {
    }
}