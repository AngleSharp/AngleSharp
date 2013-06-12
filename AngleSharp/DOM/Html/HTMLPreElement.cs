using System;

namespace AngleSharp.DOM.Html
{
    public sealed class HTMLPreElement : HTMLElement
    {
        public const string Tag = "pre";

        internal HTMLPreElement()
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
