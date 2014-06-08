namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML audio element.
    /// </summary>
    [DomName("HTMLAudioElement")]
    public sealed class HTMLAudioElement : HTMLMediaElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML audio element.
        /// </summary>
        internal HTMLAudioElement()
        {
            _name = Tags.Audio;
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
