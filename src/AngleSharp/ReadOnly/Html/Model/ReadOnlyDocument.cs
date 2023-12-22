using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Common;
using AngleSharp.Dom;
using AngleSharp.Html.Construction;
using AngleSharp.ReadOnly.Html;
using AngleSharp.Text;

internal class ReadOnlyDocument : ReadOnlyNode, IConstructableDocument, IReadOnlyDocument
{
    public ReadOnlyDocument(IReadOnlyTextSource source) : base(null, "#document", NodeType.Document)
    {
        Source = source;
    }

    public IReadOnlyTextSource Source { get; set; }

    public IDisposable? Builder { get; set; }

    public QuirksMode QuirksMode { get; set; }

    public IConstructableElement Head => _ChildNodes.OfType<ReadOnlyHtmlElement>().First(n => n.NodeName.Is("head"));

    public IConstructableElement DocumentElement => _ChildNodes.OfType<ReadOnlyHtmlElement>().First(n => n.NodeName.Is("document"));

    IReadOnlyElement IReadOnlyDocument.Body => _ChildNodes.OfType<ReadOnlyHtmlElement>().First(n => n.NodeName.Is("body"));

    IReadOnlyElement IReadOnlyDocument.Head => _ChildNodes.OfType<ReadOnlyHtmlElement>().First(n => n.NodeName.Is("head"));

    IReadOnlyNode? IReadOnlyNode.Parent => _parent as IReadOnlyNode;

    IReadOnlyNodeList IReadOnlyNode.ChildNodes => _ChildNodes;

    public bool IsLoading => false;

    public void TrackError(Exception exception) { }

    public Task WaitForReadyAsync(CancellationToken cancelToken) => Task.CompletedTask;

    public Task FinishLoadingAsync() => Task.CompletedTask;

    public void ApplyManifest() { }

    public void Clear() => ChildNodes.Clear();

    public void PerformMicrotaskCheckpoint() { }

    public void ProvideStableState() { }

    public void Dispose()
    {
        Source.Dispose();
        Builder?.Dispose();
    }
}