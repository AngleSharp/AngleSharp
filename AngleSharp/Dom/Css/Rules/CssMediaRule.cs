namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Dom.Collections;
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// Represents a CSS @media rule.
    /// </summary>
    sealed class CssMediaRule : CssConditionRule, ICssMediaRule
    {
        #region Fields

        readonly MediaList _media;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS @media rule with a new media list.
        /// </summary>
        internal CssMediaRule(CssParser parser)
            : base(CssRuleType.Media, parser)
        {
            _media = new MediaList(parser);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the media condition.
        /// </summary>
        public String ConditionText
        {
            get { return _media.MediaText; }
            set { _media.MediaText = value; }
        }

        /// <summary>
        /// Gets a list of media types for this rule.
        /// </summary>
        IMediaList ICssMediaRule.Media
        {
            get { return _media; }
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
            base.ReplaceWith(rule);
            var newRule = rule as CssMediaRule;
            _media.Import(newRule._media);
        }

        internal override Boolean IsValid(RenderDevice device)
        {
            return _media.Validate(device);
        }

        #endregion

        #region String Representation

        public override String ToCss(IStyleFormatter formatter)
        {
            var rules = formatter.Block(Rules);
            return formatter.Rule("@media", _media.MediaText, rules);
        }

        #endregion
    }
}
