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

        HtmlFormControlsCollection _elements;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML fieldset element.
        /// </summary>
        public HtmlFieldSetElement(Document owner, String prefix = null)
            : base(owner, Tags.Fieldset, prefix)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type of input control (fieldset).
        /// </summary>
        public String Type
        {
            get { return Tags.Fieldset; }
        }

        /// <summary>
        /// Gets the elements belonging to this field set.
        /// </summary>
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
