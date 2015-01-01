namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.Html;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents the HTML fieldset element.
    /// </summary>
    sealed class HTMLFieldSetElement : HTMLFormControlElement, IHtmlFieldSetElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML fieldset element.
        /// </summary>
        internal HTMLFieldSetElement()
            : base(Tags.Fieldset)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type of input control (fieldset).
        /// </summary>
        public String Type
        {
            get { return NodeName; }
        }

        /// <summary>
        /// Gets the elements belonging to this field set.
        /// </summary>
        public IHtmlFormControlsCollection Elements
        {
            get { return new HtmlFormControlsCollection(this); }
        }

        #endregion

        #region Methods

        protected override Boolean CanBeValidated()
        {
            return true;
        }

        internal override void Close()
        {
            base.Close();
            RegisterAttributeHandler(AttributeNames.Disabled, value =>
            {
                if (value != null)
                {
                    var firstLegend = Children.FirstOrDefault(m => m is HTMLLegendElement);

                    foreach (var element in Elements.OfType<HTMLFormControlElement>())
                    {
                        if (element.ParentElement != firstLegend)
                            element.IsDisabled = true;
                    }
                }
            });
        }

        #endregion
    }
}
