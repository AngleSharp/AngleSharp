namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML isindex element.
    /// </summary>
    sealed class HTMLIsIndexElement : HTMLElement
    {
        #region ctor

        /// <summary>
        /// Creates a new isindex element.
        /// </summary>
        internal HTMLIsIndexElement()
        {
            _name = Tags.IsIndex;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the form element containing this control or null if this control is not within the context of a form.
        /// </summary>
        public IHtmlFormElement Form
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the prompt message.
        /// </summary>
        public String Prompt
        {
            get { return GetAttribute(AttributeNames.Prompt); }
            set { SetAttribute(AttributeNames.Prompt, value); }
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}
