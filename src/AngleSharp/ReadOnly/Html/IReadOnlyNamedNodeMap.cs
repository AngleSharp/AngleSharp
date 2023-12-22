namespace AngleSharp.ReadOnly.Html;

using System;
using System.Collections.Generic;
using Common;

public interface IReadOnlyNamedNodeMap : IEnumerable<IReadOnlyAttr>
{
    IReadOnlyAttr? this[StringOrMemory name] { get; }
    Int32 Length { get; }
}