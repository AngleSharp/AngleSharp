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

        public HtmlDataListElement(Document owner, String prefix = null)
            : base(owner, TagNames.Datalist, prefix)
        {
        }

        #endregion

        #region Properties

        public IHtmlCollection<IHtmlOptionElement> Options
        {
            get { return _options ?? (_options = new HtmlCollection<IHtmlOptionElement>(this)); }
        }

        #endregion
    }
}
