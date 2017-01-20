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

        #region Helpers

        internal override Node Clone(Document owner, Boolean deep)
        {
            var node = new Notation(owner)
            {
                PublicId = PublicId,
                SystemId = SystemId
            };
            CloneNode(node, owner, deep);
            return node;
        }

        #endregion
    }
}
