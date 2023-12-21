namespace AngleSharp.ReadOnly;

using Common;
using Dom;

internal class ReadOnlyDocumentType : ReadOnlyNode
{
    public ReadOnlyDocumentType(ReadOnlyHtmlDocument document, StringOrMemory nameString) : base(document, nameString, NodeType.DocumentType)
    {
    }

    public StringOrMemory SystemIdentifier { get; set; }
    public StringOrMemory PublicIdentifier { get; set; }

    public override INode Clone(bool deep)
    {
        throw new System.NotImplementedException();
    }
}