namespace AngleSharp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using AngleSharp.Extensions;

    /// <summary>
    /// A set of useful extension methods when dealing with the DOM.
    /// </summary>
    public static class ApiExtensions
    {
        #region Generic extensions

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
                throw new ArgumentNullException("document");

            var type = typeof(ApiExtensions).GetAssembly().GetTypes()
                .Where(m => m.Implements<TElement>())
                .FirstOrDefault(m => !m.IsAbstractClass());

            if (type == null)
                return default(TElement);

            var ctors = type.GetConstructors();

            foreach (var ctor in ctors.OrderBy(m => m.GetParameters().Length))
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
                    document.Adopt(element);
                    return element;
                }
            }

            throw new ArgumentException("No element could be created for the provided interface.");
        }

        /// <summary>
        /// Creates a new DocumentFragment from the given HTML code. The
        /// fragment is parsed with the Body element as context.
        /// </summary>
        /// <param name="document">The responsible document.</param>
        /// <param name="html">The HTML to transform into a fragment.</param>
        /// <returns>The fragment containing the new nodes.</returns>
        static IDocumentFragment CreateFromHtml(this IDocument document, String html)
        {
            if (document == null)
                throw new ArgumentNullException("document");

            var body = document.Body as Element;

            if (body == null)
                throw new ArgumentException("The provided document does not have a valid body element.");

            return new DocumentFragment(body, html ?? String.Empty);
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
            if (node == null)
                throw new ArgumentNullException("node");
            else if (eventName == null)
                throw new ArgumentNullException("eventName");

            var completion = new TaskCompletionSource<Event>();
            DomEventHandler handler = (s, ev) => completion.TrySetResult(ev);
            node.AddEventListener(eventName, handler);

            try { return await completion.Task.ConfigureAwait(false); }
            finally { node.RemoveEventListener(eventName, handler); }
        }

        /// <summary>
        /// Inserts a node as the last child node of this element.
        /// </summary>
        /// <typeparam name="TElement">The type of element to add.</typeparam>
        /// <param name="parent">The parent of the node to add.</param>
        /// <param name="element">The element to be appended.</param>
        /// <returns>The appended element.</returns>
        public static TElement AppendElement<TElement>(this INode parent, TElement element)
            where TElement : class, IElement
        {
            if (parent == null)
                throw new ArgumentNullException("parent");

            return parent.AppendChild(element) as TElement;
        }

        /// <summary>
        /// Inserts the newElement immediately before the referenceElement.
        /// </summary>
        /// <typeparam name="TElement">The type of element to add.</typeparam>
        /// <param name="parent">The parent of the node to add.</param>
        /// <param name="newElement">The node to be inserted.</param>
        /// <param name="referenceElement">
        /// The existing child element that will succeed the new element.
        /// </param>
        /// <returns>The inserted element.</returns>
        public static TElement InsertElement<TElement>(this INode parent, TElement newElement, INode referenceElement)
            where TElement : class, IElement
        {
            if (parent == null)
                throw new ArgumentNullException("parent");

            return parent.InsertBefore(newElement, referenceElement) as TElement;
        }

        /// <summary>
        /// Removes a child node from the current element, which must be a
        /// child of the current node.
        /// </summary>
        /// <typeparam name="TElement">The type of element.</typeparam>
        /// <param name="parent">The parent of the node to remove.</param>
        /// <param name="element">The element to be removed.</param>
        /// <returns>The removed element.</returns>
        public static TElement RemoveElement<TElement>(this INode parent, TElement element)
            where TElement : class, IElement
        {
            if (parent == null)
                throw new ArgumentNullException("parent");

            return parent.RemoveChild(element) as TElement;
        }

        /// <summary>
        /// Returns the first element matching the selectors with the provided
        /// type, or null.
        /// </summary>
        /// <typeparam name="TElement">The type to look for.</typeparam>
        /// <param name="parent">The parent of the nodes to gather.</param>
        /// <param name="selectors">The group of selectors to use.</param>
        /// <returns>The element, if there is any.</returns>
        public static TElement QuerySelector<TElement>(this IParentNode parent, String selectors)
            where TElement : class, IElement
        {
            if (parent == null)
                throw new ArgumentNullException("parent");
            else if (selectors == null)
                throw new ArgumentNullException("selectors");

            return parent.QuerySelector(selectors) as TElement;
        }

        /// <summary>
        /// Returns a list of elements matching the selectors with the
        /// provided type.
        /// </summary>
        /// <typeparam name="TElement">The type to look for.</typeparam>
        /// <param name="parent">The parent of the nodes to gather.</param>
        /// <param name="selectors">The group of selectors to use.</param>
        /// <returns>An enumeration with the elements.</returns>
        public static IEnumerable<TElement> QuerySelectorAll<TElement>(this IParentNode parent, String selectors)
            where TElement : IElement
        {
            if (parent == null)
                throw new ArgumentNullException("parent");
            else if (selectors == null)
                throw new ArgumentNullException("selectors");

            return parent.QuerySelectorAll(selectors).OfType<TElement>();
        }

        /// <summary>
        /// Gets the descendent nodes of the given parent.
        /// </summary>
        /// <typeparam name="TNode">The type of nodes to obtain.</typeparam>
        /// <param name="parent">The parent of the nodes to gather.</param>
        /// <returns>The descendent nodes.</returns>
        public static IEnumerable<TNode> Descendents<TNode>(this INode parent)
        {
            return parent.Descendents().OfType<TNode>();
        }

        /// <summary>
        /// Gets the descendent nodes of the given parent.
        /// </summary>
        /// <param name="parent">The parent of the nodes to gather.</param>
        /// <returns>The descendent nodes.</returns>
        public static IEnumerable<INode> Descendents(this INode parent)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");

            return parent.GetDescendants();
        }

        /// <summary>
        /// Gets the ancestor nodes of the given child.
        /// </summary>
        /// <typeparam name="TNode">The type of nodes to obtain.</typeparam>
        /// <param name="child">The child of the nodes to gather.</param>
        /// <returns>The ancestor nodes.</returns>
        public static IEnumerable<TNode> Ancestors<TNode>(this INode child)
        {
            return child.Ancestors().OfType<TNode>();
        }

        /// <summary>
        /// Gets the ancestor nodes of the given child.
        /// </summary>
        /// <param name="child">The child of the nodes to gather.</param>
        /// <returns>The ancestor nodes.</returns>
        public static IEnumerable<INode> Ancestors(this INode child)
        {
            if (child == null)
                throw new ArgumentNullException("child");

            return child.GetAncestors();
        }

        #endregion

        #region Navigation extensions

        /// <summary>
        /// Navigates to the hyper reference given by the provided element
        /// without any possibility for cancellation.
        /// </summary>
        /// <typeparam name="TElement">The type of element.</typeparam>
        /// <param name="element">The element of navigation.</param>
        /// <returns>The task eventually resulting in the response.</returns>
        public static Task<IDocument> Navigate<TElement>(this TElement element)
            where TElement : IUrlUtilities, IElement
        {
            return element.Navigate(CancellationToken.None);
        }

        /// <summary>
        /// Navigates to the hyper reference given by the provided element.
        /// </summary>
        /// <typeparam name="TElement">The type of element.</typeparam>
        /// <param name="element">The element of navigation.</param>
        /// <param name="cancel">The token for cancellation.</param>
        /// <returns>The task eventually resulting in the response.</returns>
        public static Task<IDocument> Navigate<TElement>(this TElement element, CancellationToken cancel)
            where TElement : IUrlUtilities, IElement
        {
            var address = element.Href;
            var url = Url.Create(address);
            return element.Owner.Context.OpenAsync(url, cancel);
        }

        #endregion
    }
}
