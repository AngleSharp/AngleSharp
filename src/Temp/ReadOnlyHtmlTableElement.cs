namespace AngleSharp.ReadOnly;

using System;
using Common;
using Dom;

internal class ReadOnlyHtmlTableElement : ReadOnlyElement
{
    public ReadOnlyHtmlTableElement(ReadOnlyHtmlDocument document) : base(document, StringOrMemory.Empty, NodeType.Element)
    {
    }
}