using System;

namespace AngleSharp.DOM.Html
{
    public sealed class HTMLModElement : HTMLElement
    {
        public const string InsTag = "ins";
        public const string DelTag = "del";

        internal HTMLModElement()
        {
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
