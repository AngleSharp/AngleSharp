namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML head element.
    /// </summary>
    [DomName("HTMLHeadElement")]
    public sealed class HTMLHeadElement : HTMLElement
    {
        #region ctor

        internal HTMLHeadElement()
        {
            _name = Tags.Head;
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