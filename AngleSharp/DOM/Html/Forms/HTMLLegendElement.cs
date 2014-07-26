namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML legend element.
    /// </summary>
    sealed class HTMLLegendElement : HTMLElement, IHtmlLegendElement
    {
        #region ctor

        internal HTMLLegendElement()
        {
            _name = Tags.Legend;
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return false; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the associated form.
        /// </summary>
        public IHtmlFormElement Form
        {
            get 
            {
                var fieldset = Parent as HTMLFieldSetElement;

                if (fieldset != null)
                    return fieldset.Form;

                return null;
            }
        }

        #endregion
    }
}
