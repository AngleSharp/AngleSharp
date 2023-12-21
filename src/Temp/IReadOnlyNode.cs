namespace AngleSharp.ReadOnly;

using Dom;

public interface IReadOnlyNode
{
    public INode Clone(bool b);
}