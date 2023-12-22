namespace AngleSharp.ReadOnly.Html;

using Common;

public interface IReadOnlyAttr
{
    public StringOrMemory Name { get; }
    public StringOrMemory Value { get; internal set; }
}