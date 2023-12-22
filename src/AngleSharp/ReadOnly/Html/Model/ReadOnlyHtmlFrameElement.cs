using AngleSharp.Common;
using AngleSharp.Dom;
using AngleSharp.Html.Construction;

class ReadOnlyHtmlFrameElement : ReadOnlyHtmlElement, IConstructableFrameElement
{
    public ReadOnlyHtmlFrameElement(ReadOnlyDocument? owner, StringOrMemory prefix = default)
        : base(owner, TagNames.Frame, prefix, NodeFlags.SelfClosing)
    {
    }

    public override IConstructableNode ShallowCopy()
    {
        var readOnlyElement = new ReadOnlyHtmlFrameElement(Owner)
        {
            _childNodes = _childNodes
        };
        return readOnlyElement;
    }
}