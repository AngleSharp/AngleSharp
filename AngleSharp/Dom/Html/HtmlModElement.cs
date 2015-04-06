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

        public HtmlModElement(Document owner)
            : this(owner, Tags.Ins)
        {
        }

        public HtmlModElement(Document owner, String name, String prefix = null)
            : base(owner, name, prefix)
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
            get { return GetOwnAttribute(AttributeNames.Cite); }
            set { SetOwnAttribute(AttributeNames.Cite, value); }
        }

        /// <summary>
        /// Gets or sets the value that contains date-and-time string
        /// representing a timestamp for the change.
        /// </summary>
        public String DateTime
        {
            get { return GetOwnAttribute(AttributeNames.Datetime); }
            set { SetOwnAttribute(AttributeNames.Datetime, value); }
        }

        #endregion
    }
}
