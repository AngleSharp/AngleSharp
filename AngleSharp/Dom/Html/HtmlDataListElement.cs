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

        readonly HtmlCollection<HtmlOptionElement> _options;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new datalist element.
        /// </summary>
        public HtmlDataListElement(Document owner)
            : base(owner, Tags.Datalist)
        {
            _options = new HtmlCollection<HtmlOptionElement>(this);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a collection whose filter matches option elements.
        /// </summary>
        public IHtmlCollection Options
        {
            get { return _options; }
        }

        #endregion
    }
}
