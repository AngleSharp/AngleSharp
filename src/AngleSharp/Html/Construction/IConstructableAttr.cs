namespace AngleSharp.Html.Construction;

using Common;

internal interface IConstructableAttr
{
    public StringOrMemory Name { get; }
    public StringOrMemory Value { get; internal set; }
};