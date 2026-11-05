namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the options of finding the root of a node.
    /// </summary>
    [DomName("GetRootNodeOptions")]
    public sealed class GetRootNodeOptions
    {
        /// <summary>
        /// Gets or sets if the search goes on from a shadow root to its host.
        /// </summary>
        [DomName("composed")]
        public Boolean? IsComposed { get; set; }
    }

    /// <summary>
    /// Tells what a document is like for those looking at it, which is always on the screen here.
    /// </summary>
    [DomExposed("Document")]
    public static class DocumentStateExtensions
    {
        /// <summary>
        /// Gets the element scrolling the page.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns>The root element.</returns>
        [DomName("scrollingElement")]
        [DomAccessor(Accessors.Getter)]
        public static IElement? GetScrollingElement(this IDocument document) => document.DocumentElement;

        /// <summary>
        /// Gets if the document is hidden.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns>False.</returns>
        [DomName("hidden")]
        [DomAccessor(Accessors.Getter)]
        public static Boolean GetHidden(this IDocument document) => false;

        /// <summary>
        /// Gets the visibility of the document.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns>visible</returns>
        [DomName("visibilityState")]
        [DomAccessor(Accessors.Getter)]
        public static String GetVisibilityState(this IDocument document) => "visible";

        /// <summary>
        /// Gets the encoding of the document, the same as characterSet.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns>The name of the encoding.</returns>
        [DomName("charset")]
        [DomAccessor(Accessors.Getter)]
        public static String GetCharset(this IDocument document) => document.CharacterSet;
    }

    /// <summary>
    /// Finds the root of a node. An extension, as what implements the interface would break otherwise.
    /// </summary>
    [DomExposed("Node")]
    public static class NodeRootExtensions
    {
        /// <summary>
        /// Gets if the other node is this very node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="other">The node to compare with.</param>
        /// <returns>True if they are the same.</returns>
        [DomName("isSameNode")]
        public static Boolean IsSameNode(this INode node, INode? other) => ReferenceEquals(node, other);

        /// <summary>
        /// Gets the root of the tree the node is in, which is the node itself if it has no parent.
        /// </summary>
        /// <param name="node">The node to start at.</param>
        /// <param name="options">The options of the search.</param>
        /// <returns>The root.</returns>
        [DomName("getRootNode")]
        public static INode GetRootNode(this INode node, GetRootNodeOptions? options = null)
        {
            var current = node;

            while (true)
            {
                if (current.Parent is INode parent)
                {
                    current = parent;
                }
                else if (options?.IsComposed == true && current is IShadowRoot shadow && shadow.Host is INode host)
                {
                    current = host;
                }
                else
                {
                    return current;
                }
            }
        }
    }
}
