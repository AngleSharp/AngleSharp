namespace AngleSharp.DOM
{
    using System;
    using System.Text;

    /// <summary>
    /// Represents a text node.
    /// </summary>
    public sealed class TextNode : CharacterData, IText
    {
        #region ctor

        /// <summary>
        /// Creates a new empty text node.
        /// </summary>
        internal TextNode()
        {
            _type = NodeType.Text;
            _name = "#text";
        }

        /// <summary>
        /// Creates a new text node with the given text.
        /// </summary>
        /// <param name="text">The text to set.</param>
        internal TextNode(String text)
            : this()
        {
            Append(text);
        }

        /// <summary>
        /// Creates a new text node with the given character.
        /// </summary>
        /// <param name="c">The character to set.</param>
        internal TextNode(Char c)
            : this()
        {
            Append(c);
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
        /// Gets the stored text content.
        /// </summary>
        public String Text
        {
            get { return Data; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        public override Node CloneNode(Boolean deep = true)
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
            _parent.InsertBefore(element, NextSibling);
            return element;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a special textual representation of the node.
        /// </summary>
        /// <returns>A string containing only (rendered) text.</returns>
        public override String ToText()
        {
            if (IsEmpty && Length > 0)
                return " ";

            return Data.Trim();
        }

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
