namespace AngleSharp.ReadOnly;

using System;
using Common;
using Dom;

internal class ReadOnlyHtmlTemplateElement : ReadOnlyElement
{
    public ReadOnlyHtmlTemplateElement(ReadOnlyHtmlDocument document) : base(document, StringOrMemory.Empty, NodeType.Element)
    {
    }

    public void PopulateFragment()
    {
        throw new NotImplementedException();
    }
}