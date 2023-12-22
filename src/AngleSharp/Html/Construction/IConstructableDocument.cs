namespace AngleSharp.Html.Construction;

using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Parser.Tokens.Struct;
using Text;

internal interface IConstructableDocument : IConstructableNode
{
    IReadOnlyTextSource Source { get; }
    IDisposable? Builder { get; set; }
    QuirksMode QuirksMode { get; set; }
    IConstructableElement? Head { get; }
    IConstructableElement DocumentElement { get; }

    Boolean IsLoading { get; }

    void PerformMicrotaskCheckpoint();
    void ProvideStableState();

    void AddComment(ref StructHtmlToken token);

    void TrackError(Exception exception);
    Task WaitForReadyAsync(CancellationToken cancelToken);
    Task FinishLoadingAsync();
    void ApplyManifest();
    void Clear();
}