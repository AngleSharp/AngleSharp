using System.IO;
using AngleSharp;
using AngleSharp.Common;
using AngleSharp.Dom;

internal class ReadOnlyDocumentType : ReadOnlyNode
{
    public ReadOnlyDocumentType(ReadOnlyDocument document, StringOrMemory nameString) : base(document, nameString, NodeType.DocumentType)
    {
    }

    public StringOrMemory SystemIdentifier { get; set; }
    public StringOrMemory PublicIdentifier { get; set; }

    public override void ToHtml(TextWriter writer, IMarkupFormatter formatter)
    {
        writer.Write("<!DOCTYPE html ");
        writer.Write(PublicIdentifier.Memory.Span);
        writer.Write(" ");
        writer.Write(SystemIdentifier.Memory.Span);
        writer.WriteLine(">");
        base.ToHtml(writer, formatter);
    }
}