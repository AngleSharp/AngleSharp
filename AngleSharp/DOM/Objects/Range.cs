namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// Represents a range of DOM nodes.
    /// </summary>
    [DOM("Range")]
    public sealed class Range : IRange
    {
        #region ctor

        internal Range()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the node that starts the container.
        /// </summary>
        public Node StartContainer
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the offset of the StartContainer in the document.
        /// </summary>
        public Int32 StartOffset
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the node that ends the container.
        /// </summary>
        public Node EndContainer
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the offset of the EndContainer in the document.
        /// </summary>
        public Int32 EndOffset
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets a value that indicates if the representation is collapsed.
        /// </summary>
        public Boolean Collapsed
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the common ancestor node of the contained range.
        /// </summary>
        public Node CommonAncestorContainer
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Selects the start of the given range by using the
        /// given reference node and a relative offset.
        /// </summary>
        /// <param name="refNode">The reference node to use.</param>
        /// <param name="offset">The offset relative to the reference node.</param>
        public void SetStart(Node refNode, Int32 offset)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Selects the end of the given range by using the
        /// given reference node and a relative offset.
        /// </summary>
        /// <param name="refNode">The reference node to use.</param>
        /// <param name="offset">The offset relative to the reference node.</param>
        public void SetEnd(Node refNode, Int32 offset)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Selects the start of the given range by using an
        /// inclusive reference node.
        /// </summary>
        /// <param name="refNode">The reference node to use.</param>
        public void SetStartBefore(Node refNode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Selects the end of the given range by using an
        /// inclusive reference node.
        /// </summary>
        /// <param name="refNode">The reference node to use.</param>
        public void SetEndBefore(Node refNode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Selects the start of the given range by using an
        /// exclusive reference node.
        /// </summary>
        /// <param name="refNode"></param>
        public void SetStartAfter(Node refNode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Selects the end of the given range by using an
        /// exclusive reference node.
        /// </summary>
        /// <param name="refNode">The referenced node.</param>
        public void SetEndAfter(Node refNode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Collapses the range to a single level.
        /// </summary>
        /// <param name="toStart">Determines if only the first level should be selected.</param>
        public void Collapse(Boolean toStart)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Selects the contained node.
        /// </summary>
        /// <param name="refNode">The node to use.</param>
        public void SelectNode(Node refNode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Selects the contained nodes by taking
        /// a reference node as origin.
        /// </summary>
        /// <param name="refNode">The reference node.</param>
        public void SelectNodeContents(Node refNode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clears the contained nodes.
        /// </summary>
        public void DeleteContents()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clears the node representation and returns a document fragment
        /// with the originally contained nodes.
        /// </summary>
        /// <returns>The document fragment containing the nodes.</returns>
        public DocumentFragment ExtractContents()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a document fragement of the contained nodes.
        /// </summary>
        /// <returns>The created document fragment.</returns>
        public DocumentFragment CloneContents()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Inserts a node into the range.
        /// </summary>
        /// <param name="node">The node to include.</param>
        public void InsertNode(Node node)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Includes the given node with its siblings in the range.
        /// </summary>
        /// <param name="newParent">The range to surround.</param>
        public void SurroundContents(Node newParent)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a copy of this range.
        /// </summary>
        /// <returns></returns>
        public Range CloneRange()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Detaches the range from the DOM tree.
        /// </summary>
        public void Detach()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if the given node is within this range by using a offset.
        /// </summary>
        /// <param name="node">The node to check for.</param>
        /// <param name="offset">The offset to use.</param>
        /// <returns>True if the point is within the range, otherwise false.</returns>
        public Boolean IsPointInRange(Node node, Int32 offset)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Compares the boundary points of the range.
        /// </summary>
        /// <param name="how">Determines how these points should be compared.</param>
        /// <param name="sourceRange">The range of the other boundary points.</param>
        /// <returns>A relative position.</returns>
        public RangePosition CompareBoundaryPoints(RangeType how, Range sourceRange)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Compares the node to the given offset and returns the relative position.
        /// </summary>
        /// <param name="node">The node to use.</param>
        /// <param name="offset">The offset to use.</param>
        /// <returns>The relative position in the range.</returns>
        public RangePosition ComparePoint(Node node, Int32 offset)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if the given node is contained in this range.
        /// </summary>
        /// <param name="node">The node to check for.</param>
        /// <returns>True if the node is within the range, otherwise false.</returns>
        public Boolean IntersectsNode(Node node)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
