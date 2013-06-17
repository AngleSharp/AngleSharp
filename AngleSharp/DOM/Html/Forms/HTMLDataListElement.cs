using AngleSharp.DOM.Collections;
using AngleSharp.DOM.Css;
using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML datalist element.
    /// </summary>
    public sealed class HTMLDataListElement : HTMLElement
    {
        #region Constants

        /// <summary>
        /// The datalist tag.
        /// </summary>
        internal const string Tag = "datalist";

        static readonly SimpleSelector optionsQuery = SimpleSelector.Type(HTMLOptionElement.Tag);

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new datalist element.
        /// </summary>
        internal HTMLDataListElement()
        {
            _name = Tag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a collection whose filter matches option elements.
        /// </summary>
        public HTMLCollection Options
        {
            get { return _children.QuerySelectorAll(optionsQuery); }
        }

        #endregion
    }
}
