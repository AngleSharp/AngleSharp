namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML pre element.
    /// </summary>
    [DomName("HTMLPreElement")]
    public sealed class HTMLPreElement : HTMLElement
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
