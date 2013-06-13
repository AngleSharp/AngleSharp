using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML map element.
    /// </summary>
    public sealed class HTMLMapElement : HTMLElement
    {
        /// <summary>
        /// The map tag.
        /// </summary>
        internal const string Tag = "map";

        internal HTMLMapElement()
        {
            _name = Tag;
        }
    }
}
