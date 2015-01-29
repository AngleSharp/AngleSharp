namespace AngleSharp
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Css;
    using AngleSharp.Dom.Events;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A set of useful extension methods when dealing with the DOM.
    /// </summary>
    public static class ApiExtensions
    {
        #region Generic extensions

        /// <summary>
        /// Creates an element of the given type or returns null, if there is
        /// no such type.
        /// </summary>
        /// <typeparam name="TElement">The type of element to create.</typeparam>
        /// <param name="document">The responsible document.</param>
        /// <returns>The new element, if available.</returns>
        public static TElement CreateElement<TElement>(this IDocument document)
            where TElement : IElement
        {
            var type = typeof(ApiExtensions).GetAssembly().GetTypes()
                .Where(m => m.Implements<TElement>())
                .FirstOrDefault(m => !m.IsAbstractClass());

            if (type == null)
                return default(TElement);

            var ctor = type.GetConstructor();
            var parameterLess = ctor != null;
            
            if (parameterLess == false)
                ctor = type.GetConstructor(new Type[] { typeof(Document) });

            if (ctor == null)
                return default(TElement);

            var element = (TElement)(parameterLess ? ctor.Invoke(null) : ctor.Invoke(new Object[] { document }));
            var el = element as Element;

            if (element != null)
                document.Adopt(element);

            return element;
        }

        /// <summary>
        /// Returns a task that is completed once the event is fired.
        /// </summary>
        /// <typeparam name="TEventTarget">The event target type.</typeparam>
        /// <param name="node">The node that fires the event.</param>
        /// <param name="eventName">The name of the event to be awaited.</param>
        /// <returns>The awaitable task returning the event arguments.</returns>
        public static async Task<Event> AwaitEvent<TEventTarget>(this TEventTarget node, String eventName)
            where TEventTarget : IEventTarget
        {
            var completion = new TaskCompletionSource<Event>();
            DomEventHandler handler = (s, ev) => completion.TrySetResult(ev);
            node.AddEventListener(eventName, handler);

            try { return await completion.Task; }
            finally { node.RemoveEventListener(eventName, handler); }
        }

        /// <summary>
        /// Gets the descendent nodes of the given parent.
        /// </summary>
        /// <typeparam name="TNode">The type of nodes to obtain.</typeparam>
        /// <param name="parent">The parent of the nodes to gather.</param>
        /// <returns>The descendent nodes.</returns>
        public static IEnumerable<TNode> Descendents<TNode>(this INode parent)
            where TNode : INode
        {
            return parent.GetDescendants().OfType<TNode>();
        }

        /// <summary>
        /// Gets the ancestor nodes of the given child.
        /// </summary>
        /// <typeparam name="TNode">The type of nodes to obtain.</typeparam>
        /// <param name="child">The child of the nodes to gather.</param>
        /// <returns>The ancestor nodes.</returns>
        public static IEnumerable<TNode> Ancestors<TNode>(this INode child)
            where TNode : INode
        {
            return child.GetAncestors().OfType<TNode>();
        }

        #endregion

        #region Construction helpers

        /// <summary>
        /// Interprets the string as HTML source code and returns new HTMLDocument
        /// with the DOM representation.
        /// </summary>
        /// <param name="content">The string to use as source code.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The HTML document.</returns>
        public static IDocument ParseHtml(this String content, IConfiguration configuration = null)
        {
            return DocumentBuilder.Html(content, configuration);
        }

        /// <summary>
        /// Interprets the string as CSS source code and returns new CSSStyleSheet
        /// with the CSS-OM representation.
        /// </summary>
        /// <param name="content">The string to use as source code.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The CSS stylesheet.</returns>
        public static ICssStyleSheet ParseCss(this String content, IConfiguration configuration = null)
        {
            return DocumentBuilder.Css(content, configuration);
        }

        /// <summary>
        /// Uses the URL to download the content, parse it as HTML and returning
        /// a new HTMLDocument with the DOM representation.
        /// </summary>
        /// <param name="uri">The source of the HTML content.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The HTML document.</returns>
        public static IDocument GetHtml(this Uri uri, IConfiguration configuration = null)
        {
            return DocumentBuilder.Html(uri, configuration);
        }

        /// <summary>
        /// Uses the URL to download the content, parse it as CSS and returning
        /// a new CSSStyleSheet with the CSS-OM representation.
        /// </summary>
        /// <param name="uri">The source of the CSS content.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The CSS stylesheet.</returns>
        public static ICssStyleSheet GetCss(this Uri uri, IConfiguration configuration = null)
        {
            return DocumentBuilder.Css(uri, configuration);
        }

        /// <summary>
        /// Uses the URL to download the content asynchronously, parse it as HTML and returning
        /// a new HTMLDocument with the DOM representation.
        /// </summary>
        /// <param name="uri">The source of the HTML content.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The HTML document.</returns>
        public static Task<IDocument> GetHtmlAsync(this Uri uri, IConfiguration configuration = null)
        {
            return DocumentBuilder.HtmlAsync(uri, configuration);
        }

        /// <summary>
        /// Uses the URL to download the content asynchronously, parse it as HTML and returning
        /// a new HTMLDocument with the DOM representation.
        /// </summary>
        /// <param name="uri">The source of the HTML content.</param>
        /// <param name="cancel">The cancellation token for aborting the download.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The HTML document.</returns>
        public static Task<IDocument> GetHtmlAsync(this Uri uri, CancellationToken cancel, IConfiguration configuration = null)
        {
            return DocumentBuilder.HtmlAsync(uri, cancel, configuration);
        }

        /// <summary>
        /// Uses the URL to download the content asynchronously, parse it as CSS and returning
        /// a new CSSStyleSheet with the CSS-OM representation.
        /// </summary>
        /// <param name="uri">The source of the CSS content.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The CSS stylesheet.</returns>
        public static Task<ICssStyleSheet> GetCssAsync(this Uri uri, IConfiguration configuration = null)
        {
            return DocumentBuilder.CssAsync(uri, configuration);
        }

        /// <summary>
        /// Uses the URL to download the content asynchronously, parse it as CSS and returning
        /// a new CSSStyleSheet with the CSS-OM representation.
        /// </summary>
        /// <param name="uri">The source of the CSS content.</param>
        /// <param name="cancel">The cancellation token for aborting the download.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The CSS stylesheet.</returns>
        public static Task<ICssStyleSheet> GetCssAsync(this Uri uri, CancellationToken cancel, IConfiguration configuration = null)
        {
            return DocumentBuilder.CssAsync(uri, cancel, configuration);
        }

        #endregion
    }
}
