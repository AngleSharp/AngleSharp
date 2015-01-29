namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML label element.
    /// </summary>
    sealed class HTMLLabelElement : HTMLElement, IHtmlLabelElement
    {
        #region ctor

        public HTMLLabelElement(Document owner)
            : base(owner, Tags.Label)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the control that the label is assigned for if any.
        /// </summary>
        public IHtmlElement Control
        {
            get
            {
                var controlId = HtmlFor;

                if (!String.IsNullOrEmpty(controlId))
                {
                    var control = Owner.GetElementById(controlId) as IHtmlElement;

                    if (control is ILabelabelElement)
                        return control;
                }
                
                return null;
            }
        }

        /// <summary>
        /// Gets or sets the ID of the labeled control. Reflects the for attribute.
        /// </summary>
        public String HtmlFor
        {
            get { return GetAttribute(AttributeNames.For); }
            set { SetAttribute(AttributeNames.For, value); }
        }

        /// <summary>
        /// Gets the form element that the label is assigned for if any.
        /// </summary>
        public IHtmlFormElement Form
        {
            get { return GetAssignedForm(); }
        }

        #endregion
    }
}
