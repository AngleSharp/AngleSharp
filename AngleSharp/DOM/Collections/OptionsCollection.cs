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

        #endregion

        #region ctor

        public OptionsCollection(Node parent)
        {
            _parent = parent;
        }

        #endregion

        #region Index

        public IElement this[Int32 index]
        {
            get { return _parent.ChildNodes.OfType<IElement>().Skip(index).FirstOrDefault(); }
        }

        public IElement this[String name]
        {
            get 
            {
                foreach (var element in _parent.ChildNodes.OfType<IElement>())
                {
                    if (element.Id == name)
                        return element;

                    var fce = element as HTMLFormControlElement;

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

                foreach (var element in _parent.ChildNodes.OfType<IHtmlOptionElement>())
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

                foreach (var element in _parent.ChildNodes.OfType<IHtmlOptionElement>())
                    element.IsSelected = index++ == value;
            }
        }

        public Int32 Length
        {
            get { return _parent.ChildNodes.OfType<IHtmlOptionElement>().Count(); }
        }

        #endregion

        #region Methods

        public IHtmlOptionElement GetOptionAt(Int32 index)
        {
            return _parent.ChildNodes.OfType<IHtmlOptionElement>().Skip((Int32)index).FirstOrDefault();
        }

        public void SetOptionAt(Int32 index, IHtmlOptionElement value)
        {
            _parent.ReplaceChild(value, GetOptionAt(index));
        }

        public void Add(IHtmlOptionElement element, IHtmlElement before = null)
        {
            _parent.InsertBefore(element, before);
        }

        public void Add(IHtmlOptionsGroupElement element, IHtmlElement before = null)
        {
            _parent.InsertBefore(element, before);
        }

        public void Remove(Int32 index)
        {
            var child = GetOptionAt(index);
            _parent.RemoveChild(child);
        }

        #endregion

        #region Enumerator

        public IEnumerator<IElement> GetEnumerator()
        {
            return _parent.ChildNodes.OfType<IElement>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
