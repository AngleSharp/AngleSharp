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

        public HtmlAllCollection(IDocument document)
        {
            _elements = document.GetElements<IElement>();
        }

        #endregion

        #region Index

        public IElement this[Int32 index]
        {
            get { return _elements.GetItemByIndex(index); }
        }

        public IElement this[String id]
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
