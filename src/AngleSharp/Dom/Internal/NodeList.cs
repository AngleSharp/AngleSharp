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
    sealed class NodeList : INodeList, IConstructableNodeList
    {
        #region Fields

        internal readonly List<Node> _entries;

        /// <summary>
        /// Gets an empty node-list. Shouldn't be modified.
        /// </summary>
        internal static readonly NodeList Empty = [];

        #endregion

        #region ctor

        internal NodeList()
        {
            _entries = [];
        }

        #endregion

        #region Index

        public Node this[Int32 index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _entries[index];
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _entries[index] = value;
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
            get => _entries.Count;
        }

        #endregion

        #region Internal Methods

        internal void Add(Node node) => _entries.Add(node);

        internal void AddRange(NodeList nodeList) => _entries.AddRange(nodeList._entries);

        internal void Insert(Int32 index, Node node) => _entries.Insert(index, node);

        internal void Remove(Node node) => _entries.Remove(node);

        internal void RemoveAt(Int32 index) => _entries.RemoveAt(index);

        internal Boolean Contains(Node node) => _entries.Contains(node);

        #endregion

        #region Methods

        public void ToHtml(TextWriter writer, IMarkupFormatter formatter)
        {
            for (var i = 0; i < _entries.Count; i++)
            {
                _entries[i].ToHtml(writer, formatter);
            }
        }

        #endregion

        #region IEnumerable Implementation

        public List<Node>.Enumerator GetEnumerator() => _entries.GetEnumerator();
        IEnumerator<INode> IEnumerable<INode>.GetEnumerator() => _entries.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _entries.GetEnumerator();

        #endregion

        #region Construction

        IEnumerator<IConstructableNode> IEnumerable<IConstructableNode>.GetEnumerator() => GetEnumerator();

        void IConstructableNodeList.Clear()
        {
            _entries.Clear();
        }

        IConstructableNode IConstructableNodeList.this[Int32 index] => _entries[index];

        #endregion
    }

    /// <summary>
    /// Helper interface which can remove interface dispatch overhead when used in combination with generic methods.
    /// </summary>
    internal interface INodeListAccessor
    {
        Int32 Length { get; }
        INode this[Int32 index] { get; }
    }

    internal readonly struct ConcreteNodeListAccessor : INodeListAccessor
    {
        private readonly List<Node> _nodeList;

        public ConcreteNodeListAccessor(NodeList nodeList)
        {
            _nodeList = nodeList._entries;
        }

        public Int32 Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _nodeList.Count;
        }

        public INode this[Int32 index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _nodeList[index];
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
    }
}
