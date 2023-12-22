namespace AngleSharp.ReadOnly.Html;

using Common;

public interface IReadOnlyProcessingInstructionNode
{
    StringOrMemory Content { get; }
}