namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;

    /// <summary>
    /// Represents the HTML datalist element.
    /// </summary>
    [DomName("HTMLDataListElement")]
    public sealed class HTMLDataListElement : HTMLElement
    {
        #region Fields

        HTMLCollection<HTMLOptionElement> _options;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new datalist element.
        /// </summary>
        internal HTMLDataListElement()
        {
            _name = Tags.Datalist;
            _options = new HTMLCollection<HTMLOptionElement>(this);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a collection whose filter matches option elements.
        /// </summary>
        [DomName("options")]
        public HTMLCollection<HTMLOptionElement> Options
        {
            get { return _options; }
        }

        #endregion
    }
}
