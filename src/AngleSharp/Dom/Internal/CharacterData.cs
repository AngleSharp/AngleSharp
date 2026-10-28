namespace AngleSharp.Dom
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// The base class for all characterdata implementations.
    /// </summary>
    abstract class CharacterData : Node, ICharacterData
    {
        #region Fields

        /// <summary>
        /// A String, or a StringBuilder once the tree builder's appends have taken the content
        /// past <see cref="PendingThreshold"/>: concatenating every append would copy the whole
        /// run each time, quadratic for text that keeps growing (e.g. split by ignored tags).
        /// </summary>
        private Object _content;

        private const Int32 PendingThreshold = 1024;

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

        public IElement? PreviousElementSibling
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
                        else if (found && parent.ChildNodes[i] is IElement childEl)
                        {
                            return childEl;
                        }
                    }
                }

                return null;
            }
        }

        public IElement? NextElementSibling
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
                        else if (found && parent.ChildNodes[i] is IElement childEl)
                        {
                            return childEl;
                        }
                    }
                }

                return null;
            }
        }

        internal Char this[Int32 index]
        {
            get => Content[index];
            set
            {
                if (index >= 0)
                {
                    if (index >= Length)
                    {
                        _content = Content.PadRight(index) + value.ToString();
                    }
                    else
                    {
                        var chrs = Content.ToCharArray();
                        chrs[index] = value;
                        _content = new String(chrs);
                    }

                    // Nothing in tree construction writes a single character, so this stays a
                    // mutation and keeps the version advance the raw appends below gave up.
                    Owner?.MarkMutated();
                }
            }
        }

        public Int32 Length => _content is StringBuilder pending ? pending.Length : ((String)_content).Length;

        public sealed override String NodeValue
        {
            get => Data;
            set => Data = value;
        }

        public sealed override String TextContent
        {
            get => Data;
            set => Data = value;
        }

        public String Data
        {
            get => Content;
            set => Replace(0, Length, value);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the content as a string, turning pending appends into one first.
        /// </summary>
        private String Content
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _content as String ?? Materialize();
        }

        // A parsed document can be read from several threads at once, and the first read of a
        // node whose appends are still pending is a write. The lock makes it one ToString, and
        // every reader returns the same string.
        private String Materialize()
        {
            var pending = _content;

            if (pending is String done)
            {
                return done;
            }

            lock (pending)
            {
                if (_content is StringBuilder builder)
                {
                    var value = builder.ToString();
                    Volatile.Write(ref _content, value);
                    return value;
                }

                return (String)_content;
            }
        }

        public String Substring(Int32 offset, Int32 count)
        {
            var content = Content;
            var length = content.Length;

            if (offset > length)
            {
                throw new DomException(DomError.IndexSizeError);
            }

            if (offset + count > length)
            {
                return content.Substring(offset);
            }

            return content.Substring(offset, count);
        }

        public void Append(String value) => Replace(Length, 0, value);

        public void Insert(Int32 offset, String data) => Replace(offset, 0, data);

        public void Delete(Int32 offset, Int32 count) => Replace(offset, count, String.Empty);

        public void Replace(Int32 offset, Int32 count, String data) => Replace(offset, count, data, reported: true);

        /// <summary>
        /// Appends to the content the way the tree builder does: no mutation record, no advance of
        /// the document's mutation version. Nothing else is needed either - an append at the end of
        /// the content can move no range boundary, since every boundary already sits at or before
        /// it, which is what makes this the cheap half of the replace data steps.
        /// </summary>
        internal void AppendData(String value)
        {
            if (_content is StringBuilder pending)
            {
                pending.Append(value);
            }
            else
            {
                var content = (String)_content;
                var length = content.Length + value.Length;
                _content = length < PendingThreshold ? content + value : new StringBuilder(content, 2 * length).Append(value);
            }
        }

        /// <summary>
        /// Inserts into the content the way the tree builder does when it foster parents text: no
        /// mutation record and no advance of the document's mutation version, but the attached
        /// ranges still follow the text they point into.
        /// </summary>
        internal void InsertData(Int32 offset, String value) => Replace(offset, 0, value, reported: false);

        private void Replace(Int32 offset, Int32 count, String data, Boolean reported)
        {
            var owner = Owner;
            var content = Content;
            var length = content.Length;

            if (offset > length)
            {
                throw new DomException(DomError.IndexSizeError);
            }

            if (offset + count > length)
            {
                count = length - offset;
            }

            var previous = content;
            var deleteOffset = offset + data.Length;
            content = content.Insert(offset, data);

            if (count > 0)
            {
                content = content.Remove(deleteOffset, count);
            }

            _content = content;

            if (reported)
            {
                // The replace data steps are what decided a mutation happened, so this is where
                // the version moves - AppendData and InsertData above are the raw writes.
                owner.MarkMutated();

                // Building the record is an allocation on every character data write, and
                // QueueMutation throws it away again when nothing is observing.
                if (owner.HasMutationObservers)
                {
                    owner.QueueMutation(MutationRecord.CharacterData(target: this, previousValue: previous));
                }
            }

            foreach (var m in owner.GetAttachedReferences<Range>())
            {
                if (m.Head == this && m.Start > offset && m.Start <= offset + count)
                {
                    m.StartWith(this, offset);
                }
                if (m.Tail == this && m.End > offset && m.End <= offset + count)
                {
                    m.EndWith(this, offset);
                }
                if (m.Head == this && m.Start > offset + count)
                {
                    m.StartWith(this, m.Start + data.Length - count);
                }
                if (m.Tail == this && m.End > offset + count)
                {
                    m.EndWith(this, m.End + data.Length - count);
                }
            }
        }

        public void Before(params INode[] nodes) => this.InsertBefore(nodes);

        public void After(params INode[] nodes) => this.InsertAfter(nodes);

        public void Replace(params INode[] nodes) => this.ReplaceWith(nodes);

        public void Remove() => this.RemoveFromParent();

        #endregion
    }
}
