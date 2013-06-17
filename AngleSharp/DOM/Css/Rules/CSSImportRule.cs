using AngleSharp.DOM.Collections;
using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a CSS import rule.
    /// </summary>
    public sealed class CSSImportRule : CSSRule
    {
        #region Constants

        internal const String RuleName = "import";

        #endregion

        #region Members

        String _href;
        MediaList _media;
        CSSStyleSheet _styleSheet;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS import rule
        /// </summary>
        internal CSSImportRule()
        {
            _media = new MediaList();
            _type = CssRule.Import;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the location of the style sheet to be imported. 
        /// </summary>
        public String Href
        {
            get { return _href; }
            internal set { _href = value; }
        }

        /// <summary>
        /// Gets a list of media types for which this style sheet may be used.
        /// </summary>
        public MediaList Media
        {
            get { return _media; }
        }

        /// <summary>
        /// Gets the style sheet referred to by this rule, if it has been loaded. 
        /// </summary>
        public CSSStyleSheet StyleSheet
        {
            get { return _styleSheet; }
            internal set { _styleSheet = value; }
        }

        #endregion
    }
}
