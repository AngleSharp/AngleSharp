namespace AngleSharp.DOM.Collections
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An HTML live collection for a specific type.
    /// </summary>
    /// <typeparam name="T">The type of elements to contain.</typeparam>
    sealed class HTMLLiveCollection<T> : HTMLCollection
        where T : Element
    {
        #region Fields

        Node _parent;
        Boolean _deep;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new live collection for the given parent.
        /// </summary>
        /// <param name="parent">The parent of this collection.</param>
        /// <param name="deep">[Optional] Determines if recursive search is activated.</param>
        public HTMLLiveCollection(Node parent, Boolean deep = true)
        {
            _deep = deep;
            _parent = parent;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if deep search is active.
        /// </summary>
        public Boolean IsDeep
        {
            get { return _deep; }
            set { _deep = value; }
        }

        /// <summary>
        /// Gets the enumerator over all contained elements.
        /// </summary>
        public IEnumerable<T> Elements
        {
            get { return GetElements(); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the specific element of type T at the specified index.
        /// </summary>
        /// <param name="index">The index of the element to retrieve.</param>
        /// <returns>The element of type T or null.</returns>
        public T GetElementAt(Int32 index)
        {
            var elements = GetElements();
            var i = 0;

            foreach(var element in elements)
            {
                if (i == index)
                    return element;

                i++;
            }

            return null;
        }

        /// <summary>
        /// Gets the enumerator over all elements.
        /// </summary>
        /// <returns>The enumerator over Element elements.</returns>
        public override IEnumerator<Element> GetEnumerator()
        {
            return GetElements().GetEnumerator();
        }

        #endregion

        #region Helpers

        protected override Element GetItem(Int32 index)
        {
            return GetElementAt(index);
        }

        protected override Int32 GetLength()
        {
            var elements = GetElements();
            var count = 0;

            foreach(var element in elements)
                count++;

            return count;
        }

        internal override Int32 IndexOf(Element child)
        {
            var elements = GetElements();
            var i = 0;

            foreach (var element in elements)
            {
                if (element == child)
                    return i;

                i++;
            }

            return -1;
        }

        IEnumerable<T> GetElements()
        {
            if (_deep)
                return GetElementsOf(_parent);
            else
                return GetOnlyElementsOf(_parent);
        }

        static IEnumerable<T> GetElementsOf(Node parent)
        {
            for (int i = 0; i < parent.ChildNodes.Length; i++)
            {
                if (parent.ChildNodes[i] is T)
                    yield return (T)parent.ChildNodes[i];

                foreach (var element in GetElementsOf(parent.ChildNodes[i]))
                    yield return element;
            }
        }

        static IEnumerable<T> GetOnlyElementsOf(Node parent)
        {
            for (int i = 0; i < parent.ChildNodes.Length; i++)
            {
                if (parent.ChildNodes[i] is T)
                    yield return (T)parent.ChildNodes[i];
            }
        }

        #endregion
    }

    /// <summary>
    /// A general HTML live collection for no specific type.
    /// </summary>
    abstract class HTMLLiveCollection : HTMLCollection
    {
        #region Fields

        Node _parent;
        Boolean _deep;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new live collection for the given parent.
        /// </summary>
        /// <param name="parent">The parent of this collection.</param>
        /// <param name="deep">[Optional] Determines if recursive search is activated.</param>
        public HTMLLiveCollection(Node parent, Boolean deep = true)
        {
            _deep = deep;
            _parent = parent;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the set parent node.
        /// </summary>
        public Node Parent
        {
            get { return _parent; }
        }

        /// <summary>
        /// Gets or sets if deep search is active.
        /// </summary>
        public Boolean IsDeep
        {
            get { return _deep; }
            set { _deep = value; }
        }

        /// <summary>
        /// Gets the enumerator over all elements.
        /// </summary>
        public IEnumerable<Element> Elements
        {
            get { return GetElements(); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="index">The index of the element to retrieve.</param>
        /// <returns>The element or null.</returns>
        public Element GetElementAt(Int32 index)
        {
            var elements = GetElements();
            var i = 0;

            foreach(var element in elements)
            {
                if (i == index)
                    return element;

                i++;
            }

            return null;
        }

        /// <summary>
        /// Gets the enumerator over all elements.
        /// </summary>
        /// <returns>The enumerator over Element elements.</returns>
        public override IEnumerator<Element> GetEnumerator()
        {
            return GetElements().GetEnumerator();
        }

        #endregion

        #region Helpers

        protected override Element GetItem(Int32 index)
        {
            return GetElementAt(index);
        }

        protected override Int32 GetLength()
        {
            var elements = GetElements();
            var count = 0;

            foreach(var element in elements)
                count++;

            return count;
        }

        internal override Int32 IndexOf(Element child)
        {
            var elements = GetElements();
            var i = 0;

            foreach (var element in elements)
            {
                if (child == element)
                    return i;

                i++;
            }

            return -1;
        }

        protected abstract IEnumerable<Element> GetElements();

        #endregion
    }

    /// <summary>
    /// An HTML live collection for three specific types.
    /// </summary>
    /// <typeparam name="T1">The first type to contain.</typeparam>
    /// <typeparam name="T2">Another type of elements to contain.</typeparam>
    /// <typeparam name="T3">The last type of elements to contain.</typeparam>
    sealed class HTMLLiveCollection<T1, T2, T3> : HTMLLiveCollection
        where T1 : Element
        where T2 : Element
        where T3 : Element
    {
        #region ctor

        public HTMLLiveCollection(Node parent, Boolean deep = true)
            : base(parent, deep)
        {
        }

        #endregion

        #region Methods

        protected override IEnumerable<Element> GetElements()
        {
            if (IsDeep)
                return GetElementsOf(Parent);
            else
                return GetOnlyElementsOf(Parent);
        }

        static IEnumerable<Element> GetElementsOf(Node parent)
        {
            for (int i = 0; i < parent.ChildNodes.Length; i++)
            {
                var child = parent.ChildNodes[i];

                if (child is T1 || child is T2 || child is T3)
                    yield return (Element)child;

                foreach(var element in GetElementsOf(child))
                    yield return element;
            }
        }

        static IEnumerable<Element> GetOnlyElementsOf(Node parent)
        {
            for (int i = 0; i < parent.ChildNodes.Length; i++)
            {
                var child = parent.ChildNodes[i];

                if (child is T1 || child is T2 || child is T3)
                    yield return (Element)child;
            }
        }

        #endregion
    }
}
