namespace AngleSharp.Dom
{
    using AngleSharp.Common;
    using AngleSharp.Html.Dom;
    using AngleSharp.Text;
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

        private readonly IElement _parent;
        private readonly IEnumerable<IHtmlOptionElement> _options;

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

        public IHtmlOptionElement? this[String name]
        {
            get 
            {
                if (name is { Length: > 0 })
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
                IHtmlOptionElement? selected = null;

                foreach (var option in _options)
                {
                    ((HtmlOptionElement)option).SetSelectedness(false);

                    if (index++ == value)
                    {
                        selected = option;
                    }
                }

                if (selected is not null)
                {
                    selected.IsSelected = true;
                }
            }
        }

        public Int32 Length => _options.Count();

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
                child.Parent!.ReplaceChild(value, child);
            }
            else
            {
                _parent.AppendChild(value);
            }
        }

        public void Add(IHtmlOptionElement element, IHtmlElement? before = null)
        {
            GetInsertionParent(before).InsertBefore(element, before);
        }

        public void Add(IHtmlOptionsGroupElement element, IHtmlElement? before = null)
        {
            GetInsertionParent(before).InsertBefore(element, before);
        }

        /// <summary>
        /// An option reference can belong to an optgroup rather than the collection's select.
        /// Both add overloads insert into that reference's actual parent.
        /// </summary>
        private IElement GetInsertionParent(IHtmlElement? before)
        {
            if (before is IHtmlOptionElement && before.Parent is HtmlOptionsGroupElement group && group.Parent == _parent)
            {
                return group;
            }

            return _parent;
        }

        public void Remove(Int32 index)
        {
            if (index >= 0 && index < Length)
            {
                var child = GetOptionAt(index);
                child.Parent!.RemoveChild(child);
            }
        }

        #endregion

        #region Enumerator

        private IEnumerable<IHtmlOptionElement> GetOptions()
        {
            foreach (var child in _parent.ChildNodes)
            {

                if (child is IHtmlOptionsGroupElement optgroup)
                {
                    foreach (var element in optgroup.ChildNodes)
                    {

                        if (element is IHtmlOptionElement option)
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
