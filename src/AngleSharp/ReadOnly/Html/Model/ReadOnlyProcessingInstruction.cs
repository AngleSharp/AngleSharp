using System.IO;
using AngleSharp;
using AngleSharp.Common;
using AngleSharp.Dom;
using AngleSharp.ReadOnly.Html;

internal class ReadOnlyProcessingInstruction : ReadOnlyCharacterData, IReadOnlyProcessingInstructionNode
{
    private ReadOnlyProcessingInstruction(ReadOnlyDocument? owner, StringOrMemory name)
        : base(owner, name, NodeType.ProcessingInstruction)
    {
    }

    public static ReadOnlyProcessingInstruction Create(ReadOnlyDocument? owner, StringOrMemory tokenData)
    {
        return new ReadOnlyProcessingInstruction(owner, tokenData);
    }

    public override void ToHtml(TextWriter writer, IMarkupFormatter formatter)
    {
        writer.Write("<?");
        writer.Write(NodeName.Memory.Span);
        writer.Write(" ");
        writer.Write(Content.Memory.Span);
        writer.Write("?>");
    }
}