namespace AngleSharp.Extensions
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Html;
    using AngleSharp.Infrastructure;
    using System;
    using System.Diagnostics;

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
            if (document == null)
                return;

            foreach (var range in document.Ranges)
            {
                if (condition(range))
                    action(range);
            }
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
        /// Applies the manifest to the given document.
        /// </summary>
        /// <param name="document">The document to modify.</param>
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
    }
}
