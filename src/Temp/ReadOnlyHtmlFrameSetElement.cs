namespace AngleSharp.ReadOnly;

using System;
using Common;
using Dom;

internal class ReadOnlyHtmlFrameSetElement : ReadOnlyHtmlElement
{
   public ReadOnlyHtmlFrameSetElement(ReadOnlyHtmlDocument document, StringOrMemory prefix = default)
       : base(document, TagNames.Frameset, prefix, NodeFlags.Special)
   {
   }
}