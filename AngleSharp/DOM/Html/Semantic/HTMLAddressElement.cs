namespace AngleSharp.DOM.Html
{
    using System;

    sealed class HTMLAddressElement : HTMLElement
    {
        internal HTMLAddressElement()
        {
            _name = Tags.ADDRESS;
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
