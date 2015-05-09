namespace AngleSharp.Extensions
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Collections;
    using AngleSharp.Dom.Html;
    using AngleSharp.Network;
    using AngleSharp.Services;
    using AngleSharp.Services.Media;

    /// <summary>
    /// Useful methods for document objects.
    /// </summary>
    [DebuggerStepThrough]
    static class DocumentExtensions
    {
        /// <summary>
        /// Iterates over all ranges in the document, applying the provided action
        /// when the given condition is fulfilled.
        /// </summary>
        /// <param name="document">The document that hosts the ranges.</param>
        /// <param name="condition">The condition that needs to be fulfilled.</param>
        /// <param name="action">The action to apply to the range.</param>
        public static void ForEachRange(this Document document, Predicate<Range> condition, Action<Range> action)
        {
            if (document != null)
            {
                foreach (var range in document.Ranges)
                {
                    if (condition(range))
                        action(range);
                }
            }
        }

        /// <summary>
        /// Adopts the given node for the provided document context.
        /// </summary>
        /// <param name="document">The new owner of the node.</param>
        /// <param name="node">The node to change its owner.</param>
        public static void AdoptNode(this IDocument document, INode node)
        {
            var adoptedNode = node as Node;

            if (adoptedNode == null)
                throw new DomException(DomError.NotSupported);

            if (adoptedNode.Parent != null)
                adoptedNode.Parent.RemoveChild(adoptedNode, false);

            adoptedNode.Owner = document as Document;
        }

        /// <summary>
        /// Queues an action in the event loop of the document.
        /// </summary>
        /// <param name="document">The document that hosts the configuration.</param>
        /// <param name="action">The action that should be invoked.</param>
        public static void QueueTask(this Document document, Action action)
        {
            var eventLoop = document.Options.GetService<IEventService>();

            if (eventLoop != null)
                eventLoop.Enqueue(new Task(action));
            else
                action();
        }

        /// <summary>
        /// Queues an task in the event loop of the document.
        /// </summary>
        /// <param name="document">The document that hosts the configuration.</param>
        /// <param name="task">The task that should be run.</param>
        public static void QueueTask(this Document document, Task task)
        {
            var eventLoop = document.Options.GetService<IEventService>();

            if (eventLoop != null)
                eventLoop.Enqueue(task);
            else if (task.Status == TaskStatus.Created)
                task.Start();
        }

        /// <summary>
        /// Spins the event loop of the document until the given predicate is matched.
        /// </summary>
        /// <param name="document">The document that hosts the configuration.</param>
        /// <param name="predicate">The condition that has to be met.</param>
        public static async Task SpinLoop(this Document document, Func<Boolean> predicate)
        {
            var eventLoop = document.Options.GetService<IEventService>();

            if (eventLoop != null)
                await eventLoop.Spin(predicate).ConfigureAwait(false);
            else
                while (predicate() == false) ;
        }

        /// <summary>
        /// Queues a mutation record for the corresponding observers.
        /// </summary>
        /// <param name="document">The document to use.</param>
        /// <param name="record">The record to enqueue.</param>
        public static void QueueMutation(this Document document, MutationRecord record)
        {
            if (document == null)
                return;

            var observers = document.Mutations.Observers.ToArray();

            if (observers.Length == 0)
                return;

            var nodes = record.Target.GetInclusiveAncestors();

            for (var i = 0; i < observers.Length; i++)
            {
                var observer = observers[i];
                var clearPreviousValue = default(bool?);

                foreach (var node in nodes)
                {
                    var options = observer.ResolveOptions(node);

                    if (options == null)
                        continue;
                    else if (node != record.Target && options.IsObservingSubtree == false)
                        continue;
                    else if (record.IsAttribute && options.IsObservingAttributes.Value == false)
                        continue;
                    else if (record.IsAttribute && options.AttributeFilters != null && (options.AttributeFilters.Contains(record.AttributeName) == false || record.AttributeNamespace != null))
                        continue;
                    else if (record.IsCharacterData && options.IsObservingCharacterData.Value == false)
                        continue;
                    else if (record.IsChildList && options.IsObservingChildNodes == false)
                        continue;

                    if (clearPreviousValue.HasValue == false || clearPreviousValue.Value == true)
                    {
                        clearPreviousValue = (record.IsAttribute && options.IsExaminingOldAttributeValue.Value == false) ||
                            (record.IsCharacterData && options.IsExaminingOldCharacterData.Value == false);
                    }
                }

                if (clearPreviousValue == null)
                    continue;

                observer.Enqueue(record.Copy(clearPreviousValue.Value));
            }

            document.PerformMicrotaskCheckpoint();
        }

        /// <summary>
        /// Adds a transient observer for the given node.
        /// </summary>
        /// <param name="document">The document to use.</param>
        /// <param name="node">The node to be removed.</param>
        public static void AddTransientObserver(this Document document, INode node)
        {
            if (document == null)
                return;

            var ancestors = node.GetAncestors();
            var observers = document.Mutations.Observers;

            foreach (var ancestor in ancestors)
            {
                foreach (var observer in observers)
                    observer.AddTransient(ancestor, node);
            }
        }

        /// <summary>
        /// Applies the manifest to the given document.
        /// </summary>
        /// <param name="document">The document to modify.</param>
        /// <param name="root">The document's element.</param>
        public static void ApplyManifest(this Document document, HtmlHtmlElement root)
        {
            if (!document.IsInBrowsingContext)
                return;

            var manifest = root.Manifest;
            //TODO
            //Replace by algorithm to resolve the value of that attribute to an absolute URL,
            //relative to the newly created element.
            Predicate<String> CanResolve = str => false;

            if (!String.IsNullOrEmpty(manifest) && CanResolve(manifest))
            {
                //Run the application cache selection algorithm with the result of applying the URL serializer
                //algorithm to the resulting parsed URL with the exclude fragment flag set.
            }
            else
            {
                //Run the application cache selection algorithm with no manifest.
                //The algorithm must be passed the Document object.
            }
        }

        /// <summary>
        /// Performs a microtask checkpoint using the mutations host.
        /// Queue a mutation observer compound microtask.
        /// </summary>
        /// <param name="document">The document to use.</param>
        public static void PerformMicrotaskCheckpoint(this Document document)
        {
            document.Mutations.ScheduleCallback();
        }

        /// <summary>
        /// Provides a stable state by running the synchronous sections of asynchronously-running
        /// algorithms until the asynchronous algorithm can be resumed (if appropriate).
        /// </summary>
        /// <param name="document">The document to use.</param>
        public static void ProvideStableState(this Document document)
        {
            //TODO
        }

        /// <summary>
        /// Checks if the document has any active stylesheets that block the scripts. A style sheet is
        /// blocking scripts if the responsible element was created by that Document's parser, and the
        /// element is either a style element or a link element that was an external resource link that
        /// contributes to the styling processing model when the element was created by the parser, and
        /// the element's style sheet was enabled when the element was created by the parser, and the
        /// element's style sheet ready flag is not yet set.
        /// http://www.w3.org/html/wg/drafts/html/master/document-metadata.html#has-no-style-sheet-that-is-blocking-scripts
        /// </summary>
        /// <param name="document">The document to use.</param>
        /// <returns>True if any stylesheets still need to be downloaded, otherwise false.</returns>
        public static Boolean HasScriptBlockingStyleSheet(this Document document)
        {
            //TODO

            if (document.IsInBrowsingContext && document.Context.Parent != null)
            {
                var parentDocument = document.Context.Parent.Active as Document;

                if (parentDocument != null)
                    return parentDocument.HasScriptBlockingStyleSheet();
            }

            return false;
        }

        /// <summary>
        /// Spins the event loop until all stylesheets are downloaded (if required) and all
        /// scripts are ready to be parser executed.
        /// http://www.w3.org/html/wg/drafts/html/master/syntax.html#the-end
        /// (bullet 3)
        /// </summary>
        /// <param name="document">The document to use.</param>
        public static async Task WaitForReady(this Document document)
        {
            if (document.HasScriptBlockingStyleSheet() || document.IsWaitingForScript())
            {
                Func<Boolean> condition = () => 
                    document.HasScriptBlockingStyleSheet() == false && 
                    document.IsWaitingForScript() == false;
                await document.SpinLoop(condition).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Gets the specified target browsing context.
        /// </summary>
        /// <param name="document">The document that originates the request.</param>
        /// <param name="target">The specified target name.</param>
        /// <returns>The available context, or null, if the context does not exist yet.</returns>
        public static IBrowsingContext GetTarget(this Document document, String target)
        {
            if (String.IsNullOrEmpty(target) || target.Equals("_self", StringComparison.Ordinal))
                return document.Context;
            else if (target.Equals("_parent", StringComparison.Ordinal))
                return document.Context.Parent ?? document.Context;
            else if (target.Equals("_top", StringComparison.Ordinal))
                return document.Context;

            return document.Options.FindContext(target);
        }

        /// <summary>
        /// Creates the specified target browsing context.
        /// </summary>
        /// <param name="document">The document that originates the request.</param>
        /// <param name="target">The specified target name.</param>
        /// <returns>The new context.</returns>
        public static IBrowsingContext CreateTarget(this Document document, String target)
        {
            var security = Sandboxes.None;

            if (target.Equals("_blank", StringComparison.Ordinal))
                return document.Options.NewContext(security);

            return document.NewContext(target, security);
        }

        /// <summary>
        /// Creates a new browsing context with the given name and creator.
        /// </summary>
        /// <param name="document">The creator of the context.</param>
        /// <param name="name">The name of the new context.</param>
        /// <param name="security">The sandbox flag of the context.</param>
        /// <returns>The new context.</returns>
        public static IBrowsingContext NewContext(this Document document, String name, Sandboxes security)
        {
            var options = document.Options;
            var service = options.GetService<IContextService>();

            if (service == null)
                return new BrowsingContext(document.Context, security);

            return service.Create(document.Context, name, security);
        }

        /// <summary>
        /// Creates a new nested browsing context with the given name and creator.
        /// </summary>
        /// <param name="document">The creator of the context.</param>
        /// <param name="security">The sandbox flag of the context.</param>
        /// <returns>The new nesteted context.</returns>
        public static IBrowsingContext NewChildContext(this Document document, Sandboxes security)
        {
            //TODO
            return document.NewContext(String.Empty, security);
        }

        /// <summary>
        /// Creates a new Window instance for the given document.
        /// </summary>
        /// <param name="document">The document that demands a DefaultView.</param>
        /// <returns>The freshly created Window.</returns>
        public static IWindow CreateWindow(this Document document)
        {
            var service = document.Options.GetService<IWindowService>();

            if (service == null)
                return new Window(document);

            return service.Create(document);
        }

        /// <summary>
        /// Releases the storage mutex. For more information, see:
        /// http://www.w3.org/html/wg/drafts/html/CR/webappapis.html#storage-mutex
        /// </summary>
        /// <param name="document">The responsible document.</param>
        public static void ReleaseStorageMutex(this Document document)
        {
            //TODO
        }

        /// <summary>
        /// Gets the resource loader for the given document, by creating it if
        /// possible.
        /// </summary>
        /// <param name="document">The document that hosts the loader.</param>
        /// <returns>A resource loader or null.</returns>
        public static IResourceLoader CreateLoader(this Document document)
        {
            var loader = document.Options.GetService<ILoaderService>();

            if (loader == null)
                return null;

            return loader.CreateResourceLoader(document);
        }

        /// <summary>
        /// Tries to load the resource of the resource type from the request.
        /// </summary>
        /// <param name="document">The document to use.</param>
        /// <param name="request">The issued request.</param>
        /// <param name="cancel">
        /// Token to trigger in case of cancellation.
        /// </param>
        /// <returns>A task that will end with an image info or null.</returns>
        public static async Task<TResource> LoadResource<TResource>(this Document document, ResourceRequest request, CancellationToken cancel)
            where TResource : IResourceInfo
        {
            var loader = document.Loader;
            var response = await loader.FetchAsync(request, cancel).ConfigureAwait(false);

            if (response != null)
            {
                var options = document.Options;
                var resourceServices = options.GetServices<IResourceService<TResource>>();

                foreach (var resourceService in resourceServices)
                {
                    if (resourceService.SupportsType(response.Headers[HeaderNames.ContentType]))
                        return await resourceService.CreateAsync(response, cancel).ConfigureAwait(false);
                }
            }

            return default(TResource);
        }
    }
}
