namespace AngleSharp.ReadOnly;

using Common;

internal class ReadOnlyAttr : IReadOnlyAttr
{
    public StringOrMemory Name { get; }
    public StringOrMemory Value { get; }

    public ReadOnlyAttr(StringOrMemory name, StringOrMemory value)
    {
        Name = name;
        Value = value;
    }
}