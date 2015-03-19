namespace AngleSharp.Dom.Collections
{
    using AngleSharp.Extensions;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A general collection for all elements of type IElement.
    /// </summary>
    sealed class HtmlAllCollection : IHtmlAllCollection
    {
        #region Fields

        readonly IEnumerable<IElement> _elements;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new live collection for the given document.
        /// </summary>
        /// <param name="document">The parent of this collection.</param>
        public HtmlAllCollection(IDocument document)
        {
            _elements = document.GetElements<IElement>();
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
