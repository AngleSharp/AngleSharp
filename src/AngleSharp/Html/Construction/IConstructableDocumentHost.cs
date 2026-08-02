namespace AngleSharp.Html.Construction;

using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Represents the browsing-host lifecycle a document participates in while it is being parsed:
/// script execution checkpoints, stylesheet/script readiness, loading, and the application cache
/// manifest. A construction backend that does not execute scripts or load resources omits this
/// interface entirely rather than supplying no-op members, and the tree builder skips the
/// corresponding steps.
/// </summary>
public interface IConstructableDocumentHost : IConstructableDocumentState
{
    /// <summary>
    /// Is the document currently loading?
    /// </summary>
    Boolean IsLoading { get; }

    /// <summary>
    /// Performs a microtask checkpoint using the mutations host.
    /// Queue a mutation observer compound microtask.
    /// </summary>
    void PerformMicrotaskCheckpoint();

    /// <summary>
    /// Provides a stable state by running the synchronous sections of
    /// asynchronously-running algorithms until the asynchronous algorithm
    /// can be resumed (if appropriate).
    /// </summary>
    void ProvideStableState();

    /// <summary>
    /// Spins the event loop until all stylesheets are downloaded (if
    /// required) and all scripts are ready to be parser executed.
    /// http://www.w3.org/html/wg/drafts/html/master/syntax.html#the-end
    /// (bullet 3)
    /// </summary>
    Task WaitForReadyAsync(CancellationToken cancelToken);

    /// <summary>
    /// Finishes writing to a document.
    /// </summary>
    Task FinishLoadingAsync();

    /// <summary>
    /// Applies the manifest to the document.
    /// </summary>
    void ApplyManifest();
}
