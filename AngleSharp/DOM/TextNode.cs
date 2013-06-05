using System;
using System.Text;

namespace AngleSharp.DOM
{
    /// <summary>
    /// Represents a text node.
    /// </summary>
    public sealed class TextNode : CharacterData
    {
        #region ctor

        /// <summary>
        /// Creates a new empty text node.
        /// </summary>
        public TextNode()
        {
            NodeType = NodeType.Text;
            _name = "#text";
        }

        /// <summary>
        /// Creates a new text node with the given text.
        /// </summary>
        /// <param name="text">The text to set.</param>
        public TextNode(string text)
            : this()
        {
            AppendData(text);
        }

        /// <summary>
        /// Creates a new text node with the given character.
        /// </summary>
        /// <param name="c">The character to set.</param>
        public TextNode(char c)
            : this()
        {
            AppendData(c);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the TextNode is "empty".
        /// </summary>
        internal bool IsEmpty
        {
            get
            {
                for (int i = 0; i < Length; i++)
                {
                    if (!Specification.IsSpaceCharacter(this[i]))
                        return false;
                }

                return true;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        public override Node CloneNode(bool deep = true)
        {
            var node = new TextNode(Data);
            CopyProperties(this, node, deep);
            return node;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a string containing the text in quotation mark.
        /// </summary>
        /// <returns>A string containing the text content.</returns>
        public override string ToString()
        {
            return '"' + Data + '"';
        }

        /// <summary>
        /// Returns an HTML-code representation of the node.
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public override string ToHtml()
        {
            return Data;
        }

        #endregion
    }
}
