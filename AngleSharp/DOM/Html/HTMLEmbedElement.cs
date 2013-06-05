using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the embed element.
    /// </summary>
    public class HTMLEmbedElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The embed tag.
        /// </summary>
        public const string Tag = "embed";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new Embed element.
        /// </summary>
        public HTMLEmbedElement()
        {
            _name = Tag;
        }

        #endregion

        #region Internal properties

        protected internal override bool IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}
