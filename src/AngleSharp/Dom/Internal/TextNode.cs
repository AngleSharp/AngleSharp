namespace AngleSharp.Dom
{
    using AngleSharp.Text;
    using System;
    using System.Text;

    /// <summary>
    /// Represents a text node.
    /// </summary>
    sealed class TextNode : CharacterData, IText
    {
        #region ctor

        internal TextNode(Document owner)
            : this(owner, String.Empty)
        {
        }

        internal TextNode(Document owner, String text)
            : base(owner, "#text", NodeType.Text, text)
        {
        }

        #endregion

        #region Properties

        internal Boolean IsEmpty
        {
            get
            {
                for (var i = 0; i < Length; i++)
                {
                    if (!this[i].IsSpaceCharacter())
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public String Text
        {
            get
            {
                var previous = PreviousSibling;
                var start = this;
                using var sb = new ValueStringBuilder(1_024);

                while (previous is TextNode)
                {
                    start = (TextNode)previous;
                    previous = start.PreviousSibling;
                }

                do
                {
                    sb.Append(start.Data);
                    start = start.NextSibling as TextNode;
                }
                while (start != null);

                return sb.ToString();
            }
        }

        public IElement? AssignedSlot
        {
            get
            {
                var parent = ParentElement!;

                if (parent.IsShadow())
                {
                    var tree = parent.ShadowRoot;
                    return tree?.GetAssignedSlot(null);
                }

                return null;
            }
        }

        #endregion

        #region Methods

        public IText Split(Int32 offset)
        {
            var length = Length;

            if (offset > length)
                throw new DomException(DomError.IndexSizeError);

            var count = length - offset;
            var newData = Substring(offset, count);
            var newNode = new TextNode(Owner, newData);
            var parent = Parent;
            var owner = Owner;

            if (parent != null)
            {
                var index = this.Index();
                parent.InsertBefore(newNode, NextSibling);

                foreach (var m in owner.GetAttachedReferences<Range>())
                {
                    if (m.Head == this && m.Start > offset)
                    {
                        m.StartWith(newNode, m.Start - offset);
                    }

                    if (m.Tail == this && m.End > offset)
                    {
                        m.EndWith(newNode, m.End - offset);
                    }

                    if (m.Head == parent && m.Start == index + 1)
                    {
                        m.StartWith(parent, m.Start + 1);
                    }

                    if (m.Tail == parent && m.End == index + 1)
                    {
                        m.StartWith(parent, m.End + 1);
                    }
                }
            }

            Replace(offset, count, String.Empty);

            if (parent != null)
            {
                foreach (var m in owner.GetAttachedReferences<Range>())
                {
                    if (m.Head == this && m.Start > offset)
                    {
                        m.StartWith(this, offset);
                    }
                    if (m.Tail == this && m.End > offset)
                    {
                        m.EndWith(this, offset);
                    }
                }
            }

            return newNode;
        }

        public override Node Clone(Document owner, Boolean deep)
        {
            var node = new TextNode(owner, Data);
            CloneNode(node, owner, deep);
            return node;
        }

        #endregion
    }
}
