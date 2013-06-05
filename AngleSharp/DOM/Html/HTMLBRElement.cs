using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML br element.
    /// </summary>
    public sealed class HTMLBRElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The br tag.
        /// </summary>
        public const string Tag = "br";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML br element
        /// </summary>
        public HTMLBRElement()
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
