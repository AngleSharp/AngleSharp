namespace AngleSharp.ReadOnly;

using System;
using Common;
using Dom;

internal class ReadOnlyHtmlHrElement : ReadOnlyHtmlElement
{
    public ReadOnlyHtmlHrElement(ReadOnlyHtmlDocument document, StringOrMemory prefix = default)
        : base(document, TagNames.Hr, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
    {
    }
}