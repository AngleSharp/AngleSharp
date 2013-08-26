using AngleSharp.DOM.Collections;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML datalist element.
    /// </summary>
    [DOM("HTMLDataListElement")]
    public sealed class HTMLDataListElement : HTMLElement
    {
        #region Members

        HTMLLiveCollection<HTMLOptionElement> _options;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new datalist element.
        /// </summary>
        internal HTMLDataListElement()
        {
            _name = Tags.DATALIST;
            _options = new HTMLLiveCollection<HTMLOptionElement>(this);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a collection whose filter matches option elements.
        /// </summary>
        [DOM("options")]
        public HTMLCollection Options
        {
            get { return _options; }
        }

        #endregion
    }
}
