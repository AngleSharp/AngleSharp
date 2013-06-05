using System;

namespace AngleSharp.DOM
{
    /// <summary>
    /// Represents a reference to an entity.
    /// </summary>
    public sealed class EntityReference : Node
    {
        #region ctor

        /// <summary>
        /// Creates a new entity node.
        /// </summary>
        public EntityReference()
        {
            NodeType = NodeType.EntityReference;
        }

        /// <summary>
        /// Creates a new entity node.
        /// </summary>
        /// <param name="name">Name of the entity reference.</param>
        public EntityReference(string name)
            : this()
        {
            _name = name;
        }
        #endregion
    }
}
