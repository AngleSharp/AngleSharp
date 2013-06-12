using System;

namespace AngleSharp.DOM.Html
{
    public sealed class HTMLTableSectionElement : HTMLElement
    {
        public const string HeadTag = "thead";
        public const string BodyTag = "tbody";
        public const string FootTag = "tfoot";

        internal HTMLTableSectionElement()
        {
            _name = BodyTag;
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
