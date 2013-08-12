using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the object for HTML dialog elements.
    /// </summary>
    [DOM("HTMLDialogElement")]
    public sealed class HTMLDialogElement : HTMLElement
    {
        /// <summary>
        /// The dialog tag.
        /// </summary>
        internal const String Tag = "dialog";

        internal HTMLDialogElement()
        {
            _name = Tag;
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
