using System;

namespace AngleSharp.DOM.Html
{
    public sealed class HTMLSpanElement : HTMLElement
    {
        public const string Tag = "span";

        internal HTMLSpanElement()
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
                return false;
            }
        }
    }
}
