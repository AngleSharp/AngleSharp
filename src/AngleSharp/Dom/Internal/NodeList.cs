namespace AngleSharp.Dom
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using Html.Construction;

    /// <summary>
    /// Represents a list of Node instances or nodes.
    /// </summary>
    /// <remarks>
    /// The entries live in an array with a free region at the front as well as at the end,
    /// so taking the first child off or putting one before it is as cheap as appending: a
    /// List would shift every remaining child each time, and moving all children of a large
    /// node to the front of another one would be quadratic. An insertion or removal in the
    /// middle shifts whichever side is shorter.
    /// </remarks>
    sealed class NodeList : INodeList, IConstructableNodeList
    {
        #region Fields

        private const Int32 DefaultCapacity = 4;

        private Node[] _items;
        private Int32 _start;
        private Int32 _count;
        private Int32 _version;

        /// <summary>
        /// Gets an empty node-list. Shouldn't be modified.
        /// </summary>
        internal static readonly NodeList Empty = [];

        #endregion

        #region ctor

        internal NodeList()
        {
            _items = [];
        }

        #endregion

        #region Index

        public Node this[Int32 index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if ((UInt32)index >= (UInt32)_count)
                {
                    ThrowIndexOutOfRange();
                }

                return _items[_start + index];
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if ((UInt32)index >= (UInt32)_count)
                {
                    ThrowIndexOutOfRange();
                }

                _items[_start + index] = value;
                _version++;
            }
        }

        INode INodeList.this[Int32 index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this[index];
        }

        #endregion

        #region Properties

        public Int32 Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _count;
        }

        #endregion

        #region Internal Methods

        internal void Add(Node node)
        {
            var end = _start + _count;

            if (end == _items.Length)
            {
                MakeRoomAtEnd();
                end = _start + _count;
            }

            _items[end] = node;
            _count++;
            _version++;
        }

        internal void AddRange(NodeList nodeList)
        {
            for (var i = 0; i < nodeList._count; i++)
            {
                Add(nodeList._items[nodeList._start + i]);
            }
        }

        internal void Insert(Int32 index, Node node)
        {
            if ((UInt32)index > (UInt32)_count)
            {
                ThrowIndexOutOfRange();
            }

            if (index == _count)
            {
                Add(node);
                return;
            }

            if (index == 0 || (index < _count / 2 && _start > 0))
            {
                if (_start == 0)
                {
                    MakeRoomAtFront();
                }

                // Moves the entries before the position one slot towards the front.
                Array.Copy(_items, _start, _items, _start - 1, index);
                _start--;
            }
            else
            {
                if (_start + _count == _items.Length)
                {
                    MakeRoomAtEnd();
                }

                Array.Copy(_items, _start + index, _items, _start + index + 1, _count - index);
            }

            _items[_start + index] = node;
            _count++;
            _version++;
        }

        internal void Remove(Node node)
        {
            var index = Index(node);

            if (index >= 0)
            {
                RemoveAt(index);
            }
        }

        internal void RemoveAt(Int32 index)
        {
            if ((UInt32)index >= (UInt32)_count)
            {
                ThrowIndexOutOfRange();
            }

            if (index < _count / 2)
            {
                Array.Copy(_items, _start, _items, _start + 1, index);
                _items[_start] = null!;
                _start++;
            }
            else
            {
                var end = _start + _count - 1;
                Array.Copy(_items, _start + index + 1, _items, _start + index, _count - index - 1);
                _items[end] = null!;
            }

            _count--;
            _version++;

            if (_count == 0)
            {
                _start = 0;
            }
        }

        internal Boolean Contains(Node node) => Index(node) >= 0;

        /// <summary>
        /// Gets the index of the given node, or -1. Shadows the IEnumerable extension, which
        /// boxes an enumerator, and answers for the last entry first: removing from the end
        /// is the usual way to empty a node and would otherwise scan the whole list each time.
        /// </summary>
        internal Int32 Index(INode? node)
        {
            var last = _count - 1;

            // A null reference child is how an append asks, so it must not cost a scan.
            if (node is null)
            {
                return -1;
            }
            else if (last >= 0 && Object.ReferenceEquals(_items[_start + last], node))
            {
                return last;
            }

            for (var i = 0; i < last; i++)
            {
                if (Object.ReferenceEquals(_items[_start + i], node))
                {
                    return i;
                }
            }

            return -1;
        }

        #endregion

        #region Methods

        public void ToHtml(TextWriter writer, IMarkupFormatter formatter)
        {
            for (var i = 0; i < _count; i++)
            {
                _items[_start + i].ToHtml(writer, formatter);
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Called when the end of the array is reached: reclaims the front region once it is
        /// at least half of the array, otherwise doubles. Either way the next append is free.
        /// </summary>
        private void MakeRoomAtEnd()
        {
            if (_items.Length == 0)
            {
                _items = new Node[DefaultCapacity];
            }
            else if (_start * 2 >= _items.Length)
            {
                Array.Copy(_items, _start, _items, 0, _count);
                Array.Clear(_items, _count, _start);
                _start = 0;
            }
            else
            {
                Resize(_items.Length * 2, 0);
            }
        }

        /// <summary>
        /// Called when an entry has to go in front of the first one: re-centers the entries,
        /// doubling first unless at least half of the array is free, so that repeated inserts
        /// at the front are amortized constant time just like appends.
        /// </summary>
        private void MakeRoomAtFront()
        {
            var length = _items.Length;

            if (length == 0)
            {
                _items = new Node[DefaultCapacity];
                _start = DefaultCapacity;
            }
            else if ((length - _count) * 2 < length)
            {
                Resize(length * 2, (length * 2 - _count + 1) / 2);
            }
            else
            {
                // Only called while the entries start at zero, so [0, start) is what they leave.
                var start = (length - _count + 1) / 2;
                Array.Copy(_items, 0, _items, start, _count);
                Array.Clear(_items, 0, start);
                _start = start;
            }
        }

        private void Resize(Int32 length, Int32 start)
        {
            var items = new Node[length];
            Array.Copy(_items, _start, items, start, _count);
            _items = items;
            _start = start;
        }

        private static void ThrowIndexOutOfRange() => throw new ArgumentOutOfRangeException("index");

        #endregion

        #region IEnumerable Implementation

        public Enumerator GetEnumerator() => new(this);
        IEnumerator<INode> IEnumerable<INode>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Enumerates the entries without allocating when used by foreach on the concrete
        /// type. Like the List enumerator it replaces, it throws once the list has changed.
        /// </summary>
        public struct Enumerator : IEnumerator<Node>
        {
            private readonly NodeList _list;
            private readonly Int32 _version;
            private Int32 _index;
            private Node? _current;

            internal Enumerator(NodeList list)
            {
                _list = list;
                _version = list._version;
                _index = 0;
                _current = null;
            }

            public readonly Node Current => _current!;

            readonly Object IEnumerator.Current => _current!;

            public Boolean MoveNext()
            {
                var list = _list;

                if (_version != list._version)
                {
                    throw new InvalidOperationException("The node list was modified during the enumeration.");
                }

                if (_index < list._count)
                {
                    _current = list._items[list._start + _index];
                    _index++;
                    return true;
                }

                _current = null;
                return false;
            }

            public void Reset()
            {
                _index = 0;
                _current = null;
            }

            public readonly void Dispose()
            {
            }
        }

        #endregion

        #region Construction

        IEnumerator<IConstructableNode> IEnumerable<IConstructableNode>.GetEnumerator() => GetEnumerator();

        void IConstructableNodeList.Clear()
        {
            Array.Clear(_items, _start, _count);
            _start = 0;
            _count = 0;
            _version++;
        }

        IConstructableNode IConstructableNodeList.this[Int32 index] => this[index];

        #endregion
    }

    /// <summary>
    /// Helper interface which can remove interface dispatch overhead when used in combination with generic methods.
    /// </summary>
    internal interface INodeListAccessor
    {
        Int32 Length { get; }
        INode this[Int32 index] { get; }

        /// <summary>
        /// Gets the node at the given index if it is an element. Preferred over an
        /// "is IElement" test on the indexer, which goes through the interface cast
        /// helper on every child of every candidate.
        /// </summary>
        Boolean TryGetElement(Int32 index, out IElement element);
    }

    internal readonly struct ConcreteNodeListAccessor : INodeListAccessor
    {
        private readonly NodeList _nodeList;

        public ConcreteNodeListAccessor(NodeList nodeList)
        {
            _nodeList = nodeList;
        }

        public Int32 Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _nodeList.Length;
        }

        public INode this[Int32 index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _nodeList[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryGetElement(Int32 index, out IElement element)
        {
            var node = _nodeList[index];

            if (node.NodeType == NodeType.Element)
            {
                element = (Element)node;
                return true;
            }

            element = null!;
            return false;
        }
    }

    internal readonly struct InterfaceNodeListAccessor : INodeListAccessor
    {
        private readonly INodeList _nodeList;

        public InterfaceNodeListAccessor(INodeList nodeList)
        {
            _nodeList = nodeList;
        }

        public Int32 Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _nodeList.Length;
        }

        public INode this[Int32 index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _nodeList[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryGetElement(Int32 index, out IElement element)
        {
            if (_nodeList[index] is IElement child)
            {
                element = child;
                return true;
            }

            element = null!;
            return false;
        }
    }
}
