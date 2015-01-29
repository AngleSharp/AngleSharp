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

        readonly IEnumerable<HTMLFormControlElement> _elements;

        #endregion

        #region ctor

        public HtmlFormControlsCollection(IElement form, IElement root = null)
        {
            if (root == null)
                root = form.Owner.DocumentElement;

            _elements = root.GetElements<HTMLFormControlElement>().Where(m =>
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

        public HTMLFormControlElement this[Int32 index]
        {
            get { return _elements.Skip(index).FirstOrDefault(); }
        }

        public HTMLFormControlElement this[String id]
        {
            get { return _elements.GetElementById(id); }
        }

        public IEnumerator<HTMLFormControlElement> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        #endregion

        #region IHtmlCollection Implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        IElement IHtmlCollection.this[int index]
        {
            get { return _elements.Skip(index).FirstOrDefault(); }
        }

        IElement IHtmlCollection.this[string id]
        {
            get { return _elements.GetElementById(id); }
        }

        IEnumerator<IElement> IEnumerable<IElement>.GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        #endregion
    }
}
