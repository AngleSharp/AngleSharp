namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;

    /// <summary>
    /// Extensions to ChildNode nodes that are not document type nodes.
    /// </summary>
    [DomName("NonDocumentTypeChildNode")]
    [DomNoInterfaceObject]
    public interface INonDocumentTypeChildNode
    {
        /// <summary>
        /// Gets the Element immediately following this ChildNode in its
        /// parent's children list, or null if there is no Element in the list
        /// following this ChildNode.
        /// </summary>
        [DomName("nextElementSibling")]
        IElement NextElementSibling { get; }

        /// <summary>
        /// Gets the Element immediately prior to this ChildNode in its
        /// parent's children list, or null if there is no Element in the list
        /// prior to this ChildNode.
        /// </summary>
        [DomName("previousElementSibling")]
        IElement PreviousElementSibling { get; }
    }
}
