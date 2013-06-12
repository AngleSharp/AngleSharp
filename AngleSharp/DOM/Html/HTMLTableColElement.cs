using System;

namespace AngleSharp.DOM.Html
{
    public sealed class HTMLTableColElement : HTMLElement
    {
        public const string ColTag = "col";
        public const string ColgroupTag = "colgroup";

        internal HTMLTableColElement()
        {
            _name = ColTag;
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
