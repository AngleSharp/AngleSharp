namespace AngleSharp.ReadOnly.Html;

using System;

public interface IReadOnlyDocument : IReadOnlyNode, IDisposable
{
    IReadOnlyElement Head { get; }
    IReadOnlyElement Body { get; }
}