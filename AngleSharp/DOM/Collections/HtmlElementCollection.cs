namespace AngleSharp.DOM.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    sealed class HtmlElementCollection : IHtmlCollection
    {
        #region Fields

        readonly IEnumerable<IElement> _elements;

        #endregion

        #region ctor

        public HtmlElementCollection(INode parent, Boolean deep = true, Predicate<IElement> predicate = null)
        {
            _elements = parent.GetElements<IElement>(deep, predicate);
        }

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
            get { return _elements.Skip(index).FirstOrDefault(); }
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
