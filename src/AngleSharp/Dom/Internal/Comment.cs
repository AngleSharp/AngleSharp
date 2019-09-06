namespace AngleSharp.Dom
{
    using System;
    using System.IO;

    /// <summary>
    /// Represents a node that contains a comment.
    /// </summary>
    sealed class Comment : CharacterData, IComment
    {
        #region ctor

        internal Comment(Document owner)
            : this(owner, String.Empty)
        {
        }

        internal Comment(Document owner, String data)
            : base(owner, "#comment", NodeType.Comment, data)
        {
        }

        #endregion

        #region Methods

        public override Node Clone(Document owner, Boolean deep)
        {
            var node = new Comment(owner, Data);
            CloneNode(node, owner, deep);
            return node;
        }

        #endregion
    }
}
