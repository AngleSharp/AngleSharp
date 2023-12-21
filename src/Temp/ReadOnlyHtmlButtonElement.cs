namespace AngleSharp.ReadOnly;

using System;
using Common;
using Dom;

internal class ReadOnlyHtmlButtonElement : ReadOnlyHtmlElement
{
    public ReadOnlyHtmlButtonElement(ReadOnlyHtmlDocument document, StringOrMemory prefix = default)
        : base(document, TagNames.Button, prefix)
    {
    }
}