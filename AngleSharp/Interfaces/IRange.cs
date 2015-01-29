namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// The Range interface represents a fragment of a document that can
    /// contain nodes and parts of text nodes in a given document.
    /// </summary>
    [DomName("Range")]
    public interface IRange
    {
        /// <summary>
        /// Gets the node that starts the container.
        /// </summary>
        [DomName("startContainer")]
        INode Head { get; }

        /// <summary>
        /// Gets the offset of the StartContainer in the document.
        /// </summary>
        [DomName("startOffset")]
        Int32 Start { get; }

        /// <summary>
        /// Gets the node that ends the container.
        /// </summary>
        [DomName("endContainer")]
        INode Tail { get; }

        /// <summary>
        /// Gets the offset of the EndContainer in the document.
        /// </summary>
        [DomName("endOffset")]
        Int32 End { get; }

        /// <summary>
        /// Gets a value that indicates if the representation is collapsed.
        /// </summary>
        [DomName("collapsed")]
        Boolean IsCollapsed { get; }

        /// <summary>
        /// Gets the common ancestor node of the contained range.
        /// </summary>
        [DomName("commonAncestorContainer")]
        INode CommonAncestor { get; }

        /// <summary>
        /// Selects the start of the given range by using the given reference
        /// node and a relative offset.
        /// </summary>
        /// <param name="refNode">The reference node to use.</param>
        /// <param name="offset">
        /// The offset relative to the reference node.
        /// </param>
        [DomName("setStart")]
        void StartWith(INode refNode, Int32 offset);

        /// <summary>
        /// Selects the end of the given range by using the given reference
        /// node and a relative offset.
        /// </summary>
        /// <param name="refNode">The reference node to use.</param>
        /// <param name="offset">
        /// The offset relative to the reference node.
        /// </param>
        [DomName("setEnd")]
        void EndWith(INode refNode, Int32 offset);

        /// <summary>
        /// Selects the start of the given range by using an inclusive
        /// reference node.
        /// </summary>
        /// <param name="refNode">The reference node to use.</param>
        [DomName("setStartBefore")]
        void StartBefore(INode refNode);

        /// <summary>
        /// Selects the end of the given range by using an inclusive reference
        /// node.
        /// </summary>
        /// <param name="refNode">The reference node to use.</param>
        [DomName("setEndBefore")]
        void EndBefore(INode refNode);

        /// <summary>
        /// Selects the start of the given range by using an exclusive
        /// reference node.
        /// </summary>
        /// <param name="refNode">The reference node to use.</param>
        [DomName("setStartAfter")]
        void StartAfter(INode refNode);

        /// <summary>
        /// Selects the end of the given range by using an exclusive reference
        /// node.
        /// </summary>
        /// <param name="refNode">The referenced node.</param>
        [DomName("setEndAfter")]
        void EndAfter(INode refNode);

        /// <summary>
        /// Collapses the range to a single level.
        /// </summary>
        /// <param name="toStart">
        /// Determines if only the first level should be selected.
        /// </param>
        [DomName("collapse")]
        void Collapse(Boolean toStart);

        /// <summary>
        /// Selects the contained node.
        /// </summary>
        /// <param name="refNode">The node to use.</param>
        [DomName("selectNode")]
        void Select(INode refNode);

        /// <summary>
        /// Selects the contained nodes by taking a reference node as origin.
        /// </summary>
        /// <param name="refNode">The reference node.</param>
        [DomName("selectNodeContents")]
        void SelectContent(INode refNode);

        /// <summary>
        /// Clears the contained nodes.
        /// </summary>
        [DomName("deleteContents")]
        void ClearContent();

        /// <summary>
        /// Clears the node representation and returns a document fragment with
        /// the originally contained nodes.
        /// </summary>
        /// <returns>The document fragment containing the nodes.</returns>
        [DomName("extractContents")]
        IDocumentFragment ExtractContent();

        /// <summary>
        /// Creates a document fragement of the contained nodes.
        /// </summary>
        /// <returns>The created document fragment.</returns>
        [DomName("cloneContents")]
        IDocumentFragment CopyContent();

        /// <summary>
        /// Inserts a node into the range.
        /// </summary>
        /// <param name="node">The node to include.</param>
        [DomName("insertNode")]
        void Insert(INode node);

        /// <summary>
        /// Includes the given node with its siblings in the range.
        /// </summary>
        /// <param name="newParent">The range to surround.</param>
        [DomName("surroundContents")]
        void Surround(INode newParent);

        /// <summary>
        /// Creates a copy of this range.
        /// </summary>
        /// <returns>The copy representing the same range.</returns>
        [DomName("cloneRange")]
        IRange Clone();

        /// <summary>
        /// Detaches the range from the DOM tree.
        /// </summary>
        [DomName("detach")]
        void Detach();

        /// <summary>
        /// Checks if the given node is within this range by using a offset.
        /// </summary>
        /// <param name="node">The node to check for.</param>
        /// <param name="offset">The offset to use.</param>
        /// <returns>
        /// True if the point is within the range, otherwise false.
        /// </returns>
        [DomName("isPointInRange")]
        Boolean Contains(INode node, Int32 offset);

        /// <summary>
        /// Compares the boundary points of the range.
        /// </summary>
        /// <param name="how">
        /// Determines how these points should be compared.
        /// </param>
        /// <param name="sourceRange">
        /// The range of the other boundary points.
        /// </param>
        /// <returns>A relative position.</returns>
        [DomName("compareBoundaryPoints")]
        RangePosition CompareBoundaryTo(RangeType how, IRange sourceRange);

        /// <summary>
        /// Compares the node to the given offset and returns the relative
        /// position.
        /// </summary>
        /// <param name="node">The node to use.</param>
        /// <param name="offset">The offset to use.</param>
        /// <returns>The relative position in the range.</returns>
        [DomName("comparePoint")]
        RangePosition CompareTo(INode node, Int32 offset);

        /// <summary>
        /// Checks if the given node is contained in this range.
        /// </summary>
        /// <param name="node">The node to check for.</param>
        /// <returns>
        /// True if the node is within the range, otherwise false.
        /// </returns>
        [DomName("intersectsNode")]
        Boolean Intersects(INode node);
    }
}
