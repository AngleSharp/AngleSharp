namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
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

        public String Type => TagNames.Fieldset;

        public IHtmlFormControlsCollection Elements => _elements ?? (_elements = new HtmlFormControlsCollection(Form, this));

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
