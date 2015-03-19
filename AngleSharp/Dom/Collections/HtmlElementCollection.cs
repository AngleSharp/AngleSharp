namespace AngleSharp.Dom.Collections
{
    using AngleSharp.Extensions;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A general collection containing elements of type IElement.
    /// </summary>
    sealed class HtmlElementCollection : IHtmlCollection<IElement>
    {
        #region Fields

        readonly IEnumerable<IElement> _elements;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new live collection for the given parent.
        /// </summary>
        /// <param name="parent">The parent of this collection.</param>
        /// <param name="deep">[Optional] Determines if recursive search is activated.</param>
        /// <param name="predicate">[Optional] The predicate function for picking elements.</param>
        public HtmlElementCollection(INode parent, Boolean deep = true, Predicate<IElement> predicate = null)
        {
            _elements = parent.GetElements<IElement>(deep, predicate);
        }

        /// <summary>
        /// Creates a new list of elements.
        /// </summary>
        /// <param name="elements">The elements to use.</param>
        public HtmlElementCollection(IEnumerable<IElement> elements)
        {
            _elements = elements;
        }

        #endregion

        #region Properties

        public Int32 Length
        {
            get { return _elements.Count(); }
        }

        #endregion

        #region Index

        public IElement this[Int32 index]
        {
            get { return index >= 0 ? _elements.Skip(index).FirstOrDefault() : null; }
        }

        public IElement this[String id]
        {
            get { return _elements.GetElementById(id); }
        }

        #endregion

        #region Methods

        public IEnumerator<IElement> GetEnumerator()
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
