namespace AngleSharp.ReadOnly.Html;

using Common;

public interface IReadOnlyTextNode
{
    StringOrMemory Content { get; }
}