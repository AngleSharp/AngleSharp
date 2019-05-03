namespace AngleSharp.Dom
{
    using AngleSharp.Common;
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

        private readonly IEnumerable<T> _elements;

        #endregion

        #region ctor

        public HtmlCollection(IEnumerable<T> elements)
        {
            _elements = elements;
        }

        public HtmlCollection(INode parent, Boolean deep = true, Func<T, bool> predicate = null)
        {
            _elements = parent.GetNodes(deep, predicate);
        }

        #endregion

        #region Index

        public T this[Int32 index] => _elements.GetItemByIndex(index);

        public T this[String id] => _elements.GetElementById(id);

        #endregion

        #region Properties

        public Int32 Length => _elements.Count();

        #endregion

        #region IEnumerable Implementation

        public IEnumerator<T> GetEnumerator() => _elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _elements.GetEnumerator();

        #endregion
    }
}
