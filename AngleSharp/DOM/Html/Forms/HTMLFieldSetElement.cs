using AngleSharp.DOM.Collections;
using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML fieldset element.
    /// </summary>
    [DOM("HTMLFieldSetElement")]
    public sealed class HTMLFieldSetElement : HTMLFormControlElement
    {
        #region Members

        HTMLFormControlsCollection _elements;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML fieldset element.
        /// </summary>
        internal HTMLFieldSetElement()
        {
            _name = Tags.Fieldset;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type of input control (fieldset).
        /// </summary>
        [DOM("type")]
        public String Type
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets the elements belonging to this field set.
        /// </summary>
        [DOM("elements")]
        public HTMLFormControlsCollection Elements
        {
            get { return _elements ?? (_elements = new HTMLFormControlsCollection(this)); }
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}
