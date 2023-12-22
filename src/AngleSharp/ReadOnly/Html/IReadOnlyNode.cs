namespace AngleSharp.ReadOnly.Html;

using Common;
using Dom;

public interface IReadOnlyNode: IMarkupFormattable
{
    StringOrMemory NodeName { get; }
    NodeFlags Flags { get; }
    IReadOnlyNode? Parent { get; }
    IReadOnlyNodeList ChildNodes { get; }
}