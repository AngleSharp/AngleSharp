using System;

namespace AngleSharp.DOM
{
    /// <summary>
    /// Enumeration of possible document position values.
    /// </summary>
    [Flags]
    public enum DocumentPosition : ushort
    {
        /// <summary>
        /// It is the same node.
        /// </summary>
        Same = 0,
        /// <summary>
        /// There is no relation.
        /// </summary>
        Disconnected = 0x01,
        /// <summary>
        /// The node preceeds the other element.
        /// </summary>
        Preceding = 0x02,
        /// <summary>
        /// The node follows the other element.
        /// </summary>
        Following = 0x04,
        /// <summary>
        /// The node is contains the other element.
        /// </summary>
        Contains = 0x08,
        /// <summary>
        /// The node is contained in the other element.
        /// </summary>
        ContainedBy = 0x10,
        /// <summary>
        /// The relation is implementation specific.
        /// </summary>
        ImplementationSpecific = 0x20
    }
}
