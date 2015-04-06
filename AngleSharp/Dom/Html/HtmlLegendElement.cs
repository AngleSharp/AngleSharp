namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML legend element.
    /// </summary>
    sealed class HtmlLegendElement : HtmlElement, IHtmlLegendElement
    {
        #region ctor

        public HtmlLegendElement(Document owner, String prefix = null)
            : base(owner, Tags.Legend, prefix)
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
