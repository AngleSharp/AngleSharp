namespace AngleSharp.Html.Construction;

using System;
using System.Collections.Generic;

internal interface IConstructableNodeList : IEnumerable<IConstructableNode>
{
    IConstructableNode this[Int32 index] { get; }
    Int32 Length { get; }
    void Clear();
}