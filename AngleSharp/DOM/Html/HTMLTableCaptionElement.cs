using System;

namespace AngleSharp.DOM.Html
{
    public sealed class HTMLTableCaptionElement : HTMLElement
    {
        public const string Tag = "caption";

        internal HTMLTableCaptionElement()
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
