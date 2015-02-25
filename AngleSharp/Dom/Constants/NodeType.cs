namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;

    /// <summary>
    /// Contains an enumeration of various node types.
    /// </summary>
    public enum NodeType : ushort
    {
        /// <summary>
        /// A standard node element.
        /// </summary>
        [DomName("ELEMENT_NODE")]
        Element = 1,
        /// <summary>
        /// An attribute node.
        /// </summary>
        [DomName("ATTRIBUTE_NODE")]
        [DomHistorical]
        Attribute = 2,
        /// <summary>
        /// A text node.
        /// </summary>
        [DomName("TEXT_NODE")]
        Text = 3,
        /// <summary>
        /// A CData text node.
        /// </summary>
        [DomName("CDATA_SECTION_NODE")]
        [DomHistorical]
        CharacterData = 4,
        /// <summary>
        /// An entity reference node.
        /// </summary>
        [DomName("ENTITY_REFERENCE_NODE")]
        [DomHistorical]
        EntityReference = 5,
        /// <summary>
        /// An entity node.
        /// </summary>
        [DomName("ENTITY_NODE")]
        [DomHistorical]
        Entity = 6,
        /// <summary>
        /// A processing instruction node.
        /// </summary>
        [DomName("PROCESSING_INSTRUCTION_NODE")]
        [DomHistorical]
        ProcessingInstruction = 7,
        /// <summary>
        /// A comment node.
        /// </summary>
        [DomName("COMMENT_NODE")]
        Comment = 8,
        /// <summary>
        /// A document node.
        /// </summary>
        [DomName("DOCUMENT_NODE")]
        Document = 9,
        /// <summary>
        /// A document type node.
        /// </summary>
        [DomName("DOCUMENT_TYPE_NODE")]
        DocumentType = 10,
        /// <summary>
        /// A document (fragment mode) node.
        /// </summary>
        [DomName("DOCUMENT_FRAGMENT_NODE")]
        DocumentFragment = 11,
        /// <summary>
        /// A notation node.
        /// </summary>
        [DomName("NOTATION_NODE")]
        [DomHistorical]
        Notation = 12
    }
}
