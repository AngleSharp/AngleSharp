namespace AngleSharp.ReadOnly.Html;

using Common;
using Dom;

public interface IReadOnlyElement : IReadOnlyNode
{
    StringOrMemory NamespaceUri { get; }
    StringOrMemory LocalName { get; }
    IReadOnlyNamedNodeMap Attributes { get; }
    ISourceReference? SourceReference { get; }
}