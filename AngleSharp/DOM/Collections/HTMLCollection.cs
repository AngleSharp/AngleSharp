namespace AngleSharp.DOM.Collections
{
    using AngleSharp.DOM.Html;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A specialized collection containing elements of type T.
    /// </summary>
    /// <typeparam name="T">The type of elements that can be contained.</typeparam>
    class HTMLCollection<T> : IHtmlCollection
        where T : class, IElement
    {
        #region Fields

        readonly IEnumerable<T> _elements;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new list of HTML elements.
        /// </summary>
        /// <param name="elements">The elements to use.</param>
        internal HTMLCollection(IEnumerable<T> elements)
        {
            _elements = elements;
        }

        /// <summary>
        /// Creates a new live collection for the given parent.
        /// </summary>
        /// <param name="parent">The parent of this collection.</param>
        /// <param name="deep">[Optional] Determines if recursive search is activated.</param>
        /// <param name="predicate">[Optional] The predicate function for picking elements.</param>
        internal HTMLCollection(INode parent, Boolean deep = true, Predicate<T> predicate = null)
            : this(parent.GetElements(deep, predicate))
        {
        }

        #endregion

        #region Index

        /// <summary>
        /// Gets the specific node whose id matches the string specified by the attribute.
        /// Matching by name is only done as a last resort, only in HTML, and only if the referenced
        /// element supports the name attribute. Returns null if no node exists by the given name.
        /// </summary>
        /// <param name="index">The 0-based index of the element.</param>
        /// <returns>The element at the specified index.</returns>
        public T this[Int32 index]
        {
            get { return _elements.Skip(index).FirstOrDefault(); }
        }

        /// <summary>
        /// Gets the specific node whose id matches the string specified by the attribute.
        /// Matching by name is only done as a last resort, only in HTML, and only if the referenced
        /// element supports the name attribute. Returns null if no node exists with the given name.
        /// </summary>
        /// <param name="id">The id of the element.</param>
        /// <returns>The element with the specified identifier.</returns>
        public T this[String id]
        {
            get { return _elements.GetElementById(id); }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of nodes in the list.
        /// </summary>
        public Int32 Length
        {
            get { return _elements.Count(); }
        }

        #endregion

        #region Methods

        internal Int32 IndexOf(T item)
        {
            var index = 0;

            foreach (var element in _elements)
            {
                if (element == item)
                    return index;

                index++;
            }

            return -1;
        }

        #endregion

        #region IEnumerable Implementation

        /// <summary>
        /// Gets an enumerator over the contained elements.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<IElement> IEnumerable<IElement>.GetEnumerator()
        {
            return _elements.OfType<IElement>().GetEnumerator();
        }

        #endregion

        #region IHtmlCollection

        IElement IHtmlCollection.this[Int32 index]
        {
            get { return this[index] as IElement; }
        }

        IElement IHtmlCollection.this[String name]
        {
            get { return this[name] as IElement; }
        }

        #endregion
    }

    /// <summary>
    /// A collection of HTML nodes.
    /// </summary>
    sealed class HTMLCollection : HTMLCollection<IElement>
    {
        #region ctor

        /// <summary>
        /// Creates a new list of HTML elements.
        /// </summary>
        /// <param name="elements">The elements to use.</param>
        internal HTMLCollection(IEnumerable<IElement> elements)
            : base(elements)
        {
        }

        /// <summary>
        /// Creates a new live collection for the given parent.
        /// </summary>
        /// <param name="parent">The parent of this collection.</param>
        /// <param name="deep">[Optional] Determines if recursive search is activated.</param>
        /// <param name="predicate">[Optional] The predicate function for picking elements.</param>
        internal HTMLCollection(INode parent, Boolean deep = true, Predicate<IElement> predicate = null)
            : base(parent, deep, predicate)
        {
        }

        #endregion
    }

    /// <summary>
    /// A collection of HTML form controls.
    /// </summary>
    sealed class HTMLFormControlsCollection : HTMLCollection<HTMLFormControlElement>, IHtmlFormControlsCollection
    {
        #region ctor

        internal HTMLFormControlsCollection(IEnumerable<HTMLFormControlElement> elements)
            : base(elements)
        {
        }

        internal HTMLFormControlsCollection(Element parent)
            : base(parent)
        {
        }

        #endregion

        #region IHtmlFormControlsCollection

        Int32 IHtmlCollection.Length
        {
            get { return Length; }
        }

        IElement IHtmlCollection.this[Int32 index]
        {
            get { return this[index]; }
        }

        IElement IHtmlCollection.this[String name]
        {
            get { return this[name]; }
        }

        IEnumerator<IElement> IEnumerable<IElement>.GetEnumerator()
        {
            var enumerator = base.GetEnumerator();

            while (enumerator.MoveNext())
                yield return enumerator.Current;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
