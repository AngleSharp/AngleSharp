using AngleSharp.Common;
using AngleSharp.Html.Construction;
using AngleSharp.ReadOnly.Html;

internal class ReadOnlyAttr : IReadOnlyAttr, IConstructableAttr
{
    public StringOrMemory Name { get; }
    public StringOrMemory Value { get; set; }

    public ReadOnlyAttr(StringOrMemory name, StringOrMemory value)
    {
        Name = name;
        Value = value;
    }
}