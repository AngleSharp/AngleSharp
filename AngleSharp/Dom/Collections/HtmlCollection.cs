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
    /// <typeparam name="T">The type of elements that is contained.</typeparam>
    sealed class HtmlCollection<T> : IHtmlCollection<T>
        where T : class, IElement
    {
        #region Fields

        readonly IEnumerable<T> _elements;

        #endregion

        #region ctor

        public HtmlCollection(IEnumerable<T> elements)
        {
            _elements = elements;
        }

        public HtmlCollection(INode parent, Boolean deep = true, Predicate<T> predicate = null)
        {
            _elements = parent.GetElements(deep, predicate);
        }

        #endregion

        #region Index

        public T this[Int32 index]
        {
            get { return _elements.GetItemByIndex(index); }
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
