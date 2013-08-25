using AngleSharp.DOM.Html;
using System;
using System.Collections.Generic;

namespace AngleSharp.DOM.Collections
{
    /// <summary>
    /// A collection of HTML form controls.
    /// </summary>
    [DOM("HTMLFormControlsCollection")]
    public sealed class HTMLFormControlsCollection : HTMLCollection
    {
        HTMLLiveCollection<HTMLFormControlElement> _elements;

        internal HTMLFormControlsCollection(HTMLLiveCollection<HTMLFormControlElement> elements)
        {
            _elements = elements;
        }

        internal HTMLFormControlsCollection(Element parent)
        {
            _elements = new HTMLLiveCollection<HTMLFormControlElement>(parent);
        }

        protected override Element GetItem(Int32 index)
        {
            return _elements[index];
        }

        protected override int GetLength()
        {
            return _elements.Length;
        }

        public override IEnumerator<Element> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

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
    }
}
