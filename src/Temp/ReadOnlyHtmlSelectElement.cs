namespace AngleSharp.ReadOnly;

using System;
using Common;
using Dom;

internal class ReadOnlyHtmlSelectElement : ReadOnlyHtmlElement
{
    public ReadOnlyHtmlSelectElement(ReadOnlyHtmlDocument document, StringOrMemory prefix = default):
        base(document, TagNames.Select, prefix)
    {
    }

}