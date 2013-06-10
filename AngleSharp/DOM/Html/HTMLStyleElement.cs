using AngleSharp.DOM.Css;
using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML style element.
    /// </summary>
    public class HTMLStyleElement : HTMLRawtextElement, IStyleSheet
    {
        #region Constant

        /// <summary>
        /// The style tag.
        /// </summary>
        public const string Tag = "style";

        #endregion

        #region Members

        StyleSheet _sheet;

        #endregion

        #region ctor

        /// <summary>
        /// Creates an HTML style element.
        /// </summary>
        public HTMLStyleElement()
        {
            NodeName = Tag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the associated style sheet.
        /// </summary>
        public StyleSheet Sheet
        {
            get { return _sheet ?? (_sheet = Builder.Style(this)); }
        }

        /// <summary>
        /// Gets or sets if the style is enabled or disabled.
        /// </summary>
        public bool Disabled
        {
            get { return Sheet.Disabled; }
            set { Sheet.Disabled = value; }
        }

        /// <summary>
        /// Gets or sets the use with one or more target media.
        /// </summary>
        public string Media
        {
            get { return GetAttribute("media"); }
            set { SetAttribute("media", value); }
        }

        /// <summary>
        /// Gets or sets the content type of the style sheet language.
        /// </summary>
        public string Type
        {
            get { return GetAttribute("type"); }
            set { SetAttribute("type", value); }
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
