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
            : base(text)
        {
            _type = NodeType.Text;
            _name = "#text";
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
                    sb.Append(start.Text);
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
            var element = new TextNode(Data.Substring(offset));
            Data = Data.Substring(0, offset);
            Parent.InsertBefore(element, NextSibling);
            return element;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a string containing the text in quotation mark.
        /// </summary>
        /// <returns>A string containing the text content.</returns>
        public override String ToString()
        {
            return '"' + Data + '"';
        }

        /// <summary>
        /// Returns an HTML-code representation of the node.
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public override String ToHtml()
        {
            return Data;
        }

        #endregion
    }
}
