namespace AngleSharp.DOM.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    sealed class HtmlAllCollection : IHtmlAllCollection
    {
        readonly IEnumerable<IElement> _elements;

        public HtmlAllCollection(IDocument document)
        {
            _elements = document.GetElements<IElement>();
        }

        public Int32 Length
        {
            get { return _elements.Count(); }
        }

        public IElement this[Int32 index]
        {
            get { return _elements.Skip(index).FirstOrDefault(); }
        }

        public IElement this[String id]
        {
            get { return _elements.GetElementById(id); }
        }

        public IEnumerator<IElement> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _elements.GetEnumerator();
        }
    }
}
