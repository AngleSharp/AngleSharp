namespace AngleSharp.ReadOnly.Html;

using System;
using System.Collections.Generic;

public interface IReadOnlyNodeList : IEnumerable<IReadOnlyNode>
{
    IReadOnlyNode this[Int32 index] { get; }
    Int32 Length { get; }
}