using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML audio element.
    /// </summary>
    public sealed class HTMLAudioElement : HTMLMediaElement
    {
        /// <summary>
        /// The audio tag.
        /// </summary>
        internal const string Tag = "audio";

        /// <summary>
        /// Creates a new HTML audio element.
        /// </summary>
        internal HTMLAudioElement()
        {
            _name = Tag;
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get { return false; }
        }
    }
}
