namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML datalist element.
    /// </summary>
    sealed class HtmlDataListElement : HtmlElement, IHtmlDataListElement
    {
        #region Fields

        HtmlCollection<IHtmlOptionElement> _options;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new datalist element.
        /// </summary>
        public HtmlDataListElement(Document owner)
            : base(owner, Tags.Datalist)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a collection whose filter matches option elements.
        /// </summary>
        public IHtmlCollection<IHtmlOptionElement> Options
        {
            get { return _options ?? (_options = new HtmlCollection<IHtmlOptionElement>(this)); }
        }

        #endregion
    }
}
