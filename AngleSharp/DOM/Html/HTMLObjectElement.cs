using System;

namespace AngleSharp.DOM.Html
{
    public sealed class HTMLObjectElement : HTMLElement
    {
        public const string Tag = "object";

        internal HTMLObjectElement()
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
