namespace AngleSharp.Dom.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    sealed class ElementMap : IElementMap
    {
        readonly Dictionary<String, IElement> _elements;

        public ElementMap()
        {
            _elements = new Dictionary<String, IElement>();
        }

        public IElement this[String name]
        {
            get { return _elements[name]; }
            set { _elements[name] = value; }
        }

        public void Remove(String name)
        {
            _elements.Remove(name);
        }

        public IEnumerator<KeyValuePair<String, IElement>> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
