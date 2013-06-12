using System;

namespace AngleSharp.DOM.Html
{
    public sealed class HTMLLabelElement : HTMLElement
    {
        public const string Tag = "label";

        internal HTMLLabelElement()
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
