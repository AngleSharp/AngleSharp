namespace AngleSharp.Dom.Collections
{
    using AngleSharp.Extensions;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A specialized collection containing elements of type T.
    /// </summary>
    /// <typeparam name="T">The type of elements that can be contained.</typeparam>
    sealed class HtmlCollection<T> : IHtmlCollection<T>
        where T : class, IElement
    {
        #region Fields

        readonly IEnumerable<T> _elements;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new list of elements.
        /// </summary>
        /// <param name="elements">The elements to use.</param>
        public HtmlCollection(IEnumerable<T> elements)
        {
            _elements = elements;
        }

        /// <summary>
        /// Creates a new live collection for the given parent.
        /// </summary>
        /// <param name="parent">The parent of this collection.</param>
        /// <param name="deep">[Optional] Determines if recursive search is activated.</param>
        /// <param name="predicate">[Optional] The predicate function for picking elements.</param>
        public HtmlCollection(INode parent, Boolean deep = true, Predicate<T> predicate = null)
        {
            _elements = parent.GetElements(deep, predicate);
        }

        #endregion

        #region Index

        public T this[Int32 index]
        {
            get { return index >= 0 ? _elements.Skip(index).FirstOrDefault() : null; }
        }

        public T this[String id]
        {
            get { return _elements.GetElementById(id); }
        }

        #endregion

        #region Properties

        public Int32 Length
        {
            get { return _elements.Count(); }
        }

        #endregion

        #region IEnumerable Implementation

        public IEnumerator<T> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        #endregion
    }
}
