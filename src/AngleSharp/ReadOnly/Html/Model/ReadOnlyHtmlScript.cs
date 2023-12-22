using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Common;
using AngleSharp.Dom;
using AngleSharp.Html.Construction;

class ReadOnlyHtmlScript : ReadOnlyHtmlElement, IConstructableScriptElement
{
    public ReadOnlyHtmlScript(ReadOnlyDocument? owner, StringOrMemory prefix = default)
        :base(owner, TagNames.Script, prefix, NodeFlags.Special | NodeFlags.LiteralText)
    {
    }

    public Boolean Prepare(IConstructableDocument document) => false;
    public Task RunAsync(CancellationToken cancel) => Task.CompletedTask;

    public override IConstructableNode ShallowCopy()
    {
        var readOnlyElement = new ReadOnlyHtmlScript(Owner)
        {
            _childNodes = _childNodes
        };
        return readOnlyElement;
    }
}