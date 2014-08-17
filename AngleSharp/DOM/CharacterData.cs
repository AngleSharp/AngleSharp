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
        /// <param name="name">The name of the node.</param>
        /// <param name="type">The exact type of the node.</param>
        internal CharacterData(String name, NodeType type)
            : this(name, type, String.Empty)
        {
        }

        /// <summary>
        /// Creates a new instance of character data with the
        /// provided initial content.
        /// </summary>
        /// <param name="name">The name of the node.</param>
        /// <param name="type">The exact type of the node.</param>
        /// <param name="content">The content to set.</param>
        internal CharacterData(String name, NodeType type, String content)
            : base(name, type)
        {
            _content = content;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the element immediately preceding in this node's parent's list of nodes, 
        /// null if the current element is the first element in that list.
        /// </summary>
        public IElement PreviousElementSibling
        {
            get
            {
                var parent = Parent;
                
                if (parent == null)
                    return null;

                var found = false;

                for (int i = parent.ChildNodes.Length - 1; i >= 0; i--)
                {
                    if (parent.ChildNodes[i] == this)
                        found = true;
                    else if (found && parent.ChildNodes[i] is IElement)
                        return (IElement)parent.ChildNodes[i];
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the element immediately following in this node's parent's list of nodes,
        /// or null if the current element is the last element in that list.
        /// </summary>
        public IElement NextElementSibling
        {
            get
            {
                var parent = Parent;
                
                if (parent == null)
                    return null;

                var n = parent.ChildNodes.Length;
                var found = false;

                for (int i = 0; i < n; i++)
                {
                    if (parent.ChildNodes[i] == this)
                        found = true;
                    else if (found && parent.ChildNodes[i] is IElement)
                        return (IElement)parent.ChildNodes[i];
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
        public sealed override String NodeValue
        {
            get { return Data; }
            set { Data = value; }
        }


        /// <summary>
        /// Gets or sets the character value.
        /// </summary>
        public sealed override String TextContent
        {
            get { return Data; }
            set { Data = value; }
        }

        /// <summary>
        /// Gets the string data in this character node.
        /// </summary>
        public String Data
        {
            get { return _content; }
            set { Replace(0, Length, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the substring of the character data starting at the offset.
        /// </summary>
        /// <param name="offset">The start index.</param>
        /// <param name="count">The number of characters.</param>
        public String Substring(Int32 offset, Int32 count)
        {
            var length = _content.Length;

            if (offset > length)
                throw new DomException(ErrorCode.IndexSizeError);

            if (offset + count > length)
                return _content.Substring(offset);

            return _content.Substring(offset, count);
        }

        /// <summary>
        /// Appends some data to the character data.
        /// </summary>
        /// <param name="value">The data to append.</param>
        public void Append(String value)
        {
            Replace(_content.Length, 0, value);
        }

        /// <summary>
        /// Inserts some data starting at the given offset.
        /// </summary>
        /// <param name="offset">The start index.</param>
        /// <param name="data">The data to insert.</param>
        public void Insert(Int32 offset, String data)
        {
            Replace(offset, 0, data);
        }

        /// <summary>
        /// Deletes some data starting at the given offset with the given length.
        /// </summary>
        /// <param name="offset">The start index.</param>
        /// <param name="count">The length of the deletion.</param>
        public void Delete(Int32 offset, Int32 count)
        {
            Replace(offset, count, String.Empty);
        }

        /// <summary>
        /// Replaces some data starting at the given offset with the given length.
        /// </summary>
        /// <param name="offset">The start index.</param>
        /// <param name="count">The length of the replacement.</param>
        /// <param name="data">The data to insert at the replacement.</param>
        public void Replace(Int32 offset, Int32 count, String data)
        {
            //TODO Mutation implemented
            //TODO Range connected ...
            var length = _content.Length;

            if (offset > length)
                throw new DomException(ErrorCode.IndexSizeError);

            if (offset + count > length)
                count = length - offset;
            
            //Queue a mutation record of "characterData" for node with oldValue node's data.
            //var record = new MutationRecord
            //{
            //    Target = this,
            //    PreviousValue = data
            //};

            var deleteOffset = offset + data.Length;
            _content = _content.Insert(offset, data).Remove(deleteOffset, count);

            //For each range whose start node is node and start offset is greater than offset but less than or equal to offset plus count, set its start offset to offset. 
            //For each range whose end node is node and end offset is greater than offset but less than or equal to offset plus count, set its end offset to offset. 
            //For each range whose start node is node and start offset is greater than offset plus count, increase its start offset by the number of code units in data, then decrease it by count. 
            //For each range whose end node is node and end offset is greater than offset plus count, increase its end offset by the number of code units in data, then decrease it by count.
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
                    case Specification.Ampersand: temp.Append("&amp;"); break;
                    case Specification.NoBreakSpace: temp.Append("&nbsp;"); break;
                    case Specification.GreaterThan: temp.Append("&lt;"); break;
                    case Specification.LessThan: temp.Append("&gt;"); break;
                    default: temp.Append(_content[i]); break;
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
            this.InsertBefore(nodes);
        }

        /// <summary>
        /// Inserts nodes after the current node.
        /// </summary>
        /// <param name="nodes">The nodes to insert after.</param>
        /// <returns>The current element.</returns>
        public void After(params INode[] nodes)
        {
            this.InsertAfter(nodes);
        }

        /// <summary>
        /// Replaces the current node with the nodes.
        /// </summary>
        /// <param name="nodes">The nodes to replace.</param>
        public void Replace(params INode[] nodes)
        {
            this.ReplaceWith(nodes);
        }

        /// <summary>
        /// Removes the current element from the parent.
        /// </summary>
        public void Remove()
        {
            this.RemoveFromParent();
        }

        #endregion
    }
}
