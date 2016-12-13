namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;

    /// <summary>
    /// The ChildNode interface contains methods that are particular to Node
    /// objects that can have a parent.
    /// </summary>
    [DomName("ChildNode")]
    [DomNoInterfaceObject]
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
    }
}
