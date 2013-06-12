using System;

namespace AngleSharp.DOM.Html
{
    public sealed class HTMLTableElement : HTMLElement
    {
        public const string Tag = "table";

        internal HTMLTableElement()
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
