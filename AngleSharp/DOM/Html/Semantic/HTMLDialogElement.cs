namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the object for HTML dialog elements.
    /// </summary>
    [DOM("HTMLDialogElement")]
    public sealed class HTMLDialogElement : HTMLElement
    {
        internal HTMLDialogElement()
        {
            _name = Tags.Dialog;
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return false; }
        }
    }
}
