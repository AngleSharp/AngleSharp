namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML label element.
    /// </summary>
    [DomName("HTMLLabelElement")]
    public sealed class HTMLLabelElement : HTMLElement
    {
        #region ctor

        internal HTMLLabelElement()
        {
            _name = Tags.Label;
        }

        #endregion

        #region Internal properties

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
        /// Gets or sets the accesskey HTML attribute.
        /// </summary>
        [DomName("accessKey")]
        public String AccessKey
        {
            get { return GetAttribute(AttributeNames.AccessKey); }
            set { SetAttribute(AttributeNames.AccessKey, value); }
        }

        /// <summary>
        /// Gets the control that the label is assigned for if any.
        /// </summary>
        [DomName("control")]
        public ILabelabelElement Control
        {
            get
            {
                var controlId = HtmlFor;

                if (!String.IsNullOrEmpty(controlId))
                {
                    var control = _owner.GetElementById(controlId);

                    if (control is ILabelabelElement)
                        return (ILabelabelElement)control;
                }
                
                return null;
            }
        }

        /// <summary>
        /// Gets or sets the ID of the labeled control. Reflects the for attribute.
        /// </summary>
        [DomName("htmlFor")]
        public String HtmlFor
        {
            get { return GetAttribute(AttributeNames.For); }
            set { SetAttribute(AttributeNames.For, value); }
        }

        /// <summary>
        /// Gets the form element that the label is assigned for if any.
        /// </summary>
        [DomName("form")]
        public IHtmlFormElement Form
        {
            get { return GetAssignedForm(); }
        }

        #endregion
    }
}
