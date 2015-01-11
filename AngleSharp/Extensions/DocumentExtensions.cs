namespace AngleSharp.Extensions
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Html;
    using AngleSharp.Infrastructure;
    using AngleSharp.Services;
    using System;
    using System.Diagnostics;
    using System.Linq;

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
                throw new DomException(ErrorCode.NotSupported);

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
                eventLoop.Enqueue(new MicroDomTask(document, action));
            else
                action.InvokeAsync();
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

            var nodes = record.Target.GetInclusiveAncestorsOf();

            foreach (var node in nodes)
            {
                for (var i = 0; i < observers.Length; i++)
                {
                    var observer = observers[i];
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

                    var clearPreviousValue = (record.IsAttribute && options.IsExaminingOldAttributeValue.Value == false) ||
                        (record.IsCharacterData && options.IsExaminingOldCharacterData.Value == false);

                    observer.Enqueue(record.Copy(clearPreviousValue));
                }
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

            var ancestors = node.GetAncestorsOf();
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
        public static void ApplyManifest(this Document document, HTMLHtmlElement root)
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
        /// Spins the event loop until all stylesheets are downloaded (if required) and all
        /// scripts are ready to be parser executed.
        /// </summary>
        /// <param name="document">The document to use.</param>
        public static void WaitForReady(this Document document)
        {
            //TODO
            //If the parser's Document has a style sheet that is blocking scripts or the script's "ready to be parser-executed"
            //flag is not set: spin the event loop until the parser's Document has no style sheet that is blocking scripts and
            //the script's "ready to be parser-executed" flag is set.
        }
    }
}
