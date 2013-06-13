using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents a no* HTML element (noembed, noscript, noframes).
    /// </summary>
    sealed class HTMLNoElement : HTMLElement
    {
        #region Constants

        /// <summary>
        /// The noembed tag.
        /// </summary>
        internal const string NoEmbedTag = "noembed";

        /// <summary>
        /// The noscript tag.
        /// </summary>
        internal const string NoScriptTag = "noscript";

        /// <summary>
        /// The noframes tag.
        /// </summary>
        internal const string NoFramesTag = "noframes";

        #endregion

        #region ctor

        internal HTMLNoElement()
        {
            _name = NoScriptTag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}
