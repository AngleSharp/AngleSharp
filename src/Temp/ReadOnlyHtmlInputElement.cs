namespace AngleSharp.ReadOnly;

using System;
using Common;
using Dom;

internal class ReadOnlyHtmlInputElement : ReadOnlyHtmlElement
{
    public ReadOnlyHtmlInputElement(ReadOnlyHtmlDocument document, StringOrMemory prefix = default)
        : base(document, TagNames.Input, prefix, NodeFlags.SelfClosing)
    {

    }
}