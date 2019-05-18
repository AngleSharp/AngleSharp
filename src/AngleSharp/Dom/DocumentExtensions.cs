namespace AngleSharp.Dom
{
    using AngleSharp.Browser;
    using AngleSharp.Html.Dom;
    using AngleSharp.Io;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

#if NETSTANDARD1_3
    // Assembly.GetTypes is available as an extension in System.Reflection from NS1.3
    using System.Reflection;
#endif

    /// <summary>
    /// Useful methods for document objects.
    /// </summary>
    public static class DocumentExtensions
    {
        /// <summary>
        /// Iterates over all ranges in the document, applying the provided
        /// action when the given condition is fulfilled.
        /// </summary>
        /// <param name="document">The document that hosts the ranges.</param>
        /// <param name="condition">
        /// The condition that needs to be fulfilled.
        /// </param>
        /// <param name="action">The action to apply to the range.</param>
        internal static void ForEachRange(this Document document, Predicate<Range> condition, Action<Range> action)
        {
            foreach (var range in document.GetAttachedReferences<Range>())
            {
                if (condition.Invoke(range))
                {
                    action.Invoke(range);
                }
            }
        }

        /// <summary>
        /// Creates an element of the given type or throws an exception, if
        /// there is no such type.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="document">The responsible document.</param>
        /// <returns>The created element.</returns>
        public static TElement CreateElement<TElement>(this IDocument document)
            where TElement : IElement
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            var type = typeof(BrowsingContext).GetAssembly().GetTypes()
                .Where(m => m.Implements<TElement>())
                .FirstOrDefault(m => !m.IsAbstractClass());

            if (type != null)
            {
                var ctors = type.GetConstructors()
                    .OrderBy(m => m.GetParameters().Length);

                foreach (var ctor in ctors)
                {
                    var parameters = ctor.GetParameters();
                    var arguments = new Object[parameters.Length];

                    for (var i = 0; i < parameters.Length; i++)
                    {
                        var isDocument = parameters[i].ParameterType == typeof(Document);
                        arguments[i] = isDocument ? document : parameters[i].DefaultValue;
                    }

                    var obj = ctor.Invoke(arguments);

                    if (obj != null)
                    {
                        var element = (TElement)obj;
                        var baseElement = element as Element;
                        baseElement?.SetupElement();
                        document.Adopt(element);
                        return element;
                    }
                }
            }

            throw new ArgumentException("No element could be created for the provided interface.");
        }

        /// <summary>
        /// Adopts the given node for the provided document context.
        /// </summary>
        /// <param name="document">The new owner of the node.</param>
        /// <param name="node">The node to change its owner.</param>
        public static void AdoptNode(this IDocument document, INode node)
        {
            if (node is Node adoptedNode)
            {
                adoptedNode.Parent?.RemoveChild(adoptedNode, false);
                adoptedNode.Owner = document as Document;
            }
            else
            {
                throw new DomException(DomError.NotSupported);
            }
        }

        /// <summary>
        /// Queues an action in the event loop of the document.
        /// </summary>
        /// <param name="document">
        /// The document that hosts the configuration.
        /// </param>
        /// <param name="action">The action that should be invoked.</param>
        internal static void QueueTask(this Document document, Action action) =>
            document.Loop.Enqueue(action);

        /// <summary>
        /// Queues an action in the event loop of the document,
        /// which can be awaited.
        /// </summary>
        /// <param name="document">
        /// The document that hosts the configuration.
        /// </param>
        /// <param name="action">The action that should be invoked.</param>
        internal static Task QueueTaskAsync(this Document document, Action<CancellationToken> action) =>
            document.Loop.EnqueueAsync(_ =>
            {
                action(_);
                return true;
            });

        /// <summary>
        /// Queues a function in the event loop of the document,
        /// which can be awaited with the result returned.
        /// </summary>
        /// <param name="document">
        /// The document that hosts the configuration.
        /// </param>
        /// <param name="func">The function that should be invoked.</param>
        internal static Task<T> QueueTaskAsync<T>(this Document document, Func<CancellationToken, T> func) =>
            document.Loop.EnqueueAsync(func);

        /// <summary>
        /// Queues a mutation record for the corresponding observers.
        /// </summary>
        /// <param name="document">The document to use.</param>
        /// <param name="record">The record to enqueue.</param>
        internal static void QueueMutation(this Document document, MutationRecord record)
        {
            var observers = document.Mutations.Observers.ToArray();

            if (observers.Length > 0)
            {
                var nodes = record.Target.GetInclusiveAncestors();

                for (var i = 0; i < observers.Length; i++)
                {
                    var observer = observers[i];
                    var clearPreviousValue = default(Boolean?);

                    foreach (var node in nodes)
                    {
                        var options = observer.ResolveOptions(node);

                        if (options.IsInvalid ||
                           (node != record.Target && !options.IsObservingSubtree) ||
                           (record.IsAttribute && !options.IsObservingAttributes) ||
                           (record.IsAttribute && options.AttributeFilters != null && (!options.AttributeFilters.Contains(record.AttributeName) || record.AttributeNamespace != null)) ||
                           (record.IsCharacterData && !options.IsObservingCharacterData) ||
                           (record.IsChildList && !options.IsObservingChildNodes))
                        {
                            continue;
                        }

                        if (!clearPreviousValue.HasValue || clearPreviousValue.Value)
                        {
                            clearPreviousValue = (record.IsAttribute && !options.IsExaminingOldAttributeValue) ||
                                (record.IsCharacterData && !options.IsExaminingOldCharacterData);
                        }
                    }

                    if (clearPreviousValue != null)
                    {
                        observer.Enqueue(record.Copy(clearPreviousValue.Value));
                    }
                }

                document.PerformMicrotaskCheckpoint();
            }
        }

        /// <summary>
        /// Adds a transient observer for the given node.
        /// </summary>
        /// <param name="document">The document to use.</param>
        /// <param name="node">The node to be removed.</param>
        internal static void AddTransientObserver(this Document document, INode node)
        {
            var ancestors = node.GetAncestors();
            var observers = document.Mutations.Observers;

            foreach (var ancestor in ancestors)
            {
                foreach (var observer in observers)
                {
                    observer.AddTransient(ancestor, node);
                }
            }
        }

        /// <summary>
        /// Applies the manifest to the given document.
        /// </summary>
        /// <param name="document">The document to modify.</param>
        internal static void ApplyManifest(this Document document)
        {
            if (document.IsInBrowsingContext)
            {
                if (document.DocumentElement is IHtmlHtmlElement root)
                {
                    var manifest = root.Manifest;

                    //TODO
                    // Replace by algorithm to resolve the value of that attribute
                    // to an absolute URL, relative to the newly created element.
                    var CanResolve = new Predicate<String>(str => false);

                    if (!String.IsNullOrEmpty(manifest) && CanResolve(manifest))
                    {
                        // Run the application cache selection algorithm with the
                        // result of applying the URL serializer algorithm to the
                        // resulting parsed URL with the exclude fragment flag set.
                    }
                    else
                    {
                        // Run the application cache selection algorithm with no
                        // manifest. The algorithm must be passed the Document
                        // object.
                    }
                }
            }
        }

        /// <summary>
        /// Performs a microtask checkpoint using the mutations host.
        /// Queue a mutation observer compound microtask.
        /// </summary>
        /// <param name="document">The document to use.</param>
        internal static void PerformMicrotaskCheckpoint(this Document document) => document.Mutations.ScheduleCallback();

        /// <summary>
        /// Provides a stable state by running the synchronous sections of
        /// asynchronously-running algorithms until the asynchronous algorithm
        /// can be resumed (if appropriate).
        /// </summary>
        /// <param name="document">The document to use.</param>
        internal static void ProvideStableState(this Document document)
        {
            //TODO
        }

        /// <summary>
        /// Checks if the document is waiting for a script to finish preparing.
        /// </summary>
        /// <param name="document">The document to use.</param>
        /// <returns>Enumerable of awaitable tasks.</returns>
        public static IEnumerable<Task> GetScriptDownloads(this IDocument document) => document.Context.GetDownloads<HtmlScriptElement>();

        /// <summary>
        /// Checks if the document has any active stylesheets that block the
        /// scripts. A style sheet is blocking scripts if the responsible
        /// element was created by that Document's parser, and the element is
        /// either a style element or a link element that was an external
        /// resource link that contributes to the styling processing model when
        /// the element was created by the parser, and the element's style
        /// sheet was enabled when the element was created by the parser, and
        /// the element's style sheet ready flag is not yet set.
        /// http://www.w3.org/html/wg/drafts/html/master/document-metadata.html#has-no-style-sheet-that-is-blocking-scripts
        /// </summary>
        /// <param name="document">The document to use.</param>
        /// <returns>Enumerable of awaitable tasks.</returns>
        public static IEnumerable<Task> GetStyleSheetDownloads(this IDocument document) => document.Context.GetDownloads<HtmlLinkElement>();

        /// <summary>
        /// Spins the event loop until all stylesheets are downloaded (if
        /// required) and all scripts are ready to be parser executed.
        /// http://www.w3.org/html/wg/drafts/html/master/syntax.html#the-end
        /// (bullet 3)
        /// </summary>
        /// <param name="document">The document to use.</param>
        /// <returns>Awaitable task.</returns>
        public static async Task WaitForReadyAsync(this IDocument document)
        {
            var scripts = document.GetScriptDownloads().ToArray();
            await Task.WhenAll(scripts).ConfigureAwait(false);
            var styles = document.GetStyleSheetDownloads().ToArray();
            await Task.WhenAll(styles).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets all downloads associated with resources of the document.
        /// </summary>
        /// <param name="document">The document hosting the downloads.</param>
        /// <returns>The collection of elements hosting resources.</returns>
        public static IEnumerable<IDownload> GetDownloads(this IDocument document)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            return document.All.OfType<ILoadableElement>().Select(m => m.CurrentDownload).Where(m => m != null);
        }
    }
}
