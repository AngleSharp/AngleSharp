namespace AngleSharp.DOM
{
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
        internal TextNode()
            : this(String.Empty)
        {
        }

        /// <summary>
        /// Creates a new text node with the given text.
        /// </summary>
        /// <param name="text">The text to set.</param>
        internal TextNode(String text)
            : base("#text", NodeType.Text, text)
        {
        }

        /// <summary>
        /// Creates a new text node with the given character.
        /// </summary>
        /// <param name="c">The character to set.</param>
        internal TextNode(Char c)
            : this(c.ToString())
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
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        public override INode Clone(Boolean deep = true)
        {
            var node = new TextNode(Data);
            CopyProperties(this, node, deep);
            return node;
        }

        /// <summary>
        /// Creates a new text node with the content starting at the specified offset.
        /// Adds the new node to the DOM as a sibling. Truncates the current node.
        /// </summary>
        /// <param name="offset">The position where the split should occur.</param>
        /// <returns>The freshly created text node.</returns>
        public IText Split(Int32 offset)
        {
            var length = Length;

            if (offset > length)
                throw new DomException(ErrorCode.IndexSizeError);

            var count = length - offset;
            var newData = Substring(offset, count);
            var newNode = new TextNode(newData) { Owner = Owner };
            var parent = Parent;

            if (parent != null)
            {
                parent.InsertBefore(newNode, NextSibling);

                //TODO Range
                // For each range whose start node is node and start offset is greater than offset, set its start node to new node and decrease its start offset by offset. 
                // For each range whose end node is node and end offset is greater than offset, set its end node to new node and decrease its end offset by offset. 
                // For each range whose start node is parent and start offset is equal to the index of node + 1, increase its start offset by one. 
                // For each range whose end node is parent and end offset is equal to the index of node + 1, increase its end offset by one.
            }

            Replace(offset, count, String.Empty);

            if (parent != null)
            {
                //TODO Range
                // For each range whose start node is node and start offset is greater than offset, set its start offset to offset. 
                // For each range whose end node is node and end offset is greater than offset, set its end offset to offset.
            }

            return newNode;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns an HTML-code representation of the node.
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public override String ToHtml()
        {
            if (Parent != null && Parent.Flags.HasFlag(NodeFlags.LiteralText))
                return Data;

            return base.ToHtml();
        }

        #endregion
    }
}
