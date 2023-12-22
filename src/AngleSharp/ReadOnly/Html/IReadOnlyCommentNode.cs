namespace AngleSharp.ReadOnly.Html;

using Common;

public interface IReadOnlyCommentNode
{
    StringOrMemory Content { get; }
}