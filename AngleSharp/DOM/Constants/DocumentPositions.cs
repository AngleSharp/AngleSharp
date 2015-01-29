namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Enumeration of possible document position values.
    /// </summary>
    [Flags]
    public enum DocumentPositions : ushort
    {
        /// <summary>
        /// It is the same node.
        /// </summary>
        Same = 0,
        /// <summary>
        /// There is no relation.
        /// </summary>
        [DomName("DOCUMENT_POSITION_DISCONNECTED")]
        Disconnected = 0x01,
        /// <summary>
        /// The node preceeds the other element.
        /// </summary>
        [DomName("DOCUMENT_POSITION_PRECEDING")]
        Preceding = 0x02,
        /// <summary>
        /// The node follows the other element.
        /// </summary>
        [DomName("DOCUMENT_POSITION_FOLLOWING")]
        Following = 0x04,
        /// <summary>
        /// The node is contains the other element.
        /// </summary>
        [DomName("DOCUMENT_POSITION_CONTAINS")]
        Contains = 0x08,
        /// <summary>
        /// The node is contained in the other element.
        /// </summary>
        [DomName("DOCUMENT_POSITION_CONTAINED_BY")]
        ContainedBy = 0x10,
        /// <summary>
        /// The relation is implementation specific.
        /// </summary>
        [DomName("DOCUMENT_POSITION_IMPLEMENTATION_SPECIFIC")]
        ImplementationSpecific = 0x20
    }
}
