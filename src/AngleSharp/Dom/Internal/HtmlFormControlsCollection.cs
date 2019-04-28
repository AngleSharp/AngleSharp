namespace AngleSharp.Dom
{
    using AngleSharp.Common;
    using AngleSharp.Html;
    using AngleSharp.Html.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A specialized collection containing elements of type HTMLFormControlElement.
    /// </summary>
    sealed class HtmlFormControlsCollection : IHtmlFormControlsCollection
    {
        #region Fields

        private readonly IEnumerable<HtmlFormControlElement> _elements;

        #endregion

        #region ctor

        public HtmlFormControlsCollection(IElement form, IElement root = null)
        {
            if (root == null)
            {
                root = form.Owner.DocumentElement;
            }

            _elements = root.GetNodes<HtmlFormControlElement>().Where(m =>
            {
                if (Object.ReferenceEquals(m.Form, form))
                {

                    if (!(m is IHtmlInputElement input) || !input.Type.Is(InputTypeNames.Image))
                    {
                        return true;
                    }
                }

                return false;
            });
        }

        #endregion

        #region Properties

        public Int32 Length => _elements.Count();

        #endregion

        #region HtmlFormControlElement Implementation

        public HtmlFormControlElement this[Int32 index] => _elements.GetItemByIndex(index);

        public HtmlFormControlElement this[String id] => _elements.GetElementById(id);

        public IEnumerator<HtmlFormControlElement> GetEnumerator() => _elements.GetEnumerator();

        #endregion

        #region IHtmlCollection Implementation

        IEnumerator IEnumerable.GetEnumerator() => _elements.GetEnumerator();

        IHtmlElement IHtmlCollection<IHtmlElement>.this[Int32 index] => this[index];

        IHtmlElement IHtmlCollection<IHtmlElement>.this[String id] => this[id];

        IEnumerator<IHtmlElement> IEnumerable<IHtmlElement>.GetEnumerator() => _elements.GetEnumerator();

        #endregion
    }
}
