namespace AngleSharp.DOM.Css
{
    using AngleSharp.DOM.Collections;
    using System;

    /// <summary>
    /// Represents a CSS import rule.
    /// </summary>
    [DomName("CSSImportRule")]
    public sealed class CSSImportRule : CSSRule
    {
        #region Fields

        String _href;
        IMediaList _media;
        ICssStyleSheet _styleSheet;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS import rule
        /// </summary>
        internal CSSImportRule()
        {
            _media = MediaList.Empty;
            _type = CssRuleType.Import;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the location of the style sheet to be imported. 
        /// </summary>
        [DomName("href")]
        public String Href
        {
            get { return _href; }
            internal set { _href = value; }
        }

        /// <summary>
        /// Gets a list of media types for which this style sheet may be used.
        /// </summary>
        [DomName("media")]
        public IMediaList Media
        {
            get { return _media; }
            internal set { _media = value; }
        }

        /// <summary>
        /// Gets the style sheet referred to by this rule, if it has been loaded. 
        /// </summary>
        [DomName("styleSheet")]
        public ICssStyleSheet StyleSheet
        {
            get { return _styleSheet; }
            internal set { _styleSheet = value; }
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = rule as CSSImportRule;
            _href = newRule._href;
            _media = newRule._media;
            _styleSheet = newRule._styleSheet;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return String.Format("@import url('{0}') {1};", _href, _media.MediaText);
        }

        #endregion
    }
}
