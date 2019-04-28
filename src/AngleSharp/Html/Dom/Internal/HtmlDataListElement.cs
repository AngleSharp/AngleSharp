namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents the HTML datalist element.
    /// </summary>
    sealed class HtmlDataListElement : HtmlElement, IHtmlDataListElement
    {
        #region Fields

        private HtmlCollection<IHtmlOptionElement> _options;

        #endregion

        #region ctor

        public HtmlDataListElement(Document owner, String prefix = null)
            : base(owner, TagNames.Datalist, prefix)
        {
        }

        #endregion

        #region Properties

        public IHtmlCollection<IHtmlOptionElement> Options => _options ?? (_options = new HtmlCollection<IHtmlOptionElement>(this));

        #endregion
    }
}
