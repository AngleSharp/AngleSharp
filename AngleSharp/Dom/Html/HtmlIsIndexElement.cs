namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML isindex element.
    /// </summary>
    sealed class HtmlIsIndexElement : HtmlElement
    {
        #region ctor

        /// <summary>
        /// Creates a new isindex element.
        /// </summary>
        public HtmlIsIndexElement(Document owner)
            : base(owner, Tags.IsIndex, NodeFlags.Special)
        {
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
            get { return GetOwnAttribute(AttributeNames.Prompt); }
            set { SetOwnAttribute(AttributeNames.Prompt, value); }
        }

        #endregion
    }
}
