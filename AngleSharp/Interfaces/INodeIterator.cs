namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// The NodeIterator interface represents an iterator over the members of a
    /// list of the nodes in a subtree of the DOM. The nodes will be returned
    /// in document order.
    /// </summary>
    [DomName("NodeIterator")]
    public interface INodeIterator
    {
        /// <summary>
        /// Gets a Node representing the root node as specified when the
        /// NodeIterator was created.
        /// </summary>
        [DomName("root")]
        INode Root { get; }

        /// <summary>
        /// Gets the Node to which the iterator is anchored.
        /// </summary>
        [DomName("referenceNode")]
        INode Reference { get; }

        /// <summary>
        /// Gets an indicator whether the NodeFilter is anchored before the
        /// reference node.
        /// </summary>
        [DomName("pointerBeforeReferenceNode")]
        Boolean IsBeforeReference { get; }

        /// <summary>
        /// Gets a description of the types of nodes that must to be presented.
        /// Non-matching nodes are skipped, but their children may be included,
        /// if relevant.
        /// </summary>
        [DomName("whatToShow")]
        FilterSettings Settings { get; }

        /// <summary>
        /// Gets the NodeFilter used to select the relevant nodes.
        /// </summary>
        [DomName("filter")]
        NodeFilter Filter { get; }

        /// <summary>
        /// Returns the next Node in the document, or null if there are none.
        /// </summary>
        /// <returns>The next Node, if any.</returns>
        [DomName("nextNode")]
        INode Next();

        /// <summary>
        /// Returns the previous Node in the document, or null if there are
        /// none.
        /// </summary>
        /// <returns>The previous Node, if any.</returns>
        [DomName("previousNode")]
        INode Previous();
    }
}