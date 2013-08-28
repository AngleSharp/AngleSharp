using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// The DOM Object representing the unordered list.
    /// </summary>
    [DOM("HTMLUListElement")]
    public sealed class HTMLUListElement : HTMLElement, IListScopeElement
    {
        internal HTMLUListElement()
        {
            _name = Tags.UL;
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
