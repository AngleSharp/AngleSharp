namespace AngleSharp.ReadOnly;

using System;
using Common;
using Dom;

internal class ReadOnlyHtmlElement : ReadOnlyElement
{
    public ReadOnlyHtmlElement(ReadOnlyHtmlDocument owner, StringOrMemory localName, StringOrMemory prefix = default, NodeFlags flags = NodeFlags.None)
        : base(owner, Combine(prefix, localName), localName, prefix, NamespaceNames.HtmlUri, flags | NodeFlags.HtmlMember)
    {
    }

    static StringOrMemory Combine(StringOrMemory prefix, StringOrMemory localName)
    {
        if (prefix.IsNullOrEmpty)
        {
            return localName;
        }

        return String.Concat(prefix.Memory.Span, ":", localName.Memory.Span);

    }
}