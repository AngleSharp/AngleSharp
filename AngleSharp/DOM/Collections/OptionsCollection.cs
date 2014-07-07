namespace AngleSharp.DOM.Collections
{
    using AngleSharp.DOM.Html;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    sealed class OptionsCollection : IHtmlOptionsCollection
    {
        #region Fields

        readonly Node _parent;
        readonly List<IHtmlElement> _elements;

        #endregion

        #region ctor

        public OptionsCollection(Node parent)
        {
            _parent = parent;
            _elements = new List<IHtmlElement>();
        }

        #endregion

        #region Index

        public IHtmlOptionElement this[UInt32 index]
        {
            get { return _elements[(int)index] as IHtmlOptionElement; }
            set { _elements[(int)index] = value; }
        }

        public Element this[Int32 index]
        {
            get { return _elements[index] as Element; }
        }

        public Element this[String name]
        {
            get 
            {
                for (var i = 0; i < _elements.Count; i++)
                {
                    if (_elements[i].Id == name)
                        return _elements[i] as Element;

                    var fce = _elements[i] as HTMLFormControlElement;

                    if (fce != null && fce.Name == name)
                        return fce;
                }

                return null;
            }
        }

        #endregion

        #region Properties

        public Int32 SelectedIndex
        {
            get
            {
                var index = 0;
                
                foreach (var element in _elements.OfType<IHtmlOptionElement>())
                {
                    if (element.IsSelected)
                        return index;

                    index++;
                }

                return -1;
            }
            set
            {
                var index = 0;

                foreach (var element in _elements.OfType<IHtmlOptionElement>())
                    element.IsSelected = index++ == value;
            }
        }

        public Int32 Length
        {
            get { return _elements.Count; }
        }

        #endregion

        #region Methods

        public void Add(IHtmlOptionElement element, IHtmlElement before = null)
        {
            var index = _elements.IndexOf(before);

            if (index >= 0)
                _elements.Insert(index, element);
            else
                _elements.Add(element);
        }

        public void Add(IHtmlOptionsGroupElement element, IHtmlElement before = null)
        {
            var index = _elements.IndexOf(before);

            if (index >= 0)
                _elements.Insert(index, element);
            else
                _elements.Add(element);
        }

        public void Remove(Int32 index)
        {
            _elements.RemoveAt(index);
        }

        #endregion

        #region Enumerator

        public IEnumerator<Element> GetEnumerator()
        {
            return _elements.OfType<Element>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
