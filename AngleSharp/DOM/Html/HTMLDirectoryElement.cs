namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML dir element.
    /// This element is obsolete since HTML 4.01.
    /// </summary>
    [DomHistorical]
    sealed class HTMLDirectoryElement : HTMLElement
    {
        #region ctor

        internal HTMLDirectoryElement()
        {
            _name = Tags.Dir;
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
