using System;
using AngleSharp.Common;
using AngleSharp.Dom;
using AngleSharp.Html.Construction;
using AngleSharp.ReadOnly.Html;

class ReadOnlyHtmlElement : ReadOnlyElement, IConstructableSvgElement, IConstructableMathElement, IReadOnlyElement
{
    public ReadOnlyHtmlElement(ReadOnlyDocument? owner, StringOrMemory localName = default, StringOrMemory prefix = default, NodeFlags flags = NodeFlags.None)
        : base(owner, Combine(prefix, localName), localName, prefix, NamespaceNames.HtmlUri, flags | NodeFlags.HtmlMember)
    {
    }

    static StringOrMemory Combine(StringOrMemory prefix, StringOrMemory localName)
    {
        if (prefix.IsNullOrEmpty)
        {
            return localName;
        }
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
        return String.Concat(prefix.Memory.Span, ":", localName.Memory.Span);
#else
        return String.Concat(prefix.String, ":", localName.String);
#endif
    }

    public StringOrMemory Prefix => StringOrMemory.Empty;

    public void SetOwnAttribute(StringOrMemory name, StringOrMemory value)
    {
        _attributes ??= new ReadOnlyNamedNodeMap();
        _attributes.AddOrUpdate(name, value);
    }

    public void SetupElement()
    {
    }

    public virtual IConstructableNode ShallowCopy()
    {
        var readOnlyElement = new ReadOnlyHtmlElement(Owner, LocalName, prefix: default, Flags)
        {
            _childNodes = _childNodes
        };
        return readOnlyElement;
    }
}