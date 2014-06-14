namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// The base class for all characterdata implementations.
    /// </summary>
    abstract class CharacterData : Node, ICharacterData
    {
        #region Fields

        String _content;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new instance of character data.
        /// </summary>
        internal CharacterData()
        {
            _content = String.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the element immediately preceding in this node's parent's list of nodes, 
        /// null if the current element is the first element in that list.
        /// </summary>
        public Element PreviousElementSibling
        {
            get
            {
                if (_parent == null)
                    return null;

                var found = false;

                for (int i = _parent.ChildNodes.Length - 1; i >= 0; i--)
                {
                    if (_parent.ChildNodes[i] == this)
                        found = true;
                    else if (found && _parent.ChildNodes[i] is Element)
                        return (Element)_parent.ChildNodes[i];
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the element immediately following in this node's parent's list of nodes,
        /// or null if the current element is the last element in that list.
        /// </summary>
        public Element NextElementSibling
        {
            get
            {
                if (_parent == null)
                    return null;

                var n = _parent.ChildNodes.Length;
                var found = false;

                for (int i = 0; i < n; i++)
                {
                    if (_parent.ChildNodes[i] == this)
                        found = true;
                    else if (found && _parent.ChildNodes[i] is Element)
                        return (Element)_parent.ChildNodes[i];
                }

                return null;
            }
        }

        /// <summary>
        /// Gets or sets the characters at the given position.
        /// </summary>
        /// <param name="index">The position of the character.</param>
        /// <returns>The character at the position.</returns>
        internal Char this[Int32 index]
        {
            get { return _content[index]; }
            set 
            {
                if (index < 0)
                    return;

                if (index >= Length)
                {
                    _content = _content.PadRight(index) + value.ToString();
                    return;
                }

                var chrs = _content.ToCharArray();
                chrs[index] = value;
                _content = new String(chrs);
            }
        }

        /// <summary>
        /// Gets the number of characters.
        /// </summary>
        public Int32 Length 
        { 
            get { return _content.Length; } 
        }

        /// <summary>
        /// Gets or sets the character value.
        /// </summary>
        public override String NodeValue
        {
            get { return _content; }
            set { _content = value; }
        }


        /// <summary>
        /// Gets or sets the character value.
        /// </summary>
        public override String TextContent
        {
            get { return _content; }
            set { _content = value; }
        }

        /// <summary>
        /// Gets the string data in this character node.
        /// </summary>
        public String Data
        {
            get { return _content; }
            set { _content = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a child to the collection of children.
        /// </summary>
        /// <param name="child">The child to add.</param>
        /// <returns>The added child.</returns>
        public override INode AppendChild(INode child)
        {
            throw new DomException(ErrorCode.NotSupported);
        }

        /// <summary>
        /// Inserts the specified node before a reference element as a child of the current node.
        /// </summary>
        /// <param name="newElement">The node to insert.</param>
        /// <param name="referenceElement">The node before which newElement is inserted. If
        /// referenceElement is null, newElement is inserted at the end of the list of child nodes.</param>
        /// <returns>The inserted node.</returns>
        public override INode InsertBefore(INode newElement, INode referenceElement)
        {
            throw new DomException(ErrorCode.NotSupported);
        }

        /// <summary>
        /// Inserts a child to the collection of children at the specified index.
        /// </summary>
        /// <param name="index">The index where to insert.</param>
        /// <param name="child">The child to insert.</param>
        /// <returns>The inserted child.</returns>
        public override INode InsertChild(int index, INode child)
        {
            throw new DomException(ErrorCode.NotSupported);
        }

        /// <summary>
        /// Removes a child from the collection of children.
        /// </summary>
        /// <param name="child">The child to remove.</param>
        /// <returns>The removed child.</returns>
        public override INode RemoveChild(INode child)
        {
            throw new DomException(ErrorCode.NotSupported);
        }

        /// <summary>
        /// Replaces one child node of the specified element with another.
        /// </summary>
        /// <param name="newChild">The new node to replace oldChild. If it already exists in the DOM, it is first removed.</param>
        /// <param name="oldChild">The existing child to be replaced.</param>
        /// <returns>The replaced node. This is the same node as oldChild.</returns>
        public override INode ReplaceChild(INode newChild, INode oldChild)
        {
            throw new DomException(ErrorCode.NotSupported);
        }

        /// <summary>
        /// Returns the substring of the character data starting at the offset.
        /// </summary>
        /// <param name="offset">The start index.</param>
        /// <param name="count">The number of characters.</param>
        public String Substring(Int32 offset, Int32 count)
        {
            return _content.Substring(offset, count);
        }

        /// <summary>
        /// Appends some data to the character data.
        /// </summary>
        /// <param name="data">The data to append.</param>
        public void Append(String value)
        {
            _content += value;
        }

        /// <summary>
        /// Appends some data to the character data.
        /// </summary>
        /// <param name="data">The data to append.</param>
        public void Append(Char data)
        {
            _content += data.ToString();
        }

        /// <summary>
        /// Inserts some data starting at the given offset.
        /// </summary>
        /// <param name="offset">The start index.</param>
        /// <param name="data">The data to insert.</param>
        public void Insert(Int32 offset, String data)
        {
            _content.Insert(offset, data);
        }

        /// <summary>
        /// Inserts some data starting at the given offset.
        /// </summary>
        /// <param name="offset">The start index.</param>
        /// <param name="data">The data to insert.</param>
        public void InsertData(Int32 offset, Char data)
        {
            _content.Insert(offset, data.ToString());
        }

        /// <summary>
        /// Deletes some data starting at the given offset with the given length.
        /// </summary>
        /// <param name="offset">The start index.</param>
        /// <param name="count">The length of the deletion.</param>
        public void Delete(Int32 offset, Int32 count)
        {
            _content.Remove(offset, count);
        }

        /// <summary>
        /// Replaces some data starting at the given offset with the given length.
        /// </summary>
        /// <param name="offset">The start index.</param>
        /// <param name="count">The length of the replacement.</param>
        /// <param name="data">The data to insert at the replacement.</param>
        public void Replace(Int32 offset, Int32 count, String data)
        {
            _content.Remove(offset, count).Insert(offset, data);
        }

        /// <summary>
        /// Returns an HTML-code representation of the character data.
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public override String ToHtml()
        {
            var temp = Pool.NewStringBuilder();

            for (int i = 0; i < _content.Length; i++)
            {
                switch (_content[i])
                {
                    case '&': temp.Append("&amp;");     break;
                    case '<': temp.Append("&lt;");      break;
                    case '>': temp.Append("&gt;");      break;
                    default : temp.Append(_content[i]); break;
                }
            }
            
            return temp.ToPool();
        }

        /// <summary>
        /// Inserts nodes before the current node.
        /// </summary>
        /// <param name="nodes">The nodes to insert before.</param>
        /// <returns>The current element.</returns>
        public void Before(params INode[] nodes)
        {
            if (_parent != null && nodes.Length > 0)
            {
                var node = MutationMacro(nodes);
                _parent.InsertBefore(node, this);
            }
        }

        /// <summary>
        /// Inserts nodes after the current node.
        /// </summary>
        /// <param name="nodes">The nodes to insert after.</param>
        /// <returns>The current element.</returns>
        public void After(params INode[] nodes)
        {
            if (_parent != null && nodes.Length > 0)
            {
                var node = MutationMacro(nodes);
                _parent.InsertBefore(node, NextSibling);
            }
        }

        /// <summary>
        /// Replaces the current node with the nodes.
        /// </summary>
        /// <param name="nodes">The nodes to replace.</param>
        public void Replace(params INode[] nodes)
        {
            if (_parent != null && nodes.Length > 0)
            {
                var node = MutationMacro(nodes);
                _parent.ReplaceChild(node, this);
            }
        }

        /// <summary>
        /// Removes the current element from the parent.
        /// </summary>
        public void Remove()
        {
            if (_parent != null)
                _parent.RemoveChild(this);
        }

        #endregion
    }
}
