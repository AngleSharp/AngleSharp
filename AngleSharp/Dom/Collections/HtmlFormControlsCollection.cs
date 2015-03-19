namespace AngleSharp.Dom.Collections
{
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
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

        readonly IEnumerable<HtmlFormControlElement> _elements;

        #endregion

        #region ctor

        public HtmlFormControlsCollection(IElement form, IElement root = null)
        {
            if (root == null)
                root = form.Owner.DocumentElement;

            _elements = root.GetElements<HtmlFormControlElement>().Where(m =>
            {
                if (m.Form == form)
                {
                    var input = m as IHtmlInputElement;

                    if (input == null || input.Type != InputTypeNames.Image)
                        return true;
                }

                return false;
            });
        }

        #endregion

        #region Properties

        public Int32 Length
        {
            get { return _elements.Count(); }
        }

        #endregion

        #region HTMLFormControlElement Implementation

        public HtmlFormControlElement this[Int32 index]
        {
            get { return index >= 0 ? _elements.Skip(index).FirstOrDefault() : null; }
        }

        public HtmlFormControlElement this[String id]
        {
            get { return _elements.GetElementById(id); }
        }

        public IEnumerator<HtmlFormControlElement> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        #endregion

        #region IHtmlCollection Implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        IHtmlElement IHtmlCollection<IHtmlElement>.this[Int32 index]
        {
            get { return this[index]; }
        }

        IHtmlElement IHtmlCollection<IHtmlElement>.this[String id]
        {
            get { return this[id]; }
        }

        IEnumerator<IHtmlElement> IEnumerable<IHtmlElement>.GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        #endregion
    }
}
