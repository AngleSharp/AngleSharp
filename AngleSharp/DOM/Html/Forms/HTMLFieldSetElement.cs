namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.Extensions;
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
        public HTMLFieldSetElement(Document owner)
            : base(owner, Tags.Fieldset)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type of input control (fieldset).
        /// </summary>
        public String Type
        {
            get { return Tags.Fieldset; }
        }

        /// <summary>
        /// Gets the elements belonging to this field set.
        /// </summary>
        public IHtmlFormControlsCollection Elements
        {
            get { return new HtmlFormControlsCollection(Form, this); }
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
            RegisterAttributeHandler(AttributeNames.Disabled, UpdateDisabled);
            UpdateDisabled(GetAttribute(AttributeNames.Disabled));
        }

        void UpdateDisabled(String value)
        {
            if (value != null)
            {
                var firstLegend = Children.FirstOrDefault(m => m is HTMLLegendElement);

                foreach (var element in Elements.OfType<HTMLFormControlElement>())
                {
                    if (element.IsDescendantOf(firstLegend) == false)
                        element.IsDisabled = true;
                }
            }
        }

        #endregion
    }
}
