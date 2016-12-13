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

        public override INode Clone(Boolean deep = true)
        {
            var node = new Comment(Owner, Data);
            CloneNode(node, deep);
            return node;
        }

        public override void ToHtml(TextWriter writer, IMarkupFormatter formatter)
        {
            writer.Write(formatter.Comment(this));
        }

        #endregion
    }
}
