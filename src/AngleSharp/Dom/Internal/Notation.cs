namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a notation node.
    /// </summary>
    [DomName("Notation")]
    public sealed class Notation : Node
    {
        #region ctor

        /// <summary>
        /// Creates a new notation node.
        /// </summary>
        public Notation(Document owner, String name)
            : base(owner, name, NodeType.Notation)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the public identifier.
        /// </summary>
        [DomName("publicId")]
        public String PublicId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value of the system identifier.
        /// </summary>
        [DomName("systemId")]
        public String SystemId
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <returns>The duplicate node.</returns>

        public override Node Clone(Document newOwner, Boolean deep)
        {
            var node = new Notation(newOwner, NodeName);
            CloneNode(node, newOwner, deep);
            return node;
        }

        #endregion
    }
}
