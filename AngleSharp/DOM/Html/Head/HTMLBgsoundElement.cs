namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML bgsound element.
    /// </summary>
    sealed class HTMLBgsoundElement : HTMLElement
    {
        internal HTMLBgsoundElement()
        {
            _name = Tags.BGSOUND;
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }
    }
}
