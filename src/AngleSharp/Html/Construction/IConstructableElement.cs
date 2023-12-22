namespace AngleSharp.Html.Construction;

using System;
using AngleSharp.Dom;
using Common;
using Parser.Tokens.Struct;

internal interface IConstructableElement : IConstructableNode
{
    StringOrMemory NamespaceUri { get; }
    StringOrMemory LocalName { get; }
    StringOrMemory Prefix { get; }

    IConstructableNamedNodeMap Attributes { get; }
    ISourceReference? SourceReference { get; set; }

    void SetAttribute(String? ns, StringOrMemory name, StringOrMemory value);
    void SetOwnAttribute(StringOrMemory name, StringOrMemory value);
    StringOrMemory GetAttribute(StringOrMemory @namespace, StringOrMemory name);
    void SetAttributes(StructAttributes tagAttributes);
    bool HasAttribute(StringOrMemory name);
    void SetupElement();

    void AddComment(ref StructHtmlToken token);

    IConstructableNode ShallowCopy();
}