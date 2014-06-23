namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML pre element.
    /// </summary>
    sealed class HTMLPreElement : HTMLElement, IHtmlPreElement
    {
        #region ctor

        internal HTMLPreElement()
        {
            _name = Tags.Pre;
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
