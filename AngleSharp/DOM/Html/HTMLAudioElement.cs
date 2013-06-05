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
        public const string Tag = "audio";

        /// <summary>
        /// Creates a new HTML audio element.
        /// </summary>
        public HTMLAudioElement()
        {
            _name = Tag;
        }

        protected internal override bool IsSpecial
        {
            get { return false; }
        }
    }
}
