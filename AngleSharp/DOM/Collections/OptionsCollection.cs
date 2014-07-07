namespace AngleSharp.DOM.Collections
{
    using AngleSharp.DOM.Html;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    sealed class OptionsCollection : IHtmlOptionsCollection
    {
        Node _parent;

        public OptionsCollection(Node parent)
        {
            _parent = parent;
        }

        public IHtmlOptionElement this[UInt32 index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(IHtmlOptionElement element, IHtmlElement before = null)
        {
            throw new NotImplementedException();
        }

        public void Add(IHtmlOptionsGroupElement element, IHtmlElement before = null)
        {
            throw new NotImplementedException();
        }

        public void Remove(Int32 index)
        {
            throw new NotImplementedException();
        }

        public Int32 SelectedIndex
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Int32 Length
        {
            get { throw new NotImplementedException(); }
        }

        public Element this[Int32 index]
        {
            get { throw new NotImplementedException(); }
        }

        public Element this[String name]
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerator<Element> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
