using System;

namespace AngleSharp.DOM.Html
{
    public sealed class HTMLLegendElement : HTMLElement
    {
        public const string Tag = "legend";

        internal HTMLLegendElement()
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
