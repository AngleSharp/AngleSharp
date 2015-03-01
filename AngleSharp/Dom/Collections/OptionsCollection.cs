namespace AngleSharp.Dom.Collections
{
    using AngleSharp.Dom.Html;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A collection specialized on IHtmlOptionElement elements.
    /// </summary>
    sealed class OptionsCollection : IHtmlOptionsCollection
    {
        #region Fields

        readonly IElement _parent;
        readonly IEnumerable<IHtmlOptionElement> _options;

        #endregion

        #region ctor

        public OptionsCollection(IElement parent)
        {
            _parent = parent;
            _options = _parent.ChildNodes.OfType<IHtmlOptionElement>();
        }

        #endregion

        #region Index

        public IElement this[Int32 index]
        {
            get { return GetOptionAt(index); }
        }

        public IElement this[String name]
        {
            get 
            {
                foreach (var option in _options)
                {
                    if (option.Id == name)
                        return option;
                }

                return _parent.Children[name];
            }
        }

        #endregion

        #region Properties

        public Int32 SelectedIndex
        {
            get
            {
                var index = 0;

                foreach (var option in _options)
                {
                    if (option.IsSelected)
                        return index;

                    index++;
                }

                return -1;
            }
            set
            {
                var index = 0;

                foreach (var option in _options)
                    option.IsSelected = index++ == value;
            }
        }

        public Int32 Length
        {
            get { return _options.Count(); }
        }

        #endregion

        #region Methods

        public IHtmlOptionElement GetOptionAt(Int32 index)
        {
            return _options.Skip(index).FirstOrDefault();
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
            return _options.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
