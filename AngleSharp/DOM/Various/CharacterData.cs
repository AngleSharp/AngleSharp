using System;
using System.Text;

namespace AngleSharp.DOM
{
    /// <summary>
    /// The base class for all characterdata implementations.
    /// </summary>
    [DOM("CharacterData")]
    public abstract class CharacterData : Node
    {
        #region Members

        StringBuilder sb;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new instance of character data.
        /// </summary>
        internal CharacterData()
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
        internal Char this[Int32 index]
        {
            get { return sb[index]; }
            set { sb[index] = value; }
        }

        /// <summary>
        /// Gets the number of characters.
        /// </summary>
        [DOM("length")]
        public Int32 Length 
        { 
            get { return sb.Length; } 
        }

        /// <summary>
        /// Gets or sets the character value.
        /// </summary>
        [DOM("nodeValue")]
        public override String NodeValue
        {
            get { return sb.ToString(); }
            set { sb.Remove(0, sb.Length).Append(value); }
        }


        /// <summary>
        /// Gets or sets the character value.
        /// </summary>
        [DOM("textContent")]
        public override String TextContent
        {
            get { return sb.ToString(); }
            set { sb.Remove(0, sb.Length).Append(value); }
        }

        /// <summary>
        /// Gets the string data in this character node.
        /// </summary>
        [DOM("data")]
        public String Data
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
        [DOM("appendChild")]
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
        [DOM("insertBefore")]
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
        [DOM("insertChild")]
        public override Node InsertChild(int index, Node child)
        {
            throw new DOMException(ErrorCode.NotSupported);
        }

        /// <summary>
        /// Removes a child from the collection of children.
        /// </summary>
        /// <param name="child">The child to remove.</param>
        /// <returns>The removed child.</returns>
        [DOM("removeChild")]
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
        [DOM("replaceChild")]
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
        [DOM("substringData")]
        public String SubstringData(Int32 offset, Int32 count)
        {
            return sb.ToString(offset, count);
        }

        /// <summary>
        /// Appends some data to the character data.
        /// </summary>
        /// <param name="data">The data to append.</param>
        /// <returns>The current instance.</returns>
        [DOM("appendData")]
        public CharacterData AppendData(String data)
        {
            sb.Append(data);
            return this;
        }

        /// <summary>
        /// Appends some data to the character data.
        /// </summary>
        /// <param name="data">The data to append.</param>
        /// <returns>The current instance.</returns>
        public CharacterData AppendData(Char data)
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
        [DOM("insertData")]
        public CharacterData InsertData(Int32 offset, String data)
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
        public CharacterData InsertData(Int32 offset, Char data)
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
        [DOM("deleteData")]
        public CharacterData DeleteData(Int32 offset, Int32 count)
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
        [DOM("replaceData")]
        public CharacterData ReplaceData(Int32 offset, Int32 count, String data)
        {
            sb.Remove(offset, count).Insert(offset, data);
            return this;
        }

        /// <summary>
        /// Returns an HTML-code representation of the character data.
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public override String ToHtml()
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
