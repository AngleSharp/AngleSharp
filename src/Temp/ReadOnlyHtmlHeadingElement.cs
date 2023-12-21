namespace AngleSharp.ReadOnly;

using System;
using Common;
using Dom;

internal class ReadOnlyHtmlHeadingElement : ReadOnlyHtmlElement
{
    public ReadOnlyHtmlHeadingElement(ReadOnlyHtmlDocument owner, StringOrMemory name = default, StringOrMemory prefix = default)
        : base(owner, name.OrElse(TagNames.H1), prefix, NodeFlags.Special)
    {
    }
}