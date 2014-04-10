namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML dl element.
    /// </summary>
    [DOM("HTMLDListElement")]
    public sealed class HTMLDListElement : HTMLElement
    {
        #region ctor

        internal HTMLDListElement()
        {
            _name = Tags.Dl;
        }

        #endregion

        #region Properties

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
