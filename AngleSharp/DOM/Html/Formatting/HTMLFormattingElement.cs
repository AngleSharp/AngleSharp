using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the object for HTML formatting (b, big, strike, code,
    /// em, i, s, small, strong, u, tt, nobr) elements.
    /// </summary>
    public class HTMLFormattingElement : HTMLElement
    {
        #region Constants 

        /// <summary>
        /// The b tag.
        /// </summary>
        internal const string BTag = "b";

        /// <summary>
        /// The big tag.
        /// </summary>
        internal const string BigTag = "big";

        /// <summary>
        /// The strike tag.
        /// </summary>
        internal const string StrikeTag = "strike";

        /// <summary>
        /// The code tag.
        /// </summary>
        internal const string CodeTag = "code";

        /// <summary>
        /// The em tag.
        /// </summary>
        internal const string EmTag = "em";

        /// <summary>
        /// The i tag.
        /// </summary>
        internal const string ITag = "i";

        /// <summary>
        /// The s tag.
        /// </summary>
        internal const string STag = "s";

        /// <summary>
        /// The small tag.
        /// </summary>
        internal const string SmallTag = "small";

        /// <summary>
        /// The strong tag.
        /// </summary>
        internal const string StrongTag = "strong";

        /// <summary>
        /// The u tag.
        /// </summary>
        internal const string UTag = "u";

        /// <summary>
        /// The tt tag.
        /// </summary>
        internal const string TtTag = "tt";

        /// <summary>
        /// The nobr tag.
        /// </summary>
        internal const string NobrTag = "nobr";

        #endregion

        #region ctor

        internal HTMLFormattingElement()
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get { return false; }
        }

        #endregion
    }
}
