namespace AngleSharp.Dom.Css
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Represents a CSS import rule.
    /// </summary>
    sealed class CssImportRule : CssRule, ICssImportRule
    {
        #region Fields

        MediaList _media;
        String _href;
        ICssStyleSheet _styleSheet;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS import rule
        /// </summary>
        internal CssImportRule()
            : base(CssRuleType.Import)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the location of the style sheet to be imported. 
        /// </summary>
        public String Href
        {
            get { return _href; }
            set { _href = value; }
        }

        /// <summary>
        /// Gets a list of media types for which this style sheet may be used.
        /// </summary>
        IMediaList ICssImportRule.Media
        {
            get { return _media ?? (_media = new MediaList()); }
        }

        public MediaList Media
        {
            get { return _media; }
            set { _media = value; }
        }

        /// <summary>
        /// Gets the style sheet referred to by this rule, if it has been loaded. 
        /// </summary>
        public ICssStyleSheet Sheet
        {
            get { return _styleSheet; }
            set { _styleSheet = value; }
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = rule as CssImportRule;
            _href = newRule._href;
            _media.Import(newRule._media);
            _styleSheet = newRule._styleSheet;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        protected override String ToCss()
        {
            var media = _media.MediaText;
            return String.Concat("@import ", _href.CssUrl(), media.Length > 0 ? " " : String.Empty, media);
        }

        #endregion
    }
}
