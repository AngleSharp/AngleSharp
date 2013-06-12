using System;

namespace AngleSharp.DOM.Html
{
    public sealed class HTMLOListElement : HTMLElement
    {
        public const string Tag = "ol";

        internal HTMLOListElement()
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
