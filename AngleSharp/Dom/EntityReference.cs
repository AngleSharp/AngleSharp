namespace AngleSharp.Dom
{
    using System;

    /// <summary>
    /// Represents a reference to an entity.
    /// </summary>
    sealed class EntityReference : Node
    {
        #region ctor

        /// <summary>
        /// Creates a new entity node.
        /// </summary>
        internal EntityReference(Document owner)
            : this(owner, String.Empty)
        {
        }

        /// <summary>
        /// Creates a new entity node.
        /// </summary>
        /// <param name="owner">The initial owner.</param>
        /// <param name="name">Name of the entity reference.</param>
        internal EntityReference(Document owner, String name)
            : base(owner, name, NodeType.EntityReference)
        {
        }

        #endregion

        #region Methods

        public override INode Clone(Boolean deep = true)
        {
            var node = new EntityReference(Owner, NodeName);
            CopyProperties(this, node, deep);
            return node;
        }

        #endregion
    }
}
