namespace AngleSharp.Html.Construction;

using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Parser.Tokens.Struct;
using Text;

/// <summary>
/// Represents a constructable document.
/// </summary>
public interface IConstructableDocument : IConstructableNode
{
    /// <summary>
    /// Document source.
    /// </summary>
    TextSource Source { get; }

    /// <summary>
    /// Builder instance to dispose and tie lifetime to the document.
    /// </summary>
    IDisposable? Builder { get; set; }

    /// <summary>
    /// Quirks mode of the document.
    /// </summary>
    QuirksMode QuirksMode { get; set; }

    /// <summary>
    /// Head element of the document.
    /// </summary>
    IConstructableElement? Head { get; }

    /// <summary>
    /// Document element of the document.
    /// </summary>
    IConstructableElement DocumentElement { get; }

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
    /// Ads a new dom representation of a comment and adds it to the document.
    /// </summary>
    /// <param name="token">The token to use.</param>
    void AddComment(ref StructHtmlToken token);

    /// <summary>
    /// Tracks the given exception which happened during parsing.
    /// </summary>
    void TrackError(Exception exception);

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

    /// <summary>
    /// Clears the document.
    /// </summary>
    void Clear();
}