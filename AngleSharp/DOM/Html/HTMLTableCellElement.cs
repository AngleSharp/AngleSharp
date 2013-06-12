using System;

namespace AngleSharp.DOM.Html
{
    public sealed class HTMLTableCellElement : HTMLElement
    {
        public const string HeadTag = "th";
        public const string NormalTag = "td";

        internal HTMLTableCellElement()
        {
            _name = NormalTag;
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
