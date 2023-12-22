using System.IO;
using AngleSharp;
using AngleSharp.Common;
using AngleSharp.Dom;
using AngleSharp.ReadOnly.Html;

internal class ReadOnlyComment : ReadOnlyCharacterData, IReadOnlyCommentNode
{
    public ReadOnlyComment(ReadOnlyDocument? owner, StringOrMemory tokenData)
        : base(owner, "#comment", NodeType.Comment, tokenData)
    {
    }

    public override void ToHtml(TextWriter writer, IMarkupFormatter formatter)
    {
        writer.Write("<!--");
        writer.Write(Content.Memory.Span);
        writer.Write("-->");
    }
}