namespace AngleSharp.Dom
{
    using AngleSharp.Extensions;
    using System;
    using System.IO;

    /// <summary>
    /// The base class for all characterdata implementations.
    /// </summary>
    abstract class CharacterData : Node, ICharacterData
    {
        #region Fields

        private String _content;

        #endregion

        #region ctor

        internal CharacterData(Document owner, String name, NodeType type)
            : this(owner, name, type, String.Empty)
        {
        }

        internal CharacterData(Document owner, String name, NodeType type, String content)
            : base(owner, name, type)
        {
            _content = content;
        }

        #endregion

        #region Properties

        public IElement PreviousElementSibling
        {
            get
            {
                var parent = Parent;

                if (parent != null)
                {
                    var found = false;

                    for (var i = parent.ChildNodes.Length - 1; i >= 0; i--)
                    {
                        if (Object.ReferenceEquals(parent.ChildNodes[i], this))
                        {
                            found = true;
                        }
                        else if (found && parent.ChildNodes[i] is IElement)
                        {
                            return (IElement)parent.ChildNodes[i];
                        }
                    }
                }

                return null;
            }
        }

        public IElement NextElementSibling
        {
            get
            {
                var parent = Parent;
                
                if (parent != null)
                {
                    var n = parent.ChildNodes.Length;
                    var found = false;

                    for (var i = 0; i < n; i++)
                    {
                        if (Object.ReferenceEquals(parent.ChildNodes[i], this))
                        {
                            found = true;
                        }
                        else if (found && parent.ChildNodes[i] is IElement)
                        {
                            return (IElement)parent.ChildNodes[i];
                        }
                    }
                }

                return null;
            }
        }

        internal Char this[Int32 index]
        {
            get { return _content[index]; }
            set 
            {
                if (index >= 0)
                {
                    if (index >= Length)
                    {
                        _content = _content.PadRight(index) + value.ToString();
                    }
                    else
                    {
                        var chrs = _content.ToCharArray();
                        chrs[index] = value;
                        _content = new String(chrs);
                    }
                }
            }
        }

        public Int32 Length 
        { 
            get { return _content.Length; } 
        }

        public sealed override String NodeValue
        {
            get { return Data; }
            set { Data = value; }
        }

        public sealed override String TextContent
        {
            get { return Data; }
            set { Data = value; }
        }

        public String Data
        {
            get { return _content; }
            set { Replace(0, Length, value); }
        }

        #endregion

        #region Methods

        public String Substring(Int32 offset, Int32 count)
        {
            var length = _content.Length;

            if (offset > length)
                throw new DomException(DomError.IndexSizeError);

            if (offset + count > length)
            {
                return _content.Substring(offset);
            }

            return _content.Substring(offset, count);
        }

        public void Append(String value)
        {
            Replace(_content.Length, 0, value);
        }

        public void Insert(Int32 offset, String data)
        {
            Replace(offset, 0, data);
        }

        public void Delete(Int32 offset, Int32 count)
        {
            Replace(offset, count, String.Empty);
        }

        public void Replace(Int32 offset, Int32 count, String data)
        {
            var owner = Owner;
            var length = _content.Length;

            if (offset > length)
                throw new DomException(DomError.IndexSizeError);

            if (offset + count > length)
            {
                count = length - offset;
            }
            
            owner.QueueMutation(MutationRecord.CharacterData(target: this, previousValue: _content));

            var deleteOffset = offset + data.Length;
            _content = _content.Insert(offset, data).Remove(deleteOffset, count);

            owner.ForEachRange(m => m.Head == this && m.Start > offset && m.Start <= offset + count, m => m.StartWith(this, offset));
            owner.ForEachRange(m => m.Tail == this && m.End > offset && m.End <= offset + count, m => m.EndWith(this, offset));
            owner.ForEachRange(m => m.Head == this && m.Start > offset + count, m => m.StartWith(this, m.Start + data.Length - count));
            owner.ForEachRange(m => m.Tail == this && m.End > offset + count, m => m.EndWith(this, m.End + data.Length - count));
        }
        
        public override void ToHtml(TextWriter writer, IMarkupFormatter formatter)
        {
            writer.Write(formatter.Text(_content));
        }

        public void Before(params INode[] nodes)
        {
            this.InsertBefore(nodes);
        }

        public void After(params INode[] nodes)
        {
            this.InsertAfter(nodes);
        }

        public void Replace(params INode[] nodes)
        {
            this.ReplaceWith(nodes);
        }

        public void Remove()
        {
            this.RemoveFromParent();
        }

        #endregion
    }
}
