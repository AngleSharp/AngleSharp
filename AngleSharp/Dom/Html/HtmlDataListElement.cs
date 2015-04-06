namespace AngleSharp.Dom.Html
{
    using System;
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
        public HtmlDataListElement(Document owner, String prefix = null)
            : base(owner, Tags.Datalist, prefix)
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
