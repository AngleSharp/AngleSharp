namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML modifier (ins / del) element.
    /// </summary>
    sealed class HtmlModElement : HtmlElement, IHtmlModElement
    {
        #region ctor

        public HtmlModElement(Document owner, String name = null, String prefix = null)
            : base(owner, name ?? TagNames.Ins, prefix)
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
            get { return this.GetOwnAttribute(AttributeNames.Cite); }
            set { this.SetOwnAttribute(AttributeNames.Cite, value); }
        }

        /// <summary>
        /// Gets or sets the value that contains date-and-time string
        /// representing a timestamp for the change.
        /// </summary>
        public String DateTime
        {
            get { return this.GetOwnAttribute(AttributeNames.Datetime); }
            set { this.SetOwnAttribute(AttributeNames.Datetime, value); }
        }

        #endregion
    }
}
