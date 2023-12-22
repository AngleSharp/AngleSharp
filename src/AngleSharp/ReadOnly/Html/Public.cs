namespace AngleSharp.ReadOnly.Html;

using System;
using System.Collections.Generic;
using AngleSharp.Html.Parser.Tokens;
using Common;
using Dom;

public interface IReadOnlyAttr
{
    public StringOrMemory Name { get; }
    public StringOrMemory Value { get; internal set; }
}

public interface IReadOnlyNode: IMarkupFormattable
{
    StringOrMemory NodeName { get; }
    NodeFlags Flags { get; }
    IReadOnlyNode? Parent { get; }
    IReadOnlyNodeList ChildNodes { get; }
}

public interface IReadOnlyTextNode
{
    StringOrMemory Content { get; }
}

public interface IReadOnlyCommentNode
{
    StringOrMemory Content { get; }
}

public interface IReadOnlyProcessingInstructionNode
{
    StringOrMemory Content { get; }
}

public interface IReadOnlyNodeList : IEnumerable<IReadOnlyNode>
{
    IReadOnlyNode this[Int32 index] { get; }
    Int32 Length { get; }
}

public interface IReadOnlyElement : IReadOnlyNode
{
    StringOrMemory NamespaceUri { get; }
    StringOrMemory LocalName { get; }
    IReadOnlyNamedNodeMap Attributes { get; }
    ISourceReference? SourceReference { get; }
}

public interface IReadOnlyDocument : IReadOnlyNode, IDisposable
{
    IReadOnlyElement Head { get; }
    IReadOnlyElement Body { get; }
}

public interface IReadOnlyNamedNodeMap : IEnumerable<IReadOnlyAttr>
{
    IReadOnlyAttr? this[StringOrMemory name] { get; }
    Int32 Length { get; }
}
