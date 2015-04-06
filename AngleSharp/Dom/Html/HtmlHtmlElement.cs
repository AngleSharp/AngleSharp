namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;
    
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
            get { return GetOwnAttribute(AttributeNames.Manifest); }
            set { SetOwnAttribute(AttributeNames.Manifest, value); }
        }

        #endregion
    }
}
