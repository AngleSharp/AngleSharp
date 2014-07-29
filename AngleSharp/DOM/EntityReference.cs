namespace AngleSharp.DOM
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
        internal EntityReference()
            : this(String.Empty)
        {
        }

        /// <summary>
        /// Creates a new entity node.
        /// </summary>
        /// <param name="name">Name of the entity reference.</param>
        internal EntityReference(String name)
            : base(name, NodeType.EntityReference)
        {
        }
        #endregion
    }
}
