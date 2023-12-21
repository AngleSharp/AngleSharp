namespace AngleSharp.ReadOnly;

using System;
using Common;
using Dom;

internal class ReadOnlyHtmlAnchorElement : ReadOnlyHtmlElement
{
    public ReadOnlyHtmlAnchorElement(ReadOnlyHtmlDocument document, StringOrMemory prefix = default)
        : base(document, TagNames.A, prefix, NodeFlags.HtmlFormatting)
    {
    }
}