namespace AngleSharp.Dom
{
    using AngleSharp.Extensions;
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
        /// <param name="owner">The initial owner.</param>
        /// <param name="name">The name of the node.</param>
        /// <param name="type">The exact type of the node.</param>
        internal CharacterData(Document owner, String name, NodeType type)
            : this(owner, name, type, String.Empty)
        {
        }

        /// <summary>
        /// Creates a new instance of character data with the
        /// provided initial content.
        /// </summary>
        /// <param name="owner">The initial owner.</param>
        /// <param name="name">The name of the node.</param>
        /// <param name="type">The exact type of the node.</param>
        /// <param name="content">The content to set.</param>
        internal CharacterData(Document owner, String name, NodeType type, String content)
            : base(owner, name, type)
        {
            _content = content;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the element immediately preceding in this node's parent's list
        /// of nodes,  null if the current element is the first element in that
        /// list.
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
        /// Gets the element immediately following in this node's parent's list
        /// of nodes, or null if the current element is the last element in that
        /// list.
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
                throw new DomException(DomError.IndexSizeError);

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
        /// Deletes some data starting at the given offset with the given
        /// length.
        /// </summary>
        /// <param name="offset">The start index.</param>
        /// <param name="count">The length of the deletion.</param>
        public void Delete(Int32 offset, Int32 count)
        {
            Replace(offset, count, String.Empty);
        }

        /// <summary>
        /// Replaces some data starting at the given offset with the given
        /// length.
        /// </summary>
        /// <param name="offset">The start index.</param>
        /// <param name="count">The length of the replacement.</param>
        /// <param name="data">The data to insert at the replacement.</param>
        public void Replace(Int32 offset, Int32 count, String data)
        {
            var owner = Owner;
            var length = _content.Length;

            if (offset > length)
                throw new DomException(DomError.IndexSizeError);

            if (offset + count > length)
                count = length - offset;
            
            owner.QueueMutation(MutationRecord.CharacterData(target: this, previousValue: _content));

            var deleteOffset = offset + data.Length;
            _content = _content.Insert(offset, data).Remove(deleteOffset, count);

            owner.ForEachRange(m => m.Head == this && m.Start > offset && m.Start <= offset + count, m => m.StartWith(this, offset));
            owner.ForEachRange(m => m.Tail == this && m.End > offset && m.End <= offset + count, m => m.EndWith(this, offset));
            owner.ForEachRange(m => m.Head == this && m.Start > offset + count, m => m.StartWith(this, m.Start + data.Length - count));
            owner.ForEachRange(m => m.Tail == this && m.End > offset + count, m => m.EndWith(this, m.End + data.Length - count));
        }
        
        /// <summary>
        /// Returns an HTML-code representation of the character data.
        /// </summary>
        /// <param name="formatter">The formatter to use.</param>
        /// <returns>A string containing the HTML code.</returns>
        public override String ToHtml(IMarkupFormatter formatter)
        {
            return formatter.Text(_content);
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
