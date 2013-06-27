using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML audio element.
    /// </summary>
    [DOM("HTMLAudioElement")]
    public sealed class HTMLAudioElement : HTMLMediaElement
    {
        #region Constant

        /// <summary>
        /// The audio tag.
        /// </summary>
        internal const String Tag = "audio";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML audio element.
        /// </summary>
        internal HTMLAudioElement()
        {
            _name = Tag;
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return false; }
        }

        #endregion
    }
}
