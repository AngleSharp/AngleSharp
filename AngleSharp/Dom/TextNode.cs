namespace AngleSharp.Dom
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents a text node.
    /// </summary>
    sealed class TextNode : CharacterData, IText
    {
        #region ctor

        /// <summary>
        /// Creates a new empty text node.
        /// </summary>
        internal TextNode(Document owner)
            : this(owner, String.Empty)
        {
        }

        /// <summary>
        /// Creates a new text node with the given text.
        /// </summary>
        /// <param name="owner">The initial owner.</param>
        /// <param name="text">The text to set.</param>
        internal TextNode(Document owner, String text)
            : base(owner, "#text", NodeType.Text, text)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the TextNode is "empty".
        /// </summary>
        internal Boolean IsEmpty
        {
            get
            {
                for (int i = 0; i < Length; i++)
                {
                    if (!this[i].IsSpaceCharacter())
                        return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Gets the whole text content of adjacent nodes.
        /// </summary>
        public String Text
        {
            get
            {
                var previous = PreviousSibling;
                var start = this;
                var sb = Pool.NewStringBuilder();

                while (previous is TextNode)
                {
                    start = (TextNode)previous;
                    previous = start.PreviousSibling;
                }

                do
                {
                    sb.Append(start.Data);
                    start = start.NextSibling as TextNode;
                }
                while (start != null);

                return sb.ToPool();
            }
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
            var node = new TextNode(Owner, Data);
            CopyProperties(this, node, deep);
            return node;
        }

        /// <summary>
        /// Creates a new text node with the content starting at the specified
        /// offset. Adds the new node to the DOM as a sibling. Truncates the
        /// current node.
        /// </summary>
        /// <param name="offset">
        /// The position where the split should occur.
        /// </param>
        /// <returns>The freshly created text node.</returns>
        public IText Split(Int32 offset)
        {
            var length = Length;

            if (offset > length)
                throw new DomException(DomError.IndexSizeError);

            var count = length - offset;
            var newData = Substring(offset, count);
            var newNode = new TextNode(Owner, newData);
            var parent = Parent;
            var owner = Owner;

            if (parent != null)
            {
                var index = this.Index();
                parent.InsertBefore(newNode, NextSibling);

                owner.ForEachRange(m => m.Head == this && m.Start > offset, m => m.StartWith(newNode, m.Start - offset));
                owner.ForEachRange(m => m.Tail == this && m.End > offset, m => m.EndWith(newNode, m.End - offset));
                owner.ForEachRange(m => m.Head == parent && m.Start == index + 1, m => m.StartWith(parent, m.Start + 1));
                owner.ForEachRange(m => m.Tail == parent && m.End == index + 1, m => m.StartWith(parent, m.End + 1));
            }

            Replace(offset, count, String.Empty);

            if (parent != null)
            {
                owner.ForEachRange(m => m.Head == this && m.Start > offset, m => m.StartWith(this, offset));
                owner.ForEachRange(m => m.Tail == this && m.End > offset, m => m.EndWith(this, offset));
            }

            return newNode;
        }

        /// <summary>
        /// Returns an HTML-code representation of the node.
        /// </summary>
        /// <param name="formatter">The formatter to use.</param>
        /// <returns>A string containing the HTML code.</returns>
        public override String ToHtml(IMarkupFormatter formatter)
        {
            if (Parent != null && Parent.Flags.HasFlag(NodeFlags.LiteralText))
                return Data;

            return base.ToHtml(formatter);
        }

        #endregion
    }
}
