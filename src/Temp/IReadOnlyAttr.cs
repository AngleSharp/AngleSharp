namespace AngleSharp.ReadOnly;

using Common;

public interface IReadOnlyAttr
{
    public StringOrMemory Name { get; }
    public StringOrMemory Value { get; }
}