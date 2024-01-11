namespace AngleSharp.Html.Construction;

using AngleSharp.Dom;
using Text;

internal class SourceReference : ISourceReference
{
    public SourceReference(TextPosition position)
    {
        Position = position;
    }

    public TextPosition Position { get; }
}