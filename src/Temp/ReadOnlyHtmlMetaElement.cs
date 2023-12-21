namespace AngleSharp.ReadOnly;

using System;
using Common;
using Dom;

internal class ReadOnlyHtmlMetaElement : ReadOnlyHtmlElement
{
    /*
     *  public HtmlMetaElement(Document owner, String? prefix = null)
            : base(owner, TagNames.Meta, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }
     */

    public ReadOnlyHtmlMetaElement(ReadOnlyHtmlDocument document, StringOrMemory prefix = default)
        : base(document, TagNames.Meta, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
    {

    }

    public void Handle()
    {
        throw new NotImplementedException();
    }
}