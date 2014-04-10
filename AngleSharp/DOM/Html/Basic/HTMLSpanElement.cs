namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML span element.
    /// </summary>
    [DOM("HTMLSpanElement")]
    public sealed class HTMLSpanElement : HTMLElement
    {
        #region ctor

        internal HTMLSpanElement()
        {
            _name = Tags.Span;
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return false; }
        }

        #endregion
    }
}
