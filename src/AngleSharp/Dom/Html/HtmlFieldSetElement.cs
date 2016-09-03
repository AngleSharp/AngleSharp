namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML fieldset element.
    /// </summary>
    sealed class HtmlFieldSetElement : HtmlFormControlElement, IHtmlFieldSetElement
    {
        #region Fields

        private HtmlFormControlsCollection _elements;

        #endregion

        #region ctor

        public HtmlFieldSetElement(Document owner, String prefix = null)
            : base(owner, TagNames.Fieldset, prefix)
        {
        }

        #endregion

        #region Properties

        public String Type
        {
            get { return TagNames.Fieldset; }
        }

        public IHtmlFormControlsCollection Elements
        {
            get { return _elements ?? (_elements = new HtmlFormControlsCollection(Form, this)); }
        }

        #endregion

        #region Methods

        protected override Boolean IsFieldsetDisabled()
        {
            return false;
        }

        protected override Boolean CanBeValidated()
        {
            return true;
        }

        #endregion
    }
}
