namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML fieldset element.
    /// </summary>
    sealed class HTMLFieldSetElement : HTMLFormControlElement, IHtmlFieldSetElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML fieldset element.
        /// </summary>
        internal HTMLFieldSetElement()
            : base(Tags.Fieldset)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type of input control (fieldset).
        /// </summary>
        public String Type
        {
            get { return NodeName; }
        }

        /// <summary>
        /// Gets the elements belonging to this field set.
        /// </summary>
        public IHtmlFormControlsCollection Elements
        {
            get { return new HtmlFormControlsCollection(this); }
        }

        #endregion
    }
}
