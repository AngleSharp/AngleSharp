namespace AngleSharp.Dom.Css
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// Represents a CSS import rule.
    /// </summary>
    sealed class CssImportRule : CssRule, ICssImportRule
    {
        #region Fields

        readonly MediaList _media;

        String _href;
        ICssStyleSheet _styleSheet;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS import rule
        /// </summary>
        internal CssImportRule(CssParser parser)
            : base(CssRuleType.Import, parser)
        {
            _media = new MediaList(parser);
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
            get { return _media; }
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

        #region Internal Properties

        internal MediaList Media
        {
            get { return _media; }
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

        #region String Representation

        public override String ToCss(IStyleFormatter formatter)
        {
            var media = _media.MediaText;
            var space = String.IsNullOrEmpty(media) ? String.Empty : " ";
            var value = String.Concat(_href.CssUrl(), space, media);
            return formatter.Rule("@import", value);
        }

        #endregion
    }
}
