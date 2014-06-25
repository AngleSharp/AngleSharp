namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML div element.
    /// </summary>
    sealed class HTMLDivElement : HTMLElement, IHtmlDivElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML div element.
        /// </summary>
        internal HTMLDivElement()
        {
            _name = Tags.Div;
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}
