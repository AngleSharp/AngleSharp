namespace AngleSharp.Extensions
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Collections;
    using AngleSharp.Dom.Html;
    using AngleSharp.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Useful methods for document objects.
    /// </summary>
    static class DocumentExtensions
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
        public static void ForEachRange(this Document document, Predicate<Range> condition, Action<Range> action)
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
        /// Adopts the given node for the provided document context.
        /// </summary>
        /// <param name="document">The new owner of the node.</param>
        /// <param name="node">The node to change its owner.</param>
        public static void AdoptNode(this IDocument document, INode node)
        {
            var adoptedNode = node as Node;

            if (adoptedNode == null)
                throw new DomException(DomError.NotSupported);

            adoptedNode.Parent?.RemoveChild(adoptedNode, false);
            adoptedNode.Owner = document as Document;
        }

        /// <summary>
        /// Queues an action in the event loop of the document.
        /// </summary>
        /// <param name="document">
        /// The document that hosts the configuration.
        /// </param>
        /// <param name="action">The action that should be invoked.</param>
        public static void QueueTask(this Document document, Action action)
        {
            document.Loop.Enqueue(action);
        }

        /// <summary>
        /// Queues a mutation record for the corresponding observers.
        /// </summary>
        /// <param name="document">The document to use.</param>
        /// <param name="record">The record to enqueue.</param>
        public static void QueueMutation(this Document document, MutationRecord record)
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

                        if (options.IsInvalid)
                        {
                            continue;
                        }
                        else if (node != record.Target && !options.IsObservingSubtree)
                        {
                            continue;
                        }
                        else if (record.IsAttribute && !options.IsObservingAttributes)
                        {
                            continue;
                        }
                        else if (record.IsAttribute && options.AttributeFilters != null && (!options.AttributeFilters.Contains(record.AttributeName) || record.AttributeNamespace != null))
                        {
                            continue;
                        }
                        else if (record.IsCharacterData && !options.IsObservingCharacterData)
                        {
                            continue;
                        }
                        else if (record.IsChildList && !options.IsObservingChildNodes)
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
        public static void AddTransientObserver(this Document document, INode node)
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
        public static void ApplyManifest(this Document document)
        {
            if (document.IsInBrowsingContext)
            {
                var root = document.DocumentElement as IHtmlHtmlElement;

                if (root != null)
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
        public static void PerformMicrotaskCheckpoint(this Document document)
        {
            document.Mutations.ScheduleCallback();
        }

        /// <summary>
        /// Provides a stable state by running the synchronous sections of 
        /// asynchronously-running algorithms until the asynchronous algorithm
        /// can be resumed (if appropriate).
        /// </summary>
        /// <param name="document">The document to use.</param>
        public static void ProvideStableState(this Document document)
        {
            //TODO
        }

        /// <summary>
        /// Checks if the document is waiting for tasks from originator of type
        /// T to finish downloading.
        /// </summary>
        /// <param name="document">The document to use.</param>
        /// <returns>Enumerable of awaitable tasks.</returns>
        public static IEnumerable<Task> GetDownloads<T>(this Document document)
            where T : INode
        {
            var loader = document.Loader;

            if (loader == null)
            {
                return Enumerable.Empty<Task>();
            }

            return loader.GetDownloads().Where(m => m.Originator is T).Select(m => m.Task);
        }

        /// <summary>
        /// Checks if the document is waiting for a script to finish preparing.
        /// </summary>
        /// <param name="document">The document to use.</param>
        /// <returns>Enumerable of awaitable tasks.</returns>
        public static IEnumerable<Task> GetScriptDownloads(this Document document)
        {
            return document.GetDownloads<HtmlScriptElement>();
        }

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
        public static IEnumerable<Task> GetStyleSheetDownloads(this Document document)
        {
            return document.GetDownloads<HtmlLinkElement>();
        }

        /// <summary>
        /// Spins the event loop until all stylesheets are downloaded (if
        /// required) and all scripts are ready to be parser executed.
        /// http://www.w3.org/html/wg/drafts/html/master/syntax.html#the-end
        /// (bullet 3)
        /// </summary>
        /// <param name="document">The document to use.</param>
        /// <returns>Awaitable task.</returns>
        public static async Task WaitForReadyAsync(this Document document)
        {
            var scripts = document.GetScriptDownloads().ToArray();
            await TaskEx.WhenAll(scripts).ConfigureAwait(false);
            var styles = document.GetStyleSheetDownloads().ToArray();
            await TaskEx.WhenAll(styles).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the specified target browsing context.
        /// </summary>
        /// <param name="document">
        /// The document that originates the request.
        /// </param>
        /// <param name="target">The specified target name.</param>
        /// <returns>
        /// The available context, or null, if the context does not exist yet.
        /// </returns>
        public static IBrowsingContext GetTarget(this Document document, String target)
        {
            if (String.IsNullOrEmpty(target) || target.Is("_self"))
            {
                return document.Context;
            }
            else if (target.Is("_parent"))
            {
                return document.Context.Parent ?? document.Context;
            }
            else if (target.Is("_top"))
            {
                return document.Context;
            }
            else
            {
                return document.Options.FindContext(target);
            }
        }

        /// <summary>
        /// Creates the specified target browsing context.
        /// </summary>
        /// <param name="document">
        /// The document that originates the request.
        /// </param>
        /// <param name="target">The specified target name.</param>
        /// <returns>The new context.</returns>
        public static IBrowsingContext CreateTarget(this Document document, String target)
        {
            var security = Sandboxes.None;

            if (target.Is("_blank"))
            {
                return document.Options.NewContext(security);
            }

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
            var factory = options.GetFactory<IContextFactory>();
            return factory.Create(document.Context, name, security);
        }

        /// <summary>
        /// Creates a new nested browsing context with the given name and 
        /// creator.
        /// </summary>
        /// <param name="document">The creator of the context.</param>
        /// <param name="security">The sandbox flag of the context.</param>
        /// <returns>The new nesteted context.</returns>
        public static IBrowsingContext NewChildContext(this Document document, Sandboxes security)
        {
            //TODO
            var context = document.NewContext(String.Empty, security);
            document.AttachReference(context);
            return context;
        }
    }
}
