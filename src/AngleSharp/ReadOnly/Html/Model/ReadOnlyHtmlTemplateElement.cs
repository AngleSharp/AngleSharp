using AngleSharp.Common;
using AngleSharp.Dom;
using AngleSharp.Html.Construction;

class ReadOnlyHtmlTemplateElement : ReadOnlyHtmlElement, IConstructableTemplateElement
{
    public ReadOnlyHtmlTemplateElement(ReadOnlyDocument? owner, StringOrMemory prefix = default)
        : base(owner, TagNames.Template, prefix, NodeFlags.Special | NodeFlags.Scoped | NodeFlags.HtmlTableScoped | NodeFlags.HtmlTableSectionScoped)
    {
    }

    public void PopulateFragment()
    {
    }

    public override IConstructableNode ShallowCopy()
    {
        var readOnlyElement = new ReadOnlyHtmlTemplateElement(Owner)
        {
            _childNodes = _childNodes
        };
        return readOnlyElement;
    }
}