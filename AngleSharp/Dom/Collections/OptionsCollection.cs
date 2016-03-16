namespace AngleSharp.Dom.Collections
{
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
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
            _options = GetOptions();
        }

        #endregion

        #region Index

        public IHtmlOptionElement this[Int32 index]
        {
            get { return GetOptionAt(index); }
        }

        public IHtmlOptionElement this[String name]
        {
            get 
            {
                if (!String.IsNullOrEmpty(name))
                {
                    foreach (var option in _options)
                    {
                        if (option.Id.Is(name))
                        {
                            return option;
                        }
                    }

                    return _parent.Children[name] as IHtmlOptionElement;
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

                foreach (var option in _options)
                {
                    if (option.IsSelected)
                    {
                        return index;
                    }

                    index++;
                }

                return -1;
            }
            set
            {
                var index = 0;

                foreach (var option in _options)
                {
                    option.IsSelected = index++ == value;
                }
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
            return _options.GetItemByIndex(index);
        }

        public void SetOptionAt(Int32 index, IHtmlOptionElement value)
        {
            var child = GetOptionAt(index);

            if (child != null)
            {
                _parent.ReplaceChild(value, child);
            }
            else
            {
                _parent.AppendChild(value);
            }
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
            if (index >= 0 && index < Length)
            {
                var child = GetOptionAt(index);
                _parent.RemoveChild(child);
            }
        }

        #endregion

        #region Enumerator

        IEnumerable<IHtmlOptionElement> GetOptions()
        {
            foreach (var child in _parent.ChildNodes)
            {
                var optgroup = child as IHtmlOptionsGroupElement;

                if (optgroup != null)
                {
                    foreach (var element in optgroup.ChildNodes)
                    {
                        var option = element as IHtmlOptionElement;

                        if (option != null)
                        {
                            yield return option;
                        }
                    }
                }
                else if (child is IHtmlOptionElement)
                {
                    yield return (IHtmlOptionElement)child;
                }
            }
        }

        public IEnumerator<IHtmlOptionElement> GetEnumerator()
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
