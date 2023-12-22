namespace AngleSharp.Html.Construction;

using System;
using Common;

internal interface IConstructableNamedNodeMap
{
    IConstructableAttr? this[StringOrMemory name] { get; }
    Int32 Length { get; }
    bool SameAs(IConstructableNamedNodeMap? attributes);
}