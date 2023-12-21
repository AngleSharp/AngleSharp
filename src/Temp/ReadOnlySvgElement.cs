namespace AngleSharp.ReadOnly;

using Common;
using Dom;
using Html.Parser.Tokens.Struct;

internal class ReadOnlySvgElement : ReadOnlyElement
{
    public ReadOnlySvgElement(ReadOnlyHtmlDocument document, StringOrMemory tagName) : base(document, tagName, NodeType.Element, NodeFlags.SvgMember)
    {
    }

    public override ReadOnlyElement Setup(ref StructHtmlToken tag)
    {
        var element = this;

        var count = tag.Attributes.Count;

        for (var i = 0; i < count; i++)
        {
            var attr = tag.Attributes[i];
            var name = attr.Name;
            var value = attr.Value;
            // todo: adjust attribute
            // element.AdjustAttribute(name.AdjustToSvgAttribute(), value);
            element.AdjustAttribute(name, value);
        }

        return element;
    }
}