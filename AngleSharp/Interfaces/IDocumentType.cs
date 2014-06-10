namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// The DocumentType interface represents a Node containing a doctype.
    /// </summary>
    [DomName("DocumentType")]
    public interface IDocumentType : INode
    {
        /// <summary>
        /// Gets or sets the name of the document type.
        /// </summary>
        [DomName("name")]
        String Name { get; }

        /// <summary>
        /// Gets or sets the public ID of the document type.
        /// </summary>
        [DomName("publicId")]
        String PublicIdentifier { get; }

        /// <summary>
        /// Gets or sets the system ID of the document type.
        /// </summary>
        [DomName("systemId")]
        String SystemIdentifier { get; }

        /// <summary>
        /// Inserts nodes before the current doctype.
        /// </summary>
        /// <param name="nodes">The nodes to insert before.</param>
        [DomName("before")]
        void Before(params Node[] nodes);

        /// <summary>
        /// Inserts nodes after the current doctype.
        /// </summary>
        /// <param name="nodes">The nodes to insert after.</param>
        [DomName("after")]
        void After(params Node[] nodes);

        /// <summary>
        /// Replaces the current doctype with the nodes.
        /// </summary>
        /// <param name="nodes">The nodes to replace.</param>
        [DomName("replace")]
        void Replace(params Node[] nodes);

        /// <summary>
        /// Removes the current doctype from the parent.
        /// </summary>
        [DomName("remove")]
        void Remove();
    }
}
