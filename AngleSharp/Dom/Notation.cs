namespace AngleSharp.Dom
{
    using System;

    /// <summary>
    /// Represents a notation node.
    /// </summary>
    sealed class Notation : Node
    {
        #region ctor

        /// <summary>
        /// Creates a new notation node.
        /// </summary>
        internal Notation(Document owner)
            : base(owner, "#notation", NodeType.Notation)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the public identifier.
        /// </summary>
        public String PublicId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value of the system identifier.
        /// </summary>
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
        /// <param name="deep">
        /// Optional value: true if the children of the node should also be
        /// cloned, or false to clone only the specified node.
        /// </param>
        /// <returns>The duplicate node.</returns>
        public override INode Clone(Boolean deep = true)
        {
            var node = new Notation(Owner)
            {
                PublicId = PublicId,
                SystemId = SystemId
            };
            CopyProperties(this, node, deep);
            return node;
        }

        #endregion
    }
}
