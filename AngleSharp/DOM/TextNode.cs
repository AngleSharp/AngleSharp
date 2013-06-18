using System;
using System.Text;

namespace AngleSharp.DOM
{
    /// <summary>
    /// Represents a text node.
    /// </summary>
    [DOM("Text")]
    public sealed class TextNode : CharacterData
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
        internal TextNode(string text)
            : this()
        {
            AppendData(text);
        }

        /// <summary>
        /// Creates a new text node with the given character.
        /// </summary>
        /// <param name="c">The character to set.</param>
        internal TextNode(char c)
            : this()
        {
            AppendData(c);
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
        [DOM("cloneNode")]
        public override Node CloneNode(Boolean deep = true)
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
