namespace AngleSharp.ReadOnly;

using System;
using System.Collections.Generic;

public interface IReadOnlyNamedNodeMap : IEnumerable<IReadOnlyAttr>
{
    IReadOnlyAttr? this[Int32 index] { get; }
    IReadOnlyAttr? this[String name] { get; }
    Int32 Length { get; }
    bool SameAs(IReadOnlyNamedNodeMap? elementAttributes);
    void FastAddItem(IReadOnlyAttr item);
}