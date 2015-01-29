namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// The enumeration with the flags for setting the NodeIterator.
    /// </summary>
    [Flags]
    [DomName("NodeFilter")]
    public enum FilterSettings : ulong
    {
        /// <summary>
        /// All nodes will be considered.
        /// </summary>
        [DomName("SHOW_ALL")]
        All = 0xffffffff,
        /// <summary>
        /// Elements will be shown.
        /// </summary>
        [DomName("SHOW_ELEMENT")]
        Element = 0x1,
        /// <summary>
        /// Attributes will be shown.
        /// </summary>
        [DomName("SHOW_ATTRIBUTE")]
        [DomHistorical]
        Attribute = 0x2,
        /// <summary>
        /// Text nodes will be shown.
        /// </summary>
        [DomName("SHOW_TEXT")]
        Text = 0x4,
        /// <summary>
        /// CData sections will be shown.
        /// </summary>
        [DomName("SHOW_CDATA_SECTION")]
        [DomHistorical]
        CharacterData = 0x8,
        /// <summary>
        /// EntityReferences will be shown.
        /// </summary>
        [DomName("SHOW_ENTITY_REFERENCE")]
        [DomHistorical]
        EntityReference = 0x10,
        /// <summary>
        /// Entities will be shown.
        /// </summary>
        [DomName("SHOW_ENTITY")]
        [DomHistorical]
        Entity = 0x20,
        /// <summary>
        /// ProcessingInstructions will be shown.
        /// </summary>
        [DomName("SHOW_PROCESSING_INSTRUCTION")]
        ProcessingInstruction = 0x40,
        /// <summary>
        /// Comments will be shown.
        /// </summary>
        [DomName("SHOW_COMMENT")]
        Comment = 0x80,
        /// <summary>
        /// Documents will be shown.
        /// </summary>
        [DomName("SHOW_DOCUMENT")]
        Document = 0x100,
        /// <summary>
        /// DocumentTypes will be shown.
        /// </summary>
        [DomName("SHOW_DOCUMENT_TYPE")]
        DocumentType = 0x200,
        /// <summary>
        /// DocumentFragments will be shown.
        /// </summary>
        [DomName("SHOW_DOCUMENT_FRAGMENT")]
        DocumentFragment = 0x400,
        /// <summary>
        /// Notations will be shown.
        /// </summary>
        [DomName("SHOW_NOTATION")]
        [DomHistorical]
        Notation = 0x800
    }
}
