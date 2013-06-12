using System;

namespace AngleSharp.DOM.Html
{
    public sealed class HTMLTableRowElement : HTMLElement
    {
        public const string Tag = "tr";

        internal HTMLTableRowElement()
        {
            _name = Tag;
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get
            {
                return true;
            }
        }
    }
}
