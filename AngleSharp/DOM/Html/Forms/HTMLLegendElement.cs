namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML legend element.
    /// </summary>
    sealed class HTMLLegendElement : HTMLElement, IHtmlLegendElement
    {
        #region ctor

        internal HTMLLegendElement()
            : base(Tags.Legend)
        {
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
