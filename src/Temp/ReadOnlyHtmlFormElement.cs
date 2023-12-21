namespace AngleSharp.ReadOnly;

using System;
using Common;
using Dom;

internal class ReadOnlyHtmlFormElement : ReadOnlyHtmlElement
{
    public ReadOnlyHtmlFormElement(ReadOnlyHtmlDocument document, StringOrMemory prefix = default)
        : base(document, TagNames.Form, prefix, NodeFlags.Special)
    {
    }
}