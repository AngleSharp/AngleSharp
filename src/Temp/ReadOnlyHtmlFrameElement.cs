namespace AngleSharp.ReadOnly;

using Common;
using Dom;

internal class ReadOnlyHtmlFrameElement : ReadOnlyHtmlElement
{
   public ReadOnlyHtmlFrameElement(ReadOnlyHtmlDocument document, StringOrMemory prefix = default)
       : base(document, TagNames.Frame, prefix, NodeFlags.SelfClosing)
   {
   }
}