using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the object for HTML dialog elements.
    /// </summary>
    public sealed class HTMLDialogElement : HTMLElement
    {
        /// <summary>
        /// The dialog tag.
        /// </summary>
        internal const string Tag = "dialog";

        internal HTMLDialogElement()
        {
            _name = Tag;
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get { return false; }
        }
    }
}
