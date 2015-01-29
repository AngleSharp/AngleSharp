namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML legend element.
    /// </summary>
    sealed class HtmlLegendElement : HtmlElement, IHtmlLegendElement
    {
        #region ctor

        public HtmlLegendElement(Document owner)
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
                var fieldset = Parent as HtmlFieldSetElement;

                if (fieldset != null)
                    return fieldset.Form;

                return null;
            }
        }

        #endregion
    }
}
