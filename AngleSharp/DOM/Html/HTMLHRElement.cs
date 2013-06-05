using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the hr element.
    /// </summary>
    public sealed class HTMLHRElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The hr tag.
        /// </summary>
        public const string Tag = "hr";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new hr element.
        /// </summary>
        public HTMLHRElement()
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
