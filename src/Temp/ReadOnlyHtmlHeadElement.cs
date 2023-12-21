namespace AngleSharp.ReadOnly;

using System;
using Common;
using Dom;

internal class ReadOnlyHtmlHeadElement : ReadOnlyElement
{
    public ReadOnlyHtmlHeadElement(ReadOnlyHtmlDocument document) : base(document, "head")
    {
    }
}