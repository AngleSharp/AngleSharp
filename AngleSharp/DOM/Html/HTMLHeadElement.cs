using System;

namespace AngleSharp.DOM.Html
{
    public sealed class HTMLHeadElement : HTMLElement
    {
        public const string Tag = "head";

        internal HTMLHeadElement()
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