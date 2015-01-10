namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML datalist element.
    /// </summary>
    sealed class HTMLDataListElement : HTMLElement, IHtmlDataListElement
    {
        #region Fields

        readonly HtmlCollection<HTMLOptionElement> _options;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new datalist element.
        /// </summary>
        public HTMLDataListElement(Document owner)
            : base(owner, Tags.Datalist)
        {
            _options = new HtmlCollection<HTMLOptionElement>(this);
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
