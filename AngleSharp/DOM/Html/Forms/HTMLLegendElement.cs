namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML legend element.
    /// </summary>
    sealed class HTMLLegendElement : HTMLElement, IHtmlLegendElement
    {
        #region ctor

        public HTMLLegendElement(Document owner)
            : base(owner, Tags.Legend)
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
