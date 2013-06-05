using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML div element.
    /// </summary>
    public sealed class HTMLDivElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The div element.
        /// </summary>
        public const string Tag = "div";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML div element.
        /// </summary>
        public HTMLDivElement()
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
