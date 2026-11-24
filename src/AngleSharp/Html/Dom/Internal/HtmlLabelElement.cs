namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the HTML label element.
    /// </summary>
    sealed class HtmlLabelElement : HtmlElement, IHtmlLabelElement
    {
        #region ctor

        public HtmlLabelElement(Document owner, String? prefix = null)
            : base(owner, TagNames.Label, prefix)
        {
        }

        #endregion

        #region Helpers

        private static Boolean IsHiddenInput(IHtmlElement element) =>
            element is IHtmlInputElement input && input.Type.Isi(InputTypeNames.Hidden);

        /// <summary>
        /// Finds the labels of the given control in its tree (HTML 4.10.4).
        /// </summary>
        internal static NodeList FindLabels(Element control)
        {
            var labels = new NodeList();

            foreach (var node in control.GetRoot().GetDescendants())
            {
                if (node is HtmlLabelElement label && label.Control == control)
                {
                    labels.Add(label);
                }
            }

            return labels;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the control that the label is assigned for if any.
        /// </summary>
        public IHtmlElement? Control
        {
            get
            {
                var controlId = HtmlFor;

                if (controlId is not null)
                {
                    var control = controlId.Length > 0 ? Owner.GetElementById(controlId) as IHtmlElement : null;
                    return control is ILabelabelElement ? control : null;
                }

                // without a for attribute it is the first labelable descendant (HTML 4.10.4)
                foreach (var descendant in this.GetDescendants())
                {
                    if (descendant is ILabelabelElement && descendant is IHtmlElement element && !IsHiddenInput(element))
                    {
                        return element;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Gets or sets the ID of the labeled control. Reflects the for attribute.
        /// </summary>
        public String? HtmlFor
        {
            get => this.GetOwnAttribute(AttributeNames.For);
            set => this.SetOwnAttribute(AttributeNames.For, value);
        }

        /// <summary>
        /// Gets the form element that the label is assigned for if any.
        /// </summary>
        public IHtmlFormElement? Form => GetAssignedForm();

        #endregion
    }
}
