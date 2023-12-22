namespace AngleSharp.Html.Construction;

using AngleSharp.Dom;
using Common;

internal interface IConstructableNode : IMarkupFormattable
{
    StringOrMemory NodeName { get; }
    NodeFlags Flags { get; }
    IConstructableNode? Parent { get; internal set; }
    IConstructableNodeList ChildNodes { get; }

    void RemoveFromParent();
    void RemoveChild(IConstructableNode childNode);
    void RemoveNode(int idx, IConstructableNode childNode);
    void InsertNode(int idx, IConstructableNode childNode);
    void AddNode(IConstructableNode node);
    void AppendText(StringOrMemory text, bool emitWhiteSpaceOnly = false);
    void InsertText(int idx, StringOrMemory text, bool emitWhiteSpaceOnly = false);
}