using System;
using System.Collections.Generic;

namespace AngleSharp.DOM.Collections
{
    sealed class HTMLStaticCollection : HTMLCollection
    {
        List<Element> _elements;

        public HTMLStaticCollection()
            : this(new List<Element>())
        {
        }

        public HTMLStaticCollection(List<Element> elements)
        {
            _elements = elements;
        }

        public List<Element> List
        {
            get { return _elements; }
        }

        protected override Element GetItem(Int32 index)
        {
            return _elements[index];
        }

        protected override Int32 GetLength()
        {
            return _elements.Count;
        }

        public override IEnumerator<Element> GetEnumerator()
        {
            foreach (var element in _elements)
                yield return element;
        }

        internal override Int32 IndexOf(Element element)
        {
            return _elements.IndexOf(element);
        }
    }
}
