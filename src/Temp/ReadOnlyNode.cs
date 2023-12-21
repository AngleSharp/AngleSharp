namespace AngleSharp.ReadOnly;

using Common;
using Dom;

internal abstract class ReadOnlyNode : IReadOnlyNode
{
    private readonly NodeType _type;
    private readonly NodeFlags _flags;
    private readonly ReadOnlyElement? _owner;
    private readonly StringOrMemory _nodeName;

    public StringOrMemory NodeName => _nodeName;
    public NodeFlags Flags => _flags;

    public ReadOnlyNode(ReadOnlyElement? owner, StringOrMemory name, NodeType type = NodeType.Element, NodeFlags flags = NodeFlags.None)
    {
        // _owner = owner;
        _nodeName = name;
        _type = type;
        _flags = flags;
    }

    public abstract INode Clone(bool deep);

    public ReadOnlyElement? Owner => _owner;

}