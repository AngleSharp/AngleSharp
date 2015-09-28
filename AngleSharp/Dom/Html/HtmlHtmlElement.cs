namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;
    
    /// <summary>
    /// Represents the HTML html element.
    /// </summary>
    sealed class HtmlHtmlElement : HtmlElement, IHtmlHtmlElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML html tag.
        /// </summary>
        public HtmlHtmlElement(Document owner, String prefix = null)
            : base(owner, Tags.Html, prefix, NodeFlags.Special | NodeFlags.ImplicitelyClosed | NodeFlags.Scoped | NodeFlags.HtmlTableScoped | NodeFlags.HtmlTableSectionScoped)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the manifest attribute.
        /// </summary>
        public String Manifest 
        {
            get { return this.GetOwnAttribute(AttributeNames.Manifest); }
            set { this.SetOwnAttribute(AttributeNames.Manifest, value); }
        }

        #endregion
    }
}
