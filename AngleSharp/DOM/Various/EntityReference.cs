using System;

namespace AngleSharp.DOM
{
    /// <summary>
    /// Represents a reference to an entity.
    /// </summary>
    [DOM("EntityReference")]
    public sealed class EntityReference : Node
    {
        #region ctor

        /// <summary>
        /// Creates a new entity node.
        /// </summary>
        internal EntityReference()
        {
            _type = NodeType.EntityReference;
        }

        /// <summary>
        /// Creates a new entity node.
        /// </summary>
        /// <param name="name">Name of the entity reference.</param>
        internal EntityReference(String name)
            : this()
        {
            _name = name;
        }
        #endregion
    }
}
