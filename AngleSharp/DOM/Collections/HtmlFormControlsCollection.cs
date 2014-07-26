namespace AngleSharp.DOM.Collections
{
    using AngleSharp.DOM.Html;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    sealed class HtmlFormControlsCollection : IHtmlFormControlsCollection
    {
        #region Fields

        readonly IEnumerable<HTMLFormControlElement> _elements;

        #endregion

        #region ctor

        public HtmlFormControlsCollection(IElement parent)
        {
            _elements = parent.GetElements<HTMLFormControlElement>();
        }

        #endregion

        #region Properties

        public Int32 Length
        {
            get { return _elements.Count(); }
        }

        #endregion

        #region HTMLFormControlElement Implementation

        public HTMLFormControlElement this[Int32 index]
        {
            get { return _elements.Skip(index).FirstOrDefault(); }
        }

        public HTMLFormControlElement this[String id]
        {
            get { return _elements.GetElementById(id); }
        }

        public IEnumerator<HTMLFormControlElement> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        #endregion

        #region IHtmlCollection Implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        IElement IHtmlCollection.this[int index]
        {
            get { return _elements.Skip(index).FirstOrDefault(); }
        }

        IElement IHtmlCollection.this[string id]
        {
            get { return _elements.GetElementById(id); }
        }

        IEnumerator<IElement> IEnumerable<IElement>.GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        #endregion
    }
}
