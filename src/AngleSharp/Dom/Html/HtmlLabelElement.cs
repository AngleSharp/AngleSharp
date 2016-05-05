namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML label element.
    /// </summary>
    sealed class HtmlLabelElement : HtmlElement, IHtmlLabelElement
    {
        #region ctor

        public HtmlLabelElement(Document owner, String prefix = null)
            : base(owner, TagNames.Label, prefix)
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
            get { return this.GetOwnAttribute(AttributeNames.For); }
            set { this.SetOwnAttribute(AttributeNames.For, value); }
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
