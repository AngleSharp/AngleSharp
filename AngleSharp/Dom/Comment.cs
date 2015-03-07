namespace AngleSharp.Dom
{
    using System;

    /// <summary>
    /// Represents a node that contains a comment.
    /// </summary>
    sealed class Comment : CharacterData, IComment
    {
        #region ctor

        /// <summary>
        /// Creates a new comment node.
        /// </summary>
        internal Comment(Document owner)
            : this(owner, String.Empty)
        {
        }

        /// <summary>
        /// Creates a new comment node with the given data.
        /// </summary>
        /// <param name="owner">The initial owner.</param>
        /// <param name="data">The data to be initially set.</param>
        internal Comment(Document owner, String data)
            : base(owner, "#comment", NodeType.Comment, data)
        {
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
            var node = new Comment(Owner, Data);
            CopyProperties(this, node, deep);
            return node;
        }

        /// <summary>
        /// Returns an HTML-code representation of the comment.
        /// </summary>
        /// <param name="formatter">The formatter to use.</param>
        /// <returns>A string containing the HTML code.</returns>
        public override String ToHtml(IMarkupFormatter formatter)
        {
            return formatter.Comment(this);
        }

        #endregion
    }
}
