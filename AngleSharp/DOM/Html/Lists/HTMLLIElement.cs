namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents an HTML li, dd or dt tag.
    /// </summary>
    [DomName("HTMLLIElement")]
    public sealed class HTMLLIElement : HTMLElement, IImpliedEnd
    {
        #region ctor

        /// <summary>
        /// Creates a new item tag.
        /// </summary>
        internal HTMLLIElement()
        {
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
