namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// The ChildNode interface contains methods that are
    /// particular to Node objects that can have a parent.
    /// </summary>
    [DomName("ChildNode")]
    public interface IChildNode
    {
        /// <summary>
        /// Inserts nodes just before the current node.
        /// </summary>
        /// <param name="nodes">The nodes to insert.</param>
        [DomName("before")]
        void Before(params INode[] nodes);

        /// <summary>
        /// Inserts nodes just after the current node.
        /// </summary>
        /// <param name="nodes">The nodes to insert.</param>
        [DomName("after")]
        void After(params INode[] nodes);

        /// <summary>
        /// Replaces the current node with nodes.
        /// </summary>
        /// <param name="nodes">The nodes to insert.</param>
        [DomName("replace")]
        void Replace(params INode[] nodes);

        /// <summary>
        /// Removes the current node.
        /// </summary>
        [DomName("remove")]
        void Remove();

        /// <summary>
        /// Gets the Element immediately following this ChildNode in its
        /// parent's children list, or null if there is no Element in the
        /// list following this ChildNode.
        /// </summary>
        [DomName("nextElementSibling")]
        Element NextElementSibling { get; }

        /// <summary>
        /// Gets the Element immediately prior to this ChildNode in its
        /// parent's children list, or null if there is no Element in the
        /// list prior to this ChildNode.
        /// </summary>
        [DomName("previousElementSibling")]
        Element PreviousElementSibling { get; }
    }
}
