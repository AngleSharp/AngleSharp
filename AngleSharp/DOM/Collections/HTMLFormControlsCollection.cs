namespace AngleSharp.DOM.Collections
{
    using AngleSharp.DOM.Html;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A collection of HTML form controls.
    /// </summary>
    [DOM("HTMLFormControlsCollection")]
    public sealed class HTMLFormControlsCollection : HTMLCollection
    {
        #region Fields

        HTMLLiveCollection<HTMLFormControlElement> _elements;

        #endregion

        #region ctor

        internal HTMLFormControlsCollection(HTMLLiveCollection<HTMLFormControlElement> elements)
        {
            _elements = elements;
        }

        internal HTMLFormControlsCollection(Element parent)
        {
            _elements = new HTMLLiveCollection<HTMLFormControlElement>(parent);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the enumerator of the container.
        /// </summary>
        /// <returns>The enumerator of the container.</returns>
        public override IEnumerator<Element> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index">The 0-based index.</param>
        /// <returns>The element or null.</returns>
        protected override Element GetItem(Int32 index)
        {
            return _elements[index];
        }

        /// <summary>
        /// Gets the length of the elements.
        /// </summary>
        /// <returns>The number of elements in the container.</returns>
        protected override int GetLength()
        {
            return _elements.Length;
        }

        /// <summary>
        /// Gets the item with the specified name.
        /// </summary>
        /// <param name="name">The name of the element (id or name).</param>
        /// <returns>The list of items, single item or null.</returns>
        protected override Object GetItem(String name)
        {
            var result = new List<Element>();

            for (int i = 0; i < _elements.Length; i++)
            {
                if (_elements[i].Id == name || _elements[i].GetAttribute("name") == name)
                    result.Add(_elements[i]);
            }

            if (result.Count == 0)
                return null;
            else if (result.Count == 1)
                return result[0];

            return new HTMLStaticCollection(result);
        }

        internal override Int32 IndexOf(Element element)
        {
            return _elements.IndexOf(element);
        }

        #endregion
    }
}
