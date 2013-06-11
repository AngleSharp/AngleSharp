using System;
using System.Text;

namespace AngleSharp.DOM
{
    /// <summary>
    /// The base class for all characterdata implementations.
    /// </summary>
    public abstract class CharacterData : Node
    {
        #region Members

        StringBuilder sb;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new instance of character data.
        /// </summary>
        public CharacterData()
        {
            sb = new StringBuilder();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the characters at the given position.
        /// </summary>
        /// <param name="index">The position of the character.</param>
        /// <returns>The character at the position.</returns>
        internal char this[int index]
        {
            get { return sb[index]; }
            set { sb[index] = value; }
        }

        /// <summary>
        /// Gets the number of characters.
        /// </summary>
        public int Length 
        { 
            get { return sb.Length; } 
        }

        /// <summary>
        /// Gets or sets the character value.
        /// </summary>
        public override string NodeValue
        {
            get { return sb.ToString(); }
            set { sb.Remove(0, sb.Length).Append(value); }
        }


        /// <summary>
        /// Gets or sets the character value.
        /// </summary>
        public override string TextContent
        {
            get { return sb.ToString(); }
            set { sb.Remove(0, sb.Length).Append(value); }
        }

        /// <summary>
        /// Gets the string data in this character node.
        /// </summary>
        public string Data
        {
            get { return sb.ToString(); }
            set { sb.Remove(0, sb.Length).Append(value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a child to the collection of children.
        /// </summary>
        /// <param name="child">The child to add.</param>
        /// <returns>The added child.</returns>
        public override Node AppendChild(Node child)
        {
            throw new DOMException(ErrorCode.NotSupported);
        }

        /// <summary>
        /// Inserts the specified node before a reference element as a child of the current node.
        /// </summary>
        /// <param name="newElement">The node to insert.</param>
        /// <param name="referenceElement">The node before which newElement is inserted. If
        /// referenceElement is null, newElement is inserted at the end of the list of child nodes.</param>
        /// <returns>The inserted node.</returns>
        public override Node InsertBefore(Node newElement, Node referenceElement)
        {
            throw new DOMException(ErrorCode.NotSupported);
        }

        /// <summary>
        /// Inserts a child to the collection of children at the specified index.
        /// </summary>
        /// <param name="index">The index where to insert.</param>
        /// <param name="child">The child to insert.</param>
        /// <returns>The inserted child.</returns>
        public override Node InsertChild(int index, Node child)
        {
            throw new DOMException(ErrorCode.NotSupported);
        }

        /// <summary>
        /// Removes a child from the collection of children.
        /// </summary>
        /// <param name="child">The child to remove.</param>
        /// <returns>The removed child.</returns>
        public override Node RemoveChild(Node child)
        {
            throw new DOMException(ErrorCode.NotSupported);
        }

        /// <summary>
        /// Replaces one child node of the specified element with another.
        /// </summary>
        /// <param name="newChild">The new node to replace oldChild. If it already exists in the DOM, it is first removed.</param>
        /// <param name="oldChild">The existing child to be replaced.</param>
        /// <returns>The replaced node. This is the same node as oldChild.</returns>
        public override Node ReplaceChild(Node newChild, Node oldChild)
        {
            throw new DOMException(ErrorCode.NotSupported);
        }

        /// <summary>
        /// Returns the substring of the character data starting at the offset.
        /// </summary>
        /// <param name="offset">The start index.</param>
        /// <param name="count">The number of characters.</param>
        /// <returns>The current instance.</returns>
        public string SubstringData(int offset, int count)
        {
            return sb.ToString(offset, count);
        }

        /// <summary>
        /// Appends some data to the character data.
        /// </summary>
        /// <param name="data">The data to append.</param>
        /// <returns>The current instance.</returns>
        public CharacterData AppendData(string data)
        {
            sb.Append(data);
            return this;
        }

        /// <summary>
        /// Appends some data to the character data.
        /// </summary>
        /// <param name="data">The data to append.</param>
        /// <returns>The current instance.</returns>
        public CharacterData AppendData(char data)
        {
            sb.Append(data);
            return this;
        }

        /// <summary>
        /// Inserts some data starting at the given offset.
        /// </summary>
        /// <param name="offset">The start index.</param>
        /// <param name="data">The data to insert.</param>
        /// <returns>The current instance.</returns>
        public CharacterData InsertData(int offset, string data)
        {
            sb.Insert(offset, data);
            return this;
        }

        /// <summary>
        /// Inserts some data starting at the given offset.
        /// </summary>
        /// <param name="offset">The start index.</param>
        /// <param name="data">The data to insert.</param>
        /// <returns>The current instance.</returns>
        public CharacterData InsertData(int offset, char data)
        {
            sb.Insert(offset, data);
            return this;
        }

        /// <summary>
        /// Deletes some data starting at the given offset with the given length.
        /// </summary>
        /// <param name="offset">The start index.</param>
        /// <param name="count">The length of the deletion.</param>
        /// <returns>The current instance.</returns>
        public CharacterData DeleteData(int offset, int count)
        {
            sb.Remove(offset, count);
            return this;
        }

        /// <summary>
        /// Replaces some data starting at the given offset with the given length.
        /// </summary>
        /// <param name="offset">The start index.</param>
        /// <param name="count">The length of the replacement.</param>
        /// <param name="data">The data to insert at the replacement.</param>
        /// <returns>The current instance.</returns>
        public CharacterData ReplaceData(int offset, int count, string data)
        {
            sb.Remove(offset, count).Insert(offset, data);
            return this;
        }

        /// <summary>
        /// Returns an HTML-code representation of the character data.
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public override string ToHtml()
        {
            var temp = new StringBuilder();

            for (int i = 0; i < sb.Length; i++)
            {
                switch (sb[i])
                {
                    case '&':
                        temp.Append("&amp;");
                        break;
                    case '<':
                        temp.Append("&lt;");
                        break;
                    case '>':
                        temp.Append("&gt;");
                        break;
                    default:
                        temp.Append(sb[i]);
                        break;
                }
            }
            
            return temp.ToString();
        }

        #endregion
    }
}
