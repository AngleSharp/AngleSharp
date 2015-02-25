namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML modifier (ins / del) element.
    /// </summary>
    sealed class HtmlModElement : HtmlElement, IHtmlModElement
    {
        #region ctor

        public HtmlModElement(Document owner, String name)
            : base(owner, name)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value that contains a URI of a resource
        /// explaining the change.
        /// </summary>
        public String Citation
        {
            get { return GetAttribute(AttributeNames.Cite); }
            set { SetAttribute(AttributeNames.Cite, value); }
        }

        /// <summary>
        /// Gets or sets the value that contains date-and-time string
        /// representing a timestamp for the change.
        /// </summary>
        public String DateTime
        {
            get { return GetAttribute(AttributeNames.Datetime); }
            set { SetAttribute(AttributeNames.Datetime, value); }
        }

        #endregion
    }
}
