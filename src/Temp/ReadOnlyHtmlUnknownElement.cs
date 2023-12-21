namespace AngleSharp.ReadOnly;

using Common;
using Dom;

internal class ReadOnlyHtmlUnknownElement : ReadOnlyElement
{
    public ReadOnlyHtmlUnknownElement(ReadOnlyHtmlDocument document, StringOrMemory tagName) : base(document, tagName, NodeType.Element)
    {
    }
}