namespace AngleSharp.ReadOnly;

using System;
using Common;
using Dom;
using Html.Parser.Tokens.Struct;

internal class ReadOnlyMathElement : ReadOnlyElement
{
    public ReadOnlyMathElement(ReadOnlyHtmlDocument document, StringOrMemory tagName) : base(document, tagName, NodeType.Element)
    {
    }

    public override ReadOnlyElement Setup(ref StructHtmlToken tag)
    {
        throw new NotImplementedException();
    }
}