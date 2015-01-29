namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;

    /// <summary>
    /// The TreeWalker object represents the nodes of a document subtree and a
    /// position within them.
    /// </summary>
    [DomName("TreeWalker")]
    public interface ITreeWalker
    {
        /// <summary>
        /// Gets a Node representing the root node as specified when the
        /// TreeWalker was created.
        /// </summary>
        [DomName("root")]
        INode Root { get; }

        /// <summary>
        /// Gets or sets the Node on which the TreeWalker is currently pointing
        /// at.
        /// </summary>
        [DomName("currentNode")]
        INode Current { get; set; }

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
        /// Moves the current Node to the next visible node in the document
        /// order, and returns the found node. It also moves the current node
        /// to this one. If no such node exists, returns null and the current
        /// node is not changed.
        /// </summary>
        /// <returns>The next Node, if any.</returns>
        [DomName("nextNode")]
        INode ToNext();

        /// <summary>
        /// Moves the current Node to the previous visible node in the document
        /// order, and returns the found node. It also moves the current node
        /// to this one. If no such node exists,or if it is before that the
        /// root node defined at the object construction, returns null and the
        /// current node is not changed.
        /// </summary>
        /// <returns>The previous Node, if any.</returns>
        [DomName("previousNode")]
        INode ToPrevious();

        /// <summary>
        /// Moves the current Node to the first visible ancestor node in the
        /// document order, and returns the found node. It also moves the
        /// current node to this one. If no such node exists, or if it is
        /// before that the root node defined at the object construction,
        /// returns null and the current node is not changed.
        /// </summary>
        /// <returns></returns>
        [DomName("parentNode")]
        INode ToParent();

        /// <summary>
        /// Moves the current Node to the first visible child of the current
        /// node, and returns the found child. It also moves the current node
        /// to this child. If no such child exists, returns null and the
        /// current node is not changed.
        /// </summary>
        /// <returns></returns>
        [DomName("firstChild")]
        INode ToFirst();

        /// <summary>
        /// Moves the current Node to the last visible child of the current
        /// node, and returns the found child. It also moves the current node
        /// to this child. If no such child exists, returns null and the
        /// current node is not changed.
        /// </summary>
        /// <returns></returns>
        [DomName("lastChild")]
        INode ToLast();

        /// <summary>
        /// Moves the current Node to its previous sibling, if any, and returns
        /// the found sibling. I there is no such node, return null and the
        /// current node is not changed.
        /// </summary>
        /// <returns></returns>
        [DomName("previousSibling")]
        INode ToPreviousSibling();

        /// <summary>
        /// Moves the current Node to its next sibling, if any, and returns the
        /// found sibling. I there is no such node, return null and the current
        /// node is not changed.
        /// </summary>
        /// <returns></returns>
        [DomName("nextSibling")]
        INode ToNextSibling();
    }
}
