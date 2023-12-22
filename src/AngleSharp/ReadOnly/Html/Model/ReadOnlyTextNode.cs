using System.IO;
using AngleSharp;
using AngleSharp.Common;
using AngleSharp.Dom;
using AngleSharp.ReadOnly.Html;

internal class ReadOnlyTextNode : ReadOnlyCharacterData, IReadOnlyTextNode
{
    public ReadOnlyTextNode(ReadOnlyDocument? owner, StringOrMemory content)
        :  base(owner, "#text", NodeType.Text, content)
    {
    }

    public override void ToHtml(TextWriter writer, IMarkupFormatter formatter)
    {
        writer.Write(Content.Memory.Span);
    }
}