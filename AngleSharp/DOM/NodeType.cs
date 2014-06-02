namespace AngleSharp.DOM
{
    /// <summary>
    /// Contains an enumeration of various node types.
    /// </summary>
    public enum NodeType : ushort
    {
        /// <summary>
        /// A standard node element.
        /// </summary>
        Element,
        /// <summary>
        /// A text node.
        /// </summary>
        Text,
        /// <summary>
        /// A CData text node.
        /// </summary>
        CData,
        /// <summary>
        /// An entity reference node.
        /// </summary>
        EntityReference,
        /// <summary>
        /// An entity node.
        /// </summary>
        Entity,
        /// <summary>
        /// A processing instruction node.
        /// </summary>
        ProcessingInstruction,
        /// <summary>
        /// A comment node.
        /// </summary>
        Comment,
        /// <summary>
        /// A document node.
        /// </summary>
        Document,
        /// <summary>
        /// A document type node.
        /// </summary>
        DocumentType,
        /// <summary>
        /// A document (fragment mode) node.
        /// </summary>
        DocumentFragment,
        /// <summary>
        /// A notation node.
        /// </summary>
        Notation
    }
}
