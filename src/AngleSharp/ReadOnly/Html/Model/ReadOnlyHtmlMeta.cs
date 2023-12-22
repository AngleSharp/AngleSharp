using AngleSharp.Common;
using AngleSharp.Dom;
using AngleSharp.Html.Construction;

class ReadOnlyHtmlMeta : ReadOnlyHtmlElement, IConstructableMetaElement
{
    public ReadOnlyHtmlMeta(ReadOnlyDocument? owner, StringOrMemory prefix = default)
        : base(owner, TagNames.Meta, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
    {
    }

    public void Handle()
    {
        // do nothing
    }

    public override IConstructableNode ShallowCopy()
    {
        var readOnlyElement = new ReadOnlyHtmlMeta(Owner)
        {
            _childNodes = _childNodes
        };
        return readOnlyElement;
    }
}